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
        // Fügt eine verlorene Figur zur Liste hinzu
        public void AddLostPiece(Piece piece)
        {
            LostPieces.Add(piece);
        }
        // Zählt die verlorenen Figuren eines bestimmten Typs
        public int count(PieceNames pieceName)
        {
            var pieces = LostPieces.Where(p => p.Name == pieceName);// Filtert Figuren nach Typ
            return pieces.Count();
        }
    }
      
}

