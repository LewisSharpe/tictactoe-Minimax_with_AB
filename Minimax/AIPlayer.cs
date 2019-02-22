using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Minimax
{
    class AIPlayer : Player
    {
        public int ply = 0;
        public int positions = 0;
        public const int maxPly = 2;
        public AIPlayer(counters _counter) : base(_counter) { }

        public override Tuple<int, int> GetMove(GameBoard board)
        {
            Tuple<int, Tuple<int, int>> result;
            result = Minimax(board, counter, ply, new Tuple<int, int>(0, 0));
            return result.Item2;
        }

        public counters Flip(counters counter)
        {
            if (counter == counters.NOUGHTS)
                return counters.CROSSES;
            else
                return counters.NOUGHTS;
        }

        public int GetNumForDir(int startSq, int dir, GameBoard board, counters us) {
            int found = 0;
            while (board[startSq, startSq] != counters.BORDER) { // while start sq not border sq
                if (board[startSq, startSq] != us) {
                    break;
                }
                found++;
                startSq += dir;
            }
            return found;
        }

        public int FindTwoInARow(GameBoard board, int ourindex, counters us) {
            int DirIndex = 0;
            int Dir = 0;
            int twoCount = 1;
            Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
            for (DirIndex = 0; DirIndex < Consts.NO_OF_DIRS; ++DirIndex) {
                Dir = Consts.DIRECTIONS[DirIndex];
                twoCount += GetNumForDir(ourindex + Dir, Dir, board, us);
                twoCount += GetNumForDir(ourindex + Dir * -1, Dir * -1, board, us);
                if (twoCount == 2) {
                    break;
                }
                twoCount = 1;
            }
            return twoCount;
        }

        public int FindThreeInARow(GameBoard board, int ourindex, counters us)
        {
            int DirIndex = 0;
            int Dir = 0;
            int threeCount = 1;
            Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
            for (DirIndex = 0; DirIndex < Consts.NO_OF_DIRS; ++DirIndex)
            {
                Dir = Consts.DIRECTIONS[DirIndex];
                threeCount += GetNumForDir(ourindex + Dir, Dir, board, us);
                threeCount += GetNumForDir(ourindex + Dir * -1, Dir * -1, board, us);
                if (threeCount == 2)
                {
                    break;
                }
                threeCount = 1;
            }
            return threeCount;
        }

        public int EvalForWin(GameBoard board, int ourindex, counters us)
        {
            // eval if move is win draw or loss
            Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
            if (FindThreeInARow(board, ourindex, us + 1) != 0) // player win?
                return 1; // player win confirmed
            if (FindThreeInARow(board, ourindex, us + 1) != 0) // opponent win?
                return -1; // opp win confirmed
            return 0;
        }

        // static evaluation function; 
        public int EvalCurrentBoard(GameBoard board, int ourindex, counters us) {
            int score;
            score = EvalForWin(board, ourindex, us); // 1 for win, 0 for unknown
            if (score == 1)
                return Consts.MAX_SCORE;
            score = EvalForWin(board, ourindex, us); // 1 for win, 0 for unknown
            if (score == 1)
                return Consts.MAX_SCORE / 10;
            return Consts.MAX_SCORE / 100;
        }

        public Tuple<int, Tuple<int, int>> Minimax(GameBoard board, counters counter, int ply, Tuple<int, int> positions)
        {
            counters us = counters.NOUGHTS;
            int ourindex = 1;
            int board1;
            int len;
            int side;
 
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board);
            int bestScore = Consts.MIN_SCORE;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(1, 1);
            Tuple<int, int> bestMove = new Tuple<int, int>(1, 1);  // best move with score// THRESHOLD <=============
            if (Win(board, counter))
                return new Tuple<int, Tuple<int, int>>(10, positions);
            else if (Win(board, this.otherCounter))
                return new Tuple<int, Tuple<int, int>>(-10, positions);
            else if (availableMoves.Count == 0)
                return new Tuple<int, Tuple<int, int>>(0, positions);
            else if (ply > maxPly)
               return new Tuple<int, Tuple<int, int>>(0, positions);
            //  EvalForWin(board, ourindex, us); // call stat evaluation func - takes board and player and gives score to that player
           // else if (ply > 0)
             //   score = EvalForWin(board, ourindex, us); // is current pos a win?
            //if (score != 0)
            //{ // if draw
              //     Console.WriteLine(score);  /* return score, stop searching, game won */
            //}
            //if (ply != 0)
            //{
                //   Console.WriteLine(bestScore);
            //}
            //else
            //{
                //  Console.WriteLine(bestMove);
            //}

            List<int> moves = new List<int>();
            for (int i = 0; i < availableMoves.Count; i++)
            {
                   Move = availableMoves[i]; ; // current move
                                                // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready
                                                 //board[Move] = counter;              // updates board - sequential only
                    Tuple<int, Tuple<int, int>> result = Minimax(board, Flip(counter), ply + 1, Move);  /* swap player */  // RECURSIVE call
                    score = -result.Item1;
                    positions = result.Item2;
                    if (score > bestScore)
                    {
                        bestMove = Move;
                        bestScore = score;
                    }
            }
                return new Tuple<int, Tuple<int, int>>(bestScore, positions);
            }
                
        public List<Tuple<int, int>> getAvailableMoves(GameBoard board)
        {
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (board[x, y] == counters.EMPTY)
                        moves.Add(new Tuple<int, int>(x, y));
            return moves;
        }
    }
}
