phone_book = {'홍길동': '1234', '이순신': '1235', '강감찬': '1236'}
for key in sorted(phone_book.keys()):
	print(key, phone_book[key])

phone_book.pop("홍길동")
print(phone_book)
phone_book.clear()
print(phone_book)
input()