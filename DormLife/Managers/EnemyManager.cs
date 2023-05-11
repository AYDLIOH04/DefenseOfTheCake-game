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

        private static void GenerateWaveEnemy()
        {
            for (int i = 1; i <= Math.Min(10, _spawnCount); i++)
            {
                AddEnemy();
            }

            _spawnCount++;
        }

        public static void Update(Cake cake)
        {
            foreach (var enemy in Enemies)
            {
                if ((enemy.position - cake.position).Length() < 30)
                {
                    enemy.TakeDamage(100);
                    cake.TakeDamage(5);
                }
            }

            _spawnTime -= Globals.TotalSeconds;
            while (_spawnTime <= 0)
            {
                _spawnTime += _spawnCooldown;
                if (Enemies.Count() == 0)
                    GenerateWaveEnemy();
            }

            
            

            foreach (var enemy in Enemies)
            {
                enemy.Update(cake);
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
