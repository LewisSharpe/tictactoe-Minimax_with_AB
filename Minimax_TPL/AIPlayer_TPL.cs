using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
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
        public static int _COORD_X = 3; // x coord
        public static int _COORD_Y = 3; // y coord
        // ******** BOARD ADJUSTMENT VARIABLES ********
        int SEGM_BOARD = 0;  // SEGMENT BOARD TO 3X3 COUNTER - 0 for off, 1 for yes, blanks out non active cells in 3x3 on 7x7 with 'N'
        static int _SEGM_BOARD = 0;  // SEGMENT BOARD TO 3X3 COUNTER - 0 for off, 1 for yes, blanks out non active cells in 3x3 on 7x7 with 'N'
        int EXECPRINT_SCOREBOARD_ON = 0; // 1 on, 0 off - TURN on/off SCORE BOARD PRINT ON CONSOLE ON AND OFF
        int EXECPRINT_GAMEBOARD_ON = 1;  // 1 on, 0 off - TURN on/off GAME BOARD PRINT ON CONSOLE ON AND OFF
        int DEBUGPRINT_ON = 0;  // 1 on, 0 off - TURN on/off FULL DEBUGGING PRINTING ON CONSOLE ON AND OFF
        int CSVWRITE_ON = 0;  // 1 on, 0 off - turn on/off tried move CSV file printing 
        private static Object TPL_FILESYNC_LOCK = new Object(); // lock to protect file update
        // ******** PRUNING ADJUSTMENT VARIABLES ********
        int PRUNE_ON = 1;// 1 on, 0 off - turn on/off alpha-beta pruning to Minimax function
        // ****************************************************
        // ******** PARALLELISM ADJUSTMENT VARIABLES ********
        const int TPL_PARALLELINVOKE_ON = 1;  // 1 on, 0 off - turn parallel invoke on and off
        const int no_of_cores_for_parallelism = 4; // specify number of cores to utilise parallelism in TPL variant
        private static Object TPL_THREADSYNC_LOCK = new Object(); // lock to protect Move and score from accidential updates
       // ****************************************************
       // PUBLIC DECS
        public static int ply = 0;    // start depth for search (should be 0)
        public const int maxPly = 3; // max depth for search: 0 = only immediate move; 1 = also next opponent move; 2 = also own next move etc
        public int alpha = Consts.MIN_SCORE; // set alpha to -1001
        public int beta = Consts.MAX_SCORE; // set beta to 1001
        public static Tuple<int, int> positions = new Tuple<int, int>(2, 2);
        public static int cont = 0; // counter for number of nodes visited
        public static int error_confirm = 0; // if positive moves to next board in case
        public const int stride = 4;  // fixed stride interation; never changess
        Tuple<int, Tuple<int, int>>[] ress = new Tuple<int, Tuple<int, int>>[4]; // set array for 4 calls of ParSearchWork
        public static int thread_no_track = 0; // thread track int variable
        Tuple<int, Tuple<int, int>> result; // return Tuple which returns score and position of Move from Minimax
        Stopwatch sw_move = new Stopwatch(); // timer for current move
        CustomStopwatch sw_thr0 = new CustomStopwatch(); // timer for thread 0 total execution time
        CustomStopwatch sw_thr1 = new CustomStopwatch(); // timer for thread 1 total execution time
        CustomStopwatch sw_thr2 = new CustomStopwatch(); // timer for thread 2 total execution time
        CustomStopwatch sw_thr3 = new CustomStopwatch(); // timer for thread 3 total execution time
        List<Tuple<int, int>> thr0_movesWithClear = new List<Tuple<int, int>>(); // list of positions considered by thread 0 for each move made (list is cleared after each move is made)  
        List<Tuple<int, int>> thr1_movesWithClear = new List<Tuple<int, int>>(); // list of positions considered by thread 1 for each move made (list is cleared after each move is made)  
        List<Tuple<int, int>> thr2_movesWithClear = new List<Tuple<int, int>>(); // list of positions considered  by thread 2 for each move made (list is cleared after each move is made)  
        List<Tuple<int, int>> thr3_movesWithClear = new List<Tuple<int, int>>(); // list of positions considered  by thread 3 for each move made (list is cleared after each move is made)  
        List<Tuple<int, int>> thr0_storemoves = new List<Tuple<int, int>>(); // list of positions considered by thread 0 for game cycle
        List<Tuple<int, int>> thr1_storemoves = new List<Tuple<int, int>>(); // list of positions considered by thread 1 for game cycle
        List<Tuple<int, int>> thr2_storemoves = new List<Tuple<int, int>>(); // list of positions considered by thread 2 for game cycle
        List<Tuple<int, int>> thr3_storemoves = new List<Tuple<int, int>>(); // list of positions considered by thread 3 for game cycle
        List<Tuple<int, int>> all_conmoves = new List<Tuple<int, int>>(); // all considered moves in game cycle
        public static List<Tuple<int, int>> all_Xplacedmoves = new List<Tuple<int, int>>(); // all placed moves in game cycle
        public static List<Tuple<int, int>> all_Oplacedmoves = new List<Tuple<int, int>>(); // all placed moves in game cycle
        int move = 1; // move number
        public AIPlayer_TPL(counters _counter) : base(_counter) {
        }
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
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>(); // initialise list with potential moves remaining on current board 
            for (int x = 1; x <= 7; x++) // for 7x7
                for (int y = 1; y <= 7; y++) // for 7x7
                    if (board[x, y] == counters.e) // if current position is empty
                    {
                        Tuple<int, int> coords = new Tuple<int, int>(x, y); // initialise current position
                        moves.Add(coords); // add current position to list of potential moves remaining on the current
                    }
            return moves; // return list with potential moves remaining on current board 
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
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>(); // initialise list with potential moves remaining on current board 
            for (int x = 1; x <= COORD_X; x++) // for 3x3
                for (int y = 1; y <= COORD_Y; y++)  // for 3x3
                    if (board[x, y] == counters.e) // if current position is empty
                    {
                        Tuple<int, int> coords = new Tuple<int, int>(x, y);  // initialise current position
                        moves.Add(coords); // add current position to list of potential moves remaining on the current
                    }
            return moves;  // return list with potential moves remaining on current board 
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
            int score = Consts.MIN_SCORE; // initial score to min score const
            bool mmax = false; // minimising
            sw_move = Stopwatch.StartNew(); // start new timer for move
            Tuple<int, Tuple<int, int>> result = new Tuple<int, Tuple<int, int>>(score, positions); // initalise result
            result = ParallelChoice(board, counter, ply, positions, mmax, scoreBoard, alpha, beta); // return result from ParallelChoice method
            // Stop timing.
            sw_move.Stop(); // stop timer for move
            sw_move.Reset(); // reset timer for next move
            move++; // increment move number counter
            // Return positions
            return result.Item2; // return coordinate position from search
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
      Returns head move to CSV file.
      --------------------------------------------------------------------------------------------------------------------------
      */
        public void PrintCSVHeadRow()
        {
            Object thisCSVLock = new Object(); // initialise for lock to handle CSV write synchronisation
            /* HWL: omit for now */
            // write to file
            var file = "data/TPLTST_Report.csv"; // initialise CSV file
            var csv = new System.Text.StringBuilder();
            var title = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", "DATE", "TIME", "RESULT", "BOARD NO", "REASON", "SCORE", "X", "Y", "SIDE", "POSITIONS VISTED", "DEPTH", "TIME ELAPSED", "THREAD NO.", "INT BOARD", "FIN BOARD", "SCORE BOARD", Environment.NewLine);
            csv.Append(title); 
            lock (thisCSVLock) // lock to handle CSV write synchronisation
            {
                File.AppendAllText(file, title.ToString()); // append title line to CSV file
            }
        }
        /* 
    ----------------------------------------------------------------------------------------------------------------
    * PrintCSVPassRow -
    --------------------------------------------------------------------------------------------------------------------------
    Returns pass move to CSV file
    --------------------------------------------------------------------------------------------------------------------------
    */
        public void PrintCSVPassRow(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard)
        {
            string intboard_COPY = File.ReadAllText("data/intboards.txt"); // initialise txt file
            string finboard_COPY = File.ReadAllText("data/finboards.txt");  // initialise txt file
            string scoreboard_COPY = File.ReadAllText("data/scoreboards.txt");  // initialise txt file
            var newLine = "";
            Object thisCSVLock = new Object(); // initialise for lock to handle CSV write synchronisation
            var file = "data/TPLTST_Report.csv";  // initialise CSV file
            var date = DateTime.Now.ToShortDateString(); // initialise date variable
            var time = DateTime.Now.ToString("HH:mm:ss"); // initialise time variable
            var csv = new System.Text.StringBuilder();
            string status = "PASS";
            string reason = "Winning combination found";
            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", date, time, status.ToString(), "Board " + int.Parse(Game_TPL.cntr.ToString()), reason.ToString(), result.Item1.ToString(), result.Item2.Item1.ToString(), result.Item2.Item2.ToString(), counter, cont, ply, 7777, thread_no_track, intboard_COPY, finboard_COPY, scoreboard_COPY, Environment.NewLine);
            csv.Append(newLine);
            lock (thisCSVLock) // lock to handle CSV write synchronisation
            {
                File.AppendAllText(file, newLine.ToString()); // append return position with stat info to CSV file
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
            string intboard_COPY = File.ReadAllText("data/intboards.txt");  // initialise txt file
            string finboard_COPY = File.ReadAllText("data/finboards.txt");  // initialise txt file
            string scoreboard_COPY = File.ReadAllText("data/scoreboards.txt");  // initialise txt file
            // #ifdef DEBUG
            Object thisCSVLock = new Object(); // initialise for lock to handle CSV write synchronisation
            var file = "data//TPLTST_Report.csv";  // initialise CSV file
            var date = DateTime.Now.ToShortDateString(); // initialise date variable
            var time = DateTime.Now.ToString("HH:mm:ss"); // initialise time variable
            var csv = new System.Text.StringBuilder();
            var newLine = "";
            string status = "----";
            string reason = "----";
            newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}", date, time, status.ToString(), "Board " + int.Parse(Game_TPL.cntr.ToString()), reason.ToString(), result.Item1.ToString(), result.Item2.Item1.ToString(), result.Item2.Item2.ToString(), counter, cont, ply, 7777, thread_no_track, intboard_COPY, finboard_COPY, scoreboard_COPY, Environment.NewLine);
            csv.Append(newLine);
            lock (thisCSVLock) // lock to handle CSV write synchronisation
            {
                File.AppendAllText(file, newLine.ToString());  // append return position with stat info to CSV file 
                board.DisplayIntBoardToFile();
                board.DisplayFinBoardToFile();
            }
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
            for (int x = 1; x <= ((_SEGM_BOARD == 1) ? 3 : 7); x++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                       for (int y = 1; y <= ((_SEGM_BOARD == 1) ? 3 : 7); y++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            // checks for valid two in gap combinations
                            if (yy == 0 && xx == 0)     // if not found
                                continue; // continue the search
                            // if counter is on its own then for one in row
                            if (board[x, y] == us)
                                return true;
                        }
                }
            return false; // function returns false if there is not a one in row
        }
        /* 
 ----------------------------------------------------------------------------------------------------------------
 * FindTwoInARow -
 --------------------------------------------------------------------------------------------------------------------------
  A bool which returns true or false of the presence of a two counters of the same symbol placed side by side on the board
 --------------------------------------------------------------------------------------------------------------------------
 */
        public bool FindTwoInARow(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= ((_SEGM_BOARD == 1) ? 3 : 7); x++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                       for (int y = 1; y <= ((_SEGM_BOARD == 1) ? 3 : 7); y++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            // checks for valid two in gap combinations
                            if (yy == 0 && xx == 0)     // if not found
                                continue; // continue with search
                            if (board[x, y] == us && board[x, y] == board[x + xx, y + yy]) // if current cell has neighbour with same symbol then
                                // two in a row in centre should give higher score
                                return true;
                        }
                }
            return false; // function returns false if there is not a two in row
        }
        /* 
----------------------------------------------------------------------------------------------------------------
* FindTwoWithAGap -
--------------------------------------------------------------------------------------------------------------------------
 A bool which returns true or false of the presence of a two counters of the same symbol with an empty gap inbetween which 
 has potential to be turned into a three in a row.
--------------------------------------------------------------------------------------------------------------------------
*/
        public static bool FindTwoWithGap(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= ((_SEGM_BOARD == 1) ? 3 : 7); x++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                       for (int y = 1; y <= ((_SEGM_BOARD == 1) ? 3 : 7); y++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                {
                    // check whether position piece at [x,y] has the same piece as both neighbours
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            if (yy == 0 && xx == 0)     // if not found
                                continue; // continue the search
                            // check that all coordinates tested are on the board
                            // TOCHECK: if the border uses a BORDER, this shouldn't be necessary
                            if (x + xx <= 0 ||
                            x + xx > _COORD_X ||
                            y + yy <= 0 ||
                            y + yy > _COORD_Y ||
                            x - xx <= 0 ||
                            x - xx > _COORD_X ||
                            y - yy <= 0 ||
                            y - yy > _COORD_Y)
                                continue; // continue the search
                            // checks for valid two in gap combinations
                            if (board[x, y] == counters.e &&
                            board[x + xx, y + yy] == us &&
                            board[x - xx, y - yy] == us) // checks for top-left to bottom-right diag
                            {
                                return true;
                            }
                            if (yy == 1 && xx == 1 &&
                            board[x, y] == us &&
                            board[x + xx, y - yy] == us &&
                            board[x - xx, y + yy] == us) // checks for bottom-left to top-right diag
                            {
                                return true;
                            }
                        }
                }
            return false;  // function returns false if there is not a two in row with a gap that can be build on from the right hand side is found
        }
        /* 
----------------------------------------------------------------------------------------------------------------
* FindTwoWithRightBuild -
--------------------------------------------------------------------------------------------------------------------------
A bool which returns true or false of the presence of a two counters of the same symbol which has potential 
to be turned into a three-in-a-row with an empty cell available to the right of the existing two-in-a-row.
--------------------------------------------------------------------------------------------------------------------------
*/
        public static bool FindTwoWithRightBuild(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= ((_SEGM_BOARD == 1) ? 3 : 7); x++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                       for (int y = 1; y <= ((_SEGM_BOARD == 1) ? 3 : 7); y++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                {
                    // check whether position piece at [x,y] has the same piece as both neighbours
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            if (yy == 0 && xx == 0)  // if not found
                                continue; // continue the search
                            // check that all coordinates tested are on the board
                            // TOCHECK: if the border uses a BORDER, this shouldn't be necessary
                            if (x + xx <= 0 ||
                            x + xx > _COORD_X ||
                            y + yy <= 0 ||
                            y + yy > _COORD_Y ||
                            x - xx <= 0 ||
                            x - xx > _COORD_X ||
                            y - yy <= 0 ||
                            y - yy > _COORD_Y)
                                continue; // continue the search
                            if (board[x, y] == us &&
                            board[x + xx, y + yy] == counters.e &&
                            board[x - xx, y - yy] == us) // checks for top-left to bottom-right diag
                            {
                                return true;
                            }
                            if (yy == 1 && xx == 1 &&
                            board[x, y] == us &&
                            board[x + xx, y - yy] == counters.e &&
                            board[x - xx, y + yy] == us) // checks for bottom-left to top-right diag
                            {
                                return true;
                            }
                        }
                }
            return false;  // function returns false if no two in row that can be build on from the right hand side is found
        }
        /* 
----------------------------------------------------------------------------------------------------------------
* FindTwoWithLeftBuild -
--------------------------------------------------------------------------------------------------------------------------
A bool which returns true or false of the presence of a two counters of the same symbol which has potential 
to be turned into a three-in-a-row with an empty cell available to the left of the existing two-in-a-row.
--------------------------------------------------------------------------------------------------------------------------
*/
        public static bool FindTwoWithLeftBuild(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= ((_SEGM_BOARD == 1) ? 3 : 7); x++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                       for (int y = 1; y <= ((_SEGM_BOARD == 1) ? 3 : 7); y++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                {
                    // check whether position piece at [x,y] has the same piece as both neighbours
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            if (yy == 0 && xx == 0) // if not found
                                continue; // continue the search
                            // check that all coordinates tested are on the board
                            // TOCHECK: if the border uses a BORDER, this shouldn't be necessary
                            if (x + xx <= 0 ||
                            x + xx > _COORD_X ||
                            y + yy <= 0 ||
                            y + yy > _COORD_Y ||
                            x - xx <= 0 ||
                            x - xx > _COORD_X ||
                            y - yy <= 0 ||
                            y - yy > _COORD_Y)
                                continue; // continue the search
                                          // checks for valid combinations
                            if (board[x, y] == us &&
                            board[x + xx, y + yy] == us &&
                            board[x - xx, y - yy] == counters.e) // checks for top-left to bottom-right diag
                            {
                                return true;
                            }
                            if (yy == 1 && xx == 1 &&
                            board[x, y] == us &&
                            board[x + xx, y - yy] == us &&
                            board[x - xx, y + yy] == counters.e) // checks for bottom-left to top-right diag
                            {
                                return true;
                            }
                        }
                }
            return false; // function returns false if no two in row that can be build on from the left hand side is found
        }
        /* 
        ----------------------------------------------------------------------------------------------------------------
        * FindTwoWithBothBuild -
        --------------------------------------------------------------------------------------------------------------------------
        A bool which returns true or false of the presence of a two counters of the same symbol which has potential 
        to be turned into a three-in-a-row with an empty cells available to the left and to the right of the
        existing two-in-a-row.
        --------------------------------------------------------------------------------------------------------------------------
        */
        public static bool FindTwoWithBothBuild(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= ((_SEGM_BOARD == 1) ? 3 : 7); x++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                       for (int y = 1; y <= ((_SEGM_BOARD == 1) ? 3 : 7); y++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                {
                    // check whether position piece at [x,y] has the same piece as both neighbours
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            if (yy == 0 && xx == 0) // if not found
                                continue; // continue the search
                            // check that all coordinates tested are on the board
                            // TOCHECK: if the border uses a BORDER, this shouldn't be necessary
                            if (x + xx <= 0 ||
                            x + xx > _COORD_X ||
                            y + yy <= 0 ||
                            y + yy > _COORD_Y ||
                            x - xx <= 0 ||
                            x - xx > _COORD_X ||
                            y - yy <= 0 ||
                            y - yy > _COORD_Y)
                                continue; // continue the search
                            // checks for valid combinations
                            if (board[x, y] == us &&
                            board[x + xx, y + yy] == us &&
                            board[x + xx + xx, y + yy + yy] == counters.e &&
                            board[x - xx, y - yy] == counters.e) // checks for top-left to bottom-right diag
                            {
                                return true;
                            }
                            if (yy == 1 && xx == 1 &&
                            board[x, y] == us &&
                            board[x + xx, y - yy] == us &&
                            board[x + xx + xx, y - yy - yy] == us &&
                            board[x - xx, y + yy] == counters.e) // checks for bottom-left to top-right diag
                            {
                                return true;
                            }
                        }
                }
            return false; // function returns false if no two in a row that can be build on from both side is found 
        }
        /* 
        ----------------------------------------------------------------------------------------------------------------
        * FindTwoInARowWithNoBuild -
        --------------------------------------------------------------------------------------------------------------------------
        A bool which returns true or false of the presence of a two counters of the same symbol which has no potential 
        to be turned into a three-in-a-row with no empty cells available to the left of the existing two-in-a-row.
        --------------------------------------------------------------------------------------------------------------------------
        */
        public static bool FindTwoWithNoBuild(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= ((_SEGM_BOARD == 1) ? 3 : 7); x++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                       for (int y = 1; y <= ((_SEGM_BOARD == 1) ? 3 : 7); y++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                {
                    // check whether position piece at [x,y] has the same piece as both neighbours
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            if (yy == 0 && xx == 0) // if not found
                                continue; // continue the search
                            // check that all coordinates tested are on the board
                            // TOCHECK: if the border uses a BORDER, this shouldn't be necessary
                            if (x + xx <= 0 ||
                            x + xx > _COORD_X ||
                            y + yy <= 0 ||
                            y + yy > _COORD_Y ||
                            x - xx <= 0 ||
                            x - xx > _COORD_X ||
                            y - yy <= 0 ||
                            y - yy > _COORD_Y)
                                continue; // continue the search
                            // checks for valid combinations
                            if (board[x, y] == us &&
                            board[x + xx, y + yy] == us &&
                            board[x + xx + xx, y + yy + yy] == us &&
                            board[x - xx, y - yy] == us) // checks for top-left to bottom-right diag
                            {
                                return true;
                            }
                            if (yy == 1 && xx == 1 &&
                            board[x, y] == us &&
                            board[x + xx, y - yy] == us &&
                            board[x + xx + xx, y - yy - yy] == us &&
                            board[x - xx, y + yy] == us) // checks for bottom-left to top-right diag
                            {
                                return true;
                            }
                        }
                }
            return false; // returns false if two in a row with no build on either side is now
        }
        /* 
       ----------------------------------------------------------------------------------------------------------------
       * EmptySpacesAroundCounter -
       --------------------------------------------------------------------------------------------------------------------------
       This function counts number of empty cells around an existing counter placement. A const value is set to 2 which is multiped
       by an integer value of the number of empty cells around the existing counter placement. This multiped value is returned and 
       used in the static evaluation function to influence scoring.
       --------------------------------------------------------------------------------------------------------------------------
       */
        public static Tuple<int, int, int> EmptySpacesAroundCounter(GameBoard_TPL<counters> board, counters us)
        {
            int const_val = 2; // const value is set to 2
            int cell_sum = 0; // number of empty cells around the current counter
            for (int x = 1; x <= ((_SEGM_BOARD == 1) ? 3 : 7); x++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                       for (int y = 1; y <= ((_SEGM_BOARD == 1) ? 3 : 7); y++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                {
                    // check whether position piece at [x,y] has the same piece as both neighbours
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            if (yy == 0 && xx == 0) // if not found
                                continue; // continue the search
                            /*
                             Current cell is x,y:
                            // Console.WriteLine("!! CURRENT CELL: {0}{1}{2})\n", x, ",", y);                          
                            */
                            // checks for valid combinations
                            // above right: Console.WriteLine("!! above right diagonal: {0}{1}{2})\n", x + 1, ",", y - yy);
                            if (board[x, y] == us &&
                            board[x + 1, y - yy] == counters.e)
                            {
                                cell_sum = cell_sum + 1;
                            }
                            // above left: Console.WriteLine("!! above left diagonal: {0}{1}{2})\n", x - 1, ",", y - yy);
                            else if (board[x, y] == us &&
                            board[x - 1, y - yy] == counters.e)
                            {
                                cell_sum = cell_sum + 1;
                            }
                            // above: Console.WriteLine("!! above: {0}{1}{2})\n", x, ",", y - yy);
                            else if (board[x, y] == us &&
                            board[x, y - yy] == counters.e) // checks for top-left to bottom-right diag
                            {
                                cell_sum = cell_sum + 1;
                            }
                            //left: Console.WriteLine("!! left: {0}{1}{2})\n", x + 1, ",", y);
                            else if (board[x, y] == us &&
                            board[x - 1, y] == counters.e) 
                            {
                                cell_sum = cell_sum + 1;
                            }
                            // right: Console.WriteLine("!! right: {0}{1}{2})\n", x - 1, ",", y);
                            else if (board[x, y] == us &&
                            board[x + 1, y] == counters.e) 
                            {
                                cell_sum = cell_sum + 1;
                            }
                            // below: Console.WriteLine("!! below: {0}{1}{2})\n", x, ",", y + yy);
                            else if (board[x, y] == us &&
                            board[x, y + yy] == counters.e)
                            {
                                cell_sum = cell_sum + 1;
                            }
                            // below right: Console.WriteLine("!! below right diagonal: {0}{1}{2})\n", x + 1, ",", y + yy);
                            else if (board[x, y] == us &&
                            board[x + 1, y + yy] == counters.e)
                            {
                                cell_sum = cell_sum + 1;
                            }
                            // below left: Console.WriteLine("!! below left diagonal: {0}{1}{2})\n", x - 1, ",", y + yy);
                            else if (board[x, y] == us &&
                            board[x - 1, y + yy] == counters.e)
                            {
                                cell_sum = cell_sum + 1;
                            }

                        }
                }
            return new Tuple<int, int, int>(const_val, cell_sum, const_val*(cell_sum)); // function returns a tuple with the constant value used, the number of empty cells around the current counter, and a integer made up of the multiplication of the constant value multiped by the number of empty cells around the current counter
        }
        /*
         ----------------------------------------------------------------------------------------------------------------
          FindThreeInARow -
         --------------------------------------------------------------------------------------------------------------------------
          A bool which returns true or false of the presence of a three counters of the same symbol placed side by side on the board
         --------------------------------------------------------------------------------------------------------------------------
         */
        public static bool FindThreeInARow(GameBoard_TPL<counters> board, counters us)
        {
            for (int x = 1; x <= ((_SEGM_BOARD == 1) ? 3 : 7); x++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                       for (int y = 1; y <= ((_SEGM_BOARD == 1) ? 3 : 7); y++) // 1 <=3 for segemented 3x3, or 1 <=7 for original 7x7 board
                {
                    // check whether position piece at [x,y] has the same piece as both neighbours
                    for (int xx = 0; xx <= 1; xx++)
                        for (int yy = 0; yy <= 1; yy++)
                        {
                            if (yy == 0 && xx == 0) // if not found
                                continue; // continue the search
                            // check that all coordinates tested are on the board
                            // TOCHECK: if the border uses a BORDER, this shouldn't be necessary
                            if (x + xx <= 0 ||
                            x + xx > _COORD_X ||
                            y + yy <= 0 ||
                            y + yy > _COORD_Y ||
                            x - xx <= 0 ||
                            x - xx > _COORD_X ||
                            y - yy <= 0 ||
                            y - yy > _COORD_Y)
                                continue; // continue the search
                            // checks for valid three in row combinations
                            if (board[x, y] == us &&
                            board[x, y] == board[x + xx, y + yy] &&
                            board[x, y] == board[x - xx, y - yy]) // checks for top-left to bottom-right diag
                            {
                                // Console.WriteLine("!! HWL: Centre of 3-in-a-row: {0}{1}{2} (with {3},{4} and {5},{6})\n", x,",",y,x + xx, y + yy, x - xx, y - yy);
                                return true;
                            }
                            if (yy == 1 && xx == 1 &&
                            board[x, y] == us &&
                            board[x, y] == board[x + xx, y - yy] &&
                            board[x, y] == board[x - xx, y + yy]) // checks for bottom-left to top-right diag
                            {
                                // Console.WriteLine("!! HWL: Centre of 3-in-a-row: {0}{1}{2} (with {3},{4} and {5},{6})\n", x, ",", y, x + xx, y - yy, x - xx, y + yy);
                                //Console.WriteLine("-- next adjacent cell: {0}{1}{2})\n", x + xx + xx, ",", y - yy - yy);
                                /*
                                 ! HWL: Centre of 3-in-a-row: 2,2 (with 3,1 and 1,3)
                                 -- next adjacent cell: 4,0)
                                 */
                                return true;
                            }
                        }
                }
            return false; // returns false if 3 in row now found
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
            int score; // initialise score
            // eval if move is win draw or loss
            if (FindThreeInARow(board, us)) // Player_TPL win?
                return score = 1000; // Player_TPL win confirmed
            else if (FindThreeInARow(board, us + 1)) // opponent win?
                return score = -1000; // opp win confirmed
            else if (FindTwoWithBothBuild(board, us)) // Player_TPL both build?
                return score = 110 + EmptySpacesAroundCounter(board, us).Item3; // Player_TPL both build confirmed
            else if (FindTwoWithBothBuild(board, us + 1)) // opponent both build?
                return score = -110 + -EmptySpacesAroundCounter(board, us).Item3; // opp both build confirmed
            else if (FindTwoWithNoBuild(board, us)) // Player_TPL no build?
                return score = 5 + EmptySpacesAroundCounter(board, us).Item3 / 2; // Player_TPL no build confirmed
            else if (FindTwoWithNoBuild(board, us + 1)) // opponent no build?
                return score = -5 + -EmptySpacesAroundCounter(board, us).Item3 / 2; // opp no build confirmed
            else if (FindTwoWithGap(board, us)) // Player_TPL twor with gap?
                return score = 50 + EmptySpacesAroundCounter(board, us).Item3; // Player_TPL two with gap confirmed
            else if (FindTwoWithGap(board, us + 1)) // opponent two with gap?
                return score = -50 + -EmptySpacesAroundCounter(board, us).Item3; // opp two with gap confirmed
            else if (FindTwoWithRightBuild(board, us)) // Player_TPL right build?
                return score = 55 + EmptySpacesAroundCounter(board, us).Item3; // Player_TPL right build confirmed
            else if (FindTwoWithRightBuild(board, us + 1)) // opponent right build?
                return score = -55 + -EmptySpacesAroundCounter(board, us).Item3; // opp right build confirmed
            else if (FindTwoWithLeftBuild(board, us)) // Player_TPL left build?
                return score = 55 + EmptySpacesAroundCounter(board, us).Item3; // Player_TPL left build confirmed
            else if (FindTwoWithLeftBuild(board, us + 1)) // opponent left build?
                return score = -55; // opp left build confirmed 
            else if (FindOneInARow(board, us)) // Player_TPL one in row?
                return score = 10 + EmptySpacesAroundCounter(board, us).Item3; // Player_TPL one in row confirmed
            else if (FindOneInARow(board, us + 1)) // opponent one in row?
                return score = -10 + -EmptySpacesAroundCounter(board, us).Item3; // opp one in row confirmed
            else
                return score = 23; // return dummy value
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
            counters us = counter /* Flip(counter) */; // HWL: why flip counter here? should only be flipped when calling SeqSearch recursively
            List<Tuple<int, int>> availableMoves = new List<Tuple<int, int>>(); // intialise blank list for available moves 
            // if board is a standard 7x7, set available moves to 49
            if (SEGM_BOARD == 0)
            {
                availableMoves = getAvailableMoves(board, positions);
            }
            // if board is a standard 3x3, set available moves to 9
            else if (SEGM_BOARD == 1)
            {
                availableMoves = getAvailableSegmentedMoves(board, positions);
            }
            mmax = true; // are we maximising?
            int bestScore = mmax ? -1002 : 1002; // best score in search
            int score; // Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(0, 0); // initalise current move
            Tuple<int, int> bestMove = new Tuple<int, int>(8, 1);  // initalise best move
            GameBoard_TPL<counters> copy = board.Clone(); // initialise copy board that takes a clone of the original board
            GameBoard_TPL<counters> input_board = board.Clone(); // HWL: for DEBUGGING onlu
            // if the list available moves is empty
            if (availableMoves.Count == 0)
            {
                int score_ = EvalCurrentBoard(board, scoreBoard, Flip(counter) /* us */); // evaluate current score
                Console.WriteLine(".. HWL: no moves on following board (ply={1}, counter={2}); static eval = {0}", score_, ply, counter);
                return new Tuple<int, Tuple<int, int>>(score_, positions); // return
            }
            // CHECK DEPTH: if deeper than maxPly, don't search further, just return the current score
            if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, scoreBoard, counter /* us */ ); // call stat evaluation func - takes board and Player_TPL and gives score to that Player_TPL
                return new Tuple<int, Tuple<int, int>>(score, positions);
            }
            int end = availableMoves.Count; // initialise int variable to the number of moves of the availableMoves list
            // initialise for loop
            for (int i = 0; i < end /* availableMoves.Count */; i++)
            { // for
                Move = availableMoves[i]; // current move
                copy[Move.Item1, Move.Item2] = counter; // place counter
                                                        // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready
                // check for Win
                if (FindThreeInARow(copy, us))
                {
                    // add deeper debugging printing
                    if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection    // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection   // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                    {
                        Console.WriteLine("          3-in-a-row found at {3} for player {0} (ply={1}, positions={2})", counter, ply, positions.ToString(), Move.ToString());
                    }
                    copy[Move.Item1, Move.Item2] = counters.e;      // blank the field again
                    return new Tuple<int, Tuple<int, int>>(1000, Move); // return win-in-1-move
                }
                // list defined in Minimax declarations
                // HWL: in the initial parallel version you should NOT generate parallelism recursively; the only place where you use parallelism constructs should be in ParSearchWrapper!
                result = SeqSearch(copy, Flip(counter), ply + 1, Move, !mmax, scoreBoard, alpha, beta); /* swap Player_TPL */ // RECURSIVE call  

                // trying to prevent preventing cell overwrite
                copy[Move.Item1, Move.Item2] = counters.e; /*  counter; */ // HWL: remove counter that was tried in this iteration
                                                                           // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready
                score = -result.Item1; // assign score
                positions = result.Item2; // present position (x,y)
                scoreBoard[Move.Item1, Move.Item2] = score;         // assign score to correct cell in score
                // if score board print is turned on then
                if (EXECPRINT_SCOREBOARD_ON == 1)
                {
                    scoreBoard.DisplayScoreBoard();
                    lock (TPL_FILESYNC_LOCK)
                    {
                        scoreBoard.DisplayScoreBoardToFile();
                    }
                }
                // if depth level is 1
                if (ply == 1) // HWL: DEBUGGING only
                    // if detailed debugging printing is turned on
                    if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                    {
                        Console.WriteLine(".... {1} at {0} (ply={2}); score = {3}", Move.ToString(), counter, ply, score); /* , positions.ToString() */
                    }
                // if depth level is 0
                if (ply == 0)
                {
                    scoreBoard.DisplayScoreBoardToFile();
                    string path = "data/printresult_stream.txt";
                    string createText = "++ FOR BOARD " + Game_TPL.cntr + " and depth ply = " + ply.ToString() + "HWL score: " + score.ToString() + " for Move " + Move.ToString() + " Result " + result.ToString() + Environment.NewLine;
                    File.AppendAllText(path, createText); // append output to file
                }
                // start of alpha-beta printing
                Object my_object = new Object(); // initialise object lock to protect updating of bestMove and bestScore               
                if (/* true || */  mmax)                  // if maximising   
                {
                    if (PRUNE_ON == 1)  // turn on alpha-beta pruning
                    {
                        alpha = score; // assign alpha to score
                    }
                    if (score > bestScore) // if score is greater than best score
                    {
                        lock (my_object) // lock of move update between threads
                        {
                            bestMove = Move; // assign bestMove to the current move
                            bestScore = score; // assign bestScore to the current score
                            // if the depth level is greater or equal to 1
                            if (ply >= 1)
                            {
                                // if detailed debugging printing is turned on
                                if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                                {
                                    Console.WriteLine("        maximising: player {4} new best score {0} at {1} (ply={2}, positions={3})", bestScore, bestMove, ply, positions.ToString(), counter);
                                }
                                // if score board print is turned on then
                                if (EXECPRINT_SCOREBOARD_ON == 1)
                                {
                                    scoreBoard.DisplayScoreBoard();
                                    lock (TPL_FILESYNC_LOCK) // lock to protect file synchronisation
                                    {
                                        scoreBoard.DisplayScoreBoardToFile();
                                    }
                                }
                            }
                        }
                    }
                    if (PRUNE_ON == 1)
                    {
                        // if alpha is greater than best score
                        if (alpha > bestScore)
                        {
                            lock (my_object) // lock of move update between threads
                            {
                                bestMove = Move; // assign bestMove to the current move
                                bestScore = alpha; // assign bestScore to alpha
                                                   // if the depth level is equal to 1
                                if (ply == 1)
                                {
                                    // Console.WriteLine("-- HWL: new best score {0} at {1} (ply={2})", bestScore, bestMove, ply);
                                }
                            }
                        }
                    }
                }
                else                 // if minimising
                {
                    // if best score is greater than score
                    if (bestScore > score)
                    {
                        lock (my_object) // lock of move update between threads
                        {
                            bestMove = Move; // assign bestMove to the current move
                            bestScore = score;  // assign bestMove to the current move
                            // if depth level is greater or equal to 1
                            if (ply >= 1)
                            {
                                // if detailed debugging printing is turned on
                                if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                                {
                                    Console.WriteLine("        minimising: player {4} new best score {0} at {1} (ply={2}, positions={3})", bestScore, bestMove, ply, positions.ToString(), counter);
                                }
                                // if score board print is turned on then
                                if (EXECPRINT_SCOREBOARD_ON == 1)
                                {
                                    scoreBoard.DisplayScoreBoard();
                                    lock (TPL_FILESYNC_LOCK) // lock to protect file synchronisation
                                    {
                                        scoreBoard.DisplayScoreBoardToFile();
                                    }
                                }
                            }
                        }
                    }
                    if (PRUNE_ON == 1)
                    {
                        // if beta is less than or equal to alpha
                        if (beta <= alpha)
                            lock (my_object) // lock of move update between threads
                            {
                                bestScore = alpha; // assign bestScore to alpha
                                                   // if the depth level is 0
                                if (ply == 0)
                                {
                                    // if detailed debugging printing is turned on
                                    if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                                    {
                                        Console.WriteLine("-- HWL: new best score {0} at {1}", bestScore, bestMove);
                                    }
                                }
                            }
                    }
                }
                // if csv printing is turned on
                if (CSVWRITE_ON == 1)
                {
                    lock (TPL_FILESYNC_LOCK) // lock to protect file synchronisation
                    {
                        PrintCSVHeadRow(); // print title row
                    }
                }
                // if result move coords is not 0,0
                if (result.Item2 != new Tuple<int, int>(0, 0))
                {
                    // if csv printing is turned on
                    if (CSVWRITE_ON == 1)
                    {
                        lock (TPL_FILESYNC_LOCK) // lock to protect file synchronisation
                        {
                            PrintCSVFailRow(board, scoreBoard);  // print a current position searched that qualifies for a fail comment to CSV file
                        }
                    }
                }
                continue; // continue the search
            }
            // if the depth level is equal to 1
            if (ply == 1)
                // if detailed debugging printing is turned on
                if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                {
                    Console.WriteLine("--      best move at ply={0} for {1} is {2} with score {3}", ply, counter, bestMove, bestScore);
                }
            return new Tuple<int, Tuple<int, int>>(bestScore, bestMove); // function returns bestScore and bestMove of search
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
            List<Tuple<int, int>> availableMoves = new List<Tuple<int, int>>();  // intialise blank list for available moves 
            // if board is a standard 7x7, set available moves to 49
            if (SEGM_BOARD == 0)
            {
                availableMoves = getAvailableMoves(board, positions);
            }
            // if board is a standard 3x3, set available moves to 9
            else if (SEGM_BOARD == 1)
            {
                availableMoves = getAvailableSegmentedMoves(board, positions);
            }
            int score = Consts.MIN_SCORE; // initalise score to min score constant
            // compute the maximum over all results
            Tuple<int, Tuple<int, int>> res = new Tuple<int, Tuple<int, int>>(score, positions); ; // initialise result tuple variable
            Tuple<int, Tuple<int, int>> bestRes = new Tuple<int, Tuple<int, int>>(score, positions); // initialise best result tuple variable
            Tuple<int, Tuple<int, int>> worstRes = new Tuple<int, Tuple<int, int>>(Consts.MIN_SCORE, positions); // initialise worst result tuple variable
            // create clones of original board - one board for each thread
            GameBoard_TPL<counters> board1 = board.Clone(); // initialise board 1 to a clone of the original board
            GameBoard_TPL<counters> board2 = board.Clone(); // initialise board 2 to a clone of the original board
            GameBoard_TPL<counters> board3 = board.Clone(); // initialise board 3 to a clone of the original board
            GameBoard_TPL<counters> board4 = board.Clone(); // initialise board 4 to a clone of the original board
            Console.WriteLine("*** Move {0}", move); // start of move
            List<Tuple<int, int>> unconsideredMoves = new List<Tuple<int, int>>(); // intialise blank list for unconsidered moves 
            // if board is a standard 7x7, set available moves to 49
            if (SEGM_BOARD == 0)
            {
                unconsideredMoves = getAvailableMoves(board, positions); 
            }
            // if board is a standard 3x3, set available moves to 9
            else if (SEGM_BOARD == 1)
            {
                unconsideredMoves = getAvailableSegmentedMoves(board, positions); 
            }
            int len = unconsideredMoves.Count; // assign len to the number of elements of the list of unconsideredMoves
            if (TPL_PARALLELINVOKE_ON == 0) // if TPL parallel invoking is turned off
            {
                sw_thr0.Start();
                ress[0] = ParSearchWork(board1, counter, ply, positions, true, scoreBoard, stride, 0, bestRes, 1, unconsideredMoves /* for DEBUGGING only */);
                sw_thr0.Stop();
                sw_thr1.Start();
                ress[1] = (len <= 1) ? worstRes : ParSearchWork(board2, counter, ply, positions, true, scoreBoard, stride, 1, bestRes, 2, unconsideredMoves /* for DEBUGGING only */);
                sw_thr1.Stop();
                sw_thr2.Start();
                ress[2] = (len <= 2) ? worstRes : ParSearchWork(board3, counter, ply, positions, true, scoreBoard, stride, 2, bestRes, 3, unconsideredMoves /* for DEBUGGING only */);
                sw_thr2.Stop();
                sw_thr3.Start();
                ress[3] = (len <= 3) ? worstRes : ParSearchWork(board4, counter, ply, positions, true, scoreBoard, stride, 3, bestRes, 4, unconsideredMoves /* for DEBUGGING only */);
                sw_thr3.Stop();
            }
            if (TPL_PARALLELINVOKE_ON == 1) // if TPL parallel invoking is turned on
            {
                 // number of cores for parallelism 
                 ParallelOptions po = new ParallelOptions();
                 po.MaxDegreeOfParallelism = no_of_cores_for_parallelism; // specify to number of cores to const value 
                Parallel.Invoke(po,
                    () =>
                {
                    Console.WriteLine("+++++++ PARALLELISM ON with " + po.MaxDegreeOfParallelism + " cores");
                        sw_thr0.Start();
                        ress[0] = ParSearchWork(board1, Flip(counter), ply, positions, true, scoreBoard, stride, 0, bestRes, 1, unconsideredMoves);
                        sw_thr0.Stop();
                    Console.WriteLine("#### THREAD 0 - StartAt: {0}, EndAt: {1}", sw_thr0.StartAt.Value, sw_thr0.EndAt.Value); // timestamp to identify level of thread distribution representation
                },
                () =>
                {
                        sw_thr1.Start();
                        ress[1] = ParSearchWork(board2, Flip(counter), ply, positions, true, scoreBoard, stride, 1, bestRes, 2, unconsideredMoves);
                        sw_thr1.Stop();
                    Console.WriteLine("#### THREAD 1 - StartAt: {0}, EndAt: {1}", sw_thr1.StartAt.Value, sw_thr1.EndAt.Value); // timestamp to identify level of thread distribution representation
                },
                () =>
                {
                        sw_thr2.Start();
                        ress[2] = ParSearchWork(board3, Flip(counter), ply, positions, true, scoreBoard, stride, 2, bestRes, 3, unconsideredMoves);
                        sw_thr2.Stop();
                    Console.WriteLine("#### THREAD 2 - StartAt: {0}, EndAt: {1}", sw_thr2.StartAt.Value, sw_thr2.EndAt.Value); // timestamp to identify level of thread distribution representation 
                },
                () =>
                {
                        sw_thr3.Start();
                        ress[3] = ParSearchWork(board4, Flip(counter), ply, positions, true, scoreBoard, stride, 3, bestRes, 4, unconsideredMoves);
                        sw_thr3.Stop();
                    Console.WriteLine("#### THREAD 3 - StartAt: {0}, EndAt: {1}", sw_thr3.StartAt.Value, sw_thr3.EndAt.Value); // timestamp to identify level of thread distribution representation
                });
            }
            bestRes = res = ress[0]; // assign best result to res to the result of thread 0
            Console.WriteLine("__ HWL: best result on board {0} and player {1} from thread 0: {2}", Game_TPL.cntr, counter /* Flip(counter) */, bestRes.ToString());
            if (counter == counters.O)
            {
                all_Oplacedmoves.Add(res.Item2);
            }
            if (counter == counters.X)
            {
                all_Xplacedmoves.Add(res.Item2);
            }
            // begin for loop
            for (int j = 1; j < ress.Length; j++)
            {              
                lock (TPL_THREADSYNC_LOCK) // lock for thread synchronisation
                {
                  Console.WriteLine("__ HWL: best result on board {0} and player {1} from thread {2}: {3}", Game_TPL.cntr, counter /* Flip(counter) */, j, ress[j].ToString());
                  res = (ress[j].Item1 > res.Item1) ? ress[j] : res;  // res is equal to: the score of current thread returned position if it is greater than the score of current val of res then.... (result display format: <score, <position>>)
                  board[res.Item2.Item1, res.Item2.Item2] = counter /* Flip(counter) */; // place res val on board with counter                    
                 
                    if (!Win(board, counter) || !Win(board, otherCounter))
                    {
                        if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                        {
                            Console.WriteLine("++LS X PLACED MOVES:" + showList(all_Xplacedmoves));
                            Console.WriteLine("++LS O PLACED MOVES:" + showList(all_Oplacedmoves));
                        }                     
                    }
                    if (j == 3)
                    {
                        Console.WriteLine("**** HWL: OVERALL best result on board {0} and player {1}: {2}", Game_TPL.cntr, counter /*Flip(counter)*/, res.ToString());
                        Console.WriteLine("-- LS Elapsed time for move: " + sw_move.Elapsed); // display elapsed for move consideration       
                        Console.WriteLine("Total number of considered positions:" + all_conmoves.Count);
                        Console.WriteLine("-"); // BP 
                    }
                    thr0_movesWithClear.Clear(); // clear list of positions considered by thread 0 for each move made (list is cleared after each move is made)  
                    thr1_movesWithClear.Clear(); // clear list of positions considered by thread 1 for each move made (list is cleared after each move is made)  
                    thr2_movesWithClear.Clear(); // clear list of positions considered by thread 2 for each move made (list is cleared after each move is made)  
                    thr3_movesWithClear.Clear(); // clear list of positions considered by thread 3 for each move made (list is cleared after each move is made)               
                                                 // if Win is detected, display thread running times (threads' 0 to 3)
                    if (Win(board, counter) || Win(board, otherCounter))
                    {
                        TimeSpan thr0 = sw_thr0.Elapsed;
                        TimeSpan thr1 = sw_thr1.Elapsed;
                        TimeSpan thr2 = sw_thr2.Elapsed;
                        TimeSpan thr3 = sw_thr3.Elapsed;
                        TimeSpan[] thr_times = new TimeSpan[] { thr0, thr1, thr2, thr3 };
                        TimeSpan maxthread_time = thr_times.Max();
                        move = 0; // set move number counter to 0 - reset when board is over
                        // display exec time for each time
                        Console.WriteLine("#### Thread 0 execution time: " + sw_thr0.Elapsed + ", with " + thr0_storemoves.Count + " positions visited.");
                        Console.WriteLine("#### Thread 1 execution time: " + sw_thr1.Elapsed + ", with " + thr1_storemoves.Count + " positions visited.");
                        Console.WriteLine("#### Thread 2 execution time: " + sw_thr2.Elapsed + ", with " + thr2_storemoves.Count + " positions visited.");
                        Console.WriteLine("#### Thread 3 execution time: " + sw_thr3.Elapsed + ", with " + thr3_storemoves.Count + " positions visited.");
                        // percentage utilisation of threads over entire game execution
                        if (thr_times.Max() == thr0)
                        {
                            var thr0_gamepercent = maxthread_time.Seconds / maxthread_time.Seconds;
                            var thr1_gamepercent = maxthread_time.Seconds - thr1.Seconds;
                            var thr2_gamepercent = maxthread_time.Seconds - thr2.Seconds;
                            var thr3_gamepercent = maxthread_time.Seconds - thr3.Seconds;
                            Console.WriteLine("*** Percentage utilisation of threads over entire game execution:");
                            Console.WriteLine("THR 0: " + thr0_gamepercent * 100 + " %");
                            Console.WriteLine("THR 1: " + (100 - thr1_gamepercent) + " %");
                            Console.WriteLine("THR 2: " + (100 - thr2_gamepercent) + " %");
                            Console.WriteLine("THR 3: " + (100 - thr3_gamepercent) + " %");
                        }
                        if (thr_times.Max() == thr1)
                        {
                            var thr0_gamepercent = maxthread_time.Seconds - thr0.Seconds;
                            var thr1_gamepercent = maxthread_time.Seconds / maxthread_time.Seconds;
                            var thr2_gamepercent = maxthread_time.Seconds - thr2.Seconds;
                            var thr3_gamepercent = maxthread_time.Seconds - thr3.Seconds;
                            Console.WriteLine("*** Percentage utilisation of threads over entire game execution:");
                            Console.WriteLine("THR 0: " + (100-thr0_gamepercent) + " %");
                            Console.WriteLine("THR 1: " + thr1_gamepercent * 100 + " %");
                            Console.WriteLine("THR 2: " + (100 - thr2_gamepercent) + " %");
                            Console.WriteLine("THR 3: " + (100 - thr3_gamepercent) + " %");
                        }
                        if (thr_times.Max() == thr2)
                        {
                            var thr0_gamepercent = maxthread_time.Seconds - thr0.Seconds;
                            var thr1_gamepercent = maxthread_time.Seconds - thr1.Seconds;
                            var thr2_gamepercent = maxthread_time.Seconds / maxthread_time.Seconds;
                            var thr3_gamepercent = maxthread_time.Seconds - thr3.Seconds;
                            Console.WriteLine("*** Percentage utilisation of threads over entire game execution:");
                            Console.WriteLine("THR 0: " + (100-thr0_gamepercent) + " %");
                            Console.WriteLine("THR 1: " + (100 - thr1_gamepercent) + " %");
                            Console.WriteLine("THR 2: " + thr2_gamepercent * 100 + " %");
                            Console.WriteLine("THR 3: " + (100 - thr3_gamepercent) + " %");
                        }
                        if (thr_times.Max() == thr3)
                        {
                            var thr0_gamepercent = maxthread_time.Seconds - thr0.Seconds;
                            var thr1_gamepercent = maxthread_time.Seconds - thr1.Seconds;
                            var thr2_gamepercent = maxthread_time.Seconds - thr2.Seconds;
                            var thr3_gamepercent = maxthread_time.Seconds / maxthread_time.Seconds;
                            Console.WriteLine("*** Percentage utilisation of threads over entire game execution:");
                            Console.WriteLine("THR 0: " + (100-thr0_gamepercent) + " %");
                            Console.WriteLine("THR 1: " + (100 - thr1_gamepercent) + " %");
                            Console.WriteLine("THR 2: " + (100 - thr2_gamepercent) + " %");
                            Console.WriteLine("THR 3: " + thr3_gamepercent * 100 + " %");
                        }
                        if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                        {
                            Console.WriteLine("++LS X PLACED MOVES:" + showList(all_Xplacedmoves));
                            Console.WriteLine("++LS O PLACED MOVES:" + showList(all_Oplacedmoves));
                        }
                    }
                }
                // if board is a standard 3x3, set available moves to 9
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
            // if game board print is turned on then
            if (EXECPRINT_GAMEBOARD_ON == 1)
            {
                lock (TPL_FILESYNC_LOCK) // lock to protect file synchronisation
                {

                    board.DisplayBoard(); // display board to console
                    board.DisplayFinBoardToFile();
                }
            }
                return res; // function return score and position of move selected
        }
        /*
     --------------------------------------------------------------------------------------------------------------------------
     ParSearchWork -
     --------------------------------------------------------------------------------------------------------------------------
     This method constructs the execution of the parallel search and brings on results of each parallel search complied
     together in a culminative summary.
     --------------------------------------------------------------------------------------------------------------------------
     */
        public Tuple<int, Tuple<int, int>> ParSearchWork(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positons, bool mmax, GameBoard_TPL<int> scoreBoard, int stride, int id, Tuple<int, Tuple<int, int>> bestRes, int thread_no, List<Tuple<int, int>> unconsideredMoves /* for DEBUGGING only */)
        {
            Tuple<int, Tuple<int, int>> res = new Tuple<int, Tuple<int, int>>(999, new Tuple<int, int>(9, 9)); // initialise res return value
            List<Tuple<int, int>> availableMoves = new List<Tuple<int, int>>(); // intialise blank list for available moves 
            // if board is a standard 7x7, set available moves to 49
            if (SEGM_BOARD == 0)
            {
                availableMoves = getAvailableMoves(board, positions);
            }
            // if board is a standard 3x3, set available moves to 9
            else if (SEGM_BOARD == 1)
            {
                availableMoves = getAvailableSegmentedMoves(board, positions); 
            }
            List<Tuple<int, int>> consideredMoves = new List<Tuple<int, int>>(); // intialise blank list for considered moves 
            List<Tuple<int, int>> duplicateMoves = new List<Tuple<int, int>>(); // intialise blank list for duplicate moves 
            Tuple<int, int> Move; // intialise current move tuple variant
            int score = Consts.MIN_SCORE; // current score of move set the min score constant
            int cnt = 0, offset = id; // set cnt to 0 and offset to thread id
            Debug.Assert(0 <= id && id < stride);  // Assertion: 0 <= id < stride
            counters us = counter; /* Flip(counter); */ // HWL: DONE: I don't think you should flip at this point, rather at the call to SeqSearch
            if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
            {
                Console.WriteLine("======================================================================================================");
                Console.WriteLine("-- THREAD " + id + ":");
                Console.WriteLine("======================================================================================================");
                Console.WriteLine("__ HWL: ParSearchWork called on board {0} with player {1} and thread id {2}", Game_TPL.cntr, us.ToString(), id);
                Console.WriteLine("__ HWL:   stride={0}, id={1}, thread_no={2}  ", stride, id, thread_no);
                System.Console.WriteLine("__ HWL:   Input board: ");
            }
                // if board is a standard 3x3, set available moves to 9
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
            // if the depth level is greater than the max depth level then
            if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, scoreBoard, us); // call stat evaluation func - takes board and Player_TPL and gives score to that Player_TPL
            }
            // start for loop
            for (int i = 0; i < availableMoves.Count; i++)
            {
                {
                    // if offset and cnt is 0
                    if (offset == 0 && cnt == 0)
                    {
                        // HWL: this is a move for the current thread to process; remember it (for debugging)
                        // if depth level is equal to 0
                        if (ply == 0)
                        {
                            consideredMoves.Add(availableMoves[i]); // add current considered move to considered moves list
                            // if thread 0
                            if (id == 0)
                            {
                                thr0_movesWithClear.Add(availableMoves[i]);  // add current considered move to thr0 considered moves list for each move
                                thr0_storemoves.Add(availableMoves[i]);  // add current considered move to thr0 considered moves list for entire cycle
                                all_conmoves.Add(availableMoves[i]);
                            }
                            // if thread 1
                            if (id == 1)
                            {
                                thr1_movesWithClear.Add(availableMoves[i]); // add current considered move to thr1 considered moves list for each move
                                thr1_storemoves.Add(availableMoves[i]);  // add current considered move to thr1 considered moves list for entire cycle
                                all_conmoves.Add(availableMoves[i]);
                            }
                            // if thread 2
                            if (id == 2)
                            {
                                thr2_movesWithClear.Add(availableMoves[i]); // add current considered move to thr2 considered moves list for each move
                                thr2_storemoves.Add(availableMoves[i]);  // add current considered move to thr2 considered moves list for entire cycle
                                all_conmoves.Add(availableMoves[i]);
                            }
                            // if thread 3
                            if (id == 3)
                            {
                                thr3_movesWithClear.Add(availableMoves[i]); // add current considered move to thr3 considered moves list for each move
                                thr3_storemoves.Add(availableMoves[i]);  // add current considered move to thr3 considered moves list for entire cycle
                                all_conmoves.Add(availableMoves[i]);
                            }
                        }
                        Move = availableMoves[i]; // current move to pick the next available move for this thread to consider
                        if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                        {
                            Console.WriteLine(".. HWL: ParSearchWork: considering move {0}", Move.ToString());
                            Console.WriteLine(".. {0} at {1} ", counter, Move.ToString());
                        }
                        // make the move
                        board[Move.Item1, Move.Item2] = us; // place counter
                        // check for an immediate win
                        if (FindThreeInARow(board, us))
                        {
                            if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                            {
                                Console.WriteLine("          3-in-a-row found at {3} for player {0} (ply={1}, positions={2})", counter, ply, positions.ToString(), Move.ToString());
                            }
                            board[Move.Item1, Move.Item2] = counters.e;     // blank the field again
                            return new Tuple<int, Tuple<int, int>>(1000, Move); // return win-in-1-move
                        }
                        // do a regular, sequential search to get to the score for this move
                        res = SeqSearch(board, Flip(counter), ply + 1, positions, false, scoreBoard, alpha, beta); // undo the move
                        board[Move.Item1, Move.Item2] = counters.e; // remove counter from board
                        bestRes = (-res.Item1 > bestRes.Item1) ? new Tuple<int, Tuple<int, int>>(-res.Item1, Move) : bestRes; // best result
                        cnt = stride - 1; // cnt is equal to stride - 1 (4-1)
                    }
                    else
                    {
                        // if offset is equal to 0, deincrement counter, else deincrement counter
                        if (offset == 0) { cnt--; } else { offset--; }
                    }
                    cont++; // increment positions visited counter
                }
                // if false and depth level is 0
                if (false /* HWL: prevent file access for now */&& ply == 0)
                {
                    if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                    {
                        // HWL: print the moves considered by current thread; they must not overlap!
                        Console.WriteLine("__ HWL: {0} consideredMoves so far (thread {1}): {2}", consideredMoves.Count, id, showList(consideredMoves));
                        Console.WriteLine("__ HWL: ALL {0} available Moves (thread {1}): {2} ", availableMoves.Count, id, showList(availableMoves));
                        Console.WriteLine("board " + Game_TPL.cntr + " processed by thread id: " + thread_no + " :");
                    }
                        // if board is a standard 3x3, set available moves to 9
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
           // if depth level isr 0
            if (ply == 0)
            {
                if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                {
                    Console.WriteLine("__ HWL: {0} consideredMoves so far (thread {1}): {2} ", consideredMoves.Count, id, showList(consideredMoves));
                    Console.WriteLine("__ HWL: {0} ALL available Moves (thread {1}): {2} ", availableMoves.Count, id, showList(availableMoves));
                    Console.WriteLine("thr0 " + thr0_movesWithClear.Count + " thr1 " + thr1_movesWithClear.Count + " thr2 " + thr2_movesWithClear.Count + " thr3 " + thr3_movesWithClear.Count);
                }
                    // HWL: remove all considered moves from the global list unconsideredMoves, to check that all moves are considered at the end
                foreach (var mv in consideredMoves)
                {
                    unconsideredMoves.Remove(mv);
                }
                if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                {
                    Console.WriteLine("__ HWL: best res so far: {0} ", bestRes.ToString()); // display best result
                }
                /*
   --------------------------------------------------------------------------------------------------------------------------
   Check that strided iteration is correct:
   No move is visited twice by more than one thread -
   --------------------------------------------------------------------------------------------------------------------------
   The code below creates lists of visited moves for each thread and check them against each other to check
   for any moves duplicated and visited more than one thread - this is inefficient. Any existing moves that 
   have been considered by more than one thread, that position will added to list of duplicated moves and 
   elements of this list will be printed to console. If there are no visited moves more than once then a
   console message will be displayed confirming there are no duplicated moves.
   --------------------------------------------------------------------------------------------------------------------------
                  */
                // series of foreach checking strided iteration between threads does not visit the same position (twice)
                foreach (Tuple<int, int> r in thr0_movesWithClear.Intersect(thr1_movesWithClear))
                {
                    duplicateMoves.Add(r); // add duplicate move between threads to duplicate list
                }
                foreach (Tuple<int, int> r in thr0_movesWithClear.Intersect(thr2_movesWithClear))
                {
                    duplicateMoves.Add(r);  // add duplicate move between threads to duplicate list
                }
                foreach (Tuple<int, int> r in thr0_movesWithClear.Intersect(thr3_movesWithClear))
                {
                    duplicateMoves.Add(r);  // add duplicate move between threads to duplicate list
                }
                foreach (Tuple<int, int> r in thr1_movesWithClear.Intersect(thr2_movesWithClear))
                {
                    duplicateMoves.Add(r);  // add duplicate move between threads to duplicate list
                }
                foreach (Tuple<int, int> r in thr1_movesWithClear.Intersect(thr3_movesWithClear))
                {
                    duplicateMoves.Add(r);  // add duplicate move between threads to duplicate list
                }
                foreach (Tuple<int, int> r in thr2_movesWithClear.Intersect(thr3_movesWithClear))
                {
                    duplicateMoves.Add(r);  // add duplicate move between threads to duplicate list
                }
                bool isEmpty = !duplicateMoves.Any();  // if duplicate list is not empty, then display elements of existing duplicate positions visited by threads to console
                // if duplicate list is not, then message to console confirming no duplicate positions visited by threads
                if (isEmpty)
                {
                    if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                    {
                        Console.WriteLine("++ LS: NO DUPLICATES FOUND IN THREAD CONSIDERED MOVES LIST");
                        Console.WriteLine("++ LS: Visited moves for thread 0: " + showList(thr0_movesWithClear));
                        Console.WriteLine("++ LS: Visited moves for thread 1: " + showList(thr1_movesWithClear));
                        Console.WriteLine("++ LS: Visited moves for thread 2: " + showList(thr2_movesWithClear));
                        Console.WriteLine("++ LS: Visited moves for thread 3: " + showList(thr3_movesWithClear));
                        Console.WriteLine("++ LS: Duplicate moves: " + showList(duplicateMoves));
                    }
                }
                // if duplicate list is not empty, then display elements of existing duplicate positions visited by threads to console
                if (!isEmpty)
                {
                    if (DEBUGPRINT_ON == 1)  // enable detailed print statements for debugging of combining of score and the adjacent move selection  
                    {
                        Console.WriteLine("++ LS: DUPLICATES FOUND IN THREAD CONSIDERED MOVES LIST");
                        Console.WriteLine("++ LS: Visited moves for thread 0: " + showList(thr0_movesWithClear));
                        Console.WriteLine("++ LS: Visited moves for thread 1: " + showList(thr1_movesWithClear));
                        Console.WriteLine("++ LS: Visited moves for thread 2: " + showList(thr2_movesWithClear));
                        Console.WriteLine("++ LS: Visited moves for thread 3: " + showList(thr3_movesWithClear));
                        Console.WriteLine("++ LS: Duplicate moves: " + showList(duplicateMoves));
                    }
                }
                /*
--------------------------------------------------------------------------------------------------------------------------
END: Check that strided iteration is correct:
No move is visited twice by more than one thread -
--------------------------------------------------------------------------------------------------------------------------
*/
            }
            return bestRes; // function returns best result with score and position
        }
        /*
----------------------------------------------------------------------------------------------------------------
 showList -
--------------------------------------------------------------------------------------------------------------------------
 A string method that returns a list of vacant positions (represented in a Tuple<int,int> format) left on the board.
--------------------------------------------------------------------------------------------------------------------------
*/
        public static string showList(List<Tuple<int, int>> xs)
        {
            string str = "";
            foreach (Tuple<int, int> t in xs)
            {
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
            counters us = counter /*Flip(counter) */;
            int move = 1; // initialise move to 1
            int numTasks = 1; // initialise numTasks variable to 1 
            int bestScore = mmax ? -1001 : 1001; // initalise best score
            int score = Consts.MIN_SCORE;  // current score of move set the min score constant
            Tuple<int, int> Move = new Tuple<int, int>(0, 0); // initialise current move tuple variable to 0,0 
            Tuple<int, int> bestMove = new Tuple<int, int>(0, 0); // initialise best move tuple variable to 0,0  // best move with score// THRESHOLD <=============
            // decs for random move 
            Random rnd = new Random();
            int randMoveX = rnd.Next(1, 7); // creates a number between 1 and 7
            int randMoveY = rnd.Next(1, 7); // creates a number between 1 and 7
            Tuple<int, int> randMove = new Tuple<int, int>(randMoveX, randMoveY); // initialise random coordinate position
            // print statement to confirm alpha-beta pruning is turne don
            if (PRUNE_ON == 1)
            {
                Console.WriteLine("++++ PRUNING ON");
            }
            // if depth level is 0 or 1
            if (ply == 0 || ply == 1)
                return ParSearchWrap(board, counter /*Flip(counter)*/, numTasks, scoreBoard); // return result from parallel search
            // if ply is greater than 1
            else if (ply > 1)
                return SeqSearch(board, Flip(counter), ply, positions, true, scoreBoard, alpha, beta); // return result from sequential search
            else // else: return and exit (should never be reached!)
            {
                Environment.Exit(97); // exit console
                return new Tuple<int, Tuple<int, int>>(bestScore, bestMove); // return current best move and score
            }
        }
    }
}
