using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace DormLife.Components
{
    public class Score
    {
        private int _score = 0;
        private SpriteFont _font;
        private Vector2 _position;
        private Color _color = Color.White;
        private string _text;
        public int GetScore => _score;

        private Texture2D _texture;
        private Vector2 _texturePosition;


        public Score(string text, Vector2 position, string texture = "", int score = 0)
        {
            _text = text;
            _position = position;
            if (!string.IsNullOrEmpty(texture))
            {
                _texture = Globals.Content.Load<Texture2D>(texture);
            }
            _texturePosition = new(position.X - 35, position.Y - 2);
            _score = score;
            _font = Globals.Content.Load<SpriteFont>("Font");
        }

        public void IncrementScore(int amount)
        {
            _score += amount;
        }

        public void DecrementScore(int amount)
        {
            _score -= amount;
        }

        public void SetScore(int score)
        {
            _score = score;
        }

        public void ResetScore()
        {
            _score = 0;
        }

        public void Draw()
        {
            string scoreText = $"{_text} : {_score}";

            if (_texture != null)
            {
                scoreText = $"{_score}";
                Globals.SpriteBatch.Draw(_texture, _texturePosition, _color);
            }
            Globals.SpriteBatch.DrawString(_font, scoreText, _position, _color);

        }
    }
}
