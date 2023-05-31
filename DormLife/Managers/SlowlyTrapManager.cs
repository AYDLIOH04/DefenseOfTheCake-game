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
    public class SlowlyTrapManager
    {
        private static Texture2D _textureSlowlyTrap;
        public static List<SlowlyTrap> SlowlyTraps;

        public static int currentHaveCount;
        public readonly static int Limit = 5;


        public static void Init()
        {
            _textureSlowlyTrap = Globals.Content.Load<Texture2D>("sprites/slowlytrap");
            SlowlyTraps = new();

            currentHaveCount = 0;
        }

        public static void AddSlowlyTrap(Vector2 position, float rotation)
        {
            SoundManager.PlaySoundEffect("trapspawn");
            currentHaveCount--;
            SlowlyTraps.Add(new(_textureSlowlyTrap, position, rotation));
        }


        public static void Update(List<Enemy> enemies)
        {
            foreach (var trap in SlowlyTraps)
            {
                trap.Update(enemies);
            }

            SlowlyTraps.RemoveAll(trap =>
            {
                if (trap.IsTimeOver)
                {
                    SoundManager.PlaySoundEffect("trapexp");
                    return true;
                }
                return false;
            });

        }

        public static void Draw()
        {
            foreach (var trap in SlowlyTraps)
            {
                trap.Draw();
            }
        }
    }
}
