import datetime
x = datetime.datetime(2022, 9, 1)
y = datetime.datetime.now()
delta = y - x
print("만난 일: "+str(x))
print("현재: "+str(y))
print("만난 지 "+ str(delta.days)+ "일 되었습니다.")
input()