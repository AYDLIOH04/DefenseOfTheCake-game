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
    public class Wall : Sprite
    {
        public bool IsHorizontal { get; }
        public Wall(Texture2D texture, Vector2 position, bool isHorizontal, float speed = 0)
        : base(texture, position, speed)
        {
            IsHorizontal = isHorizontal;
        }
    }
}