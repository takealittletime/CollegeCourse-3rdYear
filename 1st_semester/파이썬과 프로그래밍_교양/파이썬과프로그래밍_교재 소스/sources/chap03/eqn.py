a = float(input("a: "))
b = float(input("b: "))
c = float(input("c: "))

r = b**2 - 4*a*c

print("2개의 실근이 있는 경우만 계산할 수 있습니다.")
x1 = (((-b) + r**0.5)/(2*a))
x2 = (((-b) - r**0.5)/(2*a))
print("2개의 실근:", x1, x2)
input()