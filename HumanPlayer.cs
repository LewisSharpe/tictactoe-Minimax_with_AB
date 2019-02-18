using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
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
            // Debug.Assert();
            return false;
        }
    }
}
