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
            if (InputManager.MouseClicked)
            {
                ProjectileData pd = new()
                {
                    Position = position,
                    Rotation = rotation,
                    Lifespan = 2,
                    Speed = 600,
                };
                ProjectileManager.AddProjectile(pd);
            }
        }

        private void Move()
        {
            if (InputManager.Direction != Vector2.Zero)
            {
                var dir = Vector2.Normalize(InputManager.Direction);
                position = new(
                        MathHelper.Clamp(position.X + (dir.X * speed * Globals.TotalSeconds), 30, Globals.Bounds.X - 30),
                        MathHelper.Clamp(position.Y + (dir.Y * speed * Globals.TotalSeconds), 30, Globals.Bounds.Y - 30)
                    );
            }

            var toMouse = InputManager.MousePosition - position;
            rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);
        }

        private void MoveInWalk(List<Wall> walls)
        {
            //  Спасите помогите! Зачем я использую менеджеры?... Чтобы страдать
        }

        public void Update(List<Wall> walls)
        {
            MoveInWalk(walls);
            Move();
            Fire();
        }
    }
}
