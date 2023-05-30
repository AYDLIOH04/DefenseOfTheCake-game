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
            _textureGorizontal = Globals.Content.Load<Texture2D>("walls/big-gor-wall");
            _textureVertical = Globals.Content.Load<Texture2D>("walls/big-vert-wall");
            _random = new();

            Walls = new();
        }

        private static Vector2 SetPositionGorizontal()
        {
            float x = _random.Next(100, (int)w - 100);
            float y = _random.Next(100, (int)h - 100);

            if (x > (w / 2 - 200) && x < (w / 2 + 200)
                && y > (h / 2 - 200) && y < (h / 2 + 200))
                return SetPositionGorizontal();

            return new Vector2(x, y);
        }

        private static Vector2 SetPositionVertical()
        {
            float x = _random.Next(100, (int)w - 100);
            float y = _random.Next(100, (int)h - 100);

            if (x > (w / 2 - 200) && x < (w / 2 + 200)
                && y > (h / 2 - 200) && y < (h / 2 + 200))
                return SetPositionGorizontal();

            return new Vector2(x, y);
        }

        private static void RandomWallGenerate()
        {
            var numTexture = _random.Next(0, 2);

            if (numTexture == 0)
                Walls.Add(new(_textureGorizontal, SetPositionGorizontal()));
            else
                Walls.Add(new(_textureVertical, SetPositionVertical()));
        }

        public static void AddWall()
        {
            RandomWallGenerate();
        }

        public static void GenerateWalls()
        {
            for (int i = 0; i < 4; i++)
            {
                AddWall();

                if (GameState.gameManager.player.CheckRectangleCollision(Walls[Walls.Count - 1]))
                    Walls.RemoveAt(Walls.Count - 1);
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
