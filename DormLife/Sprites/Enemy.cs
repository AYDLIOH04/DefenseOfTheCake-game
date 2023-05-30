using DormLife.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Sprites
{
    public class Enemy : Sprite
    {
        public int HP { get; private set; }
        public int Damage;
        public bool Buff;


        public Enemy(Texture2D texture, Vector2 position, float speed, int hp, int damage) : base(texture, position, speed)
        {
            this.speed = speed;
            HP = hp;
            Damage = damage;
            Buff = false;
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
        }

        public void Update(Cake cake, List<Wall> walls, List<Enemy> enemies)
        {
            // Обход других врагов
            foreach (Enemy otherEnemy in enemies)
            {
                if (otherEnemy != this && CheckRectangleCollision(otherEnemy))
                {
                    Vector2 awayFromOtherEnemy = Vector2.Normalize(position - otherEnemy.position);
                    position += awayFromOtherEnemy * speed * Globals.TotalSeconds;
                }
            }

            // Проверка столкновения со стенами
            bool isCollidingWithWall = false;
            foreach (var wall in walls)
            {
                if (CheckRectangleCollision(wall))
                {
                    isCollidingWithWall = true;
                    break;
                }
            }

            if (isCollidingWithWall)
            {
                // Враг столкнулся со стеной
                Vector2 toPlayer = cake.position - position;
                rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);

                // Вычисление направления для обхода препятствия
                Vector2 obstacleAvoidanceDir = new Vector2(-toPlayer.Y, toPlayer.X);
                obstacleAvoidanceDir.Normalize();

                position += obstacleAvoidanceDir * speed * Globals.TotalSeconds;
            }
            else
            {
                // Враг двигается в сторону тортика
                Vector2 toCake = cake.position - position;
                rotation = (float)Math.Atan2(toCake.Y, toCake.X);

                toCake.Normalize();
                position += toCake * speed * Globals.TotalSeconds;
            }
        }


    }
}
