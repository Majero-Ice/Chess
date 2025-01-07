using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.models.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(int x, int y, Color color, Board board)
            : base(x, y, color, board) {
            Name = PieceNames.Pawn;
        }
    }
}
