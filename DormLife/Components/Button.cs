using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Components
{
    public class Button
    {
        private Vector2 _position;
        private Texture2D _texture;
        private SpriteFont _font;
        private string _text;

        public event EventHandler Clicked;

        public Button(string text, Vector2 position)
        {
            _text = text;
            _texture = Globals.Content.Load<Texture2D>("button");
            _position = new(position.X - _texture.Width / 2, position.Y - _texture.Height / 2);
            _font = Globals.Content.Load<SpriteFont>("Font");
        }

        private Rectangle _rectangle
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
            }
        }

        public void Update()
        {
            if (_rectangle.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Clicked?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(_texture, _position, Color.White);

            var textSize = _font.MeasureString(_text);
            var textPosition = new Vector2(_position.X + (_texture.Width - textSize.X) / 2, _position.Y + (_texture.Height - textSize.Y) / 2);
            Globals.SpriteBatch.DrawString(_font, _text, textPosition, Color.Black);
        }
    }
}
