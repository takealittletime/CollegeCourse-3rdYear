import cv2

cap= cv2.VideoCapture(0)		# (1)

writer= cv2.VideoWriter('myvideo.mp4', cv2.VideoWriter_fourcc(*'DIVX'), 
			20, (640, 480))		# (2)

while True:				# (3)
    ret,frame= cap.read()		# (4)
    writer.write(frame)		# (5)
    cv2.imshow('frame', frame)		# (6)
    if cv2.waitKey(1) & 0xFF == 27:	# (7)
        break

cap.release()				# (8)
writer.release()
cv2.destroyAllWindows()
