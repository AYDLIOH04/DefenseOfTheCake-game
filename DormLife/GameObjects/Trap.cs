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
    public class Trap : Sprite
    {
        public int HP { get; private set; }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
        }

        public Trap(Texture2D texture, Vector2 position, float rotate, float speed = 0)
        : base(texture, position, speed)
        {
            HP = 5;
            rotation = rotate;
        }
    }
}
