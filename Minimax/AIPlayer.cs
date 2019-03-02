using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Minimax
{
    class AIPlayer : Player
    {
        // PUBLIC DECS
        public int ply = 0;
        public int positions = 0;
        public const int maxPly = 2;
        public AIPlayer(counters _counter) : base(_counter) { }

        // GET MOVE
        public override Tuple<int, int> GetMove(GameBoard board)
        {
            Tuple<int, Tuple<int, int>> result;
            result = Minimax(board, counter, ply, new Tuple<int, int>(0, 0));
            return result.Item2;
        }

        // WHICH SIDE IS IN PLAY?
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

        // FIND ONE CELL OF SAME SYMBOL ON ITS OWN
        public int FindOneInARow(GameBoard board, int ourindex, counters us)
        {
            int DirIndex = 0;
            int Dir = 0;
            int oneCount = 1;
            Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
            for (DirIndex = 0; DirIndex < Consts.NO_OF_DIRS; ++DirIndex)
            {
                Dir = Consts.DIRECTIONS[DirIndex];
                oneCount += GetNumForDir(ourindex + Dir, Dir, board, us);
                oneCount += GetNumForDir(ourindex + Dir * -1, Dir * -1, board, us);
                if (oneCount == 1)
                {
                    break;
                }
                oneCount = 1;
            }
            return oneCount;
        }

        // FIND TWO CELLS OF SAME SYMBOL IN A ROW
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

        // FIND THREE CELLS OF SAME SYMBOL IN A ROW
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
                if (threeCount == 3)
                {
                    break;
                }
                threeCount = 1;
            }
            return threeCount;
        }

        // DOES CURRENT MOVE ATTRIBUTE IN WIN
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

        // STATIC EVALUATION FUNCTION
        public int EvalCurrentBoard(GameBoard board, int ourindex, counters us) {
            // decs
            int score;
            int two_score;
            int one_score;
            // assign
            score = EvalForWin(board, ourindex, us); // 1 for win, 0 for unknown
            two_score = FindTwoInARow(board, ourindex, us);
            one_score = FindOneInARow(board, ourindex, us);
            // if one in a row, if two in a row found, etc....
            if (score == 1)
                return Consts.MAX_SCORE;
            if (two_score == 1)
                return Consts.MAX_SCORE / 10;
            if (one_score == 1)
                return Consts.MAX_SCORE / 100;
            return Consts.MAX_SCORE / 100;
        }

        // MINIMAX FUNCTION
        public Tuple<int, Tuple<int, int>> Minimax(GameBoard board, counters counter, int ply, Tuple<int, int> positions)
        {
            counters us = counters.NOUGHTS;
            int ourindex = 1;

            List<Tuple<int, int>> availableMoves = getAvailableMoves(board);
            int bestScore = Consts.MIN_SCORE;
            int score = Consts.MIN_SCORE; // current score of move
            Random rnd = new Random();

            int randMoveX = rnd.Next(1, 7); // creates a number between 1 and 49
            int randMoveY = rnd.Next(1, 7); // creates a number between 1 and 49
            Tuple<int, int> randMove = new Tuple<int, int>(randMoveX, randMoveY);

            Tuple<int, int> Move = new Tuple<int, int>(1, 1);
            Tuple<int, int> bestMove = new Tuple<int, int>(1, 1);  // best move with score// THRESHOLD <=============
            if (Win(board, counter))
                return new Tuple<int, Tuple<int, int>>(10, positions);
            else if (Win(board, this.otherCounter))
                return new Tuple<int, Tuple<int, int>>(-10, positions);
            else if (availableMoves.Count == 0)
                return new Tuple<int, Tuple<int, int>>(0, positions);
            else if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, ourindex, us); // call stat evaluation func - takes board and player and gives score to that player
                return new Tuple<int, Tuple<int, int>>(0, positions);
            }
            else if (ply > 0)
            {
                score = EvalCurrentBoard(board, ourindex, us);  // is current pos a win?
                if (score != 0)
                {
                    { // if draw
                      //     Console.WriteLine(score);  /* return score, stop searching, game won */
                    }
                }
            }
            else if (ply != 0)
            {
                //  Console.WriteLine(bestScore);
            }
            else
            {
                //  Console.WriteLine(bestMove);
            }
            List<int> moves = new List<int>();
            if (board.IsEmpty())
            {
                Move = randMove;
                positions = randMove;
                Console.Write("I am a random move");
                return new Tuple<int, Tuple<int, int>>(score, positions);
            }
            else 
            {
                //Console.WriteLine(randMove);
                for (int i = 0; i < availableMoves.Count; i++)
                {
                    // minimax - for everything after the first iteration
                    Move = availableMoves[i] ; // current move
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
                return new Tuple<int, Tuple<int, int>>(bestScore, bestMove);
            }
        }
         
        // GENERATE LIST OF REMAINING AVAILABLE MOVES
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
