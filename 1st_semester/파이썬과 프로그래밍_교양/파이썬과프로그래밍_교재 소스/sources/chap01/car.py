import turtle
t = turtle.Turtle()
t.shape("turtle")
t.forward(300) # 자동차 몸체를 그린다.
t.left(90)
t.forward(100)
t.left(90)
t.forward(300)
t.left(90)
t.forward(100)
# 타이어를 그린다.
t.up() # 펜을 들어서 거북이가 움직이더라도 그림이 그려지지 않게 한다.
t.goto(0, 0) # 좌표 (0, 0)으로 간다
t.down() # 펜을 내린다.
t.circle(50) # 원을 그린다.
t.up()
t.goto(200, 0)
t.down()
t.circle(50)
turtle.done()
