sts segment stack 'stack'
	db 512 dup(0)
sts ends

dane segment
	txt1 db 'a = $'
	txt2 db '+$'
	txt3 db 'b = $'
	txt4 db 'W = $'
	txt5 db 'Linia Polecen: $'

;############################################
	   
	l1 db 0,0,0,0
	l2 db 0,0,0,0
	lw db 0,0,0,0

	z1 db 0
	c1 db 0
	m1 db 0,0,0	  

	z2 db 0
	c2 db 0
	m2 db 0,0,0	 

	zw db 0
	cw db 0
	mw db 0,0,0
dane ends


wyswietl macro txt
	lea dx,txt
	mov ah,9
	int 21h
endm

ustaw_kursor macro A,B
	mov bh,0
	mov dh,A	
	mov dl,B	
	mov ah,2	
	int 10h		
endm

wprowadz_single_w_hex macro A
local ety1
local cyfra
local lit_D
local lit_M
local dalej
local przesuwanie_zmiennej
local koniec
local czy_koniec

	mov cx,8
ety1:
	mov ah,7
	int 21h
	cmp al,27 ;kod ascii escape
	je wylacz
	cmp al,13 ;kod ascii enter
	je koniec
	cmp al,'0'
	jb	ety1
	cmp al,'9'
	jbe cyfra
	cmp al,'A'
	jb	ety1
	cmp al,'F'
	jbe lit_D
	cmp al,'a'
	jb	ety1
	cmp al,'f'
	jbe lit_M
	jmp ety1
cyfra:
	mov dl,al
	mov ah,2
	int 21h
	sub al,30h
	jmp dalej
lit_D:
	mov dl,al
	mov ah,2
	int 21h
	sub al,55
	jmp dalej
lit_M:
	mov dl,al
	mov ah,2
	int 21h
	sub al,87
dalej:
	shl al,4
	mov si,4
przesuwanie_zmiennej:
	clc
	rcl al,1
	rcl [A+3],1
	rcl [A+2],1
	rcl [A+1],1
	rcl [A],1
	dec si
	cmp si,0
	jne przesuwanie_zmiennej

	loop ety1

czy_koniec:
	mov ah,7
	int 21h
	cmp al,27 ;kod ascii escape
	je wylacz
	cmp al,13 ;kod ascii enter
	jne czy_koniec
koniec:
endm

zmienne macro X,a,b,c
local znak
local cecha
local mantysa

;^^^^^^^otrzymanie mantysy, cechy, znaku^^^^^^^^

;znak
znak:
	mov al,[X]
	shr al,7
	mov [a],al

;cecha w U2
cecha:
	clc
	mov al,[X+1]
	rcl al,1
	mov al,[X]
	rcl al,1
	sub al,127
	mov [b],al

;mantysa ca³a
mantysa:
	mov al,[X+1]
	or al,80h
	mov [c],al
	mov al,[X+2]
	mov [c+1],al
	mov al,[X+3]
	mov [c+2],al
endm

wyswietl_w_hex macro A,l
local wysw_hex
local ety4
local ety5
local ety6
local ety7

	mov si,0
	mov cx,l
wysw_hex:
	mov al,[A+si]  ;pierwsza cyfra hex
	shr al,4
	cmp al,10
	jb ety4
	add al,55	; kody ascii liter
	jmp ety5
ety4:
	add al,30h
ety5:
	mov dl,al
	mov ah,2
	int 21h

	mov al,[A+si]  ;druga cyfra hex
	and al,0Fh
	cmp al,10
	jb ety6
	add al,55	; kody ascii liter
	jmp ety7
ety6:
	add al,30h
ety7:
	mov dl,al
	mov ah,2
	int 21h

	inc si
	loop wysw_hex
endm

dodaj_mantysy macro W,A,B,C
local dodaj
local modyf_ceche
local koniec

	clc
	mov al,[A+2]
	mov dl,[B+2]
	add al,dl
	mov [W+2],al

	mov si,1
	mov cx,2
dodaj:
	mov al,[A+si]
	mov dl,[B+si]
	adc al,dl
	mov [W+si],al
	dec si
	loop dodaj

	jc modyf_ceche
	jmp koniec

modyf_ceche:
	rcr [W],1
	rcr [W+1],1
	rcr [W+2],1
	add [C],1

koniec:
endm

odejmij_mantysy macro W,A,B,C
local odejmij
local modyf_ceche
local koniec

	clc
	mov si,2
	mov cx,3
odejmij:
	mov al,[A+si]
	mov dl,[B+si]
	sbb al,dl
	mov [W+si],al
	dec si
	loop odejmij

	clc
	rcl [W+2],1
	rcl [W+1],1
	rcl [W],1
	jnc modyf_ceche
	jmp koniec

modyf_ceche:
	clc
	sub [C],1
	rcl [W+2],1
	rcl [W+1],1
	rcl [W],1
	jnc modyf_ceche

koniec:
	rcr [W],1
	rcr [W+1],1
	rcr [W+2],1
endm

;********************************************************************************************

prog segment
	assume cs:prog,ds:dane,ss:sts
P486N

start:
	mov ax,seg dane
	mov ds,ax

	call clrscr
	call ekran

ktora_dana:
	mov ah,7
	int 21h
	cmp al,27 ;kod ascii escape
	je wylacz
	cmp al,'w'
	je wynik
	cmp al,'W'
	je wynik
	cmp al,'a'
	je dana1
	cmp al,'A'
	je dana1
	cmp al,'b'
	je dana2
	cmp al,'B'
	je dana2
	jmp ktora_dana
backspace:
	ustaw_kursor 20,26
	mov al,""
	mov dl,al
	mov ah,2
	int 21h
	ustaw_kursor 20,26
	jmp ktora_dana
dana1:
	mov dl,al
	mov ah,2
	int 21h
spr_czy_dana1:
	mov ah,7
	int 21h
	cmp al,8 ;kod ascii backspace
	je backspace
	cmp al,27 ;kod ascii escape
	je wylacz
	cmp al,13 ;kod ascii enter
	jne spr_czy_dana1
	ustaw_kursor 20,29
	wprowadz_single_w_hex l1
	zmienne l1,z1,c1,m1
	call clrscr
	call ekran
	jmp ktora_dana
dana2:
	mov dl,al
	mov ah,2
	int 21h
spr_czy_dana2:
	mov ah,7
	int 21h
	cmp al,8 ;kod ascii backspace
	je backspace
	cmp al,27 ;kod ascii escape
	je wylacz
	cmp al,13 ;kod ascii enter
	jne spr_czy_dana2
	ustaw_kursor 20,29
	wprowadz_single_w_hex l2
	zmienne l2,z2,c2,m2
	call clrscr
	call ekran
	jmp ktora_dana

wynik:
	mov dl,al
	mov ah,2
	int 21h
spr_czy_wynik:
	mov ah,7
	int 21h
	cmp al,8 ;kod ascii backspace
	je backspace
	cmp al,27 ;kod ascii escape
	je wylacz
	cmp al,13 ;kod ascii enter
	jne spr_czy_wynik
	jmp gen_wyniku

;********************************************************

gen_wyniku:

porownaj_cechy:
	mov al,[c1]
	cmp al,[c2]
	jl cecha1_mniejsza
	je rowne	;znak wyniku!!!

cecha1_wieksza:
	clc
	rcr [m2],1
	rcr [m2+1],1
	rcr [m2+2],1
	add [c2],1
	mov al,[c2]
	cmp al,[c1]
	jne cecha1_wieksza
	mov al,[z1]
	mov [zw],al
	jmp obliczenia

cecha1_mniejsza:
	clc
	rcr [m1],1
	rcr [m1+1],1
	rcr [m1+2],1
	add [c1],1
	mov al,[c1]
	cmp al,[c2]
	jne cecha1_mniejsza
	mov al,[z2]
	mov [zw],al
	jmp obliczenia

rowne:			  ;ustalanie znaku wyniku gdy rowne cechy
	mov si,0
	mov cx,3
porownaj_3B_znak:
	mov al,[m1+si]
	cmp al,[m2+si]
	ja wieksza_m1_znak	;m1 wieksza od m2
	jb mniejsza_m1_znak	 ;m1 mnijesza od m2
	inc si
	loop porownaj_3B_znak
rowne_mantysy_znak:
	mov al,[z1]
	mov [zw],al
	cmp al,[z2]
	je dodaj
	jmp rowne_mantysy
mniejsza_m1_znak:
	mov al,[z2]
	mov [zw],al
	jmp obliczenia
wieksza_m1_znak:
	mov al,[z1]
	mov [zw],al

obliczenia:
	mov al,[c1]
	mov [cw],al

	mov al,[z1]
	cmp al,[z2]
	je dodaj

	mov si,0
	mov cx,3
porownaj_3B:
	mov al,[m1+si]
	cmp al,[m2+si]
	ja wieksza_m1  ;m1 wieksza od m2
	jb mniejsza_m1	;m1 mniejsza od m2
	inc si
	loop porownaj_3B
rowne_mantysy:
	mov [mw],0
	mov [mw+1],0
	mov [mw+2],0
	mov [cw],-127
	mov [zw],0
	jmp dalej
mniejsza_m1:
	odejmij_mantysy mw,m2,m1,cw
	jmp dalej
wieksza_m1:
	odejmij_mantysy mw,m1,m2,cw
	jmp dalej
dodaj:
	dodaj_mantysy mw,m1,m2,cw
dalej:
	mov al,[mw+2]
	mov [lw+3],al
	mov al,[mw+1]
	mov [lw+2],al
	mov al,[mw]
	shl al,1
	mov [lw+1],al
	mov al,[cw]
	add al,127
	mov [lw],al

	clc
	mov al,[zw]
	rcr al,1
	rcr [lw],1
	rcr [lw+1],1
;;;
;
	zmienne l1,z1,c1,m1
	zmienne l2,z2,c2,m2

	call clrscr
	call ekran

	jmp ktora_dana

wylacz:
	call clrscr
	mov ah,4ch
	int 21h

	
;*********************************************************************************************

clrscr:
	mov ah,0
	mov al,3
	int 10h
	ret

ekran:
	ustaw_kursor 3,5
	wyswietl txt1

	ustaw_kursor 5,7
	wyswietl txt2

	ustaw_kursor 7,5
	wyswietl txt3

	ustaw_kursor 12,5
	wyswietl txt4

	ustaw_kursor 20,5
	wyswietl txt5

	ustaw_kursor 3,15
	wyswietl_w_hex l1,4

	ustaw_kursor 7,15
	wyswietl_w_hex l2,4

	ustaw_kursor 12,15
	wyswietl_w_hex lw,4

	ustaw_kursor 20,26

	ret

prog ends
end start

;################################################