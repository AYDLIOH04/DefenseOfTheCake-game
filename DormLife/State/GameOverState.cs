using DormLife.Components;
using DormLife.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.State
{
    public class GameOverState : BaseState
    {
        private Button _restartGameButton;
        private Button _menuButton;
        public GameOverState()
        {
            _restartGameButton = new Button("Restart Game", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 + 30), "btn/btn-newgame");
            _menuButton = new Button("Menu", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 - 80), "btn/btn-menu");

            _restartGameButton.Clicked += StateManager.CreateGame;
            _menuButton.Clicked += StateManager.GoToMenu;
        }


        public override void Update()
        {
            _restartGameButton.Update();
            _menuButton.Update();
        }
        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.LightSkyBlue);

            _restartGameButton.Draw();
            _menuButton.Draw();
        }
    }
}
