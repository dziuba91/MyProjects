===========
README [PL]
===========

---------------------------------
1) PODSTAWOWE INFORMACJE
---------------------------------
	* Nazwa projektu: Mobile Game
	
	* Opis: Prosta gra zręcznościowa na urządzenia mobilne
	
	* Prezentacja wideo na YouTube: youtu.be/trvLDEXFzUA
	
	* Autor: Tomasz Dziuba
	
	* Data wykonania: 20.09.2016
	
---------------------------------
2) GAMEPLAY
---------------------------------
	* gracz steruje okrągłym obiektem
	
	* scena podzialona jest na dwa pola (panele) które umożliwiająją interakcję poprzez kliknięcie
	
	* panele oddzielone są przeszkodą (bramą) z wąskim przejściem (w centrum sceny)
	
	* zadaniem gracza jest przerzucenie obiektu z jednego panelu na drugi przez przejście w bramie

	* każdy przerzut obiektu jest punktowany
	
	* po bezkolizyjnym przerzucie zmienia się pozycja przejścia w bramie
	
	* obiekt w trakcie interrakcji (kliknięcia) porusza się ruchem wahadła matematycznego
	
	* gdy interakcja zakończy się obiekt porusza się z swobodnie z odpowiednią nadaną prędkością
	
	* kolizja z bramą lub z obramowaniem otaczjącym scenę kończy rozgrywkę

---------------------------------
3) WYKONANIE
---------------------------------
	* Język: C++
	
	* Silnik gry: Cocos2d-x (v3.10)
	
	* Środowisko programistyczne: Visual Studio 2015 (Community)
	
	* Dodatkowe narzędzia: Cocos Studio

---------------------------------
4) STRUKTURA PROJEKTU
---------------------------------
	* 'Początek kodu' zdefiniowany jest w plikach:
		-> AppDelegate.h
		-> AppDelegate.cpp
	
	* Funkcjonalność poszczególnych scen gry zdefiniowane są w plikach:
		-> GameScene.h
		-> GameScene.cpp
		-> MainMenuScene.h
		-> MainMenuScene.cpp
	
	* Pozostała część projektu została podzielona na 3 części.
	
		a) 'GameSceneElements': 
			* elementy gry (Komponenty) zaprojektowane dla głównej sceny:
			
			* Struktura projektu:
				-> GameSceneElements/
				-> GameSceneElements/PassGateComponent.h
				-> GameSceneElements/PassGateComponent.cpp
				-> GameSceneElements/PlayerComponent.h
				-> GameSceneElements/PlayerComponent.cpp
				-> GameSceneElements/ScoreCounterComponent.h
				-> GameSceneElements/ScoreCounterComponent.cpp
				-> GameSceneElements/SpikesBorderComponent.h
				-> GameSceneElements/SpikesBorderComponent.cpp
				-> GameSceneElements/TouchPanelComponent.h
				-> GameSceneElements/TouchPanelComponent.cpp
				-> GameSceneElements/Definitions.h
	
		b) 'GeneralGameElements':
			* elementy gry wspólne dla całego projektu (mogą być dostępne i uruchamiane z poziomu dowolnej sceny)

			* Struktura projektu:
				-> GeneralGameElements/
				-> GeneralGameElements/MyDialogBox.h
				-> GeneralGameElements/MyDialogBox.cpp
				-> GeneralGameElements/Definitions.h
	
		c) 'ExtensionsProject': 
			* dodatkowe elementy (głównie struktury) zwiększające ogólną logikę:
			
			* Struktura projektu:
				-> ExtensionsProject/
				-> ExtensionsProject/Color.h
				-> ExtensionsProject/Color.cpp
				-> ExtensionsProject/Movements.h
				-> ExtensionsProject/Movements.cpp
				-> ExtensionsProject/Panel.h
				-> ExtensionsProject/Panel.cpp
				-> ExtensionsProject/Definitions.h
			
---------------------------------
5) DODATKOWE INFORMACJE
---------------------------------
	* Parametry gry (np. wielkości obiektów) są łatwo modyfikowalne w plikach 'Definitions.h' w poszczególnych częściach projektu
	
	* Ideologia poszczególnych elementów została wyjaśniona w kodzie

	* Komentarze w kodzie standardowo w języku Angielskim

---------------------------------
6) URUCHOMIENIE PROJEKTU
---------------------------------
	* Najłatwiejszym sposobem jest wykorzystać narzędzie 'Cocos Studio', utworzyć pusty projekt 
		i podmienić zawartość folderu 'Classes'
	
	* W przypadku uruchamiania na urządzenia mobilne konieczne może być skonfigurowanie dodatkowo
		pliku 'Android.mk' (plik typu 'makefile')

---------------------------------
7) INFORMACJE KONTAKTOWE
---------------------------------
	W razie jakichkolwiek niejasności proszę o kontakt mailowy.
	
	* mail: tomasz.dziuba91@gmail.com
	