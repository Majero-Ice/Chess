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

        public bool IsEmpty
        {
            get { return this.Piece == null; }
        }

        public bool IsEnemy(Cell target)
        {
            if (target.Piece != null)
            {
                return this.Piece?.Color != target.Piece.Color;
            }
            return false;
        }

        public bool IsEmptyVertical(Cell target)
        {
            if (this.X != target.X)
            {
                return false;
            }

            int max = Math.Max(this.Y, target.Y);
            int min = Math.Min(this.Y, target.Y);

            for (int y = min + 1; y < max; y++)
            {
                if (!this.Board.GetCell(this.X, y).IsEmpty)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsEmptyHorizontal(Cell target)
        {
            if (this.Y != target.Y)
            {
                return false;
            }

            int max = Math.Max(this.X, target.X);
            int min = Math.Min(this.X, target.X);

            for (int x = min + 1; x < max; x++)
            {
                if (!this.Board.GetCell(x, this.Y).IsEmpty)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsEmptyDiagonal(Cell target)
        {
            int absX = Math.Abs(this.X - target.X);
            int absY = Math.Abs(this.Y - target.Y);

            if (absX != absY)
            {
                return false;
            }

            int dy = this.Y < target.Y ? 1 : -1;
            int dx = this.X < target.X ? 1 : -1;

            for (int i = 1; i < absY; i++)
            {
                if (!this.Board.GetCell(this.X + dx * i, this.Y + dy * i).IsEmpty)
                {
                    return false;
                }
            }

            return true;
        }



    }

}
