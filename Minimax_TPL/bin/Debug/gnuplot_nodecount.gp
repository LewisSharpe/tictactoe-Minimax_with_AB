set terminal pngcairo font "arial,10" size 500,500

set title 'Node count for first move on Board 10'
set xlabel 'thread number'
set ylabel 'positions visited'
set term png
set xrange [0:8]
set yrange [1:50]
set output 'node-count-board10_150620.png'
set boxwidth 0.75
set style fill solid
plot "nodecount-fmv-board10_150620.csv" using 2:xtic(1) with boxes


