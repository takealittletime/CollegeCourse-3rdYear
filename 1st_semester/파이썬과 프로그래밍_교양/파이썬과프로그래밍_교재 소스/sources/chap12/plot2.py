import matplotlib.pyplot as plt

X = [ "Mon", "Tue", "Wed", "Thur", "Fri",  "Sat", "Sun" ] 
Y1 = [15.6, 14.2, 16.3, 18.2, 17.1, 20.2, 22.4]
Y2 = [20.1, 23.1, 23.8, 25.9, 23.4, 25.1, 26.3]

plt.plot(X, Y1, X, Y2)		# plot()에 2개의 리스트를 보낸다. 
plt.xlabel("day")		# x축 레이블	
plt.ylabel("temperature")	# y축 레이블
plt.show()
