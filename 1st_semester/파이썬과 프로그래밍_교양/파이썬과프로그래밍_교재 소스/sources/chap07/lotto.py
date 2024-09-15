import random

def getLotto():
    numbers = []
    while len(numbers) < 6 :
        n = random.randint(1,45)
        if n not in numbers:
            numbers.append(n)
    return numbers;

print(f"생성된 로또번호: {getLotto()}")
input()