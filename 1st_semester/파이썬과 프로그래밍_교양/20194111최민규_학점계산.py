#20194111 최민규
import random                       #random 라이브러리 추가

#학점은 0~100사이의 난수로 생성.
gpa = random.randint(0,100)
print("생성된 점수는",gpa,"입니다.") #학점 출력

if (gpa>=90):                   #90점 이상이면 A학점
    print("A학점입니다.")

elif (gpa>=80):                 #80~89학점이면 B학점
    print("B학점입니다.")

elif (gpa>=70):                 #70~79학점이면 C학점
    print("C학점입니다.")

elif (gpa>60):                  #60~69학점이면 D학점
    print("D학점입니다. ")

else:                           #59학점 아래부터는 F학점.
    print("F학점입니다.")