import random

while True:
    x = random.randint(1, 100)
    y = random.randint(1, 100)
    print(x, "+", y, "=", end= " ")
    answer = int(input())
    if answer == x + y:
        print("잘했어요!!")
    else:
        print("다음번에는 잘할 수 있죠?")
input()