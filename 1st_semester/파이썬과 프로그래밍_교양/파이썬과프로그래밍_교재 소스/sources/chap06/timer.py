import time 
import winsound 

seconds = int(input("초단위의 시간을 입력하시오: "))

for i in range(seconds, 0, -1):
	print(f"{i}초 남았습니다.")
	time.sleep(1)

winsound.Beep(2000, 3000)
input()