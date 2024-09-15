import matplotlib.pyplot as plt

x, y, z = [], [], []

for i in range(100):	# 선형 함수
	x.append(i/50.0)

for i in x:		# 2차 함수
	y.append(i**2)

for i in x:		# 3차 함수
	z.append(i**3)

plt.plot(x, x, label="linear")		
plt.plot(x, y, label="quadratic")	
plt.plot(x, z, label="cubic")	

plt.xlabel("input")
plt.ylabel("output")
plt.legend(loc="upper left")	# 레전드
plt.title("Function")		# 그래프의 제목
plt.show()
