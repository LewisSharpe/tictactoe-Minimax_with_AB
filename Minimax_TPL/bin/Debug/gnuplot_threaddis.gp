set terminal pngcairo font "arial,10" size 500,500

set title 'Thread distribution for first move on Board 10'
set xlabel 'time (seconds)'
set ylabel 'thread number'
set term png
set xrange [1:100]
set yrange [0:8]
set output 'thr-dis-board10-adapted_200620.png'
set boxwidth 0.75
set style fill solid
set style fill solid
plot 'thr1_thrtimes_fmv_150620-board10.csv' using ($2*0.5):0:($2*0.5):(0.4):yticlabels(1) with boxxyerrorbars t ''

