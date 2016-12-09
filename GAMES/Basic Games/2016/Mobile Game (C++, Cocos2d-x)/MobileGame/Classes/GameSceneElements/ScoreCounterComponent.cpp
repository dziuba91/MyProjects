#include "ScoreCounterComponent.h"

USING_NS_CC;
USING_NS_GAME;

int ScoreCounterComponent::getScore()
{
	return this->_score;
}

bool ScoreCounterComponent::init()
{
	if (!Component::init())
	{
		return false;
	}
	
	this->setName(SCORECOUNTER_NAME);

	return true;
}

void ScoreCounterComponent::onEnter()
{
	Component::onEnter();

	Size visibleSize = Director::getInstance()->getVisibleSize();

	//
	Node* mainNode = Node::create();
	mainNode->setPosition(Vec2(visibleSize.width / 2, visibleSize.height - (SCORE_LAYOUT_BORDER_SIZE + SCORE_TEXT_SIZE / 2)));
	this->getOwner()->addChild(mainNode, 2);

	// draw layout
	Sprite* drawLayout = Sprite::create("points1.png", Rect(Vec2(0, 0), Size(GATE_WIDTH - SCORE_LAYOUT_BORDER_SIZE * 2, SCORE_TEXT_SIZE)));
	mainNode->addChild(drawLayout);
	
	// display text with the score
	_scoreLabel = Label::create(StringUtils::toString(_score), "Helvetica", SCORE_TEXT_SIZE);
	_scoreLabel->setColor(Color3B(0,0,0));
	mainNode->addChild(_scoreLabel);

	// set the function for control a collision actions
	auto collisionListener = EventListenerPhysicsContact::create();
	collisionListener->onContactBegin = CC_CALLBACK_1(ScoreCounterComponent::onContactBegin, this);
	Director::getInstance()->getEventDispatcher()->addEventListenerWithSceneGraphPriority(collisionListener, mainNode);
}

bool ScoreCounterComponent::onContactBegin(PhysicsContact& contact)
{
	if (contact.getShapeB()->getCollisionBitmask() == PASSGATE_CENTER_ID)
	{
		_scoreLabel->setString(StringUtils::toString(++_score));
	}

	return false;
}