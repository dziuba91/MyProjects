#ifndef __PASSGATE_COMPONENT_H__
#define __PASSGATE_COMPONENT_H__

#include "cocos2d.h"
#include "Definitions.h"

GAME_NS_BEGIN

/**
* define object at the center of the window
* control passing the player beetwen 'Touch Area' ('TouchAreaComponent')
*
* @Draw:
* - bottom and top objects are an obstacle of the game
*
* @Update:
* - position of the 'Y' coordinates change after the player pass through the gate
*/
class PassGateComponent : public cocos2d::Component
{
public:
	CREATE_FUNC(PassGateComponent);
	
private:
	virtual bool init();
	virtual void onEnter();

	bool onContactSeparate(cocos2d::PhysicsContact& contact);

	/** select next center position of the object */
	int nextPosY();

	cocos2d::Node* _mainNode; // * control component position; center of the 'pass gate'

	cocos2d::Size _visibleSize; // * size of the game window

	int _collisionPointX = 0; // * save position of the 'Player Component' when collision happen; need to check if 'Player' fully passed the 'Gate' 
};

GAME_NS_END

#endif // __PASSGATE_COMPONENT_H__