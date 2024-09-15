from turtle import *
class Car:
    def __init__(self, speed, color, fname):
        self.speed = speed
        self.color = color
        self.turtle = Turtle()
        self.turtle.shape(fname)

    def drive(self, distance):
        self.turtle.forward(distance)

    def turnleft(self, degree):
        self.turtle.left(degree)


register_shape("car1.gif")
myCar = Car(60, "blue", "car1.gif")
for i in range(4):
    myCar.drive(100)
    myCar.turnleft(90)
