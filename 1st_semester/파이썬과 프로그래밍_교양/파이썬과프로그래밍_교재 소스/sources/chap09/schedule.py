mydict = {}

while True:
    date = input("날짜를 입력하시오: ")
    if date == "q" : break
    job = input("일정을 입력하시오: ")

    if date not in mydict:
        mydict[date]=job
    else:
        print("오류입니다.")

print(mydict)
input()