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
    public class PauseState : BaseState
    {
        private Button _continueGameButton;
        private Button _menuButton;
        private Button _exitButton;

        private TextureContent _textureContent;

        public PauseState()
        {
            _continueGameButton = new Button("Continue Game", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 + 30), "btn/btn-continue");
            _continueGameButton.Clicked += StateManager.BackToGame;

            _menuButton = new Button("Menu", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 - 80), "btn/btn-menu");
            _menuButton.Clicked += StateManager.GoToMenu;

            _exitButton = new Button("Exit", new(Globals.Bounds.X - 70, 70), "btn/btn-exit");
            _exitButton.Clicked += StateManager.ExitGame;

            _textureContent = new TextureContent(new(Globals.Bounds.X / 2 - 156, 250), "BackgroundContent/pause");
        }
        public override void Update()
        {
            _continueGameButton.Update();
            _menuButton.Update();
            _exitButton.Update();
            MenuState.musicButton.Update();
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.LightSkyBlue);

            _continueGameButton.Draw();
            _menuButton.Draw();
            _exitButton.Draw();
            MenuState.musicButton.Draw();


            _textureContent.Draw();
        }
    }
}
