using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Minimax
{
    // AIPLAYER CLASS
    class AIPlayer : Player
    {
        // PUBLIC DECS
        public int ply = 2;
        public int maxPly = 4; // expand
        GameBoard copy;
        public Tuple<int,int> positions = new Tuple<int, int> (2,2);
        public AIPlayer(counters _counter) : base(_counter) { }

        // GET MOVE
        public override Tuple<int, int> GetMove(GameBoard board)
        {
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            // Begin timing.
            stopwatch.Start();
            // Do work
            Tuple<int, Tuple<int, int>, GameBoard> result;
            result = Minimax(board, counter, ply, positions, true); // 0,0
          //  List<Tuple<int, int>> availableMoves = getAvailableMoves(board, result.Item2);
            board.DisplayBoard();
            // Stop timing.
            stopwatch.Stop();
            // Write result.
            Console.WriteLine("========================================================================================================================" + Environment.NewLine +
            "SELECTED MOVE:" + Environment.NewLine + "------------------------------------------------------------------------------------------------------------------------" + Environment.NewLine +
            "position: " + result.Item2 + Environment.NewLine + "for player: " + counter + Environment.NewLine + "depth level: " + ply + Environment.NewLine + "score: " + result.Item1 + Environment.NewLine + "no. of remaining moves left: " + Environment.NewLine + "elapsed time for move: " + stopwatch.Elapsed);
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
                                // return x and y here, as well as bool value
                                // two in a row in centre should give higher score
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
                                //   System.Console.WriteLine("Centre of 3-in-a-row: {0}{1}{2}\n", x,",",y);
                                return true;
                            }
                        }
                }
            return false;
        }

        // IS CENTRE OF THREE IN A ROW
        public static Tuple<int, int> IsLeftOfThree(GameBoard board, counters us)
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
                                return new Tuple<int, int>(x - xx, y - yy);
                            }
                        }
                }
            return new Tuple<int, int>(0, 0);
        }

        // IS CENTRE OF THREE IN A ROW
        public static Tuple<int, int> IsCentreOfThree(GameBoard board, counters us)
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
                                return new Tuple<int, int>(x, y);
                            }
                        }
                }
            return new Tuple<int, int>(0, 0);
        }

        // IS CENTRE OF THREE IN A ROW
        public static Tuple<int, int> IsRightOfThree(GameBoard board, counters us)
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
                                return new Tuple<int, int>(x + xx, y + yy);
                            }
                        }
                }
            return new Tuple<int, int>(0, 0);
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

            // assign two score
            if (FindTwoInARow(board, us)) // player win?
                two_score = 100; // player win confirmed
            if (FindTwoInARow(board, us + 1)) // opponent win?
                two_score = -100; // opp win confirmed   
            // one score
            if (FindOneInARow(board, ourindex, us)) // player win?
                one_score = 10; // player win confirmed
            if (FindOneInARow(board, ourindex, us + 1)) // opponent win?
                one_score = -10; // opp win confirmed
            /*
            // assign more weight to score with individual cell moves with prominent positioning
            if (copy.IsMiddleEmpty() == true & FindTwoInARow(board, us))
            {
                two_score = 100;
                // player win?
                copy[4, 4] = counter;
                if (copy[4, 4] == counter)
                {
                    return two_score + 25; // player win confirmed
                }
            }
            else if (copy.IsMiddleEmpty() == true & FindTwoInARow(board, us + 1))
            {
                two_score = 100;
                // player win?
                copy[4, 4] = counter;
                if (copy[4, 4] == counter)
                {
                    return -two_score + 25; // opponent win confirmed
                }
            }
            if (copy.IsTopLeftEmpty() == true & FindTwoInARow(board, us))
            {
                two_score = 100;
                // player win?
                copy[1, 1] = counter;
                if (copy[1, 1] == counter)
                {
                    return two_score + 15; // player win confirmed
                }
            }
            else if (copy.IsTopLeftEmpty() == true & FindTwoInARow(board, us + 1))
            {
                two_score = 100;
                // player win?
                copy[1, 1] = counter;
                if (copy[1, 1] == counter)
                {
                    return -two_score + 15; // opponent win confirmed
                }
            }
            if (copy.IsTopRightEmpty() == true & FindTwoInARow(board, us))
            {
                two_score = 100;
                // player win?
                copy[7, 1] = counter;
                if (copy[7, 1] == counter)
                {
                    return two_score + 15; // player win confirmed
                }
            }
            else if (copy.IsTopRightEmpty() == true & FindTwoInARow(board, us + 1))
            {
                two_score = 100;
                // player win?
                copy[7, 1] = counter;
                if (copy[7, 1] == counter)
                {
                    return -two_score + 15; // opponent win confirmed
                }
            }
            if (copy.IsBottomLeftEmpty() == true & FindTwoInARow(board, us))
            {
                two_score = 100;
                // player win?
                copy[1, 7] = counter;
                if (copy[1, 7] == counter)
                {
                    return two_score + 15; // player win confirmed
                }
            }
            else if (copy.IsBottomLeftEmpty() == true & FindTwoInARow(board, us + 1))
            {
                two_score = 100;
                // player win?
                copy[1, 7] = counter;
                if (copy[1, 7] == counter)
                {
                    return -two_score + 15; // opponent win confirmed
                }
            }
            if (copy.IsBottomLeftEmpty() == true & FindTwoInARow(board, us))
            {
                two_score = 100;
                // player win?
                copy[7, 7] = counter;
                if (copy[7, 7] == counter)
                {
                    return two_score + 15;
                }
            }
            else if (copy.IsBottomRightEmpty() == true & FindTwoInARow(board, us + 1))
            {
                two_score = 100;
                // player win?
                copy[7, 7] = counter;
                if (copy[7, 7] == counter)
                {
                    return -two_score + 15; // opponent win confirmed
                }
            }
            */
            // if one in a row, if two in a row found, etc....
            if (score == -1 || score == 1)
            {
                      board.DisplayBoard();
                      Console.Write("three: " + score);
                    //  Console.ReadLine();
                  
                return score * Consts.MAX_SCORE;
            }
            if (two_score != 0)
            {
                   board.DisplayBoard();
                  Console.Write("two: " + two_score);
                  //Console.ReadLine();
                
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
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            int bestScore = Consts.MIN_SCORE;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(0, 0);

            Tuple<int, int> bestMove = new Tuple<int, int>(-11, -11);  // best move with score// THRESHOLD <=============
                                                                       // add assertion here
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
                // make copy original board
                copy = board.Clone(); // make copy board
                copy.DisplayBoard(); // display copy board
                for (int i = 0; i < availableMoves.Count; i++)
                {
                    Move = availableMoves[i]; // current move
                                              // cell priority - favour centre and corners
                    if (copy.IsMiddleEmpty() == true)
                    {
                        List<Tuple<int, int>> nodelist = new List<Tuple<int, int>>();
                        nodelist.Add((new Tuple<int, int>(4, 4)));
                        nodelist.Add((new Tuple<int, int>(3, 4)));
                        nodelist.Add((new Tuple<int, int>(5, 4)));
                        nodelist.Add((new Tuple<int, int>(4, 4)));
                        nodelist.Add((new Tuple<int, int>(3, 3)));
                        nodelist.Add((new Tuple<int, int>(3, 5)));
                        nodelist.Add((new Tuple<int, int>(5, 3)));
                        nodelist.Add((new Tuple<int, int>(5, 5)));
                        copy.DisplayBoard();

                        for (int index = 0; index < (nodelist.Count - 1); index++)
                        {
                            int x = nodelist[i].Item1;
                            int y = nodelist[i].Item2;
                            if (copy[x, y] == counters.EMPTY)
                            {
                                return new Tuple<int, Tuple<int, int>, GameBoard>(score, nodelist[i], board); // return
                            }
                        }
                    }

                    else if (copy.IsTopRightEmpty() == true)
                    {
                        List<Tuple<int, int>> nodelist = new List<Tuple<int, int>>();
                        nodelist.Add((new Tuple<int, int>(6, 1)));
                        nodelist.Add((new Tuple<int, int>(6, 2)));
                        nodelist.Add((new Tuple<int, int>(7, 2)));
                        nodelist.Add((new Tuple<int, int>(4, 4)));
                        nodelist.Add((new Tuple<int, int>(3, 3)));
                        nodelist.Add((new Tuple<int, int>(3, 5)));
                        nodelist.Add((new Tuple<int, int>(5, 3)));
                        nodelist.Add((new Tuple<int, int>(5, 5)));
                        copy.DisplayBoard();

                        for (int index = 0; index < (nodelist.Count - 1); index++)
                        {
                            int x = nodelist[i].Item1;
                            int y = nodelist[i].Item2;
                            if (copy[x, y] == counters.EMPTY)
                            {
                                return new Tuple<int, Tuple<int, int>, GameBoard>(score, nodelist[i], board); // return
                            }
                        }
                    }

                    else if (copy.IsTopLeftEmpty() == true)
                    {
                        List<Tuple<int, int>> nodelist = new List<Tuple<int, int>>();
                        nodelist.Add((new Tuple<int, int>(1, 2)));
                        nodelist.Add((new Tuple<int, int>(2, 2)));
                        nodelist.Add((new Tuple<int, int>(2, 1)));
                        copy.DisplayBoard();

                        for (int index = 0; index < (nodelist.Count - 1); index++)
                        {
                            int x = nodelist[i].Item1;
                            int y = nodelist[i].Item2;
                            if (copy[x, y] == counters.EMPTY)
                            {
                                return new Tuple<int, Tuple<int, int>, GameBoard>(score, nodelist[i], board); // return
                            }
                        }
                    }
                    
                    else if (copy.IsBottomLeftEmpty() == true)
                    {
                        List<Tuple<int, int>> nodelist = new List<Tuple<int, int>>();
                        nodelist.Add((new Tuple<int, int>(1, 6)));
                        nodelist.Add((new Tuple<int, int>(2, 6)));
                        nodelist.Add((new Tuple<int, int>(2, 7)));
                        copy.DisplayBoard();

                        for (int index = 0; index < (nodelist.Count - 1); index++)
                        {
                            int x = nodelist[i].Item1;
                            int y = nodelist[i].Item2;
                            if (copy[x, y] == counters.EMPTY)
                            {
                                return new Tuple<int, Tuple<int, int>, GameBoard>(score, nodelist[i], board); // return
                            }
                        }
                    }
                    else if (copy.IsBottomRightEmpty() == true)
                    {
                        List<Tuple<int, int>> nodelist = new List<Tuple<int, int>>();
                        nodelist.Add((new Tuple<int, int>(7, 6)));
                        nodelist.Add((new Tuple<int, int>(6, 6)));
                        nodelist.Add((new Tuple<int, int>(6, 7)));
                        copy.DisplayBoard();

                        for (int index = 0; index < (nodelist.Count - 1); index++)
                        {
                            int x = nodelist[i].Item1;
                            int y = nodelist[i].Item2;
                            if (copy[x, y] == counters.EMPTY)
                            {
                                return new Tuple<int, Tuple<int, int>, GameBoard>(score, nodelist[i], board); // return
                            }
                        }
                    }
                    else
                    {
                        // ************************************************************************************************
                        // main minimax work
                        // ************************************************************************************************
                        Tuple<int, Tuple<int, int>, GameBoard> result = Minimax(copy, Flip(counter), ply + 1, Move, max);  /* swap player */  // RECURSIVE call  
                                                                                                                                              // trying to prevent preventing cell overwrite

                        copy[Move.Item1, Move.Item2] = counter; // place counter
                                                                // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready
                        score = -result.Item1; // assign score
                        positions = result.Item2; // present position (x,y)

                        // if maximising
                        if (max)
                        {
                            if (score > bestScore)
                            {
                                bestMove = Move;
                                bestScore = score;
                            }
                        }
                        // if minimising
                        else
                        {
                            if (bestScore > score)
                            {
                                bestMove = Move;
                                bestScore = score;
                            }
                        }
                    }
                }
                return new Tuple<int, Tuple<int, int>, GameBoard>(bestScore, bestMove, board); // return
            }
        }

        // GENERATE LIST OF REMAINING AVAILABLE MOVES
        public List<Tuple<int, int>> getAvailableMoves(GameBoard board, Tuple<int, int> positions)
        {
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (board[x, y] == counters.EMPTY)
                    {
                        Tuple<int, int> coords = new Tuple<int, int>(x, y);
                        moves.Add(coords);
                        //moves.Add(positions);
                    }
            return moves;
        }
    }
}


/*
=============================================================================================
Next steps: w/c 15/3/19
=============================================================================================
1 why miss three in a row
 - define three in a row doesnt find that config?
 - the search never reaches this point?
 - test three in a row in isolation
 - XX-XX why does it miss this
2 prevent cell overwriting 
- assertion to prevent this
3 two in a row tweaking - can we build on either side?
 - left and right?
4 add assertion if cell empty, locate error
5 improve scoring - give higher scores to
- connected two in a row 
- two in a row you can build on - are both ends free?
- location is closer to the edge - less valuable - give lower score to this config.
5 unit test the above 
- can we spot three in a row in one move?
- give unit test a predefined board
6 implement counter for nodes - not searching part of tree? missing something in search?
 - search never gets up to this point
 - increment counter value when you declare a move
 - add "ref int n" to Minimax arguments
7 add argument to Minimax for counter of nodes
8 make checks to see if depth level and ply are correct
9 print results to structured file
=============================================================================================
*/
