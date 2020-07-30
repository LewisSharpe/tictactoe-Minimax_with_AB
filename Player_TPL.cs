using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax_TPL
{
    /* 
 ----------------------------------------------------------------------------------------------------------------
  * Player_TPL.CS -
 --------------------------------------------------------------------------------------------------------------------------
 Class controls all generic abstract behaviour from all Player_TPL instances. Class is inherited by AIPlayer_TPL and HumanPlayer_TPL.
 --------------------------------------------------------------------------------------------------------------------------
 */
    abstract class Player_TPL
    {
        // PUBLIC DECLARATIONS
        public string name;
        public counters counter;
        public counters otherCounter;
        protected counters _counter;

        /* 
----------------------------------------------------------------------------------------------------------------
* Player_TPL constructor -
--------------------------------------------------------------------------------------------------------------------------
Assigns player counter symbol and flips turn based symbols accordingly.
--------------------------------------------------------------------------------------------------------------------------
*/
        public Player_TPL(counters _counter)
        {
            counter = _counter;
            if (counter == counters.O) 
                otherCounter = counters.X;
            else if (counter == counters.X)
                otherCounter = counters.O;
        }
        /* 
         ----------------------------------------------------------------------------------------------------------------
          * GetMove -
         --------------------------------------------------------------------------------------------------------------------------
         Takes user for x,y coords from Minimax for next move and then return a tuple containing the x,y coordinate position and then
         places current counter in that coordinate position.
         --------------------------------------------------------------------------------------------------------------------------
         */
        public abstract Tuple<int, int> GetMove(GameBoard_TPL<counters> board, counters counter, GameBoard_TPL<int> scoreBoard);
        /* 
        ----------------------------------------------------------------------------------------------------------------
         * Win -
        --------------------------------------------------------------------------------------------------------------------------
        The boolean method returns true or false wherever there is a three in a row (winning position) existing on the entire board.
        --------------------------------------------------------------------------------------------------------------------------
        */
        // DETERMINE IF Player_TPL HAS WON
        public bool Win(GameBoard_TPL<counters> board, counters counter)
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
        /* 
         ----------------------------------------------------------------------------------------------------------------
          * Two -
         --------------------------------------------------------------------------------------------------------------------------
         The boolean method returns true or false wherever there is a two in a row (score, 100) existing on the entire board. 
         --------------------------------------------------------------------------------------------------------------------------
         */
        public bool Two(GameBoard_TPL<counters> board, counters counter)
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
            // Two at position (x,y), position (x,y).
                return false;
        }
    }
}