# 텍스트 파일만이 들어 있는 디렉토리에서 실행하여야 함
import os
arr = os.listdir()
for f in arr:
    infile = open(f, "r", encoding="utf-8")
    for line in infile:
        e = line.rstrip() # 오른쪽 줄바꿈 문자를 없앤다.
        if "Python" in e:
            print(f, ":", e)
infile.close()
input()
