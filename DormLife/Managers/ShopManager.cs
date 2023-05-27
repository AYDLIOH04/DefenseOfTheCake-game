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

        private static int _tokens;
        public static int Tokens => _tokens;

        public static void Init()
        {
             _tokens = 0;
        }

        public static void IncrementTokens()
        {
            _tokens++;
        }
        

        public static void GetSpeedBuff(object sender = null, EventArgs e = null)
        {
            if (_tokens >= 5)
            {
                _tokens -= 5;
                GameState.player.speed += 25;
            }
        }

        public static void GetShootingSpeedSpeedBuff(object sender = null, EventArgs e = null)
        {
            if (_tokens >= 5)
            {
                _tokens -= 5;
                ProjectileManager.ShootingSpeedBuff();
            }
        }

        public static void GetDamageBuff(object sender = null, EventArgs e = null)
        {
            if (_tokens >= 10)
            {
                _tokens -= 10;
                ProjectileManager.ShootingDamageBuff();
            }
        }
    }
}
