using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Minimax
{
    static class Consts
    {
        public const int MAX_SCORE = 1000;
        public const int MIN_SCORE = -1000;
        public const int NO_OF_DIRS = 3;
        public static readonly int[] DIRECTIONS = {1, 9, 10};
    }

    public enum counters
    {
        NOUGHTS,
        CROSSES,
        BORDER,
        EMPTY
    }

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
                GameBoard board;
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

   

    

   

    
}
