using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax_TPL
{
    // Player_TPL CLASS
    abstract class Player_TPL
    {
        public string name;
        public static counters counter;
        public counters otherCounter;
        protected  counters _counter;
        
        // COUNTER ASSIGNMENT FOR Player_TPL
        public Player_TPL(counters _counter)
        {
            counter = _counter;
            if (counter == counters.O) 
                otherCounter = counters.X;
            else if (counter == counters.X)
                otherCounter = counters.O;
        }
                public abstract void GetMove(GameBoard_TPL<counters> board, GameBoard_TPL<int> scoreBoard);
       
        // DETERMINE IF Player_TPL HAS WON
        public static bool Win(GameBoard_TPL<counters> board, counters counter)
        {
            if (
                //HORIZONTAL
                (board[1, 1] == counter && board[2, 1] == counter && board[3, 1] == counter) ||
                (board[2, 1] == counter && board[3, 1] == counter && board[4, 1] == counter) ||
                (board[3, 1] == counter && board[4, 1] == counter && board[5, 1] == counter) ||
                (board[4, 1] == counter && board[5, 1] == counter && board[6, 1] == counter) ||
                (board[5, 1] == counter && board[6, 1] == counter && board[7, 1] == counter) ||
                (board[1, 2] == counter && board[2, 2] == counter && board[3, 2] == counter) ||
                (board[2, 2] == counter && board[3, 2] == counter && board[4, 2] == counter) ||
                (board[3, 2] == counter && board[4, 2] == counter && board[5, 2] == counter) ||
                (board[4, 2] == counter && board[5, 2] == counter && board[6, 2] == counter) ||
                (board[5, 2] == counter && board[6, 2] == counter && board[7, 2] == counter) ||
                (board[1, 3] == counter && board[2, 3] == counter && board[3, 3] == counter) ||
                (board[2, 3] == counter && board[3, 3] == counter && board[4, 3] == counter) ||
                (board[3, 3] == counter && board[4, 3] == counter && board[5, 3] == counter) ||
                (board[4, 3] == counter && board[5, 3] == counter && board[6, 3] == counter) ||
                (board[5, 3] == counter && board[6, 3] == counter && board[7, 3] == counter) ||
                (board[1, 4] == counter && board[2, 4] == counter && board[3, 4] == counter) ||
                (board[2, 4] == counter && board[3, 4] == counter && board[4, 4] == counter) ||
                (board[3, 4] == counter && board[4, 4] == counter && board[5, 4] == counter) ||
                (board[4, 4] == counter && board[5, 4] == counter && board[6, 4] == counter) ||
                (board[5, 4] == counter && board[6, 4] == counter && board[7, 4] == counter) ||
                (board[1, 5] == counter && board[2, 5] == counter && board[3, 5] == counter) ||
                (board[2, 5] == counter && board[3, 5] == counter && board[4, 5] == counter) ||
                (board[3, 5] == counter && board[4, 5] == counter && board[5, 5] == counter) ||
                (board[4, 5] == counter && board[5, 5] == counter && board[6, 5] == counter) ||
                (board[5, 5] == counter && board[6, 5] == counter && board[7, 5] == counter) ||
                (board[1, 6] == counter && board[2, 6] == counter && board[3, 6] == counter) ||
                (board[2, 6] == counter && board[3, 6] == counter && board[4, 6] == counter) ||
                (board[3, 6] == counter && board[4, 6] == counter && board[5, 6] == counter) ||
                (board[4, 6] == counter && board[5, 6] == counter && board[6, 6] == counter) ||
                (board[5, 6] == counter && board[6, 6] == counter && board[7, 6] == counter) ||
                (board[1, 7] == counter && board[2, 7] == counter && board[3, 7] == counter) ||
                (board[2, 7] == counter && board[3, 7] == counter && board[4, 7] == counter) ||
                (board[3, 7] == counter && board[4, 7] == counter && board[5, 7] == counter) ||
                (board[4, 7] == counter && board[5, 7] == counter && board[6, 7] == counter) ||
                (board[5, 7] == counter && board[6, 7] == counter && board[7, 7] == counter) ||
                //VERTICAL
                (board[1, 1] == counter && board[1, 2] == counter && board[1, 3] == counter) ||
                (board[1, 2] == counter && board[1, 3] == counter && board[1, 4] == counter) ||
                (board[1, 3] == counter && board[1, 4] == counter && board[1, 5] == counter) ||
                (board[1, 4] == counter && board[1, 5] == counter && board[1, 6] == counter) ||
                (board[1, 5] == counter && board[1, 6] == counter && board[1, 7] == counter) ||
                (board[2, 1] == counter && board[2, 2] == counter && board[2, 3] == counter) ||
                (board[2, 2] == counter && board[2, 3] == counter && board[2, 4] == counter) ||
                (board[2, 3] == counter && board[2, 4] == counter && board[2, 5] == counter) ||
                (board[2, 4] == counter && board[2, 5] == counter && board[2, 6] == counter) ||
                (board[2, 5] == counter && board[2, 6] == counter && board[2, 7] == counter) ||
                (board[3, 1] == counter && board[3, 2] == counter && board[3, 3] == counter) ||
                (board[3, 2] == counter && board[3, 3] == counter && board[3, 4] == counter) ||
                (board[3, 3] == counter && board[3, 4] == counter && board[3, 5] == counter) ||
                (board[3, 4] == counter && board[3, 5] == counter && board[3, 6] == counter) ||
                (board[3, 5] == counter && board[3, 6] == counter && board[3, 7] == counter) ||
                (board[4, 1] == counter && board[4, 2] == counter && board[4, 3] == counter) ||
                (board[4, 2] == counter && board[4, 3] == counter && board[4, 4] == counter) ||
                (board[4, 3] == counter && board[4, 4] == counter && board[4, 5] == counter) ||
                (board[4, 4] == counter && board[4, 5] == counter && board[4, 6] == counter) ||
                (board[4, 5] == counter && board[4, 6] == counter && board[4, 7] == counter) ||
                (board[5, 1] == counter && board[5, 2] == counter && board[5, 3] == counter) ||
                (board[5, 2] == counter && board[5, 3] == counter && board[5, 4] == counter) ||
                (board[5, 3] == counter && board[5, 4] == counter && board[5, 5] == counter) ||
                (board[5, 4] == counter && board[5, 5] == counter && board[5, 6] == counter) ||
                (board[5, 5] == counter && board[5, 6] == counter && board[5, 7] == counter) ||
                (board[6, 1] == counter && board[6, 2] == counter && board[6, 3] == counter) ||
                (board[6, 2] == counter && board[6, 3] == counter && board[6, 4] == counter) ||
                (board[6, 3] == counter && board[6, 4] == counter && board[6, 5] == counter) ||
                (board[6, 4] == counter && board[6, 5] == counter && board[6, 6] == counter) ||
                (board[6, 5] == counter && board[6, 6] == counter && board[6, 7] == counter) ||
                (board[7, 1] == counter && board[7, 2] == counter && board[7, 3] == counter) ||
                (board[7, 2] == counter && board[7, 3] == counter && board[7, 4] == counter) ||
                (board[7, 3] == counter && board[7, 4] == counter && board[7, 5] == counter) ||
                (board[7, 4] == counter && board[7, 5] == counter && board[7, 6] == counter) ||
                (board[7, 5] == counter && board[7, 6] == counter && board[7, 7] == counter) ||
                //DIAGONAL
                (board[1, 1] == counter && board[2, 2] == counter && board[3, 3] == counter) ||
                (board[2, 2] == counter && board[3, 3] == counter && board[4, 4] == counter) ||
                (board[3, 3] == counter && board[4, 4] == counter && board[5, 5] == counter) ||
                (board[4, 4] == counter && board[5, 5] == counter && board[6, 6] == counter) ||
                (board[5, 5] == counter && board[6, 6] == counter && board[7, 7] == counter) ||
                (board[1, 7] == counter && board[2, 6] == counter && board[3, 5] == counter) ||
                (board[2, 6] == counter && board[3, 5] == counter && board[4, 4] == counter) ||
                (board[3, 5] == counter && board[2, 6] == counter && board[1, 7] == counter) ||
                (board[1, 2] == counter && board[2, 3] == counter && board[3, 4] == counter) ||
                (board[2, 3] == counter && board[3, 4] == counter && board[4, 5] == counter) ||
                (board[3, 4] == counter && board[4, 5] == counter && board[5, 6] == counter) ||
                (board[4, 5] == counter && board[5, 6] == counter && board[6, 7] == counter) ||
                (board[1, 3] == counter && board[2, 4] == counter && board[3, 5] == counter) ||
                (board[2, 4] == counter && board[3, 5] == counter && board[4, 6] == counter) ||
                (board[3, 5] == counter && board[4, 6] == counter && board[5, 7] == counter) ||
                (board[1, 4] == counter && board[2, 5] == counter && board[3, 6] == counter) ||
                (board[2, 5] == counter && board[3, 6] == counter && board[4, 5] == counter) ||
                (board[2, 1] == counter && board[3, 2] == counter && board[4, 3] == counter) ||
                (board[3, 2] == counter && board[4, 3] == counter && board[5, 4] == counter) ||
                (board[4, 3] == counter && board[5, 4] == counter && board[6, 5] == counter) ||
                (board[5, 4] == counter && board[6, 5] == counter && board[7, 6] == counter) ||
                (board[3, 1] == counter && board[4, 2] == counter && board[5, 3] == counter) ||
                (board[4, 2] == counter && board[5, 6] == counter && board[6, 4] == counter) ||
                (board[5, 3] == counter && board[6, 4] == counter && board[7, 5] == counter) ||
                (board[4, 1] == counter && board[5, 2] == counter && board[6, 3] == counter) ||
                (board[5, 2] == counter && board[6, 3] == counter && board[7, 1] == counter) ||
                (board[5, 1] == counter && board[6, 2] == counter && board[7, 3] == counter) ||
                (board[7, 2] == counter && board[6, 3] == counter && board[5, 4] == counter) ||
                (board[6, 3] == counter && board[5, 4] == counter && board[4, 5] == counter) ||
                (board[5, 4] == counter && board[4, 5] == counter && board[3, 6] == counter) ||
                (board[4, 5] == counter && board[3, 6] == counter && board[2, 7] == counter) ||
                (board[5, 5] == counter && board[6, 5] == counter && board[7, 5] == counter) ||
                (board[5, 6] == counter && board[6, 6] == counter && board[7, 6] == counter) ||
                (board[7, 3] == counter && board[6, 4] == counter && board[5, 5] == counter) ||
                (board[6, 4] == counter && board[5, 5] == counter && board[4, 6] == counter) ||
                (board[5, 5] == counter && board[4, 6] == counter && board[3, 7] == counter) ||
                (board[7, 4] == counter && board[6, 5] == counter && board[5, 6] == counter) ||
                (board[6, 5] == counter && board[5, 6] == counter && board[4, 7] == counter) ||
                (board[7, 5] == counter && board[6, 6] == counter && board[5, 7] == counter) ||
                (board[7, 7] == counter && board[6, 6] == counter && board[5, 5] == counter) ||
                (board[6, 6] == counter && board[5, 5] == counter && board[4, 4] == counter) ||
                (board[5, 5] == counter && board[4, 4] == counter && board[3, 3] == counter) ||
                (board[4, 4] == counter && board[3, 3] == counter && board[2, 2] == counter) ||
                (board[3, 3] == counter && board[2, 2] == counter && board[1, 1] == counter) ||
                (board[6, 7] == counter && board[5, 6] == counter && board[4, 5] == counter) ||
                (board[5, 6] == counter && board[4, 5] == counter && board[3, 4] == counter) ||
                (board[4, 5] == counter && board[3, 4] == counter && board[2, 3] == counter) ||
                (board[3, 4] == counter && board[2, 3] == counter && board[1, 2] == counter) ||
                (board[5, 7] == counter && board[4, 6] == counter && board[3, 5] == counter) ||
                (board[4, 6] == counter && board[3, 5] == counter && board[2, 4] == counter) ||
                (board[3, 5] == counter && board[2, 4] == counter && board[1, 3] == counter) ||
                (board[4, 7] == counter && board[3, 6] == counter && board[2, 5] == counter) ||
                (board[3, 6] == counter && board[2, 5] == counter && board[1, 4] == counter) ||
                (board[7, 3] == counter && board[6, 2] == counter && board[1, 5] == counter) ||
                (board[1, 6] == counter && board[2, 5] == counter && board[3, 4] == counter) ||
                (board[2, 5] == counter && board[3, 4] == counter && board[4, 3] == counter) ||
                (board[3, 4] == counter && board[4, 3] == counter && board[2, 5] == counter) ||
                (board[4, 3] == counter && board[2, 5] == counter && board[1, 6] == counter) ||
                (board[1, 5] == counter && board[2, 4] == counter && board[3, 3] == counter) ||
                (board[2, 4] == counter && board[3, 3] == counter && board[4, 2] == counter) ||
                (board[3, 3] == counter && board[4, 2] == counter && board[5, 1] == counter) ||
                (board[1, 4] == counter && board[2, 3] == counter && board[3, 2] == counter) ||
                (board[2, 3] == counter && board[3, 2] == counter && board[4, 1] == counter) ||
                (board[1, 3] == counter && board[2, 2] == counter && board[3, 1] == counter) ||
                (board[7, 1] == counter && board[6, 2] == counter && board[5, 3] == counter) ||
                (board[6, 2] == counter && board[5, 3] == counter && board[4, 4] == counter) ||
                (board[5, 3] == counter && board[4, 4] == counter && board[3, 5] == counter) ||
                (board[4, 4] == counter && board[3, 5] == counter && board[2, 6] == counter) ||
                (board[3, 5] == counter && board[2, 6] == counter && board[1, 7] == counter) ||
                (board[7, 2] == counter && board[6, 3] == counter && board[5, 4] == counter) ||
                (board[6, 3] == counter && board[5, 4] == counter && board[4, 5] == counter) ||
                (board[5, 4] == counter && board[4, 5] == counter && board[3, 6] == counter) ||
                (board[4, 5] == counter && board[3, 6] == counter && board[2, 7] == counter) ||
                (board[7, 3] == counter && board[6, 4] == counter && board[5, 2] == counter) ||
                (board[6, 4] == counter && board[5, 5] == counter && board[4, 6] == counter) ||
                (board[5, 5] == counter && board[4, 6] == counter && board[3, 7] == counter) ||
                (board[7, 4] == counter && board[6, 5] == counter && board[5, 6] == counter) ||
                (board[6, 5] == counter && board[5, 6] == counter && board[4, 7] == counter) ||
                (board[7, 5] == counter && board[6, 6] == counter && board[5, 7] == counter) ||
                (board[6, 1] == counter && board[5, 2] == counter && board[4, 3] == counter) ||
                (board[5, 2] == counter && board[4, 3] == counter && board[3, 4] == counter) ||
                (board[4, 3] == counter && board[3, 4] == counter && board[2, 6] == counter) ||
                (board[3, 4] == counter && board[2, 6] == counter && board[1, 7] == counter) ||
                (board[5, 1] == counter && board[4, 2] == counter && board[3, 3] == counter) ||
                (board[4, 2] == counter && board[3, 3] == counter && board[2, 4] == counter) ||
                (board[3, 3] == counter && board[2, 4] == counter && board[1, 3] == counter) ||
                (board[5, 7] == counter && board[4, 6] == counter && board[3, 5] == counter) ||
                (board[4, 6] == counter && board[3, 5] == counter && board[2, 6] == counter) ||
                (board[3, 7] == counter && board[2, 6] == counter && board[1, 5] == counter))
            return true;            
            // Win at position (x,y), position (x,y), position(x,y)
            else
                Two(board, counter);
            return false;
        }

        // DETERMINE IF Player_TPL HAS TWO
        public static bool Two(GameBoard_TPL<counters> board, counters counter)
        {
            if (
                //HORIZONTAL
                (board[1, 1] == counter && board[2, 1] == counter) ||
                (board[2, 1] == counter && board[3, 1] == counter) ||
                (board[3, 1] == counter && board[4, 1] == counter) ||
                (board[4, 1] == counter && board[5, 1] == counter) ||
                (board[5, 1] == counter && board[6, 1] == counter) ||
                (board[6, 1] == counter && board[7, 1] == counter) ||
                (board[1, 2] == counter && board[2, 2] == counter) ||
                (board[2, 2] == counter && board[3, 2] == counter) ||
                (board[3, 2] == counter && board[4, 2] == counter) ||
                (board[4, 2] == counter && board[5, 2] == counter) ||
                (board[5, 2] == counter && board[6, 2] == counter) ||
                (board[6, 2] == counter && board[7, 2] == counter) ||
                (board[1, 3] == counter && board[2, 3] == counter) ||
                (board[2, 3] == counter && board[3, 3] == counter) ||
                (board[3, 3] == counter && board[4, 3] == counter) ||
                (board[4, 3] == counter && board[5, 3] == counter) ||
                (board[5, 3] == counter && board[6, 3] == counter) ||
                (board[6, 3] == counter && board[7, 3] == counter) ||
                (board[1, 4] == counter && board[2, 4] == counter) ||
                (board[2, 4] == counter && board[3, 4] == counter) ||
                (board[3, 4] == counter && board[4, 4] == counter) ||
                (board[4, 4] == counter && board[5, 4] == counter) ||
                (board[5, 4] == counter && board[6, 4] == counter) ||
                (board[6, 4] == counter && board[7, 4] == counter) ||
                (board[1, 5] == counter && board[2, 5] == counter) ||
                (board[2, 5] == counter && board[3, 5] == counter) ||
                (board[3, 5] == counter && board[4, 5] == counter) ||
                (board[4, 5] == counter && board[5, 5] == counter) ||
                (board[5, 5] == counter && board[6, 5] == counter) ||
                (board[6, 5] == counter && board[7, 5] == counter) ||
                (board[1, 6] == counter && board[2, 6] == counter) ||
                (board[2, 6] == counter && board[3, 6] == counter) ||
                (board[3, 6] == counter && board[4, 6] == counter) ||
                (board[4, 6] == counter && board[5, 6] == counter) ||
                (board[5, 6] == counter && board[6, 6] == counter) ||
                (board[6, 6] == counter && board[7, 6] == counter) ||
                (board[1, 7] == counter && board[2, 7] == counter) ||
                (board[2, 7] == counter && board[3, 7] == counter) ||
                (board[3, 7] == counter && board[4, 7] == counter) ||
                (board[4, 7] == counter && board[5, 7] == counter) ||
                (board[5, 7] == counter && board[6, 7] == counter) ||
                (board[6, 7] == counter && board[7, 7] == counter) ||
                //VERTICAL
                (board[1, 1] == counter && board[1, 2] == counter) ||
                (board[1, 2] == counter && board[1, 3] == counter) ||
                (board[1, 3] == counter && board[1, 4] == counter) ||
                (board[1, 4] == counter && board[1, 5] == counter) ||
                (board[1, 5] == counter && board[1, 6] == counter) ||
                (board[1, 6] == counter && board[1, 7] == counter) ||
                (board[2, 1] == counter && board[2, 2] == counter) ||
                (board[2, 2] == counter && board[2, 3] == counter) ||
                (board[2, 3] == counter && board[2, 4] == counter) ||
                (board[2, 4] == counter && board[2, 5] == counter) ||
                (board[2, 5] == counter && board[2, 6] == counter) ||
                (board[2, 6] == counter && board[2, 7] == counter) ||
                (board[3, 1] == counter && board[3, 2] == counter) ||
                (board[3, 2] == counter && board[3, 3] == counter) ||
                (board[3, 3] == counter && board[3, 4] == counter) ||
                (board[3, 4] == counter && board[3, 5] == counter) ||
                (board[3, 5] == counter && board[3, 6] == counter) ||
                (board[3, 6] == counter && board[3, 7] == counter) ||
                (board[4, 1] == counter && board[4, 2] == counter) ||
                (board[4, 2] == counter && board[4, 3] == counter) ||
                (board[4, 3] == counter && board[4, 4] == counter) ||
                (board[4, 4] == counter && board[4, 5] == counter) ||
                (board[4, 5] == counter && board[4, 6] == counter) ||
                (board[4, 6] == counter && board[4, 7] == counter) ||
                (board[5, 1] == counter && board[5, 2] == counter) ||
                (board[5, 2] == counter && board[5, 3] == counter) ||
                (board[5, 3] == counter && board[5, 4] == counter) ||
                (board[5, 4] == counter && board[5, 5] == counter) ||
                (board[5, 5] == counter && board[5, 6] == counter) ||
                (board[5, 6] == counter && board[5, 7] == counter) ||
                (board[6, 1] == counter && board[6, 2] == counter) ||
                (board[6, 2] == counter && board[6, 3] == counter) ||
                (board[6, 3] == counter && board[6, 4] == counter) ||
                (board[6, 4] == counter && board[6, 5] == counter) ||
                (board[6, 5] == counter && board[6, 6] == counter) ||
                (board[6, 6] == counter && board[6, 7] == counter) ||
                (board[7, 1] == counter && board[7, 2] == counter) ||
                (board[7, 2] == counter && board[7, 3] == counter) ||
                (board[7, 3] == counter && board[7, 4] == counter) ||
                (board[7, 4] == counter && board[7, 5] == counter) ||
                (board[7, 5] == counter && board[7, 6] == counter) ||
                (board[7, 6] == counter && board[7, 7] == counter) ||
                //DIAGONAL
                (board[1, 1] == counter && board[2, 2] == counter) ||
                (board[2, 2] == counter && board[3, 3] == counter) ||
                (board[3, 3] == counter && board[4, 4] == counter) ||
                (board[4, 4] == counter && board[5, 5] == counter) ||
                (board[5, 5] == counter && board[6, 6] == counter) ||
                (board[6, 6] == counter && board[7, 7] == counter) ||
                (board[1, 7] == counter && board[2, 6] == counter) ||
                (board[2, 6] == counter && board[3, 5] == counter) ||
                (board[3, 5] == counter && board[4, 4] == counter) ||
                (board[3, 5] == counter && board[2, 6] == counter) ||
                (board[2, 6] == counter && board[1, 7] == counter) ||
                (board[1, 2] == counter && board[2, 3] == counter) ||
                (board[2, 3] == counter && board[3, 4] == counter) ||
                (board[3, 4] == counter && board[4, 5] == counter) ||
                (board[4, 5] == counter && board[5, 6] == counter) ||
                (board[5, 6] == counter && board[6, 7] == counter) ||
                (board[1, 3] == counter && board[2, 4] == counter) ||
                (board[2, 4] == counter && board[3, 5] == counter) ||
                (board[3, 5] == counter && board[4, 6] == counter) ||
                (board[4, 6] == counter && board[5, 7] == counter) ||
                (board[1, 4] == counter && board[2, 5] == counter) ||
                (board[2, 5] == counter && board[3, 6] == counter) ||
                (board[3, 6] == counter && board[4, 5] == counter) ||
                (board[2, 1] == counter && board[3, 2] == counter) ||
                (board[3, 2] == counter && board[4, 3] == counter) ||
                (board[4, 3] == counter && board[5, 4] == counter) ||
                (board[5, 4] == counter && board[6, 5] == counter) ||
                (board[6, 5] == counter && board[7, 6] == counter) ||
                (board[3, 1] == counter && board[4, 2] == counter) ||
                //
                (board[4, 2] == counter && board[5, 3] == counter) ||
                (board[5, 6] == counter && board[6, 4] == counter) ||
                (board[5, 3] == counter && board[6, 4] == counter) ||
                (board[6, 4] == counter && board[7, 5] == counter) ||
                (board[4, 1] == counter && board[5, 2] == counter) ||
                (board[5, 2] == counter && board[6, 3] == counter) ||
                (board[6, 3] == counter && board[7, 1] == counter) ||
                (board[5, 1] == counter && board[6, 2] == counter) ||
                (board[6, 2] == counter && board[7, 3] == counter) ||
                (board[7, 2] == counter && board[6, 3] == counter) ||
                (board[6, 3] == counter && board[5, 4] == counter) ||
                (board[5, 4] == counter && board[4, 5] == counter) ||
                (board[4, 5] == counter && board[3, 6] == counter) ||
                (board[3, 6] == counter && board[2, 7] == counter) ||
                (board[5, 5] == counter && board[6, 5] == counter) ||
                (board[6, 5] == counter && board[7, 5] == counter) |
                (board[5, 6] == counter && board[6, 6] == counter) ||
                (board[6, 6] == counter && board[7, 6] == counter) ||
                (board[7, 3] == counter && board[6, 4] == counter) ||
                (board[6, 4] == counter && board[5, 5] == counter) ||
                (board[6, 4] == counter && board[5, 5] == counter) ||
                (board[5, 5] == counter && board[4, 6] == counter) |
                (board[5, 5] == counter && board[4, 6] == counter) ||
                (board[4, 6] == counter && board[3, 7] == counter) ||
                (board[7, 4] == counter && board[6, 5] == counter) ||
                (board[6, 5] == counter && board[5, 6] == counter) ||
                (board[5, 6] == counter && board[4, 7] == counter) ||
                (board[7, 5] == counter && board[6, 6] == counter) ||
                (board[6, 6] == counter && board[5, 7] == counter) ||
                (board[7, 7] == counter && board[6, 6] == counter) ||
                (board[6, 6] == counter && board[5, 5] == counter) ||
                (board[5, 5] == counter && board[4, 4] == counter) ||
                (board[4, 4] == counter && board[3, 3] == counter) ||
                (board[3, 3] == counter && board[2, 2] == counter) ||
                (board[2, 2] == counter && board[1, 1] == counter) ||
                (board[6, 7] == counter && board[5, 6] == counter) ||
                (board[5, 6] == counter && board[4, 5] == counter) ||
                (board[4, 5] == counter && board[3, 4] == counter) ||
                (board[3, 4] == counter && board[2, 3] == counter) ||
                (board[2, 3] == counter && board[1, 2] == counter) ||
                (board[5, 7] == counter && board[4, 6] == counter) ||
                (board[4, 6] == counter && board[3, 5] == counter) ||
                (board[3, 5] == counter && board[2, 4] == counter) ||
                (board[2, 4] == counter && board[1, 3] == counter) ||
                (board[4, 7] == counter && board[3, 6] == counter) ||
                (board[3, 6] == counter && board[2, 5] == counter) ||
                (board[2, 5] == counter && board[1, 4] == counter) ||
                (board[7, 3] == counter && board[6, 4] == counter) ||
                (board[6, 4] == counter && board[5, 2] == counter) ||
                (board[1, 6] == counter && board[2, 5] == counter) ||
                (board[2, 5] == counter && board[3, 4] == counter) ||
                (board[3, 4] == counter && board[4, 3] == counter) ||
                (board[4, 3] == counter && board[2, 5] == counter) ||
                (board[2, 5] == counter && board[1, 6] == counter) ||
                (board[1, 5] == counter && board[2, 4] == counter) ||
                (board[2, 4] == counter && board[3, 3] == counter) ||
                (board[3, 3] == counter && board[4, 2] == counter) ||
                (board[4, 2] == counter && board[5, 1] == counter) ||
                (board[1, 4] == counter && board[2, 3] == counter) ||
                (board[3, 2] == counter && board[4, 1] == counter) ||
                (board[1, 3] == counter && board[2, 2] == counter) ||
                (board[2, 2] == counter && board[3, 1] == counter) ||
                (board[7, 1] == counter && board[6, 2] == counter) ||
                (board[6, 2] == counter && board[5, 3] == counter) ||
                (board[5, 3] == counter && board[4, 4] == counter) ||
                (board[4, 4] == counter && board[3, 5] == counter) ||
                (board[3, 5] == counter && board[2, 6] == counter) ||
                (board[2, 6] == counter && board[1, 7] == counter) ||
                (board[7, 2] == counter && board[6, 3] == counter) ||
                (board[6, 3] == counter && board[5, 4] == counter) ||
                (board[5, 4] == counter && board[4, 5] == counter) ||
                (board[4, 5] == counter && board[3, 6] == counter) ||
                (board[3, 6] == counter && board[2, 7] == counter) ||
                (board[7, 3] == counter && board[6, 4] == counter) ||
                (board[6, 4] == counter && board[5, 5] == counter) ||
                (board[5, 5] == counter && board[4, 6] == counter) ||
                (board[4, 6] == counter && board[3, 7] == counter) |
                (board[7, 4] == counter && board[6, 5] == counter) ||
                (board[6, 5] == counter && board[5, 6] == counter) ||
                (board[6, 5] == counter && board[5, 6] == counter) ||
                (board[5, 6] == counter && board[4, 7] == counter) ||
                (board[7, 5] == counter && board[6, 6] == counter) ||
                (board[6, 6] == counter && board[5, 7] == counter) ||
                (board[6, 1] == counter && board[5, 2] == counter) ||
                (board[5, 2] == counter && board[4, 3] == counter) ||
                (board[4, 3] == counter && board[3, 4] == counter) ||
                (board[3, 4] == counter && board[2, 6] == counter) ||
                (board[2, 6] == counter && board[1, 7] == counter) ||
                (board[5, 1] == counter && board[4, 2] == counter) ||
                (board[4, 2] == counter && board[3, 3] == counter) ||
                (board[3, 3] == counter && board[2, 4] == counter) ||
                (board[2, 4] == counter && board[1, 3] == counter) ||
                (board[5, 7] == counter && board[4, 6] == counter) ||
                (board[4, 6] == counter && board[3, 5] == counter) ||
                (board[3, 7] == counter && board[2, 6] == counter) ||
                (board[2, 6] == counter && board[1, 5] == counter))
                return true;
            // Two at position (x,y), position (x,y), position(x,y)
            else
                One(board, counter);
                return false;
        }

        // DETERMINE IF Player_TPL HAS ONE
        public static bool One(GameBoard_TPL<counters> board, counters counter)
        {
            if (
                //HORIZONTAL
                (board[1, 1] == counter) ||
                (board[1, 2] == counter) ||
                (board[1, 3] == counter) ||
                (board[1, 4] == counter) ||
                (board[1, 5] == counter) ||
                (board[1, 6] == counter) ||
                (board[1, 7] == counter) ||
                (board[2, 1] == counter) ||
                (board[2, 2] == counter) ||
                (board[2, 3] == counter) ||
                (board[2, 4] == counter) ||
                (board[2, 5] == counter) ||
                (board[2, 6] == counter) ||
                (board[2, 7] == counter) ||
                (board[3, 1] == counter) ||
                (board[3, 2] == counter) ||
                (board[3, 3] == counter) ||
                (board[3, 4] == counter) ||
                (board[3, 5] == counter) ||
                (board[3, 6] == counter) ||
                (board[3, 7] == counter) ||
                (board[4, 1] == counter) ||
                (board[4, 2] == counter) ||
                (board[4, 3] == counter) ||
                (board[4, 4] == counter) ||
                (board[4, 5] == counter) ||
                (board[4, 6] == counter) ||
                (board[4, 7] == counter) ||
                (board[5, 1] == counter) ||
                (board[5, 2] == counter) ||
                (board[5, 3] == counter) ||
                (board[5, 4] == counter) ||
                (board[5, 5] == counter) ||
                (board[5, 6] == counter) ||
                (board[5, 7] == counter) ||
                (board[6, 1] == counter) ||
                (board[6, 2] == counter) ||
                (board[6, 3] == counter) ||
                (board[6, 4] == counter) ||
                (board[6, 5] == counter) ||
                (board[6, 6] == counter) ||
                (board[6, 7] == counter) ||
                (board[7, 1] == counter) ||
                (board[7, 2] == counter) ||
                (board[7, 3] == counter) ||
                (board[7, 4] == counter) ||
                (board[7, 5] == counter) ||
                (board[7, 6] == counter) ||
                (board[7, 7] == counter))
                return true;
            // One at position (x,y), position (x,y), position(x,y)
            else
                return false;
        }


    }
}