import random
import turtle


def draw_road():
    t.width(10)
    t.up();
    t.goto(-100, -300)
    t.down();
    t.goto(-100, +300)
    t.penup();
    t.goto(+100, -300)
    t.down();
    t.goto(+100, +300)


def turn_left():
    t.left(10)


def turn_right():
    t.right(10)


def drive():
    t.forward(20)

t = turtle.Turtle()
screen = turtle.Screen()
t.shape("turtle")

draw_road()
screen.onkey(turn_left, "Left")
screen.onkey(turn_right, "Right")
screen.onkey(drive, "Up")

t.penup();
t.goto(0, -300)
t.pendown();
t.color("blue")
screen.listen()
screen.mainloop()
