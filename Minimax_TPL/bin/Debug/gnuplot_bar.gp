set terminal pngcairo font "arial,10" size 500,500

set title 'Thread distribution for first move on Board 6'
set xlabel 'thread number'
set ylabel 'time (seconds)'
set term png
set xrange [0:8]
set yrange [1:100]
set output 'thr-dis-board6_150620.png'
set boxwidth 0.75
set style fill solid
plot "thr1_thrtimes_fmv_140620-board6.csv" using 2:xtic(1) with boxes
