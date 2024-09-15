import turtle
import time

width, height = 600, 300		# 게임 보드의 크기
gravity = 0.05				# 중력
x, y = 0, height/2			# 공의 위치
vx, vy = 0.25, 1			# 공의 속도
coef_res = 0.90			# 반발 계수

screen = turtle.Screen()
screen.setup(width+100, height+100)
screen.tracer(0)			# 수동 화면 업데이트로 설정한다. 

ball = turtle.Turtle()
ball.color("orange", "black")
ball.shape("circle")
ball.up()
ball.goto(x, y)			# 공을 시작 위치로 옮긴다. 
ball.down()

while True:				# 애니메이션 루프
    x = x + vx				# 현재 위치가 업데이트된다. 
    y = y + vy
    ball.goto(x, y)
    vy = vy - gravity			# y방향 속도는 중력 만큼 감소된다. 

    if y < -height / 2:			# 바닥에 충돌하면
        vy = -vy* coef_res		# y방향 속도는 약간 줄어들고 방향은 반대가 된다. 
        ball.sety(-height / 2)		# 공의 y 좌표를 바닥으로 고정한다. 

    if x > width / 2 or x < -width / 2:	# 좌우의 벽에 충돌하면
        vx = - vx			# x 방향 속도는 반대가 된다. 
    screen.update()			# 화면을 업데이트한다. 
