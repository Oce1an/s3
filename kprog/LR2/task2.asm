 org $9000

 fcb $01
 fcb $02 

 org $8000

start:
 ldx #$9000
 ldy #$90ff

loop:
 ldaa 0,x
 ldab 0,y
 staa 0,y
 stab 0,x

 inx
 dey

 cpx #$9080
 bne loop

 bra *