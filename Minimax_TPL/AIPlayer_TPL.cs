using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Minimax_TPL
{
    // AIPlayer_TPL CLASS
    class AIPlayer_TPL : Player_TPL
    {
        // PUBLIC DECS
        public int ply = 0;    // start depth for search (should be 0)
        public int maxPly = 1; // max depth for search
        public int alpha = Consts.MIN_SCORE;
        public int beta = Consts.MAX_SCORE;
        public Tuple<int, int> positions = new Tuple<int, int>(2, 2);
        public static int cont = 0; // counter for number of nodes visited
        public AIPlayer_TPL(counters _counter) : base(_counter) { }

        // GENERATE LIST OF REMAINING AVAILABLE MOVES
        public List<Tuple<int, int>> getAvailableMoves(GameBoard_TPL<counters> board, Tuple<int, int> positions)
        {
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (board[x, y] == counters.EMPTY)
                    {
                        Tuple<int, int> coords = new Tuple<int, int>(x, y);
                        moves.Add(coords);
                    }
            return moves;
        }
    // GET MOVE
    public override Tuple<int, int> GetMove(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard)
        {
            int score = Consts.MIN_SCORE;
            Tuple<int, int> pop = new Tuple<int, int>(0, 0);
            Tuple<int, Tuple<int, int>> bestRes = new Tuple<int, Tuple<int, int>>(score, pop);
            bool mmax = true;
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            // Begin timing.
            stopwatch.Start();
            // Do work
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            Tuple<int, Tuple<int, int>> result;
            
            result = Minimax(board, counter, ply, positions, true, scoreBoard, alpha, beta);
            if (ply > 1)
                result = SeqSearch(board, counter, ply, positions, true, scoreBoard, alpha, beta);
            // Stop timing.
            stopwatch.Stop();
            // Return positions
            return result.Item2;
        }
        // WHICH SIDE IS IN PLAY?
        public counters Flip(counters counter)
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
        // FIND ONE CELL OF SAME SYMBOL IN A ROW
        public bool FindOneInARow(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us)
                                // two in a row in centre should give higher score
                                return true;
                        }
                }
            return false;
        }
        // FIND TWO CELLS OF SAME SYMBOL IN A ROW
        public bool FindTwoInARow(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us && board[x, y] == board[x + xx, y + yy])
                                // two in a row in centre should give higher score
                                return true;
                        }
                }
            return false;
        }
        // IS LEFT OF TWO IN A ROW
        public static Tuple<int, int> IsLeftofTwo(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us && board[x, y] == board[x + xx, y + yy])
                                // two in a row in centre should give higher score
                                return new Tuple<int, int>(x, y);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS RIGHT OF THE TWO IN ROW
        public static Tuple<int, int> IsRightofTwo(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us && board[x, y] == board[x + xx, y + yy])
                                // two in a row in centre should give higher score
                                return new Tuple<int, int>(x + xx, y + yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // FIND HORZI GAP BETWEEN TWO IN A ROW
        public bool FindTwoInARowWithAHorziGap(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us && board[x, y] == board[x + xx + 1, y + yy])
                                // two in a row in centre should give higher score
                                return true;
                        }
                }
            return false;
        }
        // FIND VERTICAL GAP BETWEEN TWO IN A ROW
        public bool FindTwoInARowWithAVerticalGap(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us && board[x, y] == board[x + xx, y + yy - 1])
                                // two in a row in centre should give higher score
                                return true;
                        }
                }
            return false;
        }
        // FIND THREE CELLS OF SAME SYMBOL IN A ROW
        public static bool FindThreeInARow(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
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
        public static Tuple<int, int> IsLeftOfThree(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
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
        public static Tuple<int, int> IsCentreOfThree(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
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
        public static Tuple<int, int> IsRightOfThree(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
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

        // SCORE BOARD SUMMARY PRINT USED FOR MINIMAX MOVES FOR DEBUGGING ONLY
        public Tuple<counters, Tuple<int, int>, Tuple<int, int>, int, int, int> PlyScoringSummary(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard)
        {
            int bestScore = Consts.MIN_SCORE;
            int ply = 2;
            Tuple<int, int> positions = new Tuple<int, int>(2, 2);
            Tuple<int, int> bestMove = new Tuple<int, int>(1, 1);
            Console.WriteLine(Tuple.Create("-----------------------------------------------------------------------------------------------------------------------"
                + "DEBUGGING: SCORING SUMMARY FOR MOVE:" + Environment.NewLine +
                "------------------------------------------------------------------------------------------------------------------------" +
                 "for Player_TPL: " + Flip(counter) + Environment.NewLine,
                 "position: " + positions + Environment.NewLine,
                 "best move: " + bestMove + Environment.NewLine,
                 "best score: " + bestScore + Environment.NewLine,
                 "positions visited: " + cont + Environment.NewLine,
                 "depth level: " + ply + Environment.NewLine +
                 "-----------------------------------------------------------------------------------------------------------------------"));
            return new Tuple<counters, Tuple<int, int>, Tuple<int, int>, int, int, int>(counter, positions, bestMove, bestScore, cont, ply);
        }

        // SCORE BOARD SUMMARY PRINT USED FOR PRIORITY MOVES FOR DEBUGGING ONLY
        public Tuple<Tuple<int, int>, counters, int, Tuple<int, int>, int, int, int> PriorityScoringSummary(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard)
        {
            int bestScore = Consts.MIN_SCORE;
            int ply = 2;
            int score = 10;
            Tuple<int, int> positions = new Tuple<int, int>(2, 2);
            Tuple<int, int> bestMove = new Tuple<int, int>(1, 1);
            Console.Write(Tuple.Create("-----------------------------------------------------------------------------------------------------------------------"
                + "DEBUGGING: SCORING SUMMARY FOR MOVE:" + Environment.NewLine +
                "------------------------------------------------------------------------------------------------------------------------" +
                "Priority Moves function selects position " + positions + Environment.NewLine,
                "for Player_TPL:" + Flip(counter) + Environment.NewLine,
                "score: " + score + Environment.NewLine,
                "best move: " + bestMove + Environment.NewLine,
                "best score: " + bestScore + Environment.NewLine,
                "positions visited: " + cont + Environment.NewLine,
                "depth level: " + ply + Environment.NewLine +
                "-----------------------------------------------------------------------------------------------------------------------"));
            return new Tuple<Tuple<int, int>, counters, int, Tuple<int, int>, int, int, int>(positions, counter, score, bestMove, bestScore, cont, ply);
        }

        // SCORE BOARD SUMMARY PRINT FOR RANDOM MOVES USED FOR DEBUGGING ONLY
        public Tuple<Tuple<int, int>, counters, int, Tuple<int, int>, int, int, int> RandScoringSummary(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard)
        {
            int bestScore = Consts.MIN_SCORE;
            int ply = 2;
            int score = 10;
            Tuple<int, int> positions = new Tuple<int, int>(2, 2);
            counters counter = counters.NOUGHTS;
            Tuple<int, int> bestMove = new Tuple<int, int>(1, 1);
            Tuple.Create(Environment.NewLine + "-----------------------------------------------------------------------------------------------------------------------"
                        + "SELECTED MOVE:" + Environment.NewLine +
                        "------------------------------------------------------------------------------------------------------------------------" +
                       "I AM AN RANDOMLY ASSIGNED MOVE" + Environment.NewLine,
                       "position: " + positions,
                       "for Player_TPL: " + Flip(counter),
                        "score: " + score + Environment.NewLine,
                "best move: " + bestMove + Environment.NewLine,
                "best score: " + bestScore + Environment.NewLine,
                "positions visited: " + cont + Environment.NewLine,
                "depth level: " + ply + Environment.NewLine +
                "------------------------------------------------------------------------------------------------------------=----------");
            return new Tuple<Tuple<int, int>, counters, int, Tuple<int, int>, int, int, int>(positions, counter, score, bestMove, bestScore, cont, ply);
        }

        // IS THERE A WINNING THREE IN A ROW?
        public int EvalForWin(GameBoard_TPL<counters> board, counters us)
        {
            // eval if move is win draw or loss
            if (FindThreeInARow(board, us)) // Player_TPL win?
                return 1000; // Player_TPL win confirmed
            else if (FindThreeInARow(board, us + 1)) // opponent win?
                return -1000; // opp win confirmed
            else if (FindTwoInARow(board, us)) // Player_TPL win?
                return 100; // Player_TPL win confirmed
            else if (FindTwoInARow(board, us + 1)) // opponent win?
                return -100; // opp win confirmed
            if (FindOneInARow(board, us)) // Player_TPL win?
                return 10; // Player_TPL win confirmed
            else if (FindOneInARow(board, us + 1)) // opponent win?
                return -10; // opp win confirmed
            else
                return 23; // dummy value
        }
        // STATIC EVALUATION FUNCTION
        public int EvalCurrentBoard(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard, counters us)
        {
            // score decs
            int score = 0;
        
            // assign
            score = EvalForWin(board, us); // 1 for win, 0 for unknown
           
                return score;
        }

        public Tuple<int, Tuple<int, int>> SeqSearch(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positions, bool mmax, GameBoard_TPL<int> scoreBoard, int alpha, int beta)
        {
            // decs
            counters us = Flip(counter);
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            // create new list of Tuple<int,int>
            List<Tuple<int, Tuple<int, int>>> result_list = new List<Tuple<int, Tuple<int, int>>>();
            int bestScore = mmax ? -1001 : 1001;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(0, 0);
            Tuple<int, int> bestMove = new Tuple<int, int>(1, 1);  // best move with score// THRESHOLD <=============
            // CHECK DEPTH
            if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, scoreBoard, us); // call stat evaluation func - takes board and Player_TPL and gives score to that Player_TPL
                return new Tuple<int, Tuple<int, int>>(score, positions);
            }
            GameBoard_TPL<counters> copy = board.Clone();
            for (int i = 0; i < availableMoves.Count; i++)
            {
                Move = availableMoves[i]; // current move
                                          // cell priority - favour centre and corners
                                          // HWL: where do you actual place the piece for the position in Move? you don't do this here, just pass Move to the call of Minimax below; in the recursive call you then overwrite the input argument with a random move (see comment at start of Minimax; so you are actually not considering Move at all!
                                          // HWL: try placing the piece here, and below just use the score
                copy[Move.Item1, Move.Item2] = counter; // place counter
                                                        // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready

                // list defined in Minimax declarations
                Tuple<int, Tuple<int, int>> result = Minimax(copy, Flip(counter), ply + 1, Move, !mmax, scoreBoard, alpha, beta); /* swap Player_TPL */ // RECURSIVE call  

                // trying to prevent preventing cell overwrite
                copy[Move.Item1, Move.Item2] = counters.EMPTY; /*  counter; */ // HWL: remove counter that was tried in this iteration
                                                                               // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready

                score = -result.Item1; // assign score
                positions = result.Item2; // present position (x,y)

                // assign score to correct cell in score
                scoreBoard[result.Item2.Item1, result.Item2.Item2] = score;

                Object my_object = new Object();
                // if maximising                  
                if (/* true HWL || */ mmax)
                {
                    alpha = score;
                    if (score > bestScore)
                    {
                        lock (my_object)
                        {
                            bestMove = Move;
                            bestScore = score;
                        }
                    }
                    if (alpha > bestScore)
                    {
                        lock (my_object)
                        {
                            bestMove = Move;
                            bestScore = alpha;
                        }
                    }
                }
                // if minimising
                else
                {
                    if (bestScore > score)
                    {
                        lock (my_object)
                        {
                            bestMove = Move;
                            bestScore = score;
                        }
                    }
                    if (beta <= alpha)
                        lock (my_object)
                        {
                            bestScore = alpha;
                        }
                }
                // HWL: summarise the result of having tried Move, print the assoc scoreboard and check that the matching move is the one for the highest score on the board
                Console.WriteLine(mmax.ToString() +
                " **HWL (ply={0}) Trying Move ({4},{5}) gives score {1} and position ({2},{3})  [[so far bestScore={6}, bestMove=({7},{8})",
                      ply, score, result.Item2.Item1, result.Item2.Item2, Move.Item1, Move.Item2,
                      bestScore, bestMove.Item1, bestMove.Item2);
                board.DisplayBoard();
            }
            return new Tuple<int, Tuple<int, int>>(score, positions); // return
        }
        // top-level fct that generates parallelism;
        // currently fixed to 4 parallel tasks
        // each task steps in strides of 4 over all possible moves
        // NOTE: each tasks needs a clone of the board; but in the recursive calls no cloning is needed
        public Tuple<int, Tuple<int, int>> ParSearchWrap(GameBoard_TPL<counters> board, int numTasks, GameBoard_TPL<int> scoreBoard)
        {
            int score = Consts.MIN_SCORE;
            Tuple<int, int> pop = new Tuple<int, int>(0, 0);
            int stride = 1; int id = 1;
            Tuple<int, Tuple<int, int>>[] ress = new Tuple<int, Tuple<int, int>>[4];
            // compute the maximum over all results
            Tuple<int, Tuple<int, int>> res = ress[0]; // , res1, res2, res3, res4;
            Tuple<int, Tuple<int, int>> bestRes = new Tuple<int, Tuple<int, int>>(score, pop);

            // counters?[] board1 = new counters?[board.Length];
            GameBoard_TPL<counters> board1 = board.Clone();
            GameBoard_TPL<counters> board2 = board.Clone();
            GameBoard_TPL<counters> board3 = board.Clone();
            GameBoard_TPL<counters> board4 = board.Clone();

                        // start and synchronise 4 parallel tasks
            Parallel.Invoke(() => { ress[0] = ParSearchWork(board1, counter, ply, positions, true, scoreBoard, stride, id, bestRes); },
                    () => { ress[1] = ParSearchWork(board2, counter, ply, positions, true, scoreBoard, stride, id, bestRes); },
                    () => { ress[2] = ParSearchWork(board3, counter, ply, positions, true, scoreBoard, stride, id, bestRes); },
                    () => { ress[3] = ParSearchWork(board4, counter, ply, positions, true, scoreBoard, stride, id, bestRes); });
        
            for (int j = 1; j < ress.Length; j++)
            {
                res = (ress[j].Item1 > res.Item1) ? ress[j] : res;
            }
            // return overall maximum
            return res;
        }

        public Tuple<int, Tuple<int, int>> ParSearchWork(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positons, bool mmax, GameBoard_TPL<int> scoreBoard, int stride, int id, Tuple<int,Tuple<int,int>> bestRes)
        {
            Tuple<int, Tuple<int, int>> res;
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            int score = Consts.MIN_SCORE; // current score of move
            counters us = counters.CROSSES;
            int cnt = stride, offset = id;
            if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, scoreBoard, us); // call stat evaluation func - takes board and Player_TPL and gives score to that Player_TPL
            }
            for (int i = 0; i < availableMoves.Count; i++)
            {
                    // try values for position i
                    for (int val = 0; val < maxPly; val++)
                    {
                        if (offset == 0 && cnt == 0)
                        {
                            res = SeqSearch(board, counter, ply+1, positions, mmax, scoreBoard, alpha, beta);
                            bestRes = (res.Item1 > bestRes.Item1) ? res : bestRes;
                            cnt = stride;
                        }
                        else
                        {
                            if (offset == 0) { cnt--; } else { offset--; }
                        }
                    }
                }
            return bestRes;
    }
        // MINIMAX FUNCTION
        public Tuple<int, Tuple<int, int>> Minimax(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positions, bool mmax, GameBoard_TPL<int> scoreBoard, int alpha, int beta)
        {
            // decs
            counters us = Flip(counter);
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            // create new list of Tuple<int,int>
            int numTasks = 1;
            int bestScore = mmax ? -1001 : 1001;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(0, 0);
            Tuple<int, int> bestMove = new Tuple<int, int>(1, 1);  // best move with score// THRESHOLD <=============
                                                                   // add assertion here
                                                                   // decs for random move 
            Random rnd = new Random();
            int randMoveX = rnd.Next(1, 7); // creates a number between 1 and 7
            int randMoveY = rnd.Next(1, 7); // creates a number between 1 and 7
            Tuple<int, int> randMove = new Tuple<int, int>(randMoveX, randMoveY);

            // check win
            if (availableMoves.Count == 0)
            {
                return new Tuple<int, Tuple<int, int>>(10, positions);
            }
            if (ply == 0 || ply == 1)
            {
                return ParSearchWrap(board, numTasks, scoreBoard); // return
            }
            else if (ply > 1)
            {
                return SeqSearch(board, counter, ply, positions, true, scoreBoard, alpha, beta);
            }
           
           
            if (Win(board, counter))
            {
                return new Tuple<int, Tuple<int, int>>(1000, positions);
            }
            else if (Win(board, this.otherCounter))
            {
                return new Tuple<int, Tuple<int, int>>(-1000, positions);
            }
           
            cont++; // increment positions visited
            return new Tuple<int, Tuple<int, int>>(bestScore, bestMove); // return
        }
       }
      }


