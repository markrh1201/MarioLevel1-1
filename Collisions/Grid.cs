using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Sprint4BeanTeam
{
    public class Grid
    {
        private int cellSize;
        private int gridWidth;
        private int gridHeight;
        private Cell[,] grid;

        public Grid(int cellSize, int gridWidth, int gridHeight)
        {
            this.cellSize = cellSize;
            this.gridWidth = gridWidth;
            this.gridHeight = gridHeight;
            grid = new Cell[gridWidth, gridHeight];

            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    grid[x, y] = new Cell(x, y, cellSize);
                }
            }
        }

        public void Update()
        {
            for (int x = 0; x < gridWidth; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    Cell cell = grid[x, y];
                    for (int i = 0; i < cell.objects.Count; i++)
                    {
                        IGameObject obj = cell.objects[i];
                        Rectangle box = obj.getCollisionBox();
                        int new_x1 = box.Left / cellSize;
                        int new_y1 = box.Top / cellSize;

                        if (new_x1 != x || new_y1 != y)
                        {
                            RemoveObject(obj);
                            AddObject(obj);
                        }
                    }
                }
            }
        }

        public Cell getCell(int x, int y)
        {
            x = x / cellSize;
            y = y / cellSize;
            return grid[x, y];
        }


        public void AddObject(IGameObject obj)
        {
            Rectangle box = obj.getCollisionBox();
            int x1 = box.Left / cellSize;
            int y1 = box.Top / cellSize;
            int x2 = box.Right / cellSize;
            int y2 = box.Bottom / cellSize;

            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
                    {
                        if (!grid[x, y].objects.Contains(obj))
                        {
                            grid[x, y].objects.Add(obj);
                        }
                        obj.updateCell(grid[x, y]);
                    }
                }
            }
        }

        public void RemoveObject(IGameObject obj)
        {
            Rectangle box = obj.getCollisionBox();
            int x1 = box.Left / cellSize;
            int y1 = box.Top / cellSize;
            int x2 = box.Right / cellSize;
            int y2 = box.Bottom / cellSize;

            for (int x = x1; x <= x2; x++)
            {
                for (int y = y1; y <= y2; y++)
                {
                    if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight)
                    {
                        grid[x, y].objects.Remove(obj);
                    }
                }
            }
        }

    }

}