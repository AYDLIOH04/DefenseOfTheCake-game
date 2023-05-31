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
        private Button _exitButton;

        private Score _scoreKills;
        private Score _scoreWave;


        private TextureContent _textureContent;

        public GameOverState()
        {
            _restartGameButton = new Button("Restart Game", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 + 100), "btn/btn-newgame");
            _restartGameButton.Clicked += StateManager.CreateGame;

            _menuButton = new Button("Menu", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2), "btn/btn-menu");
            _menuButton.Clicked += StateManager.GoToMenu;

            _exitButton = new Button("Exit", new(Globals.Bounds.X - 70, 70), "btn/btn-exit");
            _exitButton.Clicked += StateManager.ExitGame;

            _textureContent = new TextureContent(new(Globals.Bounds.X / 2 - 250, 250), "BackgroundContent/gameover");

            _scoreKills = new Score("Kills", new(Globals.Bounds.X / 2 - 120, Globals.Bounds.Y / 2 - 100));
            _scoreWave = new Score("Score", new(Globals.Bounds.X / 2 + 30, Globals.Bounds.Y / 2 - 100));

        }

        public override void Update()
        {
            _restartGameButton.Update();
            _menuButton.Update();
            _exitButton.Update();
            MenuState.musicButton.Update();

            _scoreKills.SetScore(GameState.scoreEnemyKills.GetScore);
            _scoreWave.SetScore(GameState.scoreWave.GetScore);

        }
        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.LightSkyBlue);

            _restartGameButton.Draw();
            _menuButton.Draw();
            _exitButton.Draw();
            MenuState.musicButton.Draw();

            _scoreKills.Draw();
            _scoreWave.Draw();

            _textureContent.Draw();
        }
    }
}
