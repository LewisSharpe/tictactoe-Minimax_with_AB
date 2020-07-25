# -------------------------------------------------------------------------------------------------$
# Python script that searches for time taken line for each core number in log file and print them a$
# Lewis Sharpe
# Date: 04/05/20
# -------------------------------------------------------------------------------------------------$
with open('LOG_100720_board6-memconumpcontd', 'r') as f:
  with open("mem_consump_board6_100720-CONTD.txt", "w") as f1:
    for line in f:
        if 'LS Memory consumption:' in line:
                f1.write(line)
# -------------------------------------------------------------------------------------------------$



