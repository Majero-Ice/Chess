using Chess.models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Chess.models
{
    class PlayerPanel
    {
        Color Color { get;}
        Grid Grid { get; }
        List<Piece> LostPieces = new List<Piece>();
        public PlayerPanel(Color Color) {
            this.Color = Color;
            Grid = new Grid();
        }

        public void AddLostPiece(Piece piece)
        {
            LostPieces.Add(piece);
            //Grid.Children.Add(piece.Image);
        }
    }
}
