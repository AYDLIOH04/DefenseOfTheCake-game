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

        public bool CheckCollision(Sprite otherSprite)
        {
            return Rectangle.Intersects(otherSprite.Rectangle);
        }
    }
}
