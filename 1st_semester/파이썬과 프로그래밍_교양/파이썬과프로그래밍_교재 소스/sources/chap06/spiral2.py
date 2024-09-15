import turtle

# 색상은 리스트에 저장했다가 하나씩 꺼내서 변경하도록 하자. 
colors = ["red", "purple", "blue", "green", "yellow", "orange" ]
t = turtle.Turtle()

# 배경색은 다음과 같은 문장으로 변경이 가능하다. 
turtle.bgcolor("black")

# 거북이의 속도는 0으로 설정하면 최대가 된다. 
t.speed(2)

#거북이가 그리는 선의 두께는 width()를 호출하면 된다. 
t.width(3)

length = 10	# 초기 선의 길이는 10으로 한다. 

# while 반복문이다. 선의 길이가 500보다 작으면 반복한다.  
while length < 500:	
    t.forward(length)			# length만큼 전진한다. 
    t.pencolor(colors[length%6])		# 선의 색상을 변경한다. 
    t.right (89)				# 89도 오른쪽으로 회전한다. 
    length += 5			# 선의 길이를 5만큼 증가한다. 

turtle.done()
