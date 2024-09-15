infile = open("phones.txt", "r")
line = infile.readline().rstrip()
while line != "":
	print(line)
	line = infile.readline().rstrip()
infile.close() 
input()