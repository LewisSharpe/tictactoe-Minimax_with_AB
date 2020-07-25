set key autotitle columnhead
set title 'Memory consumption for first move on Board 6'
set xlabel 'cores'
set ylabel 'memory consumption (MB)'
set term png
# type set to 'png''
# specify the range of values on all axes
set xrange [1:64]
set yrange [1:3700]
set output 'memconsump_fmv-board6-190720.png'
plot 'mem_190720.csv' title "memory consumption (MB)" with linesp, \
     'garbage_190720.csv' title "garbage collection (MB)" with linesp, \

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

