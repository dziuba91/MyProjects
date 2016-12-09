#include "SpikesBorderComponent.h"

USING_NS_CC;
USING_NS_GAME;

bool SpikesBorderComponent::init()
{
	if (!Component::init())
	{
		return false;
	}

	this->setName(SPIKESBORDER_NANE);

	return true;
}

void SpikesBorderComponent::onEnter()
{
	Component::onEnter();

	Size visibleSize = Director::getInstance()->getVisibleSize();

	auto mainNode = Node::create();
	mainNode->setPosition(Point(visibleSize.width / 2, visibleSize.height / 2));
	this->getOwner()->addChild(mainNode);

	// set collision body
	auto body = PhysicsBody::createEdgeBox(Size(visibleSize.width - SPIKES_BORDER_SIZE *2, visibleSize.height - SPIKES_BORDER_SIZE *2), PHYSICSBODY_MATERIAL_DEFAULT, 1);
	body->setCollisionBitmask(SPIKESBORDER_ID);
	body->setContactTestBitmask(true);
	body->setDynamic(false);
	mainNode->setPhysicsBody(body);

	// draw graphics
	Sprite* drawTop = Sprite::create("wall1.png", Rect(Vec2::ZERO, Size(visibleSize.width, SPIKES_BORDER_SIZE)));
	drawTop->setPosition(Point(0, visibleSize.height/2 - SPIKES_BORDER_SIZE/2));
	mainNode->addChild(drawTop);

	Sprite* drawLeft = Sprite::create("wall1.png", Rect(Vec2::ZERO, Size(SPIKES_BORDER_SIZE, visibleSize.height)));
	drawLeft->setPosition(Point(-visibleSize.width / 2 + SPIKES_BORDER_SIZE / 2, 0));
	mainNode->addChild(drawLeft);

	Sprite* drawRight = Sprite::create("wall1.png", Rect(Vec2::ZERO, Size(SPIKES_BORDER_SIZE, visibleSize.height)));
	drawRight->setPosition(Point(visibleSize.width / 2 - SPIKES_BORDER_SIZE / 2, 0));
	mainNode->addChild(drawRight);

	Sprite* drawBottom = Sprite::create("wall1.png", Rect(Vec2::ZERO, Size(visibleSize.width, SPIKES_BORDER_SIZE)));
	drawBottom->setPosition(Point(0, -visibleSize.height / 2 + SPIKES_BORDER_SIZE / 2));
	mainNode->addChild(drawBottom);
}
