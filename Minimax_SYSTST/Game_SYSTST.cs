using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minimax_SYSTST
{
    // GAME EXECUTION CLASS
    class Game_SYSTST
    {
        public static int cntr = 23;
        public static int nowcount = 1;
        bool stopMe = false;
      public static GameBoard_SYSTST<counters> board = new GameBoard_SYSTST<counters>(counters.EMPTY);
     public static  GameBoard_SYSTST<int> scoreBoard = new GameBoard_SYSTST<int>(21);
        static counters counter = counters.CROSSES;
        counters us = Flip(counter);

        public Game_SYSTST(Player_SYSTST _xPlayer, Player_SYSTST _oPlayer)
        {
            if (cntr == 1)
            {
                File.WriteAllText(@"C:/Users/Lewis/Desktop/files_150819/ttt_csharp_270719/Minimax_SYSTST/intboards.txt", string.Empty);
                File.WriteAllText(@"C:/Users/Lewis/Desktop/files_150819/ttt_csharp_270719/Minimax_SYSTST/finboards.txt", string.Empty);
                File.WriteAllText(@"C:/Users/Lewis/Desktop/files_150819/ttt_csharp_270719/Minimax_SYSTST/scoreboards.txt", string.Empty);
            }
            PlayGame(_xPlayer, _oPlayer, ref cntr);
        }

        public void PlayGame(Player_SYSTST currentPlayer, Player_SYSTST otherPlayer, ref int cntr)
        {
            // Create new stopwatch.
            Stopwatch stopwatch_minimax = new Stopwatch();
            // Begin timing.
            stopwatch_minimax.Start();
            switch (cntr)
            {
                case 1:
                    // choose win
                    board[1, 1] = counters.CROSSES; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.CROSSES; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 2:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.CROSSES; board[3, 1] = counters.CROSSES; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.CROSSES; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 3:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.CROSSES; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.CROSSES; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 4:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.CROSSES; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.CROSSES; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 5:
                    // try multiple blocks or choose win
                    board[1, 1] = counters.CROSSES; board[2, 1] = counters.CROSSES; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.NOUGHTS; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.NOUGHTS; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.NOUGHTS; board[4, 4] = counters.NOUGHTS; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;

                case 6:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.NOUGHTS; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.CROSSES; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.CROSSES; board[6, 6] = counters.CROSSES; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.CROSSES; board[7, 7] = counters.EMPTY;
                    break;
                case 7:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.NOUGHTS; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.NOUGHTS; board[5, 3] = counters.NOUGHTS; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.NOUGHTS; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 8:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.CROSSES; board[4, 3] = counters.EMPTY; board[5, 3] = counters.NOUGHTS; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.NOUGHTS; board[4, 5] = counters.EMPTY; board[5, 5] = counters.CROSSES; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 9:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.CROSSES; board[6, 1] = counters.EMPTY; board[7, 1] = counters.CROSSES;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.NOUGHTS;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.NOUGHTS;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.NOUGHTS;
                    break;
                case 10:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.CROSSES; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.CROSSES; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.NOUGHTS; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.NOUGHTS; board[5, 4] = counters.NOUGHTS; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.CROSSES; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.CROSSES; board[4, 6] = counters.CROSSES; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.CROSSES; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 11:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.NOUGHTS; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.NOUGHTS; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.CROSSES; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.CROSSES; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 12:
                    board[1, 1] = counters.CROSSES; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.CROSSES; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.CROSSES; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.CROSSES; board[2, 3] = counters.EMPTY; board[3, 3] = counters.NOUGHTS; board[4, 3] = counters.EMPTY; board[5, 3] = counters.NOUGHTS; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.NOUGHTS; board[5, 4] = counters.NOUGHTS; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.NOUGHTS; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.CROSSES; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.CROSSES; board[4, 6] = counters.CROSSES; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.CROSSES; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 13:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.CROSSES; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.CROSSES; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.NOUGHTS; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.CROSSES; board[3, 4] = counters.CROSSES; board[4, 4] = counters.NOUGHTS; board[5, 4] = counters.NOUGHTS; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.CROSSES; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.CROSSES; board[4, 6] = counters.CROSSES; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.CROSSES; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 14:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.CROSSES; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.CROSSES; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.NOUGHTS; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.NOUGHTS; board[5, 4] = counters.NOUGHTS; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.CROSSES; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.CROSSES; board[2, 6] = counters.EMPTY; board[3, 6] = counters.CROSSES; board[4, 6] = counters.CROSSES; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.CROSSES; board[2, 7] = counters.EMPTY; board[3, 7] = counters.CROSSES; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 15:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.CROSSES; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.NOUGHTS; board[2, 7] = counters.CROSSES; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 16:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.NOUGHTS; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.NOUGHTS; board[2, 7] = counters.CROSSES; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 17:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.CROSSES; board[2, 7] = counters.NOUGHTS; board[3, 7] = counters.CROSSES; board[4, 7] = counters.NOUGHTS; board[5, 7] = counters.CROSSES; board[6, 7] = counters.NOUGHTS; board[7, 7] = counters.CROSSES;
                    break;
                case 18:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.NOUGHTS; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.CROSSES; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.CROSSES; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 19:
                    board[1, 1] = counters.CROSSES; board[2, 1] = counters.NOUGHTS; board[3, 1] = counters.NOUGHTS; board[4, 1] = counters.EMPTY; board[5, 1] = counters.NOUGHTS; board[6, 1] = counters.EMPTY; board[7, 1] = counters.CROSSES;
                    board[1, 2] = counters.NOUGHTS; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.CROSSES; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.NOUGHTS; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.CROSSES; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.NOUGHTS; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.CROSSES; board[2, 7] = counters.NOUGHTS; board[3, 7] = counters.CROSSES; board[4, 7] = counters.NOUGHTS; board[5, 7] = counters.CROSSES; board[6, 7] = counters.NOUGHTS; board[7, 7] = counters.CROSSES;
                    break;
                case 20:
                    board[1, 1] = counters.CROSSES; board[2, 1] = counters.NOUGHTS; board[3, 1] = counters.NOUGHTS; board[4, 1] = counters.CROSSES; board[5, 1] = counters.NOUGHTS; board[6, 1] = counters.CROSSES; board[7, 1] = counters.CROSSES;
                    board[1, 2] = counters.NOUGHTS; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.CROSSES;
                    board[1, 3] = counters.CROSSES; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.NOUGHTS;
                    board[1, 4] = counters.NOUGHTS; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.CROSSES;
                    board[1, 5] = counters.CROSSES; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.NOUGHTS;
                    board[1, 6] = counters.NOUGHTS; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.NOUGHTS;
                    board[1, 7] = counters.CROSSES; board[2, 7] = counters.NOUGHTS; board[3, 7] = counters.CROSSES; board[4, 7] = counters.NOUGHTS; board[5, 7] = counters.CROSSES; board[6, 7] = counters.NOUGHTS; board[7, 7] = counters.CROSSES;
                    break;
                case 21:
                    // choose win
                    board[1, 1] = counters.NOUGHTS; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.NOUGHTS; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 22:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.NOUGHTS; board[3, 1] = counters.NOUGHTS; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.NOUGHTS; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 23:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.NOUGHTS; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.NOUGHTS; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 24:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.CROSSES; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.NOUGHTS; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.CROSSES; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.CROSSES; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.NOUGHTS; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 25:
                    // try multiple blocks or choose win
                    board[1, 1] = counters.NOUGHTS; board[2, 1] = counters.NOUGHTS; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.CROSSES; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.CROSSES; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.CROSSES; board[4, 4] = counters.CROSSES; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 26:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.NOUGHTS; board[6, 1] = counters.EMPTY; board[7, 1] = counters.NOUGHTS;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.CROSSES;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.CROSSES;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.CROSSES;
                    break;
                case 27:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.CROSSES; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.NOUGHTS; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.NOUGHTS; board[6, 6] = counters.NOUGHTS; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.NOUGHTS; board[7, 7] = counters.EMPTY;
                    break;
                case 28:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.CROSSES; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.CROSSES; board[5, 3] = counters.CROSSES; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.CROSSES; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 29:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.NOUGHTS; board[4, 3] = counters.EMPTY; board[5, 3] = counters.CROSSES; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.CROSSES; board[4, 5] = counters.EMPTY; board[5, 5] = counters.NOUGHTS; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 30:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.NOUGHTS; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.NOUGHTS; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.CROSSES; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.CROSSES; board[5, 4] = counters.CROSSES; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.NOUGHTS; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.NOUGHTS; board[4, 6] = counters.NOUGHTS; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.NOUGHTS; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 31:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.CROSSES; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.CROSSES; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.NOUGHTS; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.NOUGHTS; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 32:
                    board[1, 1] = counters.NOUGHTS; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.NOUGHTS; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.NOUGHTS; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.NOUGHTS; board[2, 3] = counters.EMPTY; board[3, 3] = counters.CROSSES; board[4, 3] = counters.EMPTY; board[5, 3] = counters.CROSSES; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.CROSSES; board[5, 4] = counters.CROSSES; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.CROSSES; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.NOUGHTS; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.NOUGHTS; board[4, 6] = counters.NOUGHTS; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.NOUGHTS; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 33:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.NOUGHTS; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.NOUGHTS; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.CROSSES; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.NOUGHTS; board[3, 4] = counters.NOUGHTS; board[4, 4] = counters.CROSSES; board[5, 4] = counters.CROSSES; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.NOUGHTS; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.NOUGHTS; board[4, 6] = counters.NOUGHTS; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.EMPTY; board[2, 7] = counters.EMPTY; board[3, 7] = counters.NOUGHTS; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 34:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.NOUGHTS; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.NOUGHTS; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.CROSSES; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.CROSSES; board[5, 4] = counters.CROSSES; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.NOUGHTS; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.NOUGHTS; board[2, 6] = counters.EMPTY; board[3, 6] = counters.NOUGHTS; board[4, 6] = counters.NOUGHTS; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.NOUGHTS; board[2, 7] = counters.EMPTY; board[3, 7] = counters.NOUGHTS; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 35:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.NOUGHTS; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.CROSSES; board[2, 7] = counters.NOUGHTS; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 36:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.CROSSES; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.CROSSES; board[2, 7] = counters.NOUGHTS; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 37:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.EMPTY; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.EMPTY; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.NOUGHTS; board[2, 7] = counters.CROSSES; board[3, 7] = counters.NOUGHTS; board[4, 7] = counters.CROSSES; board[5, 7] = counters.NOUGHTS; board[6, 7] = counters.CROSSES; board[7, 7] = counters.NOUGHTS;
                    break;
                case 38:
                    board[1, 1] = counters.EMPTY; board[2, 1] = counters.EMPTY; board[3, 1] = counters.EMPTY; board[4, 1] = counters.EMPTY; board[5, 1] = counters.EMPTY; board[6, 1] = counters.EMPTY; board[7, 1] = counters.EMPTY;
                    board[1, 2] = counters.EMPTY; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.EMPTY; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.CROSSES; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.NOUGHTS; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.EMPTY; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.NOUGHTS; board[2, 7] = counters.EMPTY; board[3, 7] = counters.EMPTY; board[4, 7] = counters.EMPTY; board[5, 7] = counters.EMPTY; board[6, 7] = counters.EMPTY; board[7, 7] = counters.EMPTY;
                    break;
                case 39:
                    board[1, 1] = counters.NOUGHTS; board[2, 1] = counters.CROSSES; board[3, 1] = counters.CROSSES; board[4, 1] = counters.EMPTY; board[5, 1] = counters.CROSSES; board[6, 1] = counters.EMPTY; board[7, 1] = counters.NOUGHTS;
                    board[1, 2] = counters.CROSSES; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.EMPTY;
                    board[1, 3] = counters.NOUGHTS; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.EMPTY;
                    board[1, 4] = counters.CROSSES; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.EMPTY;
                    board[1, 5] = counters.NOUGHTS; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.EMPTY;
                    board[1, 6] = counters.CROSSES; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.EMPTY;
                    board[1, 7] = counters.NOUGHTS; board[2, 7] = counters.CROSSES; board[3, 7] = counters.NOUGHTS; board[4, 7] = counters.CROSSES; board[5, 7] = counters.NOUGHTS; board[6, 7] = counters.CROSSES; board[7, 7] = counters.NOUGHTS;
                    break;
                case 40:
                    board[1, 1] = counters.NOUGHTS; board[2, 1] = counters.CROSSES; board[3, 1] = counters.CROSSES; board[4, 1] = counters.NOUGHTS; board[5, 1] = counters.CROSSES; board[6, 1] = counters.NOUGHTS; board[7, 1] = counters.NOUGHTS;
                    board[1, 2] = counters.CROSSES; board[2, 2] = counters.EMPTY; board[3, 2] = counters.EMPTY; board[4, 2] = counters.EMPTY; board[5, 2] = counters.EMPTY; board[6, 2] = counters.EMPTY; board[7, 2] = counters.NOUGHTS;
                    board[1, 3] = counters.NOUGHTS; board[2, 3] = counters.EMPTY; board[3, 3] = counters.EMPTY; board[4, 3] = counters.EMPTY; board[5, 3] = counters.EMPTY; board[6, 3] = counters.EMPTY; board[7, 3] = counters.CROSSES;
                    board[1, 4] = counters.CROSSES; board[2, 4] = counters.EMPTY; board[3, 4] = counters.EMPTY; board[4, 4] = counters.EMPTY; board[5, 4] = counters.EMPTY; board[6, 4] = counters.EMPTY; board[7, 4] = counters.NOUGHTS;
                    board[1, 5] = counters.NOUGHTS; board[2, 5] = counters.EMPTY; board[3, 5] = counters.EMPTY; board[4, 5] = counters.EMPTY; board[5, 5] = counters.EMPTY; board[6, 5] = counters.EMPTY; board[7, 5] = counters.CROSSES;
                    board[1, 6] = counters.CROSSES; board[2, 6] = counters.EMPTY; board[3, 6] = counters.EMPTY; board[4, 6] = counters.EMPTY; board[5, 6] = counters.EMPTY; board[6, 6] = counters.EMPTY; board[7, 6] = counters.CROSSES;
                    board[1, 7] = counters.NOUGHTS; board[2, 7] = counters.CROSSES; board[3, 7] = counters.NOUGHTS; board[4, 7] = counters.CROSSES; board[5, 7] = counters.NOUGHTS; board[6, 7] = counters.CROSSES; board[7, 7] = counters.NOUGHTS;
                    break;
                default:
                    Environment.Exit(99);
                    break;
            }
            Tuple<int, int> selectedMove = currentPlayer.GetMove(board, scoreBoard);
            board[selectedMove.Item1, selectedMove.Item2] = currentPlayer.counter;
            Tuple<int, int> centreof3inarow = new Tuple<int, int>(0, 0);

            if (IsOver(board, currentPlayer))
            {
                if (currentPlayer.Win(board, currentPlayer.counter))
                {
                    board.DisplayBoard();


                    if (currentPlayer.GetType() == typeof(AIPlayer_SYSTST))
                    {
                        int score = 0;
                        if (AIPlayer_SYSTST.FindThreeInARow(board, currentPlayer.counter) == true)
                        {

                            score = 1000;


                        }

                        Console.WriteLine("========================================================================================================================"
                          + Environment.NewLine + "GAME OVER! " + Environment.NewLine +
                            "------------------------------------------------------------------------------------------------------------------------" +
                            "Winner: " + currentPlayer.counter
                            + Environment.NewLine + "Score: " + score + Environment.NewLine +
                            "Positions visited: " + AIPlayer_SYSTST.cont + Environment.NewLine +
                            "Coordinations of winning three-in-a-row at: "
                             + Environment.NewLine + "Cell 1: " + AIPlayer_SYSTST.IsLeftOfThree(board, currentPlayer.counter)
                             + Environment.NewLine + "Cell 2: " + AIPlayer_SYSTST.IsCentreOfThree(board, currentPlayer.counter)
                             + Environment.NewLine + "Cell 3: " + AIPlayer_SYSTST.IsRightOfThree(board, currentPlayer.counter));


                    }
                    else
                    {
                        int score = 0;
                        if (AIPlayer_SYSTST.FindThreeInARow(board, otherPlayer.counter) == true)
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
                                 + "Cell 1: " + AIPlayer_SYSTST.IsLeftOfThree(board, otherPlayer.counter) + Environment.NewLine
                                 + "Cell 2: " + AIPlayer_SYSTST.IsCentreOfThree(board, otherPlayer.counter) + Environment.NewLine
                                 + "Cell 3: " + AIPlayer_SYSTST.IsRightOfThree(board, otherPlayer.counter));
                    }
                    // Stop timing.
                    stopwatch_minimax.Stop();

                    if (AIPlayer_SYSTST.error_confirm == 1)
                    {

                        ErrorSkip(board, currentPlayer, otherPlayer);
                    }
                    else
                    {
                        ReRun(board, currentPlayer, otherPlayer);
                    }
                    // Write result.
                    Console.WriteLine("Total elapsed for Minimax over full game execution: " + stopwatch_minimax.Elapsed + Environment.NewLine +
                            "========================================================================================================================");
                  Console.ReadLine();
                    Program.Main();
                }
                Console.WriteLine("The game is a draw.");
                Program.Main();

                if (stopMe)
                {
                    stopwatch_minimax.Stop();
                    Console.WriteLine("**HWL One move made. ");
                    Console.WriteLine("**HWL elapsed time for one move: " + stopwatch_minimax.Elapsed + Environment.NewLine + "-------------------------------------------------------");
                }
                else
                {
                    stopMe = true;
                }
            }
            PlayGame(otherPlayer, currentPlayer, ref cntr);
        }
        public bool IsOver(GameBoard_SYSTST<counters> board, Player_SYSTST currentPlayer)
        {
            if (currentPlayer.Win(board, currentPlayer.counter) || board.IsFull())
                return true;
            return false;
        }

        // WHICH SIDE IS IN PLAY?
        public static counters Flip(counters counter)
        {
            if (counter == counters.NOUGHTS)
            {
                return counters.CROSSES;
            }
            else
            {
                return counters.NOUGHTS;
            }
        }
        public void ErrorSkip(GameBoard_SYSTST<counters> board, Player_SYSTST currentPlayer, Player_SYSTST otherPlayer)
        {  
                nowcount = cntr;
                if (AIPlayer_SYSTST.error_confirm == 1 & AIPlayer_SYSTST.positions == new Tuple<int, int>(2, 2))
                {
                    ++cntr;
                    if (cntr > nowcount)
                    {
                        PlayGame(currentPlayer, otherPlayer, ref cntr);
                }
                }
          }
        public void ReRun(GameBoard_SYSTST<counters> board, Player_SYSTST currentPlayer, Player_SYSTST otherPlayer)
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
            