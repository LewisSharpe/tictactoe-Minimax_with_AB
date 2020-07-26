using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Minimax_SYSTST
{
    // AIPLAYER CLASS
    class AIPlayer_SYSTST : Player_SYSTST
    {
        // PUBLIC DECS
        public int ply = 0;    // start depth for search (should be 0)
        public int maxPly = 2; // max depth for search
        public int alpha = Consts.MIN_SCORE;
        public int beta = Consts.MAX_SCORE;
        public static Tuple<int, int> positions = new Tuple<int, int>(2, 2);
        public static int cont = 0; // counter for number of nodes visited
        public static int error_confirm = 0;
        // Create new stopwatch.
        Stopwatch stopwatch = new Stopwatch();
        public AIPlayer_SYSTST(counters _counter) : base(_counter) { }
        // GENERATE LIST OF REMAINING AVAILABLE MOVES
        public List<Tuple<int, int>> getAvailableMoves(GameBoard_SYSTST<counters> board, Tuple<int, int> positions)
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
        public override Tuple<int, int> GetMove(GameBoard_SYSTST<counters> board, GameBoard_SYSTST<int> scoreBoard)
        {
            counters us = Flip(counter);
            // Begin timing.
            stopwatch.Start();
            // Do work
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>> result;
            result = Minimax(board, counter, ply, positions, true, scoreBoard, ref cont, alpha, beta); // 0,0
            Console.WriteLine("board " + Game_SYSTST.cntr + ":");
            board.DisplayBoard();
            Console.WriteLine("Player move: " + counter + " which, returns: " + result.Item1 + result.Item2);
            int score = result.Item1;
            var file = @"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_SYSTST//SYSTST_Report.csv";
            var date = DateTime.Now.ToShortDateString();
            var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
            var csv = new System.Text.StringBuilder();
            var title = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", "DATE", "TIME", "INT BOARD", "RESULT", "BOARD NO", "REASON", "SCORE", "X", "Y", "SIDE", "FIN BOARD", "SCORE BOARD", "POSITIONS VISTED", "DEPTH", "TIME ELAPSED", Environment.NewLine);
            csv.Append(title);
            File.AppendAllText(file, title.ToString());
            board.DisplayIntBoardToFile();
            board.DisplayFinBoardToFile();
       //   Console.WriteLine(Game_SYSTST.cntr + "NOWCOUNT" + Game_SYSTST.nowcount);
            string status = "-";
            if (result.Item1 == 1001 | result.Item1 == -1001 | result.Item1 == -1000 | result.Item1 == -1000)
            {
                status = "PASS";
            }
            else
            {
                status = "FAIL";
            }
            string reason = "Board combination missed";
            var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", date, time, board, status.ToString(), "Board " + int.Parse(Game_SYSTST.cntr.ToString()), reason.ToString(), result.Item1.ToString(), result.Item2.Item1.ToString(), result.Item2.Item2.ToString(), counter, Game_SYSTST.board, Game_SYSTST.scoreBoard, cont, ply, stopwatch.Elapsed, Environment.NewLine);
            csv.Append(newLine);
            File.AppendAllText(file, newLine.ToString());
            // Console.ReadLine();
            if (result.Item1 == 1000 & result.Item2 == new Tuple<int, int>(1,2))
            {
              //  Game_SYSTST.cntr++;
        //      Console.WriteLine(Game_SYSTST.cntr + "NOWCOUNT" + Game_SYSTST.nowcount);
      //        Console.ReadLine();
                error_confirm = 1;
                Console.Write("X ERROR on Board " + Game_SYSTST.cntr + " : Board combination missed" + Environment.NewLine);
                //          Console.ReadLine();
                board.DisplayBoard();
                
            }
            else if (result.Item1 == 100 || result.Item1 == -100 & result.Item2 == new Tuple<int, int>(2, 2))
            {
      //         Game_SYSTST.cntr++;
      //        Console.WriteLine(Game_SYSTST.cntr + "NOWCOUNT" + Game_SYSTST.nowcount);
               // Console.ReadLine();
                error_confirm = 1;
                Console.Write("X ERROR on Board " + Game_SYSTST.cntr + " : Board combination missed");
                // Console.ReadLine();
                board.DisplayBoard();
            }
            // Stop timing.
            stopwatch.Stop();
            // Return positions
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
        // SPECIFY DIRECTION
        public int GetNumForDir(int startSq, int dir, GameBoard_SYSTST<counters> board, counters us)
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

        // FIND ONE CELL OF SAME SYMBOL IN A ROW
        public bool FindOneInARow(GameBoard_SYSTST<counters> board, int ourindex, counters us)
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
        public bool FindTwoInARow(GameBoard_SYSTST<counters> board, counters us)
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
        public static Tuple<int, int> IsLeftofTwo(GameBoard_SYSTST<counters> board, counters us)
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
        public static Tuple<int, int> IsRightofTwo(GameBoard_SYSTST<counters> board, counters us)
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
        public bool FindTwoInARowWithAHorziGap(GameBoard_SYSTST<counters> board, counters us)
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
        public bool FindTwoInARowithAVerticalGap(GameBoard_SYSTST<counters> board, counters us)
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
        public static bool FindThreeInARow(GameBoard_SYSTST<counters> board, counters us)
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
        public static Tuple<int, int> IsLeftOfThree(GameBoard_SYSTST<counters> board, counters us)
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
        public static Tuple<int, int> IsCentreOfThree(GameBoard_SYSTST<counters> board, counters us)
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
        public static Tuple<int, int> IsRightOfThree(GameBoard_SYSTST<counters> board, counters us)
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
        // IS THERE A WINNING THREE IN A ROW?
        public int EvalForWin(GameBoard_SYSTST<counters> board, int ourindex, counters us)
        {
            // eval if move is win draw or loss
            if (FindThreeInARow(board, us)) // player win?
                return 1000; // player win confirmed
            else if (FindThreeInARow(board, us + 1)) // opponent win?
                return -1000; // opp win confirmed
            else if (FindTwoInARow(board, us)) // player win?
                return 100; // player win confirmed
            else if (FindTwoInARow(board, us + 1)) // opponent win?
                return -100; // opp win confirmed
            if (FindOneInARow(board, ourindex, us)) // player win?
                return 10; // player win confirmed
            else if (FindOneInARow(board, ourindex, us + 1)) // opponent win?
                return -10; // opp win confirmed
            else
                return 23; // dummy value
        }
        // STATIC EVALUATION FUNCTION
        public int EvalCurrentBoard(GameBoard_SYSTST<counters> board, GameBoard_SYSTST<int> scoreBoard, int ourindex, counters us)
        {
            // score decs
            int score = 0;
            // assign
            score = EvalForWin(board, ourindex, us); // 1 for win, 0 for unknown
            return score;
        }

        // MINIMAX FUNCTION
        public Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>> Minimax(GameBoard_SYSTST<counters> board, counters counter, int ply, Tuple<int, int> positions, bool mmax, GameBoard_SYSTST<int> scoreBoard, ref int cont, int alpha, int beta)
        {
            // decs
            counters us = Flip(counter);
            int ourindex = 1;
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            // create new list of Tuple<int,int>
            List<Tuple<int, Tuple<int, int>>> result_list = new List<Tuple<int, Tuple<int, int>>>();
            int bestScore = mmax ? -1001 : 1001;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move = new Tuple<int, int>(6, 2);

            Tuple<int, int> bestMove = new Tuple<int, int>(7, 1);  // best move with score// THRESHOLD <=============
                                                                   // add assertion here
                                                                   // decs for random move 
            Random rnd = new Random();
            int randMoveX = rnd.Next(1, 7); // creates a number between 1 and 7
            int randMoveY = rnd.Next(1, 7); // creates a number between 1 and 7
            Tuple<int, int> randMove = new Tuple<int, int>(randMoveX, randMoveY);
            // check win
            if (availableMoves.Count == 0)
            {
                return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(10, positions, board, scoreBoard);
            }
            // if board no over 40
            if (Win(board, counter))
            {
                Game_SYSTST.cntr++;
                Game_SYSTST.nowcount++;
          //      Console.WriteLine(Game_SYSTST.cntr + "NOWCOUNT" + Game_SYSTST.nowcount);
                Console.WriteLine("✓ PASS on Board " + Game_SYSTST.cntr + " : Winning combination found");
      //        board.DisplayBoard();
              //  Console.ReadLine();
                var file = @"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_SYSTST//SYSTST_report.csv";
                var date = DateTime.Now.ToShortDateString();
                var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
                var csv = new System.Text.StringBuilder();
                var title = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", "DATE", "TIME", "INT BOARD", "RESULT", "BOARD NO", "REASON", "SCORE", "X", "Y", "SIDE", "FIN BOARD", "SCORE BOARD", "POSITIONS VISTED", "DEPTH", "TIME ELAPSED", Environment.NewLine);
                csv.Append(title);
                File.AppendAllText(file, title.ToString());
                board.DisplayIntBoardToFile();
                board.DisplayFinBoardToFile();
                string status = "-";
                if (score == 1001 | score == -1001 | score == -1000 | score == -1000)
                {
                    status = "PASS";
                }
                else
                {
                    status = "FAIL";
                }
                string reason = "Winning combination found";
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", date, time, board, status.ToString(), "Board " + int.Parse(Game_SYSTST.cntr.ToString()), reason.ToString(), score.ToString(), positions.Item1.ToString(), positions.Item2.ToString(), counter, Game_SYSTST.board, Game_SYSTST.scoreBoard, cont, ply, stopwatch.Elapsed, Environment.NewLine);
                //var newLine = string.Format(Environment.NewLine + date + " " + time + "{0}", first, Environment.NewLine);
                csv.Append(newLine);
                    File.AppendAllText(file, newLine);
                return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(1000, positions, board, scoreBoard);
            }
            if (Win(board, this.otherCounter))
            {

                Game_SYSTST.cntr++;
                Game_SYSTST.nowcount++;
                Console.WriteLine(Game_SYSTST.cntr + "NOWCOUNT" + Game_SYSTST.nowcount);
               // Console.ReadLine();
                Console.WriteLine(Environment.NewLine + "✓ PASS on Board " + Game_SYSTST.cntr + " : Winning combination found");
         //    board.DisplayBoard();
             //   Console.ReadLine();
                var file = @"C://Users//Lewis//Desktop//files_150819//ttt_csharp_270719//Minimax_SYSTST//SYSTST_report.csv";
                var date = DateTime.Now.ToShortDateString();
                var time = DateTime.Now.ToString("HH:mm:ss"); //result 22:11:45
                var csv = new System.Text.StringBuilder();
                var title = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", "DATE", "TIME", "INT BOARD", "RESULT", "BOARD NO", "REASON", "SCORE", "X", "Y", "SIDE", "FIN BOARD", "SCORE BOARD", "POSITIONS VISTED", "DEPTH", "TIME ELAPSED", Environment.NewLine);
                csv.Append(title);
                File.AppendAllText(file, title.ToString());
                board.DisplayIntBoardToFile();
                board.DisplayFinBoardToFile();
                string status = "-";
                if (score == 1001 | score == -1001 | score == -1000 | score == -1000)
                {
                    status = "PASS";
                }
                else
                {
                    status = "FAIL";
                }
                string reason = "Winning combination found";
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}", date, time, board, status.ToString(), "Board " + int.Parse(Game_SYSTST.cntr.ToString()), reason.ToString(), score.ToString(), positions.Item1.ToString(), positions.Item2.ToString(), counter, Game_SYSTST.board, Game_SYSTST.scoreBoard, cont, ply, stopwatch.Elapsed, Environment.NewLine);
                //var newLine = string.Format(Environment.NewLine + date + " " + time + "{0}", first, Environment.NewLine);
                csv.Append(newLine);
                File.AppendAllText(file, newLine.ToString());
                return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(-1000, positions, board, scoreBoard);
            }
            else
            { 
                if (FindTwoInARow(board, counter) && Two(board, counter) && board[Move.Item1, Move.Item2] == counters.e)
                {
                    // return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(100, positions, board, scoreBoard);
                }
                else if (FindTwoInARow(board, this.otherCounter) && Two(board, this.otherCounter) && board[Move.Item1, Move.Item2] == counters.e)
                {
                    //  return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(-100, positions, board, scoreBoard);
                }
                else if (FindOneInARow(board, ourindex, this.otherCounter) && One(board, counter) && board[Move.Item1, Move.Item2] == counters.e)
                {
                    // return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(100, positions, board, scoreBoard);
                }
                else if (FindOneInARow(board, ourindex, this.otherCounter) && One(board, this.otherCounter) && board[Move.Item1, Move.Item2] == counters.e)
                {
                    //    return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(-100, positions, board, scoreBoard);
                }
            }
            if (Game_SYSTST.cntr >= 40)
            {
                Environment.Exit(99);
            }
            // CHECK DEPTH
            if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, scoreBoard, ourindex, us); // call stat evaluation func - takes board and player and gives score to that player
                return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(score, positions, board, scoreBoard);
            }
            GameBoard_SYSTST<counters> copy;
                // make copy original board
                copy = board.Clone(); // make copy board
                                      // copy.DisplayBoard(); // display copy board
                                      // cell priority - favour centre and corners
            int iteration = 0;
                for (int i = 0; i < availableMoves.Count; i++)
                {
                    Move = availableMoves[i]; // current move
                                              // cell priority - favour centre and corners
                                              // HWL: where do you actual place the piece for the position in Move? you don't do this here, just pass Move to the call of Minimax below; in the recursive call you then overwrite the input argument with a random move (see comment at start of Minimax; so you are actually not considering Move at all!
                                              // HWL: try placing the piece here, and below just use the score
                    copy[Move.Item1, Move.Item2] = counter; // place counter
                                                            // GameBoard_SYSTST board0 = MakeMove(board, move); // copies board - parallel ready

                    // ************************************************************************************************
                    // ************************************************************************************************
                    // ************************************** MAIN MINIMAX WORK ***************************************
                    // ************************************************************************************************
                    // ************************************************************************************************

                    // list defined in Minimax declarations
                    Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>> result = Minimax(copy, Flip(counter), ply + 1, Move, !mmax, scoreBoard, ref cont, alpha, beta); /* swap player */ // RECURSIVE call  
               
                    // trying to prevent preventing cell overwrite
                    copy[Move.Item1, Move.Item2] = counters.e; /*  counter; */ // HWL: remove counter that was tried in this iteration
                                                                                   // GameBoard_SYSTST board0 = MakeMove(board, move); // copies board - parallel ready

                    score = -result.Item1; // assign score
                    positions = result.Item2; // present position (x,y)

                    iteration++;

                // assign score to correct cell in score
                scoreBoard[result.Item2.Item1, result.Item2.Item2] = score;

                if (ply == 0)
                {
                    scoreBoard.DisplayBoard();
                    Console.WriteLine("Player move: " + counter + " which, returns: " + result.Item1 + result.Item2);
                    scoreBoard.DisplayScoreBoardToFile();
                }

                // if maximising                  
                if (/* true HWL || */ mmax)
                        {
                        alpha = score;
                            if (score > bestScore)
                            {
                                bestMove = Move;
                                bestScore = score;
                        Console.WriteLine("score board for fixed board no " + Game_SYSTST.cntr + ": ");
                        scoreBoard.DisplayBoard();
                    }
                            if (alpha > bestScore)
                            {
                                bestMove = Move;
                                bestScore = alpha;
                        Console.WriteLine("score board for fixed board no " + Game_SYSTST.cntr + ": ");
                        scoreBoard.DisplayBoard();
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
                            if (beta <= alpha)
                                bestScore = alpha;                  
                    return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(bestScore, positions, board, scoreBoard);
                    }

                    // ************************************************************************************************
                    // ************************************************************************************************
                    // ********************* END OF WHEN TO MAXIMISE, WHEN TO MINIMISE ********************************
                    // ******************************* WITH ALPHA-BETA PRUNING ****************************************
                    // ************************************************************************************************

                        cont++; // increment positions visited
               

            }
            return new Tuple<int, Tuple<int, int>, GameBoard_SYSTST<counters>, GameBoard_SYSTST<int>>(bestScore, bestMove, board, scoreBoard); // return
            }
        }
    }