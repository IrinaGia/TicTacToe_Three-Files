public static class Logic
{
    public const int ROWS = 3;
    public const int COLS = 3;

    public static string[,] InitializeGrid()
    {
        string[,] grid = new string[ROWS, COLS];
        int cellNumber = 1;
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
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

        int row = (choice - 1) / 3;
        int col = (choice - 1) % 3;

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