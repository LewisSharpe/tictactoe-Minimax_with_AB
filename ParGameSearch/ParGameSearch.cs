using System;
using System.Threading.Tasks;
// using Parallel;
using System.Collections.Generic;    // for List<T>
// using System.Collections.Concurrent; // for Partitioner

class ParallelGameSearch {

  private const int maxLevel = 3;
  private const int maxVal = 9;
  private const int bonzo = 0;

  // Board is an array of int? values such as
  //  [ 2, 1, ?, 8 ]  where ? is a null pointer
  // the static eval fct just takes the sum over all non-null values ie
  //  11
  // for the above example

  // Trying all moves means, going over the board and trying all values from 0 to maxVal
  
  // try all possible moves
  public static int SeqSearch(int?[] board, int bestRes, int level) {
    int res;
    if (level>maxLevel) {
      return StaticEval(board);
    }
    for (int i = 0; i<board.Length; i++) {
      if (board[i] == null) {
	// try values for position i
	for (int val = 0 ; val < maxVal; val++) {
	  board[i] = val;  // make move
	  res = SeqSearch(board, bestRes, level+1);
	  board[i] = null; // undo move
	  bestRes = (res>bestRes) ? res : bestRes;
	}
      }
    }
    return bestRes;
  }

  // top-level fct that generates parallelism;
  // currently fixed to 4 parallel tasks
  // each task steps in strides of 4 over all possible moves
  // NOTE: each tasks needs a clone of the board; but in the recursive calls no cloning is needed
  public static int ParSearchWrapper(int?[] board, int numTasks) {
    int res; // , res1, res2, res3, res4;
    int[] ress = new int[4];
    // int?[] board1 = new int?[board.Length];
    int?[] board1 = (int?[])board.Clone();
    int?[] board2 = (int?[])board.Clone();
    int?[] board3 = (int?[])board.Clone();
    int?[] board4 = (int?[])board.Clone();

    // start and synchronise 4 parallel tasks
    Parallel.Invoke(() => { ress[0] = ParSearchWorker(board1, 4, 0, 0, 0); },
		    () => { ress[1] = ParSearchWorker(board2, 4, 1, 0, 0); },
		    () => { ress[2] = ParSearchWorker(board3, 4, 2, 0, 0); },
		    () => { ress[3] = ParSearchWorker(board4, 4, 3, 0, 0); });
    // compute the maximum over all results
    res = 0;
    for (int j = 0; j<ress.Length; j++) {
      res = (ress[j]>res) ? ress[j] : res;
    }
    // return overall maximum
    return res;
  }

  // Like SeqSearch, iterates over all possible moves, but tries only every @stride candidate
  public static int ParSearchWorker(int?[] board, int stride, int id, int bestRes, int level) {
    int res, cnt = stride, offset = id;
    if (level>maxLevel) {
      return StaticEval(board);
    }
    for (int i = 0; i<board.Length; i++) {
      if (board[i] == null) {
	// try values for position i
	for (int val = 0 ; val < maxVal; val++) {
	  if (offset == 0 && cnt == 0) {
	    board[i] = val;  // make move
	    res = SeqSearch(board, bestRes, level+1);
	    board[i] = null; // undo move
	    bestRes = (res>bestRes) ? res : bestRes;
	    cnt = stride;
	  } else {
	    if (offset == 0) { cnt--; } else { offset--; }
	  }
	}
      }
    }
    return bestRes;
  }

  // could tune this to make it more interesting
  public static int StaticEval(int?[] board) {
    int val = 0;
    for (int i = 0; i<board.Length; i++) {
      val += (board[i]==null) ? 0 : (int)board[i];
    }
    return val;
  }

  public static void Main(string[] args) {
     if (args.Length != 2) { // expect 1 arg: value to double
       System.Console.WriteLine("Usage: <prg> <len> <maxVal> ");
       System.Console.WriteLine("len ... list length");
       System.Console.WriteLine("maxVal ... maximal value per slot");
       // System.Console.WriteLine("t ... threshold for generating parallelism");
     } else {    
       // int k = Convert.ToInt32(args[0]);
       int n = Convert.ToInt32(args[0]);
       int v = Convert.ToInt32(args[1]);
       int res;
       int?[] board = new int?[n];

       {
	 DateTime startTime = DateTime.Now;
	 res = SeqSearch(board, 0, 0);
	 DateTime stopTime = DateTime.Now;
	 TimeSpan duration = stopTime - startTime;

	 System.Console.WriteLine("SeqSearch for a board of length {0} and max-val {1} = {2}\n", n, maxVal, res);
	 Console.WriteLine("Elapsed time: {0}", duration.ToString());
       }

       {
	 DateTime startTime = DateTime.Now;
	 res = ParSearchWrapper(board, 4);
	 DateTime stopTime = DateTime.Now;
	 TimeSpan duration = stopTime - startTime;

	 System.Console.WriteLine("ParSearch for a board of length {0} and max-val {1} = {2}\n", n, maxVal, res);
	 Console.WriteLine("Elapsed time: {0}", duration.ToString());
       }
}
  }
}
  