using Chess.models.Pieces;
using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Chess.models
{
    public class Cell
    {
        public int X { get; }
        public int Y { get; }
        public Board Board { get; }
        public Grid Grid { get; }
        public Rectangle CellUI { get; set; }
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
            Grid = new Grid();
        }

        public void SetFigure(Piece piece)
        {
            Piece = piece;
        }

        public void MoveFigure(Cell target)
        {
            if (Piece != null)
            {
                target.Piece = Piece;
                Piece.Move(target);
                Piece = null;
            }
        }

        public bool click()
        {
            if (Available)
            {
                Board.TogglePlayer();
                return true;
            }else if (Piece?.Color == Board.CurrentPlayer) 
            {
                Selected = true;
                return true;
            }
            return false;
        }


    }

}
