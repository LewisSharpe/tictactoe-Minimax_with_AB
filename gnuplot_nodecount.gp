set terminal pngcairo font "arial,10" size 500,500

set title 'Node count for first move on Board 6'
set xlabel 'thread number'
set ylabel 'positions visited'
set term png
set xrange [0:18]
set yrange [1:3000]
set output 'node-count-board6_220720.png'
set boxwidth 0.75
set style fill solid
plot "nodecount-280620-board6-md.csv" using 2:xtic(1) with boxes


