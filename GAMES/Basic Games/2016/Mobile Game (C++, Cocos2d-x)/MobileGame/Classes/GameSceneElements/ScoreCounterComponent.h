#ifndef __SCORECOUNTER_COMPONENT_H__
#define __SCORECOUNTER_COMPONENT_H__

#include "cocos2d.h"
#include "Definitions.h"

GAME_NS_BEGIN

/**
* control and display 'score' parameter of the game
*
* @Draw:
* - layout and text need to show the game score
*
* @Update:
* - 'score' parameter when the player ('PlayerComponent') pass through the gate ('PassGateComponent')
*/
class ScoreCounterComponent : public cocos2d::Component
{
public:
	CREATE_FUNC(ScoreCounterComponent);

	int getScore();

private:
	virtual bool init();
	virtual void onEnter();
	
	bool onContactBegin(cocos2d::PhysicsContact& contact);

	// @Variables:
	cocos2d::Label* _scoreLabel; 

	int _score = 0; // * conrol score of the game
};

GAME_NS_END

#endif // __SCORECOUNTER_COMPONENT_H__