using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Sprites
{
    public class Enemies
    {
        public List<Enemy> list;
        private Texture2D texture;
        private int MaxCount;
        public Enemies(Texture2D pt, int count)
        {
            MaxCount = count;
            texture = pt;
            list = new List<Enemy>();
            for (int i = 0; i < MaxCount; i++)
                list.Add(new Enemy(texture, Enemy.GeneratePositionEnemy(pt), Color.Red, 2));
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in list)
                enemy.Update(gameTime);

            list.RemoveAll(e => e.IsRemoved);

            if (Count == 0)
            {
                for (int i = 0; i < MaxCount; i++)
                    list.Add(new Enemy(texture, Enemy.GeneratePositionEnemy(texture), Color.Red, 2));
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (var enemy in list)
                enemy.Draw(_spriteBatch);
        }

        public int Count
        {
            get { return list.Count; }
        }
    }

    public class Enemy : GameObject
    {
        protected float Speed;
        public bool IsRemoved;

        public Enemy(Texture2D texture, Vector2 position, Color color, float speed)
        : base(texture, position, color)
        {
            Speed = speed;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Rectangle enemyRectangle = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
                if (enemyRectangle.Contains(Mouse.GetState().Position))
                {
                    IsRemoved = true;
                }
            }
        }
        public static Vector2 GeneratePositionEnemy(Texture2D pt)
        {
            var random = new Random();
            var side = new Random().Next(0, 4);

            if (side == 0) return new Vector2(random.Next(10, Game1.Width) - 10, pt.Height);
            if (side == 1) return new Vector2(Game1.Width - pt.Width, random.Next(10, Game1.Height) - 10);
            if (side == 2) return new Vector2(random.Next(10, Game1.Width) - 10, Game1.Height - pt.Height);
            return new Vector2(pt.Width, random.Next(10, Game1.Height) - 10);
        }
    }
}
