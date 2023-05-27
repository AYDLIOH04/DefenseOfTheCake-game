using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Components
{
    public class TextureContent
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Color _color = Color.White;

        public TextureContent(Vector2 position, string texture)
        {
            _position = position;
            _texture = Globals.Content.Load<Texture2D>(texture);
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(_texture, _position, _color);
        }
    }
}
