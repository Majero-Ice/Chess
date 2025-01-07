using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.models.Pieces
{
    public class Queen : Piece
    {
        public Queen(int x, int y, Color color, Board board)
            : base(x, y, color, board) {
            Name = PieceNames.Queen;
        }

        public override bool CanMove(Cell target)
        {
            if (!base.CanMove(target))
            {
                return false;
            }

            var currentCell = this.Board.GetCell(this.X, this.Y);

            return currentCell.IsEmptyVertical(target) ||
                   currentCell.IsEmptyHorizontal(target) ||
                   currentCell.IsEmptyDiagonal(target);
        }

    }
}
