#include "PassGateComponent.h"

USING_NS_CC;
USING_NS_GAME;

bool PassGateComponent::init()
{
	if (!Component::init())
	{
		return false;
	}

	this->setName(PASSGATE_NAME);

	_visibleSize = Director::getInstance()->getVisibleSize();

	return true;
}

void PassGateComponent::onEnter()
{
	Component::onEnter();

	float centerPosition = nextPosY();

	_mainNode = Node::create();
	_mainNode->setPosition(Point(_visibleSize.width / 2, centerPosition));
	this->getOwner()->addChild(_mainNode, 1);

	// center point in the gate for the score updating (check the collision)
	Node* gateCenterNode = Node::create();
	auto gateCenterBody = PhysicsBody::createBox(Size(0, GATE_HEIGHT));
	gateCenterBody->setCollisionBitmask(PASSGATE_CENTER_ID);
	gateCenterBody->setDynamic(false);
	gateCenterBody->setContactTestBitmask(true);
	_mainNode->setPhysicsBody(gateCenterBody);

	// area inside the gate
	Node* gateNode = Node::create();
	auto gateBody = PhysicsBody::createEdgeBox(Size(GATE_WIDTH, GATE_HEIGHT));
	gateBody->setCollisionBitmask(PASSGATE_AREA_ID);
	gateBody->setDynamic(false);
	gateBody->setContactTestBitmask(true);
	gateNode->setPhysicsBody(gateBody);
	_mainNode->addChild(gateNode);

	// height of top 'wall'
	float height1 = _visibleSize.height - SCORE_LAYOUT_BORDER_SIZE * 2 - SCORE_TEXT_SIZE-GATE_HEIGHT;

	// draw graphics of the top 'wall'
	Sprite* topNode = Sprite::create("wall2.png", Rect(Vec2(0, centerPosition), Size(GATE_WIDTH, height1)));
	topNode->setPosition(Point(0, topNode->getBoundingBox().getMaxY()+GATE_HEIGHT/2));
	auto topBody = PhysicsBody::createEdgeBox(topNode->getContentSize());
	topBody->setCollisionBitmask(OBSTACLE_ID);
	topBody->setDynamic(false);
	topBody->setContactTestBitmask(true);
	topNode->setPhysicsBody(topBody);
	_mainNode->addChild(topNode);

	// draw graphics of the bottom 'wall'
	Sprite* bottomNode = Sprite::create("wall2.png", Rect(Vec2(0, centerPosition), Size(GATE_WIDTH, height1)));
	bottomNode->setPosition(Point(0, -(bottomNode->getBoundingBox().getMaxY() + GATE_HEIGHT / 2)));
	auto bottomBody = PhysicsBody::createEdgeBox(bottomNode->getContentSize());
	bottomBody->setCollisionBitmask(OBSTACLE_ID);
	bottomBody->setDynamic(false);
	bottomBody->setContactTestBitmask(true);
	bottomNode->setPhysicsBody(bottomBody);
	_mainNode->addChild(bottomNode);

	//
	auto collisionListener = EventListenerPhysicsContact::create();
	collisionListener->onContactSeparate = CC_CALLBACK_1(PassGateComponent::onContactSeparate, this);
	Director::getInstance()->getEventDispatcher()->addEventListenerWithSceneGraphPriority(collisionListener, gateNode);
}

bool PassGateComponent::onContactSeparate(PhysicsContact& contact)
{
	if ((contact.getShapeA()->getCollisionBitmask() == PLAYER_ID) && (contact.getShapeB()->getCollisionBitmask() == PASSGATE_AREA_ID))
	{
		if ((_collisionPointX != 0) && (abs(_collisionPointX - contact.getShapeA()->getBody()->getPosition().x) > GATE_WIDTH-5))
		{
			// change 'Y' coordinate of the 'gate' main point
			auto action = MoveTo::create(GATE_ACTION_SPEED, Point(_visibleSize.width / 2, nextPosY()));
			_mainNode->runAction(action);
		}

		_collisionPointX = contact.getShapeA()->getBody()->getPosition().x;
	}

	return false;
}

int PassGateComponent::nextPosY()
{
	int minHeight = SCORE_LAYOUT_BORDER_SIZE*2 + SCORE_TEXT_SIZE + GATE_HEIGHT/2;

	int tmp = rand() % ((int)_visibleSize.height - 2*minHeight) + minHeight;

	return tmp;
}