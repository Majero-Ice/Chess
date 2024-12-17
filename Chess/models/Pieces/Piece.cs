using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.models.Pieces
{
    public class Piece
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Img { get; set; }
        public Color Color { get; }
        public PieceNames Name { get; set; }
        public Board Board { get; }

        public Piece(int x, int y, Color color, Board board)
        {
            X = x;
            Y = y;
            Color = color;
            Name = PieceNames.Piece; 
            Img = "path_to_black_pawn_image"; 
            Board = board;
        }

        public bool CanMove() {
            return true;
        }
    }

}
