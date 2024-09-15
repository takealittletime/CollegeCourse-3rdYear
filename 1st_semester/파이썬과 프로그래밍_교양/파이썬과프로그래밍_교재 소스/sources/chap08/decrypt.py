encrypted_text = "Mpwf!xjmm!gjoe!b!xbz/"	# 암호문
                                                   
plain_text = ""                                   # 평문
for c in encrypted_text:                         # 암호문의 모든 글자에 대하여 반복한다. 
    x = ord(c)                                    # 글자의 코드값을 구한다. 
    x = x - 1                                      # 코드값을 하나 감소한다. 
    cc  = chr(x)                                  # 감소된 코드값에 해당하는 문자를 계산한다. 
    plain_text = plain_text + cc                  # 평문에 추가한다. 
print(plain_text)                                # 평문을 출력한다. 
input()