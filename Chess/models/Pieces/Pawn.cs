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

            int direction = Color == Color.Black ? 1 : -1;

            int firstStep = Color == Color.Black ? 2 : -2;

            bool step = (target.Y == Y + direction) ||
                        (isFirstStep && target.Y == Y + firstStep);

            bool rightAttack = target.X == X + direction && target.Y == Y + direction;
            bool leftAttack = target.X == X - direction && target.Y == Y + direction;

            if (step && target.X == X &&
                Board.GetCell(target.X, target.Y).IsEmpty &&
                Board.GetCell(X, Y + direction).IsEmpty)
            {
                return true;
            }

            if ((rightAttack || leftAttack) && Board.GetCell(X, Y).IsEnemy(target))
            {
                return true;
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
