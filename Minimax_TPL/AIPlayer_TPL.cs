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
        GameBoard_TPL<counters> copy;
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
        // GET MOVE
        public override Tuple<int, int> GetMove(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard)
        {
            bool mmax = true;
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            // Begin timing.
            stopwatch.Start();
            // Do work
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            var tasks = new List<Task>();
            Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>> result;
            foreach (Tuple<int, int> m in availableMoves)
            {
                var tmp = m;
                tasks.Add(Task.Factory.StartNew(() => Minimax(board, counter, ply, positions, mmax, scoreBoard, alpha, beta)));
            }
            result = Minimax(board, counter, ply, positions, true, scoreBoard, alpha, beta); // 0,0
            int score = result.Item1;
            board.DisplayBoard();
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
        // SPECIFY DIRECTION
        public int GetNumForDir(int startSq, int dir, GameBoard_TPL<counters> board, counters us)
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
        public bool FindOneInARow(GameBoard_TPL<counters> board, int ourindex, counters us)
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
        public Tuple<counters, Tuple<int, int>, Tuple<int, int>, int, int, int> PlyScoringSummary(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard) {
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
        public int EvalForWin(GameBoard_TPL<counters> board, int ourindex, counters us)
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
            if (FindOneInARow(board, ourindex, us)) // Player_TPL win?
                return 10; // Player_TPL win confirmed
            else if (FindOneInARow(board, ourindex, us + 1)) // opponent win?
                return -10; // opp win confirmed
            else
                return 23; // dummy value
        }
        // STATIC EVALUATION FUNCTION
        public int EvalCurrentBoard(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard, int ourindex, counters us)
        {
            // score decs
            int score = 10;
            int two_score = 10;
            int one_score = 10;
            // assign
            score = EvalForWin(board, ourindex, us); // 1 for win, 0 for unknown
            // assign two score
            if (FindTwoInARow(board, us)) // Player_TPL win?
                score = 100; // twoinrow confirmed
            two_score = 100;
            if (FindTwoInARow(board, us + 1)) // twoinrow opponent?
                score = -100; // twoinrow confirmed
            two_score = -100;
            // one score
            if (FindOneInARow(board, ourindex, us)) // oneinrow?
                score = 10; // oneinrow confirmed
            one_score = 10;
            if (FindOneInARow(board, ourindex, us + 1)) // oneinarow opponent?
                score = -10; // oneinrow confirmed
            one_score = -10;

            // ************************************************************************************************
            // ************************************************************************************************
            // ******** ASSIGN MORE WEIGHT TO INDIVIDUAL CELLS WITH MORE PROMINENT POSITIONING TO BUILD ON ****
            // ************************************************************************************************
            // ************************************************************************************************

            // assign more weight to score with individual cell moves with prominent positioning
            if (copy.IsMiddleEmpty() == true & FindTwoInARow(board, us))
            {
                score = 100;
                // Player_TPL win?
                copy[4, 4] = counter;
                if (copy[4, 4] == counter)
                {
                    return score = 125; // Player_TPL win confirmed
                }
                return score = 10;
            }
            // assign more weight to score with individual cell moves with prominent positioning
            if (copy.IsMiddleEmpty() == true & FindTwoInARow(board, us))
            {
                score = 100;
                // Player_TPL win?
                copy[4, 4] = counter;
                if (copy[4, 4] == counter)
                {
                    // assign score to correct cell in score
                    scoreBoard[4, 4] = score;
                    return score = 125; // Player_TPL win confirmed
                }
                return score = 10;
            }
            else if (copy.IsMiddleEmpty() == true & FindTwoInARow(board, us + 1))
            {
                score = -100;
                // Player_TPL win?
                copy[4, 4] = counter;
                if (copy[4, 4] == counter)
                {
                    // assign score to correct cell in score
                    scoreBoard[4, 4] = score;
                    return score = -125; // opponent win confirmed
                }
                return score = 10;
            }
            if (copy.IsTopLeftEmpty() == true & FindTwoInARow(board, us))
            {
                score = 100;
                // Player_TPL win?
                copy[1, 1] = counter;
                if (copy[1, 1] == counters.EMPTY)
                {
                    // assign score to correct cell in score
                    scoreBoard[1, 1] = score;
                    return score = 115; // Player_TPL win confirmed
                }
                return score = 10;
            }
            else if (copy.IsTopLeftEmpty() == true & FindTwoInARow(board, us + 1))
            {
                score = -100;
                // Player_TPL win?
                copy[1, 1] = counter;
                if (copy[1, 1] == counters.EMPTY)
                {
                    // assign score to correct cell in score
                    scoreBoard[1, 1] = score;
                    return score = -115; // opponent win confirmed
                }
                return score = 10;
            }
            if (copy.IsTopRightEmpty() == true & FindTwoInARow(board, us))
            {
                score = 100;
                // Player_TPL win?
                copy[7, 1] = counter;
                if (copy[7, 1] == counters.EMPTY)
                {
                    // assign score to correct cell in score
                    scoreBoard[7, 1] = score;
                    return score = 115; // Player_TPL win confirmed
                }
                return score = 10;
            }
            else if (copy.IsTopRightEmpty() == true & FindTwoInARow(board, us + 1))
            {
                score = -100;
                // Player_TPL win?
                copy[7, 1] = counter;
                if (copy[7, 1] == counters.EMPTY)
                {
                    // assign score to correct cell in score
                    scoreBoard[7, 1] = score;
                    return score = -115; // opponent win confirmed
                }
                return score = 10;
            }
            if (copy.IsBottomLeftEmpty() == true & FindTwoInARow(board, us))
            {
                score = 100;
                // Player_TPL win?
                copy[1, 7] = counter;
                if (copy[1, 7] == counters.EMPTY)
                {
                    // assign score to correct cell in score
                    scoreBoard[1, 7] = score;
                    return score = 115; // Player_TPL win confirmed
                }
                return score = 10;
            }
            else if (copy.IsBottomLeftEmpty() == true & FindTwoInARow(board, us + 1))
            {
                score = -100;
                // Player_TPL win?
                copy[1, 7] = counter;
                if (copy[1, 7] == counters.EMPTY)
                {
                    // assign score to correct cell in score
                    scoreBoard[1, 7] = score;
                    return score = -115; // opponent win confirmed
                }
                return score = 10;
            }
            if (copy.IsBottomRightEmpty() == true & FindTwoInARow(board, us))
            {
                score = 100;
                // Player_TPL win?
                copy[7, 7] = counter;
                if (copy[7, 7] == counters.EMPTY)
                {
                    // assign score to correct cell in score
                    scoreBoard[7, 7] = score;
                    return score = 115;
                }
                return score = 10;
            }
            else if (copy.IsBottomRightEmpty() == true & FindTwoInARow(board, us + 1))
            {
                two_score = -100;
                // Player_TPL win?
                copy[7, 7] = counter;
                if (copy[7, 7] == counters.EMPTY)
                {
                    // assign score to correct cell in score
                    scoreBoard[7, 7] = score;
                    return score = -115; // opponent win confirmed
                }
            }
            // ************************************************************************************************
            // ************************************************************************************************
            // ** END OF ASSIGN MORE WEIGHT TO INDIVIDUAL CELLS WITH MORE PROMINENT POSITIONING TO BUILD ON **
            // ************************************************************************************************
            // ************************************************************************************************

            // ************************************************************************************************
            // ************************************************************************************************
            // ************************* ASSIGNING LESSER VALUE TO EDGES ***********************************
            // ************************************************************************************************
            // ************************************************************************************************

            // ************************************************************************************************
            // ************************************************************************************************
            // ************************* END OF ASSIGNING LESSER VALUE TO EDGES *******************************
            // ************************************************************************************************
            // ************************************************************************************************

            /*  // if one in a row, if two in a row found, three in a row found etc....
              if (score == -1000 || score == 1000)
              {
                  board.DisplayBoard();
                  //         Console.Write("three: " + score);
                  //  Console.ReadLine();

                  return score * Consts.MAX_SCORE;
              } 
            */
            if (two_score != 0)
            {
                // board.DisplayBoard();
                //       Console.Write("two: " + two_score);
                //Console.ReadLine();
                return two_score;
            }
            if (one_score != 0)
            {
                //board.DisplayBoard();
                //       Console.Write("one: " + one_score);
                return one_score;
            }
            else
                return score = 10;
        }

        public Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>> MakeRandomMove(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positions, bool mmax, GameBoard_TPL<int> scoreBoard, ref int cont)
        {
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            Tuple<int, int> Move = new Tuple<int, int>(0, 0);
            Random rnd = new Random();
            int randMoveX = rnd.Next(1, 7); // creates a number between 1 and 7
            int randMoveY = rnd.Next(1, 7); // creates a number between 1 and 7
            Tuple<int, int> randMove = new Tuple<int, int>(randMoveX, randMoveY);
            int score = Consts.MIN_SCORE; // current score of move
            counters us = Flip(counter);
            int ourindex = 1;
            // Create new stopwatch.
            Stopwatch stopwatch = new Stopwatch();
            // Begin timing.
            stopwatch.Start();
            Move = randMove;
            positions = randMove; // HWL: NB: this overwrites the arg positions; which means you are actually not considering the move in positions, but a different, random move; this probably explains why you get such strange moves in your gameplay
            board.DisplayBoard();
            //cont = 1;
            if (ply > 0)
            {
                score = EvalCurrentBoard(board, scoreBoard, ourindex, us);  // is current pos a win?
            }
            // Stop timing.
            stopwatch.Stop();
            Console.WriteLine("========================================================================================================================" +
                       "RANDOMLY SELECTED MOVE:" + Environment.NewLine + "------------------------------------------------------------------------------------------------------------------------" +
                       "position: " + positions + "; " +
                       "for Player_TPL: " + counter + "; " +
                       "score: " + score + "; " +
                       "positions visited " + cont + "; " +
                       "depth level: " + ply + Environment.NewLine +
                       "elapsed time for move: " + stopwatch.Elapsed + "; " +
                       "no. of remaining moves left: " + availableMoves.Count + Environment.NewLine +
                       "two in a row detected at: " + "Cell 1: " + IsLeftofTwo(board, counter) + ", " + "Cell 2: " + IsRightofTwo(board, counter)
                       + Environment.NewLine +
                       "build on two-in-row? " + "left: " + copy.IsTwoLeftNeighbourEmpty(board, counter) + " at position " + copy.PrintTwoLeftNeighbour(board, counter) +
                       ", right: " + copy.IsTwoRightNeighbourEmpty(board, counter) + " at position " + copy.PrintTwoRightNeighbour(board, counter) + Environment.NewLine +
                       "top: " + copy.IsTwoTopNeighbourEmpty(board, counter) + " at position " + copy.PrintTwoTopNeighbour(board, counter) +
                       ", bottom: " + copy.IsTwoBottomNeighbourEmpty(board, counter) + " at position " + copy.PrintTwoBottomNeighbour(board, counter) + Environment.NewLine
                        + "build on one-in-row? " + "left: " + copy.IsOneLeftNeighbourEmpty(board, counter) + " at position " + copy.PrintOneLeftNeighbour(board, counter) +
                       ", right: " + copy.IsOneRightNeighbourEmpty(board, counter) + " at position " + copy.PrintOneRightNeighbour(board, counter) + Environment.NewLine +
                       "top: " + copy.IsOneTopNeighbourEmpty(board, counter) + " at position " + copy.PrintOneTopNeighbour(board, counter) +
                       ", bottom: " + copy.IsOneBottomNeighbourEmpty(board, counter) + " at position " + copy.PrintOneBottomNeighbour(board, counter) + Environment.NewLine +
                       "build on two-in-row?:" + " with horzi gap? " + copy.IsTwoWithHorziGapEmpty(board, counter) + " at position " + copy.PrintTwoWithHorziGap(board, counter) +
                        ", with vertical gap?: " + copy.IsTwoWithVerticalGapEmpty(board, counter) + " at position " + copy.PrintTwoWithVerticalGap(board, counter));
            Console.WriteLine("========================================================================================================================");
            // Console.ReadLine();
            // assign score to correct cell in score
            scoreBoard[randMoveX, randMoveY] = score;
            // RandScoringSummary(board, scoreBoard);
            //  scoreBoard.DisplayBoard();
            // Console.ReadLine();
            return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(score, positions, board, scoreBoard);
        }
        // MINIMAX FUNCTION
        public Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>> Minimax(GameBoard_TPL<counters> board, counters counter, int ply, Tuple<int, int> positions, bool mmax, GameBoard_TPL<int> scoreBoard, int alpha, int beta)
        {
            // decs
            counters us = Flip(counter);
            int ourindex = 1;
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board, positions);
            // create new list of Tuple<int,int>
            List<Tuple<int, Tuple<int, int>>> result_list = new List<Tuple<int, Tuple<int, int>>>();
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
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(10, positions, board, scoreBoard);
            }
            else if (Win(board, counter))
            {
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(1000, positions, board, scoreBoard);
            }
            else if (Win(board, this.otherCounter))
            {
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(-1000, positions, board, scoreBoard);
            }

            if (FindTwoInARow(board, counter) && Two(board, counter) && copy[Move.Item1, Move.Item2] == counters.EMPTY)
            {
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(100, positions, board, scoreBoard);
            }
            else if (FindTwoInARow(board, this.otherCounter) && Two(board, this.otherCounter) && copy[Move.Item1, Move.Item2] == counters.EMPTY)
            {
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(-100, positions, board, scoreBoard);
            }
            else if (FindOneInARow(board, ourindex, this.otherCounter) && One(board, counter) && copy[Move.Item1, Move.Item2] == counters.EMPTY)
            {
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(100, positions, board, scoreBoard);
            }
            else if (FindOneInARow(board, ourindex, this.otherCounter) && One(board, this.otherCounter) && copy[Move.Item1, Move.Item2] == counters.EMPTY)
            {
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(-100, positions, board, scoreBoard);
            }

            // CHECK DEPTH
            else if (ply > maxPly)
            {
                score = EvalCurrentBoard(board, scoreBoard, ourindex, us); // call stat evaluation func - takes board and Player_TPL and gives score to that Player_TPL
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(score, positions, board, scoreBoard);
            }
            // Console.WriteLine("HWL: Is this line reached?");
            // Console.ReadLine(); // HWL: even with a ReadLine here, the program runs to the end
            // place random move
            if (board.IsEmpty()) // if board is empty then play random move
            {
                // decide scoring for random move
                score = EvalForWin(board, ourindex, us); // 1 for win, 0 for unknown
                // assign two score
                if (FindTwoInARow(board, us)) // Player_TPL win?
                    score = 100; // twoinrow confirmed
                if (FindTwoInARow(board, us + 1)) // twoinrow opponent?
                    score = -100; // twoinrow confirmed
                if (FindOneInARow(board, ourindex, us)) // oneinrow?
                    score = 10; // oneinrow confirmed
                if (FindOneInARow(board, ourindex, us + 1)) // oneinarow opponent?
                    score = -10; // oneinrow confirmed

                // MakeRandomMove(board, counter, ply, positions, !mmax, scoreBoard, ref cont);

                //  Console.ReadLine();
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(score, positions, board, scoreBoard);
            }
            // else run priority moves and Minimax
          
                // make copy original board
                copy = board.Clone(); // make copy board
                                      // copy.DisplayBoard(); // display copy board
                                      // cell priority - favour centre and corners


                /* Parallel version, using only 2 tasks */
                int k = 2;
                var options = new ParallelOptions() { MaxDegreeOfParallelism = k };
                // if ply is 0 or 1
                Parallel.For(0, availableMoves.Count, options, i =>
                {
                    // make copy 
                    //board.Clone(); - ONE THAT IS PROCESSED, NOT THE ORIGINAL BOARD, less than the 0 or 1 parallel for loop, else sequential for loop
                    Move = availableMoves[i]; // current move
                                              // cell priority - favour centre and corners
                                              // HWL: where do you actual place the piece for the position in Move? you don't do this here, just pass Move to the call of Minimax below; in the recursive call you then overwrite the input argument with a random move (see comment at start of Minimax; so you are actually not considering Move at all!
                                              // HWL: try placing the piece here, and below just use the score

                    copy[Move.Item1, Move.Item2] = counter; // place counter

                    // ************************************************************************************************
                    // ************************************************************************************************
                    // ************************************** MAIN MINIMAX WORK ***************************************
                    // ************************************************************************************************
                    // ************************************************************************************************

                    // list defined in Minimax declarations
                    Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>> result = Minimax(copy, Flip(counter), ply + 1, Move, !mmax, scoreBoard, alpha, beta); /* swap Player_TPL */ // RECURSIVE call  

                    // trying to prevent preventing cell overwrite
                    copy[Move.Item1, Move.Item2] = counters.EMPTY; /*  counter; */ // HWL: remove counter that was tried in this iteration
                                                                                   // GameBoard_TPL board0 = MakeMove(board, move); // copies board - parallel ready

                    score = -result.Item1; // assign score
                    positions = result.Item2; // present position (x,y)

                    // assign score to correct cell in score
                    scoreBoard[result.Item2.Item1, result.Item2.Item2] = score;
                    // HWL: summarise the result of having tried Move, print the assoc scoreboard and check that the matching move is the one for the highest score on the board
                    /*    Console.WriteLine(mmax.ToString() +
                    " **HWL (ply={0}) Trying Move ({4},{5}) gives score {1} and position ({2},{3})  [[so far bestScore={6}, bestMove=({7},{8})",
                          ply, score, result.Item2.Item1, result.Item2.Item2, Move.Item1, Move.Item2,
                          bestScore, bestMove.Item1, bestMove.Item2);
                          */
                    //   scoreBoard.DisplayBoard();
                    if (score == Consts.MIN_SCORE || score == Consts.MAX_SCORE)
                    {
                        /*   Console.WriteLine("**HWL CLAIM: putting piece {0} at position ({1},{2}) gives 3-in-a-row:",
                                 counter, Move.Item1, Move.Item2);
                                 */
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
                            bestMove = Move;
                            bestScore = score;
                        }
                        if (beta <= alpha)
                            bestScore = alpha;

                    }



                    // ************************************************************************************************
                    // ************************************************************************************************
                    // ******************************** END OF MAIN MINIMAX WORK **************************************
                    // ************************************************************************************************
                    // ************************************************************************************************

                    // ************************************************************************************************
                    // ************************* WHEN TO MAXIMISE, WHEN TO MINIMISE ***********************************
                    // ******************************* WITH ALPHA-BETA PRUNING ****************************************
                    // ************************************************************************************************
                    // ************************************************************************************************





                    // ************************************************************************************************
                    // ************************************************************************************************
                    // ********************* END OF WHEN TO MAXIMISE, WHEN TO MINIMISE ********************************
                    // ******************************* WITH ALPHA-BETA PRUNING ****************************************
                    // ************************************************************************************************

                    // ************************************************************************************************
                    // ************************************************************************************************
                    // ***************** PRIORITY MOVES - PRIORITISE PRIME POSITIONS ON BOARD  ************************
                    // ************************************************************************************************
                    // ************************************************************************************************
                    // IF MIDDLE CELL (4,4) IS EMPTY
                    if (copy.IsMiddleEmpty() == true)
                    {
                        // if middle (4,4) then choose the defined nearest positions to the cell in list
                        List<Tuple<int, int>> nodelist = new List<Tuple<int, int>>();
                        nodelist.Add((new Tuple<int, int>(4, 4)));
                        nodelist.Add((new Tuple<int, int>(3, 4)));
                        nodelist.Add((new Tuple<int, int>(5, 4)));
                        nodelist.Add((new Tuple<int, int>(3, 3)));
                        nodelist.Add((new Tuple<int, int>(3, 5)));
                        nodelist.Add((new Tuple<int, int>(5, 3)));
                        nodelist.Add((new Tuple<int, int>(5, 5)));
                        // copy.DisplayBoard();

                        for (int indexer = 0; indexer < nodelist.Count; indexer++)
                        {
                            int x = nodelist[indexer].Item1;
                            int y = nodelist[indexer].Item2;
                            if (copy[x, y] == counters.EMPTY)
                            {
                                //   cont = 1;
                                // assign score to correct cell in score
                                // make copy of board
                                scoreBoard[x, y] = score;
                                //     scoreBoard.DisplayBoard();
                                //    PriorityScoringSummary(board, scoreBoard);
                                // Console.ReadLine();

                                score = 125;
                            }
                        }
                    }

                    // ************************************************************************************************
                    // ************************************************************************************************
                    // ***************** END OF PRIORITY MOVES - PRIORITISE PRIME POSITIONS ON BOARD  *****************
                    // ************************************************************************************************
                    // ************************************************************************************************
                    else
                    {

                        // ************************************************************************************************
                        // ************************************************************************************************
                        // ***************** PRIORITY MOVES - CHECKING NEIGHBOURING CELLS TO EXISTING COUNTERS ************
                        // ************************************************************************************************
                        // ************************************************************************************************

                        // IS NEIGHBOUR TO CURRENT CELL EMPTY
                        if (copy.IsOneLeftNeighbourEmpty(board, counter) & FindOneInARow(board, ourindex, us) == true)
                        {
                            score = 10;
                            Tuple<int, int> neigh = copy.PrintOneLeftNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            score = 105;

                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            //  Console.ReadLine();
                                        }
                                    }
                                }
                            }
                        }     // IS NEIGHBOUR TO CURRENT CELL EMPTY
                        else if (copy.IsOneLeftNeighbourEmpty(board, counter) & FindOneInARow(board, ourindex, us + 1) == true)
                        {
                            score = -10;
                            Tuple<int, int> neigh = copy.PrintOneLeftNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            score = -105;
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            //    Console.ReadLine();
                                        }
                                    }
                                }
                            }
                        }
                        // IS NEIGHBOUR TO CURRENT CELL EMPTY                        // IS NEIGHBOUR TO CURRENT CELL EMPTY
                        else if (copy.IsOneRightNeighbourEmpty(board, counter) & FindOneInARow(board, ourindex, us) == true)
                        {
                            score = 10;
                            Tuple<int, int> neigh = copy.PrintOneRightNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            score = 105;
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            //      Console.ReadLine();
                                        }
                                    }
                                }
                            }
                        }
                        // IS NEIGHBOUR TO CURRENT CELL EMPTY
                        else if (copy.IsOneRightNeighbourEmpty(board, counter) & FindOneInARow(board, ourindex, us + 1) == true)
                        {
                            score = -10;
                            Tuple<int, int> neigh = copy.PrintOneRightNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            //     Console.ReadLine();
                                            score = -105;
                                        }
                                    }
                                }
                            }
                        }
                        // IS NEIGHBOUR TO CURRENT CELL EMPTY
                        else if (copy.IsOneTopNeighbourEmpty(board, counter) & FindOneInARow(board, ourindex, us) == true)
                        {
                            score = 10;
                            Tuple<int, int> neigh = copy.PrintOneTopNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            //Console.ReadLine();
                                            score = 105;
                                        }
                                    }
                                }
                            }
                        }
                        // IS NEIGHBOUR TO CURRENT CELL EMPTY
                        else if (copy.IsOneTopNeighbourEmpty(board, counter) & FindOneInARow(board, ourindex, us + 1) == true)
                        {
                            score = -10;
                            Tuple<int, int> neigh = copy.PrintOneTopNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            score = -105;
                                            //        Console.ReadLine();
                                        }
                                    }
                                }
                            }
                        }
                        // IS NEIGHBOUR TO CURRENT CELL EMPTY
                        else if (copy.IsOneBottomNeighbourEmpty(board, counter) & FindOneInARow(board, ourindex, us) == true)
                        {
                            score = 10;
                            Tuple<int, int> neigh = copy.PrintOneBottomNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            score = 105;
                                            //               Console.ReadLine();
                                        }
                                    }
                                }
                            }
                        }
                        // IS NEIGHBOUR TO CURRENT CELL EMPTY
                        else if (copy.IsOneBottomNeighbourEmpty(board, counter) & FindOneInARow(board, ourindex, us + 1) == true)
                        {
                            score = -10;
                            Tuple<int, int> neigh = copy.PrintOneBottomNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            score = -105;
                                            //             Console.ReadLine();
                                        }
                                    }
                                }
                            }
                        }
                        // FIND TWO IN A ROW WITH GAP HORIZONTALLY LEFT
                        if (copy.IsTwoLeftNeighbourEmpty(board, counter) & FindTwoInARow(board, us) == true)
                        {
                            score = 105;
                            Tuple<int, int> neigh = copy.PrintTwoLeftNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            score = 1000;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND TWO IN A ROW WITH GAP HORIZONTALLY LEFT
                        else if (copy.IsTwoLeftNeighbourEmpty(board, counter) & FindTwoInARow(board, us + 1) == true)
                        {
                            score = -105;
                            Tuple<int, int> neigh = copy.PrintTwoLeftNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            score = -1000;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND TWO IN A ROW WITH GAP HORIZONTALLY RIGHT
                        else if (copy.IsTwoRightNeighbourEmpty(board, counter) & FindTwoInARow(board, us) == true)
                        {
                            score = 105;
                            Tuple<int, int> neigh = copy.PrintTwoRightNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            score = 1000;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND TWO IN A ROW WITH GAP HORIZONTALLY RIGHT
                        else if (copy.IsTwoRightNeighbourEmpty(board, counter) & FindTwoInARow(board, us + 1) == true)
                        {
                            score = -105;
                            Tuple<int, int> neigh = copy.PrintTwoRightNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            score = -1000;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND TWO IN A ROW VERTICAL ABOVE WITH GAP
                        else if (copy.IsTwoTopNeighbourEmpty(board, counter) & FindTwoInARow(board, us) == true)
                        {
                            score = 105;
                            Tuple<int, int> neigh = copy.PrintTwoTopNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            score = 1000;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND TWO IN A ROW VERTICAL ABOVE WITH GAP
                        else if (copy.IsTwoTopNeighbourEmpty(board, counter) & FindTwoInARow(board, us + 1) == true)
                        {
                            score = -105;
                            Tuple<int, int> neigh = copy.PrintTwoTopNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            score = -1000;
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND TWO IN A ROW VERTICAL DOWN WAY WITH GAP
                        else if (copy.IsTwoBottomNeighbourEmpty(board, counter) & FindTwoInARow(board, us) == true)
                        {
                            score = 105;
                            Tuple<int, int> neigh = copy.PrintTwoBottomNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                            score = 1000;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND TWO IN A ROW VERTICAL DOWN WAY WITH GAP
                        else if (copy.IsTwoBottomNeighbourEmpty(board, counter) & FindTwoInARow(board, us + 1) == true)
                        {
                            score = -105;
                            Tuple<int, int> neigh = copy.PrintTwoBottomNeighbour(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            score = -1000;
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND HORIZONTAL TWO IN A ROW WITH GAP
                        else if (copy.IsTwoWithHorziGapEmpty(board, counter) & FindTwoInARow(board, us) == true)
                        {
                            score = -105;
                            Tuple<int, int> neigh = copy.PrintTwoWithHorziGap(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            score = -1000;
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND HORIZONTAL TWO IN A ROW WITH GAP
                        else if (copy.IsTwoWithHorziGapEmpty(board, counter) & FindTwoInARow(board, us + 1) == true)
                        {
                            score = -105;
                            Tuple<int, int> neigh = copy.PrintTwoWithHorziGap(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            score = -1000;
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND VERTICAL TWO IN A ROW WITH GAP
                        else if (copy.IsTwoWithVerticalGapEmpty(board, counter) & FindTwoInARow(board, us) == true)
                        {
                            score = -105;
                            Tuple<int, int> neigh = copy.PrintTwoWithVerticalGap(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            score = -1000;
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                        }
                                    }
                                }
                            }
                        }
                        // FIND VERTICAL TWO IN A ROW WITH GAP
                        else if (copy.IsTwoWithVerticalGapEmpty(board, counter) & FindTwoInARow(board, us + 1) == true)
                        {
                            score = -105;
                            Tuple<int, int> neigh = copy.PrintTwoWithVerticalGap(board, counter);
                            if (neigh.Item1 < 0 & neigh.Item1 > 7)
                            {
                                if (neigh.Item2 < 0 & neigh.Item2 > 7)
                                {
                                    if (copy[neigh.Item1, neigh.Item2] == counters.EMPTY)
                                    {
                                        copy[neigh.Item1, neigh.Item2] = counter;
                                        positions = new Tuple<int, int>(neigh.Item1, neigh.Item2);
                                        if (copy[neigh.Item1, neigh.Item2] == counter)
                                        {
                                            score = -1000;
                                            // assign score to correct cell in score
                                            scoreBoard[neigh.Item1, neigh.Item2] = score;
                                        }
                                    }
                                }

                            }

                        }
                    }


                    // ************************************************************************************************
                    // ************************************************************************************************
                    // *********** END OF PRIORITY MOVES - CHECKING NEIGHBOURING CELLS TO EXISTING COUNTERS ***********
                    // ************************************************************************************************
                    // ************************************************************************************************
                    cont++; // increment positions visited

                });
                return new Tuple<int, Tuple<int, int>, GameBoard_TPL<counters>, GameBoard_TPL<int>>(bestScore, bestMove, board, scoreBoard); // return
            } 
          }
         }
       
    
                // only if ply is 0 then
                // display score board for potential moves on ONLY this current iteration board
                // scoreBoard.DisplayBoard();
                // PlyScoringSummary(board, scoreBoard);
                //  Console.ReadLine();

    /*
     ===============================
     TO DO
     ===============================
     Check for level between switch of loops
     Lock update for all global vars
     Seq version - return in all cases, or just update the score
     Change returns to score updates
     Print score board, and redirect to file, check for reasonable score, why miss three in row?

      */