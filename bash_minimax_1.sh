# ---------------------------------------------------------------------------------------------
# Script automates run of algorithm for every iteration between seq, 1 to 48 cores and prints times$
# Lewis Sharpe
# Date: 04/05/2020
# Run script with command: 'bash ./Minimax_TPL.exe 1>LOG &'
# ---------------------------------------------------------------------------------------------

SECONDS1=0;
mono Minimax_TPL.exe 1 1 >> LOG1;
echo -n "###Time taken for core number $i:" $SECONDS1 "seconds"
SECONDS2=0;
mono Minimax_TPL.exe 1 2 >> LOG2;
echo -n "###Time taken for core number $i:" $SECONDS2 "seconds"
SECONDS4=0;
mono Minimax_TPL.exe 1 4 >> LOG4;
echo -n "###Time taken for core number $i:" $SECONDS4 "seconds"
SECONDS8=0;
mono Minimax_TPL.exe 1 8 >> LOG8;
echo -n "###Time taken for core number $i:" $SECONDS8 "seconds"
SECONDS16=0;
mono Minimax_TPL.exe 1 16 >> LOG16;
echo -n "###Time taken for core number $i:" $SECONDS16 "seconds"
SECONDS32=0;
mono Minimax_TPL.exe 1 32 >> LOG32;
echo -n "###Time taken for core number $i:" $SECONDS32 "seconds"
SECONDS48=0;
mono Minimax_TPL.exe 1 48 >> LOG48;
echo -n "###Time taken for core number $i:" $SECONDS48 "seconds"
SECONDS64=0;
mono Minimax_TPL.exe 1 64 >> LOG64;
echo -n "###Time taken for core number $i:" $SECONDS64 "seconds"
exit 1
done

# ---------------------------------------------------------------------------------------------


