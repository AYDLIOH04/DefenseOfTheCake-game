using DormLife.GameObjects;
using DormLife.Sprites;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public class GameManager
    {
        private readonly MainHero _player;

        private Cake _cake;

        public GameManager()
        {
            ProjectileManager.Init();
            _player = new(Globals.Content.Load<Texture2D>("sprites/player"), new(200, 200));
            _cake = new(Globals.Content.Load<Texture2D>("sprites/cake"), new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2));
            EnemyManager.Init();
            EnemyManager.AddEnemy();
            WallManager.Init();
            WallManager.GenerateWalls();
        }
        
        public void Update()
        {
            InputManager.Update();
            _player.Update(WallManager.Walls);
            ProjectileManager.Update(EnemyManager.Enemies, WallManager.Walls, _cake);
            EnemyManager.Update(_cake, WallManager.Walls);
        }

        public void Draw()
        {
            _player.Draw();
            _cake.Draw();
            ProjectileManager.Draw();
            EnemyManager.Draw();
            WallManager.Draw();
        }
    }
}
