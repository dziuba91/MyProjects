; @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
; Program: SYMULCJA RUCHU WAHAD£A MATEMATYCZNEGO
; Autor: Tomasz Dziuba
; Data: 21.05.2012
;
; @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
; Jêzyk programowania: Assembler
; Rodzaj Assemblera: Flat Assembler (FASM)
; (program pisany pod system: DOS)
;
; @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
;
;             OBS£UGA PROGRAMU:
;         --------------------------
;    'ENTER' -> Uruchomienie ruchu wahad³a
;    'ESC' -> Zakoñczenie programu
;    '1' -> Przywrócenie pocz¹tkowych ustawieñ wahad³a (przed wciœniêciem ENTERA)
;    '2' -> PAUZA (zatrzymanie ruchu wahad³a podczas ruchu- wznawiamy ruch od danego momentu ENTEREM)
;    'w' -> Zmniejszenie d³ugoœci wahad³a
;    's' -> Zwiêkszenie d³ugoœci wahad³a
;    'a' -> Zmniejszenie wartoœci k¹ta (-5 stopni)
;    'd' -> Zwiêkszenie wartoœci k¹ta (+5 stopni)
;
; @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@



format MZ
entry  main:start
stack 100h
segment main


macro pixel X,Y,C
{
	mov  ax, 320
	mul  [Y]
	mov  di, ax
	add  di, [X]

	mov  al, [C]
	stosb
}

macro graphinit
{
	mov  ax, 13h
	int  10h

	mov  di, 0a000h
	mov  es, di
}

macro ramka
{
local czekaj1
local czekaj2

	mov dx,3dah
czekaj1:
	in al,dx
	test al,8
	jnz czekaj1
czekaj2:
	in al,dx
	test al,8
	jz czekaj2
}


macro linia X1,Y1,X2,Y2
{
local rownanie
local rysuj1
local rysuj2
local X1_nie_rowny_X2
local koniec

	 finit
rownanie:
	fild [Y2]
	fisub [Y1]
	fild [X2]
	fisub [X1]
	fdivp
	fstp [A]

	fild [Y1]
	fld [A]
	fimul [X1]
	fsubp
	fstp [B]


	mov ax,[Y1]
	mov [tmp1],ax

	mov ax,[X1]
	cmp ax,[X2]
	jne X1_nie_rowny_X2
	mov [tmp2],ax
 ;       mov cx,100
rysuj1:
	pixel tmp2,tmp1,C_3
	mov ax,[tmp1]
	clc
	adc ax,1
	mov [tmp1],ax
	cmp ax,[Y2]
	je koniec
	jmp rysuj1
  ;      loop rysuj1

X1_nie_rowny_X2:
       ; mov ax,[Y1]
       ; mov [tmp],ax

       ; mov cx,100
rysuj2:
	fild [tmp1]
	fsub [B]
	fdiv [A]
	fistp [tmp2]
	pixel tmp2,tmp1,C_3
	mov ax,[tmp1]
	clc
	adc ax,1
	mov [tmp1],ax
	cmp ax,[Y2]
	je koniec
	jmp rysuj2
     ;   loop rysuj

koniec:
;        ffree st0
;        ffree st1
;        ffree st2
}


macro kolo X0,Y0,R
{
local rysuj
local delta
local iks_1
local iks_2
local tmp3_mniejsze
local tmp3_wieksze
local koniec

	finit

	mov ax,[Y0]
	clc
	sbb ax,R
	mov [tmp1],ax
	pixel X0,tmp1,C_1

	mov ax,[Y0]
	clc
	adc ax,R
	mov [tmp2],ax
	pixel X0,tmp2,C_1

	mov ax,R
	mov [pro],ax

	mov [A],1.0

	fild [X0]
	fmul [min_jed]
	fmul [dwa]
	fstp [B]


;        mov cx,100
rysuj:
	mov ax,[tmp1]
	clc		    ;
	adc ax,1
	mov [tmp1],ax
	cmp ax,[tmp2]
	je koniec

	pixel X0,tmp1,C_4

	fild [X0]
	fimul [X0]
	fild [pro] ;promieñ
	fimul [pro]
	fsubp
	fstp [C]

	fld [C]
	fild [tmp1]
	fisub [Y0]
	fst [C]
	fmul [C]
	faddp
	fstp [C]

delta:
	fld [B]
	fmul [B]
	fld [cztery]
	fmul [A]
	fmul [C]
	fsubp
	fsqrt
	fstp [del]
iks_1:
	fld [B]
	fmul [min_jed]
	fld [del]
	fsubp
	fld [dwa]
	fmul [A]
	fdivp
	fabs
	fistp [tmp3]
iks_2:
	fld [B]
	fmul [min_jed]
	fld [del]
	faddp
	fld [dwa]
	fmul [A]
	fdivp
	fabs
	fistp [tmp4]

;        mov ax,[tmp3]
;        cmp ax,[tmp4]
;        jb tmp3_mniejsze
;tmp3_wieksze:
;        pixel tmp4,tmp1,C_4
;        mov ax,[tmp4]
;        clc
;        adc ax,1
;        mov [tmp4],ax
;        cmp ax,[tmp3]
;        jbe tmp3_wieksze
;        jmp rysuj

tmp3_mniejsze:
	pixel tmp3,tmp1,C_5
	pixel tmp4,tmp1,C_5
	mov ax,[tmp3]
	clc
	adc ax,1
	mov [tmp3],ax
	cmp ax,[tmp4]
	jbe tmp3_mniejsze
;        ffree st0
;        ffree st1
;        ffree st2
	jmp rysuj

koniec:
}

macro prostokat X,Y,dlug_prost
{
local rysuj
local dajesz
	finit

	fild [X]
	fisub [dlug_prost]
	fistp [tmp1]

	fild [X]
	fiadd [dlug_prost]
	fistp [tmp2]

	fild [Y]
	fsub [jed]
	fistp [tmp4]

	mov cx,3
rysuj:
	mov ax,[tmp1]
	mov [tmp3],ax

dajesz:
	pixel tmp3,tmp4,C_6
	mov ax,[tmp3]
	add ax,1
	mov [tmp3],ax
	cmp ax,[tmp2]
	jne dajesz

	add [tmp4],1
	loop rysuj
}

macro stop_na_rad X,Y
{
	finit

	fld [X]
	fmul [PI]
	fdiv [st_os]
	fstp [Y]
}

macro rad_na_stop X,Y
{
	finit

	fld [X]
	fmul [st_os]
	fdiv [PI]
	fstp [Y]
}

macro parametry kat,dlugosc
{
	finit

	fld [alfa]
	fsin
	fmul [dlugosc]
	fstp [iks]

	fld [alfa]
	fcos
	fmul [dlugosc]
	fstp [igrek]


	fld [iks]
	fiadd [X_1]
	fistp [X_2]

	fld [igrek]
	fiadd [Y_1]
	fistp [Y_2]
}

macro wahadlo X,Y
{
	finit

	fld [przy]
	fmul [min_jed]
	fdiv [dlu]
	fld [alfa]
	fsin
	fmulp
	fld [beta]
	fmul [dwa]
	fmul [omega]
	fsubp
	fstp [k1]

	fld [przy]
	fmul [min_jed]
	fdiv [dlu]
	fld [alfa]
	fsin
	fmulp
	fld [krok]
	fmul [pol]
	fmul [k1]
	fadd [omega]
	fmul [dwa]
	fmul [beta]
	fsubp
	fstp [k2]


	fld [k2]
	fmul [krok]
	fadd [omega]
	fstp [omega]

	fld [omega]
	fmul [krok]
	fadd [alfa]
	fstp [alfa]


	fld [alfa]
	fsin
	fmul [dlu]
	fstp [X]

	fld [alfa]
	fcos
	fmul [dlu]
	fstp [Y]


	fld [X]
	fiadd [X_1]
	fistp [X_2]

	fld [Y]
	fiadd [Y_1]
	fistp [Y_2]
}


;********************************************************************************************

start:
	mov ax,dane
	mov ds,ax

	graphinit

ustawienia:
	mov [spr],0
	mov [omega],0.0
	stop_na_rad alfa2,alfa
	parametry alfa,dlu

	ramka
	call cls

	linia X_1,Y_1,X_2,Y_2
	prostokat X_1,Y_1,prost
	pixel X_1,Y_1,C_2
	kolo X_2,Y_2,5
	pixel X_2,Y_2,C_2

ety:
	mov ah,7
	int 21h
	cmp al,27 ;kod ascii escape
	je wylacz
	cmp al,'a'
	je zmien1
	cmp al,'d'
	je zmien2
	cmp al,'w'
	je zmien3
	cmp al,'s'
	je zmien4
	cmp al,'1'
	je ustawienia
	cmp al,'2'
	je ety
	cmp al,13
	je petla
	cmp [spr],1
	je petla
	jmp ety

zmien1:
	finit

	fld [alfa2]
	fsub [go]

	ficom [min_nana_juu]	   ;+
	fstsw ax	  ;+
	sahf		  ;+
	jb ety

	fstp [alfa2]
	jmp ustawienia

zmien2:
	finit

	fld [alfa2]
	fadd [go]

	ficom [nana_juu]       ;+
	fstsw ax	  ;+
	sahf		  ;+
	ja ety

	fstp [alfa2]
	jmp ustawienia

zmien3:
	finit

	fld [dlu]
	fsub [go]

	ficom [ni_juu]	     ;+
	fstsw ax	  ;+
	sahf		  ;+
	jb ety

	fstp [dlu]
	jmp ustawienia

zmien4:
	finit

	fld [dlu]
	fadd [go]

	ficom [hyaku_go_juu]	   ;+
	fstsw ax	  ;+
	sahf		  ;+
	ja ety

	fstp [dlu]
	jmp ustawienia


petla:
	mov [spr],1

	 ramka
	call cls


	linia X_1,Y_1,X_2,Y_2
	prostokat X_1,Y_1,prost
	pixel X_1,Y_1,C_2
	kolo X_2,Y_2,5
	pixel X_2,Y_2,C_2

	wahadlo iks,igrek

	fwait
	mov ah,01h
	int 16h
	jnz ety


	jmp petla


game_over:
	mov ah,7
	int 21h

wylacz:
	mov ah,4ch
	int 21h

;*********************************************************************************************


cls:
    mov [color], 100

    push cx
    push ax
    mov cx, 64000
paint:
    mov di, cx
    mov al, [color]
    stosb
    sub cx, 1
    cmp cx, 0
    jne paint
    pop ax
    pop cx
ret



;@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

segment dane

    ;zmienne operacyjne
       tmp1 dw 0
       tmp2 dw 0
       tmp3 dw 0
       tmp4 dw 0

	A  dd 1.0
	B  dd 0.0
	C  dd 0.0
	del dd 0.0
	pro dw 0

	spr db 0

    ;liczby
	min_jed dd -1.0
	jed dd 1.0
	dwa	dd 2.0
	cztery	dd 4.0
	pol	dd 0.5
	PI  dd 3.14
	st_os dd 180.0
	go dd 5.0
	nana_juu dd 70
	hyaku_go_juu dd 164
	ni_juu dd 20
	min_nana_juu dd -70

;##############################################
	;dane
       X_1 dw 160
       Y_1 dw 20

       X_2 dw 0
       Y_2 dw 0

	iks   dd 30
	igrek  dd 100

       C_1 db 39
       C_2 db 13
       C_3 db 2
       C_4 db 71
       C_5 db 0
       C_6 db 245
       color db 0

       prost dw 20

;##############################################
	;zmienne do wyliczania ruchu wahadla

	dlu dd 70.0
	przy dd 9.81
	alfa dd 45.0
	alfa2 dd 45.0
	beta dd 0.01
	omega dd 0.0
	X_pom dd 0.0
	Y_pom dd 0.0
	krok dd 0.5
	k1 dd 0.0
	k2 dd 0.0
