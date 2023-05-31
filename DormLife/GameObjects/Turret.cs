using DormLife.Managers;
using DormLife.Models;
using DormLife.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.GameObjects
{
    public class Turret : Sprite
    {
        private float _eventTimer;
        private float _shootingDelay;

        private TimeSpan _creationTime;
        private float _lifeTime = 25f;
        public bool IsTimeOver { get; private set; }

        public Turret(Texture2D texture, Vector2 position, float speed = 0)
        : base(texture, position, speed)
        {
            _eventTimer = 0;
            _shootingDelay = 0.5f;

            IsTimeOver = false;
            _creationTime = Globals.GameTime.TotalGameTime;
        }

        public void Update(List<Enemy> enemies)
        {
            _eventTimer += Globals.TotalSeconds;

            if ((Globals.GameTime.TotalGameTime - _creationTime).TotalSeconds >= _lifeTime)
            {
                IsTimeOver = true;
            }

            foreach (var enemy in enemies)
            {
                if (CheckVectorCollision(enemy, 500))
                {
                    

                    if (_eventTimer > _shootingDelay)
                    {
                        Vector2 toEnemy = enemy.position - position;
                        rotation = (float)Math.Atan2(toEnemy.Y, toEnemy.X);

                        ProjectileData pd = new ProjectileData()
                        {
                            Position = new(Rectangle.X + Rectangle.Width / 2 - 10, Rectangle.Y + Rectangle.Height / 2 - 10),
                            Rotation = rotation,
                            Lifespan = 3.5f,
                            Speed = 600,
                        };

                        ProjectileManager.AddProjectile(pd);
                        _eventTimer = 0;
                    }
                }
            }
        }
    }
}
