set terminal pngcairo font "arial,12" size 500,500

set title 'Thread distribution on 48 cores for first move on Board 6'
set xlabel 'time (seconds)'
set ylabel 'thread number'
set term png
set xrange [1:50]
set yrange [0:49]
set output '48cores-thrdis-board6_260620.png'
set boxwidth 0.75
set style fill solid
set style fill solid
plot '48cores-thrdis_board6.csv' using ($2*0.5):0:($2*0.5):(0.4):yticlabels(1) with boxxyerrorbars t ''

