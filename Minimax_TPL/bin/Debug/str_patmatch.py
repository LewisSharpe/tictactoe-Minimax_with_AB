# -------------------------------------------------------------------------------------------------$
# Python script that searches for time taken line for each core number in log file and print them a$
# Lewis Sharpe
# Date: 04/05/20
# -------------------------------------------------------------------------------------------------$
with open('LOG_020620_BOARD3', 'r') as f:
  with open("output.txt", "w") as f1:
    for line in f:
        if 'LS Elapsed game time' in line:
                f1.write(line)
# -------------------------------------------------------------------------------------------------$



