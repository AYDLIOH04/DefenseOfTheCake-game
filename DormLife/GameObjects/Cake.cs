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
    public class Cake : Sprite
    {
        public int HP { get; private set; }
        private static Random random = new();

        public void GameOver()
        {
            // Конец игры
        }

        private void DamageTrigger()
        {
            float x = 0;
            float y = 0;

            if (random.Next(0, 2) == 1)
            {
                x = position.X + random.Next(0, 2) * 10;
                y = position.Y + random.Next(0, 2) * 10;
            }
            else
            {
                x = position.X - random.Next(0, 2) * 10;
                y = position.Y - random.Next(0, 2) * 10;
            }

            position = new(x, y);
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;

            DamageTrigger();
        }

        public Cake(Texture2D texture, Vector2 position)
        : base(texture, position)
        {
            HP = 100;
        }
    }
}
