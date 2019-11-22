using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Minimax_TPL
{
/* 
----------------------------------------------------------------------------------------------------------------
 * AIPlayer_TPL.CS -
--------------------------------------------------------------------------------------------------------------------------
Class controls all behaviour from all AIPlayer_TPL instances. Class inherits behaviour from Player_TPL.
--------------------------------------------------------------------------------------------------------------------------
*/
    class AIPlayer_TPL : Player_TPL
    {
       // BOARD DIMENSIONS 
        public int COORD_X = 3; // x coord
        public int COORD_Y = 3; // y coord
        // BOARD ADJUSTMENT VARIABLES
        int SEGM_BOARD = 1;  // SEGMENT BOARD TO 3X3 COUNTER - 0 for off, 1 for yes, blanks out non active cells in 3x3 on 7x7 with 'N'
        int EXECPRINT_SCOREBOARD_ON = 0; // 1 on, 0 off - TURN SCORE BOARD PRINT ON CONSOLE ON AND OFF
        int EXECPRINT_GAMEBOARD_ON = 1;  // 1 on, 0 off - TURN GAME BOARD PRINT ON CONSOLE ON AND OFF
        // PUBLIC DECS
        public static int ply = 0;    // start depth for search (should be 0)
        public const int maxPly = 1; // max depth for search
        public int alpha = Consts.MIN_SCORE; // set alpha to -1001
        public int beta = Consts.MAX_SCORE; // set beta to 1001
        public static Tuple<int, int> positions = new Tuple<int, int>(2, 2);
        public static int cont = 0; // counter for number of nodes visited
        public static int error_confirm = 0; // if positive moves to next board in case
	    public const int stride = 4;  // fixed stride interation; never changes
        private static Object thisLock = new Object(); // lock to protect Move and score from accidential updates
        Tuple<int, Tuple<int, int>>[] ress = new Tuple<int, Tuple<int, int>>[4]; // set array for 4 calls of ParSearchWork
        public static int thread_no_track = 0; // thread track int variable
        Tuple<int, Tuple<int, int>> result; // return Tuple which returns score and position of Move from Minimax
        Stopwatch stopwatch = new Stopwatch(); // stopwatch - for timing of moves

        public AIPlayer_TPL(counters _counter) : base(_counter) { }
        /* 
         ----------------------------------------------------------------------------------------------------------------
         * getAvailableMoves -
         --------------------------------------------------------------------------------------------------------------------------
          A list which generates, hold and updates a list of remaining position coordinates on a fixed board, 
          with each position represented in a Tuple construct.
         --------------------------------------------------------------------------------------------------------------------------
         */
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
        /* 
          ----------------------------------------------------------------------------------------------------------------
          * getAvailableSegmentedMoves -
          --------------------------------------------------------------------------------------------------------------------------
           A list which generates, hold and updates a list of remaining position coordinates on a variable board size ranging from 1 to 7, 
           with each position represented in a Tuple construct.
          --------------------------------------------------------------------------------------------------------------------------
          */
        public List<Tuple<int, int>> getAvailableSegmentedMoves(GameBoard_TPL<counters> board, Tuple<int, int> positions)
        {
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
            for (int x = 1; x <= COORD_X; x++)
                for (int y = 1; y <= COORD_Y; y++)
                    if (board[x, y] == counters.e)
                    {
                        Tuple<int, int> coords = new Tuple<int, int>(x, y);
                        moves.Add(coords);
                    }
            return moves;
        }
        /* 
           ----------------------------------------------------------------------------------------------------------------
           * getMove -
           --------------------------------------------------------------------------------------------------------------------------
            Method begins execution of next move and returns the result of it.
           --------------------------------------------------------------------------------------------------------------------------
           */
        public override Tuple<int, int> GetMove(GameBoard_TPL<counters> board, counters counter, GameBoard_TPL<int> scoreBoard)
        {
            int score = Consts.MIN_SCORE;
            bool mmax = true;
            // Begin timing.
            stopwatch.Start();
            // Do work
            Tuple<int, Tuple<int, int>> result = new Tuple<int, Tuple<int, int>>(score, positions);
            result = ParallelChoice(board, counter, ply, positions, mmax, scoreBoard, alpha, beta); // return         
            // Stop timing.
            stopwatch.Stop();
            // Return positions
            //}
            return result.Item2;
        }
        /* 
         ----------------------------------------------------------------------------------------------------------------
         * Flip -
         --------------------------------------------------------------------------------------------------------------------------
         Construct flips the counter have each turn of play.
         --------------------------------------------------------------------------------------------------------------------------
         */
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
        /* 
      ----------------------------------------------------------------------------------------------------------------
      * PrintCSVHeadRow -
      --------------------------------------------------------------------------------------------------------------------------
      Returns passed move to CSV file.
      --------------------------------------------------------------------------------------------------------------------------
      */
        public void PrintCSVHeadRow()
        {
            Object thisCSVLock = new Object();
            /* HWL: omit for now */
            // write to file
            // var file = @"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_TPL//TPLTST_Report.csv";
            var file = "data/TPLTST_Report.csv";
            var date = DateTime.Now.ToShortDateString();
            var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
            var csv = new System.Text.StringBuilder();
            var title = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", "DATE", "TIME", "RESULT", "BOARD NO", "REASON", "SCORE", "X", "Y", "SIDE", "POSITIONS VISTED", "DEPTH", "TIME ELAPSED", "THREAD NO.", "INT BOARD", "FIN BOARD", "SCORE BOARD", Environment.NewLine);
            csv.Append(title);
            lock (thisCSVLock)
            {
                File.AppendAllText(file, title.ToString());
            }
        }
        /* 
    ----------------------------------------------------------------------------------------------------------------
    * PrintCSVPassRow -
    --------------------------------------------------------------------------------------------------------------------------
    Returns failed move to CSV file
    --------------------------------------------------------------------------------------------------------------------------
    */
        public void PrintCSVPassRow(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard)
        {
            string intboard_COPY = File.ReadAllText("data/intboards.txt");
            string finboard_COPY = File.ReadAllText("data/finboards.txt");
            string scoreboard_COPY = File.ReadAllText("data/scoreboards.txt");
            var newLine = "";
            Object thisCSVLock = new Object();
            var file = "data/TPLTST_Report.csv";
            var date = DateTime.Now.ToShortDateString();
            var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
            var csv = new System.Text.StringBuilder();
            string status = "PASS";
            string reason = "Winning combination found";
            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", date, time, status.ToString(), "Board " + int.Parse(Game_TPL.cntr.ToString()), reason.ToString(), result.Item1.ToString(), result.Item2.Item1.ToString(), result.Item2.Item2.ToString(), counter, cont, ply, stopwatch.Elapsed, thread_no_track, intboard_COPY, finboard_COPY, scoreboard_COPY, Environment.NewLine);
            csv.Append(newLine);
            lock (thisCSVLock)
            {
                File.AppendAllText(file, newLine.ToString());
                board.DisplayIntBoardToFile();
                board.DisplayFinBoardToFile();
            }
        }
        /* 
            ----------------------------------------------------------------------------------------------------------------
            * PrintCSVFailRow -
            --------------------------------------------------------------------------------------------------------------------------
            Prints head title row to CSV file.
            --------------------------------------------------------------------------------------------------------------------------
            */
        public void PrintCSVFailRow(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard)
        {
            string intboard_COPY = File.ReadAllText("data/intboards.txt");
            string finboard_COPY = File.ReadAllText("data/finboards.txt");
            string scoreboard_COPY = File.ReadAllText("data/scoreboards.txt");
            // #ifdef DEBUG
            Object thisCSVLock = new Object();
            var file = "data//TPLTST_Report.csv";
            var date = DateTime.Now.ToShortDateString();
            var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
            var csv = new System.Text.StringBuilder();
            List<string> read_intboard_tocsv = new List<string>();
            var newLine = "";
            /* HWL: disabled for now; path invalid */
            // write to file
            string status = "FAIL";
            string reason = "Board combination missed";
            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", date, time, status.ToString(), "Board " + int.Parse(Game_TPL.cntr.ToString()), reason.ToString(), result.Item1.ToString(), result.Item2.Item1.ToString(), result.Item2.Item2.ToString(), counter, cont, ply, stopwatch.Elapsed, thread_no_track, intboard_COPY, finboard_COPY, scoreboard_COPY, Environment.NewLine);
            csv.Append(newLine);
            lock (thisCSVLock)
            {
                File.AppendAllText(file, newLine.ToString());
                board.DisplayIntBoardToFile();
                board.DisplayFinBoardToFile();
            }
            // #endif
        }
        /* 
         ----------------------------------------------------------------------------------------------------------------
         * FindOneInARow -
         --------------------------------------------------------------------------------------------------------------------------
          A bool which returns true or false of the presence of any symbol placed on the board
         --------------------------------------------------------------------------------------------------------------------------
         */
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
        /* 
 ----------------------------------------------------------------------------------------------------------------
 * FindTwoInARow -
 --------------------------------------------------------------------------------------------------------------------------
  A bool which returns true or false of the presence of a two counters of the same symbol placed side by side on the board
 --------------------------------------------------------------------------------------------------------------------------
 */
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
        /*
 ----------------------------------------------------------------------------------------------------------------
  FindTwoInARow -
 --------------------------------------------------------------------------------------------------------------------------
  A bool which returns true or false of the presence of a three counters of the same symbol placed side by side on the board
 --------------------------------------------------------------------------------------------------------------------------
 */
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
        /*
        ----------------------------------------------------------------------------------------------------------------
         EvalCurrentBoard -
        --------------------------------------------------------------------------------------------------------------------------
         A integer variable methods which returns the score of the positions on the current board based on satisfaction of the above methods, with
         greater scores assigned to combinational placement of counters of the same symbol.
        --------------------------------------------------------------------------------------------------------------------------
        */
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
        /*
----------------------------------------------------------------------------------------------------------------
 SeqSearch (Minimax) -
--------------------------------------------------------------------------------------------------------------------------
This method executes the Minimax search on the current board being vi and returns the bestScore and bestMove found in the search in a 
Tuple<int,int> construct.
--------------------------------------------------------------------------------------------------------------------------
*/
        public Tuple<int, Tuple<int, int>> SeqSearch(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positions, bool mmax, GameBoard_TPL<int> scoreBoard, int alpha, int beta)
{
            // decs
	    counters us = Flip(counter); // HWL: why flip counter here? should only be flipped when calling SeqSearch recursively
            List<Tuple<int, int>> availableMoves = new List<Tuple<int, int>>();
            if (SEGM_BOARD == 0)
            {
               availableMoves = getAvailableMoves(board, positions);
            }
            else if (SEGM_BOARD == 1)
            {
                availableMoves = getAvailableSegmentedMoves(board, positions); // HWL: <==== change to only return indices betwee 1-3
            }
                // create new list of Tuple<int,int>
            int bestScore = mmax ? -1001 : 1001;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(0, 0);
            Tuple<int, int> bestMove = new Tuple<int, int>(8, 1);  // best move with score// THRESHOLD <=============
            GameBoard_TPL<counters> copy = board.Clone();
            GameBoard_TPL<counters> input_board = board.Clone(); // HWL: for DEBUGGING onlu
            // check win
            if (availableMoves.Count == 0)
            {
                return new Tuple<int, Tuple<int, int>>(10, positions);
            }
	    // CHECK DEPTH: if deeper than maxPly, don't search further just return the current score
	    if (ply > maxPly) {
		score = EvalCurrentBoard(board, scoreBoard, counter /* us */ ); // call stat evaluation func - takes board and Player_TPL and gives score to that Player_TPL
		// Console.WriteLine("== HWL: static eval: {0}", score);
		return new Tuple<int, Tuple<int, int>>(score, positions);
	    }
	    int end = (ply==0) || (ply == 1) ? 1 : availableMoves.Count;
            for (int i = 0; i < end /* availableMoves.Count */; i++)
	      { // for
                Move = availableMoves[i]; // current move
                                          // cell priority - favour centre and corners
                                          // HWL: where do you actual place the piece for the position in Move? you don't do this here, just pass Move to the call of Minimax below; in the recursive call you then overwrite the input argument with a random move (see comment at start of Minimax; so you are actually not considering Move at all!
                                          // HWL: try placing the piece here, and below just use the score
                copy[Move.Item1, Move.Item2] = counter; // place counter
                                                        // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready

                // HWL: move the check for Win in here <======

                // list defined in Minimax declarations
                // HWL: in the initial parallel version you should NOT generate parallelism recursively; the only place where you use parallelism constructs should be in ParSearchWrapper!
                //result = ParallelChoice(copy, Flip(counter), ply + 1, Move, !mmax, scoreBoard, alpha, beta); /* swap Player_TPL */ // RECURSIVE call  
                result = SeqSearch(copy, Flip(counter), ply + 1, Move, !mmax, scoreBoard, alpha, beta); /* swap Player_TPL */ // RECURSIVE call  

                // trying to prevent preventing cell overwrite
                copy[Move.Item1, Move.Item2] = counters.e; /*  counter; */ // HWL: remove counter that was tried in this iteration
                                                                           // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready

                // PROBLEM EXISTS HERE BELOW , result.Item2 return null - LS 14.11.19
                score = -result.Item1; // assign score
                positions = result.Item2; // present position (x,y)

                // assign score to correct cell in score
                scoreBoard[result.Item2.Item1, result.Item2.Item2] = score;
         
                if (ply == 0)
                {
                    // assign score to correct cell in score
                    scoreBoard[Move.Item1, Move.Item2] = score;
                    scoreBoard.DisplayScoreBoardToFile();
                    string path = "data/printresult_stream.txt";
                    string createText = "++ FOR BOARD "  + Game_TPL.cntr + " and depth ply = " + ply.ToString()  + "HWL score: " + score.ToString() + " for Move " + Move.ToString() + " Result " + result.ToString() + Environment.NewLine;
		            // Console.WriteLine(createText);
                    File.AppendAllText(path, createText);
                    if (EXECPRINT_SCOREBOARD_ON == 1)
                    {
                        scoreBoard.DisplayScoreBoard();
                    }
                }
                if (Game_TPL.cntr >=40)
                {
                    Environment.Exit(99);
                }
                Object my_object = new Object();
                // if maximising                  
                if (/* true HWL || */ mmax)  // TOCHECK
                {
                    alpha = score;
                    if (score > bestScore)
                    {
                        lock (my_object)
                        {
                            // Move = bestMove; // HWL: wrong way around
                            bestMove = Move;
                            bestScore = score;
                            if (ply == 0)
                            {
                                Console.WriteLine("== HWL: new best score {0} at {1}", bestScore, bestMove);
                            }
                        }
                    }
                    if (alpha > bestScore)
                    {
                        lock (my_object)
                        {
                            bestMove = Move;
                            bestScore = alpha;
                            if (ply == 0)
                            {
                                Console.WriteLine("-- HWL: new best score {0} at {1}", bestScore, bestMove);
                            }
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
                            // Move = bestMove; // HWL: wrong way around
                            bestMove = Move;
                            bestScore = score;
                        }
                    }
                    if (beta <= alpha)
                        lock (my_object)
                        {
                            bestScore = alpha;
                            if (ply == 0)
                            {
                                Console.WriteLine("-- HWL: new best score {0} at {1}", bestScore, bestMove);
                            }
                        }
                }
                PrintCSVHeadRow();
                if (result.Item2 != new Tuple<int, int>(0, 0)) { 
            //    Console.Write(result.Item2);
                PrintCSVFailRow(board, scoreBoard);
            }
		continue; // BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD BAD
            }
            // HWL was: return new Tuple<int, Tuple<int, int>>(score, positions); // return
            return new Tuple<int, Tuple<int, int>>(bestScore, bestMove); // return
        }
        /*
--------------------------------------------------------------------------------------------------------------------------
ParSearchWrap -
--------------------------------------------------------------------------------------------------------------------------
This method construct top-level fct that generates parallelism; which is currently fixed to 4 parallel tasks and
each task steps in strides of 4 over all possible moves. For example, (1,1) is processed by a Thread 1, (1,2) is processed by 
Thread 2, Thread 3 is processed by (1,3), etc. Each task takes a clone of the current board; but in the recursive calls no
cloning is needed.
--------------------------------------------------------------------------------------------------------------------------
*/
        public Tuple<int, Tuple<int, int>> ParSearchWrap(GameBoard_TPL<counters> board, counters counter, int numTasks, GameBoard_TPL<int> scoreBoard)
        {
            List<Tuple<int, int>> availableMoves = new List<Tuple<int, int>>();
            if (SEGM_BOARD == 0)
            {
                availableMoves = getAvailableMoves(board, positions);
            }
            else if (SEGM_BOARD == 1)
            {
                availableMoves = getAvailableSegmentedMoves(board, positions); // HWL: <==== change to only return indices betwee 1-3
            }
            int score = Consts.MIN_SCORE;

            // compute the maximum over all results
            Tuple<int, Tuple<int, int>> res = new Tuple<int, Tuple<int, int>>(score, positions); ; // , res1, res2, res3, res4;
            Tuple<int, Tuple<int, int>> bestRes = new Tuple<int, Tuple<int, int>>(score, positions);

            GameBoard_TPL<counters> board1 = board.Clone();
            GameBoard_TPL<counters> board2 = board.Clone();
            GameBoard_TPL<counters> board3 = board.Clone();
            GameBoard_TPL<counters> board4 = board.Clone();

            // start and synchronise 4 parallel tasks
            /* HWL: try a sequential version first, to test strided iteration (below):
                Parallel.Invoke(() => { ress[0] = ParSearchWork(board1, Flip(counter), ply, positions, true, scoreBoard, stride, id, bestRes, 1); },
                        () => { ress[1] = ParSearchWork(board2, Flip(counter), ply, positions, true, scoreBoard, stride, id, bestRes, 2); },
                        () => { ress[2] = ParSearchWork(board3, Flip(counter), ply, positions, true, scoreBoard, stride, id, bestRes, 3); },
                        () => { ress[3] = ParSearchWork(board4, Flip(counter), ply, positions, true, scoreBoard, stride, id, bestRes, 4); });
            */

            List<Tuple<int, int>> unconsideredMoves = new List<Tuple<int, int>>();
            if (SEGM_BOARD == 0)
            {
                unconsideredMoves = getAvailableMoves(board, positions);
            }
            else if (SEGM_BOARD == 1)
            {
                unconsideredMoves = getAvailableSegmentedMoves(board, positions); // HWL: <==== change to only return indices betwee 1-3
            }        
            ress[0] = ParSearchWork(board1, counter, ply, positions, true, scoreBoard, stride, 0, bestRes, 1, unconsideredMoves /* for DEBUGGING only */);
            ress[1] = ParSearchWork(board2, counter, ply, positions, true, scoreBoard, stride, 1, bestRes, 2, unconsideredMoves /* for DEBUGGING only */);
            ress[2] = ParSearchWork(board3, counter, ply, positions, true, scoreBoard, stride, 2, bestRes, 3, unconsideredMoves /* for DEBUGGING only */);
            ress[3] = ParSearchWork(board4, counter, ply, positions, true, scoreBoard, stride, 3, bestRes, 4, unconsideredMoves /* for DEBUGGING only */);   
	    bestRes = res = result;
	    Console.WriteLine("__ HWL: best result on board {0} and player {1} from thread 0: {2}", Game_TPL.cntr, Flip(counter), bestRes.ToString());

            for (int j = 1; j < ress.Length; j++)
            { 
	      Console.WriteLine("__ HWL: best result on board {0} and player {1} from thread {2}: {3}", Game_TPL.cntr, Flip(counter), j, ress[j].ToString());
	      res = (ress[j].Item1 > res.Item1) ? ress[j] : res;  // result: <score, <position>>
            }
            Console.WriteLine("======================================================================================================");
            Console.WriteLine("-- OVERALL " + ":");
            Console.WriteLine("======================================================================================================");
            Console.WriteLine("__ HWL: OVERALL best result on board {0} and player {1}: {2}", Game_TPL.cntr, Flip(counter), result.ToString());
            Console.WriteLine("======================================================================================================");
            board[result.Item2.Item1, result.Item2.Item2] = Flip(counter);
            if (EXECPRINT_GAMEBOARD_ON == 1)
            {
                board.DisplayBoard();
            }
            if (SEGM_BOARD == 1)
            {
                for (int x = COORD_X + 1; x <= 7; x++)
                    for (int y = 1; y <= 7; y++)
                        if (board[x, y] != counters.N)
                        {
                            board[x, y] = counters.N;
                            scoreBoard[x, y] = 77; // 77 indicates blanked out cell on 3x3
                        }
                for (int x = 1; x <= COORD_X; x++)
                    for (int y = COORD_Y + 1; y <= 7; y++)
                        if (board[x, y] != counters.N)
                        {
                            board[x, y] = counters.N;
                            scoreBoard[x, y] = 77; // 77 indicates blanked out cell on 3x3
                        }
            }
       while (!Win(board, Flip(counter))) {
                ParSearchWrap(board, Flip(counter), numTasks, scoreBoard); // return
            
                break;
            }
                // return overall maximum
            return res;
        }
        /*
     --------------------------------------------------------------------------------------------------------------------------
     ParSearchWork -
     --------------------------------------------------------------------------------------------------------------------------
     This method constructs the execution of the parallel search and brings on results of each parallel search complied
     together in a culminative summary.
     --------------------------------------------------------------------------------------------------------------------------
     */
        public Tuple<int, Tuple<int, int>> ParSearchWork(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positons, bool mmax, GameBoard_TPL<int> scoreBoard, int stride, int id, Tuple<int,Tuple<int,int>> bestRes, int thread_no, List<Tuple<int, int>> unconsideredMoves /* for DEBUGGING only */)
{
            Tuple<int, Tuple<int, int>> res = new Tuple<int, Tuple<int, int>>(999, new Tuple<int, int>(9,9));
            List<Tuple<int, int>> availableMoves = new List<Tuple<int, int>>();
            if (SEGM_BOARD == 0)
            {
                availableMoves = getAvailableMoves(board, positions);
            }
            else if (SEGM_BOARD == 1)
            {
                availableMoves = getAvailableSegmentedMoves(board, positions); // HWL: <==== change to only return indices betwee 1-3
            }
            List<Tuple<int, int>> consideredMoves = new List<Tuple<int, int>>();
            int score = Consts.MIN_SCORE; // current score of move
            // stride = 1; // ???
            // int cnt = stride, offset = id; // HWL BUG: cnt needs to start with 0 (my bad!)
            int cnt = 0, offset = id; // HWL BUG: cnt needs to start with 0 (my bad!)
	    // ASSERT: 0 <= id < stride
	    Debug.Assert(0 <= id && id < stride);
            counters us = Flip(counter); // HWL: TOCHECK: I don't think you should flip at this point, rather at the call to SeqSearch
            Console.WriteLine("======================================================================================================");
            Console.WriteLine("-- THREAD " + id + ":");
            Console.WriteLine("======================================================================================================");
            Console.WriteLine("__ HWL: ParSearchWork called on board {0} with player {1} and thread id {2}", Game_TPL.cntr, counter.ToString(), id);
            if (SEGM_BOARD == 1)
            {
                for (int x = COORD_X+1; x <= 7; x++)
                    for (int y = 1; y <= 7; y++)
                        if (board[x, y] != counters.N)
                        {
                            board[x, y] = counters.N;
                            scoreBoard[x, y] = 77; // 77 indicates blanked out cell on 3x3
                        }
                for (int x = 1; x <= COORD_X; x++)
                    for (int y = COORD_Y+1; y <= 7; y++)
                        if (board[x, y] != counters.N)
                        {
                            board[x, y] = counters.N;
                            scoreBoard[x, y] = 77; // 77 indicates blanked out cell on 3x3
                        }
            }
            if (ply == 0)
            {
             //   scoreBoard.DisplayScoreBoardToFile();
            }
            if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, scoreBoard, us); // call stat evaluation func - takes board and Player_TPL and gives score to that Player_TPL
            }
            for (int i = 0; i < availableMoves.Count; i++)
            {
	      {
		if (offset == 0 && cnt == 0)
		  {
		    // HWL: this is a move for the current thread to process; remember it (for debugging)
		    if (ply == 0 ) {
            consideredMoves.Add(availableMoves[i]); } // HWL DEBUGGING
            res = SeqSearch(board, Flip(counter), ply, positions, true, scoreBoard, alpha, beta);
            bestRes = (res.Item1 > bestRes.Item1) ? res : bestRes;
		  }
		else
		  {
		    if (offset == 0) { cnt--; } else { offset--; }
		  }
		cont++;
	      }
	      if (false /* HWL: prevent file access for now */&& ply == 0)
                {
		    // HWL: print the moves considered by current thread; they must not overlap!
		    Console.WriteLine("__ HWL: {0} consideredMoves so far (thread {1}): {2} ", consideredMoves.Count, id, showList(consideredMoves));
		    Console.WriteLine("__ HWL: {0} ALL available Moves (thread {1}): {2} ", availableMoves.Count, id, showList(availableMoves));
                    Console.WriteLine("board " + Game_TPL.cntr + " processed by thread id: " + thread_no + " :");
                    if (SEGM_BOARD == 1)
                    {
                        for (int x = COORD_X + 1; x <= 7; x++)
                            for (int y = 1; y <= 7; y++)
                                if (board[x, y] != counters.N)
                                {
                                    board[x, y] = counters.N;
                                    scoreBoard[x, y] = 77; // 77 indicates blanked out cell on 3x3
                                }
                        for (int x = 1; x <= COORD_X; x++)
                            for (int y = COORD_Y + 1; y <= 7; y++)
                                if (board[x, y] != counters.N)
                                {
                                    board[x, y] = counters.N;
                                    scoreBoard[x, y] = 77; // 77 indicates blanked out cell on 3x3
                                }
                    }
         
                }
            }         
	    /* HWL: here, after the loop, print the considered moves; do you want to print to file in each loop iteration, or just at the end after the loop!? */
	    if (ply == 0) {
	      Console.WriteLine("__ HWL: {0} consideredMoves so far (thread {1}): {2} ", consideredMoves.Count, id, showList(consideredMoves));
	      Console.WriteLine("__ HWL: {0} ALL available Moves (thread {1}): {2} ", availableMoves.Count, id, showList(availableMoves));
	      // HWL: remove all cosideredMoves from the global list unconsideredMoves, to check that all moves are considered at the end
	      foreach (var mv in consideredMoves) {
		unconsideredMoves.Remove(mv);
	      }
	    }
	    return bestRes;
}
        /*
----------------------------------------------------------------------------------------------------------------
 showList -
--------------------------------------------------------------------------------------------------------------------------
 A string method that returns a list of vacant positions (represented in a Tuple<int,int> format) left on the board.
--------------------------------------------------------------------------------------------------------------------------
*/
        public static string showList(List<Tuple<int,int>> xs) {
  string str = "";
  foreach (Tuple<int,int> t in xs) {
    str += t.ToString() + ", ";
  }
  return str;
}
        /*
        ----------------------------------------------------------------------------------------------------------------
         ParallelChoice -
        --------------------------------------------------------------------------------------------------------------------------
         A method that choices to execute Minimax either in Parallel or Sequentially based on the current depth of the search.
        --------------------------------------------------------------------------------------------------------------------------
        */
        public Tuple<int, Tuple<int, int>> ParallelChoice(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positions, bool mmax, GameBoard_TPL<int> scoreBoard, int alpha, int beta)
{
            // decs
            counters us = Flip(counter);
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
                    return ParSearchWrap(board, Flip(counter), numTasks, scoreBoard); // return
                }
                else if (ply > 1)
                {
                    return SeqSearch(board, Flip(counter), ply, positions, true, scoreBoard, alpha, beta);
                }
            return new Tuple<int, Tuple<int, int>>(bestScore, bestMove);
        }
    }
}
