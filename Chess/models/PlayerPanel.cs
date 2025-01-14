using Chess.models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Chess.models
{
    public class PlayerPanel
    {
        Color Color { get; }

        List<Piece> LostPieces = new List<Piece>();
        public PlayerPanel(Color Color)
        {
            this.Color = Color;
        }

        public void AddLostPiece(Piece piece)
        {
            LostPieces.Add(piece);
        }

        public int count(PieceNames pieceName)
        {
            var pieces = LostPieces.Where(p => p.Name == pieceName);
            return pieces.Count();
        }
    }
      
}

