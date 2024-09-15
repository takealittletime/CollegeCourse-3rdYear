import random

num1 = random.randint(1,5)
num2 = random.randint(1,5)

print("생성된 두 수 :",num1,",",num2)

if (num1 == num2):
    print("두 수는 같습니다.")

elif (num1>num2):
    print("더 큰 수는",num1,"입니다.")

else:
    print("더 큰 수는",num2,"입니다.")