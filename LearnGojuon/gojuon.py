import sys
import random
from tkinter import *
from tkinter import messagebox

def ShowHide():
    global position
    global words
    if (text2.get() != ""):
        text2.set("")
    else:
        word = words[position]
        text2.set(word[1 - mode])

def Next():
    global position
    global words
    position += 1
    if position >= len(words):
        position = 0
        random.shuffle(words)
        messagebox.showinfo("SelectWord", "New Round!")
    word = words[position]
    text1.set(word[mode])
    text2.set("")

def Mode():
    global position
    global words
    global mode
    messagebox.showinfo("SelectWord", "New Round!")
    mode = 1 - mode
    random.shuffle(words)
    position = 0
    text1.set(words[position][mode])
    text2.set("")
    
mode = 0

words = [
("a", u"あ"), ("i", u"い"), ("u", u"う"), ("e", u"え"), ("o", u"お"),
("ka", u"か"), ("ki", u"き"), ("ku", u"く"), ("ke", u"け"), ("ko", 
u"こ"),
("sa", u"さ"), ("shi", u"し"), ("su", u"す"), ("se", u"せ"), ("so", 
u"そ"),
("ta", u"た"), ("chi", u"ち"), ("tsu", u"つ"), ("te", u"て"), ("to", 
u"と"),
("na", u"な"), ("ni", u"に"), ("nu", u"ぬ"), ("ne", u"ね"), ("no", 
u"の"),
("ha", u"は"), ("hi", u"ひ"), ("fu", u"ふ"), ("he", u"へ"), ("ho", 
u"ほ"),
("ma", u"ま"), ("mi", u"み"), ("mu", u"む"), ("me", u"め"), ("mo", 
u"も"),
("ya", u"や"), ("yu", u"ゆ"), ("yo", u"よ"),
("ra", u"ら"), ("ri", u"り"), ("ru", u"る"), ("re", u"れ"), ("ro", 
u"ろ"),
("wa", u"わ"), ("wo", u"を"),
]
random.shuffle(words)
position = 0

root = Tk()

text1 = StringVar()
text1.set(words[position][mode])
text2 = StringVar()
text2.set("")

label1 = Label(root, textvariable=text1)
label1.pack()
label2 = Label(root, textvariable=text2)
label2.pack()
button1 = Button(root, text="Show/Hide", command=ShowHide)
button1.pack()
button2 = Button(root, text="Next", command=Next)
button2.pack()
button3 = Button(root, text="Mode", command=Mode)
button3.pack()

root.mainloop()

