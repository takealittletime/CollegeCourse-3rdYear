class Car:
	def __init__(self, speed, color):
		self.speed = speed	# 반드시 self.을 앞에 붙여야 속성이 된다. 
		self.color = color		# 반드시 self.을 앞에 붙여야 속성이 된다. 

	def drive(self):			# 메소드의 첫 번째 인수는 항상 self이다.
		self.speed = 60		# 반드시 self.을 앞에 붙여야 속성이 된다. 
		print("주행중입니다.")
	def stop(self):
		self.speed = 0		# 반드시 self.을 앞에 붙여야 속성이 된다. 
		print("정지했습니다.")
myCar = Car(0, "white")	# 객체를 생성한다. 

myCar.drive()			# 객체의 drive() 메소드를 호출한다. 
print(myCar.speed)		# 60이 출력된다. 
myCar.stop()			# 객체의 stop() 메소드를 호출한다. 
print(myCar.speed)		# 0이 출력된다. 
input()