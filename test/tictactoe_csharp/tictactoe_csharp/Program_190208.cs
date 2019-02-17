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
        private char s1, s2, s3, s4, s5, s6, s7, s8, s9;
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
        }

        public void DisplayBoard()
        {
            Console.Clear();
            for (int x = 1; x <= 3; x++)
                Console.Write("  " + x + " ");
            Console.WriteLine();
            for (int y = 1; y <= 3; y++)
            {
                Console.Write(y + " ");
                for (int x = 1; x <= 3; x++)
                {
                    if (this[x, y] == '-')
                        Console.Write(" ");
                    else
                        Console.Write(this[x, y]);
                    Console.Write(" | ");
                }
                Console.WriteLine();
                Console.WriteLine("  -   -   -  ");
            }
            Console.WriteLine();
        }

        public bool IsFull()
        {
            for (int x = 1; x <= 3; x++)
                for (int y = 1; y <= 3; y++)
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
                if (x == 1 && y == 2)
                    return s4;
                if (x == 2 && y == 2)
                    return s5;
                if (x == 3 && y == 2)
                    return s6;
                if (x == 1 && y == 3)
                    return s7;
                if (x == 2 && y == 3)
                    return s8;
                if (x == 3 && y == 3)
                    return s9;
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
                if (x == 1 && y == 2)
                    s4 = value;
                if (x == 2 && y == 2)
                    s5 = value;
                if (x == 3 && y == 2)
                    s6 = value;
                if (x == 1 && y == 3)
                    s7 = value;
                if (x == 2 && y == 3)
                    s8 = value;
                if (x == 3 && y == 3)
                    s9 = value;
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
                (board[1, 1] == counter && board[2, 1] == counter && board[3, 1] == counter) ||
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
                while (!(int.TryParse(Console.ReadKey().KeyChar.ToString(), out x) && (x >= 1 && x <= 3)))
                    Console.Write("\nInvalid input. Try again: ");
                Console.Write("\nEnter y coordinate: ");
                while (!(int.TryParse(Console.ReadKey().KeyChar.ToString(), out y) && (y >= 1 && y <= 3)))
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
        public AIPlayer(char _counter) : base(_counter){}

        public override Tuple<int, int> GetMove(GameBoard board)
        {
            return new Tuple<int, int>(0,0);
        }

        /*public int Minimax(GameBoard board, char counter)
        {
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board);
            if (Win(board, this.counter))
                return 10;
            else if (Win(board, this.otherCounter))
                return -10;
            else if (availableMoves.Count == 0)
                return 0;
            List<int> moves = new List<int>();
            for (int i = 0; i < availableMoves.Count; i++)
            {
                
            }
        }*/

        public List<Tuple<int, int>> getAvailableMoves(GameBoard board)
        {
            List<Tuple<int, int>> moves = new List<Tuple<int, int>>();
            for (int x = 1; x <= 3; x++)
                for (int y = 1; y <= 3; y++)
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