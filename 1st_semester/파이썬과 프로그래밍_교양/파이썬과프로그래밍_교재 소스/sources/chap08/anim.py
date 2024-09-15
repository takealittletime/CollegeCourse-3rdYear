import turtle
import random 
t = turtle.Turtle()

t.speed(0)
t.pensize(5)
t.goto(0,0)
while True:
	for i in range(30):
		t.circle(1+5*i)
		t.color((random.random(),random.random(),random.random()))
		t.goto(i*20, 0)
	t.clear()

turtle.done()
