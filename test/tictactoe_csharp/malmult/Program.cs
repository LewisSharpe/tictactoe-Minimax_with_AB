using System;
using System.Collections.Generic;

namespace Minimax
{
    class Program
    {
        public static void Main()
        {
            DisplayMenu();
            int menuChoice;
            while (!(int.TryParse(Console.ReadKey().KeyChar.ToString(), out menuChoice) && (menuChoice >= 1 && menuChoice <= 3)))
                Console.Write("\nInvalid input. Try again: ");
            Console.WriteLine();
            if (menuChoice == 1)
            {
                Console.Write("\nEnter your name: ");
                string name = Console.ReadLine();
                Console.Write("Would you like to be X or O? ");
                char counter;
                while (!(char.TryParse(Console.ReadKey().KeyChar.ToString().ToUpper(), out counter) && (counter == 'X' || counter == 'O')))
                    Console.Write("\nInvalid counter. Enter 'X' or 'O': ");
                Player player = new HumanPlayer(name, counter);
                Player computer = new AIPlayer(player.otherCounter);
                Game game;
                if (counter == 'X')
                    game = new Game(player, computer);
                else
                    game = new Game(computer, player);
            }
            if (menuChoice == 2)
            {
                Console.Write("\nEnter name for X: ");
                string xName = Console.ReadLine();
                Console.Write("Enter name for O: ");
                string oName = Console.ReadLine();
                Player xplayer = new HumanPlayer(xName, 'X');
                Player oplayer = new HumanPlayer(oName, 'O');
                Game game = new Game(xplayer, oplayer);
            }
            Environment.Exit(0);
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tic Tac Toe!");
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Play against the computer");
            Console.WriteLine("2. Play against another player");
            Console.WriteLine("3. Exit");
        }
    }

    struct GameBoard
    {
        private char s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18, s19, s20, s21, s22, s23, s24, s25, s26, s27, s28, s29, s30, s31, s32, s33, s34, s35, s36, s37, s38, s39, s40, s41, s42, s43, s44, s45, s46, s47, s48, s49;
        private char filler;

        public GameBoard(char _filler)
        {
            filler = _filler;
            s1 = filler;
            s2 = filler;
            s3 = filler;
            s4 = filler;
            s5 = filler;
            s6 = filler;
            s7 = filler;
            s8 = filler;
            s9 = filler;
            s10 = filler;
            s11 = filler;
            s12 = filler;
            s13 = filler;
            s14 = filler;
            s15 = filler;
            s16 = filler;
            s17 = filler;
            s18 = filler;
            s19 = filler;
            s20 = filler;
            s21 = filler;
            s22 = filler;
            s23 = filler;
            s24 = filler;
            s25 = filler;
            s26 = filler;
            s27 = filler;
            s28 = filler;
            s29 = filler;
            s30 = filler;
            s31 = filler;
            s32 = filler;
            s33 = filler;
            s34 = filler;
            s35 = filler;
            s36 = filler;
            s37 = filler;
            s38 = filler;
            s39 = filler;
            s40 = filler;
            s41 = filler;
            s42 = filler;
            s43 = filler;
            s44 = filler;
            s45 = filler;
            s46 = filler;
            s47 = filler;
            s48 = filler;
            s49 = filler;

        }

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
                    if (this[x, y] == '-')
                        Console.Write(" ");
                    else
                        Console.Write(this[x, y]);
                    Console.Write(" | ");
                }
                Console.WriteLine();
                Console.WriteLine("  -   -   -   -   -   -   -  ");
            }
            Console.WriteLine();
        }

        public bool IsFull()
        {
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (this[x, y] == filler)
                        return false;
            return true;
        }

        public char this[int x, int y]
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
                return '-';
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
                    s49 = value;
            }
        }
    }

    abstract class Player
    {
        public string name;
        public char counter;
        public char otherCounter;

        public Player(char _counter)
        {
            counter = _counter;
            if (counter == 'X')
                otherCounter = 'O';
            else
                otherCounter = 'X';
        }

        public abstract Tuple<int, int> GetMove(GameBoard board);

        public bool Win(GameBoard board, char counter)
        {
            if (
                (board[1, 1] == counter && board[2, 1] == counter && board[3, 1] == counter ) ||
                (board[1, 2] == counter && board[2, 2] == counter && board[3, 2] == counter) ||
                (board[1, 3] == counter && board[2, 3] == counter && board[3, 3] == counter) ||
                (board[1, 1] == counter && board[1, 2] == counter && board[1, 3] == counter) ||
                (board[2, 1] == counter && board[2, 2] == counter && board[2, 3] == counter) ||
                (board[3, 1] == counter && board[3, 2] == counter && board[3, 3] == counter) ||
                (board[1, 1] == counter && board[2, 2] == counter && board[3, 3] == counter) ||
                (board[1, 3] == counter && board[2, 2] == counter && board[3, 1] == counter)
            )
                return true;
            else
                return false;
        }
    }

    class HumanPlayer : Player
    {
        public HumanPlayer(string _name, char _counter) : base(_counter)
        {
            name = _name;
        }

        public override Tuple<int, int> GetMove(GameBoard board)
        {
            int x;
            int y;
            Console.WriteLine("It's {0}'s turn.", name);
            Console.WriteLine();
            do
            {
                Console.Write("Enter x coordinate: ");
                while (!(int.TryParse(Console.ReadKey().KeyChar.ToString(), out x) && (x >= 1 && x <= 7)))
                    Console.Write("\nInvalid input. Try again: ");
                Console.Write("\nEnter y coordinate: ");
                while (!(int.TryParse(Console.ReadKey().KeyChar.ToString(), out y) && (y >= 1 && y <= 7)))
                    Console.Write("\nInvalid input. Try again: ");
            } while (!CheckValidMove(board, x, y));
            return new Tuple<int, int>(x, y);
        }

        public bool CheckValidMove(GameBoard board, int x, int y)
        {
            if (board[x, y] == '-')
                return true;
            Console.WriteLine("\nThere is already a counter at ({0}, {1}). Try again.", x, y);
            return false;
        }
    }

    class AIPlayer : Player
    {
        public AIPlayer(char _counter) : base(_counter) { }

        public override Tuple<int, int> GetMove(GameBoard board)
        {
            return new Tuple<int, int>(0, 0);
        }

          public List<Tuple<int, int>> getAvailableMoves(GameBoard board)
        {
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
            for (int x = 1; x <= 7; x++)
                for (int y = 1; y <= 7; y++)
                    if (board[x, y] == '-')
                        moves.Add(new Tuple<int, int>(x, y));
            return moves;
        }
    }

    class Game
    {
        GameBoard board = new GameBoard('-');

        public Game(Player _xPlayer, Player _oPlayer)
        {
            PlayGame(_xPlayer, _oPlayer);
        }

        public void PlayGame(Player currentPlayer, Player otherPlayer)
        {
            board.DisplayBoard();
            Tuple<int, int> selectedMove = currentPlayer.GetMove(board);
            board[selectedMove.Item1, selectedMove.Item2] = currentPlayer.counter;
            if (IsOver(board, currentPlayer))
            {
                if (currentPlayer.Win(board, currentPlayer.counter))
                {
                    board.DisplayBoard();
                    if (currentPlayer.GetType() == typeof(AIPlayer))
                    {
                        Console.WriteLine("The computer has beaten you. Better luck next time...");
                    }
                    else
                    {
                        Console.WriteLine("Congratulations! {0} has won!", currentPlayer.name);
                    }
                    Console.ReadLine();
                    Program.Main();
                }
                Console.WriteLine("The game is a draw.");
                Console.ReadLine();
                Program.Main();
            }
            PlayGame(otherPlayer, currentPlayer);
        }

        public bool IsOver(GameBoard board, Player currentPlayer)
        {
            if (currentPlayer.Win(board, currentPlayer.counter) || board.IsFull())
                return true;
            return false;
        }
    }
}
