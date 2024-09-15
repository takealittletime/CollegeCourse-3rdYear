from PIL import Image, ImageTk, ImageFilter 
import tkinter as tk

window = tk.Tk()
canvas = tk.Canvas(window, width=500, height=500)
canvas.pack()

# 영상 파일을 연다. 
im = Image.open("lenna.png")

# 영상을 흐리게 한다. 
out = im.filter(ImageFilter.BLUR)

# 영상을 tkinter 형식으로 변환한다. 
tk_img = ImageTk.PhotoImage(out)

# 영상을 tkinter에서 화면에 표시한다. 
canvas.create_image(250, 250, image=tk_img)
window.mainloop()
