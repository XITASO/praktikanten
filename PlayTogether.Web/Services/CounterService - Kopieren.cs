using System;

namespace PlayTogether.Web.Services
{
    public class GameService
    {
        public string[,] Board { get; private set; } = new string[3, 3];

        public string CurrentPlayer { get; private set; } = "X";
        public bool GameOver { get; private set; } = false;
        public string WinnerMessage { get; private set; } = string.Empty;

        public int ScoreX { get; private set; } = 0;
        public int ScoreO { get; private set; } = 0;

        public string PlayerXName { get; set; } = "Player X";
        public string PlayerOName { get; set; } = "Player O";

        public string SpectatorName { get; private set; } = string.Empty;

        public event EventHandler? StateChanged;

        public void MakeMove(int row, int col)
        {

            Board[row, col] = CurrentPlayer;

            CheckWinner();

            if (!GameOver)
                TogglePlayer();

            NotifyStateChanged();
        }

        private void TogglePlayer()
        {
            CurrentPlayer = (CurrentPlayer == "X") ? "O" : "X";
        }

        public void ResetGame()
        {
            Board = new string[3, 3];
            GameOver = false;
            WinnerMessage = string.Empty;
            CurrentPlayer = "X";

            NotifyStateChanged();
        }

        public void ResetScore()
        {
            ScoreX = 0;
            ScoreO = 0;
            ResetGame();

            NotifyStateChanged();
        }

        private void CheckWinner()
        {
            string[,] b = Board;

            string? winner = null;

            string[][] lines = new string[][]
            {
                new[] { b[0, 0], b[0, 1], b[0, 2] },
                new[] { b[1, 0], b[1, 1], b[1, 2] },
                new[] { b[2, 0], b[2, 1], b[2, 2] },
                new[] { b[0, 0], b[1, 0], b[2, 0] },
                new[] { b[0, 1], b[1, 1], b[2, 1] },
                new[] { b[0, 2], b[1, 2], b[2, 2] },
                new[] { b[0, 0], b[1, 1], b[2, 2] },
                new[] { b[0, 2], b[1, 1], b[2, 0] },
            };

            foreach (var line in lines)
            {
                if (line[0] != null && line[0] == line[1] && line[1] == line[2])
                {
                    winner = line[0];
                    break;
                }
            }

            if (winner != null)
            {
                GameOver = true;
                if (winner == "X")
                {
                    ScoreX++;
                    WinnerMessage = $"{PlayerXName} wins!";
                }
                else
                {
                    ScoreO++;
                    WinnerMessage = $"{PlayerOName} wins!";
                }
            }
            else if (IsBoardFull())
            {
                GameOver = true;
                WinnerMessage = "It's a draw!";
            }
        }

        private bool IsBoardFull()
        {
            foreach (var cell in Board)
            {
                if (cell == null)
                    return false;
            }
            return true;
        }

        private void NotifyStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void SetSpectatorName(string spectatorName)
        {
            SpectatorName = spectatorName?.Trim() ?? string.Empty;
            NotifyStateChanged();
        }

        public void ClearSpectator()
        {
            SpectatorName = string.Empty;
            NotifyStateChanged();
        }
    }
}