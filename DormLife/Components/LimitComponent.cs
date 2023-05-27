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
    public class LimitComponent
    {
        private int _currendScore = 0;
        private int _maxScore = 0;
        private SpriteFont _font;
        private Vector2 _position;
        private Color _color = Color.White;

        public LimitComponent(Vector2 position, int score, int maxScore)
        {
            _position = position;
            _currendScore = score;
            _maxScore = maxScore;
            _font = Globals.Content.Load<SpriteFont>("Font");
        }

        public void IncrementScore()
        {
            _currendScore++;
        }

        public void Update(int score)
        {
            _currendScore = score;
        }

        public void Draw()
        {
            string scoreText = $"{_currendScore} / {_maxScore}";

            Globals.SpriteBatch.DrawString(_font, scoreText, _position, _color);
        }
    }
}
