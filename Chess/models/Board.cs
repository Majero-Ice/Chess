using Chess.models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess.models
{
    public class Board
    {
        //public Cell[][] Cells { get; private set; }
        public List<List<Cell>> Cells { get; private set; } = new List<List<Cell>>();
        public Color CurrentPlayer { get; private set; } = Color.White;

        public Board()
        {
            CreateCells();
            SetFigures();
        }

        private void CreateCells()
        {
            for (int y = 0; y < 8; y++)
            {
                var row = new List<Cell>();
                for (int x = 0; x < 8; x++)
                {
                    var color = (x + y) % 2 == 0 ? Color.White : Color.Black;
                    var rectangle = new Rectangle
                    {
                        Width = 50,
                        Height = 50,
                        Fill = color == models.Color.White ? Brushes.White : Brushes.Black
                    };
                    Cell cell = new Cell(x, y, color, this);
                    cell.Grid.Children.Add(rectangle);
                    cell.CellUI = rectangle;
                    row.Add(cell);
                    

                }
                Cells.Add(row);
            }
        }

        private void SetFigures()
        {
            List<Piece> figures = AddPieces(this);
            foreach (Piece figure in figures)
            {
              Cells[figure.Y][figure.X].Piece = figure;
            }
        }
        public Cell GetCell(int x, int y)
        {
            return Cells[y][x];
        }

        public void TogglePlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        public bool IsKingPossibleMoves(Piece? king)
        {
            if (king == null)
            {
                return false;
            }

            foreach (var row in Cells)
            {
                foreach (var cell in row)
                {
                    if (king.CanMove(cell))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsKingProtection(Color color)
        {
            foreach (var row in Cells)
            {
                foreach (var cell in row)
                {
                    if (cell.Piece?.Color == color && GetAvailableMoves(cell))
                    {
                        ClearAvailableMoves();
                        return true;
                    }
                }
            }
            return false;
             
        }



        public bool IsKingUnderAttack(Cell? king)
        {
            if (king?.Piece == null || king.Piece.Name != PieceNames.King)
            {
                return false;
            }

            foreach (var row in Cells)
            {
                foreach (var target in row)
                {
                    if (target.Piece?.CanMove(king) == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public bool GetAvailableMoves(Cell? selectedCell)
        {
            bool result = false;

            for (int y = 0; y < Cells.Count; y++)
            {
                var row = Cells[y];
                for (int x = 0; x < row.Count; x++)
                {
                    var target = row[x];

                    if (selectedCell?.Piece?.CanMove(target) == true)
                    {
                        var selectedCellPiece = selectedCell.Piece;
                        var targetPiece = target.Piece;

                        selectedCell.Piece = null;
                        target.Piece = selectedCellPiece;

                        if (IsKingUnderAttack(GetKing(selectedCellPiece?.Color)))
                        {
                            target.Available = false;
                        }
                        else
                        {
                            target.Available = true;
                            result = true;
                            selectedCell.Piece = selectedCellPiece;
                            target.Piece = targetPiece;
                            if (target.Piece != null)
                            {
                                ((Rectangle)target.Grid.Children[0]).Fill = Brushes.Green;
                            }
                            else
                            {
                                Ellipse highlight = new Ellipse
                                {
                                    Width = 12.5,
                                    Height = 12.5,
                                    Fill = Brushes.Green
                                };
                                target.Grid.Children.Add(highlight);
                            }
                        }
                        selectedCell.Piece = selectedCellPiece;
                        target.Piece = targetPiece;


                    }

                }
            }

            return result;
        }



        public void ClearAvailableMoves()
        {
            for (int y = 0; y < Cells.Count; y++)
            {
                var row = Cells[y];
                for (int x = 0; x < row.Count; x++)
                {
                    var target = row[x];

                    if (target.Available)
                    {
                        target.Available = false;

                        if (target.Piece == null)
                        {
                            target.Grid.Children.RemoveAt(1);
                        }
                        else
                        {
                            ((Rectangle)target.Grid.Children[0]).Fill = (target.Color == models.Color.White) ? Brushes.White : Brushes.Black;
                        }
                    }

                }
            }

        }

        public Cell? GetKing(Color? color)
        {
            foreach (var row in Cells)
            {
                foreach (var target in row)
                {
                    if (target.Piece?.Color == color && target.Piece?.Name == PieceNames.King)
                    {
                        return target;
                    }
                }
            }

            return null;
        }



        public List<Piece> AddPieces(Board board)
        {
            var figures = new List<Piece>();
            figures.AddRange(AddQueen(board));
            figures.AddRange(AddBishop(board));
            figures.AddRange(AddKing(board));
            figures.AddRange(AddKnight(board));
            figures.AddRange(AddRook(board));
            figures.AddRange(AddPawn(board));
            return figures;
        }

        private List<Piece> AddPawn(Board board)
        {
            var pawns = new List<Piece>();
            for (int i = 0; i < 8; i++)
            {
                pawns.Add(new Pawn(i, 6, Color.White, board));
                pawns.Add(new Pawn(i, 1, Color.Black, board));
            }
            return pawns;
        }

        private List<Piece> AddQueen(Board board)
        {
            return new List<Piece>
        {
            new Queen(3, 7, Color.White, board),
            new Queen(3, 0, Color.Black, board)
        };
        }

        private List<Piece> AddKing(Board board)
        {
            return new List<Piece>
        {
            new King(4, 7, Color.White, board),
            new King(4, 0, Color.Black, board)
        };
        }

        private List<Piece> AddBishop(Board board)
        {
            return new List<Piece>
        {
            new Bishop(2, 7, Color.White, board),
            new Bishop(5, 7, Color.White, board),
            new Bishop(2, 0, Color.Black, board),
            new Bishop(5, 0, Color.Black, board)
        };
        }

        private List<Piece> AddRook(Board board)
        {
            return new List<Piece>
        {
            new Rook(0, 7, Color.White, board),
            new Rook(7, 7, Color.White, board),
            new Rook(7, 0, Color.Black, board),
            new Rook(0, 0, Color.Black, board)
        };
        }

        private List<Piece> AddKnight(Board board)
        {
            return new List<Piece>
        {
            new Knight(1, 7, Color.White, board),
            new Knight(6, 7, Color.White, board),
            new Knight(1, 0, Color.Black, board),
            new Knight(6, 0, Color.Black, board)
        };
        }
    }
}
