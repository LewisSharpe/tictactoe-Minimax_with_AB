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
    // GAME EXECUTION CLASS
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
            }
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
            // Create new stopwatch.
            Stopwatch stopwatch_minimax = new Stopwatch();
            // Begin timing.
            stopwatch_minimax.Start();
            switch (cntr)
            {
                case 0:
                    // HWL: this is the 'diagonal' board that should find a win in 2 moves
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.O; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.X; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.X; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.O; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 1:
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
                    board[1, 1] = counters.e; board[2, 1] = counters.X; board[3, 1] = counters.X; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.X; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 3:
                    // choose win
                    board[1, 1] = counters.X; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.X; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
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
                case 7:
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.O; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.O; board[5, 3] = counters.O; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.O; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 8:
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
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.O; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.O; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.X; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.X; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 12:
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
                    board[1, 1] = counters.X; board[2, 1] = counters.O; board[3, 1] = counters.O; board[4, 1] = counters.e; board[5, 1] = counters.O; board[6, 1] = counters.e; board[7, 1] = counters.X;
                    board[1, 2] = counters.O; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.X; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.O; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.X; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.O; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.X; board[2, 7] = counters.O; board[3, 7] = counters.X; board[4, 7] = counters.O; board[5, 7] = counters.X; board[6, 7] = counters.O; board[7, 7] = counters.X;
                    break;
                case 20:
                   board[1, 1] = counters.X; board[2, 1] = counters.O; board[3, 1] = counters.O; board[4, 1] = counters.e; board[5, 1] = counters.O; board[6, 1] = counters.e; board[7, 1] = counters.X;
                    board[1, 2] = counters.O; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.X; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.O; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.X; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.O; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.X; board[2, 7] = counters.O; board[3, 7] = counters.X; board[4, 7] = counters.O; board[5, 7] = counters.X; board[6, 7] = counters.O; board[7, 7] = counters.X;
                    break;
                case 21:
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
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.e; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.X; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.e; board[3, 3] = counters.O; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.e; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.X; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.X; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.O; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 25:
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
                    board[1, 1] = counters.e; board[2, 1] = counters.e; board[3, 1] = counters.e; board[4, 1] = counters.X; board[5, 1] = counters.e; board[6, 1] = counters.e; board[7, 1] = counters.e;
                    board[1, 2] = counters.e; board[2, 2] = counters.e; board[3, 2] = counters.X; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.e; board[2, 3] = counters.O; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.O; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.e; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.e; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.e; board[2, 7] = counters.e; board[3, 7] = counters.e; board[4, 7] = counters.e; board[5, 7] = counters.e; board[6, 7] = counters.e; board[7, 7] = counters.e;
                    break;
                case 32:
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
                    board[1, 1] = counters.O; board[2, 1] = counters.X; board[3, 1] = counters.X; board[4, 1] = counters.e; board[5, 1] = counters.X; board[6, 1] = counters.e; board[7, 1] = counters.O;
                    board[1, 2] = counters.X; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.e;
                    board[1, 3] = counters.O; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.e;
                    board[1, 4] = counters.X; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.e;
                    board[1, 5] = counters.O; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.e;
                    board[1, 6] = counters.X; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.e;
                    board[1, 7] = counters.O; board[2, 7] = counters.X; board[3, 7] = counters.O; board[4, 7] = counters.X; board[5, 7] = counters.O; board[6, 7] = counters.X; board[7, 7] = counters.O;
                    break;
                case 40:
                    board[1, 1] = counters.O; board[2, 1] = counters.X; board[3, 1] = counters.X; board[4, 1] = counters.O; board[5, 1] = counters.X; board[6, 1] = counters.O; board[7, 1] = counters.O;
                    board[1, 2] = counters.X; board[2, 2] = counters.e; board[3, 2] = counters.e; board[4, 2] = counters.e; board[5, 2] = counters.e; board[6, 2] = counters.e; board[7, 2] = counters.O;
                    board[1, 3] = counters.O; board[2, 3] = counters.e; board[3, 3] = counters.e; board[4, 3] = counters.e; board[5, 3] = counters.e; board[6, 3] = counters.e; board[7, 3] = counters.X;
                    board[1, 4] = counters.X; board[2, 4] = counters.e; board[3, 4] = counters.e; board[4, 4] = counters.e; board[5, 4] = counters.e; board[6, 4] = counters.e; board[7, 4] = counters.O;
                    board[1, 5] = counters.O; board[2, 5] = counters.e; board[3, 5] = counters.e; board[4, 5] = counters.e; board[5, 5] = counters.e; board[6, 5] = counters.e; board[7, 5] = counters.X;
                    board[1, 6] = counters.X; board[2, 6] = counters.e; board[3, 6] = counters.e; board[4, 6] = counters.e; board[5, 6] = counters.e; board[6, 6] = counters.e; board[7, 6] = counters.X;
                    board[1, 7] = counters.O; board[2, 7] = counters.X; board[3, 7] = counters.O; board[4, 7] = counters.X; board[5, 7] = counters.O; board[6, 7] = counters.X; board[7, 7] = counters.O;
                    break;
                default:
                    Environment.Exit(99);
                    break;
            }
	    Console.WriteLine("** HWL: Running board {0} "  , cntr);
	    board.DisplayBoard();
            Tuple<int, int> selectedMove = currentPlayer.GetMove(board, currentPlayer.counter, scoreBoard);
            board[selectedMove.Item1, selectedMove.Item2] = currentPlayer.counter;
            Tuple<int, int> centreof3inarow = new Tuple<int, int>(0, 0);

	    cntr++;
            PlayGame(otherPlayer, currentPlayer, ref cntr);
	    return;
        }
        /* 
      ----------------------------------------------------------------------------------------------------------------
      * IsOver -
      --------------------------------------------------------------------------------------------------------------------------
      This boolean dictates wherever if there is win on the current board, and if not the search will continue.
      --------------------------------------------------------------------------------------------------------------------------
      */
        public bool IsOver(GameBoard_TPL<counters> board, Player_TPL currentPlayer)
        {
            if (currentPlayer.Win(board, currentPlayer.counter) || board.IsFull())
                return true;
            return false;
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
        /* 
 ----------------------------------------------------------------------------------------------------------------
 * ErrorSkip -
 --------------------------------------------------------------------------------------------------------------------------
 This method moves the search onto the next board, if errors or problems are found on the current board.
 --------------------------------------------------------------------------------------------------------------------------
 */
        public void ErrorSkip(GameBoard_TPL<counters> board, Player_TPL currentPlayer, Player_TPL otherPlayer)
        {
            nowcount = cntr;
            if (AIPlayer_TPL.error_confirm == 1 & AIPlayer_TPL.positions == new Tuple<int, int>(2, 2))
            {
                ++cntr;
                if (cntr > nowcount)
                {
                    PlayGame(currentPlayer, otherPlayer, ref cntr);
                }
            }
        }
        /* 
 ----------------------------------------------------------------------------------------------------------------
 * ReRun -
 --------------------------------------------------------------------------------------------------------------------------
 Continue the search onto the next board in the switch statement.
 --------------------------------------------------------------------------------------------------------------------------
 */
        public void ReRun(GameBoard_TPL<counters> board, Player_TPL currentPlayer, Player_TPL otherPlayer)
        {
            nowcount = cntr;
            if (IsOver(board, currentPlayer))
            {
                ++cntr;
                if (cntr > nowcount)
                {
                    PlayGame(currentPlayer, otherPlayer, ref cntr);
                }
            }
        }
    }
}
