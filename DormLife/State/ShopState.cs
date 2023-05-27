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

        private ShopComponent _speedBuff;
        private ShopComponent _damageBuff;
        private ShopComponent _shootingBuff;

        public ShopState()
        {
            _exitButton = new Button("Exit", new(Globals.Bounds.X - 70, 70), "btn/btn-exit");
            _exitButton.Clicked += StateManager.BackToGame;

            _damageBuff = new ShopComponent("10 tokens", ShopManager.GetDamageBuff, new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 - 110), "btn/btn-damagebuff");
            _speedBuff = new ShopComponent("5 tokens", ShopManager.GetSpeedBuff, new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2), "btn/btn-speedbuff");
            _shootingBuff = new ShopComponent("5 tokens", ShopManager.GetShootingSpeedSpeedBuff, new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2 + 110), "btn/btn-shotspeedbuff");

            scoreTokens = new("Tokens", new(Globals.Bounds.X - 350, Globals.Bounds.Y - 100), 0);
        }
        public override void Update()
        {
            _exitButton.Update();

            _damageBuff.Update();
            _speedBuff.Update();
            _shootingBuff.Update();

            scoreTokens.SetScore(ShopManager.Tokens);
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.RosyBrown);
            _exitButton.Draw();

            _damageBuff.Draw();
            _speedBuff.Draw();
            _shootingBuff.Draw();

            scoreTokens.Draw();
        }
    }
}
