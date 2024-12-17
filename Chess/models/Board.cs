using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.models
{
    public class Board
    {
        public Cell[][] Cells { get; private set; }
        public Color CurrentPlayer { get; private set; } = Color.White;

        public Board()
        {
            CreateCells();
            SetFigures();
        }

        private void CreateCells()
        {
        }

        private void SetFigures()
        {
        }
    }
}
