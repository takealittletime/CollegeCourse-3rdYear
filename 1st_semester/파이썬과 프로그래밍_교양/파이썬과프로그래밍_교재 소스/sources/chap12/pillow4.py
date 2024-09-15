import tkinter as tk

# 파일 메뉴에서 “열기”를 선택하였을 때 호출되는 함수
def open():
    pass

# 파일 메뉴에서 “종료”를 선택하였을 때 호출되는 함수
def quit():
    window.quit()

# 윈도우를 생성한다. 
window = tk.Tk()

# Menu()를 사용하여 윈도우 안에 메뉴를 생성한다. 
menubar = tk.Menu(window)

# 메뉴바 안에 “파일” 메뉴를 생성한다.  
filemenu = tk.Menu(menubar)

# “파일” 메뉴 안에 “열기” 메뉴항목을 추가한다. 
filemenu.add_command(label="열기", command=open)
filemenu.add_command(label="종료", command=quit)

# “파일” 메뉴를 누르면 아래로 다른 메뉴가 확장되도록 한다. 
menubar.add_cascade(label="파일", menu=filemenu)

# 윈도우 창의 메뉴로 menubar를 지정한다. 
window.config(menu=menubar)
window.mainloop()
