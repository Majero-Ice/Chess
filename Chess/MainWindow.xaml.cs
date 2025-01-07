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
                    var rectangle = new Rectangle
                    {
                        Width = 50,
                        Height = 50,
                        Fill = cell.Color == models.Color.White ? Brushes.White : Brushes.Black
                    };

                    var grid = new Grid();
                    grid.Children.Add(rectangle);

                    if (cell.Piece != null)
                    {
                       
                        grid.Children.Add(cell.Piece.Image);
                    }

                    grid.MouseLeftButtonDown += (s, e) =>
                    {
                        if (SelectedCellRectangle != null) 
                        {
                            SelectedCellRectangle.Fill = SelectedCell?.Color == models.Color.White ? Brushes.White : Brushes.Black;
                            SelectedCell = null;
                        }
                        if (cell.click())
                        {
                            rectangle.Fill = Brushes.Teal;
                            SelectedCell = cell;
                            SelectedCellRectangle = rectangle;

                            GetAvailableMoves(cell, grid);
                        }
                    };

                    Grid.SetRow(grid, y);
                    Grid.SetColumn(grid, x);
                    ChessBoardGrid.Children.Add(grid);
                }
            }
        }

        public bool GetAvailableMoves(Cell? selectedCell, Grid grid)
        {
            bool result = false;

            for (int y = 0; y < Board.Cells.Count; y++)
            {
                var row = Board.Cells[y];
                for (int x = 0; x < row.Count; x++)
                {
                    var target = row[x];

                    if (selectedCell?.Piece?.CanMove(target) == true)
                    {
                        var selectedCellFigure = selectedCell.Piece;
                        var targetFigure = target.Piece;

                        selectedCell.Piece = null;
                        target.Piece = selectedCellFigure;

                        if (Board.IsKingUnderAttack(Board.GetKing(selectedCellFigure.Color)))
                        {
                            target.Available = false;
                        }
                        else
                        {
                            target.Available = true;
                            result = true;

                            Ellipse highlight = new Ellipse
                            {
                                Width = 25,
                                Height = 25,
                                Fill = Brushes.Green
                            };




                        }

                        selectedCell.Piece = selectedCellFigure;
                        target.Piece = targetFigure;
                    }
                    else
                    {
                        target.Available = false;
                    }
                }
            }

            return result;
        }
    }
}