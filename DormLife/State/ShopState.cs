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
    public class ShopState : BaseState
    {
        private Button _exitButton;
        public static Score scoreTokens;

        private Button _speedBuffButton;
        private Button _damageBuffButton;
        private Button _shootingSpeedBuffButton;

        public ShopState()
        {
            _exitButton = new Button("Exit", new(Globals.Bounds.X - 70, 70), "btn/btn-exit");
            _exitButton.Clicked += StateManager.BackToGame;

            _speedBuffButton = new Button("Speed Buff", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2), "btn/btn-speedbuff");
            _speedBuffButton.Clicked += ShopManager.GetSpeedBuff;

            _damageBuffButton = new Button("Damage Buff", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 - 110), "btn/btn-damagebuff");
            _damageBuffButton.Clicked += ShopManager.GetDamageBuff;

            _shootingSpeedBuffButton = new Button("Shooting Speed", new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 + 110), "btn/btn-shotspeedbuff");
            _shootingSpeedBuffButton.Clicked += ShopManager.GetShootingSpeedSpeedBuff;

            scoreTokens = new("Tokens", new(Globals.Bounds.X - 350, Globals.Bounds.Y - 100), 0);
        }
        public override void Update()
        {
            _exitButton.Update();
            _speedBuffButton.Update();
            _damageBuffButton.Update();
            _shootingSpeedBuffButton.Update();

            scoreTokens.SetScore(ShopManager.Tokens);
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.RosyBrown);

            _exitButton.Draw();
            _speedBuffButton.Draw();
            _damageBuffButton.Draw();
            _shootingSpeedBuffButton.Draw();

            scoreTokens.Draw();
        }
    }
}
