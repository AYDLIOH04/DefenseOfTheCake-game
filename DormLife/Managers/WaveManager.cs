using DormLife.Components;
using DormLife.Sprites;
using DormLife.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public static class WaveManager
    {
        private static int _spawnCount = -1;
        private static int _count = 0;
        private static int _waveLevel = 1;
        private static Random _random = new();

        private static float enemyTimer = 0;
        private static float enemySpawnTime = 1.23f;

        private static bool isGenerating = false;
        private static Vector2 position;

        public static void NewWaves()
        {
            _spawnCount = -1;
            _waveLevel = 1;
        }

        public static int GetWaveLevel() => _waveLevel;
       
        public static void GenerateWave()
        {
            enemyTimer += Globals.TotalSeconds;

            GenerateMap();
            GenerateEnemies();
        }


        #region Enemies
        private static void GenerateEnemies()
        {
            if (isGenerating && enemyTimer > enemySpawnTime && _count < _spawnCount)
            {
                GenerateСockroaches();
                GenerateMice();

                if (_count >= _spawnCount)
                    isGenerating = false;

                enemyTimer = 0;
            }
        }

        private static void GenerateСockroaches()
        {
            EnemyManager.AddEnemy();
            _count++;

            position = EnemyManager.Enemies.Last().position;

            for (int i = 0; i < _random.Next(2, 5); i++)
            {
                if (_count >= _spawnCount)
                {
                    isGenerating = false;
                    break;
                }

                EnemyManager.AddNeighborEnemy(position);
                _count++;

                position = new(position.X + _random.Next(-30, 30), position.Y + _random.Next(-30, 30));
            }
        }

        private static void GenerateMice()
        {
            if (_waveLevel > 4 && _random.Next(0, 2) == 1)
            {
                EnemyManager.AddHardEnemy();
                position = EnemyManager.Enemies.Last().position;

                if (_random.Next(0, 2) == 1)
                {
                    for (int i = 0; i < _random.Next(0, 3); i++)
                    {
                        EnemyManager.AddNeighborHardEnemy(position);

                        position = new(position.X + _random.Next(-30, 30), position.Y + _random.Next(-30, 30));
                    }
                }
            }
        }

        #endregion

        #region Map
        private static void GenerateMap()
        {
            if (EnemyManager.Enemies.Count() == 0 && !isGenerating)
            {
                WavesGenerator();

                _count = 0;
                _spawnCount += 2;
                _waveLevel++;
                isGenerating = true;
            }
        }

        private static void ScoreChanger()
        {
            GameState.scoreWave.IncrementScore(1);
            if (GameState.highscoreWave.GetScore < GameState.scoreWave.GetScore)
            {
                GameState.highscoreWave.IncrementScore(1);
            }
        }

        private static void WavesGenerator()
        {
            ScoreChanger();

            EnemyManager.DeleteEnemies();
            ProjectileManager.DeleteTiles();
            WallManager.DeleteWalls();


            for (int i = 0; i < Math.Min((_waveLevel / 5) + 1, 4); i++)
                WallManager.GenerateWalls();


            AdditionalObjectsGenerator();
        }

        private static void AdditionalObjectsGenerator()
        {

            if (_waveLevel > 3 && _waveLevel % 2 == 0)
            {
                BonusManager.CreateBonus(new(_random.Next(100, Globals.Bounds.X), _random.Next(100, Globals.Bounds.Y)));
            }
        }

        #endregion
    }
}
