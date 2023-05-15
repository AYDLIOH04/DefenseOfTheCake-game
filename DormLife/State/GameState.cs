using DormLife.Components.GameState;
using DormLife.GameObjects;
using DormLife.Managers;
using DormLife.Sprites;
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
        private MainHero _player;
        public Cake _cake;

        public static Score _scoreWave;
        public static Score _highscoreWave = new Score("Highscore", new (10, 10)) ;
        public static Score _cakeHP;
        public static Score _enemyKills;


        public void NewGame()
        {
            _scoreWave = new Score("Wave", new(10, 30));
            _enemyKills = new Score("Kills", new(Globals.Bounds.X - 100, Globals.Bounds.Y - 30), 0);


            WaveManager.NewWaves();

            ProjectileManager.Init();
            EnemyManager.Init();
            WallManager.Init();

            _player = new(Globals.Content.Load<Texture2D>("sprites/player"), new(200, 200), 300);
            _cake = new(Globals.Content.Load<Texture2D>("sprites/cake"), new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2));
            _cakeHP = new Score("Cake HP", new(Globals.Bounds.X - 250, Globals.Bounds.Y - 30), _cake.HP);
        }

        public void ContinueGame()
        {
            // TODO Когда-нибудь
        }

        public override void Update()
        {
            InputManager.Update();
            _player.Update(WallManager.Walls);

            ProjectileManager.Update(EnemyManager.Enemies, WallManager.Walls, _cake);
            EnemyManager.Update(_cake, WallManager.Walls);
        }

        public override void Draw()
        {
            _player.Draw();
            _cake.Draw();

            _scoreWave.Draw();
            _highscoreWave.Draw();

            _cakeHP.Draw();
            _enemyKills.Draw();

            ProjectileManager.Draw();
            EnemyManager.Draw();
            WallManager.Draw();
        }
    }
}
