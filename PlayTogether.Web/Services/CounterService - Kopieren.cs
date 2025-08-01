using System;
using Blazored.LocalStorage;


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
        public int GetWinsForPlayer(string playerName)
        {
            // Example logic
            return playerName == PlayerXName ? ScoreX : (playerName == PlayerOName ? ScoreO : 0);
        }

        public int GetLossesForPlayer(string playerName)
        {
            // Very basic, just a mirror of wins for now
            if (playerName == PlayerXName) return ScoreO;
            if (playerName == PlayerOName) return ScoreX;
            return 0;
        }
        
        public class PlayerStats
        {
            public int Wins { get; set; }
            public int Losses { get; set; }
        }
        public async Task SaveScoresToLocalStorageAsync(ILocalStorageService localStorage)
        {
            var statsX = new PlayerStats { Wins = ScoreX, Losses = ScoreO };
            var statsO = new PlayerStats { Wins = ScoreO, Losses = ScoreX };

            await localStorage.SetItemAsync($"history_{PlayerXName.ToLower()}", statsX);
            await localStorage.SetItemAsync($"history_{PlayerOName.ToLower()}", statsO);
        }

        public async Task LoadScoresFromLocalStorageAsync(ILocalStorageService localStorage)
        {
            var statsX = await localStorage.GetItemAsync<PlayerStats>($"history_{PlayerXName.ToLower()}");
            var statsO = await localStorage.GetItemAsync<PlayerStats>($"history_{PlayerOName.ToLower()}");

            if (statsX != null)
            {
                ScoreX = statsX.Wins;
                ScoreO = statsX.Losses;
            }
            else if (statsO != null)
            {
                ScoreO = statsO.Wins;
                ScoreX = statsO.Losses;
            }
        }

        



    }
}