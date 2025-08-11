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
        if (!int.TryParse(input, out int choice) || choice < 1 || choice > Constants.CELLS_AMOUNT)
            return false;

        int row = (choice - 1) / Constants.ROWS;
        int col = (choice - 1) % Constants.COLS;

        // Check if the cell is already taken
        if (grid[row, col] == "X" || grid[row, col] == "O")
            return false;

        grid[row, col] = currentPlayer;
        return true;
    }

    public static string SwitchPlayer(string currentPlayer)
    {
        return currentPlayer == "X" ? "O" : "X";
    }
}