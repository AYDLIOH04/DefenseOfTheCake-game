using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormLife.Managers;
using DormLife.GameObjects;

namespace DormLife.Sprites
{
    public class Sprite
    {
        protected readonly Texture2D texture;
        protected Vector2 origin;
        public Vector2 position;
        public float speed;
        public float rotation;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X - texture.Width / 2, (int)position.Y - texture.Height / 2, texture.Width, texture.Height);
            }
        }

        public Sprite(Texture2D texture, Vector2 position, float speed)
        {
            this.speed = speed;
            this.texture = texture;
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.position = new Vector2(position.X - texture.Width / 2, position.Y - texture.Height / 2);
        }


        public void Draw()
        {
            Globals.SpriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }

        #region Collision
        public bool CheckRectangleCollision(Sprite otherSprite)
        {
            return Rectangle.Intersects(otherSprite.Rectangle);
        }

        public bool CheckVectorCollision(Sprite otherSprite, int diff)
        {
            return (position - otherSprite.position).Length() < diff;
        }

        protected bool IsTouchingLeft(Sprite sprite, Vector2 Velocity)
        {
            return this.Rectangle.Right + 5 * Velocity.X > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(Sprite sprite, Vector2 Velocity)
        {
            return this.Rectangle.Left + 5 * Velocity.X < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Sprite sprite, Vector2 Velocity)
        {
            return this.Rectangle.Bottom + 5 * Velocity.Y > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(Sprite sprite, Vector2 Velocity)
        {
            return this.Rectangle.Top + 5 * Velocity.Y < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        #endregion
    }
}
