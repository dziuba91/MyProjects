#ifndef __PLAYER_COMPONENT_H__
#define __PLAYER_COMPONENT_H__

#include "MyGameEngine.h"

#include "PlayerComponent/Spaceship.h"

/**
* 'Component' controlled by the player
*
* @Draw: model represents the 'Spaceship'
* @Update: model position
*/
class PlayerComponent : public MyGameEngine::Component
{
public:
	virtual void init();
	virtual void draw();
	virtual void update();
	virtual void reset();
	virtual void onExit();

	/** need for synchronization an operations beetwen components (e.g.: models 'position', 'size')
	* @Return: 'Spaceship' object (which inherits 'BoundingBox' class) 
	*/
	Spaceship* getSpaceship();

private:
	Spaceship* _spaceship;
};

#endif // __PLAYER_COMPONENT_H__