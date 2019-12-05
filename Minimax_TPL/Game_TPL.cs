using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minimax_TPL
{
    /* 
    ----------------------------------------------------------------------------------------------------------------
     * Game_TPL.CS -
    --------------------------------------------------------------------------------------------------------------------------
    Class controls flow of order of execution in the gameplay.
    --------------------------------------------------------------------------------------------------------------------------
    */
    class Game_TPL
    {
        // PUBLIC DECS
        public static int cntr = 1;
        public static int nowcount = 0;
        bool stopMe = false;
        public static GameBoard_TPL<counters> board = new GameBoard_TPL<counters>(counters.e);
        public static GameBoard_TPL<int> scoreBoard = new GameBoard_TPL<int>(21);
        static counters counter = counters.X;
        counters us = Flip(counter);
        /* 
-------------------------------------------------------------------------------------------------------------------------
* Game constructor -
--------------------------------------------------------------------------------------------------------------------------
*/
        public Game_TPL(Player_TPL _xPlayer, Player_TPL _oPlayer)
        {
            if (cntr == 1)
            {
                File.WriteAllText(@"data/intboards.txt", string.Empty);
                File.WriteAllText(@"data/finboards.txt", string.Empty);
                File.WriteAllText(@"data/scoreboards.txt", string.Empty);
                File.WriteAllText(@"data/TPLTST_Report.csv", string.Empty);
                File.WriteAllText(@"data/printresult_stream.txt", string.Empty);
		// HWL: header for output to console
		Console.WriteLine(" -*- outline -*- ");
		Console.WriteLine("* LOG ");
            }
	    // if (cntr==1) cntr=7; // HWL: jump to board 7
            PlayGame(_xPlayer, _oPlayer, ref cntr);
        }

        /* 
----------------------------------------------------------------------------------------------------------------
* PlayGame -
--------------------------------------------------------------------------------------------------------------------------
The method runs the execution of the entire game, iterating the starting board each time. 
--------------------------------------------------------------------------------------------------------------------------
*/
        public void PlayGame(Player_TPL currentPlayer, Player_TPL otherPlayer, ref int cntr)
        {
	    Tuple<int, int> expectedMove = new Tuple<int, int>(2, 2);
	    // Create new stopwatch.
            Stopwatch stopwatch_minimax = new Stopwatch();
            // Begin timing.
            stopwatch_minimax.Start();
            switch (cntr)
            {
                case 1:
		    currentPlayer.counter = counters.X; // HWL: set the current player here as well
		    expectedMove = new Tuple<int, int>(2,3);
                    // choose win
                    board[1, 1] = counters.O; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.X; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.X; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.O; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.X; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.O; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 2:
		    currentPlayer.counter = counters.X; // HWL: set the current player here as well
		    expectedMove = new Tuple<int, int>(1,1);
                    board[1, 1] = counters.e; board[2, 1] = counters.X; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.X; board[2, 2] = counters.e; board[3, 2] = counters.O; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.O; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 3:
                    currentPlayer.counter = counters.X; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(1, 2);
                    // choose win
                    board[1, 1] = counters.X; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.X; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 4:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.X; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.X; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 5:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(1, 3);
                    // try multiple blocks or choose win
                    board[1, 1] = counters.X; board[2, 1] = counters.X; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.O; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.O; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.X; board[4, 4] = counters.O; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 6:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.O; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.X; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.X; board[6, 6] = counters.X; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.X; board[7, 7] = counters.e;
                    break;
	      case 7: // X should pick (3,3) (with maxPly=4), which then results in Board 1, which is an X win
		    currentPlayer.counter = counters.X; // HWL: set the current player here as well
		    expectedMove = new Tuple<int, int>(3,1); // (3,3) is also a winning move, but comes later
                    // choose win
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.X; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.O; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.X; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.O; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 8:
                    currentPlayer.counter = counters.X; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(2, 2);
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.X; board[4, 3] = counters.e; board[5, 3] = counters.O; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.O; board[4, 5] = counters.e; board[5, 5] = counters.X; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 9:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.X; board[6, 1] = counters.e; board[7, 1] = counters.X;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.O;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.O;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.O;
                    break;
                case 10:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.X; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.X; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.O; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.O; board[5, 4] = counters.O; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.X; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.X; board[4, 6] = counters.X; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.X; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 11:
                    currentPlayer.counter = counters.X; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(3, 1);
                    board[1, 1] = counters.X; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.O; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.X; board[3, 2] = counters.O; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.O; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.X; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 12:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(1, 2);
                    board[1, 1] = counters.X; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.X; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.X; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.X; board[2, 3] = counters.e; board[3, 3] = counters.O; board[4, 3] = counters.e; board[5, 3] = counters.O; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.O; board[5, 4] = counters.O; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.O; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.X; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.X; board[4, 6] = counters.X; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.X; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 13:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.X; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.X; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.O; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.X; board[3, 4] = counters.X; board[4, 4] = counters.O; board[5, 4] = counters.O; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.X; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.X; board[4, 6] = counters.X; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.X; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 14:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.X; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.X; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.O; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.O; board[5, 4] = counters.O; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.X; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.X; board[2, 6] = counters.e; board[3, 6] = counters.X; board[4, 6] = counters.X; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.X; board[2, 7] = counters.e; board[3, 7] = counters.X; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 15:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.X; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.O; board[2, 7] = counters.X; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 16:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.O; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.O; board[2, 7] = counters.X; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 17:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(1, 3);
                    // choose win
                    board[1, 1] = counters.X; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.X; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 18:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.O; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.X; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.X; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 19:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(2, 2);
                    board[1, 1] = counters.X; board[2, 1] = counters.O; board[3, 1] = counters.O; board[4, 1] = counters.e; board[5, 1] = counters.O; board[6, 1] = counters.e; board[7, 1] = counters.X;
                    board[1, 2] = counters.O; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.X; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.O; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.X; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.O; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.X; board[2, 7] = counters.O; board[3, 7] = counters.X; board[4, 7] = counters.O; board[5, 7] = counters.X; board[6, 7] = counters.O; board[7, 7] = counters.X;
                    break;
                case 20:
                    currentPlayer.counter = counters.X; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(2, 2);
                    board[1, 1] = counters.O; board[2, 1] = counters.X; board[3, 1] = counters.X; board[4, 1] = counters.e; board[5, 1] = counters.O; board[6, 1] = counters.e; board[7, 1] = counters.X;
                    board[1, 2] = counters.X; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.O; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.O; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.X; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.O; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.X; board[2, 7] = counters.O; board[3, 7] = counters.X; board[4, 7] = counters.O; board[5, 7] = counters.X; board[6, 7] = counters.O; board[7, 7] = counters.X;
                    break;
                case 21:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(1, 2);
                    // choose win
                    board[1, 1] = counters.O; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.O; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 22:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(2, 2);
                    board[1, 1] = counters.e; board[2, 1] = counters.O; board[3, 1] = counters.O; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.O; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 23:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.O; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.O; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 24:
                    currentPlayer.counter = counters.X; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(3, 1);
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.X; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.O; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.X; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.X; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.O; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 25:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(1, 2);
                    // try multiple blocks or choose win
                    board[1, 1] = counters.O; board[2, 1] = counters.O; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.X; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.O; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.X; board[4, 4] = counters.X; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 26:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.O; board[6, 1] = counters.e; board[7, 1] = counters.O;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.X;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.X;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.X;
                    break;
                case 27:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.X; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.O; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.O; board[6, 6] = counters.O; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.O; board[7, 7] = counters.e;
                    break;
                case 28:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.X; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.X; board[5, 3] = counters.X; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.X; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 29:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(2, 2);
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.O; board[4, 3] = counters.e; board[5, 3] = counters.X; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.X; board[4, 5] = counters.e; board[5, 5] = counters.O; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 30:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.O; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.O; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.X; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.X; board[5, 4] = counters.X; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.O; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.O; board[4, 6] = counters.O; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.O; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 31:
                    currentPlayer.counter = counters.X; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(2, 2);
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.X; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.X; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.O; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.O; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 32:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(1, 2);
                    board[1, 1] = counters.O; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.O; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.O; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.O; board[2, 3] = counters.e; board[3, 3] = counters.X; board[4, 3] = counters.e; board[5, 3] = counters.X; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.X; board[5, 4] = counters.X; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.X; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.O; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.O; board[4, 6] = counters.O; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.O; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 33:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.O; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.O; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.X; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.O; board[3, 4] = counters.O; board[4, 4] = counters.X; board[5, 4] = counters.X; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.O; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.O; board[4, 6] = counters.O; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.O; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 34:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.O; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.O; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.X; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.X; board[5, 4] = counters.X; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.O; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.O; board[2, 6] = counters.e; board[3, 6] = counters.O; board[4, 6] = counters.O; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.O; board[2, 7] = counters.e; board[3, 7] = counters.O; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 35:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.O; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.X; board[2, 7] = counters.O; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 36:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.X; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.X; board[2, 7] = counters.O; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 37:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.X; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.X; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 38:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.X; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.O; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.O; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 39:
                    currentPlayer.counter = counters.X; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(2, 2);
                    board[1, 1] = counters.O; board[2, 1] = counters.X; board[3, 1] = counters.X; board[4, 1] = counters.e; board[5, 1] = counters.X; board[6, 1] = counters.e; board[7, 1] = counters.O;
                    board[1, 2] = counters.X; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.O; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.X; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.O; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.X; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.O; board[2, 7] = counters.X; board[3, 7] = counters.O; board[4, 7] = counters.X; board[5, 7] = counters.O; board[6, 7] = counters.X; board[7, 7] = counters.O;
                    break;
                case 40:
                    currentPlayer.counter = counters.O; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(2, 2);
                    board[1, 1] = counters.O; board[2, 1] = counters.X; board[3, 1] = counters.X; board[4, 1] = counters.O; board[5, 1] = counters.X; board[6, 1] = counters.O; board[7, 1] = counters.O;
                    board[1, 2] = counters.X; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.O;
                    board[1, 3] = counters.O; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.X;
                    board[1, 4] = counters.X; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.O;
                    board[1, 5] = counters.O; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.X;
                    board[1, 6] = counters.X; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.X;
                    board[1, 7] = counters.O; board[2, 7] = counters.X; board[3, 7] = counters.O; board[4, 7] = counters.X; board[5, 7] = counters.O; board[6, 7] = counters.X; board[7, 7] = counters.O;
                    break;
                case 41:
                    currentPlayer.counter = counters.X; // HWL: set the current player here as well
                    expectedMove = new Tuple<int, int>(2, 3);
                    board[1, 1] = counters.O; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.O; board[5, 1] = counters.X; board[6, 1] = counters.O; board[7, 1] = counters.O;
                    board[1, 2] = counters.e; board[2, 2] = counters.X; board[3, 2] = counters.O; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.O;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.X; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.X;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.O; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.O;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.O; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.X;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.X;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.X; board[5, 7] = counters.O; board[6, 7] = counters.X; board[7, 7] = counters.O;
                    break;
                default:
                    Environment.Exit(99);
                    break;
            }

	    { // HWL: main loop of running the game now here, rather than in GetMove()
	      int move = 0;
	      int numTasks = 4;
	      Tuple<int, Tuple<int, int>> bestRes;
	      Tuple<int, int> bestMove;
	      int bestScore;
	      Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
	      Console.WriteLine("** HWL: Running board {0} "  , cntr);
	      Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++");
	      board.DisplayBoard();
	      counter = currentPlayer.counter;
	      do {
		move++;
		// parallel version needs to call the ParSearchWrapper
		// bestRes = ParSearchWrap(board, counter, numTasks, scoreBoard, ref move);
		// seq version
		bestMove = currentPlayer.GetMove(board, counter, scoreBoard);
		// bestScore = bestRes.Item1;
		// bestMove  = bestRes.Item2;
		// place the current piece
		board[bestMove.Item1, bestMove.Item2] = counter;
		// opponent's turn
		counter = Flip(counter);
                    if (IsOver(board, currentPlayer)) {
                        Console.WriteLine("=========================================================================================================");
                        Console.WriteLine("-- Winning notification for Board " + Game_TPL.cntr + " :");
                        Console.WriteLine("=========================================================================================================");
                        Console.WriteLine("-- Game result: WINNER on Board " + Game_TPL.cntr + " is: counter " + currentPlayer.counter + " with winning position " + new Tuple<int,int>(bestMove.Item1, bestMove.Item2));
                        Console.WriteLine("--------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("-- Expected result on Board " + Game_TPL.cntr + " is: " + expectedMove + " with counter " + currentPlayer.counter);
                        Console.WriteLine("========================================================================================================" + Environment.NewLine);
                        cntr++;
                        if (cntr > 2 && cntr < 40) return;
                        PlayGame(currentPlayer, otherPlayer, ref cntr);
                    }
                }
                while (!currentPlayer.Win(board, Flip(counter)) && !otherPlayer.Win(board, counter) && !board.IsFull(3));
                // TODO: print message who actually won; check this against expected result
                
                // Tuple<int, int> selectedMove = currentPlayer.GetMove(board, currentPlayer.counter, scoreBoard);
                // board[selectedMove.Item1, selectedMove.Item2] = currentPlayer.counter;
                // Tuple<int, int> centreof3inarow = new Tuple<int, int>(0, 0);

                /*
                // cntr = (cntr==1) ? 7 : cntr+1;
                  
	      // cntr = (cntr==1) ? 7 : cntr+1;
	      cntr++;
	      if (cntr > 2) return;
	      PlayGame(currentPlayer, otherPlayer, ref cntr);
	      
                */
            }
            return;
        }
        /* 
      ----------------------------------------------------------------------------------------------------------------
      * IsOver -
      --------------------------------------------------------------------------------------------------------------------------
      This boolean dictates wherever if there is win on the current board, and if not the search will continue.
      --------------------------------------------------------------------------------------------------------------------------
      */
        public static bool IsOver(GameBoard_TPL<counters> board, Player_TPL currentPlayer)
        {
            if (currentPlayer.Win(board, currentPlayer.counter) || board.IsFull(3))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
            /* 
                  ----------------------------------------------------------------------------------------------------------------
                  * Flip -
                  --------------------------------------------------------------------------------------------------------------------------
                  Construct flips the counter have each turn of play.
                  --------------------------------------------------------------------------------------------------------------------------
                  */
            public static counters Flip(counters counter)
        {
            if (counter == counters.O)
            {
                return counters.X;
            }
            else
            {
                return counters.O;
            }
        }
    }
}
