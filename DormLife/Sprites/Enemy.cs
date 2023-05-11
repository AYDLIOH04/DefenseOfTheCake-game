using DormLife.GameObjects;
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
    public class Enemy : Sprite
    {
        public int HP { get; private set; }

        public Enemy(Texture2D texture, Vector2 position) : base(texture, position)
        {
            speed = 100;
            HP = 1;
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
        }

        public void Update(Cake cake)
        {
            var toPlayer = cake.position - position;
            rotation = (float)Math.Atan2(toPlayer.X, toPlayer.Y);

            if (toPlayer.Length() > 4)
            {
                var dir = Vector2.Normalize(toPlayer);
                position += dir * speed * Globals.TotalSeconds;
            }
        }
    }
}
