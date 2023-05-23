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

        public Score(string text, Vector2 position, int score = 0)
        {
            _text = text;
            _position = position;
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
            string scoreText = $"{_text}: " + _score;

            Globals.SpriteBatch.DrawString(_font, scoreText, _position, _color);
        }
    }
}
