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
        public Cake cake;

        public static Score scoreWave;
        public static Score highscoreWave = new Score("Highscore", new (10, 10)) ;
        public static Score cakeHP;
        public static Score enemyKills;

        public static Score countUlt;


        public void NewGame()
        {
            scoreWave = new Score("Wave", new(10, 30));
            enemyKills = new Score("Kills", new(Globals.Bounds.X - 100, Globals.Bounds.Y - 30), 0);

            WaveManager.NewWaves();

            ProjectileManager.Init();
            BonusManager.Init();
            EnemyManager.Init();
            WallManager.Init();

            cake = new(Globals.Content.Load<Texture2D>("sprites/cake"), new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2));
            player = new(Globals.Content.Load<Texture2D>("sprites/player"), new(cake.position.X - 200, cake.position.Y), 300);
            cakeHP = new("Cake HP", new(Globals.Bounds.X - 250, Globals.Bounds.Y - 30), cake.HP);
            countUlt = new("Ultimatum Projectiles", new(Globals.Bounds.X - 450, Globals.Bounds.Y - 30), 5);
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
            EnemyManager.Update(cake, WallManager.Walls);
            BonusManager.Update(player, cake);

            cakeHP.SetScore(cake.HP);
        }

        public override void Draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(Color.RosyBrown);

            player.Draw();
            cake.Draw();
            scoreWave.Draw();
            highscoreWave.Draw();
            cakeHP.Draw();
            enemyKills.Draw();

            ProjectileManager.Draw();
            BonusManager.Draw();
            EnemyManager.Draw();
            WallManager.Draw();

            if (ProjectileManager.IsUlt)
            {
                countUlt.Draw();
            }
        }
    }
}
