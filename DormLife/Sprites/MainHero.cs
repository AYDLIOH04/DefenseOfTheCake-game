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
    public class MainHero : GameObject
    {
        protected float Speed;
        protected Input Input;
        public MainHero(Texture2D texture, Vector2 position, Color color, float speed)
        : base(texture, position, color)
        {
            Speed = speed;
            Input = new Input()
            {
                Up = Keys.W,
                Left = Keys.A,
                Down = Keys.S,
                Right = Keys.D
            };
        }

        public override void Update(GameTime gameTime, List<GameObject> sprites, List<Enemy> enemies)
        {
            Move();

            foreach (var sprite in sprites)
            {
                if (sprite == this) continue;
                FindCollision(sprite);
            }

            foreach (var enemy in enemies)
            {
                if(FindCollision(enemy)) enemy.IsRemoved = true;
            }

            MoveInWalk();

            Position += Velocity;

            Velocity = Vector2.Zero;
        }

        private void MoveInWalk()
        {
            if (Position.X + Velocity.X + _texture.Width >= Game1.Width
             || Position.X + Velocity.X < 0
             || Position.Y + Velocity.Y + _texture.Height >= Game1.Height
             || Position.Y + Velocity.Y < 0)
            {
                Velocity = Vector2.Zero;
            }
        }

        private bool FindCollision(GameObject sprite)
        {
            if ((this.Velocity.X > 0 && this.IsTouchingLeft(sprite)) ||
                   (this.Velocity.X < 0 && this.IsTouchingRight(sprite)))
            {
                this.Velocity.X = 0;
                return true;
            }

            if ((this.Velocity.Y > 0 && this.IsTouchingTop(sprite)) ||
                (this.Velocity.Y < 0 && this.IsTouchingBottom(sprite)))
            {
                this.Velocity.Y = 0;
                return true;
            }

            return false;
        }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;

            if (Keyboard.GetState().IsKeyDown(Input.Up))
                Velocity.Y = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = Speed;
        }
    }
}
