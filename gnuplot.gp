set key autotitle columnhead
set title 'Absolute Speed-up for first move on Board 6'
set xlabel 'cores'
set ylabel 'speed up'
set term png
# type set to 'png''
# specify the range of values on all axes
set xrange [1:64]
set yrange [1:65]
set output 'absolute_firstmv-board6-adapted_220720.png'
plot 'absolute-board6-fmv_260620.csv' title "board 6 absolute" with linesp, \
     'relative-board6-fmv_260620.csv' title "board 6 relative" with linesp, \
     x  title "Linear" with lines

# NOTE:
# if you have a sequential runtime of say 109.96, and a data file rt.dat
# which contains *runtimes* in the format of
#  <No.of Proc> <Runtime>
# per line int he file, then you can automatically calculate the speedup like this
# set sequential time here:
# seq = 109.96
# 
# # plot the speedup, reading runtimes from rt.dat
# plot   \
#  "rt.dat" using 1:(seq/$2) title "Naive parallel (matrix04.c)"  with linesp 1, \
#  x  title "Linear" with lines 2
 
