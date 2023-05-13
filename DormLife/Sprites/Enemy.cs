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

        public Enemy(Texture2D texture, Vector2 position, int hp = 1, int damage = 5) : base(texture, position)
        {
            speed = 100;
            HP = hp;
            Damage = damage;
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
        }

        public void Update(Cake cake, List<Wall> walls, List<Enemy> enemies)
        {

            foreach (Enemy otherEnemy in enemies)
            {
                if (otherEnemy != this && CheckCollision(otherEnemy))
                {
                    Vector2 awayFromOtherEnemy = Vector2.Normalize(position - otherEnemy.position);
                    position += awayFromOtherEnemy * speed * Globals.TotalSeconds;
                }
            }

            foreach (var wall in walls)
            {
                if ((position - wall.position).Length() < 50)
                {
                    var toPlayer = cake.position - position;
                    rotation = (float)Math.Atan2(toPlayer.Y, toPlayer.X);


                    var obstacleAvoidanceDir = new Vector2(-toPlayer.Y, toPlayer.X);
                    obstacleAvoidanceDir.Normalize();

                    position += obstacleAvoidanceDir * speed * Globals.TotalSeconds;
                    return; 
                }
            }

            var toCake = cake.position - position;
            rotation = (float)Math.Atan2(toCake.Y, toCake.X);

            toCake.Normalize();
            position += toCake * speed * Globals.TotalSeconds;
        }

    }
}
