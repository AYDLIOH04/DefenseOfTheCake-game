using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Sprites
{
    public class Cake : GameObject
    {

        public int HP;
        public Cake(Texture2D texture, Vector2 position, Color color)
        : base(texture, position, color)
        {
            HP = 1000;
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
