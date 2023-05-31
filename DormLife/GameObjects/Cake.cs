using DormLife.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.GameObjects
{
    public class Cake : Sprite
    {
        public int HP { get; private set; }
        private static Random random = new();
        public static int CakeHP = 0;

        public void ChangePosition()
        {
            // TODO Спасаем торик
            position = new(position.X, position.Y);
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
            CakeHP = HP;
        }

        public void GetHP(int hp)
        {
            HP += hp;
            CakeHP = HP;
        }

        public Cake(Texture2D texture, Vector2 position, float speed = 0)
        : base(texture, position, speed)
        {
            HP = 1;
            CakeHP = HP;
        }
    }
}
