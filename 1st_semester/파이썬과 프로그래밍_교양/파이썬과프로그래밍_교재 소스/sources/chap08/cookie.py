import turtle
import random
import time

score = 0

screen = turtle.Screen()
screen.tracer(0)
screen.addshape("dog.gif")

player = turtle.Turtle()
player.shape("dog.gif")
player.up()
player.goto(-200, 200)
player.down()
player.goto(200, 200)
player.goto(200, -200)
player.goto(-200, -200)
player.goto(-200, 200)
player.up()
player.goto(0, 0)

display = turtle.Turtle()
display.hideturtle()
display.penup()
display.goto(-210,200)
display.write(f"점수={score}", font=("Arial",20,"italic"))

bread = turtle.Turtle()
bread.shape("circle")
bread.penup()
x = random.randint(-180,180)
y = random.randint(-180,180)
bread.goto(x,y)

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
screen.onkeypress(moveRight,"Right")
screen.onkeypress(moveLeft,"Left")
screen.onkeypress(moveUp,"Up")
screen.onkeypress(moveDown,"Down")

while True:
  if player.distance(bread) < 30:
    x = random.randint(-180,180)
    y = random.randint(-180,180)
    bread.goto(x,y)
    score = score + 1
    display.clear()
    display.write(f"점수={score}", font=("Arial",20,"italic"))
  screen.update()
  
  
    
    
