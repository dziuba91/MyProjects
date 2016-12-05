format binary
org 100h

macro mnozenie A,B,l,W
{
	mov si,l

	mov al,[A+2]
	mov bl,[B+si]
	mul bl
	mov [W+5],al
	mov [W+4],ah

	mov al,[A+1]
	mov bl,[B+si]
	mul bl
	mov [W+3],ah
	clc
	add [W+4],al
	adc [W+3],0

	mov al,[A]
	mov bl,[B+si]
	mul bl
	clc
	add [W+3],al
	adc [W+2],ah
}

macro wyswietl_w_hex A,l
{
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
}

macro mnozenie_mantys i1, i2, i3
{
local et1
local et2
local et3
local et4
local et5
local et6
local et7

	mov cl,[mantysa1+i1]
	mov [pomoc],0
et1:
	mov al,[mantysa2+i2]
	add al,[pomoc]
	jno et4
	sub al,10
	mov [pomoc],al
	push ax
	push cx
	mov cl,i3
	mov si,mantysaw+i3-1
et5:
	mov bl,[si]
	inc bl
	jno et3
et6:
	mov [si],bl
	dec si
	loop et5
	jmp gwiazdki
et3:
	mov [si],bl
et7:
	pop cx
	pop ax
et4:
	mov [pomoc],al
et2:
	loop et1
	mov bl,[pomoc]
	mov [mantysaw+i3],bl
}

macro wypisz txt, x, y
{
	kursor x, y
	mov ah, 9
	mov dx, txt
	int 21h
}

macro kursor x, y
{
	mov ah, 2
	mov bh, 0
	mov dh, x
	mov dl, y
	int 10h
}

macro zeruj_liczbe zmienna, ile
{
local etyk7

	mov si, zmienna
	mov bl,0
	mov cx, [ile]
etyk7:
	mov [si], bl
	inc si
	loop etyk7
}

macro czytaj_liczbe liczbax, x
{
	kursor 6, 18
	call czytaj_jeden_znak
	mov [liczbax], al

	call czytaj_jeden_znak
	mov [liczbax+1], al

	call czytaj_jeden_znak
	mov [liczbax+2], al

	call czytaj_jeden_znak
	mov [liczbax+3], al

	call czytaj_jeden_znak
	mov [liczbax+4], al

	call czytaj_jeden_znak
	mov [liczbax+5], al

	call czytaj_jeden_znak
	mov [liczbax+6], al

	call czytaj_jeden_znak
	mov [liczbax+7], al

	wyswietl_liczbe liczbax, x
}

macro wyswietl_liczbe zmienna, x
{
local litera_duza
local wyswietlaj
local dalej

	mov cx, 8
	mov si, 0
	mov [kolumna], 14

wyswietlaj:
	mov al, [zmienna+si]

	cmp al, 9
	ja litera_duza
	add al, 48
	jmp dalej

litera_duza:
	add al, 55

dalej:
	inc [kolumna]
	kursor x, [kolumna]
	wyswietl_znak
	inc si
	loop wyswietlaj
}

macro wyswietl_znak
{
	mov ah, 2
	mov dl, al
	int 21h
}

macro ustal_znak liczbax, znakx
{
local etyk1
local etyk2

	rcl [liczbax],5
	jnc etyk1
	sub [znakx],1				; znakx = -1
	jmp etyk2
etyk1:
	inc [znakx]					; znakx = 1
etyk2:
	rcr [liczbax],5
	; w [znakx] wartosc: 1 lub -1
}

macro oblicz_ceche liczbax, cechax
{
	mov al,[liczbax]			; al =	0000xxxx
	shl al,5					; al =	xxx00000
	mov bl,[liczbax+1]			; bl =	0000yyyy
	shl bl,1					; bl =	000yyyy0
	or al,bl					; al =	xxxyyyy0
	mov bl,[liczbax+2]			; bl =	0000zzzz
	shr bl,3					; bl =	0000000z
	or al,bl
	sub al,127
	mov [cechax],al
}

macro oblicz_mantyse liczbax, mantysax
{
	mov al,[liczbax+2]			; al = 0000xxxx
	and al,07h					; al = 00000xxx
	shl al,4					; al = 0xxx0000
	mov bl,[liczbax+3]			; bl = 0000yyyy
	or al,bl					; al = 0xxxyyyy
	or al,80h
	mov [mantysax],al

	mov al,[liczbax+4]			; al = 0000uuuu
	shl al,4					; al = uuuu0000
	mov bl,[liczbax+5]			; bl = 0000vvvv
	or al,bl					; al = uuuuvvvv
	mov [mantysax+1],al

	mov al,[liczbax+6]			; al = 0000wwww
	shl al,4					; al = wwww0000
	mov bl,[liczbax+7]			; bl = 0000zzzz
	or al,bl					; al = wwwwzzzz
	mov [mantysax+2],al
}

start:
	wypisz txt1, 0, 6
	wypisz txt2, 1, 8
	wypisz txt3, 2, 6
	wypisz txt4, 3, 8
	wypisz txt5, 4, 6
	wypisz txt6, 6, 0

czytaj_polecenie:
	kursor 6, 16
	mov ah, 7
	int 21h

	wyswietl_znak

	cmp al, 'q'
	je koniec
	cmp al, 'Q'
	je koniec
	cmp al, 'l'
	je pierwsza_liczba
	cmp al, 'L'
	je pierwsza_liczba
	cmp al, 'p'
	je druga_liczba
	cmp al, 'P'
	je druga_liczba
	cmp al, 'w'
	je wykonaj
	cmp al, 'W'
	je wykonaj

	jmp czytaj_polecenie

pierwsza_liczba:
	zeruj_liczbe liczba1, 8
	czytaj_liczbe liczba1, 0
	wypisz txt7, 6, 18
	jmp czytaj_polecenie

druga_liczba:
	zeruj_liczbe liczba2, 8
	czytaj_liczbe liczba2, 2
	wypisz txt7, 6, 18
	jmp czytaj_polecenie

;
wykonaj:

sprawdz_czy_zero:
	mov si,0
	mov cx,8
porownaj_8B_liczba1:
	mov al,[liczba1+si]
	cmp al,0
	jne druga_liczba_czy_zero
	inc si
	loop porownaj_8B_liczba1

	mov [liczbaw],0
	mov [liczbaw+1],0
	mov [liczbaw+2],0
	mov [liczbaw+3],0
	jmp wyswietl

druga_liczba_czy_zero:
	mov si,0
	mov cx,8
porownaj_8B_liczba2:
	mov al,[liczba2+si]
	cmp al,0
	jne mnozenie_liczby
	inc si
	loop porownaj_8B_liczba2

	mov [liczbaw],0
	mov [liczbaw+1],0
	mov [liczbaw+2],0
	mov [liczbaw+3],0
	jmp wyswietl

mnozenie_liczby:
	ustal_znak liczba1, znak1
	ustal_znak liczba2, znak2

	oblicz_ceche liczba1, cecha1
	oblicz_ceche liczba2, cecha2

	oblicz_mantyse liczba1, mantysa1
	oblicz_mantyse liczba2, mantysa2

	mnozenie mantysa1,mantysa2,2,man1
	mnozenie mantysa1,mantysa2,1,man2
	mnozenie mantysa1,mantysa2,0,man3

	mov cx,8
przesun1:
	shl [man2+5],1
	rcl [man2+4],1
	rcl [man2+3],1
	rcl [man2+2],1
	rcl [man2+1],1
	rcl [man2],1
	loop przesun1

	mov cx,16
przesun2:
	shl [man3+5],1
	rcl [man3+4],1
	rcl [man3+3],1
	rcl [man3+2],1
	rcl [man3+1],1
	rcl [man3],1
	loop przesun2

dodaj1:
	clc
	mov al,[man1+5]
	add [man2+5],al
	mov al,[man1+4]
	adc [man2+4],al
	mov al,[man1+3]
	adc [man2+3],al
	mov al,[man1+2]
	adc [man2+2],al
	mov al,[man1+1]
	adc [man2+1],al
	mov al,[man1]
	adc [man2],al

dodaj2:
	clc
	mov al,[man2+5]
	add [man3+5],al
	mov al,[man2+4]
	adc [man3+4],al
	mov al,[man2+3]
	adc [man3+3],al
	mov al,[man2+2]
	adc [man3+2],al
	mov al,[man2+1]
	adc [man3+1],al
	mov al,[man2]
	adc [man3],al

	mov al,[man3+5]
	mov [mantysaw+5],al
	mov al,[man3+4]
	mov [mantysaw+4],al
	mov al,[man3+3]
	mov [mantysaw+3],al
	mov al,[man3+2]
	mov [mantysaw+2],al
	mov al,[man3+1]
	mov [mantysaw+1],al
	mov al,[man3]
	mov [mantysaw],al

	call dodaj_cechy

	clc
optymalizacja_mantysy:
	sub [cechaw],1
	shl [mantysaw+5],1
	rcl [mantysaw+4],1
	rcl [mantysaw+3],1
	rcl [mantysaw+2],1
	rcl [mantysaw+1],1
	rcl [mantysaw],1
	jnc optymalizacja_mantysy
	rcr [mantysaw],1
	rcr [mantysaw+1],1
	rcr [mantysaw+2],1
	rcr [mantysaw+3],1
	rcr [mantysaw+4],1
	rcr [mantysaw+5],1
	add [cechaw],2

	call ustal_znak_liczby

dalej:		;"sklejanie" wyniku
	mov al,[mantysaw+2]
	mov [liczbaw+3],al
	mov al,[mantysaw+1]
	mov [liczbaw+2],al
	mov al,[mantysaw]
	shl al,1
	mov [liczbaw+1],al
	mov al,[cechaw]
	add al,127
	mov [liczbaw],al

	clc
	mov al,[znakw]
	rcr al,1
	rcr [liczbaw],1
	rcr [liczbaw+1],1

wyswietl:
	call wyswietl_wynik

	jmp czytaj_polecenie

;
dodaj_cechy:
	mov al,[cecha1]
	mov bl,[cecha2]
	add al,bl
	mov [cechaw],al
	ret

;
czytaj_jeden_znak:
	mov ah, 7
	int 21h
	cmp al, '0'
	jb czytaj_jeden_znak

	;znak wiêkszy lub rowny '0'
	cmp al, '9'
	jbe cyfra

	;znak wiekszy od '9'
	cmp al, 'A'
	jb czytaj_jeden_znak

	;znak wiekszy lub rowny 'A'
	cmp al, 'F'
	jbe duza_litera

	;znak wiekszy od 'F'
	cmp al, 'a'
	jb czytaj_jeden_znak

	;znak wiekszy lub rowny 'a'
	cmp al, 'f'
	ja czytaj_jeden_znak
	jmp mala_litera
cyfra:
	push ax
	wyswietl_znak
	pop ax
	sub al, 48
	jmp koniec_czytania

duza_litera:
	push ax
	wyswietl_znak
	pop ax
	sub al, 55
	jmp koniec_czytania

mala_litera:
	push ax
	wyswietl_znak
	pop ax
	sub al, 87

koniec_czytania:
	ret

;
usun_stara_hex:
	kursor 6, 17
	mov ah, 9
	mov dx, txt7
	int 21h
	ret

;
gwiazdki:
	wypisz txt8, 4, 14
	wypisz txt7, 6, 15
	jmp czytaj_polecenie

wyswietl_wynik:
	kursor 4,15
	wyswietl_w_hex liczbaw,4
	ret

;
ustal_znak_liczby:
	mov al,[znak1]
	cmp al,[znak2]
	jne minus
	mov [znakw],0
	jmp powrot
minus:
	mov [znakw],1
powrot:
	ret
	
;
wyswietl_hex:
	mov al,[pomoc]						;pierwsza cyfra hex	  10111001
	shr al,4							; 00001011 = 11 = Bh
	cmp al,10
	jb ety4								; Skok, jezeli AL < 10
	add al,55 							; litera "B"
	jmp ety5
ety4:
	add al,30h
ety5:
	mov dl,al
	mov ah,2
	int 21h								; wyswietl cyfre/litere

	mov al,[pomoc] 						; druga cyfra hex - 10111001
	and al,0Fh							; 10111001 AND 00001111 = 00001001
	cmp al,10
	jb ety6								; Skok, jezeli AL < 10
	add al,55 							; kody ascii liter
	jmp ety7
   ety6:
	add al,30h							; cyfra "9"
   ety7:
	mov dl,al
	mov ah,2
	int 21h
	ret

;
koniec:
	mov ax, 4C00h
	int 21h

; ###################################
Dana:
	txt1	   db "Dana1 = $"
	txt2	   db "*$"
	txt3	   db "Dana2 = $"
	txt4	   db "=$"
	txt5	   db "Wynik = $"
	txt6	   db "Linia polecen = $"
	txt7	   db "						  $"
	txt8	   db "*****************$"
	txt9	   db "2^$"
	txtm	   db "-$"
	txtp	   db "+$"
	liczba1	   db 0,0,0,0,0,0,0,0
	liczba2	   db 0,0,0,0,0,0,0,0
	liczbaw	   db 0,0,0,0
	mantysa1   db 0,0,0
	mantysa2   db 0,0,0
	mantysaw   db 0,0,0,0,0,0
	man1	   db 0,0,0,0,0,0
	man2	   db 0,0,0,0,0,0
	man3	   db 0,0,0,0,0,0
	cecha1	   db 0
	cecha2	   db 0
	cechaw	   db 0
	znak1	   db 0
	znak2	   db 0
	znakw	   db 0
	kolumna	   db 0
	pomoc	   db 0

; x1 = (-1)^z1 * M1 * B^C1
; x2 = (-1)^z2 * M2 * B^C2
; x1 * x2 = ((-1)^z1 * (-1)^z2) * (M1 * M2) * 2^(C1+C2)
