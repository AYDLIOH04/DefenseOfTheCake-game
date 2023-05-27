using DormLife.Managers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Components
{
    public class ShopComponent
    {
        public Button Button;
        public TextContent Text;
                                          
        public ShopComponent(string text, EventHandler callback, Vector2 position, string textureRef)
        {
            Button = new Button("", position, textureRef);
            Button.Clicked += callback;

            var textPosition = new Vector2(position.X + Button.Texture.Width / 2 + 10, position.Y - 10);
            Text = new TextContent(text, textPosition);
        }

        public void Update()
        {
            Button.Update();
        }

        public void Draw()
        {
            Button.Draw();
            Text.Draw();
        }
    }
}
