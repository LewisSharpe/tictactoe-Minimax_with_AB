using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minimax
{
    class AIPlayer : Player
    {
        public AIPlayer(char _counter) : base(_counter) { }

        public override Tuple<int, int> GetMove(GameBoard board)
        {
            return new Tuple<int, int>(0, 0);
            // MINIMAX
        }


    public int GetNumForDir(char startSq, int dir, GameBoard board, counters us) {
        int found = 0; 
	        while (board[startSq, startSq] != counters.BORDER) { // while start sq not border sq
		        if(board[startSq, startSq] != us) {
		        break;
		        }
		        found++;
		        startSq += dir;
	                }
	                return found;
                    }

    public int FindTwoInARow(GameBoard board, int ourindex, counters us) {
        int DirIndex = 0;
        int Dir = 0;
        int twoCount = 1;
        Debug.Assert(us == counters.NOUGHTS || us == counters.CROSSES);
        for(DirIndex = 0; DirIndex<Consts.NO_OF_DIRS; ++DirIndex) {
                Dir = Consts.DIRECTIONS[DirIndex];
                twoCount += GetNumForDir(ourindex + Dir, Dir, board, us);
                twoCount += GetNumForDir(ourindex + Dir* -1, Dir* -1, board, us);
                if (twoCount == 2) {
                        break;
                }
                twoCount = 1;
        }
        return twoCount;
}

        public int Minimax(GameBoard board, int counter)
        {
            List<Tuple<int, int>> availableMoves = getAvailableMoves(board);
            int bestScore = Consts.MIN_SCORE;
            int score = Consts.MIN_SCORE; // current score of move
            Tuple<int, int> Move;
            int bestMove = -1; // best move with score
            if (Win(board, this.counter))
                return 10;
            else if (Win(board, this.otherCounter))
                return -10;
            else if (availableMoves.Count == 0)
                return 0;
            List<int> moves = new List<int>();
            for (int i = 0; i < availableMoves.Count; i++)
            {
                Move = availableMoves[i]; ; // current move
                // GameBoard board0 = MakeMove(board, move); // copies board - parallel ready
                //board[Move] = counter;              // updates board - sequential only
                score = -Minimax(board, counter ^ 1 /* swap player */);  // RECURSIVE call
                if (score > bestScore)
                {
                    //   bestMove = Move;
                    bestScore = score;
                }
                else
                {
                }
            }
            return bestScore; // also return bestMove          
        }

        // TWO 
        // THREE
        // EVAL

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
}
