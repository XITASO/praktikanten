using System;

public class GameService
{
    public string[,] Board { get; private set; } = new string[3, 3];
    public bool WasLastMoveX { get; private set; }
    public string WinnerMessage { get; private set; } = string.Empty;
    public bool GameOver { get; private set; }

    public int XWins { get; private set; }
    public int OWins { get; private set; }

    public string PlayerXName { get; set; } = "Player X";
    public string PlayerOName { get; set; } = "Player O";

    public event EventHandler? StateChanged;

    public void MakeMove(int row, int col)
    {
        if (Board[row, col] != null || GameOver)
            return;

        Board[row, col] = WasLastMoveX ? "o" : "x";
        WasLastMoveX = !WasLastMoveX;

        CheckWinner();
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ResetGame()
    {
        Board = new string[3, 3];
        GameOver = false;
        WinnerMessage = string.Empty;
        WasLastMoveX = false;
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ResetScore()
    {
        XWins = 0;
        OWins = 0;
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void CheckWinner()
    {
        string[,] b = Board;

        string[][] lines = new[]
        {
            new[] { b[0,0], b[0,1], b[0,2] },
            new[] { b[1,0], b[1,1], b[1,2] },
            new[] { b[2,0], b[2,1], b[2,2] },
            new[] { b[0,0], b[1,0], b[2,0] },
            new[] { b[0,1], b[1,1], b[2,1] },
            new[] { b[0,2], b[1,2], b[2,2] },
            new[] { b[0,0], b[1,1], b[2,2] },
            new[] { b[0,2], b[1,1], b[2,0] },
        };

        foreach (var line in lines)
        {
            if (line[0] != null && line[0] == line[1] && line[1] == line[2])
            {
                string winner = line[0]!;
                WinnerMessage = $"{(winner == "x" ? PlayerXName : PlayerOName)} wins!";
                GameOver = true;

                if (winner == "x") XWins++;
                else OWins++;
                return;
            }
        }

        // Draw
        bool isDraw = true;
        foreach (var cell in b)
        {
            if (cell == null)
            {
                isDraw = false;
                break;
            }
        }

        if (isDraw)
        {
            WinnerMessage = "It's a draw!";
            GameOver = true;
        }
    }
}