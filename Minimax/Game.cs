﻿using System;
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
        GameBoard<counters> board = new GameBoard<counters>(counters.EMPTY);

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
            Tuple<int, int> selectedMove = currentPlayer.GetMove(board);
            board[selectedMove.Item1, selectedMove.Item2] = currentPlayer.counter;
            Tuple<int, int> centreof3inarow = new Tuple<int, int> (0,0);

            if (IsOver(board, currentPlayer))
            {
                if (currentPlayer.Win(board, currentPlayer.counter))
                {
                    board.DisplayBoard();
                 
                    if (currentPlayer.GetType() == typeof(AIPlayer<counters>))
                    {
                        int score = 0;
                        if (AIPlayer<counters>.FindThreeInARow(board, currentPlayer.counter) == true)
                        {

                            score = 1000;
                           
                        }

                        Console.WriteLine("========================================================================================================================"
                          + Environment.NewLine + "GAME OVER! " + Environment.NewLine +
                            "------------------------------------------------------------------------------------------------------------------------" +
                            "Winner: " + currentPlayer.counter 
                            + Environment.NewLine + "Score: " + score + Environment.NewLine +
                            "Positions visited: " + AIPlayer<counters>.cont + Environment.NewLine +
                            "Coordinations of winning three-in-a-row at: "
                             + Environment.NewLine + "Cell 1: " + AIPlayer<counters>.IsLeftOfThree(board, currentPlayer.counter) 
                             + Environment.NewLine + "Cell 2: " + AIPlayer<counters>.IsCentreOfThree(board, currentPlayer.counter) 
                             + Environment.NewLine + "Cell 3: " + AIPlayer<counters>.IsRightOfThree(board, currentPlayer.counter));
                    }
                    else
                    {
                        int score = 0;
                        if (AIPlayer<counters>.FindThreeInARow(board, otherPlayer.counter) == true)
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
                             + "Cell 1: " + AIPlayer<counters>.IsLeftOfThree(board, otherPlayer.counter) + Environment.NewLine 
                             + "Cell 2: " + AIPlayer<counters>.IsCentreOfThree(board, otherPlayer.counter) + Environment.NewLine
                             + "Cell 3: " + AIPlayer<counters>.IsRightOfThree(board, otherPlayer.counter));
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
