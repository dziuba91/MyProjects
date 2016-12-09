#ifndef __PLAYER_COMPONENT_H__
#define __PLAYER_COMPONENT_H__

#include "cocos2d.h"
#include "./ExtensionsProject.h"
#include "./GeneralGameElements.h"
#include "Definitions.h"

GAME_NS_BEGIN

/**
* the object is controlled by user (touch interaction)
* control collision with the player
* display 'Dialog Box' on end game (when collision happens)
*
* @Draw:
* - an object which represents current player position
* - additional graphics when touch interaction happens
*
* @Update:
* - position of the object based on defined movements ('Movement.h') 
*/
class PlayerComponent : public cocos2d::Component
{
public:
	static cocos2d::Component* create(std::vector<ext::Panel*>& touchPanel, cocos2d::Point startPosition);

	CREATE_FUNC(PlayerComponent);

private:
	void setParam(std::vector<ext::Panel*>& touchPanel, cocos2d::Point pos);

	virtual bool init();
	virtual void update(float dt);
	virtual void onEnter();
	virtual void onExit();
	void draw(float dt);

	bool onTouchBegan(cocos2d::Touch* touch, cocos2d::Event* event);
	void onTouchEnded(cocos2d::Touch* touch, cocos2d::Event* event); 
	bool onContactBegin(cocos2d::PhysicsContact& contact);

	// @Variables
	std::vector<ext::Panel*> _touchPanels; // * represents 'Panel' identificators need to control moments when player position is on 'Touch Panels'
	cocos2d::Point _startPosition; // * start position of the component ('player')
	cocos2d::Vec2 _touchPosition; // * touch moment begin position

	cocos2d::Sprite* _player; // * graphic which represent the player
	cocos2d::DrawNode* _drawPointNode; // * control drawing a part of the component graphics (touch point)
	cocos2d::DrawNode* _drawLineNode; // * control drawing a part of the component graphics

	ext::Movements* _movements; // * represents all player movements methods for the game

	//
	bool is_onTouch = false; // * control moment when will happen an interaction with the game by touch pad will happen
	bool is_fall = false; // * control moment when 'player' falling animation should happens (no interactions with the game)
	bool is_endGame = false; // * control end game moment; if 'true' all component actions should stop
};

GAME_NS_END

#endif // __PLAYER_COMPONENT_H__