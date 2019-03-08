using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
    // GAME EXECUTION CLASS
    class Game
    {
        GameBoard board = new GameBoard(counters.EMPTY);

        public Game(Player _xPlayer, Player _oPlayer)
        {
            PlayGame(_xPlayer, _oPlayer);
        }

        public void PlayGame(Player currentPlayer, Player otherPlayer)
        {
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            // Begin timing.
            stopwatch.Start();
            board.DisplayBoard();
            Tuple<int, int> selectedMove = currentPlayer.GetMove(board);
            board[selectedMove.Item1, selectedMove.Item2] = currentPlayer.counter;
            // Stop timing.
            stopwatch.Stop();
            // Write result.
            Console.Write("Time elapsed: {0}", stopwatch.Elapsed, "selected move:", selectedMove);

            if (IsOver(board, currentPlayer))
            {
                if (currentPlayer.Win(board, currentPlayer.counter))
                {
                    board.DisplayBoard();
                    if (currentPlayer.GetType() == typeof(AIPlayer))
                    {
                        Console.WriteLine("The computer has beaten you. Better luck next time...");
                    }
                    else
                    {
                        Console.WriteLine("Congratulations! {0} has won!", currentPlayer.name);
                    }

                    Console.ReadLine();
                    Program.Main();
                }
                Console.WriteLine("The game is a draw.");
                Console.ReadLine();
                Program.Main();
            }
            
          //  Console.Write(selectedMove);
           // string x = Console.ReadLine();
              PlayGame(otherPlayer, currentPlayer);
        }

        public bool IsOver(GameBoard board, Player currentPlayer)
        {
            if (currentPlayer.Win(board, currentPlayer.counter) || board.IsFull())
                return true;
            return false;
        }
    }
}
