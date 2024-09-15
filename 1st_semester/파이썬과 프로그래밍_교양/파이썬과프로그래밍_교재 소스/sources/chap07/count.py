import random

def getLotto():
    numbers = []
    while True:
        n = random.randint(1,45)
        if n not in numbers:
            numbers.append(n)
        if len(numbers) >=6:
            return sorted(numbers)

print(f"생성된 로또번호: {getLotto()}")