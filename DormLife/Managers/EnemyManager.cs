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

        #region Enemy Params
        private static int _speedEnemy;
        private static int _damageEnemy;
        private static int _hpEnemy;

        private static int _speedHardEnemy;
        private static int _damageHardEnemy;
        private static int _hpHardEnemy;
        #endregion

        private static Random _random;

        #region Init
        public static void Init()
        {
            _enemyTexture = Globals.Content.Load<Texture2D>("sprites/monster");
            _hardEnemyTexture = Globals.Content.Load<Texture2D>("sprites/hardmonster");
            _random = new();
            Enemies = new();

            InitEnemyParam();
        }

        private static void InitEnemyParam()
        {
            _speedEnemy = 0;
            _speedHardEnemy = 0;

            _damageEnemy = 5;
            _damageHardEnemy = 15;

            _hpEnemy = 1;
            _hpHardEnemy = 5;
        }
        #endregion

        #region Buffs
        public static void BuffHP()
        {
            Buff();

            _hpEnemy += 1;
            _hpHardEnemy += 3;
        }

        public static void Buff()
        {
            _speedEnemy += 5;
            _speedHardEnemy += 5;

            _damageEnemy += 1;
            _damageHardEnemy += 2;
        }
        #endregion

        #region Random Position
        private static Vector2 RandomPosition()
        {
            var random = new Random();
            var side = new Random().Next(0, 4);

            if (side == 0) return new Vector2(random.Next(-10, Globals.Bounds.X), 0);
            if (side == 1) return new Vector2(Globals.Bounds.X, random.Next(-10, Globals.Bounds.Y));
            if (side == 2) return new Vector2(random.Next(-10, Globals.Bounds.X), Globals.Bounds.Y);
            return new Vector2(0, random.Next(-10, Globals.Bounds.Y));
        }
        #endregion

        #region Add Enemies
        public static void AddEnemy()
        {
            var rndSpeedBuf = _random.Next(60, 125);
            Enemies.Add(new(_enemyTexture, RandomPosition(), rndSpeedBuf + _speedEnemy, _hpEnemy, _damageEnemy));
        }

        public static void AddNeighborEnemy(Vector2 position)
        {
            var rndSpeedBuf = _random.Next(60, 125);
            Enemies.Add(new(_enemyTexture, position, rndSpeedBuf + _speedEnemy, _hpEnemy, _damageEnemy));
        }

        public static void AddHardEnemy()
        {
            var rndSpeedBuf = _random.Next(40, 75);
            Enemies.Add(new(_hardEnemyTexture, RandomPosition(), rndSpeedBuf + _speedHardEnemy, _hpHardEnemy, _damageHardEnemy));
        }

        public static void AddNeighborHardEnemy(Vector2 position)
        {
            var rndSpeedBuf = _random.Next(40, 75);
            Enemies.Add(new(_hardEnemyTexture, position, rndSpeedBuf + _speedHardEnemy, _hpHardEnemy, _damageHardEnemy));
        }
        #endregion

        public static void Update(Cake cake, List<Wall> walls, List<Trap> traps)
        {
            foreach (var enemy in Enemies)
            {
                foreach (var trap in traps)
                {
                    if (trap.CheckRectangleCollision(enemy))
                    {
                        trap.TakeDamage(1);
                        enemy.TakeDamage(100);
                    }
                }

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
