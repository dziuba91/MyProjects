#include "Game.h"

#include <windows.h>
#include <gl/GL.h>

#include <iostream>
#include <ctime>

USING_NS_ENGINE;

Game* Game::_game;

std::vector<Component*> Game::_gameComponents;

bool Game::_isOnResetAllComponents = false;

Game* Game::create(int width, int height, char* title)
{
	_game = new Game();

	_game->createWindow(width, height, title);

	return _game;
}

void Game::run()
{	
	for (auto tmp : _gameComponents) tmp->init();

	// time control, need for adapt default FPS and synchronize an game actions 
	clock_t currentTime = 0;
	clock_t lastFrameTime = 0;
	
	int count = 0;
	WORD myTime = 0;

	MSG msg;											// 

	while (true)
	{
		if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))	// is there a message waiting?
		{
			if (msg.message == WM_QUIT)					// have we received a quit message?
			{
				break;
			}
			else										// if not, deal with Window messages
			{
				TranslateMessage(&msg);					// translate the message
				DispatchMessage(&msg);					// dispatch the message
			}
		}
		else		 									// if there are no messages
		{
			// draw the scene, watch for ESC key and quit messages, controll game parametrs
			if (_isActive)								// program active?
			{
				if (isKeyDown(Keys::ESC))				// was ESC pressed?
				{
					break;								// ESC signalled a quit
				}
				else if (_isOnResetAllComponents)		// reset parametrs of all components when the flag is 'true'
				{
					for (auto tmp : _gameComponents) tmp->reset();

					_isOnResetAllComponents = false;
				}
				else									// perform game 'Components' actions
				{
					// draw elements with default number of 'frames per second'
					currentTime = std::clock();
					if (static_cast<double>(currentTime - lastFrameTime)/CLOCKS_PER_SEC >= (1.0/FPS))
					{
						lastFrameTime = currentTime;

						count++;

						// update 'components'
						for (auto tmp : _gameComponents) tmp->update();

						// draw frame
						glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

						for (auto tmp : _gameComponents) tmp->draw();

						SwapBuffers(_hDC);				// swap buffers (double buffering)
					}

					if (static_cast<double>(currentTime - myTime) / CLOCKS_PER_SEC > 1.0)
					{
						std::cout << count << std::endl;
						myTime = currentTime;
						count = 0;
					}
				}
			}
		}
	}

	this->onExit();
}

void Game::addComponent(Component* component)
{
	_gameComponents.push_back(component);
}

Component* Game::getComponent(unsigned int id)
{
	for (auto tmp : _gameComponents) 
		if (id && tmp->isComponentID(id)) return tmp;

	return nullptr;
}

void Game::resetAllComponents()
{
	_isOnResetAllComponents = true;
}

void Game::onExit()
{
	for (auto tmp : _gameComponents)
	{
		tmp->onExit();

		delete tmp;
	}

	delete _game;

	killWindow();
}