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
            //  board.DisplayBoard();
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            // Begin timing.
            stopwatch.Start();
            Tuple<int, int> selectedMove = currentPlayer.GetMove(board);
            board[selectedMove.Item1, selectedMove.Item2] = currentPlayer.counter;
            
            if (IsOver(board, currentPlayer))
            {
                if (currentPlayer.Win(board, currentPlayer.counter))
                {
                    board.DisplayBoard();
                    if (currentPlayer.GetType() == typeof(AIPlayer))
                    {
                        Console.WriteLine("========================================================================================================================"
                          + Environment.NewLine + "GAME OVER! " + Environment.NewLine + 
                            "------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine +
                            "Winner: " + currentPlayer.counter);
                    }
                    else
                    {
                        Console.WriteLine("========================================================================================================================"
                           + Environment.NewLine + "GAME OVER! " + Environment.NewLine +
                             "------------------------------------------------------------------------------------------------------------------------" +
                             "Winner: " + currentPlayer.counter);
                    }
                    // Stop timing.
                    stopwatch.Stop();
                    // Write result.
                    Console.WriteLine("Total elapsed for Minimax over full game execution: " + stopwatch.Elapsed + Environment.NewLine +
                            "========================================================================================================================");
                    Console.ReadLine();
                    Program.Main();
                }
                Console.WriteLine("The game is a draw.");
                // Stop timing.
                stopwatch.Stop();
                Console.WriteLine("Total elapsed game time: " + stopwatch.Elapsed);
                Console.ReadLine();
                Program.Main();
            }
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
