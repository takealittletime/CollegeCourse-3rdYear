import turtle
import random
import time

score = 0
myList = []			# 공백 리스트를 생성한다. 

screen = turtle.Screen()
screen.tracer(0)		# 화면을 우리가 업데이트하겠다고 알린다.
screen.addshape("dog.gif")

player = turtle.Turtle()	# 주인공 강아지를 생성한다. 
player.shape("dog.gif")
player.up()
player.goto(-200, 200)
player.down()
player.goto(200, 200)		# 경기장을 그린다. 
player.goto(200, -200)
player.goto(-200, -200)
player.goto(-200, 200)
player.up()
player.goto(0, 0)

display = turtle.Turtle()	# 점수를 표시하는 터틀 객체를 생성한다. 
display.hideturtle()
display.penup()
display.goto(-210,200)
display.write(f"점수={score}", font=("Arial",20,"italic"))

for i in range(10):		# 과자 10개를 생성하고 리스트에 저장한다. 
    bread = turtle.Turtle()
    bread.shape("circle")
    bread.penup()
    x = random.randint(-180,180)
    y = random.randint(-180,180)
    bread.goto(x,y)
    myList.append(bread)

def moveRight():
    player.setheading(0)
    player.forward(10)

def moveLeft():
    player.setheading(180)
    player.forward(10)

def moveUp():
    player.setheading(90)
    player.forward(10)

def moveDown():
    player.setheading(270)
    player.forward(10)

screen.listen()
screen.onkeypress(moveRight,"Right")		# 키 이벤트를 처리한다. 
screen.onkeypress(moveLeft,"Left")
screen.onkeypress(moveUp,"Up")
screen.onkeypress(moveDown,"Down")

while True:				# 게임 루프이다. 
    for cookie in myList:		# 리스트에 저장된 과자를 하나씩 처리한다. 
        if player.distance(cookie) < 30:	# 강아지와의 거리가 30 미만이면
            x = random.randint(-180,180)	
            y = random.randint(-180,180)
            cookie.goto(x,y)	
            score = score + 1			# 점수를 하나 증가한다. 
            display.clear()			# 점수를 다시 표시한다. 
            display.write(f"점수={score}", font=("Arial",20,"italic"))
    screen.update()			# 화면을 업데이트한다. 
