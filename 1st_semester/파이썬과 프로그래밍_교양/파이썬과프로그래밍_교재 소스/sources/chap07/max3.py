def getMax(a, b, c=-10000):
    if (a >= b) and (a >= c):
        largest = a
    elif (b >= a) and (b >= c):
        largest = b
    else:
        largest = c
    return largest

print(f"(10, 20, 50)중에서 최대값: {getMax(10, 20, 50)}")
print(f"(10, 20)중에서 최대값: {getMax(10, 20)}")
input()