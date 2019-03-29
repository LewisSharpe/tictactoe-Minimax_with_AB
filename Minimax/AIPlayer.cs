using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Minimax
{
    class AIPlayer : Player
    {
        // PUBLIC DECS
        public int ply = 0;
        public Tuple<int, int> positions = new Tuple<int, int>(0, 0);
        public const int maxPly = 2;
        GameBoard copy;
        public AIPlayer(counters _counter) : base(_counter) { }

        // GET MOVE
        public override Tuple<int, int> GetMove(GameBoard board)
        {
            Tuple<int, Tuple<int, int>, GameBoard> result;
            result = Minimax(board, counter, ply, new Tuple<int, int>(0, 0), true);
            Console.WriteLine("Minimax selects next move at " + "position " + result.Item2 + " for player " + counter + " at depth level of " + maxPly + " in the tree" + ", giving a score of " + result.Item1 + "." );
            Console.ReadLine();
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

        // SPECIFY DIRECTION
        public int GetNumForDir(int startSq, int dir, GameBoard board, counters us)
        {
            int found = 0;
            while (board[startSq, startSq] != counters.BORDER)
            { // while start sq not border sq
                if (board[startSq, startSq] != us)
                {
                    break;
                }
                found++;
                startSq += dir;
            }
            return found;
        }

        // FIND ONE CELL OF SAME SYMBOL ON ITS OWN
        public bool FindOneInARow(GameBoard board, int ourindex, counters us)
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
                    return true;
                }
            }
            return false;
        }

        public bool FindTwoInARow(GameBoard board, counters us)
        {
            // Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == counters.NOUGHTS || board[x, y] == counters.CROSSES);
                    for (int xx = -1; xx <= 1; xx++)
                        for (int yy = -1; yy <= 1; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us && board[x, y] == board[x + xx, y + yy])
                                return true;
                        }
                }
            return false;
        }

        public static bool FindThreeInARow(GameBoard board, counters us)
        {
            //Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == counters.NOUGHTS || board[x, y] == counters.CROSSES);
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us &&
                            board[x, y] == board[x + xx, y + yy] &&
                            board[x, y] == board[x - xx, y - yy])
                            {
                        //        System.Console.WriteLine("Centre of 3-in-a-row: {0} {1}\n", x, y);
                                return true;
                            }
                        }
                }
            return false;
        }

        public int EvalForWin(GameBoard board, int ourindex, counters us)
        {
            // eval if move is win draw or loss
            Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
            if (FindThreeInARow(board, us)) // player win?
                return 1; // player win confirmed
            if (FindThreeInARow(board, us + 1)) // opponent win?
                return -1; // opp win confirmed
            else
                return 0;
        }

        // STATIC EVALUATION FUNCTION
        public int EvalCurrentBoard(GameBoard board, int ourindex, counters us)
        {
            // score decs
            int score;
            bool two_score;
            bool one_score;

            // assign
            score = EvalForWin(board, ourindex, us); // 1 for win, 0 for unknown
            two_score = FindTwoInARow(board, us);
            one_score = FindOneInARow(board, ourindex, us);
            // if one in a row, if two in a row found, etc....
            if (score == 1 || score == -1)
            {
                /*      board.DisplayBoard();
                      Console.Write("three: " + score);
                      Console.ReadLine();
                  */
                return Consts.MAX_SCORE;
            }
            if (two_score)
            {
                /*  board.DisplayBoard();
                  Console.Write("two: " + two_score);
                  Console.ReadLine();
                 */
                return Consts.MAX_SCORE / 10;
            }
            if (one_score)
            {
                /*board.DisplayBoard();
                Console.Write("one: " + one_score);
                Console.ReadLine();
                */
                return Consts.MAX_SCORE / 100;
            }
            else
                return 0;
        }

        // MINIMAX FUNCTION
        public Tuple<int, Tuple<int, int>, GameBoard> Minimax(GameBoard board, counters counter, int ply, Tuple<int, int> positions, bool max)
        {
            // decs
            counters us = counters.NOUGHTS;
            int ourindex = 1;
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board);
            int bestScore = Consts.MIN_SCORE;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(1, 1);
            Tuple<int, int> bestMove = new Tuple<int, int>(1, 1);  // best move with score// THRESHOLD <=============
            List<int> moves = new List<int>();

            // decs for random move 
            Random rnd = new Random();
            int randMoveX = rnd.Next(1, 7); // creates a number between 1 and 49
            int randMoveY = rnd.Next(1, 7); // creates a number between 1 and 49
            Tuple<int, int> randMove = new Tuple<int, int>(randMoveX, randMoveY);

            if (Win(board, counter))
                return new Tuple<int, Tuple<int, int>, GameBoard>(1000, positions, board);
            else if (Win(board, this.otherCounter))
                return new Tuple<int, Tuple<int, int>, GameBoard>(-1000, positions, board);
            else if (availableMoves.Count == 0)
                return new Tuple<int, Tuple<int, int>, GameBoard>(0, positions, board);
            else if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, ourindex, us); // call stat evaluation func - takes board and player and gives score to that player
                return new Tuple<int, Tuple<int, int>, GameBoard>(0, positions, board);
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

            if (board.IsEmpty()) // if board is empty then play random move
            {
                Move = randMove;
                positions = randMove;
                Console.Write("I am a random move");
                return new Tuple<int, Tuple<int, int>, GameBoard>(score, positions, board);
            }
            else // else run Minimax
            {
                copy = board.Clone();
             
                copy.DisplayBoard();
                for (int i = 0; i < availableMoves.Count; i++)
                {
                    // minimax - for everything after the first iteration
                    Move = availableMoves[i]; // current move  
                    Tuple<int, Tuple<int, int>, GameBoard> result = Minimax(copy, Flip(counter), ply + 1, Move, max);  /* swap player */  // RECURSIVE call                    
                    copy[Move.Item1, Move.Item2] = counter;


                    // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready

                    score = -result.Item1;
                    positions = result.Item2;
                    if (max)
                    {
                        if (score > bestScore)
                        {
                            bestMove = Move;
                            bestScore = score;
                        }
                    }
                    else
                    {
                        if (bestScore > score)
                        {
                            bestMove = Move;
                            bestScore = score;
                        }
                    }

                }
                return new Tuple<int, Tuple<int, int>, GameBoard>(bestScore, bestMove, board);
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