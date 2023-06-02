using DormLife.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

        private bool isArg = false;
        private Turret lastAgrTurret;
        private int octant = 0;

        public Enemy(Texture2D texture, Vector2 position, float speed, int hp, int damage) : base(texture, position, speed)
        {
            this.speed = speed;
            lastSpeed = speed;
            HP = hp;
            Damage = damage;
            Buff = false;
            isSlowly = false;
        }

        public override Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)position.X - texture.Width / 2 - 5, (int)position.Y - texture.Height / 2 - 5, texture.Width + 10, texture.Height + 10);
            }
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


        #region Update Helper-Methods
        private void IsTouchingHorizontalWall(Wall wall)
        {
            if (IsTouchingLeft(wall) || IsTouchingRight(wall))
            {
                if (octant == 1 || octant == 3)
                {
                    position += new Vector2(0, 1) * speed * Globals.TotalSeconds;
                }
                else if (octant == 2 || octant == 4)
                {
                    position += new Vector2(0, -1) * speed * Globals.TotalSeconds;
                }
            }
        }

        private void IsTouchingVerticalWall(Wall wall)
        {
            if (IsTouchingTop(wall) || IsTouchingBottom(wall))
            {
                if (octant == 1 || octant == 2)
                {
                    position += new Vector2(1, 0) * speed * Globals.TotalSeconds;
                }
                else if (octant == 3 || octant == 4)
                {
                    position += new Vector2(-1, 0) * speed * Globals.TotalSeconds;
                }
            }
        }

        private void GetOctant(Cake cake)
        {
            if (position.X < cake.position.X && position.Y < cake.position.Y)
            {
                octant = 1;
            }
            else if (position.X < cake.position.X && position.Y > cake.position.Y)
            {
                octant = 2;
            }
            else if (position.X > cake.position.X && position.Y < cake.position.Y)
            {
                octant = 3;
            }
            else if (position.X > cake.position.X && position.Y > cake.position.Y)
            {
                octant = 4;
            }
        }

        private void AgrToTurret(List<Turret> turrets)
        {
            if (turrets.Count > 0)
            {
                foreach (var turret in turrets)
                {
                    if (!isArg && CheckVectorCollision(turret, 175) && turret.HP > 0)
                    {
                        lastAgrTurret = turret;
                        isArg = true;
                        break;
                    }
                }
            }
        }

        private bool IsCollisionWalls(List<Wall> walls, Cake cake)
        {
            var isCollision = false;
            var countHor = false;
            var countVert = false;


            foreach (var wall in walls)
            {
                if (CheckRectangleCollision(wall))
                {
                    if (wall.IsHorizontal && !countVert)
                    {
                        if (Math.Abs(wall.position.X - cake.position.X) < 100)
                        {
                            if (Math.Abs(position.X - (wall.position.X + wall.Rectangle.X))
                                < Math.Abs(position.X - (wall.position.X - wall.Rectangle.X)))
                            {
                                position += new Vector2(1, 0) * speed * Globals.TotalSeconds;
                            }
                            else
                            {
                                position += new Vector2(-1, 0) * speed * Globals.TotalSeconds;
                            }
                        }
                        else if (wall.position.X < cake.position.X)
                        {
                            position += new Vector2(1, 0) * speed * Globals.TotalSeconds;
                        }
                        else
                        {
                            position += new Vector2(-1, 0) * speed * Globals.TotalSeconds;
                        }

                        countHor = true;
                    }
                    else if (!countHor)
                    {
                        if (Math.Abs(wall.position.Y - cake.position.Y) < 100)
                        {

                            if (Math.Abs(position.Y - (wall.position.Y + wall.Rectangle.Y))
                                < Math.Abs(position.Y - (wall.position.Y - wall.Rectangle.Y)))
                            {
                                position += new Vector2(0, 1) * speed * Globals.TotalSeconds;
                            }
                            else
                            {
                                position += new Vector2(0, -1) * speed * Globals.TotalSeconds;
                            }
                        }
                        else if (wall.position.Y < cake.position.Y)
                        {
                            position += new Vector2(0, 1) * speed * Globals.TotalSeconds;
                        }
                        else
                        {
                            position += new Vector2(0, -1) * speed * Globals.TotalSeconds;
                        }

                        countVert = true;
                    }

                    isCollision = true;
                }
            }

            return isCollision;
        }

        private void IsNotCollisionWalls(List<Enemy> enemies, Vector2 toCake)
        {
            foreach (Enemy otherEnemy in enemies)
            {
                if (otherEnemy != this && CheckRectangleCollision(otherEnemy))
                {
                    Vector2 awayFromOtherEnemy = Vector2.Normalize(position - otherEnemy.position);
                    position += awayFromOtherEnemy * speed * Globals.TotalSeconds;
                }
            }

            toCake.Normalize();
            position += toCake * speed * Globals.TotalSeconds;
        }
        #endregion

        public void Update(Cake cake, List<Wall> walls, List<Enemy> enemies, List<Turret> turrets)
        {
            GetOctant(cake);

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

            AgrToTurret(turrets);

            var isCollision = IsCollisionWalls(walls, cake);

            Vector2 toCake = cake.position - position;

            if (!isCollision)
            {
                IsNotCollisionWalls(enemies, toCake);
            }

            rotation = (float)Math.Atan2(toCake.Y, toCake.X);
        }
    }
}

