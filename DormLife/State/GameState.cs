using DormLife.Components;
using DormLife.GameObjects;
using DormLife.Managers;
using DormLife.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.State
{
    public class GameState : BaseState
    {
        public static MainHero player;
        public static Cake cake;


        #region Score
        public static Score scoreWave;
        public static Score scoreHighWave = new Score("Highscore", new (10, 10));
        public static Score scoreCakeHP;
        public static Score scoreEnemyKills;
        public static Score scoreTokens;
        public static Score scoreUltCount;                         
        public static Score scoreTrapCount;
        #endregion

        private static Button _shopButton;

        public void NewGame()
        {
            cake = new(Globals.Content.Load<Texture2D>("sprites/cake"), new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2));
            player = new(Globals.Content.Load<Texture2D>("sprites/player"), new(cake.position.X - 200, cake.position.Y), 300);

            #region Managers Init
            WaveManager.NewWaves();
            ProjectileManager.Init();                               
            BonusManager.Init();
            TokenManager.Init();
            EnemyManager.Init();
            WallManager.Init();
            ShopManager.Init();
            TrapManager.Init();
            #endregion

            #region Scores + Buttons Init
            scoreWave = new Score("Wave", new(10, 30));
            scoreEnemyKills = new Score("Kills", new(Globals.Bounds.X - 100, Globals.Bounds.Y - 30), "scores/kills");
            scoreCakeHP = new("Cake HP", new(Globals.Bounds.X - 200, Globals.Bounds.Y - 30), "scores/cakehp", cake.HP);
            scoreTokens = new("Tokens", new(Globals.Bounds.X - 300, Globals.Bounds.Y - 110), "scores/coin");
            scoreUltCount = new("Ultimatum Projectiles", new(Globals.Bounds.X - 320, Globals.Bounds.Y - 30), "arms/ult", 5);
            scoreTrapCount = new("You have traps", new(Globals.Bounds.X - 420, Globals.Bounds.Y - 30), "scores/scoretrap");


            _shopButton = new Button("Shop", new(Globals.Bounds.X - 150, Globals.Bounds.Y - 100), "btn/btn-shop");
            _shopButton.Clicked += StateManager.GoToShop;
            #endregion
        }

        public void ContinueGame()
        {
            // TODO Когда-нибудь
        }

        public override void Update()
        {
            InputManager.Update();
            player.Update(WallManager.Walls, cake);

            WaveManager.GenerateWave();
            ProjectileManager.Update(EnemyManager.Enemies, WallManager.Walls, cake);
            EnemyManager.Update(cake, WallManager.Walls, TrapManager.Traps);
            TrapManager.Update();
            BonusManager.Update(player, cake);
            TokenManager.Update(player);

            scoreCakeHP.SetScore(cake.HP);
            scoreTokens.SetScore(ShopManager.Tokens);
            scoreTrapCount.SetScore(TrapManager.currentHaveCount);

            _shopButton.Update();
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.RosyBrown);

            player.Draw();
            cake.Draw();

            #region Scores + Button Draw
            scoreWave.Draw();
            scoreHighWave.Draw();
            scoreCakeHP.Draw();
            scoreEnemyKills.Draw();
            scoreTokens.Draw();
            _shopButton.Draw();
            #endregion

            #region Managers Draw
            ProjectileManager.Draw();
            BonusManager.Draw();
            TokenManager.Draw();
            EnemyManager.Draw();
            WallManager.Draw();
            TrapManager.Draw();
            #endregion

            if (ProjectileManager.IsUlt)
            {
                scoreUltCount.Draw();
            }

            if (TrapManager.currentHaveCount > 0)
            {
                scoreTrapCount.Draw();
            }
        }
    }
}
