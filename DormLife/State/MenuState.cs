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
        private Button _exitButton;
        public static MusicButton musicButton;

        private TextureContent _textureContent;

        public MenuState()
        {
            SoundManager.LoadContent();
            SoundManager.PlayBasedBackgroundMusic();

            _newGameButton = new Button("Play", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 + 70), "btn/btn-play");
            _newGameButton.Clicked += StateManager.CreateGame;

            _exitButton = new Button("Exit", new(Globals.Bounds.X - 70, 70), "btn/btn-exit");
            _exitButton.Clicked += StateManager.ExitGame;

            musicButton = new MusicButton(new(20, 20));

            _textureContent = new TextureContent(new(Globals.Bounds.X / 2 - 250, 20), "BackgroundContent/menu");
        }

        public override void Update()
        {
            _exitButton.Update();
            musicButton.Update();
            _newGameButton.Update();
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.LightSkyBlue);

            _exitButton.Draw();
            musicButton.Draw();
            _newGameButton.Draw();

            _textureContent.Draw();
        }
    }
}
