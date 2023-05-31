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
        public static Score scoreTurretCount;
        public static Score scoreSlowlyTrapCount;


        public static Score scoreCakeHP;

        private ShopComponent _speedBuff;
        private ShopComponent _getHpCake;
        private ShopComponent _damageBuff;
        private ShopComponent _shootingBuff;
        private ShopComponent _trap;
        private ShopComponent _turret;
        private ShopComponent _slowlyTrap;




        public ShopState()
        {
            _exitButton = new Button("Exit", new(Globals.Bounds.X - 70, 70), "btn/btn-exit");
            _exitButton.Clicked += StateManager.BackToGame;

            _getHpCake = new ShopComponent("5 tokens", ShopManager.GetHpCake, new(Globals.Bounds.X / 2 + 200, Globals.Bounds.Y / 2 - 220), "btn/btn-cakehp");
            _speedBuff = new ShopComponent("5 tokens", ShopManager.GetSpeedBuff, new(Globals.Bounds.X / 2 + 200, Globals.Bounds.Y / 2 - 110), "btn/btn-speedbuff");
            _damageBuff = new ShopComponent("10 tokens", ShopManager.GetDamageBuff, new(Globals.Bounds.X / 2 + 200, Globals.Bounds.Y / 2), "btn/btn-damagebuff", ProjectileManager.DamageBuffCount, ProjectileManager.DamageBuffLimit);
            _shootingBuff = new ShopComponent("5 tokens", ShopManager.GetShootingSpeedSpeedBuff, new(Globals.Bounds.X / 2 + 200, Globals.Bounds.Y / 2 + 110), "btn/btn-shotspeedbuff", ProjectileManager.ShootingBuffCount, ProjectileManager.ShootingBuffLimit);

            _trap = new ShopComponent("10 tokens", ShopManager.GetTrap, new(Globals.Bounds.X / 2 - 320, Globals.Bounds.Y / 2), "btn/btn-buytrap", TrapManager.currentHaveCount, TrapManager.Limit);
            _turret = new ShopComponent("25 tokens", ShopManager.GetTurret, new(Globals.Bounds.X / 2 - 320, Globals.Bounds.Y / 2 - 110), "btn/btn-buyturret", TurretManager.currentHaveCount, TurretManager.Limit);
            _slowlyTrap = new ShopComponent("5 tokens", ShopManager.GetSlowlyTrap, new(Globals.Bounds.X / 2 - 320, Globals.Bounds.Y / 2 + 110), "btn/btn-slowlytrap", SlowlyTrapManager.currentHaveCount, SlowlyTrapManager.Limit);

            scoreTrapCount = new("You have traps", new(Globals.Bounds.X - 420, Globals.Bounds.Y - 50), "scores/scoretrap");
            scoreSlowlyTrapCount = new("You have slowly traps", new(Globals.Bounds.X - 520, Globals.Bounds.Y - 50), "scores/slowlytrapmini");
            scoreTurretCount = new("You have turrets", new(Globals.Bounds.X - 620, Globals.Bounds.Y - 50), "scores/turretmini");

            scoreCakeHP = new("Cake HP", new(Globals.Bounds.X - 200, Globals.Bounds.Y - 50), "scores/cakehp", Cake.CakeHP);
            scoreTokens = new("Tokens", new(Globals.Bounds.X - 320, Globals.Bounds.Y - 110), "scores/coin", 0);
        }

        public override void Update()
        {
            _exitButton.Update();

            _getHpCake.Update();                                                                
            _speedBuff.Update();
            _damageBuff.Update(ProjectileManager.DamageBuffCount);
            _shootingBuff.Update(ProjectileManager.ShootingBuffCount);

            _trap.Update(TrapManager.currentHaveCount + TrapManager.Traps.Count);
            _turret.Update(TurretManager.currentHaveCount + TurretManager.Turrets.Count);
            _slowlyTrap.Update(SlowlyTrapManager.currentHaveCount + SlowlyTrapManager.SlowlyTraps.Count);


            scoreTokens.SetScore(ShopManager.Tokens);
            scoreCakeHP.SetScore(Cake.CakeHP);
            scoreTrapCount.SetScore(TrapManager.currentHaveCount);
            scoreTurretCount.SetScore(TurretManager.currentHaveCount);
            scoreSlowlyTrapCount.SetScore(SlowlyTrapManager.currentHaveCount);

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
            _slowlyTrap.Draw();

            scoreTokens.Draw();
            scoreCakeHP.Draw();

            if (TrapManager.currentHaveCount > 0)
            {
                scoreTrapCount.Draw();
            }

            if (TurretManager.currentHaveCount > 0)
            {
                scoreTurretCount.Draw();
            }

            if (SlowlyTrapManager.currentHaveCount > 0)
            {
                scoreSlowlyTrapCount.Draw();
            }
        }
    }
}
