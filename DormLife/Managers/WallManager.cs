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

        private static Vector2 _lastPositionGor = Vector2.Zero;
        private static Vector2 _lastPositionVer = Vector2.Zero;

        private static float w = Globals.Bounds.X;
        private static float h = Globals.Bounds.Y;

        public static List<Wall> Walls { get; private set;  }

        public static void Init()
        {
            _textureGorizontal = Globals.Content.Load<Texture2D>("walls/gorizontal_wall");
            _textureVertical = Globals.Content.Load<Texture2D>("walls/vertical_wall");
            _random = new();

            Walls = new();
        }

        private static Vector2 SetPositionGorizontal()
        {
            float y = 0;
            float x = 0;

            if (_lastPositionGor == Vector2.Zero)
            {
                x = _random.Next(100, (int)w - 100);
                y = _random.Next(100, (int)h - 100);
                if (x > (w / 2 - 200) && x < (w / 2 + 200)
                 && y > (h / 2 - 200) && y < (h / 2 + 200))
                    return SetPositionGorizontal();
                _lastPositionGor = new Vector2(x, y);
            } 
            else
            {
                x = _lastPositionGor.X + 80;
                y = _lastPositionGor.Y;
                _lastPositionGor = Vector2.Zero;
            }

            Vector2 pos = new Vector2(x, y);
            return pos;
        }

        private static Vector2 SetPositionVertical()
        {
            float y = 0;
            float x = 0;

            if (_lastPositionVer == Vector2.Zero)
            {
                x = _random.Next(100, (int)w - 100);
                y = _random.Next(100, (int)h - 100);
                if (x > (w / 2 - 200) && x < (w / 2 + 200)
                 && y > (h / 2 - 200) && y < (h / 2 + 200))
                    return SetPositionGorizontal();
                _lastPositionVer = new Vector2(x, y);
            }
            else
            {
                x = _lastPositionVer.X;
                y = _lastPositionVer.Y + 80;
                _lastPositionVer = Vector2.Zero;
            }

            Vector2 pos = new Vector2(x, y);
            return pos;
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
            for (int i = 0; i < 12; i++)
            {
                AddWall();

                if (GameState.player.CheckCollision(Walls[Walls.Count - 1]))
                {
                    Walls.RemoveAt(Walls.Count - 1);
                }
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
