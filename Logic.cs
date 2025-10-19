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
        string opponent = computerSymbol == Constants.PLAYER_X ? Constants.PLAYER_O : Constants.PLAYER_X;

        // block the opponent from winning
        if (TryBlockOpponent(grid, opponent, computerSymbol))
            return true;

        // Otherwise make a random move
        List<(int, int)> freeCells = new List<(int, int)>();
        for (int i = 0; i < Constants.ROWS; i++)
        {
            for (int j = 0; j < Constants.COLS; j++)
            {
                if (grid[i, j] != Constants.PLAYER_X && grid[i, j] != Constants.PLAYER_O)
                    freeCells.Add((i, j));
            }
        } 

        if (freeCells.Count > 0)
        {
            var (row, col) = freeCells[rand.Next(freeCells.Count)];
            grid[row, col] = computerSymbol;
            return true;
        }

        return false;
    }

    private static bool TryBlockOpponent(string[,] grid, string opponent, string computerSymbol)
    {
        // Check rows
        for (int i = 0; i < Constants.ROWS; i++)
        {
            int opponentCount = 0;
            int emptyCount = 0;
            int emptyCol = Constants.NOT_FOUND;

            for (int j = 0; j < Constants.COLS; j++)
            {
                if (grid[i, j] == opponent) opponentCount++;
                else if (grid[i, j] != Constants.PLAYER_X && grid[i, j] != Constants.PLAYER_O)
                {
                    emptyCount++;
                    emptyCol = j;
                }
            }

            if (opponentCount == Constants.TWO_IN_A_ROW && emptyCount == Constants.ONE_EMPTY_CELL)
            {
                grid[i, emptyCol] = computerSymbol;
                return true;
            }
        }

        // Check columns
        for (int j = 0; j < Constants.COLS; j++)
        {
            int opponentCount = 0;
            int emptyCount = 0;
            int emptyRow = Constants.NOT_FOUND;

            for (int i = 0; i < Constants.ROWS; i++)
            {
                if (grid[i, j] == opponent) opponentCount++;
                else if (grid[i, j] != Constants.PLAYER_X && grid[i, j] != Constants.PLAYER_O)
                {
                    emptyCount++;
                    emptyRow = i;
                }
            }

            if (opponentCount == Constants.TWO_IN_A_ROW && emptyCount == Constants.ONE_EMPTY_CELL)
            {
                grid[emptyRow, j] = computerSymbol;
                return true;
            }
        }

        // Check main diagonal
        int diagOpponent = 0;
        int diagEmpty = 0;
        int diagEmptyIndex = Constants.NOT_FOUND;

        for (int i = 0; i < Constants.ROWS; i++)
        {
            if (grid[i, i] == opponent) diagOpponent++;
            else if (grid[i, i] != Constants.PLAYER_X && grid[i, i] != Constants.PLAYER_O)
            {
                diagEmpty++;
                diagEmptyIndex = i;
            }
        }
        if (diagOpponent == Constants.TWO_IN_A_ROW && diagEmpty == Constants.ONE_EMPTY_CELL)
        {
            grid[diagEmptyIndex, diagEmptyIndex] = computerSymbol;
            return true;
        }

        // Check anti-diagonal
        diagOpponent = 0;
        diagEmpty = 0;
        diagEmptyIndex = Constants.NOT_FOUND;

        for (int i = 0; i < Constants.ROWS; i++)
        {
            if (grid[i, Constants.COLS - 1 - i] == opponent) diagOpponent++;
            else if (grid[i, Constants.COLS - 1 - i] != Constants.PLAYER_X && grid[i, Constants.COLS - 1 - i] != Constants.PLAYER_O)
            {
                diagEmpty++;
                diagEmptyIndex = i;
            }
        }
        if (diagOpponent == Constants.TWO_IN_A_ROW && diagEmpty == Constants.ONE_EMPTY_CELL)
        {
            grid[diagEmptyIndex, Constants.COLS - 1 - diagEmptyIndex] = computerSymbol;
            return true;
        }

        return false;
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