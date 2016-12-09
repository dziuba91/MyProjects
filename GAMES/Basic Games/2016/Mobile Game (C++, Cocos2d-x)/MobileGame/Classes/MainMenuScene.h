#ifndef __MAINMENU_SCENE_H__
#define __MAINMENU_SCENE_H__

#include "cocos2d.h"

/**
* main menu of the game
* replace the scene after user interaction
*/
class MainMenuScene : public cocos2d::Layer
{
public:
	static cocos2d::Scene* createScene();

	CREATE_FUNC(MainMenuScene);

private:
	virtual bool init();

	void onKeyReleased(cocos2d::EventKeyboard::KeyCode keycode, cocos2d::Event* e);

	/** replace the scene to 'GameScene' (need for 'cocos2d' callback method) */
	void goToGameScene(cocos2d::Ref* sender);
};

#endif // __MAINMENU_SCENE_H__