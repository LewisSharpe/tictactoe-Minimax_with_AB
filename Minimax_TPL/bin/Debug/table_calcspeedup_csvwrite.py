# -------------------------------------------------------------------------------------------------$
# Python script that calculates speedups for run-time for user stated parameters and prints to CSV
# Lewis Sharpe
# Date: 17/05/20
# Run with command: python table_calcspeedup.py
# Output appends to 'speedups.txt'
# -------------------------------------------------------------------------------------------------$
with open('seqtimes-board1_130620.txt', 'r+') as f:
   e = raw_input("Enter output file name: ")
   g = raw_input("Enter first core number: ")
   h = raw_input("Enter second core number: ")
   for line in f:
        array = []
        array1 = []
        found = line.find(g)
        found1 = line.find(h)
        if found  != -1:
            version = line[found+len('ccccccccccccccc'):]
            array.append(version)
            first = array[0]
        if found1  != -1:
            version1 = line[found1+len('ccccccccccccccc'):]
            array1.append(version1)
            first1 = array1[0]
	    print first, first1
            result = int(float(first)) / int(float(first1));
            print 'Speed up achieved for ', str(g), 'from ', str(h), ' : ', str(result)
with open("speedups.txt", "a") as myfile:
            myfile.write(str(h) + 'from ' + str(g) + ' : ' + str(result) + '\n')
import csv
filename = e
with open(filename, mode='a+') as csv_file:
    csv_writer = csv.writer(csv_file, delimiter=',', quotechar='"', quoting=csv.QUOTE_MINIMAL)
    if 'core number' in csv_file.read():   
	    csv_writer.writerow([str(h), result])
    # do nothing
    else:
            csv_writer.writerow(['core number', 'speed up achieved'])
            csv_writer.writerow([str(h), result])
