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

        private float lastSpeed;
        private bool isSlowly;



        public Enemy(Texture2D texture, Vector2 position, float speed, int hp, int damage) : base(texture, position, speed)
        {
            this.speed = speed;
            lastSpeed = speed;
            HP = hp;
            Damage = damage;
            Buff = false;
            isSlowly = false;
        }

        public void TakeDamage(int dmg)
        {
            HP -= dmg;
        }

        public void ChangeSpeed(bool makeSlowly)
        {
            if (makeSlowly && !isSlowly)
            {
                if (speed > 60)
                {
                    lastSpeed = speed;
                    speed -= (speed - 17);
                }
                else if (speed > 35)
                {
                    lastSpeed = speed;
                    speed -= 30;
                }

                isSlowly = true;
            }
            else if (!makeSlowly && isSlowly)
            {
                speed = lastSpeed;
                isSlowly = false;
            }
        }


        private bool isArg = false;
        private Turret lastAgrTurret;

        public void Update(Cake cake, List<Wall> walls, List<Enemy> enemies, List<Turret> turrets)
        {
            if (lastAgrTurret != null && lastAgrTurret.HP <= 0)
            {
                isArg = false;
                lastAgrTurret = null;
            }

            if (isArg && lastAgrTurret != null && lastAgrTurret.HP > 0)
            {
                Vector2 toTurret = lastAgrTurret.position - position;

                rotation = (float)Math.Atan2(toTurret.Y, toTurret.X);
                toTurret.Normalize();
                position += toTurret * speed * Globals.TotalSeconds;

                return;
            }

            if (turrets.Count > 0)
            {
                foreach (var turret in turrets)
                {
                    if (!isArg && CheckVectorCollision(turret, 200) && turret.HP > 0)
                    {
                        lastAgrTurret = turret;
                        isArg = true;
                        break;
                    }
                }
            }


            bool isCollidingWithWall = false;
            foreach (var wall in walls)
            {
                if (CheckRectangleCollision(wall))
                {
                    isCollidingWithWall = true;
                    break;
                }
            }

            Vector2 toCake = cake.position - position;
            rotation = (float)Math.Atan2(toCake.Y, toCake.X);

            if (isCollidingWithWall)
            {
                Vector2 obstacleAvoidanceDir = new Vector2(-toCake.Y, toCake.X);
                obstacleAvoidanceDir.Normalize();

                position += obstacleAvoidanceDir * speed * Globals.TotalSeconds;
            }
            else
            {
                foreach (Enemy otherEnemy in enemies)
                {
                    if (otherEnemy != this && CheckRectangleCollision(otherEnemy))
                    {
                        Vector2 awayFromOtherEnemy = Vector2.Normalize(position - otherEnemy.position);
                        position += awayFromOtherEnemy * speed * Globals.TotalSeconds;
                    }
                }
                // Враг двигается в сторону тортика
                toCake.Normalize();
                position += toCake * speed * Globals.TotalSeconds;
            }
        }
    }
}

