using System.Windows;
using System.Windows.Controls;

namespace gra1
{
    public partial class MainWindow : Window
    {
        private string currentPlayer = "X";
        private string[,] board = new string[3, 3];

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    board[row, col] = "";
                }
            }
        }

        private void OnCellClicked(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null || !string.IsNullOrEmpty(button.Content?.ToString())) return;

            int row = Grid.GetRow(button);
            int col = Grid.GetColumn(button);
            board[row, col] = currentPlayer;
            button.Content = currentPlayer;

            if (CheckWin())
            {
                StatusLabel.Content = $"Gracz {currentPlayer} wygrywa!";
                DisableBoard();
                return;
            }

            if (CheckDraw())
            {
                StatusLabel.Content = "Remis!";
                return;
            }

            currentPlayer = (currentPlayer == "X") ? "O" : "X";
            StatusLabel.Content = $"Tura: {currentPlayer}";
        }

        private bool CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(board[i, 0]) && board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2])
                    return true;
                if (!string.IsNullOrEmpty(board[0, i]) && board[0, i] == board[1, i] && board[1, i] == board[2, i])
                    return true;
            }
            if (!string.IsNullOrEmpty(board[0, 0]) && board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
                return true;
            if (!string.IsNullOrEmpty(board[0, 2]) && board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0])
                return true;
            return false;
        }

        private bool CheckDraw()
        {
            foreach (var cell in board)
            {
                if (string.IsNullOrEmpty(cell)) return false;
            }
            return true;
        }

        private void DisableBoard()
        {
            foreach (var child in GameGrid.Children)
            {
                if (child is Button button)
                {
                    button.IsEnabled = false;
                }
            }
        }
    }
}