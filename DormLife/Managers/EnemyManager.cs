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

        private static Random _random;
        private static int _padding;

        public static void Init()
        {
            _enemyTexture = Globals.Content.Load<Texture2D>("sprites/monster");
            _hardEnemyTexture = Globals.Content.Load<Texture2D>("sprites/hardmonster");
            _random = new();
            _padding = _enemyTexture.Width / 2;

            Enemies = new();
        }

        private static Vector2 RandomPosition()
        {
            var random = new Random();
            var side = new Random().Next(0, 4);

            if (side == 0) return new Vector2(random.Next(-10, Globals.Bounds.X), 0);
            if (side == 1) return new Vector2(Globals.Bounds.X, random.Next(-10, Globals.Bounds.Y));
            if (side == 2) return new Vector2(random.Next(-10, Globals.Bounds.X), Globals.Bounds.Y);
            return new Vector2(0, random.Next(-10, Globals.Bounds.Y));
        }

        public static void AddEnemy()
        {
            var speed = _random.Next(60, 120);
            Enemies.Add(new(_enemyTexture, RandomPosition(), speed, 1, 5));
        }

        public static void AddNeighborEnemy(Vector2 position)
        {
            var speed = _random.Next(60, 120);
            Enemies.Add(new(_enemyTexture, position, speed, 1, 5));
        }

        public static void AddHardEnemy()
        {
            var speed = _random.Next(40, 60);
            Enemies.Add(new(_hardEnemyTexture, RandomPosition(), speed, 5, 20));
        }

        public static void AddNeighborHardEnemy(Vector2 position)
        {
            var speed = _random.Next(40, 60);
            Enemies.Add(new(_hardEnemyTexture, position, speed, 5, 20));
        }

        public static void Update(Cake cake, List<Wall> walls)
        {
            foreach (var enemy in Enemies)
            {
                if ((enemy.position - cake.position).Length() < 30)
                {
                    enemy.TakeDamage(100);
                    cake.TakeDamage(enemy.Damage);
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
