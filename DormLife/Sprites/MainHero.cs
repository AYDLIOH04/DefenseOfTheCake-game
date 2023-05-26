using DormLife.GameObjects;
using DormLife.Managers;
using DormLife.Models;
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
        public MainHero(Texture2D texture, Vector2 position, float speed) : base(texture, position, speed)
        { }

        private void Fire()
        {
            if (InputManager.MouseLeftClicked)
            {
                ProjectileData pd = new()
                {
                    Position = position,
                    Rotation = rotation,
                    Lifespan = 3,
                    Speed = 600,
                };
                ProjectileManager.AddProjectile(pd);
            } 

            if (InputManager.MouseRightClicked)
            {
                ProjectileData pd = new()
                {
                    Position = position,
                    Rotation = rotation,
                    Lifespan = 3,
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
            Move(walls, cake);
            Fire();
        }
    }
}
