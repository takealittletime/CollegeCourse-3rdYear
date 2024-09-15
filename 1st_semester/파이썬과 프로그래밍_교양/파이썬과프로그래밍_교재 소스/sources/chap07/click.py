import turtle
import random 

t = turtle.Turtle()


def square(length):			
    for i in range(4):
        t.forward(length)
        t.left(90)

def drawit(x, y):
    t.penup()
    t.goto(x, y)
    t.pendown()
    t.begin_fill()
    t.color(random.random(), random.random(), random.random())	# 랜덤 색상 지정
    square(100)
    t.end_fill()
    
s = turtle.Screen()			# 그림이 그려지는 화면을 얻는다. 
s.onscreenclick(drawit)		# 마우스 클릭 이벤트 처리 함수를 등록한다.

turtle.done()
