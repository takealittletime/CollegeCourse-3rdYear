import random

num1 = random.randint(1,100)
num2 = random.randint(1,100)
num3 = random.randint(1,100)

print("생성된 세 수 :",num1,",",num2,",",num3)

if (num1 == num2 == num3):
    print("두 수는 같습니다.")

elif (num1>=num2 and num1>=num3):
    print("가장 큰 수는",num1,"입니다.")

elif (num2>=num1 and num2>=num3):
    print("가장 큰 수는",num2,"입니다.")

else:
    print("가장 큰 수는",num3,"입니다.")