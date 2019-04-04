using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Minimax
{
    class AIPlayer : Player
    {
        // PUBLIC DECS
        public int ply;
        public Tuple<int, int> positions = new Tuple<int, int>(0, 0);
        public int maxPly = 2;
        GameBoard copy;
        public AIPlayer(counters _counter) : base(_counter) { }

        // GET MOVE
        public override Tuple<int, int> GetMove(GameBoard board)
        {
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            // Begin timing.
            stopwatch.Start();
            // Do work
            Random rnd = new Random();
            int randMoveX = rnd.Next(1, 7); // creates a number between 1 and 49
            int randMoveY = rnd.Next(1, 7); // creates a number between 1 and 49
            Tuple<int, Tuple<int, int>, GameBoard> result;
            result = Minimax(board, counter, ply, new Tuple<int, int>(randMoveX,randMoveY), true); // 0,0
            board.DisplayBoard();
            // Stop timing.
            stopwatch.Stop();
            // Write result.
            Console.WriteLine("========================================================================================================================" + Environment.NewLine +
            "SELECTED MOVE:" + Environment.NewLine + "------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine +
            "position: " + result.Item2 + Environment.NewLine + "for player: " + counter + Environment.NewLine + "depth level: " + ply + Environment.NewLine + "score: " + result.Item1 + Environment.NewLine + "elapsed time for move:" + stopwatch.Elapsed);
            Console.WriteLine("========================================================================================================================");
            Console.ReadLine();
            // Return positions
            return result.Item2;
        }

        // WHICH SIDE IS IN PLAY?
        public counters Flip(counters counter)
        {
            if (counter == counters.NOUGHTS)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                return counters.CROSSES;
            }
            else
            {
                Console.BackgroundColor
                   = ConsoleColor.White;
                return counters.NOUGHTS;
            }
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

        // FIND TWO CELLS OF SAME SYMBOL IN A ROW
        public bool FindTwoInARow(GameBoard board, counters us)
        {
            // Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == counters.NOUGHTS || board[x, y] == counters.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us && board[x, y] == board[x + xx, y + yy])
                                return true;
                        }
                }
            return false;
        }

        // FIND THREE CELLS OF SAME SYMBOL IN A ROW
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

        // IS THERE A WINNING THREE IN A ROW?
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
            int two_score = 0;
            int one_score = 0;

            // assign
            score = EvalForWin(board, ourindex, us); // 1 for win, 0 for unknown
                                                    
            // two score
            if (FindTwoInARow(board, us)) // player win?
                two_score = 100; // player win confirmed
            if (FindTwoInARow(board, us + 1)) // opponent win?
                two_score = -100; // opp win confirmed
            // one score
            if (FindOneInARow(board, ourindex, us)) // player win?
                one_score = 10; // player win confirmed
            if (FindOneInARow(board, ourindex, us + 1)) // opponent win?
                one_score = -10; // opp win confirmed
            // if one in a row, if two in a row found, etc....
            if (score == -1 || score == 1)
            {
                /*      board.DisplayBoard();
                      Console.Write("three: " + score);
                      Console.ReadLine();
                  */
                return score * Consts.MAX_SCORE;
            }
            if (two_score != 0)
            {
                /*   board.DisplayBoard();
                  Console.Write("two: " + two_score);
                  Console.ReadLine();
                */
                return two_score;
            }
            if (one_score != 0)
            {
                /*board.DisplayBoard();
                Console.Write("one: " + one_score);
                Console.ReadLine();
                */
                return one_score;
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
            Tuple<int, int> Move = new Tuple<int, int>(0, 0);
            Tuple<int, int> bestMove = new Tuple<int, int>(-11,-11);  // best move with score// THRESHOLD <=============
            // decs for random move 
            Random rnd = new Random();
            int randMoveX = rnd.Next(1, 7); // creates a number between 1 and 49
            int randMoveY = rnd.Next(1, 7); // creates a number between 1 and 49
            Tuple<int, int> randMove = new Tuple<int, int>(randMoveX, randMoveY);
            // check win
            if (Win(board, counter))
                return new Tuple<int, Tuple<int, int>, GameBoard>(1000, positions, board);
            else if (Win(board, this.otherCounter))
                return new Tuple<int, Tuple<int, int>, GameBoard>(-1000, positions, board);
            else if (availableMoves.Count == 0)
                return new Tuple<int, Tuple<int, int>, GameBoard>(0, positions, board);
            else if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, ourindex, us); // call stat evaluation func - takes board and player and gives score to that player
                return new Tuple<int, Tuple<int, int>, GameBoard>(score, positions, board);
            }
            else if (ply > 0)
            {
                score = EvalCurrentBoard(board, ourindex, us);  // is current pos a win?
            }
            // place random move
            if (board.IsEmpty()) // if board is empty then play random move
            {
                Move = randMove;
                positions = randMove;
                board.DisplayBoard();
                Console.Write(Environment.NewLine + "I am a random move" + Environment.NewLine);
                return new Tuple<int, Tuple<int, int>, GameBoard>(score, positions, board);
            }
            // else run Minimax
            else
            {
                copy = board.Clone(); // make copy board
                copy.DisplayBoard(); // display copy board
                for (int i = 0; i < availableMoves.Count; i++)
                {
                    Move = availableMoves[i]; // current move 
                    Tuple<int, Tuple<int, int>, GameBoard> result = Minimax(copy, Flip(counter), ply + 1, Move, max);  /* swap player */  // RECURSIVE call    
                    copy[Move.Item1, Move.Item2] = counter; // place counter
                                                            // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready
                    score = -result.Item1; // assign score
                    positions = result.Item2; // present position (x,y)
                    if (max) // if maximising
                    {
                        if (score > bestScore)
                        {
                            bestMove = Move;
                            bestScore = score;
                        }
                    }
                    else // if minimising
                    {
                        if (bestScore > score)
                        {
                            bestMove = Move;
                            bestScore = score;
                        }
                    }
                }
                return new Tuple<int, Tuple<int, int>, GameBoard>(bestScore, bestMove, board); // return
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

/*
 Next steps: 
 - address cell overwrite
 - fix ply
 - fix two in a row in cases like this X-X
 - print results to structured file
 - sort display board for one player
 */