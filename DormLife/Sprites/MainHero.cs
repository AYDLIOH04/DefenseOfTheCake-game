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
        public MainHero(Texture2D texture, Vector2 position) : base(texture, position)
        {
            speed = 300;
        }

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
                        MathHelper.Clamp(position.X + (dir.X * speed * Globals.TotalSeconds), 0, Globals.Bounds.X),
                        MathHelper.Clamp(position.Y + (dir.Y * speed * Globals.TotalSeconds), 0, Globals.Bounds.Y)
                    );
            }

            var toMouse = InputManager.MousePosition - position;
            rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);
        }

        private void MoveInWalk()
        {
            //TODO
        }

        public void Update()
        {
            MoveInWalk();
            Move();
            Fire();
        }
    }
}
