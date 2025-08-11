using System;

public static class UI
{
    public static void ShowTitle()
    {
        Console.Clear();
        Console.WriteLine("Tic Tac Toe");
    }

    public static void PrintGrid(string[,] grid)
    {
        for (int i = 0; i < Constants.ROWS; i++)
        {
            Console.WriteLine();
            for (int j = 0; j < Constants.COLS; j++)
            {
                Console.Write(" " + grid[i, j] + " ");
                if (j < 2) Console.Write("|");
            }
            if (i < 2)
                Console.WriteLine("\n---+---+---");
        }
    }

    public static string GetPlayerInput(string currentPlayer)
    {
        Console.Write($"\nPlayer {currentPlayer}, choose a cell (1-9): ");
        return Console.ReadLine();
    }

    public static void ShowInvalidInputMessage()
    {
        Console.WriteLine("Invalid input. Press any key to try again.");
        Console.ReadKey();
    }

    public static void ShowDrawMessage()
    {
        Console.WriteLine("\nIt's a draw!");
    }
}