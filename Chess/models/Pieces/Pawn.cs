using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.models.Pieces
{
    public class Pawn : Piece
    {
        private bool isFirstStep = true;
        public Pawn(int x, int y, Color color, Board board)
            : base(x, y, color, board) {
            Name = PieceNames.Pawn;
        }

        public override bool CanMove(Cell target)
        {
            if (!base.CanMove(target))
            {
                return false;
            }

            int direction = Color == Color.Black ? 1 : -1; // Richtung basierend auf der Farbe

            int firstStep = Color == Color.Black ? 2 : -2; // Zwei Felder für den ersten Zug

            bool step = (target.Y == Y + direction) ||
                        (isFirstStep && target.Y == Y + firstStep);

            bool rightAttack = target.X == X + direction && target.Y == Y + direction;
            bool leftAttack = target.X == X - direction && target.Y == Y + direction;

            if (step && target.X == X &&
                Board.GetCell(target.X, target.Y).IsEmpty && // Ziel-Feld muss leer sein
                Board.GetCell(X, Y + direction).IsEmpty) // Feld vor dem Ziel muss leer sein
            {
                return true;
            }

            if ((rightAttack || leftAttack) && Board.GetCell(X, Y).IsEnemy(target))
            {
                return true; // Angriff auf eine gegnerische Figur
            }

            return false;
        }

        public override void Move(Cell target)
        {
            base.Move(target);
            isFirstStep = false;
        }
    }
}
