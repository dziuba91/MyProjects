#include "MainMenuScene.h"

#include "GameScene.h"

USING_NS_CC;

Scene* MainMenuScene::createScene()
{
	auto scene = Scene::create();

	auto layer = MainMenuScene::create();
	scene->addChild(layer);

	return scene;
}

bool MainMenuScene::init()
{
	if (!Layer::init())
	{
		return false;
	}

	// layer settings
	this->setKeypadEnabled(true);

	// window size
	Size visibleSize = Director::getInstance()->getVisibleSize();

	Node* mainNode = Node::create();
	mainNode->setPosition(Point(visibleSize.width / 2, visibleSize.height / 2));
	this->addChild(mainNode);

	// draw background
	auto backgroundSprite = Sprite::create("background1.png", Rect(Vec2::ZERO, Size(visibleSize.width, visibleSize.height)));
	mainNode->addChild(backgroundSprite);

	// draw 'MenuItemLabels' (buttons)
	Label* label = Label::create("PLAY", "Helvetica", 40);
	label->setColor(Color3B(0, 0, 0));
	auto playItem = MenuItemLabel::create(label, CC_CALLBACK_1(MainMenuScene::goToGameScene, this));

	auto menu = Menu::create(playItem, NULL);
	menu->setPosition(Point::ZERO);
	mainNode->addChild(menu);
	
	return true;
}

void MainMenuScene::onKeyReleased(EventKeyboard::KeyCode keycode, Event* e)
{
	if (keycode == EventKeyboard::KeyCode::KEY_BACK)
		Director::getInstance()->end();
}

void MainMenuScene::goToGameScene(cocos2d::Ref* sender)
{
	Director::getInstance()->replaceScene(TransitionFade::create(0.5f, GameScene::createScene()));
}