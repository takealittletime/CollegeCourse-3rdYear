#20194111 최민규
n = int(input("정수(홀수)를 하나 입력하세요:"))     # 정수 입력 (홀수 조건)

for i in range(1,n+1,2):                            # (위쪽 탑 그리기: 1부터 n까지 2씩 증가하며 반복)
    for j in range(1,int(n-i/2)):                   # 앞 쪽 공백 (n-i/2)만큼 출력
        print(" ",end = "")
    print("*"*i)                                    # i만큼 * 출력

for k in range(n-1,-1,-2):                          # (아래쪽 탑 그리기: n-1부터 0까지 2씩 감소하며 반복)
    for m in range(1,int(n-k/2)):                   # 앞 쪽 공백 (n-k/2)만큼 출력
        print(" ",end = "")
    print("*"*(k-1))                                # k-1만큼 * 출력