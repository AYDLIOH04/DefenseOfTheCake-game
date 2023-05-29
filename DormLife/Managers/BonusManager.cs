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
    public class BonusManager
    {
        private static Texture2D _textureBonusHP;
        private static Texture2D _textureBonusULT;

        private static Random _random;

        public static List<Bonus> Bonuses;

        public static void Init()
        {
            _textureBonusHP = Globals.Content.Load<Texture2D>("bonus/hp");
            _textureBonusULT = Globals.Content.Load<Texture2D>("bonus/ult");
            _random = new Random();
            Bonuses = new();
        }

        public static void CreateBonus(Vector2 position)
        {
            var textureRnd = _random.Next(0, 2);

            if (textureRnd == 1)
            {
                Bonuses.Add(new Bonus(_textureBonusHP, position, Bonus.BonusType.BottleHP));
            }
            else
            {
                Bonuses.Add(new Bonus(_textureBonusULT, position, Bonus.BonusType.BottleULT));
            }

        }

        private static void DestroyBonuses(Bonus bonus)
        {
            Bonuses.Clear();
        }

        private static void GiveHP(Cake cake)
        {
            cake.GetHP(25);
        }

        private static void GiveULT()
        {
            ProjectileManager.SetUltProjectiles();
        }


        public static void Update(MainHero player, Cake cake)
        {
            foreach (var bonus in Bonuses)
            {
                if (bonus.CheckVectorCollision(player, 50))
                {
                    if (bonus.Type == Bonus.BonusType.BottleHP)
                    {
                        SoundManager.PlaySoundEffect("healbottle");
                        GiveHP(cake);
                    }
                    else
                    {
                        GiveULT();
                        SoundManager.PlaySoundEffect("ultbottle");
                    }

                    bonus.IsTaken = true;
                }
            }

            Bonuses.RemoveAll(bonus => bonus.IsTaken);
        }

        public static void Draw()
        {
            foreach (var bonus in Bonuses)
            {
                bonus.Draw();
            }
        }
    }
}
