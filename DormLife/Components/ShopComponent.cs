using DormLife.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace DormLife.Components
{
    public class ShopComponent
    {
        public Button Button;
        public TextContent Text;
        public LimitComponent Score;
                                          
        public ShopComponent(string text, EventHandler callback, Vector2 position, string textureRef,int score = 0, int Limit = -1)
        {
            Button = new Button("", position, textureRef);
            Button.Clicked += callback;

            var textPosition = new Vector2(position.X - Button.Texture.Width + 45, position.Y - 10);
            Text = new TextContent(text, textPosition);

            if (Limit != -1)
            {
                Score = new(new(position.X + Button.Texture.Width / 2 + 10, position.Y - 10), score, Limit);
            }
        }

        public void Update(int score = 0)
        {
            Button.Update();
            if (Score != null)
            {
                Score.Update(score);
            }
        }

        public void Draw()
        {
            Button.Draw();
            Text.Draw();

            if (Score != null)
            {
                Score.Draw();
            }
        }
    }
}
