﻿using DormLife.GameObjects;
using DormLife.Models;
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
    public class ProjectileManager
    {
        private static Texture2D _texture;
        private static Texture2D _textureUlt;
        public static List<Projectile> Projectiles { get; private set; }
        public static List<Projectile> UltProjectiles { get; private set; }
        public static bool IsUlt { get; private set; }
        public static int CountUlt { get; private set; }

        private static int _damage;
        private static int _shootingSpeed;

        public static int ShootingBuffCount { get; private set; }
        public readonly static int ShootingBuffLimit = 5;

        public static int DamageBuffCount { get; private set; }
        public readonly static int DamageBuffLimit = 10;


        public static void Init()
        {
            _texture = Globals.Content.Load<Texture2D>("arms/range_arm");
            _textureUlt = Globals.Content.Load<Texture2D>("arms/ult");

            IsUlt = false;
            CountUlt = 0;
            Projectiles = new(); 
            UltProjectiles = new();

            _damage = 1;
            _shootingSpeed = 0;

            ShootingBuffCount = 0;
            DamageBuffCount = 0;
        }

        public static bool ShootingDamageBuff()
        {
            if (DamageBuffCount < DamageBuffLimit)
            {
                _damage += 1;
                DamageBuffCount++;
                return true;
            }

            return false;
        }

        public static bool ShootingSpeedBuff()
        {
            if (ShootingBuffCount < ShootingBuffLimit)
            {
                _shootingSpeed += 25;
                GameState.gameManager.player.IncrementShootingDelay();
                ShootingBuffCount++;
                return true;
            }

            return false;
        }

        public static void SetUltProjectiles()
        {
            IsUlt = true;
            CountUlt += 5;
            GameState.scoreUltCount.SetScore(CountUlt);
        }

        public static void AddTurretProjectile(ProjectileData data)
        {
            Projectiles.Add(new(_texture, data, _damage));
            SoundManager.PlaySoundEffect("shoot");
        }

        public static void AddProjectile(ProjectileData data)
        {
            data.Speed += _shootingSpeed;
            Projectiles.Add(new(_texture, data, _damage));
            SoundManager.PlaySoundEffect("shoot");
        }

        public static void AddUltProjectile(ProjectileData data)
        {
            if (IsUlt)
            {
                SoundManager.PlaySoundEffect("shoot");
                CountUlt--;
                GameState.scoreUltCount.SetScore(CountUlt);
                UltProjectiles.Add(new(_textureUlt, data, 1000));
            }
        }

        public static void Update(List<Enemy> enemies, List<Wall> walls, Cake cake)
        {
            foreach (var p in Projectiles)
            {
                p.Update();

                if ((p.position - cake.position).Length() < 30)
                {
                    p.Destroy();
                    continue;
                }

                var isRemove = false;
                foreach (var wall in walls)
                {
                    if (p.CheckRectangleCollision(wall))
                    {
                        p.Destroy();
                        isRemove = true;
                        break;
                    }
                }

                if (isRemove) continue;

                foreach (var enemy in enemies)
                {
                    if (enemy.HP <= 0) continue;
                    
                    if ((p.position - enemy.position).Length() < 32)
                    {
                        enemy.TakeDamage(p.Damage);
                        SoundManager.PlaySoundEffect("nomissed");
                        if (enemy.HP < 1)
                        {
                            GameState.scoreEnemyKills.IncrementScore(1);
                            TokenManager.CreateToken(enemy.position);
                        }

                        p.Destroy();
                        break;
                    }
                }
            }

            foreach (var up in UltProjectiles)
            {
                up.Update();


                if (up.CheckVectorCollision(cake, 30))
                {
                    up.Destroy();
                    continue;
                }

                var isRemove = false;

                foreach (var wall in walls)
                {
                    if (up.CheckRectangleCollision(wall))
                    {
                        up.Destroy();
                        isRemove = true;
                        break;
                    }
                }

                if (isRemove) continue;


                foreach (var enemy in enemies)
                {
                    if (enemy.HP <= 0) continue;

                    if (up.CheckVectorCollision(enemy, 45))
                    {
                        enemy.TakeDamage(up.Damage);
                        if (enemy.HP < 1)
                        {
                            TokenManager.CreateToken(enemy.position);
                            GameState.scoreEnemyKills.IncrementScore(1);
                        }
                    }
                }
            }

            if (CountUlt == 0)
                IsUlt = false;

            Projectiles.RemoveAll((p) => p.Lifespan <= 0);
            UltProjectiles.RemoveAll((p) => p.Lifespan <= 0);
        }

        public static void DeleteTiles()
        {
            Projectiles.Clear();
            UltProjectiles.Clear();
        }

        public static void Draw()
        {
            foreach (var p in Projectiles)
            {
                p.Draw();
            }

            foreach (var up in UltProjectiles)
            {
                up.Draw();
            }
        }      
    }
}
