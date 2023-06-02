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
    public class WallManager
    {
        private static Texture2D _textureGorizontal;
        private static Texture2D _textureVertical;
        private static Random _random;

        private static float w = Globals.Bounds.X;
        private static float h = Globals.Bounds.Y;

        public static List<Wall> Walls { get; private set;  }

        public static void Init()
        {
            _textureGorizontal = Globals.Content.Load<Texture2D>("walls/gorwall");
            _textureVertical = Globals.Content.Load<Texture2D>("walls/vertwall");
            _random = new();

            Walls = new();
        }


        public static void AddWall(Texture2D texture, Vector2 position, bool isHorizontal)
        {
            Walls.Add(new(texture, position, isHorizontal));
        }

        public static void AddHorizontalWall(Vector2 position)
        {
            AddWall(_textureGorizontal, position, true);
        }
        public static void AddVerticalWall(Vector2 position)
        {
            AddWall(_textureVertical, position, false);
        }


        public static void GenerateFirstMap()
        {
            AddHorizontalWall(new(470, 200));
            AddVerticalWall(new(170, 470));

            AddHorizontalWall(new(Globals.Bounds.X / 2, 150));
            AddHorizontalWall(new(Globals.Bounds.X - 300, 150));

            AddVerticalWall(new(Globals.Bounds.X - 400, 520));

            AddHorizontalWall(new(400, Globals.Bounds.Y - 100));
            AddVerticalWall(new(100, Globals.Bounds.Y - 100));

            AddHorizontalWall(new(Globals.Bounds.X - 530, Globals.Bounds.Y - 150));
            AddVerticalWall(new(Globals.Bounds.X - 500, Globals.Bounds.Y - 150));

            AddHorizontalWall(new(Globals.Bounds.X / 2, Globals.Bounds.Y - 200));

        }

        public static void GenerateSecondMap()
        {
            AddVerticalWall(new(200, 370));
            AddVerticalWall(new(200, Globals.Bounds.Y - 100));
            AddVerticalWall(new(290, Globals.Bounds.Y / 2 + 150));

            AddVerticalWall(new(Globals.Bounds.X - 200, 370));
            AddVerticalWall(new(Globals.Bounds.X - 200, Globals.Bounds.Y - 100));
            AddVerticalWall(new(Globals.Bounds.X - 290, Globals.Bounds.Y / 2 + 150));

            AddHorizontalWall(new(700, 130));
            AddHorizontalWall(new(Globals.Bounds.X / 2 + 150, 120));
            AddHorizontalWall(new(Globals.Bounds.X - 400, 130));

            AddHorizontalWall(new(700, Globals.Bounds.Y - 130));
            AddHorizontalWall(new(Globals.Bounds.X / 2 + 150, Globals.Bounds.Y - 120));
            AddHorizontalWall(new(Globals.Bounds.X - 400, Globals.Bounds.Y - 130));
        }

        public static void GenerateThirdMap()
        {
            AddHorizontalWall(new(400, 200));
            AddHorizontalWall(new(Globals.Bounds.X - 300, 200));

            AddVerticalWall(new(200, Globals.Bounds.Y - 100));
            AddVerticalWall(new(Globals.Bounds.X - 450, Globals.Bounds.Y / 2 + 170));
            AddVerticalWall(new(Globals.Bounds.X - 200, Globals.Bounds.Y - 100));


            AddHorizontalWall(new(Globals.Bounds.X / 2 - 230, Globals.Bounds.Y / 2 - 200));
            AddVerticalWall(new(Globals.Bounds.X / 2 - 530, Globals.Bounds.Y / 2 + 70));
            AddHorizontalWall(new(Globals.Bounds.X / 2 - 200, Globals.Bounds.Y / 2 + 250));

            AddHorizontalWall(new(Globals.Bounds.X / 2 + 280, Globals.Bounds.Y / 2 - 200));
            AddVerticalWall(new(Globals.Bounds.X / 2 + 310, Globals.Bounds.Y / 2 + 70));
            AddHorizontalWall(new(Globals.Bounds.X / 2 + 250, Globals.Bounds.Y / 2 + 250));
        }

        public static void GenerateFourthMap()
        {
            AddHorizontalWall(new(450, 120));
            AddHorizontalWall(new(Globals.Bounds.X / 2 - 50, 120));
            AddHorizontalWall(new(Globals.Bounds.X / 2 + 365, 120));
            AddHorizontalWall(new(Globals.Bounds.X - 150, 120));

            AddHorizontalWall(new(Globals.Bounds.X / 2 - 180, 230));
            AddHorizontalWall(new(Globals.Bounds.X / 2 + 445, 230));

            AddVerticalWall(new(180, Globals.Bounds.Y / 2));
            AddVerticalWall(new(180, Globals.Bounds.Y / 2 + 400));

            AddVerticalWall(new(Globals.Bounds.X - 180, Globals.Bounds.Y / 2));
            AddVerticalWall(new(Globals.Bounds.X - 180, Globals.Bounds.Y / 2 + 400));

            AddHorizontalWall(new(Globals.Bounds.X / 2 - 50, Globals.Bounds.Y -  120));
            AddHorizontalWall(new(Globals.Bounds.X / 2 + 350, Globals.Bounds.Y - 120));
        }

        public static void GenerateFivethMap()
        {
            AddHorizontalWall(new(450, 120));
            AddHorizontalWall(new(Globals.Bounds.X / 2 - 100, 120));
            AddHorizontalWall(new(Globals.Bounds.X / 2 + 300, 120));

            AddHorizontalWall(new(650, Globals.Bounds.Y - 120));
            AddHorizontalWall(new(Globals.Bounds.X / 2 + 100, Globals.Bounds.Y - 120));
            AddHorizontalWall(new(Globals.Bounds.X / 2 + 500, Globals.Bounds.Y - 120));

            AddVerticalWall(new(250, Globals.Bounds.Y / 2));
            AddVerticalWall(new(250, Globals.Bounds.Y / 2 + 380));

            AddVerticalWall(new(Globals.Bounds.X / 2 + 380, Globals.Bounds.Y / 2));
            AddVerticalWall(new(Globals.Bounds.X / 2 + 600, Globals.Bounds.Y / 2));

            AddVerticalWall(new(Globals.Bounds.X / 2 + 500, Globals.Bounds.Y / 2 + 220));
        }

        public static void GenerateMap()
        {
            var rndMap = _random.Next(0, 5);
            if (rndMap == 0)
            {
                GenerateFirstMap();
            } 
            else if (rndMap == 1)
            {
                GenerateSecondMap();
            } 
            else if (rndMap == 2)
            {
                GenerateThirdMap();
            } 
            else if (rndMap == 3)
            {
                GenerateFourthMap();
            }
            else
            {
                GenerateFivethMap();
            }
        }

        
        public static void DeleteWalls()
        {
            Walls.Clear();
        }

        public static void Draw()
        {
            foreach (var wall in Walls)
            {
                wall.Draw();
            }
        }
    }
}
