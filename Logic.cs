using System;

public static class Logic
{
    public static string[,] InitializeGrid()
    {
        string[,] grid = new string[Constants.ROWS, Constants.COLS];
        int cellNumber = 1;
        for (int i = 0; i < Constants.ROWS; i++)
        {
            for (int j = 0; j < Constants.COLS; j++)
            {
                grid[i, j] = cellNumber.ToString();
                cellNumber++;
            }
        }
        return grid;
    }

    public static bool TryMakeMove(string input, string[,] grid, string currentPlayer)
    {
        if (!int.TryParse(input, out int choice) || choice < 1 || choice > 9)
            return false;

        int row = (choice - 1) / Constants.COLS;
        int col = (choice - 1) % Constants.COLS;

        if (grid[row, col] == Constants.PLAYER_X || grid[row, col] == Constants.PLAYER_O)
            return false;

        grid[row, col] = currentPlayer;
        return true;
    }

    public static string SwitchPlayer(string currentPlayer)
    {
        return currentPlayer == Constants.PLAYER_X ? Constants.PLAYER_O : Constants.PLAYER_X;
    }


    private static readonly Random rand = new Random();
    public static bool MakeComputerMove(string[,] grid, string computerSymbol)
    {
        // var freeCells = new List<(int, int)>();
        List<(int, int)> freeCells = new List<(int, int)>(); // wanted to try to use this way of defining lists

        for (int i = 0; i < Constants.ROWS; i++)
        {
            for (int j = 0; j < Constants.COLS; j++)
            {
                if (grid[i, j] != "X" && grid[i, j] != "O")
                {
                    freeCells.Add((i, j));
                }
            }
        }

        // Otherwise, make a random move
        if (freeCells.Count > 0)
        {
            var (row, col) = freeCells[rand.Next(freeCells.Count)];
            grid[row, col] = computerSymbol;
            return true;
        }

        return false; // no move possible
    }

    public static bool CheckWin(string[,] grid, string player)
    {
        int size = Constants.ROWS; 

        // Check rows
        for (int i = 0; i < size; i++)
        {
            bool rowWin = true;
            for (int j = 0; j < size; j++)
            {
                if (grid[i, j] != player)
                {
                    rowWin = false;
                    break;
                }
            }
            if (rowWin) return true;
        }

        // Check columns
        for (int j = 0; j < size; j++)
        {
            bool colWin = true;
            for (int i = 0; i < size; i++)
            {
                if (grid[i, j] != player)
                {
                    colWin = false;
                    break;
                }
            }
            if (colWin) return true;
        }

        // Check main diagonal (top-left to bottom-right)
        bool mainDiagWin = true;
        for (int i = 0; i < size; i++)
        {
            if (grid[i, i] != player)
            {
                mainDiagWin = false;
                break;
            }
        }
        if (mainDiagWin) return true;

        // Anti-diagonal (top-right to bottom-left)
        bool antiDiagWin = true;
        for (int i = 0; i < size; i++)
        {
            if (grid[i, size - 1 - i] != player)
            {
                antiDiagWin = false;
                break;
            }
        }
        if (antiDiagWin) return true;

        return false;
    }
}