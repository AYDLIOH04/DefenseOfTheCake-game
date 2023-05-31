using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Grid
{
    public class Cell
    {
        private int x;
        private int y;
        public bool IsWalkable { get; set; }
        public Texture2D WallTexture { get; set; }

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            IsWalkable = true;
        }

        public int X => x;
        public int Y => y;
    }
}
