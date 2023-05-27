using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Components
{
    public class TextContent
    {
        private SpriteFont _font;
        private Vector2 _position;
        private Color _color = Color.White;
        private string _text;
        public TextContent(string text, Vector2 position)
        {
            _text = text;
            _position = position;
            _font = Globals.Content.Load<SpriteFont>("Font");
        }

        public void ChangeText(string text)
        {
            _text = text;
        }

        public void Draw()
        {
            Globals.SpriteBatch.DrawString(_font, _text, _position, _color);
        }
    }
}
