using DormLife.GameObjects;
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
    public class EnemyManager
    {
        public static List<Enemy> Enemies { get; private set; }
        private static Texture2D _enemyTexture;
        private static Texture2D _hardEnemyTexture;

        private static float _spawnCooldown;
        private static float _spawnTime;
        private static Random _random;
        private static int _padding;

        public static void Init()
        {
            _enemyTexture = Globals.Content.Load<Texture2D>("sprites/monster");
            _hardEnemyTexture = Globals.Content.Load<Texture2D>("sprites/hardmonster");
            _spawnCooldown = 2f;
            _spawnTime = _spawnCooldown;
            _random = new();
            _padding = _enemyTexture.Width / 2;

            Enemies = new();
        }

        private static Vector2 RandomPosition()
        {
            float w = Globals.Bounds.X;
            float h = Globals.Bounds.Y;
            Vector2 pos = new();

            if (_random.NextDouble() < w / (w + h))
            {
                pos.X = (int)(_random.NextDouble() * w);
                pos.Y = (int)(_random.NextDouble() < 0.5 ? -_padding : h + _padding);
            }
            else
            {
                pos.X = (int)(_random.NextDouble() * h);
                pos.Y = (int)(_random.NextDouble() < 0.5 ? -_padding : w + _padding);
            }

            return pos;
        }

        public static void AddEnemy()
        {
            Enemies.Add(new(_enemyTexture, RandomPosition(), 100, 1, 5));
        }

        public static void AddHardEnemy()
        {
            Enemies.Add(new(_hardEnemyTexture, RandomPosition(), 50, 5, 20));
        }

        public static void Update(Cake cake, List<Wall> walls)
        {
            if (Enemies.Count() == 0)
                WaveManager.GenerateWave();

            /*
            _spawnTime -= Globals.TotalSeconds;
            while (_spawnTime <= 0)
            {
                _spawnTime += _spawnCooldown;
                
            }
            */

            foreach (var enemy in Enemies)
            {
                if ((enemy.position - cake.position).Length() < 30)
                {
                    enemy.TakeDamage(100);
                    cake.TakeDamage(enemy.Damage);
                    GameState._cakeHP.DecrementScore(enemy.Damage);
                }

                enemy.Update(cake, walls, Enemies);

            }

            Enemies.RemoveAll((enemy) => enemy.HP <= 0);
        }

        public static void Draw()
        {
            foreach (var enemy in Enemies)
            {
                enemy.Draw();
            }
        }

        public static void DeleteEnemies()
        {
            Enemies.Clear();
        }
    }
}
