using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace Chess.models.Pieces
{
    public class Piece
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Image Image { get; set; }
        public Color Color { get; }
        public PieceNames Name { get; set; }
        public Board Board { get; }

        public Piece(int x, int y, Color color, Board board)
        {
            X = x;
            Y = y;
            Color = color;
            Name = PieceNames.Piece; 
            Image = new Image
            {
                Width = 35,
                Height = 35,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Source = new BitmapImage(new Uri($"pack://application:,,,/assets/{Color}_{this.GetType().Name}.png"))
            };
            Board = board;
        }

        public virtual bool CanMove(Cell target) {
            if (target.Piece?.Color == Color) { return false; }
            return true;
        }

        public virtual void Move(Cell target)
        {
            X = target.X;
            Y = target.Y;
        }
    }

}
