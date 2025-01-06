using Chess.models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Chess.models
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public Board Board { get; }
        public Color Color { get; }
        public Piece? Piece { get; set; }
        public bool Selected { get; set; }
        public bool Available { get; set; }

        public Cell(int x, int y, Color color, Board board)
        {
            X = x;
            Y = y;
            Color = color;
            Board = board;
            Piece = null;
            Selected = false;
            Available = false;
        }

        public void click()
        {
            Selected = true;
            Console.WriteLine(1);
        }
    }

}
