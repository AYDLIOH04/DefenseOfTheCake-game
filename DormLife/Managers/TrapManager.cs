using DormLife.GameObjects;
using DormLife.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public class TrapManager
    {
        private static Texture2D _textureTrap;
        public static List<Trap> Traps;

        public static int currentHaveCount;
        public readonly static int Limit = 5;


        public static void Init()
        {
            _textureTrap = Globals.Content.Load<Texture2D>("sprites/trap");
            Traps = new();

            currentHaveCount = 0;
        }

        public static void AddTrap(Vector2 position, float rotation)
        {
            SoundManager.PlaySoundEffect("trapspawn");
            currentHaveCount--;
            Traps.Add(new(_textureTrap, position, rotation));
        }


        public static void Update()
        {
            Traps.RemoveAll(trap =>
            {
                if (trap.HP <= 0)
                {
                    SoundManager.PlaySoundEffect("trapexp");
                    return true;
                }
                return false;
            });

        }

        public static void Draw()
        {
            foreach (var trap in Traps)
            {
                trap.Draw();
            }
        }
    }
}
