using DormLife.GameObjects;
using DormLife.Sprites;
using DormLife.State;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public class GameManager
    {
        public MainHero player;
        public Cake cake;

        private Grid grid;
        private int gridWidth = Globals.Bounds.X / 25;
        private int gridHeight = Globals.Bounds.Y / 25;
        private int cellSize = 25;

        public GameManager()
        {
            grid = new Grid(gridWidth, gridHeight, cellSize);

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

            LoadWallsToGrid();
        }

        private void LoadWallsToGrid()
        {
            foreach (Wall wall in WallManager.Walls)
            {
                // Определение позиции стенки в сетке
                int wallStartX = (int)(wall.position.X / cellSize);
                int wallStartY = (int)(wall.position.Y / cellSize);

                // Вычисление границ клеток, соответствующих стенке
                int wallEndX = wallStartX + (wall.Rectangle.Width / cellSize);
                int wallEndY = wallStartY + (wall.Rectangle.Height / cellSize);

                // Заполняем клетки, соответствующие стенке
                for (int x = wallStartX; x < wallEndX; x += cellSize)
                {
                    for (int y = wallStartY; y < wallEndY; y += cellSize)
                    {
                        // Установка ячейки как препятствие (занято)
                        grid.SetObstacle(x, y, true);
                    }
                }
            }
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
