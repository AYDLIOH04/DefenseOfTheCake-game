using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Sprites
{
    public class Wall : GameObject
    {
        public Wall(Texture2D texture, Vector2 position)
        : base(texture, position)
        {
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
