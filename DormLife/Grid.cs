using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DormLife
{
    public class Grid
    {
        private readonly GridCell[,] cells;

        public int Width { get; }
        public int Height { get; }
        public int CellSize { get; }

        public Grid(int width, int height, int cellSize)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;

            cells = new GridCell[width, height];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    cells[x, y] = new GridCell(x, y, false);
                }
            }
        }

        public void SetObstacle(int x, int y, bool isObstacle)
        {
            cells[x, y].IsObstacle = isObstacle;
        }

        public bool IsCellValid(int x, int y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height && !cells[x, y].IsObstacle;
        }

        public List<GridCell> GetNeighborCells(GridCell cell)
        {
            List<GridCell> neighbors = new List<GridCell>();

            int[] dx = { -1, 0, 1, 0 };
            int[] dy = { 0, 1, 0, -1 };

            for (int i = 0; i < dx.Length; i++)
            {
                int neighborX = cell.X + dx[i];
                int neighborY = cell.Y + dy[i];

                if (IsCellValid(neighborX, neighborY))
                {
                    neighbors.Add(cells[neighborX, neighborY]);
                }
            }

            return neighbors;
        }

        public List<Vector2> FindPath(Vector2 start, Vector2 goal)
        {
            int startX = (int)(start.X / CellSize);
            int startY = (int)(start.Y / CellSize);
            int goalX = (int)(goal.X / CellSize);
            int goalY = (int)(goal.Y / CellSize);

            if (!IsCellValid(startX, startY) || !IsCellValid(goalX, goalY))
            {
                return null;
            }

            GridCell startCell = cells[startX, startY];
            GridCell goalCell = cells[goalX, goalY];

            foreach (GridCell cell in cells)
            {
                cell.DistanceFromStart = int.MaxValue;
                cell.HeuristicDistanceToGoal = Math.Abs(cell.X - goalX) + Math.Abs(cell.Y - goalY);
                cell.TotalDistance = int.MaxValue;
                cell.Parent = null;
                cell.IsVisited = false;
            }

            startCell.DistanceFromStart = 0;
            startCell.TotalDistance = startCell.HeuristicDistanceToGoal;

            List<GridCell> openSet = new List<GridCell>();
            openSet.Add(startCell);

            while (openSet.Count > 0)
            {
                GridCell currentCell = FindCellWithLowestTotalDistance(openSet);

                if (currentCell == goalCell)
                {
                    return ReconstructPath(startCell, goalCell);
                }

                openSet.Remove(currentCell);
                currentCell.IsVisited = true;

                foreach (GridCell neighborCell in GetNeighborCells(currentCell))
                {
                    if (neighborCell.IsVisited)
                    {
                        continue;
                    }

                    int tentativeDistance = currentCell.DistanceFromStart + 1;

                    if (tentativeDistance < neighborCell.DistanceFromStart)
                    {
                        neighborCell.Parent = currentCell;
                        neighborCell.DistanceFromStart = tentativeDistance;
                        neighborCell.TotalDistance = neighborCell.DistanceFromStart + neighborCell.HeuristicDistanceToGoal;

                        if (!openSet.Contains(neighborCell))
                        {
                            openSet.Add(neighborCell);
                        }
                    }
                }
            }

            return null;
        }

        private GridCell FindCellWithLowestTotalDistance(List<GridCell> cells)
        {
            GridCell lowestCell = cells[0];
            int lowestDistance = int.MaxValue;

            foreach (GridCell cell in cells)
            {
                if (cell.TotalDistance < lowestDistance)
                {
                    lowestCell = cell;
                    lowestDistance = cell.TotalDistance;
                }
            }

            return lowestCell;
        }

        private List<Vector2> ReconstructPath(GridCell startCell, GridCell goalCell)
        {
            List<Vector2> path = new List<Vector2>();
            GridCell currentCell = goalCell;

            while (currentCell != startCell)
            {
                path.Add(new Vector2(currentCell.X * CellSize + CellSize / 2, currentCell.Y * CellSize + CellSize / 2));
                currentCell = currentCell.Parent;
            }

            path.Reverse();
            return path;
        }
    }

    public class GridCell
    {
        public int X { get; }
        public int Y { get; }
        public bool IsObstacle { get; set; }
        public int DistanceFromStart { get; set; }
        public int HeuristicDistanceToGoal { get; set; }
        public int TotalDistance { get; set; }
        public GridCell Parent { get; set; }
        public bool IsVisited { get; set; }

        public GridCell(int x, int y, bool isObstacle)
        {
            X = x;
            Y = y;
            IsObstacle = isObstacle;
        }
    }
}
