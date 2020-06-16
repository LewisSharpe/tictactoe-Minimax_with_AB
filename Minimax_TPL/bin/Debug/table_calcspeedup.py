# -------------------------------------------------------------------------------------------------$
# Python script that calculates speedups for run-time for user stated parameters
# Lewis Sharpe
# Date: 17/05/20
# Run with command: python table_calcspeedup.py
# Output appends to 'speedups.txt'
# -------------------------------------------------------------------------------------------------$
with open('log', 'r+') as f:
   g = raw_input("Enter first core number: ") 
   h = raw_input("Enter second core number: ")  
   for line in f:
        array = []
        array1 = []
        found = line.find(g)
        found1 = line.find(h)
        if found  != -1:
            version = line[found+len('ccccccccccccccc')+1:]
            array.append(version)
            first = array[0]
        if found1  != -1:
            version1 = line[found1+len('ccccccccccccccc')+1:]
            array1.append(version1)
            first1 = array1[0]
            result = (float(first) / float(first1));
            print 'Speed up achieved for ', g, 'from ', h, ' : ', result
with open("speedups.txt", "a") as myfile:
            myfile.write(str(h) + 'from ' + str(g) + ' : ' + str(result) + '\n')

