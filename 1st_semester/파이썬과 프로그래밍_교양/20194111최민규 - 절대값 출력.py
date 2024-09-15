import random

num = random.randint(-5,5)
print("생성된 수 :",num)
if (num < 0):
    print(num,"의 절대값은",(-1*num),"입니다.")

else:
    print(num, "의 절대값은", num, "입니다.")