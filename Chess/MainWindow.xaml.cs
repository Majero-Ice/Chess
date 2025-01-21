using Chess.models;
using Chess.models.Pieces;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess
{
    // Hauptklasse für das Schachspiel
    public partial class MainWindow : Window
    {
        Cell? SelectedCell = null;
        Rectangle? SelectedCellRectangle = null;
        Board Board = new Board();
        bool IsCheck = false;
        private PlayerPanel WhitePlayer = new PlayerPanel(models.Color.White);
        private PlayerPanel BlackPlayer = new PlayerPanel(models.Color.Black);
        public MainWindow()
        {
            InitializeComponent();
            GenerateChessBoard(Board.Cells);
        }

        // Überprüft, ob der König im Schach steht und ob ein Spieler gewonnen hat
        private void IsVictory()
        {
            Cell CurrentKingCell = Board.GetKing(Board.CurrentPlayer);
             if (Board.IsKingUnderAttack(CurrentKingCell)){
                IsCheck = true;
                CurrentKingCell.CellUI.Fill = Brushes.Red;
            }else
            {
                IsCheck = false;
                CurrentKingCell.CellUI.Fill = CurrentKingCell?.Color == models.Color.White ? Brushes.Beige : Brushes.SaddleBrown;
            }

            if (!Board.IsKingPossibleMoves(CurrentKingCell?.Piece) && !Board.IsKingProtection(Board.CurrentPlayer) && IsCheck)
            {
                MessageBox.Show($"{Board.CurrentPlayer.ToString()} Player won!");
            }
        }

        // Fügt die geschlagene Figur der Liste der verlorenen Figuren hinzu
        private void AddLostPiece(Cell cell)
        {
            if (cell.Piece == null)
            {
                return;
            }
            if (cell.Piece.Color != models.Color.White)
            {
                WhitePlayer.AddLostPiece(cell.Piece);
            }
            else
            {
                BlackPlayer.AddLostPiece(cell.Piece);
            }
        }
        // Aktualisiert die Anzahl der verlorenen Figuren in der Anzeige
        private void LostPiecesCount(Piece? piece)
        {
            if (piece == null)
            {
                return;
            }
            switch (piece.Name)
            {
                case PieceNames.Pawn:

                    if (Board.CurrentPlayer == models.Color.White)
                    {
                        BlackPawnCount.Text = "x" + WhitePlayer.count(PieceNames.Pawn).ToString();
                    }
                    else
                    {
                        WhitePawnCount.Text = "x" + BlackPlayer.count(PieceNames.Pawn).ToString();
                    }
                    break;
                case PieceNames.Rook:
                    if (Board.CurrentPlayer == models.Color.White)
                    {
                        BlackRookCount.Text = "x" + WhitePlayer.count(PieceNames.Rook).ToString();
                    }
                    else
                    {
                        WhiteRookCount.Text = "x" + BlackPlayer.count(PieceNames.Rook).ToString();
                    }
                    break;
                case PieceNames.Queen:
                    if (Board.CurrentPlayer == models.Color.White)
                    {
                        BlackQueenCount.Text = "x" + WhitePlayer.count(PieceNames.Queen).ToString();
                    }
                    else
                    {
                        WhiteQueenCount.Text = "x" + BlackPlayer.count(PieceNames.Queen).ToString();
                    }
                    break;
                case PieceNames.Bishop:
                    if (Board.CurrentPlayer == models.Color.White)
                    {
                        BlackBishopCount.Text = "x" + WhitePlayer.count(PieceNames.Bishop).ToString();
                    }
                    else
                    {
                        WhiteBishopCount.Text = "x" + BlackPlayer.count(PieceNames.Bishop).ToString();
                    }
                    break;
                case PieceNames.Knight:
                    if (Board.CurrentPlayer == models.Color.White)
                    {
                        BlackKnightCount.Text = "x" + WhitePlayer.count(PieceNames.Knight).ToString();
                    }
                    else
                    {
                        WhiteKnightCount.Text = "x" + BlackPlayer.count(PieceNames.Knight).ToString();
                    }
                    break;
            }
        }
        // Wird aufgerufen, wenn der Spieler auf eine Zelle klickt
        private void Click(Cell cell) 
        {

            if (SelectedCellRectangle != null)
            {
                SelectedCellRectangle.Fill = SelectedCell?.Color == models.Color.White ? Brushes.Beige : Brushes.SaddleBrown;

            }
            if (cell.Available && SelectedCell != null)
            {
                Board.ClearAvailableMoves();
                AddLostPiece(cell);
                LostPiecesCount(cell.Piece);
                SelectedCell.MovePiece(cell);
                SelectedCell = null;
                SelectedCellRectangle = null;
                IsVictory();
                Board.TogglePlayer();
                IsVictory();
            }
            else if (Board.CurrentPlayer == cell.Piece?.Color)
            {
                Board.ClearAvailableMoves();
                cell.CellUI.Fill = Brushes.Green;
                SelectedCell = cell;
                SelectedCellRectangle = cell.CellUI;
                Board.GetAvailableMoves(cell);
            }
            else
            {
                Board.ClearAvailableMoves();
                SelectedCell = null;
                SelectedCellRectangle = null;
            }


        }
        // Generiert das Schachbrett und fügt es zur Benutzeroberfläche hinzu
        private void GenerateChessBoard(List<List<Cell>> cells)
        {
            ChessBoardGrid.RowDefinitions.Clear();
            ChessBoardGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < cells.Count; i++)
            {
                ChessBoardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
                ChessBoardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
            }

            for (int y = 0; y < cells.Count; y++)
            {
                for (int x = 0; x < cells[y].Count; x++)
                {
                    Cell cell = cells[y][x];

                    if (cell.Piece != null)
                    {

                        cell.Grid.Children.Add(cell.Piece.Image);
                    }

                    cell.Grid.MouseLeftButtonDown += (s, e) => Click(cell);

                    Grid.SetRow(cell.Grid, y);
                    Grid.SetColumn(cell.Grid, x);
                    ChessBoardGrid.Children.Add(cell.Grid);
                }
            }
        }
    }
}