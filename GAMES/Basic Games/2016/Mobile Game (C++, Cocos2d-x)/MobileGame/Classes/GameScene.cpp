#include "GameScene.h"

#include "./MainMenuScene.h"

USING_NS_CC;
USING_NS_EXT;
USING_NS_GAME;

Scene* GameScene::createScene()
{
	auto scene = Scene::createWithPhysics();
	scene->getPhysicsWorld()->setGravity(Vect(0, 0));

	auto layer = GameScene::create();
    scene->addChild(layer);
	
    return scene;
}

bool GameScene::init()
{
    if ( !Layer::init() )
    {
        return false;
    }

	// layer settings
	this->setKeypadEnabled(true);

	// important data initialization
	Size visibleSize = Director::getInstance()->getVisibleSize();

	// set up 'Panel' properties for 'TouchPanelComponent' (which display 'Panel')
	Size size = Size(visibleSize.width / 2 - SPIKES_BORDER_SIZE - GATE_WIDTH / 2 - 2 * FREE_SPACE_SIZE, visibleSize.height - SPIKES_BORDER_SIZE * 2 - FREE_SPACE_SIZE * 2);
	_touchPanels.push_back(new Panel(PANEL_LEFT_NAME, Point(SPIKES_BORDER_SIZE + FREE_SPACE_SIZE + size.width/2, visibleSize.height / 2), size, PANEL_LEFT_ID));
	_touchPanels.push_back(new Panel(PANEL_RIGHT_NAME, Point(visibleSize.width / 2 + GATE_WIDTH / 2 + FREE_SPACE_SIZE + size.width/2, visibleSize.height / 2), size, PANEL_RIGHT_ID));

	// select start position of 'PlayerComponent'
	int activeArea = rand() % _touchPanels.size();

	// set components of the layer
	for (int i = 0; i < _touchPanels.size(); i++)
		this->addComponent(TouchPanelComponent::create(_touchPanels[i], (i == activeArea) ? true : false));

	this->addComponent(SpikesBorderComponent::create());

	this->addComponent(PassGateComponent::create());

	this->addComponent(ScoreCounterComponent::create());

	this->addComponent(PlayerComponent::create(_touchPanels, _touchPanels[activeArea]->getCenterPosition()));

    return true;
}

void GameScene::onExit()
{
	for (int i = 0; i < _touchPanels.size(); i++)
		delete _touchPanels[i];
}

void GameScene::onKeyReleased(EventKeyboard::KeyCode keycode, Event* e)
{
	if (keycode == EventKeyboard::KeyCode::KEY_BACK)
		goToMainMenuScene(NULL);
}

void GameScene::goToGameScene(cocos2d::Ref* sender)
{
	Director::getInstance()->replaceScene(TransitionFade::create(0.5f, GameScene::createScene()));
}

void GameScene::goToMainMenuScene(cocos2d::Ref* sender)
{
	Director::getInstance()->replaceScene(TransitionFade::create(0.5f, MainMenuScene::createScene()));
}