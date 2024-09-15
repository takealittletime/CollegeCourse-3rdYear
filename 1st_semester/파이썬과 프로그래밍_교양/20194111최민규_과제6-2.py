#20194111 최민규
import random                                                   #난수 발생을 위해 random 라이브러리를 import.

ans = random.randint(1,100)                                     #1부터 100 사이 난수 생성
inp = int(input("숫자 입력: "))                                 #사용자가 숫자 입력
tried = 0                                                       #시도 횟수를 표시할 정수형 변수

while True:                                                     #무한 루프
    tried += 1                                                  #시도 횟수 증가

    if (ans == inp or tried >=10):                              #정답을 맞추거나 시도횟수 10번 이상일 때 break.
        break;

    elif (ans>inp):                                             #답이 입력 값보다 큰 경우 출력
        print("말씀하신 값이 답보다 작습니다.")
        print("시도 횟수:",tried)
        print()

    else:                                                       #답이 입력 값보다 작은 경우 출력
        print("말씀하신 값이 답보다 큽니다.")
        print("시도 횟수:",tried)
        print()

    inp = int(input("숫자 입력: "))                             #사용자가 숫자 입력

                                                                #루프 밖 결과 출력
if (tried >= 10):                                               #시도 횟수 제한 초과 시 출력
    print("아쉽습니다! 시도 횟수 제한을 초과하셨습니다.")
    print("답:", ans)
    print("시도 횟수:", tried)

else:                                                           #정답 시 출력
    print("축하합니다! 정답입니다!")
    print("답:",ans)
    print("시도 횟수:",tried)