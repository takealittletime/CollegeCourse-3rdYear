import turtle
import math
import random

player = turtle.Turtle()
player.shape("turtle")
screen = player.getscreen()
screen.bgcolor("black")		# 화면 배경을 검정색으로 한다. 
screen.setup(800, 600)			# 화면의 크기를 800×600으로 한다.
player.color("yellow")			# 색상은 파랑색으로 하자. 

player.goto(-300, 0)
velocity = 70              # 초기속도 70픽셀/sec
player.left(45) 

def turnleft():
    player.left(5)				# 왼쪽으로 5도 회전
def turnright():
    player.right(5)				# 오른쪽으로 5도 회전
def turnup():
    global velocity
    velocity += 10
def turndown():
    global velocity
    velocity -= 10

def fire():
        x = -300
        y = 0
        player.color(random.random(),random.random(),random.random()) 
        player.goto(x, y)
        angle = player.heading()        # 초기각도 
        vx = velocity * math.cos(angle * 3.14 / 180.0)  # 도 -> 라디안
        vy = velocity * math.sin(angle * 3.14 / 180.0)  # 도 -> 라디안
        while player.ycor() >= 0 :			# y좌표가 음수가 될때까지
                vx = vx
                vy = vy - 10
                x = x + vx
                y = y + vy
                player.goto(x, y)
                player.stamp()
	
screen.onkeypress(turnleft, "Left")
screen.onkeypress(turnright, "Right")
screen.onkeypress(turnup, "Up")
screen.onkeypress(turndown, "Down")
screen.onkeypress(fire, "space")
screen.listen()
turtle.mainloop()
