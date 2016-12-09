#include "PlayerComponent.h"

#include "ScoreCounterComponent.h"
#include "TouchPanelComponent.h"
#include "./GameScene.h"

USING_NS_CC;
USING_NS_EXT;
USING_NS_PROJECT;
USING_NS_GAME;

Component* PlayerComponent::create(std::vector<Panel *>& touchPanel, Point startPosition)
{
	auto comp = PlayerComponent::create();
	comp->setParam(touchPanel, startPosition);

	return comp;
}

void PlayerComponent::setParam(std::vector<Panel *>& touchPanel, Point pos)
{
	this->_touchPanels = touchPanel;
	this->_startPosition = pos;
}

bool PlayerComponent::init()
{
	if (!Component::init())
	{
		return false;
	}

	this->setName(PLAYER_NAME);

	return true;
}

void PlayerComponent::onEnter()
{
	Component::onEnter();

	_movements = new Movements(_startPosition);

	// draw player graphic
	_player = Sprite::create("circle.png");
	_player->setPosition(_movements->getPoint());
	_player->setScale(.5f);
	this->getOwner()->addChild(_player);

	// set the draw nodes
	_drawPointNode = DrawNode::create();
	_player->addChild(_drawPointNode, -1);

	_drawLineNode = DrawNode::create();
	_player->addChild(_drawLineNode, -2);

	// set collision mask
	auto body = PhysicsBody::createCircle(_player->getContentSize().height / 2);
	body->setCollisionBitmask(PLAYER_ID);
	body->setContactTestBitmask(true);
	_player->setPhysicsBody(body);

	//
	auto touchListener = EventListenerTouchOneByOne::create();
	touchListener->onTouchBegan = CC_CALLBACK_2(PlayerComponent::onTouchBegan, this);
	touchListener->onTouchEnded = CC_CALLBACK_2(PlayerComponent::onTouchEnded, this);
	Director::getInstance()->getEventDispatcher()->addEventListenerWithSceneGraphPriority(touchListener, _player);

	auto collisionListener = EventListenerPhysicsContact::create();
	collisionListener->onContactBegin = CC_CALLBACK_1(PlayerComponent::onContactBegin, this);
	Director::getInstance()->getEventDispatcher()->addEventListenerWithSceneGraphPriority(collisionListener, _player);
}

void PlayerComponent::update(float dt)
{
	if (!is_endGame)
	{
		if (is_onTouch)
		{
			_movements->touch_Movement();

			_player->setPosition(_movements->getPoint());
		}
		else if (is_fall)
		{
			_movements->fall_Movement();

			_player->setPosition(_movements->getPoint());
		}

		draw(dt);
	}
}

void PlayerComponent::draw(float dt)
{
	_drawPointNode->clear();
	_drawLineNode->clear();
	
	if (is_onTouch)
	{
		Vec2 centerPosition(_player->getBoundingBox().size.width, _player->getBoundingBox().size.height);
		Vec2 destination = (_touchPosition - _movements->getPoint()) * (1 / _player->getScale()) + centerPosition;

		_drawPointNode->drawPoint(destination, 15, Color4F(0.0, 1.0, 0.0, 1.0));
		_drawLineNode->drawLine(centerPosition, destination, Color4F(1.0, 1.0, 1.0, 1.0));
	}
}

bool PlayerComponent::onTouchBegan(Touch* touch, Event* event)
{
	if (!is_endGame)
	{
		for (int i = 0; i < _touchPanels.size(); i++)
			if (((TouchPanelComponent*)this->getOwner()->getComponent(_touchPanels[i]->getName()))->isOnTouchPanel(touch->getLocation()))
			{
				is_fall = false;
				is_onTouch = true;

				_touchPosition = touch->getLocation();
				_movements->setTouchPosition(_touchPosition);

				return true;
			}
	}

	return false;
}

void PlayerComponent::onTouchEnded(Touch* touch, Event* event)
{
	if (!is_endGame)
	{
		is_onTouch = false;
		is_fall = true;

		_movements->actualizeFallingSpeed();
	}
}

bool PlayerComponent::onContactBegin(PhysicsContact& contact)
{
	if (!is_endGame)
	{
		PhysicsBody *a = contact.getShapeB()->getBody();

		if ((a->getCollisionBitmask() == OBSTACLE_ID) || (a->getCollisionBitmask() == SPIKESBORDER_ID))
		{
			is_onTouch = false;
			is_fall = false;
			is_endGame = true;

			// display 'dialog box'
			this->getOwner()->addChild(MyDialogBox::create(
				{
					Label::create("Your Score:", "Calibri", 30),
					Label::create(StringUtils::toString(((ScoreCounterComponent*)this->getOwner()->getComponent(SCORECOUNTER_NAME))->getScore()), "Calibri", 120)
				},
				{
					DialogBoxButton("TRY AGAIN", TextHAlignment::LEFT, CC_CALLBACK_1(GameScene::goToGameScene, ((GameScene*)this->getOwner()))),
					DialogBoxButton("BACK TO\n MAIN MENU", TextHAlignment::RIGHT, CC_CALLBACK_1(GameScene::goToMainMenuScene, ((GameScene*)this->getOwner())))
				}
			), 10);
		}
	}

	return false;
}

void PlayerComponent::onExit()
{
	delete _movements;
}