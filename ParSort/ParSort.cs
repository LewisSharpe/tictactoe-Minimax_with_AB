/* 
From: 
Parallel Programming with Microsoft� .NET
Design Patterns for Decomposition and Coordination on Multicore Architectures
By
Colin Campbell, Ralph Johnson, Ade Miller, Stephen Toub
Publisher: Microsoft Press
Released:  August 2010 
On-line: http://msdn.microsoft.com/en-us/library/ff963547.aspx

Chapter on Dynamic Parallelism

This is an example of divide-and-conquer parallelism.

Possible extensions:
. implement a more efficient sorting alg for the base case
. use generics to abstract over the data-type
. use delegates to abstract over the comparison function

Parallel Quicksort

Compile: ~/OPT/x86_64-unknown-linux/bin/dmcs  -optimize+ -out:ParSort.exe ParSort.cs
Execute: time ~/OPT/x86_64-unknown-linux/bin/mono ParSort.exe 2 2000000 8

*/

using System;
using System.Threading.Tasks;
// using Parallel;
using System.Collections.Generic;    // for List<T>
// using System.Collections.Concurrent; // for Partitioner

class ParSort
{
   private static void Swap(int[] array, int i, int j)
    {
        // requires: 0 <= i,j, <= array.Length-1
        /*
        if (i<0 || i>array.Length-1 || j<0 || j>array.Length-1) {
          throw new System.Exception("Swap: indices out of bounds");
        }
        */
        if (i == j)
        {
            return;
        }
        else
        {
            int tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }
    }

    // useful for checking
    private static bool IsSorted(int[] array)
    {
        return IsSorted(array, 0, array.Length - 2);
    }
    private static bool IsSorted(int[] array, int from, int to)
    {
        for (int i = from; i < to; i++)
        {
            if (array[i] > array[i + 1])
            { // error case
                throw new System.Exception(String.Format("Array not sorted at {0} with {1} > {2}", i, array[i], array[i + 1]));
                //return false;
            }
        }
        return true;
    }
    private delegate bool TestDelegate(int x);
    private static bool Forall(TestDelegate is_ok, int[] array, int from, int to)
    {
        bool ok = true;
        for (int i = from; i <= to; i++)
        {
            if (!is_ok(array[i]))
            {
                // throw new System.Exception(String.Format("Fail at {0} ", i));
                return false;
            }
        }
        return ok;
    }
    private static bool IsPartition(int[] array, int from, int to, int pivot)
    {
        return
          Forall((x) => x <= array[pivot], array, from, pivot) &&
          Forall((x) => x > array[pivot], array, pivot + 1, to);
    }

    // ToDo: use  a more efficient sort as base case
    // This implements a naive bubble-sort
    private static void BubbleSort(int[] array, int from, int to)
    {
        // requires: 0 <= from <= to <= array.Length-1
        bool swapped = false;

        if (from < 0 || to > array.Length - 1)
        {
            throw new System.Exception("BubbleSort: out of bounds");
        }
        if (from >= to)
        {
            return;
        }
        do
        {
            swapped = false;
            for (int i = from; i < to; i++)
            {
                if (array[i] > array[i + 1])
                {
                    Swap(array, i, i + 1);
                    swapped = true;
                }
            }
        } while (swapped);
        // provides: IsSorted(array, from, to)
    }

    private static void BubbleSort(int[] array)
    {
        BubbleSort(array, 0, array.Length - 1);
    }

    private static int Partition(int[] array, int from, int to, int pivot)
    {
        // requires: 0 <= from <= pivot <= to <= array.Length-1
        // int old_from = from, old_to = to;
        int last_pivot = -1;
        int pivot_val = array[pivot];
        if (from < 0 || to > array.Length - 1)
        {
            throw new System.Exception(String.Format("Partition: indices out of bounds: from={0}, to={1}, Length={2}",
                                 from, to, array.Length));
        }
        while (from < to)
        {
            if (array[from] > pivot_val)
            {
                Swap(array, from, to);
                to--;
            }
            else
            {
                if (array[from] == pivot_val)
                {
                    last_pivot = from;
                }
                from++;
            }
        }
        if (last_pivot == -1)
        {
            if (array[from] == pivot_val)
            {
                return from;
            }
            else
            {
                throw new System.Exception(String.Format("Partition: pivot element not found in array"));
            }
        }
        if (array[from] > pivot_val)
        {
            // bring pivot element to end of lower half
            Swap(array, last_pivot, from - 1);
            return from - 1;
        }
        else
        {
            // done, bring pivot element to end of lower half
            Swap(array, last_pivot, from);
            return from;
        }
        // provides: forall from <= i <= from. array[i]<=array[from] && 
        //           forall from+1 <= i <= to. array[i]>array[from] &&
    }

    private static string ShowArray(int[] array, int from, int to)
    {
        string str = "";
        if (from > to) { return str; }
        do
        {
            str += " " + array[from].ToString();
            from++;
        } while (from <= to);
        return str;
    }

    // From: http://msdn.microsoft.com/en-us/library/ff963551.aspx
    static void SequentialQuickSort(int[] array, int from, int to)
    {
        // requires: 0 <= from <= to <= array.Length-1
        if (to - from <= Threshold)
        {
            // InsertionSort(array, from, to);
            BubbleSort(array, from, to);
        }
        else
        {
            int pivot = from + (to - from) / 2;
            pivot = Partition(array, from, to, pivot);
            if (!IsPartition(array, from, to, pivot))
            {
                throw new System.Exception(String.Format("segment from {0} to {1} (pivot {2}) is not a partition", from, to, pivot));
            }
            SequentialQuickSort(array, from, pivot - 1);
            // assert: IsSorted(array, from, pivot)
            SequentialQuickSort(array, pivot + 1, to);
            // assert: IsSorted(array, pivot+1, to)
        }
        // provides: IsSorted(array, from, to)
    }

    public static void SequentialQuickSort(int[] array)
    {
        SequentialQuickSort(array, 0, array.Length - 1);
        // provides: IsSorted(array)
    }

    // parallel divide-and-conquer with explicit thresholding
    static void ParallelQuickSort(int[] array, int from, int to, int depthRemaining)
    {
        // requires: 0 <= from <= to <= array.Length-1
        if (to - from <= Threshold)
        {
            // InsertionSort(array, from, to);
            BubbleSort(array, from, to);
        }
        else
        {
            int pivot = from + (to - from) / 2;
            pivot = Partition(array, from, to, pivot);
            if (!IsPartition(array, from, to, pivot))
            {
                throw new System.Exception(String.Format("segment from {0} to {1} (pivot {2}) is not a partition", from, to, pivot));
            }
            if (depthRemaining > 0)
            {
                Parallel.Invoke(
                  () => ParallelQuickSort(array, from, pivot - 1,
                                 depthRemaining - 1),
                  () => ParallelQuickSort(array, pivot + 1, to,
                                 depthRemaining - 1));
            }
            else
            {
                ParallelQuickSort(array, from, pivot - 1, 0);
                // assert: IsSorted(array, from, pivot)
                ParallelQuickSort(array, pivot + 1, to, 0);
                // assert: IsSorted(array, pivot+1, to)
            }
        }
        // provides: IsSorted(array, from, to)
    }

    // wrapper, explicit parallelism threshold
    public static void ParallelQuickSort(int[] array, int t)
    {
        ParallelQuickSort(array, 0, array.Length - 1, t);
        // provides: IsSorted(array, from, to)
    }

    // wrapper, limiting the amount of parallelism
    public static void ParallelQuickSort(int[] array)
    {
        ParallelQuickSort(array, 0, array.Length - 1,
          (int)Math.Log(Environment.ProcessorCount, 2) + 4);
    }

    private const int Threshold = 1; // threshold switching to a different sort
    private const int Size = 1000000; // size of arrays to sort
    private const int MaxVal = 1000;  // bound on values
    private const int Iterations = 1; // number of tests to run

    public static void Main(string[] args)
    {
        if (args.Length != 3)
        { // expect 1 arg: value to double
            System.Console.WriteLine("Usage: <prg> <v> <n> <t>");
            System.Console.WriteLine("v ... version (0: sequential, 1: parallel, implicit threshold, 2: parallel, explicit threshold)");
            System.Console.WriteLine("n ... list length");
            System.Console.WriteLine("t ... threshold for generating parallelism");
        }
        else
        {
            // int k = Convert.ToInt32(args[0]);
            int v = Convert.ToInt32(args[0]);
            int n = Convert.ToInt32(args[1]);
            int t = Convert.ToInt32(args[2]);

            int seed = 1701 + 13 * n;
            int j = 0;
            for (j = 0; j < Iterations; j++)
            {
                Random rg = new Random((seed + j * 7) % 65535); // fix a seed for deterministic results
                int[] arr = new int[n];
                Console.WriteLine("Generating an array of size {0} ...", n);
                for (int i = 0; i < n; i++)
                {
                    arr[i] = rg.Next() % MaxVal;
                }
                switch (v)
                { // sequential sort
                    case 0:
                        Console.WriteLine("Sequential sorting (size {0}) ...", n);
                        SequentialQuickSort(arr);
                        break;
                    case 1:
                        Console.WriteLine("Parallel sorting (size {0}, implicit threshold)...", n, t);
                        ParallelQuickSort(arr);
                        break;
                    case 2:
                        Console.WriteLine("Parallel sorting (size {0}, threshold {1}))...", n, t);
                        ParallelQuickSort(arr, t);
                        break;
                    default:
                        Console.WriteLine("Unknown version {0}; use 0: sequential, 1: parallel, implicit threshold, 2: parallel, explicit threshold", v);
                        continue;
                }
                /* test whether the result is Sorted
                try {
                  Console.WriteLine("Sorted?: {0}", IsSorted(arr).ToString());
                } catch (Exception e) {
                  Console.WriteLine("Some test failed!!!");
                }
                */
            }
        }
    }
}