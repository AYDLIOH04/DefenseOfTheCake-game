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
    public class Bonus : Sprite
    {
        public enum BonusType
        {
            BottleHP,
            BottleULT
        }

        public BonusType Type;
        public bool IsTaken = false;
        public Bonus(Texture2D texture, Vector2 position, BonusType type, float speed = 0)
        : base(texture, position, speed)
        {
            Type = type; 
        }
    }
}
