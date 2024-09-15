import turtle
t = turtle.Turtle()
t.shape("turtle")

s = turtle.textinput("", "도형을 입력하시오: ")
if s == "사각형" :
    w = turtle.numinput("","가로: ")
    h = turtle.numinput("","세로: ")
    t.forward(w)
    t.left(90)
    t.forward(h)
    t.left(90)
    t.forward(w)
    t.left(90)
    t.forward(h)

elif s == "삼각형" :
    pass

else :
    pass

turtle.done()
