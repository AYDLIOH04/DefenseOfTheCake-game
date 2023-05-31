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
    public class SlowlyTrap : Sprite
    {
        private TimeSpan _creationTime;
        private float _lifeTime = 45f;
        public bool IsTimeOver { get; private set; }


        public SlowlyTrap(Texture2D texture, Vector2 position, float rotate, float speed = 0)
        : base(texture, position, speed)
        {
            rotation = rotate;

            IsTimeOver = false;
            _creationTime = Globals.GameTime.TotalGameTime;
        }

        public void Update(List<Enemy> enemies)
        {
            if ((Globals.GameTime.TotalGameTime - _creationTime).TotalSeconds >= _lifeTime)
            {
                IsTimeOver = true;
            }


            foreach (var enemy in enemies)
            {
                if (CheckVectorCollision(enemy, 60))
                {
                    enemy.ChangeSpeed(true);
                } 
                else
                {
                    enemy.ChangeSpeed(false);
                }
            }
        }
    }
}
