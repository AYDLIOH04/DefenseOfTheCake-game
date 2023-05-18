using DormLife.GameObjects;
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
        private static Texture2D texture;
        public static List<Projectile> Projectiles { get; private set; }

        public static void Init()
        {
            texture = Globals.Content.Load<Texture2D>("arms/range_arm");

            Projectiles = new(); 
        }

        public static void AddProjectile(ProjectileData data)
        {
            Projectiles.Add(new(texture, data));
        }

        public static void Update(List<Enemy> enemies, List<Wall> walls, Cake cake)
        {
            foreach (var p in Projectiles)
            {
                p.Update();

                if ((p.position - cake.position).Length() < 30)
                {
                    p.Destroy();
                }

                foreach (var wall in walls)
                {
                    if (p.CheckCollision(wall))
                    {
                        p.Destroy();

                    }
                } 

                foreach (var enemy in enemies)
                {
                    if (enemy.HP <= 0) continue;
                    
                    if ((p.position - enemy.position).Length() < 32)
                    {
                        if (enemy.HP == 1)
                        {
                            GameState.enemyKills.IncrementScore(1);
                        }
                        enemy.TakeDamage(1);
                        p.Destroy();
                        break;
                    }
                }
            }

            Projectiles.RemoveAll((p) => p.Lifespan <= 0);
        }

        public static void DeleteTiles()
        {
            Projectiles.Clear();
        }

        public static void Draw()
        {
            foreach (var p in Projectiles)
            {
                p.Draw();
            }
        }
    }
}
