using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.models.Pieces
{
    public class Rook : Piece
    {
        public Rook(int x, int y, Color color, Board board)
            : base(x, y, color, board) {
            Name = PieceNames.Rook;
        }
    }
}
