#ifndef __MYGAMEENGINE_GAME_H__
#define __MYGAMEENGINE_GAME_H__

#include <vector>

#include "Definitions.h"
#include "Application.h"
#include "Component.h"
#include "Extensions/Keys.h"

ENGINE_NS_BEGIN

/**
* the most important class if we want use the 'Game Engine' ('MyGameEngine.h')
* create the game
* control game 'Main Loop'
* control game 'Components' operations
*/
class Game : Application
{
public:
	static Game* create(int width, int height, char* title);

	/** perform game main loop until exit */
	void run();

	void addComponent(Component* component);

	/** allow to access selected 'component' by 'id' parameter
	* selected component must have properly set the 'ID' with unique value
	* @Return: pointer to the selected 'Component' object
	*/
	static Component* getComponent(unsigned int id);

	/** allow to perform 'reset()' function on all Components */
	static void resetAllComponents();

private:
	void onExit();

	//
	static Game* _game;

	static std::vector<Component*> _gameComponents;
	
	static bool _isOnResetAllComponents; // * need for 'resetAllComponents()' function properly working
};

ENGINE_NS_END

#endif // __MYGAMEENGINE_GAME_H__