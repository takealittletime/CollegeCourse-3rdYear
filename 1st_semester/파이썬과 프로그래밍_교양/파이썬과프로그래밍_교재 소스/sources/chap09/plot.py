import matplotlib.pyplot as plt
import random

numbers = []
for i in range(10):			# 난수로 리스트를 채운다. 
	numbers.append(random.randint(1, 10))

plt.plot(numbers)			# 리스트를 선그래프로 그린다. 
plt.ylabel('some random numbers')	# 레이블을 붙인다. 
plt.show()
