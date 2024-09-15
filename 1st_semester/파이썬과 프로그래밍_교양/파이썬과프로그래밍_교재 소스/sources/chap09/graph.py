import matplotlib.pyplot as plt

xlist = []
for i in range(-100, 100):		# -10.0에서 10.0까지의 실수 200개를 만든다. 
	xlist.append(i/10.0)

a = int(input("a : "))
b = int(input("b : "))
c = int(input("c : "))

ylist = []
for i in xlist:
	ylist.append(a*i**2 + b*i + c)	# 2차 함수값을 계산하여서 ylist에 저장한다.

plt.plot(xlist, ylist)
plt.show()
