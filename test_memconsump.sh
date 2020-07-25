
# ---------------------------------------------------------------------------------------------
# Script automates run of algorithm for every iteration between seq, 1 to 48 cores and prints times$
# Lewis Sharpe
# Date: 04/05/2020
# Run script with command: 'bash ./bash_minimax.sh 1>LOG &'
# ---------------------------------------------------------------------------------------------

for i in 20 24 28 32 36 40 44 48 52 56 60 64
do
SECONDS=0;
echo mono Minimax_TPL.exe 6 $i;
mono Minimax_TPL.exe 6 $i;
echo -n "core-number-$i:" $SECONDS "seconds"
done

# ---------------------------------------------------------------------------------------------

