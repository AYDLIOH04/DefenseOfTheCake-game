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
        #region Score
        public static Score scoreWave;
        public static Score scoreHighWave = new Score("Highscore", new (20, 10));
        public static Score scoreCakeHP;
        public static Score scoreEnemyKills;
        public static Score scoreTokens;
        public static Score scoreUltCount;                         
        public static Score scoreTrapCount;
        #endregion

        private static Button _shopButton;

        public static GameManager gameManager;

        public void NewGame()
        {
            gameManager = new();

            #region Scores + Buttons Init
            scoreWave = new Score("Wave", new(20, 30));
            scoreEnemyKills = new Score("Kills", new(Globals.Bounds.X - 100, Globals.Bounds.Y - 50), "scores/kills");
            scoreCakeHP = new("Cake HP", new(Globals.Bounds.X - 200, Globals.Bounds.Y - 50), "scores/cakehp", gameManager.cake.HP);
            scoreTokens = new("Tokens", new(Globals.Bounds.X - 320, Globals.Bounds.Y - 110), "scores/coin");
            scoreUltCount = new("Ultimatum Projectiles", new(Globals.Bounds.X - 320, Globals.Bounds.Y - 50), "arms/ult", 5);
            scoreTrapCount = new("You have traps", new(Globals.Bounds.X - 420, Globals.Bounds.Y - 50), "scores/scoretrap");


            _shopButton = new Button("Shop", new(Globals.Bounds.X - 150, Globals.Bounds.Y - 100), "btn/btn-shop");
            _shopButton.Clicked += StateManager.GoToShop;
            #endregion
        }

        public override void Update()
        {
            gameManager.Update();

            #region Scores Update
            scoreCakeHP.SetScore(gameManager.cake.HP);
            scoreTokens.SetScore(ShopManager.Tokens);
            scoreTrapCount.SetScore(TrapManager.currentHaveCount);
            #endregion

            _shopButton.Update();
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.RosyBrown);

            gameManager.Draw();

            #region Scores + Button Draw
            scoreWave.Draw();
            scoreHighWave.Draw();
            scoreCakeHP.Draw();
            scoreEnemyKills.Draw();
            scoreTokens.Draw();
            _shopButton.Draw();
            #endregion
        }
    }
}
