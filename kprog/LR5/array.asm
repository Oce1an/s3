.model small
.stack 100h

MAX_ELEMS       EQU 30
BUF_SIZE        EQU 10
CR              EQU 0Dh
LF              EQU 0Ah
STR_END         EQU '$'
MIN_INT16       EQU -32768
MAX_INT16       EQU 32767

DOS_INT         EQU 21h
FUNC_PRINT_CHAR EQU 02h
FUNC_PRINT_STR  EQU 09h
FUNC_INPUT_STR  EQU 0Ah
FUNC_EXIT       EQU 4Ch

.data
msg_ask_n       db CR, LF, 'Input amount of elements: ', STR_END
msg_elem_prefix db 'Element [', STR_END
msg_elem_suffix db ']: ', STR_END
msg_res_val     db CR, LF, 'Most common occuring number: ', STR_END
msg_res_cnt     db CR, LF, 'Number of occurences: ', STR_END
msg_unique      db CR, LF, 'Every element occures exactly one time.', STR_END

msg_err_n       db CR, LF, 'Must be from 1 to 30.', CR, LF, STR_END
msg_err_syntax  db CR, LF, 'Invalid number.', CR, LF, STR_END
msg_err_ovf     db CR, LF, 'Overflow.', CR, LF, STR_END

in_buf          db BUF_SIZE
in_len          db ?
in_str          db BUF_SIZE dup(?)

arr             dw MAX_ELEMS dup(?)
elem_count      dw ?
best_val        dw ?
best_cnt        dw ?

.code
start:
    mov ax, @data
    mov ds, ax

input_n_loop:
    lea dx, msg_ask_n
    call print_str
    call read_int16
    jc input_n_loop

    cmp ax, 1
    jl invalid_n
    cmp ax, MAX_ELEMS
    jg invalid_n

    mov elem_count, ax
    jmp start_arr_input

invalid_n:
    lea dx, msg_err_n
    call print_str
    jmp input_n_loop

start_arr_input:
    call print_crlf
    xor si, si
    mov cx, 1

input_arr_loop:
    cmp si, elem_count
    jge run_search

elem_retry:
    lea dx, msg_elem_prefix
    call print_str

    mov ax, cx
    call print_int16

    lea dx, msg_elem_suffix
    call print_str

    call read_int16
    jc elem_retry

    mov bx, si
    shl bx, 1
    mov arr[bx], ax

    inc si
    inc cx
    jmp input_arr_loop

run_search:
    call find_most_frequent

    cmp best_cnt, 1
    je print_all_unique

    lea dx, msg_res_val
    call print_str
    mov ax, best_val
    call print_int16

    lea dx, msg_res_cnt
    call print_str
    mov ax, best_cnt
    call print_int16
    jmp finish_program

print_all_unique:
    lea dx, msg_unique
    call print_str

finish_program:
    call print_crlf

    mov ah, FUNC_EXIT
    xor al, al
    int DOS_INT

print_str proc
    push ax
    mov ah, FUNC_PRINT_STR
    int DOS_INT
    pop ax
    ret
print_str endp

print_crlf proc
    push ax
    push dx
    mov ah, FUNC_PRINT_CHAR
    mov dl, CR
    int DOS_INT
    mov dl, LF
    int DOS_INT
    pop dx
    pop ax
    ret
print_crlf endp

read_int16 proc
    push bx
    push cx
    push dx
    push si

    mov ah, FUNC_INPUT_STR
    lea dx, in_buf
    int DOS_INT
    call print_crlf

    xor cx, cx
    mov cl, in_len
    cmp cl, 0
    je ri_err_syn

    xor si, si
    xor bx, bx

    mov al, in_str[si]
    cmp al, '-'
    je ri_set_neg
    cmp al, '+'
    je ri_skip_sign
    jmp ri_check_digits

ri_set_neg:
    mov bl, 1
ri_skip_sign:
    inc si
    dec cl
    jz ri_err_syn

ri_check_digits:
    xor ax, ax

ri_digit_loop:
    mov dl, in_str[si]
    cmp dl, '0'
    jb ri_err_syn
    cmp dl, '9'
    ja ri_err_syn

    sub dl, '0'
    xor dh, dh

    push dx
    mov dx, 10
    mul dx
    pop dx
    jc ri_err_ovf

    add ax, dx
    jc ri_err_ovf

    cmp bl, 1
    je ri_chk_neg_lim

    cmp ax, MAX_INT16
    ja ri_err_ovf
    jmp ri_next_char

ri_chk_neg_lim:
    cmp ax, 8000h
    ja ri_err_ovf

ri_next_char:
    inc si
    loop ri_digit_loop

    cmp bl, 1
    jne ri_done_ok
    neg ax

ri_done_ok:
    clc
    jmp ri_exit

ri_err_syn:
    lea dx, msg_err_syntax
    call print_str
    stc
    jmp ri_exit

ri_err_ovf:
    lea dx, msg_err_ovf
    call print_str
    stc

ri_exit:
    pop si
    pop dx
    pop cx
    pop bx
    ret
read_int16 endp

print_int16 proc
    push ax
    push bx
    push cx
    push dx

    cmp ax, 0
    jge pi_positive

    push ax
    mov ah, FUNC_PRINT_CHAR
    mov dl, '-'
    int DOS_INT
    pop ax
    neg ax

pi_positive:
    xor cx, cx
    mov bx, 10

pi_div_loop:
    xor dx, dx
    div bx
    push dx
    inc cx
    cmp ax, 0
    jne pi_div_loop

pi_print_loop:
    pop dx
    add dl, '0'
    mov ah, FUNC_PRINT_CHAR
    int DOS_INT
    loop pi_print_loop

    pop dx
    pop cx
    pop bx
    pop ax
    ret
print_int16 endp

find_most_frequent proc
    push ax
    push bx
    push cx
    push dx
    push si
    push di

    mov ax, arr[0]
    mov best_val, ax
    mov best_cnt, 0

    xor si, si

fmf_outer:
    cmp si, elem_count
    jge fmf_done

    mov bx, si
    shl bx, 1
    mov ax, arr[bx]

    xor cx, cx
    xor di, di

fmf_inner:
    cmp di, elem_count
    jge fmf_check_max

    mov bx, di
    shl bx, 1
    cmp ax, arr[bx]
    jne fmf_next_inner
    inc cx

fmf_next_inner:
    inc di
    jmp fmf_inner

fmf_check_max:
    cmp cx, best_cnt
    jle fmf_next_outer
    mov best_cnt, cx
    mov best_val, ax

fmf_next_outer:
    inc si
    jmp fmf_outer

fmf_done:
    pop di
    pop si
    pop dx
    pop cx
    pop bx
    pop ax
    ret
find_most_frequent endp

end start
