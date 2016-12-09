#ifndef __TOUCHPANEL_COMPONENT_H__
#define __TOUCHPANEL_COMPONENT_H__

#include "cocos2d.h"
#include "./ExtensionsProject.h"
#include "Definitions.h"

GAME_NS_BEGIN

/**
* define 'Touch Panel' objects - rectangles on the right and left side of window of the game 
* allow touch interactions when 'player' ('PlayerComponent') position is on 'Touch Panel' area
* 'Panel' class ('Panel.h') represents main properties of the component
*
* @Draw:
* - rectangle which represents the 'Touch Panel' based on 'Panel' class ('Panel.h') parameters (size and position)
*
* @Update:
* - activity of the object to touch interactions ('is_active' parameter)
* - color of the object depends of activity
*/
class TouchPanelComponent : public cocos2d::Component
{
public:
	static cocos2d::Component* create(ext::Panel* info, bool active);

	CREATE_FUNC(TouchPanelComponent);

	/** @Return true: position ('pos') belongs to 'Touch Panel' area */
	bool isOnTouchPanel(cocos2d::Vec2 pos);

private:
	void setParam(bool active, ext::Panel* info);

	virtual void update(float dt);
	virtual void onEnter();

	bool onContactBegin(cocos2d::PhysicsContact& contact);
	bool onContactSeparate(cocos2d::PhysicsContact& contact);

	ext::Panel* _panel; // * parameters and informations need to properly display the component 

	cocos2d::DrawNode* _drawNode; // * need to draw the component
	 
	bool is_active; // * allow interaction by touch actions with the component ('Touch Panel')
};

GAME_NS_END

#endif // __TOUCHPANEL_COMPONENT_H__