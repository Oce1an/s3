.model small
.stack 100h

MAX_LEN         EQU 200
CR              EQU 0Dh
LF              EQU 0Ah
STR_END         EQU '$'

DOS_INT         EQU 21h
FUNC_PRINT_STR  EQU 09h
FUNC_INPUT_STR  EQU 0Ah
FUNC_EXIT       EQU 4Ch

.data
msg_in_main     db CR, LF, 'Input initial string: ', STR_END
msg_in_find     db CR, LF, 'Input substring for search: ', STR_END
msg_in_repl     db CR, LF, 'Input replacement substring: ', STR_END
msg_result      db CR, LF, CR, LF, 'Result: ', STR_END
msg_not_found   db CR, LF, 'Substring not found.', CR, LF, STR_END

msg_err_empty   db CR, LF, 'String or substring must not be empty.', CR, LF, STR_END
msg_err_full    db CR, LF, 'Buffer size exceeded.', CR, LF, STR_END

main_buf        db MAX_LEN + 1
main_len        db ?
main_str        db MAX_LEN + 1 dup(STR_END)

find_buf        db MAX_LEN + 1
find_len        db ?
find_str        db MAX_LEN + 1 dup(STR_END)

repl_buf        db MAX_LEN + 1
repl_len        db ?
repl_str        db MAX_LEN + 1 dup(STR_END)

replaced_flag   db 0

.code
start:
    mov ax, @data
    mov ds, ax
    mov es, ax 
    
    mov ah, FUNC_PRINT_STR
    lea dx, msg_in_main
    int DOS_INT
    
    mov ah, FUNC_INPUT_STR
    lea dx, main_buf
    int DOS_INT

    cmp main_len, 0
    je error_empty

    mov ah, FUNC_PRINT_STR
    lea dx, msg_in_find
    int DOS_INT

    mov ah, FUNC_INPUT_STR
    lea dx, find_buf
    int DOS_INT

    cmp find_len, 0
    je error_empty

    mov ah, FUNC_PRINT_STR
    lea dx, msg_in_repl
    int DOS_INT

    mov ah, FUNC_INPUT_STR
    lea dx, repl_buf
    int DOS_INT

    xor si, si

search_loop:
    xor ax, ax
    mov al, main_len
    sub ax, si
    
    xor bx, bx
    mov bl, find_len
    cmp ax, bx
    jb finish_replace

    push si
    lea di, main_str
    add di, si
    lea si, find_str
    xor cx, cx
    mov cl, find_len
    cld
    repe cmpsb
    pop si

    je match_found
    inc si
    jmp search_loop

match_found:
    mov replaced_flag, 1

    xor ax, ax
    mov al, repl_len
    xor bx, bx
    mov bl, find_len
    sub ax, bx

    xor dx, dx
    mov dl, main_len
    add dx, ax
    cmp dx, MAX_LEN
    ja error_overflow

    cmp ax, 0
    je copy_replacement
    jl shift_left

shift_right:
    xor cx, cx
    mov cl, main_len
    sub cx, si
    xor bx, bx
    mov bl, find_len
    sub cx, bx
    
    push si
    push ax

    cmp cx, 0
    je end_shift_right

    lea si, main_str
    xor bx, bx
    mov bl, main_len
    dec bx
    add si, bx

    mov di, si
    add di, ax

    std
    rep movsb
    cld

end_shift_right:
    pop ax
    pop si
    add main_len, al
    jmp copy_replacement

shift_left:
    xor cx, cx
    mov cl, main_len
    sub cx, si
    xor bx, bx
    mov bl, find_len
    sub cx, bx

    push si
    push ax

    cmp cx, 0
    je end_shift_left

    lea bx, main_str
    add bx, si
    xor dx, dx
    mov dl, find_len
    add bx, dx
    
    mov si, bx
    mov di, bx
    add di, ax

    cld
    rep movsb

end_shift_left:
    pop ax
    pop si
    add main_len, al

copy_replacement:
    xor cx, cx
    mov cl, repl_len
    cmp cx, 0
    je advance_index

    push si
    lea di, main_str
    add di, si
    lea si, repl_str
    cld
    rep movsb
    pop si

advance_index:
    xor ax, ax
    mov al, repl_len
    add si, ax
    jmp search_loop

finish_replace:
    xor bx, bx
    mov bl, main_len
    mov main_str[bx], STR_END

    cmp replaced_flag, 0
    jne print_success

    mov ah, FUNC_PRINT_STR
    lea dx, msg_not_found
    int DOS_INT

print_success:
    mov ah, FUNC_PRINT_STR
    lea dx, msg_result
    int DOS_INT

    lea dx, main_str
    int DOS_INT
    jmp exit_prog

error_empty:
    mov ah, FUNC_PRINT_STR
    lea dx, msg_err_empty
    int DOS_INT
    jmp exit_prog

error_overflow:
    mov ah, FUNC_PRINT_STR
    lea dx, msg_err_full
    int DOS_INT

exit_prog:
    mov ah, FUNC_EXIT
    xor al, al
    int DOS_INT

end start