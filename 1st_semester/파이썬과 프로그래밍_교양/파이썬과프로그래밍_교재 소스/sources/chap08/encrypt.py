plain_text = "Love will find a way."	# 평문
 
encrypted_text = ""			# 암호문
for c in plain_text:			# 평문의 모든 글자에 대하여 반복한다. 
    x = ord(c)				# 글자의 코드값을 구한다. 
    x = x + 1				# 코드값을 하나 증가한다. 
    cc = chr(x)			# 증가된 코드값에 해당하는 문자를 계산한다. 
    encrypted_text = encrypted_text + cc  # 암호문에 추가한다. 
print(encrypted_text)			# 암호문을 출력한다. 
input()