using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax_TPL
{
    // GAME EXECUTION CLASS
    class Game_TPL
    {
        bool stopMe = false;
        GameBoard_TPL<counters> board = new GameBoard_TPL<counters>(counters.EMPTY);
        GameBoard_TPL<int> scoreBoard = new GameBoard_TPL<int>(21);

        public Game_TPL(Player_TPL _xPlayer_TPL, Player_TPL _oPlayer_TPL)
        {
            PlayGame(_xPlayer_TPL, _oPlayer_TPL);
        }

        public void PlayGame(Player_TPL currentPlayer_TPL, Player_TPL otherPlayer_TPL)
        {
            // Create new stopwatch.
            Stopwatch stopwatch_minimax = new Stopwatch();
            // Begin timing.
            stopwatch_minimax.Start();
            Tuple<int, int> selectedMove = currentPlayer_TPL.GetMove(board, scoreBoard);
            board[selectedMove.Item1, selectedMove.Item2] = currentPlayer_TPL.counter;
            Tuple<int, int> centreof3inarow = new Tuple<int, int> (0,0);

            if (IsOver(board, currentPlayer_TPL))
            {
                if (currentPlayer_TPL.Win(board, currentPlayer_TPL.counter))
                {
                    board.DisplayBoard();
                 
                    if (currentPlayer_TPL.GetType() == typeof(AIPlayer_TPL))
                    {
                        int score = 0;
                        if (AIPlayer_TPL.FindThreeInARow(board, currentPlayer_TPL.counter) == true)
                        {

                            score = 1000;
                           
                        }

                        Console.WriteLine("========================================================================================================================"
                          + Environment.NewLine + "GAME OVER! " + Environment.NewLine +
                            "------------------------------------------------------------------------------------------------------------------------" +
                            "Winner: " + currentPlayer_TPL.counter 
                            + Environment.NewLine + "Score: " + score + Environment.NewLine +
                            "Positions visited: " + AIPlayer_TPL.cont + Environment.NewLine +
                            "Coordinations of winning three-in-a-row at: "
                             + Environment.NewLine + "Cell 1: " + AIPlayer_TPL.IsLeftOfThree(board, currentPlayer_TPL.counter) 
                             + Environment.NewLine + "Cell 2: " + AIPlayer_TPL.IsCentreOfThree(board, currentPlayer_TPL.counter) 
                             + Environment.NewLine + "Cell 3: " + AIPlayer_TPL.IsRightOfThree(board, currentPlayer_TPL.counter));
                    }
                    else
                    {
                        int score = 0;
                        if (AIPlayer_TPL.FindThreeInARow(board, otherPlayer_TPL.counter) == true)
                        {
                            score = -1000;
                        }

                        Console.WriteLine("======================================================================================================================"
                           + Environment.NewLine + "GAME OVER! " + Environment.NewLine +
                             "------------------------------------------------------------------------------------------------------------------------" +
                             "Winner: " + otherPlayer_TPL.counter 
                             + Environment.NewLine + "Score: " + score 
                             + Environment.NewLine + "Coordinations of winning three-in-a-row at: "
                             + Environment.NewLine
                             + "Cell 1: " + AIPlayer_TPL.IsLeftOfThree(board, otherPlayer_TPL.counter) + Environment.NewLine 
                             + "Cell 2: " + AIPlayer_TPL.IsCentreOfThree(board, otherPlayer_TPL.counter) + Environment.NewLine
                             + "Cell 3: " + AIPlayer_TPL.IsRightOfThree(board, otherPlayer_TPL.counter));
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
            PlayGame(otherPlayer_TPL, currentPlayer_TPL);
            
        }

        public bool IsOver(GameBoard_TPL<counters> board, Player_TPL currentPlayer_TPL)
        {
            if (currentPlayer_TPL.Win(board, currentPlayer_TPL.counter) || board.IsFull())
                return true;
            return false;
        }
    }
}
