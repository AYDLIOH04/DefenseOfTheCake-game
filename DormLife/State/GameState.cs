using DormLife.GameObjects;
using DormLife.Managers;
using DormLife.Sprites;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.State
{
    public class GameState : BaseState
    {
        private MainHero _player;

        private Cake _cake;

        public void NewGame()
        {
            WaveManager.NewWaves();

            ProjectileManager.Init();
            EnemyManager.Init();
            WallManager.Init();

            _player = new(Globals.Content.Load<Texture2D>("sprites/player"), new(200, 200), 300);
            _cake = new(Globals.Content.Load<Texture2D>("sprites/cake"), new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2));
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

            ProjectileManager.Draw();
            EnemyManager.Draw();
            WallManager.Draw();
        }
    }
}
