﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Minimax_TPL
{
    // GAME BOARD STRUCTURE CLASS
    struct GameBoard_TPL <T> where T: IComparable
    {
        // cell ids
        public T s1, s2, s3, s4, s5, s6, s7,
                         s8, s9, s10, s11, s12, s13, s14,
                         s15, s16, s17, s18, s19, s20, s21,
                         s22, s23, s24, s25, s26, s27, s28,
                         s29, s30, s31, s32, s33, s34, s35,
                         s36, s37, s38, s39, s40, s41, s42,
                         s43, s44, s45, s46, s47, s48, s49;
        public T filler; // empty filler space for cell
        private static Object thisLock = new Object();

        // 1 BLANK BOARD
        public GameBoard_TPL(T _filler)
        {
            filler = _filler; // blank filler
            s1 = filler; s2 = filler; s3 = filler; s4 = filler; s5 = filler; s6 = filler; s7 = filler; // row 1
            s8 = filler; s9 = filler; s10 = filler; s11 = filler; s12 = filler; s13 = filler; s14 = filler; // row 2
            s15 = filler; s16 = filler; s17 = filler; s18 = filler; s19 = filler; s20 = filler; s21 = filler; // row 3
            s22 = filler; s23 = filler; s24 = filler; s25 = filler; s26 = filler; s27 = filler; s28 = filler; // row 4  
            s29 = filler; s30 = filler; s31 = filler; s32 = filler; s33 = filler; s34 = filler; s35 = filler; // row 5
            s36 = filler; s37 = filler; s38 = filler; s39 = filler; s40 = filler; s41 = filler; s42 = filler; // row 6
            s43 = filler; s44 = filler; s45 = filler; s46 = filler; s47 = filler; s48 = filler; s49 = filler; // row 7
        }
        /// <summary>
        /// // change so takes in int and counters 
        /// </summary>
        // DISPLAY GameBoard_TPL AS FOLLOWS
        public void DisplayBoard()
        {
            //  Console.Clear();
            for (int x = 1; x <= 7; x++)
                Console.Write("  " + x + " ");
            Console.WriteLine();
            for (int y = 1; y <= 7; y++)
            {
                Console.Write(y + " ");
                for (int x = 1; x <= 7; x++)
                {
		  Console.Write(this[x, y].CompareTo(counters.e)==0 ? " " : this[x, y].ToString());
                    Console.Write(" | ");
                }
                Console.WriteLine();
                Console.WriteLine("  -   -   -   -   -   -   - ");
            }
            Console.WriteLine();
        }
        public void DisplayScoreBoard()
        {
            //  Console.Clear();
            for (int x = 1; x <= 7; x++)
                Console.Write("  " + x + " ");
            Console.WriteLine();
            for (int y = 1; y <= 7; y++)
            {
                Console.Write(y + " ");
                for (int x = 1; x <= 7; x++)
                {
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        Console.Write(filler);
                    else
                        Console.Write(this[x, y]);
                    Console.Write(" | ");

                }
                Console.WriteLine();
                Console.WriteLine("  -   -   -   -   -   -   - ");
            }
            Console.WriteLine();
        }

        public void DisplayFinBoardToFile()
        {
	  // string path = @"C:/Users/Lewis/Desktop/files_150819/ttt_csharp_270719/Minimax_TPL/boards/finboards.txt";
            string path = @"data/finboards.txt";
            // This text is added only once to the file.
            // Create a file to write to.
	    // File.WriteAllText(@"C:/Users/Lewis/Desktop/files_150819/ttt_csharp_270719/Minimax_TPL/boards/finboards.txt", string.Empty);
             File.WriteAllText(@"data/finboards.txt", string.Empty);
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.Write(Environment.NewLine);
                //     System.IO.File.WriteAllText(@"C:/Users/Lewis/Desktop/files_150819/ttt_csharp_270719/Minimax_TPL/finboards.txt", string.Empty);
                for (int x = 1; x <= 7; x++)
                    sw.Write("  " + x + " ");
                sw.WriteLine();
                for (int y = 1; y <= 7; y++)
                {
                    sw.Write(y + " ");
                    for (int x = 1; x <= 7; x++)
                    {
                        if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                            sw.Write(filler);
                        else
                            sw.Write(this[x, y]);
                        sw.Write(" | ");
                    }
                    sw.WriteLine();
                    sw.WriteLine("  -   -   -   -   -   -   - ");
                }
                sw.Write("^^ FIN BOARD FOR BOARD " + Game_TPL.cntr);
                sw.WriteLine();
            }
        }
        public void DisplayScoreBoardToFile()
        {
	  // string path = @"C:/Users/Lewis/Desktop/files_150819/ttt_csharp_270719/Minimax_TPL/boards/scoreboards.txt";
            string path = @"data/scoreboards.txt";
            // This text is added only once to the file.
            // Create a file to write to.
            // File.WriteAllText(@"C:/Users/Lewis/Desktop/files_150819/ttt_csharp_270719/Minimax_TPL/boards/scoreboards.txt", string.Empty);
             File.WriteAllText(@"data/scoreboards.txt", string.Empty);
            using (StreamWriter sw = new StreamWriter(path, true))
            {

                sw.Write(Environment.NewLine);
                //      System.IO.File.WriteAllText(@"C:/Users/Lewis/Desktop/files_150819/ttt_csharp_270719/Minimax_TPL/scoreboards.txt", string.Empty);
                for (int x = 1; x <= 7; x++)
                    sw.Write("  " + x + " ");
                sw.WriteLine();
                for (int y = 1; y <= 7; y++)
                {
                    sw.Write(y + " ");
                    for (int x = 1; x <= 7; x++)
                    {
                        if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                            sw.Write(filler);
                        else
                            sw.Write(this[x, y]);
                        sw.Write(" | ");
                    }
                    sw.WriteLine();
                    sw.WriteLine("  -   -   -   -   -   -   - ");
                }
                sw.Write("^^ SCORE BOARD FOR BOARD " + Game_TPL.cntr);
                sw.WriteLine();
            }
        }

        // IF GameBoard_TPL IS FULL
        public bool IsFull()
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IF GameBoard_TPL IS FULL
        public bool IsEmpty()
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (!EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS MIDDLE CELL EMPTY
        public bool IsMiddleEmpty() {
            for (int x = 4; x <= 4; x++)
                for (int y = 4; y <= 4; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return true;
            return false;
        }

        // IS TOP LEFT CELL EMPTY
        public bool IsTopLeftEmpty()
        {
            for (int x = 1; x <= 1; x++)
                for (int y = 1; y <= 1; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return true;
            return false;
        }

        // IS TOP RIGHT CELL EMPTY
        public bool IsTopRightEmpty()
        {
            for (int x = 1; x <= 1; x++)
                for (int y = 7; y <= 7; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return true;
            return false;
        }

        // IS BOTTOM LEFT EMPTY
        public bool IsBottomLeftEmpty()
        {
            for (int x = 7; x <= 7; x++)
                for (int y = 1; y <= 1; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return true;
            return false;
        }

        // IS BOTTOM RIGHT EMPTY
        public bool IsBottomRightEmpty()
        {
            for (int x = 7; x <= 7; x++)
                for (int y = 7; y <= 7; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool IsLeftEdgesEmpty()
        {
            for (int x = 1; x <= 1; x++)
                for (int y = 2; y <= 6; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool IsRightEdgesEmpty()
        {
            for (int x = 7; x <= 7; x++)
                for (int y = 2; y <= 6; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool IsTopEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 1; y <= 1; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS CENTRE BLOCK EMPTY
        public bool IsCentreBlockEmpty()
        {
            for (int x = 3; x <= 6; x++)
                for (int y = 3; y <= 6; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreBottomEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 7; y <= 7; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreTopEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 1; y <= 1; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreLeftEdgesEmpty()
        {
            for (int x = 1; x <= 1; x++)
                for (int y = 2; y <= 6; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreRightEdgesEmpty()
        {
            for (int x = 7; x <= 7; x++)
                for (int y = 2; y <= 6; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }
        
        // IS EDGES EMPTY
        public bool AreInnerBottomEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 6; y <= 6; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreInnerTopEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 6; y <= 6; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreInnerLeftEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 6; y <= 6; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreInnerRightEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 6; y <= 6; y++)
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                        return false;
            return true;
        }

        // PLACE ON EDGES
        public int PlaceAtBottomEdges(GameBoard_TPL<T> copy, T counter)
        {
            int x = 0;
            int y = 0;
            for (x = 2; x <= 6; x++)
            for (y = 7; y <= 7; y++)
                this[x, y] = counter;
                    if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
                    {
                        
                        return -45;
                    }
                        return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtTopEdges(GameBoard_TPL<T> copy, T counter)
        {
            int x = 0;
            int y = 0;
            for (x = 2; x <= 6; x++)
                for (y = 1; y <= 1; y++)
                    this[x, y] = counter;
            if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
            {

                return -45;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtLeftEdges(GameBoard_TPL<T> copy, T counter)
        {
            int x = 0;
            int y = 0;
            for (x = 1; x <= 1; x++)
                for (y = 2; y <= 6; y++)
                    this[x, y] = counter;
            if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
            {

                return -45;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtRightEdges(GameBoard_TPL<T> copy, T counter)
        {
            int x = 0;
            int y = 0;
            for (x = 7; x <= 7; x++)
                for (y = 2; y <= 6; y++)
                    this[x, y] = counter;
            if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
            {

                return -45;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtInnerBottomEdges(GameBoard_TPL<T> copy, T counter)
        {
            int x = 0;
            int y = 0;
            for (x = 2; x <= 6; x++)
                for (y = 6; y <= 6; y++)
                    this[x, y] = counter;
            if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
            {

                return -15;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtInnerTopEdges(GameBoard_TPL<T> copy, T counter)
        {
            int x = 0;
            int y = 0;
            for (x = 2; x <= 6; x++)
                for (y = 6; y <= 6; y++)
                    this[x, y] = counter;
            if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
            {
                return -15;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtInnerLeftEdges(GameBoard_TPL<T> copy, T counter)
        {
            int x = 0;
            int y = 0;
            for (x = 1; x <= 1; x++)
                for (y = 2; y <= 6; y++)
                    this[x, y] = counter;
            if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
            {
                return -15;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtInnerRightEdges(GameBoard_TPL<T> copy, T counter)
        {
            int x = 0;
            int y = 0;
            for (x = 7; x <= 7; x++)
                for (y = 2; y <= 6; y++)
                    this[x, y] = counter;
            if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
            {

                return -15;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtCentreBlock(GameBoard_TPL<T> copy, T counter)
        {
            int x = 0;
            int y = 0;
            for (x = 3; x <= 6; x++)
                for (y = 3; y <= 6; y++)
                    this[x, y] = counter;
            if (EqualityComparer<T>.Default.Equals(this[x, y], filler))
            {

                return 80;
            }
            return 25;
        }
        // IS LEFT CELL BESIDE TWO IN ROW EMPTY
        public bool IsTwoLeftNeighbourEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x - xx, y], filler))
                            return true;
                        }
                }
            return false;
        }
        // PRINT LEFT EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoLeftNeighbour(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x - xx, y], filler))
                                    return new Tuple<int, int>(x - xx, y);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS RIGHT CELL BESIDE TWO IN ROW EMPTY
        public bool IsTwoRightNeighbourEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x + xx + 1, y], filler))
                            return true;
                        }
                }
            return false;
        }
        // PRINT RIGHT EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoRightNeighbour(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x - xx, y], filler))
                                    return new Tuple<int, int>(x + xx + 1, y);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS TOP CELL BESIDE TWO IN ROW EMPTY
        public bool IsTwoTopNeighbourEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x, y + yy], filler))
                            return true;
                        }
                }
            return false;
        }
        // PRINT TOP EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoTopNeighbour(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x, y + yy], filler))
                                    return new Tuple<int, int>(x, y + yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS BOTTOM CELL BESIDE TWO IN ROW EMPTY
        public bool IsTwoBottomNeighbourEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x, y - yy], filler))
                            return true;
                        }
                }
            return false;
        }
        // PRINT BOTTOM EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoBottomNeighbour(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x, y - yy], filler))
                            return new Tuple<int, int>(x, y - yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS LEFT CELL BESIDE TWO IN ROW EMPTY
        public bool IsOneLeftNeighbourEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x - xx, y], filler))
                                    return true;
                        }
                }
            return false;
        }
        // PRINT LEFT EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintOneLeftNeighbour(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x - xx, y], filler))
                                    return new Tuple<int, int>(x - xx, y);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS RIGHT CELL BESIDE TWO IN ROW EMPTY
        public bool IsOneRightNeighbourEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x + xx + 1, y], filler))
                                    return true;
                        }
                }
            return false;
        }
        // PRINT RIGHT EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintOneRightNeighbour(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x - xx, y], filler))
                                    return new Tuple<int, int>(x + xx + 1, y);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS TOP CELL BESIDE TWO IN ROW EMPTY
        public bool IsOneTopNeighbourEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x, y + yy], filler))
                                    return true;
                        }
                }
            return false;
        }
        // PRINT TOP EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintOneTopNeighbour(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x, y + yy], filler))
                                    return new Tuple<int, int>(x, y + yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS BOTTOM CELL BESIDE TWO IN ROW EMPTY
        public bool IsOneBottomNeighbourEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us))
                                    // two in a row in centre should give higher score
                                    if (EqualityComparer<T>.Default.Equals(board[x, y - yy], filler))
                                    return true;
                        }
                }
            return false;
        }
        // PRINT BOTTOM EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintOneBottomNeighbour(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                           if (EqualityComparer<T>.Default.Equals(board[x, y], us))
                                    // two in a row in centre should give higher score
                                    if (EqualityComparer<T>.Default.Equals(board[x, y - yy], filler))
                                    return new Tuple<int, int>(x, y - yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }

        // IS THEIR GAP BETWEEN TWO IN ROW EMPTY
        public bool IsTwoWithHorziGapEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx - 1, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x + xx, y + yy], filler))
                                    return true;
                        }
                }
            return false;
        }
        // PRINT GAP BETWEEN TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoWithHorziGap(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx - 1, y + yy]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x + xx, y + yy], filler))
                                    return new Tuple<int, int>(x + xx, y + yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS GAP CELL BETWEEN VERTICAL TWO IN ROW EMPTY
        public bool IsTwoWithVerticalGapEmpty(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy - 1]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x + xx, y + yy], filler))
                                    return true;
                        }
                }
            return false;
        }
        // PRINT VERTICAL GAP BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoWithVerticalGap(GameBoard_TPL<T> board, T us)
        {
            // Debug.Assert(us == T.NOUGHTS || us == T.CROSSES);
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                {
                    // check whether position piece at [x,y] has the same piece as neighbour
                    // Debug.Assert(board[x, y] == T.NOUGHTS || board[x, y] == T.CROSSES);
                    for (int xx = -1; xx <= 7; xx++)
                        for (int yy = -1; yy <= 7; yy++)
                        {
                            if (yy == 0 && xx == 0)
                                continue;
                            if (EqualityComparer<T>.Default.Equals(board[x, y], us) && EqualityComparer<T>.Default.Equals(board[x, y], board[x + xx, y + yy - 1]))
                                // two in a row in centre should give higher score
                                if (EqualityComparer<T>.Default.Equals(board[x + xx, y + yy], filler))
                                    return new Tuple<int, int>(x + xx, y + yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }

        // CLONE A board OF THE CURRENT GAME BOARD
        public GameBoard_TPL<T> Clone()
        {
            GameBoard_TPL<T> g = new GameBoard_TPL<T>();
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    g[x, y] = this[x,y];
            return g;
            
        }

        // DISPLAY GAMEBOARD AS FOLLOWS - IN DEVELOPMENT
        public void DisplayIntBoardToFile()
        {
            string path = @"data/intboards.txt";
            int number = Game_TPL.cntr;
            // ... Cases may not be duplicated.
            File.WriteAllText(@"data/intboards.txt", string.Empty);

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                switch (number)
                {
                    case 0:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e | X | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | e | O | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | e | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e  | e | e |  O     | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 1:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e | X | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | e | O | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | e | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e  | e | e |  O     | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 2:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | X | X | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | X | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 3:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | X | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2 | X | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 4:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | e | X | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e | e | e | X | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 5:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | X | X | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2 | O | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | O | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | e | X | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 6:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e | e |   X | X    | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e | e | e | X | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 7:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | e | e | O | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 8:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | e | X | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e | e | O | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 9:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | X | e | X |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e | e | O |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e | e | O |");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e | e | O |");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 10:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | e | X | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e | e | e | X | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e | e | e | O | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e | e | O | O | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write("5  | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e | e | e | X | X  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e | e | e | X | e | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 11:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | e | e | O | e | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e | e | O | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | X | e |  e  | e | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 12:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | X | e  | e | e | X | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3 | X | e | O | e | O | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | e | e | O | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5 | O | e | e | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e | e | X | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e | e | X | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 13:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | X | X | O | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e | e | e | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e | e |   X | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e | e | X | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 14:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | e | e | O | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e | e | e | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6 | X | e | X | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | X | e | X | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 15:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e | X | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | O | X | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 16:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | O | X | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 17:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | X | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2 | X | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 18:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 19:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | X | O | O | e | O | e | X |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2 | O | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | X | O | X | O | X | O | X |");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 20:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | X | O | O | e | O | e | X |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2 | O | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | X | O | X | O | X | O | X |");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 21:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | O | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 22:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | O |  O  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | O | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 23:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 24:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e | X | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | e | O | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | e | e | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e | e | e | O | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 25:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | O | O | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2 | X | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | O | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | e | X | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 26:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | O | e | O |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 27:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | e | e | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e | e | O | O | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e | e | e | O | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 28:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | e | e | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | e | e | X | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 29:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | e | X | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | e | O | e | e | X | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e | e | X | e | e | O | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 30:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | e | e | X | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e | e | O | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e | e | O | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 31:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | e | e | X | e | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e | e | X | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | O | e |  e  | e | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 32:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | O | e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3 | O | e | X | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | e | e | X | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5 | X | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e | e | O | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e | e | O | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 33:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | O | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e | e | X | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | O | O | X | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e | e | O | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e | e | O | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 34:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e | e | O | e | e | ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e | e | O | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e | e | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e | e | e | X | X | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5 | O | e | e | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e | e | O | O | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | O | e | O | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 35:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e | O | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | X | O | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 36:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | X | O | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 37:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e | e | e | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e | e | e | X | e | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 38:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2   | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5  | e |  e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 39:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | O | X | X  | e | X | e | O |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2 | X | e  | e |  e  | e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5 | O | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6 | X | e |  e  | e |  e  | e | e |  ");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | O | X |O  | X | O | X | O |");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    case 40:
                        sw.Write("see below");
                        sw.Write(Environment.NewLine); sw.Write("  1   2   3   4   5   6   7  ");
                        sw.Write(Environment.NewLine);
                        sw.Write("1 | O | X | X | O | X | O | O  |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("2 | X | e  | e |  e  | e | e | O |");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("3 | O | e |  e  | e | e | e | X |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("4 | X | e |  e  | e | e | e | O |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("5 | O | e |  e  | e | e | e | X |");
                        sw.Write(Environment.NewLine);
                        sw.Write("- - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("6 | X | e |  e  | e | e | e | X |");
                        sw.Write(Environment.NewLine);
                        sw.Write(" - - - - - - -");
                        sw.Write(Environment.NewLine);
                        sw.Write("7 | O | X |O  | X | O | X | O |");
                        sw.Write(Environment.NewLine); sw.Write("^^ INT BOARD FOR BOARD " + Game_TPL.cntr);
                        sw.Write(Environment.NewLine);
                        return;
                    default:
                        Environment.Exit(99);
                        break;
                }
            }
        }
        // CELL ID'S AND COORDS ASSIGN
        public T this[int x, int y]
        {
            get
            {
                if (x == 1 && y == 1)
                    return s1;
                if (x == 2 && y == 1)
                    return s2;
                if (x == 3 && y == 1)
                    return s3;
                if (x == 4 && y == 1)
                    return s4;
                if (x == 5 && y == 1)
                    return s5;
                if (x == 6 && y == 1)
                    return s6;
                if (x == 7 && y == 1)
                    return s7;
                if (x == 1 && y == 2)
                    return s8;
                if (x == 2 && y == 2)
                    return s9;
                if (x == 3 && y == 2)
                    return s10;
                if (x == 4 && y == 2)
                    return s11;
                if (x == 5 && y == 2)
                    return s12;
                if (x == 6 && y == 2)
                    return s13;
                if (x == 7 && y == 2)
                    return s14;
                if (x == 1 && y == 3)
                    return s15;
                if (x == 2 && y == 3)
                    return s16;
                if (x == 3 && y == 3)
                    return s17;
                if (x == 4 && y == 3)
                    return s18;
                if (x == 5 && y == 3)
                    return s19;
                if (x == 6 && y == 3)
                    return s20;
                if (x == 7 && y == 3)
                    return s21;
                if (x == 1 && y == 4)
                    return s22;
                if (x == 2 && y == 4)
                    return s23;
                if (x == 3 && y == 4)
                    return s24;
                if (x == 4 && y == 4)
                    return s25;
                if (x == 5 && y == 4)
                    return s26;
                if (x == 6 && y == 4)
                    return s27;
                if (x == 7 && y == 4)
                    return s28;
                if (x == 1 && y == 5)
                    return s29;
                if (x == 2 && y == 5)
                    return s30;
                if (x == 3 && y == 5)
                    return s31;
                if (x == 4 && y == 5)
                    return s32;
                if (x == 5 && y == 5)
                    return s33;
                if (x == 6 && y == 5)
                    return s34;
                if (x == 7 && y == 5)
                    return s35;
                if (x == 1 && y == 6)
                    return s36;
                if (x == 2 && y == 6)
                    return s37;
                if (x == 3 && y == 6)
                    return s38;
                if (x == 4 && y == 6)
                    return s39;
                if (x == 5 && y == 6)
                    return s40;
                if (x == 6 && y == 6)
                    return s41;
                if (x == 7 && y == 6)
                    return s42;
                if (x == 1 && y == 7)
                    return s43;
                if (x == 2 && y == 7)
                    return s44;
                if (x == 3 && y == 7)
                    return s45;
                if (x == 4 && y == 7)
                    return s46;
                if (x == 5 && y == 7)
                    return s47;
                if (x == 6 && y == 7)
                    return s48;
                if (x == 7 && y == 7)
                    return s49;
                return filler;
            }
            set
            {
                if (x == 1 && y == 1)
                    s1 = value;
                if (x == 2 && y == 1)
                    s2 = value;
                if (x == 3 && y == 1)
                    s3 = value;
                if (x == 4 && y == 1)
                    s4 = value;
                if (x == 5 && y == 1)
                    s5 = value;
                if (x == 6 && y == 1)
                    s6 = value;
                if (x == 7 && y == 1)
                    s7 = value;
                if (x == 1 && y == 2)
                    s8 = value;
                if (x == 2 && y == 2)
                    s9 = value;
                if (x == 3 && y == 2)
                    s10 = value;
                if (x == 4 && y == 2)
                    s11 = value;
                if (x == 5 && y == 2)
                    s12 = value;
                if (x == 6 && y == 2)
                    s13 = value;
                if (x == 7 && y == 2)
                    s14 = value;
                if (x == 1 && y == 3)
                    s15 = value;
                if (x == 2 && y == 3)
                    s16 = value;
                if (x == 3 && y == 3)
                    s17 = value;
                if (x == 4 && y == 3)
                    s18 = value;
                if (x == 5 && y == 3)
                    s19 = value;
                if (x == 6 && y == 3)
                    s20 = value;
                if (x == 7 && y == 3)
                    s21 = value;
                if (x == 1 && y == 4)
                    s22 = value;
                if (x == 2 && y == 4)
                    s23 = value;
                if (x == 3 && y == 4)
                    s24 = value;
                if (x == 4 && y == 4)
                    s25 = value;
                if (x == 5 && y == 4)
                    s26 = value;
                if (x == 6 && y == 4)
                    s27 = value;
                if (x == 7 && y == 4)
                    s28 = value;
                if (x == 1 && y == 5)
                    s29 = value;
                if (x == 2 && y == 5)
                    s30 = value;
                if (x == 3 && y == 5)
                    s31 = value;
                if (x == 4 && y == 5)
                    s32 = value;
                if (x == 5 && y == 5)
                    s33 = value;
                if (x == 6 && y == 5)
                    s34 = value;
                if (x == 7 && y == 5)
                    s35 = value;
                if (x == 1 && y == 6)
                    s36 = value;
                if (x == 2 && y == 6)
                    s37 = value;
                if (x == 3 && y == 6)
                    s38 = value;
                if (x == 4 && y == 6)
                    s39 = value;
                if (x == 5 && y == 6)
                    s40 = value;
                if (x == 6 && y == 6)
                    s41 = value;
                if (x == 7 && y == 6)
                    s42 = value;
                if (x == 1 && y == 7)
                    s43 = value;
                if (x == 2 && y == 7)
                    s44 = value;
                if (x == 3 && y == 7)
                    s45 = value;
                if (x == 4 && y == 7)
                    s46 = value;
                if (x == 5 && y == 7)
                    s47 = value;
                if (x == 6 && y == 7)
                    s48 = value;
                if (x == 7 && y == 7)
                    s49  = value;
            }
        }
    }
}
