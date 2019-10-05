using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Minimax_TPL
{
    // AIPlayer_TPL CLASS
    class AIPlayer_TPL : Player_TPL
    {
        // PUBLIC DECS
        public int ply = 0;    // start depth for search (should be 0)
        public int maxPly = 3; // max depth for search
        public int alpha = Consts.MIN_SCORE;
        public int beta = Consts.MAX_SCORE;
        public static Tuple<int, int> positions = new Tuple<int, int>(2, 2);
        public static int cont = 0; // counter for number of nodes visited
        public static int error_confirm = 0;
        private static Object thisLock = new Object();
        public static Semaphore threadPool = new Semaphore(5, 8);
        public static Semaphore threadPool1 = new Semaphore(5, 8);
        Tuple<int, Tuple<int, int>>[] ress = new Tuple<int, Tuple<int, int>>[4];
        public AIPlayer_TPL(counters _counter) : base(_counter) { }

        // GENERATE LIST OF REMAINING AVAILABLE MOVES
        public List<Tuple<int, int>> getAvailableMoves(GameBoard_TPL<counters> board, Tuple<int, int> positions)
        {
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (board[x, y] == counters.e)
                    {
                        Tuple<int, int> coords = new Tuple<int, int>(x, y);
                        moves.Add(coords);
                    }
            return moves;
        }
        // GET MOVE
        public override Tuple<int, int> GetMove(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard)
        {
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            int score = Consts.MIN_SCORE;
            Tuple<int, int> pop = new Tuple<int, int>(0, 0);
            Tuple<int, Tuple<int, int>> bestRes = new Tuple<int, Tuple<int, int>>(score, pop);
            bool mmax = true;
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            // Begin timing.
            stopwatch.Start();
            // Do work
            Tuple<int, Tuple<int, int>> result = new Tuple<int, Tuple<int, int>>(0, new Tuple<int, int>(0, 0));
            result = ParallelChoice(board, counter, ply, positions, mmax, scoreBoard, alpha, beta); // return         
            // Stop timing.
            stopwatch.Stop();
            // Return positions
            //}
            return result.Item2;
        }
        // WHICH SIDE IS IN PLAY?
        public counters Flip(counters counter)
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
        public static Tuple<int, int> IsCentreOfThree(GameBoard_TPL<counters> board, counters us)
        {
            int x = 0; int xx = 0; int y = 0; int yy = 0;
            for (x = 1; x <= 7; x++)
                for (y = 1; y <= 7; y++)

                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (xx = 0; xx <= 1; xx++)
                        for (yy = 0; yy <= 1; yy++)


                            if (yy == 0 && xx == 0)
                                continue;
            if (board[x, y] == us &&
            board[x, y] == board[x + xx, y + yy] &&
            board[x, y] == board[x - xx, y - yy])
            {
                return new Tuple<int, int>(x - xx, y - yy);
            }


            return new Tuple<int, int>(x - xx, y - yy);
        }
        // IS CENTRE OF THREE IN A ROW
        public static Tuple<int, int> IsLeftOfThree(GameBoard_TPL<counters> board, counters us)
        {
            int x = 0; int xx = 0; int y = 0; int yy = 0;
            for (x = 1; x <= 7; x++)
                for (y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (xx = 0; xx <= 1; xx++)
                        for (yy = 0; yy <= 1; yy++)
                        {

                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us &&
                            board[x, y] == board[x + xx, y + yy] &&
                            board[x, y] == board[x - xx, y - yy])
                            {
                                return new Tuple<int, int>(x - 1, y + 1);
                            }
                        }
                }
            return new Tuple<int, int>(x - 3, y - 1);
        }
        // IS CENTRE OF THREE IN A ROW
        public static Tuple<int, int> IsRightOfThree(GameBoard_TPL<counters> board, counters us)
        {
            int x = 0; int xx = 0; int y = 0; int yy = 0;
            for (x = 1; x <= 7; x++)
                for (y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (xx = 0; xx <= 1; xx++)
                        for (yy = 0; yy <= 1; yy++)
                        {

                            if (yy == 0 && xx == 0)
                                continue;
                            if (board[x, y] == us &&
                            board[x, y] == board[x + xx, y + yy] &&
                            board[x, y] == board[x - xx, y - yy])
                            {
                                return new Tuple<int, int>(x - 1, y - 3);
                            }
                        }
                }
            return new Tuple<int, int>(x - 1, y - 3);
        }
        // STATIC EVALUATION FUNCTION
        public int EvalCurrentBoard(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard, counters us)
        {
            int score;
            // eval if move is win draw or loss
            if (FindThreeInARow(board, us)) // Player_TPL win?
                return score = 1000; // Player_TPL win confirmed
            else if (FindThreeInARow(board, us + 1)) // opponent win?
                return score = -1000; // opp win confirmed
            else if (FindTwoInARow(board, us)) // Player_TPL win?
                return score = 100; // Player_TPL win confirmed
            else if (FindTwoInARow(board, us + 1)) // opponent win?
                return score = -100; // opp win confirmed
            if (FindOneInARow(board, us)) // Player_TPL win?
                return score = 10; // Player_TPL win confirmed
            else if (FindOneInARow(board, us + 1)) // opponent win?
                return score = -10; // opp win confirmed
            else
                return score = 23; // dummy value
        }

        public Tuple<int, Tuple<int, int>> SeqSearch(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positions, bool mmax, GameBoard_TPL<int> scoreBoard, int alpha, int beta)
        {
            // decs
            counters us = Flip(counter);
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            // create new list of Tuple<int,int>
            int bestScore = mmax ? -1001 : 1001;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(0, 0);
            Tuple<int, int> bestMove = new Tuple<int, int>(0, 0);  // best move with score// THRESHOLD <=============
            GameBoard_TPL<counters> copy = board.Clone();
            // check win
            if (availableMoves.Count == 0)
            {
                return new Tuple<int, Tuple<int, int>>(10, positions);
            }
            for (int i = 0; i < availableMoves.Count; i++)
            {
                Move = availableMoves[i]; // current move
                                          // cell priority - favour centre and corners
                                          // HWL: where do you actual place the piece for the position in Move? you don't do this here, just pass Move to the call of Minimax below; in the recursive call you then overwrite the input argument with a random move (see comment at start of Minimax; so you are actually not considering Move at all!
                                          // HWL: try placing the piece here, and below just use the score
                copy[Move.Item1, Move.Item2] = counter; // place counter
                                                        // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready

                // list defined in Minimax declarations
                Tuple<int, Tuple<int, int>> result = ParallelChoice(copy, Flip(counter), ply + 1, Move, !mmax, scoreBoard, alpha, beta); /* swap Player_TPL */ // RECURSIVE call  

                // trying to prevent preventing cell overwrite
                copy[Move.Item1, Move.Item2] = counters.e; /*  counter; */ // HWL: remove counter that was tried in this iteration
                                                                           // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready

                score = -result.Item1; // assign score
                positions = result.Item2; // present position (x,y)

                // assign score to correct cell in score
                scoreBoard[result.Item2.Item1, result.Item2.Item2] = score;

                // CHECK DEPTH
                if (ply > maxPly)
                {
                    score = EvalCurrentBoard(board, scoreBoard, us); // call stat evaluation func - takes board and Player_TPL and gives score to that Player_TPL
                    return new Tuple<int, Tuple<int, int>>(score, positions);
                }

                if (Game_TPL.cntr >= 40)
                {
                    Environment.Exit(99);
                }

                Object my_object = new Object();
                // if maximising                  
                if (/* true HWL || */ mmax)
                {
                    alpha = score;
                    if (score > bestScore)
                    {
                        lock (my_object)
                        {
                            Move = bestMove;
                            score = bestScore;
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
                            Move = bestMove;
                            score = bestScore;
                        }
                    }
                    if (beta <= alpha)
                        lock (my_object)
                        {
                            bestScore = alpha;
                        }
                }
                if (Win(board, counter))
                {
                    // Create new stopwatch.
                    Stopwatch stopwatch = new Stopwatch();
                    // Begin timing.
                    stopwatch.Start();
                    Game_TPL.cntr++;
                    // write to file
                    var file = @"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_TPL//TPLTST_Report.csv";
                    var date = DateTime.Now.ToShortDateString();
                    var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
                    var csv = new System.Text.StringBuilder();
                    Console.WriteLine("✓ PASS on Board " + Game_TPL.cntr + " : Winning combination found");
                 //   Console.ReadLine();
                    List<string> read_intboard_tocsv = new List<string>();
                    var newLine = "";

                    // write to file
                    string status = "PASS";
                    string reason = "Winning combination found";
                    newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", date, time, status.ToString(), "Board " + int.Parse(Game_TPL.cntr.ToString()), reason.ToString(), result.Item1.ToString(), result.Item2.Item1.ToString(), result.Item2.Item2.ToString(), counter, Game_TPL.board, Game_TPL.scoreBoard, cont, ply, stopwatch.Elapsed, 0, string.Empty, Environment.NewLine);
                    csv.Append(newLine);

                    lock (thisLock)
                    {
                        File.AppendAllText(file, newLine.ToString());
                        board.DisplayIntBoardToFile();
                        board.DisplayFinBoardToFile();


                        scoreBoard.DisplayScoreBoardToFile();
                    }

                    // Stop timing.
                    stopwatch.Stop();
                        return new Tuple<int, Tuple<int, int>>(1000, positions);
                    }
                
                else if (Win(board, this.otherCounter))
                {
                    // Create new stopwatch.
                    Stopwatch stopwatch = new Stopwatch();
                    // Begin timing.
                    stopwatch.Start();
                    Game_TPL.cntr++;
                    // write to file
                    var file = @"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_TPL//TPLTST_Report.csv";
                    var date = DateTime.Now.ToShortDateString();
                    var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
                    var csv = new System.Text.StringBuilder();
                    Console.WriteLine("✓ PASS on Board " + Game_TPL.cntr + " : Winning combination found");
               //     Console.ReadLine();
                    var newLine = "";
                    // write to file
                    string status = "PASS";
                    string reason = "Winning combination found";
                    newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", date, time, status.ToString(), "Board " + int.Parse(Game_TPL.cntr.ToString()), reason.ToString(), result.Item1.ToString(), result.Item2.Item1.ToString(), result.Item2.Item2.ToString(), counter, Game_TPL.board, Game_TPL.scoreBoard, cont, ply, stopwatch.Elapsed, 0, string.Empty, Environment.NewLine);
                    csv.Append(newLine);

                    lock (thisLock)
                    {
                        File.AppendAllText(file, newLine.ToString());
                        board.DisplayIntBoardToFile();
                        board.DisplayFinBoardToFile();


                        scoreBoard.DisplayScoreBoardToFile();
                    }

                        // Stop timing.
                        stopwatch.Stop();
                        return new Tuple<int, Tuple<int, int>>(-1000, positions);
                    
                }
                else if (!Win(board, counter))
                {
                    // Create new stopwatch.
                    Stopwatch stopwatch = new Stopwatch();
                    // Begin timing.
                    stopwatch.Start();
                    // write to file
                    var file = @"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_TPL//TPLTST_Report.csv";
                    var date = DateTime.Now.ToShortDateString();
                    var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
                    var csv = new System.Text.StringBuilder();
                    List<string> read_intboard_tocsv = new List<string>();
                    var newLine = "";
                   
                                // write to file
                                string status = "FAIL";
                                string reason = "Board combination missed";
                                newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", date, time, status.ToString(), "Board " + int.Parse(Game_TPL.cntr.ToString()), reason.ToString(), result.Item1.ToString(), result.Item2.Item1.ToString(), result.Item2.Item2.ToString(), counter, Game_TPL.board, Game_TPL.scoreBoard, cont, ply, stopwatch.Elapsed, 0, string.Empty, Environment.NewLine);
                                csv.Append(newLine);                         
                    lock (thisLock)
                    {
                        File.AppendAllText(file, newLine.ToString());
                        board.DisplayIntBoardToFile();
                        board.DisplayFinBoardToFile();
                        scoreBoard.DisplayScoreBoardToFile();
                        
                    }
                    // Stop timing.
                    stopwatch.Stop();
                }
                else if (!Win(board, this.otherCounter))
                {
                    // Create new stopwatch.
                    Stopwatch stopwatch = new Stopwatch();
                    // Begin timing.
                    stopwatch.Start();
                    // write to file
                    var file = @"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_TPL//TPLTST_Report.csv";
                    var date = DateTime.Now.ToShortDateString();
                    var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
                    var csv = new System.Text.StringBuilder();
                    List<string> read_intboard_tocsv = new List<string>();
                    var newLine = "";
                    
                                // write to file
                                string status = "FAIL";
                                string reason = "Board combination missed";
                                newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", date, time, status.ToString(), "Board " + int.Parse(Game_TPL.cntr.ToString()), reason.ToString(), result.Item1.ToString(), result.Item2.Item1.ToString(), result.Item2.Item2.ToString(), counter, Game_TPL.board, Game_TPL.scoreBoard, cont, ply, stopwatch.Elapsed, 0, string.Empty, Environment.NewLine);
                                csv.Append(newLine);
                           
                    lock (thisLock)
                    {
                        File.AppendAllText(file, newLine.ToString());
                        board.DisplayIntBoardToFile();
                        board.DisplayFinBoardToFile();         
                        scoreBoard.DisplayScoreBoardToFile();
                        
                    }
                    // Stop timing.
                    stopwatch.Stop();
                }
                if (ply == 0)
                {
                    
                    lock (thisLock)
                    {
                        scoreBoard.DisplayScoreBoardToFile();
                    }
                 //   Console.WriteLine("Player move: " + counter + " which, returns: " + result.Item1 + result.Item2);
                }
                /*
      // HWL: summarise the result of having tried Move, print the assoc scoreboard and check that the matching move is the one for the highest score on the board
      Console.WriteLine(mmax.ToString() +
      " **HWL (ply={0}) Trying Move ({4},{5}) gives score {1} and position ({2},{3})  [[so far bestScore={6}, bestMove=({7},{8})",
            ply, score, result.Item2.Item1, result.Item2.Item2, Move.Item1, Move.Item2,
            bestScore, bestMove.Item1, bestMove.Item2);
           */
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

            // compute the maximum over all results
            Tuple<int, Tuple<int, int>> res = new Tuple<int, Tuple<int, int>>(score, pop); ; // , res1, res2, res3, res4;
            Tuple<int, Tuple<int, int>> bestRes = new Tuple<int, Tuple<int, int>>(score, pop);

            // counters?[] board1 = new counters?[board.Length];
            GameBoard_TPL<counters> board1 = board.Clone();
            GameBoard_TPL<counters> board2 = board.Clone();
            GameBoard_TPL<counters> board3 = board.Clone();
            GameBoard_TPL<counters> board4 = board.Clone();

            // start and synchronise 4 parallel tasks
            Parallel.Invoke(() => { ress[0] = ParSearchWork(board1, Flip(counter), ply, positions, true, scoreBoard, stride, id, bestRes, 1); },
                    () => { ress[1] = ParSearchWork(board2, Flip(counter), ply, positions, true, scoreBoard, stride, id, bestRes, 2); },
                    () => { ress[2] = ParSearchWork(board3, Flip(counter), ply, positions, true, scoreBoard, stride, id, bestRes, 3); },
                    () => { ress[3] = ParSearchWork(board4, Flip(counter), ply, positions, true, scoreBoard, stride, id, bestRes, 4); });

            /*
            //Write to a file
            using (StreamWriter writer = new StreamWriter(@"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_TPL//print_val.txt"))
            {
                writer.WriteLine("t");
                writer.WriteLine("res0:" + ress[0] + Environment.NewLine +
                    "res1:" + ress[1] + Environment.NewLine +
                    "res2:" + ress[2] + Environment.NewLine +
                    "res3:" + ress[3] + Environment.NewLine);
            }
            */

            for (int j = 1; j < ress.Length; j++)
            {
                res = (ress[j].Item1 > res.Item1) ? ress[j] : res;
            }
            // return overall maximum
            return res;
        }
    
        public Tuple<int, Tuple<int, int>> ParSearchWork(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positons, bool mmax, GameBoard_TPL<int> scoreBoard, int stride, int id, Tuple<int,Tuple<int,int>> bestRes, int thread_no)
        {
            Tuple<int, Tuple<int, int>> res;
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            int score = Consts.MIN_SCORE; // current score of move
            counters us = counters.X;
            stride = 1;
            int cnt = stride, offset = id;
            if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, scoreBoard, us); // call stat evaluation func - takes board and Player_TPL and gives score to that Player_TPL
            }
            for (int i = 0; i < availableMoves.Count; i++)
            {
                stride++;
                // try values for position i
                for (int val = 0; val < maxPly; val++)
                    {
                        if (offset == 0 && cnt == 0)
                        {
                        res = SeqSearch(board, counter, ply, positions, true, scoreBoard, alpha, beta);
                        if (stride == 1 && res == ress[0])
                        {
                            res = new Tuple<int, Tuple<int, int>>(score, availableMoves[availableMoves.Count - i]);
                        }
                        if (stride == 2 && res == ress[1])
                        {
                            res = new Tuple<int, Tuple<int, int>>(score, availableMoves[availableMoves.Count - i]);
                        }
                        if (stride == 3 && res == ress[2])
                        {
                            res = new Tuple<int, Tuple<int, int>>(score, availableMoves[availableMoves.Count - 1]);
                        }
                        if (stride == 4 && res == ress[3])
                        {
                            res = new Tuple<int, Tuple<int, int>>(score, availableMoves[availableMoves.Count - 1]);
                        }
                        bestRes = (res.Item1 > bestRes.Item1) ? res : bestRes;           
                        cnt = stride;
                        }
                        else
                        {
                            if (offset == 0) { cnt--; } else { offset--; }
                        }
                    cont++;
                }
                if (ply == 0)
                {
                    Console.WriteLine("board " + Game_TPL.cntr + " processed by thread id: " + thread_no + " :");
                    board.DisplayBoard();
                    // write to file
                    var file = @"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_TPL//TPLTST_Report.csv";
                    var date = DateTime.Now.ToShortDateString();
                    var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
                    var csv = new System.Text.StringBuilder();
                    var title = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", "DATE", "TIME", "RESULT", "BOARD NO", "REASON", "SCORE", "X", "Y", "SIDE", "FIN BOARD", "SCORE BOARD", "POSITIONS VISTED", "DEPTH", "TIME ELAPSED", "THREAD NO.", "INT BOARD", Environment.NewLine);
                    csv.Append(title);
                    lock (thisLock)
                    {
                        File.AppendAllText(file, title.ToString());
                    }
                }
       
            }         
            return bestRes;
    }
        // MINIMAX FUNCTION
        public Tuple<int, Tuple<int, int>> ParallelChoice(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positions, bool mmax, GameBoard_TPL<int> scoreBoard, int alpha, int beta)
        {
            // decs
            counters us = Flip(counter);
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            // create new list of Tuple<int,int>
            int numTasks = 1;
            int bestScore = mmax ? -1001 : 1001;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(0, 0);
            Tuple<int, int> bestMove = new Tuple<int, int>(0, 0);  // best move with score// THRESHOLD <=============
                                                                   // add assertion here
                                                                   // decs for random move 
            Random rnd = new Random();
            int randMoveX = rnd.Next(1, 7); // creates a number between 1 and 7
            int randMoveY = rnd.Next(1, 7); // creates a number between 1 and 7
            Tuple<int, int> randMove = new Tuple<int, int>(randMoveX, randMoveY);

            if (ply == 0 || ply == 1)
            {
                return ParSearchWrap(board, numTasks, scoreBoard); // return
            }
            else if (ply > 1)
            {
                return SeqSearch(board, Flip(counter), ply, positions, true, scoreBoard, alpha, beta);
            }
            return new Tuple<int, Tuple<int, int>>(bestScore, bestMove);
        }
    }
}
/*
 ====================================================
 ---------------------------------------------------
 To do list for w/c 30.09.19
 ---------------------------------------------------
 - stride the search
 - produce list of all tried moves
 - no need for thread pool at this stage

-----------------------------------------------------
=====================================================
-----------------------------------------------------
 Progress summary- LS
 27.09.19
-----------------------------------------------------
  *   progress:
------------------------------------------------------
    - implementation containing use of threads and thread pool - however, problem - too many threads
    - printout output of all threads to CSV file
    - print boards to CSV cell function
    - modifications to SEQ version
    - performance tuning on par sort example in prep for SEQ and TPL versions
------------------------------------------------------
  *  issues still existing:
------------------------------------------------------
     use of no of threads - too many at the moment
     display boards to CSV - function implemented, but a few issues
     selecting one move all moves considered by all threads together
======================================================
    -------------------------------------------------------------------------------------------------------------------------
 -  Sample code for debugging later on:
    -------------------------------------------------------------------------------------------------------------------------
                        Tuple<int, int> Move = new Tuple<int, int>(0, 0);
                        Move = availableMoves[i]; // current move
                                                  // cell priority - favour centre and corners
                                                  // HWL: where do you actual place the piece for the position in Move? you don't do this here, just pass Move to the call of Minimax below; in the recursive call you then overwrite the input argument with a random move (see comment at start of Minimax; so you are actually not considering Move at all!
                                                  // HWL: try placing the piece here, and below just use the score
                        res = board[Move.Item1, Move.Item2] = counter; // place counter
   ---------------------------------------------------------------------------------------------------------------------------
   ===========================================================================================================================
   ---------------------------------------------------------------------------------------------------------------------------
   Psuedocode for attempt of strided search in Par Search Worker
   ---------------------------------------------------------------------------------------------------------------------------
   if current move has finished then
   stride++

   for loop
   if stride = 1 and res[0]
   next available move using ress 1
   if stride = 2 and res[1]
   next available move using ress 2
   if stride = 3 and res[2]
   next available move using ress 2
   if stride = 4 and res[3]
   next available move using ress 3
   if stride = 4
   reset stride back to 1
   end for loop
   ---------------------------------------------------------------------------------------------------------------------------
   ===========================================================================================================================
   
     */


