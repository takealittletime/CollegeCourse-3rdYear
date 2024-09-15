s = "You said some winds blow forever and I didn't understand"
list1 = s.split()
remove_words = ['some', 'forever']

print("입력 문자열:")
print(s)
print("삭제 단어들:")
print(remove_words)
print("삭제 후 남은 단어들")

for word in list1:
    if word in remove_words:
            list1.remove(word)
print(list1)
input()