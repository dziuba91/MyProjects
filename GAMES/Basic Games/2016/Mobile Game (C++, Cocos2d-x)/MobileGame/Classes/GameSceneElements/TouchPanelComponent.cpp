#include "TouchPanelComponent.h"

USING_NS_CC;
USING_NS_EXT;
USING_NS_GAME;

void TouchPanelComponent::setParam(bool active, Panel* info)
{
	this->is_active = active;
	this->_panel = info;
}

Component* TouchPanelComponent::create(Panel* info, bool active)
{
	auto comp = TouchPanelComponent::create();
	comp->setName(info->getName());
	comp->setParam(active, info);

	return comp;
}

void TouchPanelComponent::update(float dt)
{
	this->_drawNode->clear();
	
	// draw the panel
	_drawNode->drawSolidRect(-(Vec2)_panel->getSize() / 2, (Vec2)_panel->getSize() / 2,
		is_active ? Color::GREEN : Color::RED);
}

void TouchPanelComponent::onEnter()
{
	Component::onEnter();

	// set position of the component
	Node* mainNode = Node::create();
	mainNode->setPosition(_panel->getCenterPosition());
	this->getOwner()->addChild(mainNode, -10);

	// prepare 'draw node' with collision body
	_drawNode = DrawNode::create();
	auto body = PhysicsBody::createBox(_panel->getSize());
	body->setCollisionBitmask(_panel->getID());
	body->setContactTestBitmask(true);
	body->setDynamic(false);
	_drawNode->setPhysicsBody(body);
	mainNode->addChild(_drawNode);

	//
	auto collisionListener = EventListenerPhysicsContact::create();
	collisionListener->onContactBegin = CC_CALLBACK_1(TouchPanelComponent::onContactBegin, this);
	collisionListener->onContactSeparate = CC_CALLBACK_1(TouchPanelComponent::onContactSeparate, this);

	Director::getInstance()->getEventDispatcher()->addEventListenerWithSceneGraphPriority(collisionListener, this->_drawNode);
}

bool TouchPanelComponent::onContactBegin(PhysicsContact& contact)
{
	if (contact.getShapeB()->getCollisionBitmask() == _panel->getID()) is_active = true;

	return false;
}

bool TouchPanelComponent::onContactSeparate(PhysicsContact& contact)
{
	if (contact.getShapeB()->getCollisionBitmask() == _panel->getID()) is_active = false;

	return false;
}

bool TouchPanelComponent::isOnTouchPanel(Vec2 pos)
{
	Vec2 origin = Vec2(_panel->getCenterPosition() - _panel->getSize() / 2);
	Vec2 destination = Vec2(origin + _panel->getSize());

	return (is_active && ((pos.x > origin.x) && (pos.x < destination.x) &&
			(pos.y > origin.y) && (pos.y < destination.y))) ? true : false;
}