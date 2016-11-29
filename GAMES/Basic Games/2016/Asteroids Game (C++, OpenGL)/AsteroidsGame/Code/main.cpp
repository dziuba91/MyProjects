/**
* @Project: Asteroids Game
*
* @Author: Tomasz Dziuba
* @E-mail: tomasz.dziuba91@gmail.com
*/

#include <windows.h>

#include "MyGameEngine.h"
#include "GameComponents.h" 

USING_NS_ENGINE;

int WINAPI WinMain(HINSTANCE hInstance,			// Instance
	HINSTANCE hPrevInstance,					// Previous Instance
	LPSTR lpCmdLine,							// Command Line Parameters
	int nCmdShow)
{
	Game* game = Game::create(480, 480, "Asteroids");
	
	game->addComponent(new PlayerComponent());
	game->addComponent(new ShootComponent());
	game->addComponent(new AsteroidsComponent());

	game->run();

	return 0;
}