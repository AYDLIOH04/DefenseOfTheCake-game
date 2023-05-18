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


        public void NewGame()
        {
            scoreWave = new Score("Wave", new(10, 30));
            enemyKills = new Score("Kills", new(Globals.Bounds.X - 100, Globals.Bounds.Y - 30), 0);


            WaveManager.NewWaves();

            ProjectileManager.Init();
            EnemyManager.Init();
            WallManager.Init();

            player = new(Globals.Content.Load<Texture2D>("sprites/player"), new(200, 200), 300);
            cake = new(Globals.Content.Load<Texture2D>("sprites/cake"), new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2));
            cakeHP = new Score("Cake HP", new(Globals.Bounds.X - 250, Globals.Bounds.Y - 30), cake.HP);
        }

        public void ContinueGame()
        {
            // TODO Когда-нибудь
        }

        public override void Update()
        {
            InputManager.Update();
            player.Update(WallManager.Walls, cake);

            ProjectileManager.Update(EnemyManager.Enemies, WallManager.Walls, cake);
            EnemyManager.Update(cake, WallManager.Walls);
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
            EnemyManager.Draw();
            WallManager.Draw();
        }
    }
}
