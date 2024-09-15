#20194111 최민규
import random                               #난수 발생을 위해 random 라이브러리 추가.

def remove_from_list(list,num):             #num이 제거된 새로운 리스트를 만들어서 return 함
    tmp = list                              #tmp변수에 list 할당
    while num in tmp:                       #tmp 리스트에 존재하는 모든 num을 삭제
        tmp.remove(num)
    return tmp                              #tmp 리스트를 반환

list = []                                   #리스트 정의
for i in range (10):                        #리스트에 1부터 10까지의 난수 10개를 할당
    list.append(random.randint(1,10))

print("생성된 리스트는 :", list)           #생성된 리스트 출력
n = int(input("삭제할 숫자는? : "))        #삭제 할 수 입력
res = remove_from_list(list,n)             #함수 호출 후 반환값을 res에 할당
print("제거 후의 결과는 :", res)           #결과 출력