using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace Minimax
{
    // STATIC CONSTANTS USED TO INFORM GAMEPLAY
    static class Consts
    {
        public const int MAX_SCORE = 1000;
        public const int MIN_SCORE = -1000;
        public const int NO_OF_DIRS = 3;
        public static readonly int[] DIRECTIONS = {1, 9, 10};
    }
    // COUNTER TYPES - ABLE TO PLACE OF BOARD
    public enum counters
    {
        NOUGHTS,
        CROSSES,
        BORDER,
        EMPTY
    }

    // MAIN EXECUTION
    class Program
    {
        public static void Main()
        {
            // MENU CHOICE PROMPT
            DisplayMenu();
            int menuChoice;
            while (!(int.TryParse(Console.ReadKey().KeyChar.ToString(), out menuChoice) && (menuChoice >= 1 && menuChoice <= 4)))
                Console.Write("\nInvalid input. Try again: ");
            Console.WriteLine();

            // SELECTION 1 HUMAN V AI
            if (menuChoice == 1)
            {
                Console.Write("\nEnter your name: ");
                string name = Console.ReadLine();
                Console.Write("Would you like to be X or O? ");
                counters counter;
                char new_counter;
                while (!(char.TryParse(Console.ReadKey().KeyChar.ToString().ToUpper(), out new_counter) && (new_counter == 'X' || new_counter == 'O')))
                    Console.Write("\nInvalid counter. Enter 'X' or 'O': ");
                if (new_counter == 'X')
                {
                    counter = counters.CROSSES;
                }
                else if (new_counter == 'O')
                {
                    counter = counters.NOUGHTS;
                }
                else
                {
                    counter = counters.EMPTY;
                }

                Player player = new HumanPlayer(name,counter);
                Player computer = new AIPlayer(player.otherCounter);
                Game game;
                if (counter == counters.CROSSES)
                    game = new Game(player, computer);
                else
                    game = new Game(computer, player);
            }
            // SELECTION 2 AI V AI
            if (menuChoice == 2)
            {
                Console.WriteLine("Press return");
                counters counter; 
                counter = counters.CROSSES;
                    
                Player player = new AIPlayer(counter);
                Player computer = new AIPlayer(player.otherCounter);
                Game game;
                if (counter == counters.CROSSES)
                    game = new Game(player, computer);
                else
                    game = new Game(computer, player);
            }
            // SELECTION 3 HUMAN V HUMAN
            if (menuChoice == 3)
            {
                Console.Write("\nEnter name for X: ");
                string xName = Console.ReadLine();
                Console.Write("Enter name for O: ");
                string oName = Console.ReadLine();
                Player xplayer = new HumanPlayer(xName, counters.CROSSES);
                Player oplayer = new HumanPlayer(oName, counters.NOUGHTS);
                Game game = new Game(xplayer, oplayer); // play game execution
            }
            // SELECTION 4 EXIT
            Environment.Exit(0);
        }

        // DISPLAY MENU OPTIONS TO USER
        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tic Tac Toe!");
            Console.WriteLine();
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Play against the computer");
            Console.WriteLine("2. Play AI against itself");
            Console.WriteLine("3. Play against another player");
            Console.WriteLine("4. Exit");
        }
    }
}
