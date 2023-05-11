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
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public Sprite(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.origin = new Vector2(texture.Width / 2, texture.Height / 2);
            this.position = new Vector2(position.X - texture.Width / 2, position.Y - texture.Height / 2);
        }


        public void Draw()
        {
            Globals.SpriteBatch.Draw(texture, position, null, Color.White, rotation, origin, 1, SpriteEffects.None, 0);
        }

        /*
        protected bool IsTouchingLeft(GameObject sprite)
        {
            return this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Left &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(GameObject sprite)
        {
            return this.Rectangle.Left < sprite.Rectangle.Right &&
              this.Rectangle.Right > sprite.Rectangle.Right &&
              this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(GameObject sprite)
        {
            return this.Rectangle.Bottom > sprite.Rectangle.Top &&
              this.Rectangle.Top < sprite.Rectangle.Top &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }

        protected bool IsTouchingBottom(GameObject sprite)
        {
            return this.Rectangle.Top < sprite.Rectangle.Bottom &&
              this.Rectangle.Bottom > sprite.Rectangle.Bottom &&
              this.Rectangle.Right > sprite.Rectangle.Left &&
              this.Rectangle.Left < sprite.Rectangle.Right;
        }
        */
    }
}
