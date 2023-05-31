using DormLife.Managers;
using DormLife.Models;
using DormLife.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.GameObjects
{
    public class Turret : Sprite
    {
        private float _eventTimer;
        private float _shootingDelay;

        public TimeSpan damageTime;

        private float _hpTimer;
        private float _healTimer = 10f;
        private float _healDelay = 0.6f;


        public float MaxHP { get; private set; }
        public float HP { get; private set; }
        public Turret(Texture2D texture, Vector2 position, float speed = 0)
        : base(texture, position, speed)
        {
            _eventTimer = 0;
            _shootingDelay = 0.5f;

            HP = 50;
            MaxHP = 50;
        }

        public void PassiveHeal()
        {
            if ((Globals.GameTime.TotalGameTime - damageTime).TotalSeconds >= _healTimer)
            {
                if (HP < 50)
                {
                    _hpTimer += Globals.TotalSeconds;


                    if (_hpTimer > _healDelay)
                    {
                        HP += 5;
                        _hpTimer = 0;
                    }
                }
                if (HP > 50)
                {
                    HP = MaxHP;
                }
            }
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
            damageTime = Globals.GameTime.TotalGameTime;
        }

        public void Update(List<Enemy> enemies)
        {
            _eventTimer += Globals.TotalSeconds;

            PassiveHeal();

            foreach (var enemy in enemies)
            {                                                               
                if (CheckVectorCollision(enemy, 500))
                {
                    if (_eventTimer > _shootingDelay)
                    {
                        Vector2 toEnemy = enemy.position - position;
                        rotation = (float)Math.Atan2(toEnemy.Y, toEnemy.X);

                        ProjectileData pd = new ProjectileData()
                        {
                            Position = new(Rectangle.X + Rectangle.Width / 2 - 10, Rectangle.Y + Rectangle.Height / 2 - 10),
                            Rotation = rotation,
                            Lifespan = 3.5f,
                            Speed = 600,
                        };

                        ProjectileManager.AddTurretProjectile(pd);
                        _eventTimer = 0;
                    }
                }
            }
        }

        public Turret Clone()
        {
            return new Turret(texture, new(position.X + texture.Width / 2, position.Y + texture.Height / 2))
            {
                HP = this.HP
            };
        }
    }
}
