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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cell? SelectedCell = null;
        Rectangle? SelectedCellRectangle = null;
        Board Board = new Board();
        public MainWindow()
        {
            InitializeComponent();
            GenerateChessBoard(Board.Cells);
        }

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

                    cell.Grid.MouseLeftButtonDown += (s, e) =>
                    {
                        Console.WriteLine(SelectedCell?.X.ToString() + SelectedCell?.Y);
                        if (SelectedCellRectangle != null)
                        {
                            SelectedCellRectangle.Fill = SelectedCell?.Color == models.Color.White ? Brushes.White : Brushes.Black;
                            
                        }
                        if (cell.Available && SelectedCell != null)
                        {
                            Board.ClearAvailableMoves();
                            SelectedCell.MovePiece(cell);
                            SelectedCell = null;
                            SelectedCellRectangle = null;
                            Board.TogglePlayer();
                            
                            
                        }else if(Board.CurrentPlayer == cell.Piece?.Color)
                        {
                            Board.ClearAvailableMoves();
                            cell.CellUI.Fill = Brushes.Teal;
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
                        
                    };

                    Grid.SetRow(cell.Grid, y);
                    Grid.SetColumn(cell.Grid, x);
                    ChessBoardGrid.Children.Add(cell.Grid);
                }
            }
        }
    }
}