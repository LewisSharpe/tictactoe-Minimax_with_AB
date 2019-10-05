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
        bool stopMe = false;
        GameBoard<counters> board = new GameBoard<counters>(counters.EMPTY);
        GameBoard<int> scoreBoard = new GameBoard<int>(21);

        public Game(Player _xPlayer, Player _oPlayer)
        {
            PlayGame(_xPlayer, _oPlayer);
        }

        public void PlayGame(Player currentPlayer, Player otherPlayer)
        {
            // Create new stopwatch.
            Stopwatch stopwatch_minimax = new Stopwatch();
            // Begin timing.
            stopwatch_minimax.Start();
            board[1, 3] = counters.CROSSES;
            board[1, 1] = counters.CROSSES;
            board[1, 2] = counters.CROSSES;
            board[3, 1] = counters.CROSSES;
            Tuple<int, int> selectedMove = currentPlayer.GetMove(board, scoreBoard);
            board[selectedMove.Item1, selectedMove.Item2] = currentPlayer.counter;
            Tuple<int, int> centreof3inarow = new Tuple<int, int> (0,0);

            if (IsOver(board, currentPlayer))
            {
                if (currentPlayer.Win(board, currentPlayer.counter))
                {
                    board.DisplayBoard();
                 
                    if (currentPlayer.GetType() == typeof(AIPlayer))
                    {
                        int score = 0;
                        if (AIPlayer.FindThreeInARow(board, currentPlayer.counter) == true)
                        {

                            score = 1000;
                           
                        }

                        Console.WriteLine("========================================================================================================================"
                          + Environment.NewLine + "GAME OVER! " + Environment.NewLine +
                            "------------------------------------------------------------------------------------------------------------------------" +
                            "Winner: " + currentPlayer.counter 
                            + Environment.NewLine + "Score: " + score + Environment.NewLine +
                            "Positions visited: " + AIPlayer.cont + Environment.NewLine +
                            "Coordinations of winning three-in-a-row at: "
                             + Environment.NewLine + "Cell 1: " + AIPlayer.IsLeftOfThree(board, currentPlayer.counter) 
                             + Environment.NewLine + "Cell 2: " + AIPlayer.IsCentreOfThree(board, currentPlayer.counter) 
                             + Environment.NewLine + "Cell 3: " + AIPlayer.IsRightOfThree(board, currentPlayer.counter));
                    }
                    else
                    {
                        int score = 0;
                        if (AIPlayer.FindThreeInARow(board, otherPlayer.counter) == true)
                        {
                            score = -1000;
                        }

                        Console.WriteLine("======================================================================================================================"
                           + Environment.NewLine + "GAME OVER! " + Environment.NewLine +
                             "------------------------------------------------------------------------------------------------------------------------" +
                             "Winner: " + otherPlayer.counter 
                             + Environment.NewLine + "Score: " + score 
                             + Environment.NewLine + "Coordinations of winning three-in-a-row at: "
                             + Environment.NewLine
                             + "Cell 1: " + AIPlayer.IsLeftOfThree(board, otherPlayer.counter) + Environment.NewLine 
                             + "Cell 2: " + AIPlayer.IsCentreOfThree(board, otherPlayer.counter) + Environment.NewLine
                             + "Cell 3: " + AIPlayer.IsRightOfThree(board, otherPlayer.counter));
                    }
                    // Stop timing.
                    stopwatch_minimax.Stop();
                    // Write result.
                    Console.WriteLine("Total elapsed for Minimax over full game execution: " + stopwatch_minimax.Elapsed + Environment.NewLine +
                            "========================================================================================================================");
                    Console.ReadLine();
                    Program.Main();
                }
                Console.WriteLine("The game is a draw.");
                Program.Main();
            }
	    if (stopMe) {
	      stopwatch_minimax.Stop();
	      Console.WriteLine("**HWL One move made. ");
	      Console.WriteLine("**HWL elapsed time for one move: " + stopwatch_minimax.Elapsed + Environment.NewLine + "-------------------------------------------------------");
	    } else {
	      stopMe = true;
	    }
            PlayGame(otherPlayer, currentPlayer);
            
        }

        public bool IsOver(GameBoard<counters> board, Player currentPlayer)
        {
            if (currentPlayer.Win(board, currentPlayer.counter) || board.IsFull())
                return true;
            return false;
        }
    }
}
