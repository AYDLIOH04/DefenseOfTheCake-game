using DormLife.GameObjects;
using DormLife.Sprites;
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
        public static List<Enemy> Enemies { get; } = new();
        private static Texture2D _texture;
        private static float _spawnCooldown;
        private static float _spawnTime;
        private static int _spawnCount = 1;
        private static Random _random;
        private static int _padding;

        public static void Init()
        {
            _texture = Globals.Content.Load<Texture2D>("sprites/monster");
            _spawnCooldown = 2f;
            _spawnTime = _spawnCooldown;
            _random = new();
            _padding = _texture.Width / 2;
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
            Enemies.Add(new(_texture, RandomPosition()));
        }

        public static void GenerateWave()
        {
            WallManager.DeleteWalls();
            if (_spawnCount > 5)
            {
                WallManager.GenerateWalls();
                WallManager.GenerateWalls();
            }
            else
            {
                WallManager.GenerateWalls();
            }

            for (int i = 1; i <= _spawnCount; i++)
            {
                AddEnemy();
            }

            if (_spawnCount % 10 == 0)
            {
                // TODO Кибербосс
            }

            if (_spawnCount % 5 == 0)
            {
                // TODO здоровья тортику
            }

            _spawnCount += 2;
        }

        public static void Update(Cake cake, List<Wall> walls)
        {
            /*
            _spawnTime -= Globals.TotalSeconds;
            while (_spawnTime <= 0)
            {
                _spawnTime += _spawnCooldown;
                
            }
            */

            if (Enemies.Count() == 0)
                GenerateWave();

            foreach (var enemy in Enemies)
            {
                if ((enemy.position - cake.position).Length() < 30)
                {
                    enemy.TakeDamage(100);
                    cake.TakeDamage(5);
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
    }
}
