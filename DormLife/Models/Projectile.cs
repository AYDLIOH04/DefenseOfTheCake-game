using DormLife.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Models
{
    public class Projectile : Sprite
    {
        public Vector2 Direction { get; set; }
        public float Lifespan { get; set; }
        public int Damage { get; set; }
        
        public Projectile(Texture2D texture, ProjectileData data, int damage, float speed = 0) : base(texture, new Vector2(data.Position.X + 24, data.Position.Y + 17), speed)
        {
            this.speed = data.Speed;
            rotation = data.Rotation;
            Direction = new((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            Lifespan = data.Lifespan;
            Damage = damage;
        }

        public void Destroy()
        {
            Lifespan = 0;
        }

        public void Update()
        {
            position += Direction * speed * Globals.TotalSeconds;
            Lifespan -= Globals.TotalSeconds;
        }
    }
}
