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
        public MainWindow()
        {
            Board board = new Board();
            InitializeComponent();
            GenerateChessBoard(board.Cells);
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
                        var image = new Image
                        {
                            Width = 35,
                            Height = 35,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Source = new BitmapImage(new Uri($"pack://application:,,,/assets/{cell.Piece.Color}_{cell.Piece.GetType().Name}.png"))
                        };

                        grid.Children.Add(image);
                    }

                    grid.MouseLeftButtonDown += (s, e) =>
                    {
                        rectangle.Fill = Brushes.Teal;
                        cell.click();
                    };

                    Grid.SetRow(grid, y);
                    Grid.SetColumn(grid, x);
                    ChessBoardGrid.Children.Add(grid);
                }
            }
        }
    }
}