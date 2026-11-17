.model small

.stack 100h

.data
msg1 db "Good Afternoon,", 0Dh, 0Ah, '$'
msg2 db "Good Evening,", 0Dh, 0Ah, '$'
msg3 db "And Good Night.", 0Dh, 0Ah, '$'

.code
start:
mov ax, @data
mov ds, ax

mov ah, 09h
mov dx, offset msg1
int 21h

mov ah, 09h
mov dx, offset msg2
int 21h

mov ah, 09h
mov dx, offset msg3
int 21h

mov ax, 4C00h
int 21h

end start