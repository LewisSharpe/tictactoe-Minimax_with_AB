using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
    // GAME BOARD STRUCTURE CLASS
    struct GameBoard
    {
        // cell ids
        private counters s1, s2, s3, s4, s5, s6, s7,
                         s8, s9, s10, s11, s12, s13, s14,
                         s15, s16, s17, s18, s19, s20, s21,
                         s22, s23, s24, s25, s26, s27, s28,
                         s29, s30, s31, s32, s33, s34, s35,
                         s36, s37, s38, s39, s40, s41, s42,
                         s43, s44, s45, s46, s47, s48, s49;
        private counters filler; // empty filler space for cell

        // 1 BLANK BOARD
        public GameBoard(counters _filler)
        {
            filler = _filler; // blank filler
            s1 = counters.EMPTY; s2 = counters.EMPTY; s3 = counters.EMPTY; s4 = counters.EMPTY; s5 = counters.EMPTY; s6 = counters.EMPTY; s7 = counters.EMPTY; // row 1
            s8 = counters.EMPTY; s9 = counters.EMPTY; s10 = counters.EMPTY; s11 = counters.EMPTY; s12 = counters.EMPTY; s13 = counters.EMPTY; s14 = counters.EMPTY; // row 2
            s15 = counters.EMPTY; s16 = counters.EMPTY; s17 = counters.EMPTY; s18 = counters.EMPTY; s19 = counters.EMPTY; s20 = counters.EMPTY; s21 = counters.EMPTY; // row 3
            s22 = counters.EMPTY; s23 = counters.EMPTY; s24 = counters.CROSSES; s25 = counters.CROSSES; s26 = counters.EMPTY; s27 = counters.EMPTY; s28 = counters.EMPTY; // row 4  
            s29 = counters.EMPTY; s30 = counters.EMPTY; s31 = counters.EMPTY; s32 = counters.EMPTY; s33 = counters.EMPTY; s34 = counters.EMPTY; s35 = counters.EMPTY; // row 5
            s36 = counters.EMPTY; s37 = counters.EMPTY; s38 = counters.EMPTY; s39 = counters.EMPTY; s40 = counters.EMPTY; s41 = counters.EMPTY; s42 = counters.EMPTY; // row 6
            s43 = counters.EMPTY; s44 = counters.EMPTY; s45 = counters.EMPTY; s46 = counters.EMPTY; s47 = counters.EMPTY; s48 = counters.EMPTY; s49 = counters.EMPTY; // row 7
        }

        // DISPLAY GAMEBOARD AS FOLLOWS
        public void DisplayBoard()
        {
            Console.Clear();
            for (int x = 1; x <= 7; x++)
                Console.Write("  " + x + " ");
            Console.WriteLine();
            for (int y = 1; y <= 7; y++)
            {
                Console.Write(y + " ");
                for (int x = 1; x <= 7; x++)
                {
                    if (this[x, y] == counters.EMPTY)
                        Console.Write(counters.EMPTY);
                    else
                        Console.Write(this[x, y]);
                    Console.Write(" | ");
                }
                Console.WriteLine();
                Console.WriteLine("  -   -   -   -   -   -   - ");
            }
            Console.WriteLine();
        }

        // DISPLAY GAMEBOARD AS FOLLOWS
        public void DisplayBoardToFile()
        {
            string path = @"C:\Users\Owner\Desktop\Minimax\Minimax\copy.txt";
            // This text is added only once to the file.
            // Create a file to write to.
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                    for (int x = 1; x <= 7; x++)
                    sw.Write("  " + x + " ");
                    sw.WriteLine();
                    for (int y = 1; y <= 7; y++)
                    {
                        sw.Write(y + " ");
                        for (int x = 1; x <= 7; x++)
                        {
                            if (this[x, y] == counters.EMPTY)
                                sw.Write(counters.EMPTY);
                            else
                                sw.Write(this[x, y]);
                            sw.Write(" | ");
                        }
                        sw.WriteLine();
                        sw.WriteLine("  -   -   -   -   -   -   - ");
                    }
                    sw.WriteLine();
                }
            }

        // IF GAMEBOARD IS FULL
        public bool IsFull()
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (this[x, y] == filler)
                        return false;
            return true;
        }

        // IF GAMEBOARD IS FULL
        public bool IsEmpty()
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (this[x, y] != counters.EMPTY)
                        return false;
            return true;
        }

        // IS MIDDLE CELL EMPTY
        public bool IsMiddleEmpty() {
            for (int x = 4; x <= 4; x++)
                for (int y = 4; y <= 4; y++)
                    if (this[x, y] == counters.EMPTY)
                        return true;
            return false;
        }

        // IS TOP LEFT CELL EMPTY
        public bool IsTopLeftEmpty()
        {
            for (int x = 1; x <= 1; x++)
                for (int y = 1; y <= 1; y++)
                    if (this[x, y] == counters.EMPTY)
                        return true;
            return false;
        }

        // IS TOP RIGHT CELL EMPTY
        public bool IsTopRightEmpty()
        {
            for (int x = 1; x <= 1; x++)
                for (int y = 7; y <= 7; y++)
                    if (this[x, y] == counters.EMPTY)
                        return true;
            return false;
        }

        // IS BOTTOM LEFT EMPTY
        public bool IsBottomLeftEmpty()
        {
            for (int x = 7; x <= 7; x++)
                for (int y = 1; y <= 1; y++)
                    if (this[x, y] == counters.EMPTY)
                        return true;
            return false;
        }

        // IS BOTTOM RIGHT EMPTY
        public bool IsBottomRightEmpty()
        {
            for (int x = 7; x <= 7; x++)
                for (int y = 7; y <= 7; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool IsLeftEdgesEmpty()
        {
            for (int x = 1; x <= 1; x++)
                for (int y = 2; y <= 6; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool IsRightEdgesEmpty()
        {
            for (int x = 7; x <= 7; x++)
                for (int y = 2; y <= 6; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool IsTopEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 1; y <= 1; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS CENTRE BLOCK EMPTY
        public bool IsCentreBlockEmpty()
        {
            for (int x = 3; x <= 6; x++)
                for (int y = 3; y <= 6; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreBottomEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 7; y <= 7; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreTopEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 1; y <= 1; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreLeftEdgesEmpty()
        {
            for (int x = 1; x <= 1; x++)
                for (int y = 2; y <= 6; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreRightEdgesEmpty()
        {
            for (int x = 7; x <= 7; x++)
                for (int y = 2; y <= 6; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }


        // IS EDGES EMPTY
        public bool AreInnerBottomEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 6; y <= 6; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreInnerTopEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 6; y <= 6; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreInnerLeftEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 6; y <= 6; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // IS EDGES EMPTY
        public bool AreInnerRightEdgesEmpty()
        {
            for (int x = 2; x <= 6; x++)
                for (int y = 6; y <= 6; y++)
                    if (this[x, y] == counters.EMPTY)
                        return false;
            return true;
        }

        // PLACE ON EDGES
        public int PlaceAtBottomEdges(GameBoard copy, counters counter)
        {
            int x = 0;
            int y = 0;
            for (x = 2; x <= 6; x++)
            for (y = 7; y <= 7; y++)
                this[x, y] = counter;
                    if (this[x, y] == counters.EMPTY)
                    {
                        
                        return -45;
                    }
                        return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtTopEdges(GameBoard copy, counters counter)
        {
            int x = 0;
            int y = 0;
            for (x = 2; x <= 6; x++)
                for (y = 1; y <= 1; y++)
                    this[x, y] = counter;
            if (this[x, y] == counters.EMPTY)
            {

                return -45;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtLeftEdges(GameBoard copy, counters counter)
        {
            int x = 0;
            int y = 0;
            for (x = 1; x <= 1; x++)
                for (y = 2; y <= 6; y++)
                    this[x, y] = counter;
            if (this[x, y] == counters.EMPTY)
            {

                return -45;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtRightEdges(GameBoard copy, counters counter)
        {
            int x = 0;
            int y = 0;
            for (x = 7; x <= 7; x++)
                for (y = 2; y <= 6; y++)
                    this[x, y] = counter;
            if (this[x, y] == counters.EMPTY)
            {

                return -45;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtInnerBottomEdges(GameBoard copy, counters counter)
        {
            int x = 0;
            int y = 0;
            for (x = 2; x <= 6; x++)
                for (y = 6; y <= 6; y++)
                    this[x, y] = counter;
            if (this[x, y] == counters.EMPTY)
            {

                return -15;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtInnerTopEdges(GameBoard copy, counters counter)
        {
            int x = 0;
            int y = 0;
            for (x = 2; x <= 6; x++)
                for (y = 6; y <= 6; y++)
                    this[x, y] = counter;
            if (this[x, y] == counters.EMPTY)
            {
                return -15;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtInnerLeftEdges(GameBoard copy, counters counter)
        {
            int x = 0;
            int y = 0;
            for (x = 1; x <= 1; x++)
                for (y = 2; y <= 6; y++)
                    this[x, y] = counter;
            if (this[x, y] == counters.EMPTY)
            {
                return -15;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtInnerRightEdges(GameBoard copy, counters counter)
        {
            int x = 0;
            int y = 0;
            for (x = 7; x <= 7; x++)
                for (y = 2; y <= 6; y++)
                    this[x, y] = counter;
            if (this[x, y] == counters.EMPTY)
            {

                return -15;
            }
            return 25;
        }

        // PLACE ON EDGES
        public int PlaceAtCentreBlock(GameBoard copy, counters counter)
        {
            int x = 0;
            int y = 0;
            for (x = 3; x <= 6; x++)
                for (y = 3; y <= 6; y++)
                    this[x, y] = counter;
            if (this[x, y] == counters.EMPTY)
            {

                return 80;
            }
            return 25;
        }
        // IS LEFT CELL BESIDE TWO IN ROW EMPTY
        public bool IsTwoLeftNeighbourEmpty(GameBoard board, counters us)
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
                                // two in a row in centre should give higher score
                                if (board[x - xx, y] == counters.EMPTY)
                            return true;
                        }
                }
            return false;
        }
        // PRINT LEFT EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoLeftNeighbour(GameBoard board, counters us)
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
                                // two in a row in centre should give higher score
                                if (board[x - xx, y] == counters.EMPTY)
                                    return new Tuple<int, int>(x - xx, y);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS RIGHT CELL BESIDE TWO IN ROW EMPTY
        public bool IsTwoRightNeighbourEmpty(GameBoard board, counters us)
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
                                // two in a row in centre should give higher score
                                if (board[x + xx + 1, y] == counters.EMPTY)
                            return true;
                        }
                }
            return false;
        }
        // PRINT RIGHT EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoRightNeighbour(GameBoard board, counters us)
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
                                // two in a row in centre should give higher score
                                if (board[x - xx, y] == counters.EMPTY)
                                    return new Tuple<int, int>(x + xx + 1, y);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS TOP CELL BESIDE TWO IN ROW EMPTY
        public bool IsTwoTopNeighbourEmpty(GameBoard board, counters us)
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
                                // two in a row in centre should give higher score
                                if (board[x, y + yy] == counters.EMPTY)
                            return true;
                        }
                }
            return false;
        }
        // PRINT TOP EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoTopNeighbour(GameBoard board, counters us)
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
                                // two in a row in centre should give higher score
                                if (board[x, y + yy] == counters.EMPTY)
                                    return new Tuple<int, int>(x, y + yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS BOTTOM CELL BESIDE TWO IN ROW EMPTY
        public bool IsTwoBottomNeighbourEmpty(GameBoard board, counters us)
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
                                // two in a row in centre should give higher score
                                if (board[x, y - yy] == counters.EMPTY)
                            return true;
                        }
                }
            return false;
        }
        // PRINT BOTTOM EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoBottomNeighbour(GameBoard board, counters us)
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
                                // two in a row in centre should give higher score
                                if (board[x, y - yy] == counters.EMPTY)
                                    return new Tuple<int, int>(x, y - yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS LEFT CELL BESIDE TWO IN ROW EMPTY
        public bool IsOneLeftNeighbourEmpty(GameBoard board, counters us)
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
                            if (board[x, y] == us)
                                // two in a row in centre should give higher score
                                if (board[x - xx, y] == counters.EMPTY)
                                    return true;
                        }
                }
            return false;
        }
        // PRINT LEFT EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintOneLeftNeighbour(GameBoard board, counters us)
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
                            if (board[x, y] == us)
                                // two in a row in centre should give higher score
                                if (board[x - xx, y] == counters.EMPTY)
                                    return new Tuple<int, int>(x - xx, y);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS RIGHT CELL BESIDE TWO IN ROW EMPTY
        public bool IsOneRightNeighbourEmpty(GameBoard board, counters us)
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
                            if (board[x, y] == us)
                                // two in a row in centre should give higher score
                                if (board[x + xx + 1, y] == counters.EMPTY)
                                    return true;
                        }
                }
            return false;
        }
        // PRINT RIGHT EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintOneRightNeighbour(GameBoard board, counters us)
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
                            if (board[x, y] == us)
                                // two in a row in centre should give higher score
                                if (board[x - xx, y] == counters.EMPTY)
                                    return new Tuple<int, int>(x + xx + 1, y);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS TOP CELL BESIDE TWO IN ROW EMPTY
        public bool IsOneTopNeighbourEmpty(GameBoard board, counters us)
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
                            if (board[x, y] == us)
                                // two in a row in centre should give higher score
                                if (board[x, y + yy] == counters.EMPTY)
                                    return true;
                        }
                }
            return false;
        }
        // PRINT TOP EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintOneTopNeighbour(GameBoard board, counters us)
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
                            if (board[x, y] == us)
                                // two in a row in centre should give higher score
                                if (board[x, y + yy] == counters.EMPTY)
                                    return new Tuple<int, int>(x, y + yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS BOTTOM CELL BESIDE TWO IN ROW EMPTY
        public bool IsOneBottomNeighbourEmpty(GameBoard board, counters us)
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
                            if (board[x, y] == us)
                                // two in a row in centre should give higher score
                                if (board[x, y - yy] == counters.EMPTY)
                                    return true;
                        }
                }
            return false;
        }
        // PRINT BOTTOM EMPTY CELL BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintOneBottomNeighbour(GameBoard board, counters us)
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
                            if (board[x, y] == us)
                                // two in a row in centre should give higher score
                                if (board[x, y - yy] == counters.EMPTY)
                                    return new Tuple<int, int>(x, y - yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }

        // IS THEIR GAP BETWEEN TWO IN ROW EMPTY
        public bool IsTwoWithHorziGapEmpty(GameBoard board, counters us)
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
                            if (board[x, y] == us && board[x, y] == board[x + xx - 1, y + yy])
                                // two in a row in centre should give higher score
                                if (board[x + xx, y + yy] == counters.EMPTY)
                                    return true;
                        }
                }
            return false;
        }
        // PRINT GAP BETWEEN TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoWithHorziGap(GameBoard board, counters us)
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
                            if (board[x, y] == us && board[x, y] == board[x + xx - 1, y + yy])
                                // two in a row in centre should give higher score
                                if (board[x + xx, y + yy] == counters.EMPTY)
                                    return new Tuple<int, int>(x + xx, y + yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }
        // IS GAP CELL BETWEEN VERTICAL TWO IN ROW EMPTY
        public bool IsTwoWithVerticalGapEmpty(GameBoard board, counters us)
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
                            if (board[x, y] == us && board[x, y] == board[x + xx, y + yy - 1])
                                // two in a row in centre should give higher score
                                if (board[x + xx, y + yy] == counters.EMPTY)
                                    return true;
                        }
                }
            return false;
        }
        // PRINT VERTICAL GAP BESIDE TWO IN ROW EMPTY
        public Tuple<int, int> PrintTwoWithVerticalGap(GameBoard board, counters us)
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
                            if (board[x, y] == us && board[x, y] == board[x + xx, y + yy - 1])
                                // two in a row in centre should give higher score
                                if (board[x + xx, y + yy] == counters.EMPTY)
                                    return new Tuple<int, int>(x + xx, y + yy);
                        }
                }
            return new Tuple<int, int>(0, 0);
        }

        // CLONE A board OF THE CURRENT GAME BOARD
        public GameBoard Clone()
        {
            GameBoard g = new GameBoard();
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    g[x, y] = this[x,y];
            return g;
            
        }

        // CELL ID'S AND COORDS ASSIGN
        public counters this[int x, int y]
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
                return counters.EMPTY;
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
