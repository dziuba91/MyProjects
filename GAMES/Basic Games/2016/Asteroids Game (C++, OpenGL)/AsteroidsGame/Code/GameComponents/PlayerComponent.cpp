#include "PlayerComponent.h"

#include "Definitions.h"

USING_NS_ENGINE;

void PlayerComponent::draw()
{
	this->_spaceship->draw();
}

void PlayerComponent::init()
{
	this->setComponentID(PLAYER_COMPONENT_ID);

	Vector3D size = Vector3D(PLAYER_SIZE);
	Vector3D position = Vector3D(0, (int)(-Application::getWindowSize().height / 2 + size.y / 2 + PLAYER_POSITION_OFFSET), -2 * PLAYER_SIZE);
	this->_spaceship = new Spaceship(size, position);
}

void PlayerComponent::update()
{
	if (Application::isKeyDown(Keys::LEFT))
		this->_spaceship->move(Spaceship::Direction::LEFT);

	else if (Application::isKeyDown(Keys::RIGHT))
		this->_spaceship->move(Spaceship::Direction::RIGHT);
}

void PlayerComponent::reset()
{
	this->_spaceship->reset();
}

void PlayerComponent::onExit()
{
	delete this->_spaceship;
}

Spaceship* PlayerComponent::getSpaceship()
{
	return this->_spaceship;
}