money = int(input("투입한 돈: "))
price = int(input("물건값: "))

change = money-price
print("거스름돈: ", change)
coin500s = change // 500		# 500으로 나누어서 몫이 500원짜리의 개수
change = change % 500			# 500으로 나눈 나머지를 계산한다. 
coin100s = change // 100		# 100으로 나누어서 몫이 100원짜리의 개수

print("500원 동전의 개수:", coin500s)
print("100원 동전의 개수:", coin100s)
input()
