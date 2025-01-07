using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Chess.models.Pieces
{
    public class King : Piece
    {
        public King(int x, int y, Color color, Board board) : base(x, y, color, board)
        {
            Name = PieceNames.King;
        }

        public override bool CanMove(Cell target)
        {
            if (!base.CanMove(target))
            {
                return false;
            }

            int dx = Math.Abs(target.X - this.X);
            int dy = Math.Abs(target.Y - this.Y);

            bool stepX = dx == 1 && target.Y == this.Y;
            bool stepY = dy == 1 && target.X == this.X;

            bool diagonalMove = dx == 1 && dy == 1;

            if ((stepX || stepY) && !WillBeUnderAttack(target))
            {
                return true;
            }

            return diagonalMove && !WillBeUnderAttack(target);
        }

        public bool WillBeUnderAttack(Cell target)
        {
            var prevFigure = target.Piece;

            target.Piece = new Piece(target.X, target.Y, Color, Board);

            for (int y = 0; y < this.Board.Cells.Count; y++)
            {
                var row = this.Board.Cells[y];
                for (int x = 0; x < row.Count; x++)
                {
                    var cell = row[x];
                    if (cell.IsEnemy(target) && cell.Piece?.CanMove(target) == true)
                    {
                        target.Piece = prevFigure;
                        return true; 
                    }
                }
            }

            target.Piece = prevFigure;
            return false; 
        }
    }

}
