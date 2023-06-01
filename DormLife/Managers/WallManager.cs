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

        public static void GenerateFirstMap()
        {
            AddWall(_textureGorizontal, new(470, 200), true);
            AddWall(_textureVertical, new(170, 470), false);

            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2, 150), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X - 300, 150), true);

            AddWall(_textureVertical, new(Globals.Bounds.X - 400, 520), false);

            AddWall(_textureGorizontal, new(400, Globals.Bounds.Y - 100), true);
            AddWall(_textureVertical, new(100, Globals.Bounds.Y - 100), false);

            AddWall(_textureGorizontal, new(Globals.Bounds.X - 530, Globals.Bounds.Y - 150), true);
            AddWall(_textureVertical, new(Globals.Bounds.X - 500, Globals.Bounds.Y - 150), false);

            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2, Globals.Bounds.Y - 200), true);

        }

        public static void GenerateSecondMap()
        {
            AddWall(_textureVertical, new(200, 370), false);
            AddWall(_textureVertical, new(200, Globals.Bounds.Y - 100), false);
            AddWall(_textureVertical, new(290, Globals.Bounds.Y / 2 + 150), false);

            AddWall(_textureVertical, new(Globals.Bounds.X - 200, 370), false);
            AddWall(_textureVertical, new(Globals.Bounds.X - 200, Globals.Bounds.Y - 100), false);
            AddWall(_textureVertical, new(Globals.Bounds.X - 290, Globals.Bounds.Y / 2 + 150), false);

            AddWall(_textureGorizontal, new(700, 130), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 + 150, 120), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X - 400, 130), true);

            AddWall(_textureGorizontal, new(700, Globals.Bounds.Y - 130), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 + 150, Globals.Bounds.Y - 120), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X - 400, Globals.Bounds.Y - 130), true);

        }

        public static void GenerateThirdMap()
        {
            AddWall(_textureGorizontal, new(400, 200), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X - 300, 200), true);

            AddWall(_textureVertical, new(200, Globals.Bounds.Y - 100), false);
            AddWall(_textureVertical, new(Globals.Bounds.X - 450, Globals.Bounds.Y / 2 + 170), false);
            AddWall(_textureVertical, new(Globals.Bounds.X - 200, Globals.Bounds.Y - 100), false);


            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 - 230, Globals.Bounds.Y / 2 - 200), true);
            AddWall(_textureVertical, new(Globals.Bounds.X / 2 - 530, Globals.Bounds.Y / 2 + 70), false);
            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 - 200, Globals.Bounds.Y / 2 + 250), true);

            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 + 280, Globals.Bounds.Y / 2 - 200), true);
            AddWall(_textureVertical, new(Globals.Bounds.X / 2 + 310, Globals.Bounds.Y / 2 + 70), false);
            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 + 250, Globals.Bounds.Y / 2 + 250), true);
        }

        public static void GenerateFourthMap()
        {
            AddWall(_textureGorizontal, new(450, 120), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 - 50, 120), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 + 365, 120), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X - 150, 120), true);

            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 - 180, 230), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 + 445, 230), true);

            AddWall(_textureVertical, new(180, Globals.Bounds.Y / 2), false);
            AddWall(_textureVertical, new(180, Globals.Bounds.Y / 2 + 400), false);

            AddWall(_textureVertical, new(Globals.Bounds.X - 180, Globals.Bounds.Y / 2), false);
            AddWall(_textureVertical, new(Globals.Bounds.X - 180, Globals.Bounds.Y / 2 + 400), false);

            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 - 50, Globals.Bounds.Y -  120), true);
            AddWall(_textureGorizontal, new(Globals.Bounds.X / 2 + 350, Globals.Bounds.Y - 120), true);
        }


        public static void GenerateMap()
        {
            var rndMap = _random.Next(0, 4);
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
            else
            {
                GenerateFourthMap();
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
