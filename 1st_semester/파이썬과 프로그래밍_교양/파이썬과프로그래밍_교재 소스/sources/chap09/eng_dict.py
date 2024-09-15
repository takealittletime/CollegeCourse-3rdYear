english_dict = { }

english_dict['one'] = '하나'
english_dict['two'] = '둘'
english_dict['three'] = '셋'

while True:
	word = input("단어를 입력하시오(종료는 q): ");
	if word == "q":
		break;
	print (english_dict[word])
input()