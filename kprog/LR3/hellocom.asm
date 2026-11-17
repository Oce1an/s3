.model tiny
.code
org 100h

start:
mov ah, 09h
mov dx, offset msg1
int 21h

mov ah, 09h
mov dx, offset msg2
int 21h

mov ah, 09h
mov dx, offset msg3
int 21h

ret

msg1 db "Good Afternoon,", 0Dh, 0Ah, '$'
msg2 db "Good Evening,", 0Dh, 0Ah, '$'
msg3 db "And Good Night.", 0Dh, 0Ah, '$'

end start