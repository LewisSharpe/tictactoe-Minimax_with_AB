using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Minimax_TPL
{
    // STATIC CONSTANTS USED TO INFORM Game_TPLPLAY
    static class Consts
    {
        public const int MAX_SCORE = 1001;
        public const int MIN_SCORE = -1001;
        public const int NO_OF_DIRS = 3;
        public static readonly int[] DIRECTIONS = { 1, 9, 10 };
    }

    // COUNTER TYPES - ABLE TO PLACE OF BOARD
    public enum counters
    {
        O,
        X,
        BORDER,
        N,
        e,
        
    }

    // MAIN EXECUTION
    class Program
    {
        public static void Main()
        {
            // MENU CHOICE PROMPT
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            DisplayMenu();
            int menuChoice  = 2;  // HWL: hard-wire input so that you can redirect output to a file
	    /*
            while (!(int.TryParse(Console.ReadKey().KeyChar.ToString(), out menuChoice) && (menuChoice >= 1 && menuChoice <= 4)))
                Console.Write("\nInvalid input. Try again: ");
            Console.WriteLine();
	    */
	    
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
                    counter = counters.X;
                }
                else if (new_counter == 'O')
                {
                    counter = counters.O;
                }
                else
                {
                    counter = counters.e;
                }

                Player_TPL Player_TPL = new HumanPlayer_TPL(name, counter);
                Player_TPL computer = new AIPlayer_TPL(Player_TPL.otherCounter);
                Game_TPL Game_TPL;
                if (counter == counters.X)
                    Game_TPL = new Game_TPL(Player_TPL, computer);
                else
                    Game_TPL = new Game_TPL(computer, Player_TPL);
            }
            // SELECTION 2 AI V AI
            if (menuChoice == 2)
            {
                counters counter;
                counter = counters.X;
                Player_TPL Player_TPL = new AIPlayer_TPL(counter);
                Player_TPL computer = new AIPlayer_TPL(Player_TPL.otherCounter);
                Game_TPL Game_TPL;
                if (counter == counters.X)
                    Game_TPL = new Game_TPL(Player_TPL, computer);
                else
                    Game_TPL = new Game_TPL(computer, Player_TPL);
            }
            // SELECTION 3 HUMAN V HUMAN
            if (menuChoice == 3)
            {
                Console.Write("\nEnter name for X: ");
                string xName = Console.ReadLine();
                Console.Write("Enter name for O: ");
                string oName = Console.ReadLine();
                Player_TPL xPlayer_TPL = new HumanPlayer_TPL(xName, counters.X);
                Player_TPL oPlayer_TPL = new HumanPlayer_TPL(oName, counters.O);
                Game_TPL Game_TPL = new Game_TPL(xPlayer_TPL, oPlayer_TPL); // play Game_TPL execution
            }
            // SELECTION 4 EXIT
            Environment.Exit(0);
        }

        // DISPLAY MENU OPTIONS TO USER
        static void DisplayMenu()
        {
            
            // menu centered
            Console.Clear();
            /*
            string welcome = "Welcome to Tic Tac Toe! HWL TEST VERSION";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (welcome.Length / 2)) + "}", welcome));
            string menu = "Menu:";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (menu.Length / 2)) + "}", menu));
            string o1 = "1.Play against the computer";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (o1.Length / 2)) + "}", o1));
            string o2 = "2. Play AI against itself";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (o2.Length / 2)) + "}", o2));
            string o3 = "3. Play against another Player_TPL";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (o3.Length / 2)) + "}", o3));
            string o4 = "4. Exit";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (o4.Length / 2)) + "}", o4));
            */
      }
    }
}