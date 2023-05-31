using DormLife.GameObjects;
using DormLife.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public class TurretManager
    {
        private static Texture2D _textureTurret;
        public static List<Turret> Turrets;

        public static int currentHaveCount;
        public readonly static int Limit = 5;

        private static SpriteFont _font;


        public static void Init()
        {
            _textureTurret = Globals.Content.Load<Texture2D>("sprites/turret");
            Turrets = new();

            _font = Globals.Content.Load<SpriteFont>("Font");

            currentHaveCount = 0;
        }

        public static void AddTurret(Vector2 position, float rotation)
        {
            SoundManager.PlaySoundEffect("trapspawn");
            currentHaveCount--;
            Turrets.Add(new(_textureTurret, position, rotation));
        }


        public static void Update(List<Enemy> enemies)
        {
            foreach (var turret in Turrets)
            {
                turret.Update(enemies);
            }

            Turrets.RemoveAll(turret =>
            {
                if (turret.HP <= 0)
                {
                    SoundManager.PlaySoundEffect("trapexp");
                    return true;
                }
                return false;
            });

        }

        public static void Draw()
        {
            foreach (var turret in Turrets)
            {
                turret.Draw();
                Globals.SpriteBatch.DrawString(_font, $"{turret.HP}", new(turret.Rectangle.X + 25, turret.Rectangle.Y - 25), Color.White);
            }
        }
    }
}
