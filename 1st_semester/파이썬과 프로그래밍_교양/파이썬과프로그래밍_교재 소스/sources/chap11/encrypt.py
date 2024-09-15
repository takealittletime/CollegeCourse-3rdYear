key = 'abcdefghijklmnopqrstuvwxyz'

# 평문을 받아서 암호화하고 암호문을 반환한다. 
def encrypt(n, plaintext):
    result = ''
    for l in plaintext.lower():
        try:
            i = (key.index(l) + n) % 26
            result += key[i]
        except ValueError:
            result += l

    return result.lower()

f = open("test.txt", "r")
s = f.read()
s = s.rstrip()

encrypted = encrypt(3, s)		# 3은 이동거리이다. 
print ('평문: ' ,  s)
print ('암호문: ',  encrypted)
f.close()
input()
