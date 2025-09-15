using System;

class Program
{
    static void Main(string[] args)
    {
        string[,] grid = Logic.InitializeGrid();
        string currentPlayer = Constants.PLAYER_X;
        int moves = 0;

        while (true)
        {
            UI.ShowTitle();
            UI.PrintGrid(grid);

            if (currentPlayer == Constants.PLAYER_X)
            {
                // Human move
                string input = UI.GetPlayerInput(currentPlayer);

                if (!Logic.TryMakeMove(input, grid, currentPlayer))
                {
                    UI.ShowInvalidInputMessage();
                    continue;
                }
            }
            else
            {
                // Computer move
                Console.WriteLine($"\nMachine ({currentPlayer}) is playing...");
               System.Threading.Thread.Sleep(700); // short delay for realism
                Logic.MakeComputerMove(grid, currentPlayer);

                if (!Logic.MakeComputerMove(grid, currentPlayer)) // when grid is full
                {
                 
                    Console.WriteLine("No free cells left for the computer to move.");
                    break;
                }
            }

            moves++;

            // Check win
            if (Logic.CheckWin(grid, currentPlayer))
            {
                UI.ShowTitle();
                UI.PrintGrid(grid);
                Console.WriteLine($"\nPlayer {currentPlayer} wins!");
                break;
            }

            // Check draw
            if (moves == Constants.CELLS_AMOUNT)
            {
                UI.ShowTitle();
                UI.PrintGrid(grid);
                UI.ShowDrawMessage();
                break;
            }

            // Switch turn
            currentPlayer = Logic.SwitchPlayer(currentPlayer);
        }
    }
}