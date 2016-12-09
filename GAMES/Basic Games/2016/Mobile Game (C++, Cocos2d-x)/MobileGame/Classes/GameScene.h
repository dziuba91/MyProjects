#ifndef __MAINGAME_SCENE_H__
#define __MAINGAME_SCENE_H__

#include "cocos2d.h"

#include "ExtensionsProject.h"
#include "GameSceneElements.h"

/**
* main scene of the game
* implements designed components to the scene
*/
class GameScene : public cocos2d::Layer
{
public:
	static cocos2d::Scene* createScene();

	CREATE_FUNC(GameScene);

	/** show again 'GameScene' from beginning */
	void goToGameScene(cocos2d::Ref* sender);

	/** replace scene to 'MainMenuScene' */
	void goToMainMenuScene(cocos2d::Ref* sender);

private:
	virtual bool init();
	virtual void onExit();
	void onKeyReleased(cocos2d::EventKeyboard::KeyCode keycode, cocos2d::Event* e);

	// 
	std::vector<ext::Panel*> _touchPanels;
};

#endif // __MAINGAME_SCENE_H__
