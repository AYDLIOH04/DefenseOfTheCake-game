using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DormLife.Managers;
using DormLife.Components;

namespace DormLife.State
{
    public class MenuState : BaseState
    {

        private Button _newGameButton;
        public MenuState()
        {
            _newGameButton = new Button("New Game", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2));
            _newGameButton.Clicked += StateManager.CreateGame;
        }

        public override void Update()
        {
            _newGameButton.Update();
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.RoyalBlue);

            _newGameButton.Draw();
        }
    }
}
