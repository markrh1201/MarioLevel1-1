using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Sprint4BeanTeam
{
    public class Cell
    {
        private Rectangle cellBounds;
        public List<IGameObject> objects;
        public Cell(int x, int y, int cellSize)
        {
            objects = new List<IGameObject>();
            cellBounds = new Rectangle(x, y, cellSize, cellSize);
        }
    }
}

