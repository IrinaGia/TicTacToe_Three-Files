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
        // Check rows
        for (int i = 0; i < Constants.ROWS; i++)
        {
            if (grid[i, Constants.FIRST_COL] == player && grid[i, Constants.SECOND_COL] == player && grid[i, Constants.THIRD_COL] == player)
                return true;
        }

        // Check columns
        for (int j = 0; j < Constants.COLS; j++)
        {
            if (grid[Constants.FIRST_ROW, j] == player && grid[Constants.SECOND_ROW, j] == player && grid[Constants.THIRD_ROW, j] == player)
                return true;
        }

        // Check diagonals
        if (grid[Constants.FIRST_ROW, Constants.FIRST_COL] == player && grid[Constants.SECOND_ROW, Constants.SECOND_COL] == player && grid[Constants.THIRD_ROW, Constants.THIRD_COL] == player)
            return true;

        if (grid[Constants.FIRST_ROW, Constants.THIRD_COL] == player && grid[Constants.SECOND_ROW, Constants.SECOND_COL] == player && grid[Constants.THIRD_ROW, Constants.FIRST_COL] == player)
            return true;

        return false;
    }
}