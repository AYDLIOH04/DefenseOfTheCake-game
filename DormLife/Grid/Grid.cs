using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormLife.Grid
{
    public class Grid
    {
        private int width;
        private int height;
        private Cell[,] cells;

        public Grid(int width, int height)
        {
            this.width = width;
            this.height = height;

            // Инициализируем все клетки сетки
            cells = new Cell[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells[x, y] = new Cell(x, y);
                }
            }
        }

        public int Width => width;
        public int Height => height;

        public Cell GetCell(int x, int y)
        {
            // Проверяем валидность координат
            if (x < 0 || x >= width || y < 0 || y >= height)
                return null;

            return cells[x, y];
        }

        public void SetCellBlocked(int x, int y)
        {
            Cell cell = GetCell(x, y);
            if (cell != null)
            {
                cell.IsWalkable = false;
            }
        }
    }
}
