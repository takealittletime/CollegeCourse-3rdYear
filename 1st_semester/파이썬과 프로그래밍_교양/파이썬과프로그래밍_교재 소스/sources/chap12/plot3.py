import matplotlib.pyplot as plt
X = [ "Mon", "Tue", "Wed", "Thur", "Fri",  "Sat", "Sun" ] 
Y1 = [15.6, 14.2, 16.3, 18.2, 17.1, 20.2, 22.4]
Y2 = [20.1, 23.1, 23.8, 25.9, 23.4, 25.1, 26.3]

plt.plot(X, Y1, label="Seoul")		# 분리시켜서 그려도 됨
plt.plot(X, Y2, label="Busan")		# 분리시켜서 그려도 됨
plt.xlabel("day")
plt.ylabel("temperature")
plt.legend(loc="upper left")		# 레전드
plt.title("Temperatures of Cities")	# 그래프의 제목
plt.show()
