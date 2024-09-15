myList = [ "우유", "사과", "두부", "소고기"]
for item in myList :
	print(item)
myList = [ "우유", "사과", "두부", "소고기"]
for i in range(len(myList)) :
	print(myList[i])

myList = [ "우유", "사과", "두부", "소고기"]
myList[1] = '커피'
print(myList)

myList = [ ]
myList.append("우유")
myList.append("사과")
myList.append("두부")
myList.append("소고기")
print(myList)

myList = [ "우유", "사과", "두부", "소고기"]
myList.insert(1, '커피')
print(myList)
myList = [ "우유", "사과", "두부", "소고기"]
myList.remove("소고기")
print(myList)

if "소고기" in myList:
	myList.remove("소고기")

myList = [ "우유", "사과", "두부", "소고기"]
item = myList.pop(0)		# 0번째 항목 삭제, item은 우유
print(myList)
myList = [ "우유", "사과", "두부", "소고기"]
i = myList.index("소고기")		# i는 3

if "소고기" in myList:
    print(myList.index("소고기"))
input()