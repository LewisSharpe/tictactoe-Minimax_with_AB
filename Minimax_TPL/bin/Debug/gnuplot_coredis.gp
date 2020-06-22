
set terminal pngcairo font "arial,10" size 500,500

set title 'Duration for each core number for first move on Board 1'
set xlabel 'time (seconds)'
set ylabel 'thread number'
set term png
set xrange [1:2000]
set yrange [0:18]
set output 'core-dis-board1-adapted_200620.png'
set boxwidth 0.75
set style fill solid
set style fill solid

plot 'coretimes-firstmove-board1_130620.csv' using ($2*0.5):0:($2*0.5):(0.4):yticlabels(1) with boxxyerrorbars t ''




