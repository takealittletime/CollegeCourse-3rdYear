import turtle		# 터틀 그래픽 모듈을 불러온다. 
import random 		# 난수 모듈을 불러온다. 

screen = turtle.Screen()
image1 = "front.gif"
image2 = "back.gif"
screen.addshape(image1)
screen.addshape(image2)

t1 = turtle.Turtle()		# 첫 번째 거북이를 생성한다.
coin = random.randrange(2)
if coin == 0 :
    t1.shape(image1)
    t1.stamp()
else : 
    t1.shape(image2)
    t1.stamp()	
input()