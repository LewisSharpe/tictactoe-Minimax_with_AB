# -------------------------------------------------------------------------------------------------$
# Python script that coverts time taken data in text file and converts into a table
# Lewis Sharpe
# Date: 13/05/20
# Run with command: python tab_create.py >> log
# -------------------------------------------------------------------------------------------------$

data_filename = 'firstmvtimes-board5_140620.txt'
headers = 'Core Number', 'Time', 'Time type'  # Column names.

# Read the data from file into a list-of-lists table.
with open(data_filename) as file:
   datatable = [line.split() for line in file.read().splitlines()]

# Find the longest data value or header to be printed in each column.
widths = [max(len(value) for value in col)
            for col in zip(*(datatable + [headers]))]

# Print heading followed by the data in datatable.
# (Uses '>' to right-justify the data in some columns.)
format_spec = '{:{widths[0]}}  {:<{widths[1]}}  {:<{widths[2]}}'  
print(format_spec.format(*headers, widths=widths))
for fields in datatable:
    print(format_spec.format(*fields, widths=widths)) 
