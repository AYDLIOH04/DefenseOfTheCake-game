using DormLife.Components;
using DormLife.GameObjects;
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
        public static Score scoreTrapCount;
        public static Score scoreCakeHP;

        private ShopComponent _speedBuff;
        private ShopComponent _getHpCake;
        private ShopComponent _damageBuff;
        private ShopComponent _shootingBuff;
        private ShopComponent _trap;
        private ShopComponent _turret;



        public ShopState()
        {
            _exitButton = new Button("Exit", new(Globals.Bounds.X - 70, 70), "btn/btn-exit");
            _exitButton.Clicked += StateManager.BackToGame;

            _getHpCake = new ShopComponent("5 tokens", ShopManager.GetHpCake, new(Globals.Bounds.X / 2 + 200, Globals.Bounds.Y / 2 - 220), "btn/btn-cakehp");
            _speedBuff = new ShopComponent("5 tokens", ShopManager.GetSpeedBuff, new(Globals.Bounds.X / 2 + 200, Globals.Bounds.Y / 2 - 110), "btn/btn-speedbuff");
            _damageBuff = new ShopComponent("10 tokens", ShopManager.GetDamageBuff, new(Globals.Bounds.X / 2 + 200, Globals.Bounds.Y / 2), "btn/btn-damagebuff", ProjectileManager.DamageBuffCount, ProjectileManager.DamageBuffLimit);
            _shootingBuff = new ShopComponent("5 tokens", ShopManager.GetShootingSpeedSpeedBuff, new(Globals.Bounds.X / 2 + 200, Globals.Bounds.Y / 2 + 110), "btn/btn-shotspeedbuff", ProjectileManager.ShootingBuffCount, ProjectileManager.ShootingBuffLimit);

            _trap = new ShopComponent("10 tokens", ShopManager.GetTrap, new(Globals.Bounds.X / 2 - 320, Globals.Bounds.Y / 2), "btn/btn-buytrap", TrapManager.currentHaveCount, TrapManager.Limit);
            _turret = new ShopComponent("25 tokens", ShopManager.GetHpCake, new(Globals.Bounds.X / 2 - 320, Globals.Bounds.Y / 2 - 110), "btn/btn-buyturret");

            scoreTrapCount = new("You have traps", new(Globals.Bounds.X - 420, Globals.Bounds.Y - 30), "scores/scoretrap");
            scoreCakeHP = new("Cake HP", new(Globals.Bounds.X - 200, Globals.Bounds.Y - 30), "scores/cakehp", Cake.CakeHP);
            scoreTokens = new("Tokens", new(Globals.Bounds.X - 300, Globals.Bounds.Y - 110), "scores/coin", 0);
        }

        public override void Update()
        {
            _exitButton.Update();

            _getHpCake.Update();                                                                
            _speedBuff.Update();
            _damageBuff.Update(ProjectileManager.DamageBuffCount);
            _shootingBuff.Update(ProjectileManager.ShootingBuffCount);

            _trap.Update(TrapManager.currentHaveCount + TrapManager.Traps.Count);
            _turret.Update();
                                                         
            scoreTokens.SetScore(ShopManager.Tokens);
            scoreCakeHP.SetScore(Cake.CakeHP);
            scoreTrapCount.SetScore(TrapManager.currentHaveCount);
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.RosyBrown);
            _exitButton.Draw();

            _getHpCake.Draw();
            _speedBuff.Draw();
            _damageBuff.Draw();
            _shootingBuff.Draw();

            _trap.Draw();
            _turret.Draw();

            scoreTokens.Draw();
            scoreCakeHP.Draw();

            if (TrapManager.currentHaveCount > 0)
            {
                scoreTrapCount.Draw();
            }
        }
    }
}
