phone_book = {'홍길동': '1234', '이순신': '1235', '강감찬': '1236'}
print(phone_book["강감찬"])
phone_book["강감찬"] = '9999'
print(phone_book["강감찬"])

phone_book = { }

phone_book["홍길동"] =  '1234'
phone_book["이순신"] =  '1235'
phone_book["강감찬"] =  '1236'
print(phone_book)

print(phone_book.keys())
print(phone_book.values())
input()
