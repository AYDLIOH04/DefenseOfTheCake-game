using DormLife.GameObjects;
using DormLife.Sprites;
using DormLife.State;
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
        public MainHero player;
        public Cake cake;

        public GameManager()
        {
            #region Managers Init
            WaveManager.Init();
            ProjectileManager.Init();
            BonusManager.Init();
            TokenManager.Init();
            EnemyManager.Init();
            WallManager.Init();
            ShopManager.Init();
            TrapManager.Init();
            #endregion


            cake = new(Globals.Content.Load<Texture2D>("sprites/cake"), new(Globals.Bounds.X / 2, Globals.Bounds.Y / 2));
            player = new(Globals.Content.Load<Texture2D>("sprites/player"), new(cake.position.X - 200, cake.position.Y), 300);

        }

        public void Update()
        {
            InputManager.Update();
            player.Update(WallManager.Walls, cake);

            WaveManager.GenerateWave();
            ProjectileManager.Update(EnemyManager.Enemies, WallManager.Walls, cake);
            EnemyManager.Update(cake, WallManager.Walls, TrapManager.Traps);
            TrapManager.Update();
            BonusManager.Update(player, cake);
            TokenManager.Update(player);
        }

        public void Draw()
        {
            player.Draw();
            cake.Draw();

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
                GameState.scoreUltCount.Draw();
            }

            if (TrapManager.currentHaveCount > 0)
            {
                GameState.scoreTrapCount.Draw();
            }
        }
    }
}
