#20194111 최민규
import turtle                                   #turtle 라이브러리 import.
import random                                   #random 라이브러리 import.

t = turtle.Turtle()                             #turtle 객체 생성
t.speed(0)                                      #turtle 그리기 속도를 최대로 설정
chk = 0                                         #그린 원의 개수

#사용할 색깔을 담은 리스트
colors = ['red', 'orange', 'yellow', 'green', 'blue', 'navy', 'purple']

def drawit(x, y):
    global chk                                  #chk를 전역변수로 사용할 것임을 선언.
    t.penup()                                   #펜을 들어 원하는 좌표에 가서 내림.
    t.goto(x, y)
    t.pendown()
    t.begin_fill()                              #여기서부터 색 채움
    t.color(random.choice(colors))              #colors 리스트에서 랜덤하게 색 선택
    t.circle(random.randint(10, 50))            #크기 10 ~ 50 중 랜덤 설정
    t.end_fill()                                #색 채우기 종료
    chk += 1                                    #그린 원의 개수 증가
    if chk >= 5:                                #원의 개수가 5개 이상일 때 프로그램 종료
        exit()  # 프로그램 종료

s = turtle.Screen()                             #그림을 그릴 화면
s.onscreenclick(drawit)                         #마우스 클릭 시 이벤트 처리로 drawit 함수를 호출.
turtle.done()                                   #turtle을 이용한 그래픽 처리 완료.