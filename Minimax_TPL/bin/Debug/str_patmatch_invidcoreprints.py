# -------------------------------------------------------------------------------------------------$
# Python script that searches for all analytical print statements for each core iteration that is marked with the token '**CORES'
# Lewis Sharpe
# Date: 11/05/20
# -------------------------------------------------------------------------------------------------$
with open('LOG3', 'r') as f:
  with open("output.txt", "w") as f1:
    for line in f:
        if '**CORES' in line:
            f1.write(line)
# -------------------------------------------------------------------------------------------------$



