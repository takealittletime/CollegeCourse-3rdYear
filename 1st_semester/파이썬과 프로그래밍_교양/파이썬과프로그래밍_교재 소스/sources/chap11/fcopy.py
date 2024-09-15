# 입력 파일 이름과 출력 파일 이름을 받는다. 
infilename = input("입력 파일 이름: ");
outfilename = input("출력 파일 이름: ");

# 입력과 출력을 위한 파일을 연다. 
infile = open(infilename, "r")
outfile = open(outfilename, "w")

# 전체 파일을 읽는다. 
s = infile.read()

# 전체 파일을 쓴다. 
outfile.write(s)

# 파일을 닫는다.
infile.close() 
outfile.close() 
