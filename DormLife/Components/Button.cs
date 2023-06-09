﻿using Microsoft.Xna.Framework.Graphics;
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

        private static float eventTimer = 0;


        public event EventHandler Clicked;

        public Texture2D Texture => _texture;

        public Button(string text, Vector2 position, string texture = "button")
        {
            _text = "";
            _texture = Globals.Content.Load<Texture2D>(texture);
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
            eventTimer += Globals.TotalSeconds;

            if (eventTimer > 1 && _rectangle.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Clicked?.Invoke(this, EventArgs.Empty);
                eventTimer = 0;
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
