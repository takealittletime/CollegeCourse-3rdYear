import gzip

data = """You said some winds blow forever
		And I didn't understand
		But you saw my eyes were asking
		And smiling you took my hand
		So we walked along the seaside"""

f = gzip.open('data.gz', 'wb') 		# 이진 쓰기 모드로 파일을 연다.
f.write(data.encode('UTF-8'))			# 문자열을 파일에 쓴다. 
f.close()

f = gzip.open('data.gz', 'rb') 		# 이진 읽기 모드로 파일을 연다.
result = f.read().decode('UTF-8')		# 파일에서 문자열을 읽는다. 
print(result)					# 읽은 문자열을 화면에 출력한다. 
f.close()
input()