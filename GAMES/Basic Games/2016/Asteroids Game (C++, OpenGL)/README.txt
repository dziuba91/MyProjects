===========
README [PL]
===========

---------------------------------
1) PODSTAWOWE INFORMACJE
---------------------------------
	* Nazwa projektu: Asteroids Game
	
	* Opis: Prosta gra zręcznościowa
	
	* Autor: Tomasz Dziuba
	
	* Data wykonania: 18.10.2016
	
---------------------------------
2) GAMEPLAY
---------------------------------
	* gracz steruje obiektem reprezentującym 'statek kosmiczny'
	
	* co jakiś czas z góry spadają prostopadłościany (o losowym rozmiarze) reprezentujące 'asteroidy'
	
	* gracz może poruszać się w lewo/ prawo oraz strzelać

	* 'asteroidy' spadają z lekkim odchyleniem w prawo lub lewo oraz mają nadaną losową rotację
	
	* w przypadku zestrzelenia 'asteroidu' zostaje on usunięty z ekranu, 
		natomiast kolejne obiekty będą pojawiać się z większą częstotliwością
	
	* kolizja 'asteroidu' ze 'statkiem kosmicznym' resetuje stan gry

---------------------------------
3) STEROWANIE
---------------------------------
	* LEWO (strzałka) - ruch w lewo
	
	* PRAWO (strzałka) - ruch w prawo
	
	* SPACJA - oddanie strzału
	
	* ESC - wyjście z aplikacji

---------------------------------
4) WYKONANIE
---------------------------------
	* Język: C++
	
	* Graficzne API: OpenGL
	
	* Środowisko programistyczne: Visual Studio 2015 (Community)

---------------------------------
5) STRUKTURA PROJEKTU
---------------------------------
	Projekt został podzielony na 2 logiczne części.
	
	a) 'MyGameEngine': 
		* Mini projekt mojego małego silnika gry
		
		* Zawiera elementy (klasy, struktury) niezbędne do wykonania tego zadania, m.in.:
			-> inicjalizuje i tworzy okno aplikacji (funkcje Win API)
			-> inicjuje użycie OpenGL (perspektywa kamery, oświetlenia)
			-> zarządza główną 'pętlą gry'
			-> przetwarza 'komponenty gry'
			
		* Struktura projektu:
			-> MyGameEngine/	 							// główny folder dla projektu silnika
			-> MyGameEngine/Application.h
			-> MyGameEngine/Application.cpp
			-> MyGameEngine/Component.h
			-> MyGameEngine/Component.cpp
			-> MyGameEngine/Game.h
			-> MyGameEngine/Game.cpp
			-> MyGameEngine/Definitions.h
			-> MyGameEngine/Extensions/						// dodatkowe typy danych i funkcje zwiększające przejrzystość kodu
			-> MyGameEngine/Extensions/Color.h
			-> MyGameEngine/Extensions/Color.cpp
			-> MyGameEngine/Extensions/Dimension.h
			-> MyGameEngine/Extensions/Dimension.cpp
			-> MyGameEngine/Extensions/Draw.h				// funkcje rysujące obiekty na ekranie (sześcany) wykorzystujące OpenGL
			-> MyGameEngine/Extensions/Draw.cpp
			-> MyGameEngine/Extensions/Keys.h
			-> MyGameEngine/Extensions/Keys.cpp
			-> MyGameEngine/Extensions/Vector3D.h
			-> MyGameEngine/Extensions/Vector3D.cpp
			-> MyGameEngine/Tools/							// narzędzia umożliwiające kontrolę kolizji
			-> MyGameEngine/Tools/CollisionControl.h
			-> MyGameEngine/Tools/CollisionControl.cpp
			-> MyGameEngine/Tools/CollisionControl/			// elementy związane z narzędziem 'Collision Control'
			-> MyGameEngine/Tools/CollisionControl/BoundingBox.h
			-> MyGameEngine/Tools/CollisionControl/BoundingBox.cpp
	
	b) 'GameComponents':
		* Zawiera komponenty gry oraz dodatkowe klasy
		
		* Implementuje 'gameplay' gry
		
		* Struktura projektu:
			-> GameComponents/	 							// główny folder dla projektu zawierającego komponenty gry
			-> GameComponents/AsteroidsComponent.h
			-> GameComponents/AsteroidsComponent.cpp
			-> GamaComponents/PlayerComponent.h
			-> GameComponents/PlayerComponent.cpp
			-> GameComponents/ShootComponent.h
			-> GameComponents/ShootComponent.cpp
			-> GameComponents/Definitions.h
			-> GameComponents/AsteroidsComponent/			// elementy (klasy) związane z komponentem 'Asteroids Component'
			-> GameComponents/AsteroidsComponent/Asteroid.h
			-> GameComponents/AsteroidsComponent/Asteroid.cpp
			-> GameComponents/PlayerComponent/				// elementy (klasy) związane z komponentem 'Player Component'
			-> GameComponents/PlayerComponent/Spaceship.h
			-> GameComponents/PlayerComponent/Spaceship.cpp
			-> GameComponents/ShootComponent/				// elementy (klasy) związane z komponentem 'Shoot Component'
			-> GameComponents/ShootComponent/Bullet.h
			-> GameComponents/ShootComponent/Bullet.cpp
	
---------------------------------
6) DODATKOWE INFORMACJE
---------------------------------
	* Parametry gry (szybkość ruchu/ strzału, wielkości obiektów, itp.) są łatwo 
		modyfikowalne z pliku 'GameComponents/Definitions.h'
	
	* Ideologia poszczególnych elementów została wyjaśniona w kodzie

	* Komentarze w kodzie standardowo w języku Angielskim

---------------------------------
7) KONFIGURACJA W VISUAL STUDIO
---------------------------------
	Aby uruchomić grę w środowisku Visual Studio należy stworzyć pusty projekt C++ oraz przekopiować 
	cały folder 'Code' do folderu utworzonego projektu i zmienić domyślne ustawienia projektu w VS:
	
	* Zalinkować bibliotekę OpenGL (przy założeniu że jest zainstalowana w systemie):
		"Properties"\"Configuration Properties"\"Linker"\"Input"\"Additional Dependencies"
		-> i dopisać do okienka: opengl32.lib (lub: opengl.lib)

	* Ustawić widoczność zawartości folderu "Code" z dowolnego miejsca w projekcie:
		"Properties"\"Configuration Properties"\"C/C++"\"General"\"Additional Include Directories"
		-> i w okienku wpisać: Code

---------------------------------
8) INFORMACJE KONTAKTOWE
---------------------------------
	W razie jakichkolwiek niejasności proszę o kontakt mailowy.
	
	* mail: tomasz.dziuba91@gmail.com
	