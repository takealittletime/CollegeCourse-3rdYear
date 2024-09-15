import turtle
import random
t = turtle.Turtle()
t.shape("turtle")
t.speed(0)					# 거북이의 속도를 빠르게 한다. 

for i in range(30):
    t.fillcolor(random.random(), random.random(), random.random())# 랜덤 색상 지정

    t.up()					# 펜을 든다.
    t.goto(random.randint(-100, 100), random.randint(-100, 100)) # 랜덤 위치 지정
    t.down()					# 펜을 내린다. 
    
    length = random.randint(10, 100)
    height = random.randint(10, 100)
    t.begin_fill()				# 채워서 그리기 시작
    for _ in range(2):				# 2번 반복한다. _은 이름 없는 변수이다. 
        t.forward(length)			# 거북이 length 만큼 전진
        t.right(90)				# 90도 회전
        t.forward(height)			# 거북이 height 만큼 전진
        t.right(90)				# 90도 회전 
    t.end_fill()				# 채워서 그리기 완료

turtle.done()
