using DormLife.GameObjects;
using DormLife.Managers;
using DormLife.Models;
using DormLife.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Sprites
{
    public class MainHero : Sprite
    {
        private float _eventTimer;
        private float _shootingDelay;
        private float _spawnDelay;

        private bool _isShooting;


        public MainHero(Texture2D texture, Vector2 position, float speed) : base(texture, position, speed)
        {
            _eventTimer = 0;
            _shootingDelay = 0.3f;
            _spawnDelay = 0.3f;
            _isShooting = false;

        }

        public void IncrementShootingDelay()
        {
             _shootingDelay -= 0.04f;
        }

        
        private void CreateTrap()
        {
            if (_eventTimer > _spawnDelay && InputManager.IsCKeyPressed)
            {
                if (TrapManager.currentHaveCount > 0)
                {
                    GameState.scoreTrapCount.DecrementScore(1);
                    TrapManager.AddTrap(new(position.X + texture.Width / 2, position.Y + texture.Height / 2), rotation);
                }
                _eventTimer = 0;
            }
        }

        private void CreateSlowlyTrap()
        {
            if (_eventTimer > _spawnDelay && InputManager.IsXKeyPressed)
            {
                if (SlowlyTrapManager.currentHaveCount > 0)
                {
                    GameState.scoreSlowlyTrapCount.DecrementScore(1);
                    SlowlyTrapManager.AddSlowlyTrap(new(position.X + texture.Width / 2, position.Y + texture.Height / 2), rotation);
                }
                _eventTimer = 0;
            }
        }


        private void CreateTurret()
        {
            if (_eventTimer > _spawnDelay && InputManager.IsZKeyPressed)
            {
                if (TurretManager.currentHaveCount > 0)
                {
                    GameState.scoreTurretCount.DecrementScore(1);
                    TurretManager.AddTurret(new(position.X + texture.Width / 2, position.Y + texture.Height / 2), rotation);
                }
                _eventTimer = 0;
            }
        }

        private void Fire()
        {
            ProjectileData pd = new ProjectileData()
            {
                Position = position,
                Rotation = rotation,
                Lifespan = 3.5f,
                Speed = 600,
            };

            if (InputManager.MouseLeftPressed)
            {
                if (!_isShooting && _eventTimer > _shootingDelay)
                {
                    ProjectileManager.AddProjectile(pd);
                    _isShooting = true;
                    _eventTimer = 0;
                }
                else if (_isShooting && _eventTimer > _shootingDelay)
                {
                    ProjectileManager.AddProjectile(pd);
                    _eventTimer = 0;
                }
            }
            else
            {
                _isShooting = false;
            }
        }

        private void FireUlt()
        {
            if (InputManager.MouseRightClicked)
            {
                ProjectileData pd = new()
                {
                    Position = position,
                    Rotation = rotation,
                    Lifespan = 3.5f,
                    Speed = 800,
                };
                ProjectileManager.AddUltProjectile(pd);
            }
        }

        private void Move(List<Wall> walls, Cake cake)
        {
            if (InputManager.Direction != Vector2.Zero)
            {
                var dir = CheckCollisionAndCreateDir(walls, cake);

                position = new(
                        MathHelper.Clamp(position.X + (dir.X * speed * Globals.TotalSeconds), 30, Globals.Bounds.X - 30),
                        MathHelper.Clamp(position.Y + (dir.Y * speed * Globals.TotalSeconds), 30, Globals.Bounds.Y - 30)
                    );
            }

            var toMouse = InputManager.MousePosition - position;
            rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);
        }

        private Vector2 CheckCollisionAndCreateDir(List<Wall> walls, Cake cake)
        {
            var dir = Vector2.Normalize(InputManager.Direction);

            if ((dir.X > 0 && IsTouchingLeft(cake, dir)) ||
               (dir.X < 0 && IsTouchingRight(cake, dir)))
                dir.X = 0;

            if ((dir.Y > 0 && IsTouchingTop(cake, dir)) ||
                (dir.Y < 0 && IsTouchingBottom(cake, dir)))
                dir.Y = 0;
                                                      
            foreach (var wall in walls)
            {
                if ((dir.X > 0 && IsTouchingLeft(wall, dir)) ||
               (dir.X < 0 && IsTouchingRight(wall, dir)))
                    dir.X = 0;

                if ((dir.Y > 0 && IsTouchingTop(wall, dir)) ||
                    (dir.Y < 0 && IsTouchingBottom(wall, dir)))
                    dir.Y = 0;
            }

            return dir;
        }

        public void Update(List<Wall> walls, Cake cake)
        {
            _eventTimer += Globals.TotalSeconds;
            CreateTrap();
            CreateTurret();
            CreateSlowlyTrap();
            Move(walls, cake);
            Fire();
            FireUlt();
        }
    }
}
