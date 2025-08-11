using System;

class Program
{
    static void Main(string[] args)
    {
        string[,] grid = Logic.InitializeGrid();
        string currentPlayer = "X";
        int moves = 0;

        while (true)
        {
            UI.ShowTitle();
            UI.PrintGrid(grid);

            string input = UI.GetPlayerInput(currentPlayer);

            if (!Logic.TryMakeMove(input, grid, currentPlayer))
            {
                UI.ShowInvalidInputMessage();
                continue;
            }

            moves++;

            if (moves == Constants.CELLS_AMOUNT)
            {
                UI.ShowTitle();
                UI.PrintGrid(grid);
                UI.ShowDrawMessage();
                break;
            }

            currentPlayer = Logic.SwitchPlayer(currentPlayer);
        }
    }
}