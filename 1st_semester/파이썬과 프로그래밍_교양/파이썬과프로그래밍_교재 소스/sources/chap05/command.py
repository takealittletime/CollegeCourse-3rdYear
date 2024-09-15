import os
command = input("명령을 입력하시오: ")

if command == "shutdown":
	print("컴퓨터가 곧 종료됩니다. 엔터키를 누르세요") 
	input()
	os.system("shutdown /s /t 1")
elif command == "cwd":
	cwd = os.getcwd()
	print("Current working directory:", cwd) 
else:
	print("알수 없는 명령어입니다.")
input()