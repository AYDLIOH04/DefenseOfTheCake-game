using DormLife.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Managers
{
    public class ShopManager
    {
        public static int Tokens { get; private set; }

        public static void Init()
        {
            Tokens = 1000;
        }

        public static void IncrementTokens()
        {
            Tokens++;
        }
        
        public static void GetTrap(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 10)
            {
                if (TrapManager.currentHaveCount + TrapManager.Traps.Count < TrapManager.Limit) 
                {
                    TrapManager.currentHaveCount++;
                    Tokens -= 10;
                }
            }
        }

        public static void GetSpeedBuff(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 5)
            {
                Tokens -= 5;
                GameState.player.speed += 25;
            }
        }

        public static void GetHpCake(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 5)
            {
                Tokens -= 5;
                GameState.cake.GetHP(25);
            }
        }

        public static void GetShootingSpeedSpeedBuff(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 5)
            {
                if (ProjectileManager.ShootingSpeedBuff())
                {
                    Tokens -= 5;
                }
            }
        }

        public static void GetDamageBuff(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 10)
            {
                if (ProjectileManager.ShootingDamageBuff())
                {
                    Tokens -= 10;
                }
            }
        }
    }
}
