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
            Tokens = 100;
        }

        public static void IncrementTokens()
        {
            Tokens++;
        }

        public static void GetTurret(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 25)
            {
                if (TurretManager.currentHaveCount + TurretManager.Turrets.Count < TurretManager.Limit)
                {
                    SoundManager.PlaySoundEffect("moneyspend");
                    TurretManager.currentHaveCount++;
                    Tokens -= 25;
                }
            }
        }

        public static void GetSlowlyTrap(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 5)
            {
                if (SlowlyTrapManager.currentHaveCount + SlowlyTrapManager.SlowlyTraps.Count < SlowlyTrapManager.Limit)
                {
                    SoundManager.PlaySoundEffect("moneyspend");
                    SlowlyTrapManager.currentHaveCount++;
                    Tokens -= 5;
                }
            }
        }

        public static void GetTrap(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 10)
            {
                if (TrapManager.currentHaveCount + TrapManager.Traps.Count < TrapManager.Limit) 
                {
                    SoundManager.PlaySoundEffect("moneyspend");
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
                SoundManager.PlaySoundEffect("buff");
                GameState.gameManager.player.speed += 25;
            }
        }

        public static void GetHpCake(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 5)
            {
                Tokens -= 5;
                SoundManager.PlaySoundEffect("buff");
                GameState.gameManager.cake.GetHP(25);
            }
        }

        public static void GetShootingSpeedSpeedBuff(object sender = null, EventArgs e = null)
        {
            if (Tokens >= 5)
            {
                if (ProjectileManager.ShootingSpeedBuff())
                {
                    SoundManager.PlaySoundEffect("buff");
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
                    SoundManager.PlaySoundEffect("buff");
                    Tokens -= 10;
                }
            }
        }
    }
}
