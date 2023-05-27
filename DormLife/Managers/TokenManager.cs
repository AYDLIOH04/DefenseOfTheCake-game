using DormLife.GameObjects;
using DormLife.Sprites;
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
    public class TokenManager
    {
        private static Texture2D _textureToken;
        private static Random _random;
        public static List<Token> Tokens;


        public static void Init()
        {
            _textureToken = Globals.Content.Load<Texture2D>("bonus/token");
            _random = new();
            Tokens = new();
        }

        public static void CreateBonus(Vector2 position)
        {
            var rnd = _random.Next(0, 2);

            if (rnd == 1)
            {
                Tokens.Add(new Token(_textureToken, position));
            }
        }
        

        public static void Update(MainHero player)
        {
            foreach (var token in Tokens)
            {
                if (token.CheckVectorCollision(player, 70))
                {
                    ShopManager.IncrementTokens();
                    token.IsTaken = true;
                }
            }

            Tokens.RemoveAll(bonus => bonus.IsTaken);
        }

        public static void Draw()
        {
            foreach (var token in Tokens)
            {
                token.Draw();
            }
        }
    }
}
