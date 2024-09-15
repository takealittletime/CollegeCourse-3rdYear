import turtle
from random import randint

# (x, y) 위치에 반지름 radius로 원을 그리는 함수 
def draw_circle(turtle, color, x, y, radius):
    turtle.penup()				# 펜을 올린다. 
    turtle.fillcolor(color)                      # 채우기 색상을 설정한다. 
    turtle.goto(x,y)                             # 거북이를 (x, y) 위치로 이동한다. 
    turtle.pendown()                             # 펜을 내린다. 
    turtle.begin_fill()                          # 채우기를 시작한다. 
    turtle.circle(radius)                        # 반지름 radius로 원을 그린다. 
    turtle.end_fill()		                  # 채우기를 종료한다. 
    
# (x, y) 위치에 width와 height 크기의 사각형을 그리는 함수 
def draw_rectangle(turtle, color, x, y, width, height):
    turtle.penup()				# 펜을 올린다. 
    turtle.fillcolor(color)                      # 채우기 색상을 설정한다. 
    turtle.goto(x,y)                             # 거북이를 (x, y) 위치로 이동한다. 
    turtle.pendown()                             # 펜을 내린다. 
    turtle.begin_fill()                          # 채우기를 시작한다. 
    for i in range (2):                           # 2번 반복한다. 
        turtle.forward(width)                    # width 만큼 앞으로 이동한다. 
        turtle.left(90)			# 90도 왼쪽으로 회전한다. 
        turtle.forward(height)		         # height만큼 앞으로 이동한다. 
        turtle.left(90)			# 90도 왼쪽으로 회전한다. 
    turtle.end_fill()				# 채우기를 종료한다. 

# (x, y) 위치에 width와 height 크기의 마름모꼴을 그리는 함수 
def draw_trepezoid(turtle, color, x, y, width, height):
    turtle.penup()
    turtle.fillcolor(color)
    turtle.goto(x,y)
    turtle.pendown()
    turtle.begin_fill()
    turtle.forward(width)
    turtle.right(60)
    turtle.forward(height)
    turtle.right(120)
    turtle.forward(width+20)
    turtle.right(120)
    turtle.forward(height)
    turtle.right(60)
    turtle.end_fill()
    
# (x, y) 위치에 별 모양을 그리는 함수 
def draw_star(turtle, color, x, y, size):
    turtle.penup()
    turtle.fillcolor(color)
    turtle.goto(x,y)
    turtle.pendown()
    turtle.begin_fill()
    for i in range(10):
        turtle.forward(size)
        turtle.right(144)
    turtle.end_fill()

t = turtle.Turtle()		# 거북이를 생성한다. 
t.shape("turtle")		# 커서의 형태를 거북이로 변경한다. 
t.speed(0)			# 거북이의 속도를 최대로 한다. 
	
x = 0				# 현재 그림이 그려지는 위치			
y = 0				# 현재 그림이 그려지는 위치
width = 240			# 마름모꼴의 최초 크기

# 트리의 줄기를 그린다. 
draw_rectangle(t, "brown", x-20, y-50, 30, 50)

# 트리의 잎을 그린다.
height = 20
for i in range(10):
  width = width - 20		# 마름모꼴의 폭이 줄어든다. 
  x = 0 - width/2		# x좌표는 마름모꼴의 중앙으로 한다. 
  draw_trepezoid(t, "green", x, y, width, height)	# 마름모꼴을 그린다. 
  # 랜덤한 위치에 원을 그린다. 
  draw_circle(t, "red", x+randint(0, width), y+randint(0, height), 10)
  y = y + height		# y값을 마름모꼴의 높이만큼 증가한다. 

# 별모양의 트리의 꼭대기에 그린다.
draw_star(t, "yellow", 4, y, 100)
t.penup()
t.color("red")
t.goto(-200, 250)
t.write("Merry Christmas", font=("Arial",24, "italic"))
t.goto(-200, 220)
t.write("Happy New Year!", font=("Arial",24, "italic"))
