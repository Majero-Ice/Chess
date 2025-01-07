using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.models.Pieces
{
    public class Knight : Piece
    {
        public Knight(int x, int y, Color color, Board board)
            : base(x, y, color, board) {
            Name = PieceNames.Knight;
        }

        public override bool CanMove(Cell target)
        {
            if (!base.CanMove(target))
            {
                return false;
            }

            int dx = Math.Abs(this.X - target.X);
            int dy = Math.Abs(this.Y - target.Y);

            return (dx == 2 && dy == 1) || (dx == 1 && dy == 2);
        }
    }
}
