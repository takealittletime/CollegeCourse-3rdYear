import csv					# CSV 모듈을 불러온다. 

f = open('weather.csv')			# CSV 파일을 열어서 f에 저장한다. 
data = csv.reader(f)				# reader() 함수를 이용하여 읽는다. 
header = next(data)				# 헤더를 제거한다. 
for row in data:				# 반복 루프를 사용하여 데이터를 읽는다.
	print(row)
f.close()
input()