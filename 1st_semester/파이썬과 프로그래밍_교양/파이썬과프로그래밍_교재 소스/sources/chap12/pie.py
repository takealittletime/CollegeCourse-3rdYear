import matplotlib.pyplot as plt

Y = [40, 20, 10, 30]
labels = ["Eating Out", "Shopping", "Groceries", "Housing"]
explode = [0.1, 0, 0, 0]	# 하나의 파이를 분리하여 표시한다.

plt.pie(Y, labels = labels, explode = explode)	# 파이 챠트
plt.show() 
