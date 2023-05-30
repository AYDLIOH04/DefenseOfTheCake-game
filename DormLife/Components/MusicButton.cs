using DormLife.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Components
{
    public class MusicButton
    {
        private Texture2D _textureDisable;
        private Texture2D _textureEnable;

        private Vector2 _position;
        private static float eventTimer = 0;
        public event EventHandler Clicked;

        public Texture2D _currentTexture;

        public MusicButton(Vector2 position) 
        {
            _textureDisable = Globals.Content.Load<Texture2D>("btn/musicminus");
            _textureEnable = Globals.Content.Load<Texture2D>("btn/musicplus");

            _currentTexture = _textureEnable;
            _position = position;

            Clicked += ChangeTexture;
        }

        private Rectangle _rectangle
        {
            get
            {
                return new Rectangle((int)_position.X, (int)_position.Y, _textureEnable.Width, _textureEnable.Height);
            }
        }

        public void Update()
        {
            eventTimer += Globals.TotalSeconds;

            if (eventTimer > 0.2 && _rectangle.Contains(Mouse.GetState().Position) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Clicked?.Invoke(this, EventArgs.Empty);
                eventTimer = 0;
            }
        }

        public void ChangeTexture(object sender = null, EventArgs e = null)
        {
            if (SoundManager.isMusicEnabled)
            {
                SoundManager.ToggleMusic();
                _currentTexture = _textureDisable;
            } 
            else
            {
                SoundManager.ToggleMusic();
                _currentTexture = _textureEnable;
            }
        }

        public void Draw()
        {
            Globals.SpriteBatch.Draw(_currentTexture, _position, Color.White);
        }
    }
}
