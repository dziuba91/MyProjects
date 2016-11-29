#include "Spaceship.h"

#include "GameComponents/Definitions.h"

USING_NS_ENGINE;

Spaceship::Spaceship(Vector3D size, Vector3D position)
	: BoundingBox(size, position)
{
	this->_startPosition = position;
}

void Spaceship::move(Direction d)
{
	switch (d)
	{
		case Direction::LEFT:
			if ((this->_position.x - this->_size.x / 2 - PLAYER_MOVE_SPEED) >= -Application::getWindowSize().width / 2)
				this->_position.x -= PLAYER_MOVE_SPEED;
			break;
		case Direction::RIGHT:
			if ((this->_position.x + this->_size.x / 2 + PLAYER_MOVE_SPEED) <= Application::getWindowSize().width / 2)
				this->_position.x += PLAYER_MOVE_SPEED;
			break;
		default:
			break;
	}
}

void Spaceship::draw()
{
	Draw::drawCube(this->_size, this->_position, Color::BLUE);
}

void Spaceship::reset()
{
	this->_position = this->_startPosition;
}

const Vector3D& Spaceship::getPosition()
{
	return this->_position;
}