#ifndef __SPIKESBORDER_COMPONENT_H__
#define __SPIKESBORDER_COMPONENT_H__

#include "cocos2d.h"
#include "Definitions.h"

GAME_NS_BEGIN

/**
* an obstacle of the game
*
* @Draw:
* - object representing border of the window
*
* @Update:
* - control collision with the player ('PlayerComponent')
*/
class SpikesBorderComponent : public cocos2d::Component
{
public:
	CREATE_FUNC(SpikesBorderComponent);

private:
	virtual bool init();
	virtual void onEnter();
};

GAME_NS_END

#endif // __SPIKESBORDER_COMPONENT_H__