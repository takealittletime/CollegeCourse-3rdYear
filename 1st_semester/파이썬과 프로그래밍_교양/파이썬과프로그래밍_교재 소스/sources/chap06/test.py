def click(key):
	if key == "=":
		result = eval(display.get())
		s = str(result)
		display.insert(END, "=" + s)
	else:
		display.insert(END, key)
