======
README
======

-------------------------------------------
1) INTRODUCTION
-------------------------------------------
	* Project Name: Asteroids Game
	
	* Description: A simple arcade game
	
	* Author: Tomasz Dziuba
	
	* Project was finalized in: 18.10.2016
	
-------------------------------------------
2) GAMEPLAY
-------------------------------------------
	* The player controls an object which represents 'Spaceship'.
	
	* Periodically from the top, random sized, cubes objects (represents 'Asteroids') falls.
	
	* 'Asteroids' falls with a slight deviation in left/ right direction and have given random rotation.
	
	* The player can move in left/ right direction and shot.
	
	* Shot down an 'Asteroid' remove it from the screen, while 
		another objects will appear with greater frequency.
	
	* Collision 'Asteroid' and 'Spaceship' resets the state of the game.

-------------------------------------------
3) IMPLEMENTATION
-------------------------------------------
	* Programming language: C++
	
	* Graphics programming API: OpenGL
	
	* Development environment (IDE): Visual Studio 2015 (Community)

-------------------------------------------
4) PROJECT STRUCTURE
-------------------------------------------
	The project was divided into 2 logical parts.
	
	a) 'MyGameEngine': 
		* Project of my little game engine (only for Windows system).
		
		* Contains elements (classes, structures) necessary to accomplish simple game on Windows system, which e.g.:
			+ initialize and create the application,
			+ initiates the use of OpenGL (camera perspectives, lighting),
			+ control game 'main loop',
			+ process 'components' of the game.
			
		* 'MyGameEngine' project structure:
			-> MyGameEngine/
			-> MyGameEngine/Application.h
			-> MyGameEngine/Application.cpp
			-> MyGameEngine/Component.h
			-> MyGameEngine/Component.cpp
			-> MyGameEngine/Game.h
			-> MyGameEngine/Game.cpp
			-> MyGameEngine/Definitions.h
			-> MyGameEngine/Extensions/
			-> MyGameEngine/Extensions/Color.h
			-> MyGameEngine/Extensions/Color.cpp
			-> MyGameEngine/Extensions/Dimension.h
			-> MyGameEngine/Extensions/Dimension.cpp
			-> MyGameEngine/Extensions/Draw.h
			-> MyGameEngine/Extensions/Draw.cpp
			-> MyGameEngine/Extensions/Keys.h
			-> MyGameEngine/Extensions/Keys.cpp
			-> MyGameEngine/Extensions/Vector3D.h
			-> MyGameEngine/Extensions/Vector3D.cpp
			-> MyGameEngine/Tools/
			-> MyGameEngine/Tools/CollisionControl.h
			-> MyGameEngine/Tools/CollisionControl.cpp
			-> MyGameEngine/Tools/CollisionControl/
			-> MyGameEngine/Tools/CollisionControl/BoundingBox.h
			-> MyGameEngine/Tools/CollisionControl/BoundingBox.cpp
	
	b) 'GameComponents':
		* Includes components of the game and additional classes.
		
		* Implements gameplay of the game.
		
		* Struktura projektu:
			-> GameComponents/
			-> GameComponents/AsteroidsComponent.h
			-> GameComponents/AsteroidsComponent.cpp
			-> GamaComponents/PlayerComponent.h
			-> GameComponents/PlayerComponent.cpp
			-> GameComponents/ShootComponent.h
			-> GameComponents/ShootComponent.cpp
			-> GameComponents/Definitions.h
			-> GameComponents/AsteroidsComponent/
			-> GameComponents/AsteroidsComponent/Asteroid.h
			-> GameComponents/AsteroidsComponent/Asteroid.cpp
			-> GameComponents/PlayerComponent/
			-> GameComponents/PlayerComponent/Spaceship.h
			-> GameComponents/PlayerComponent/Spaceship.cpp
			-> GameComponents/ShootComponent/
			-> GameComponents/ShootComponent/Bullet.h
			-> GameComponents/ShootComponent/Bullet.cpp
	
-------------------------------------------
5) ADDITIONAL INFORMATION
-------------------------------------------
	* Game parameters (speed of movements/ shot, size of the objects, etc.) are easily
		customizable from the file: 'GameComponents / Definitions.h'.
	
	* The ideology of individual 'components' are explained in the code (comments).

-------------------------------------------
6) PROJECT CONFIGURATION ON VISUAL STUDIO
-------------------------------------------
	For execute the code in Visual Studio please create empty C++ project, 
	copy "Code" folder to the folder of created project and change default settings:
	
	* Link up OpenGL library:
		"Properties"\"Configuration Properties"\"Linker"\"Input"\"Additional Dependencies"
		+ write: opengl32.lib (or: opengl.lib)

	* Set up the visibility of the contents of "Code" folder:
		"Properties"\"Configuration Properties"\"C/C++"\"General"\"Additional Include Directories"
		+ write: Code

-------------------------------------------
7) DEFAULT KEY BINDINGS
-------------------------------------------
	* LEFT ARROW - left move
	
	* RIGHT ARROW - right move
	
	* SPACE - shooting
	
	* ESC - exit
	
-------------------------------------------
8) CONTACT INFORMATION
-------------------------------------------
	In case of any confusion, please contact me:
	
	* mail: tomasz.dziuba91@gmail.com
	