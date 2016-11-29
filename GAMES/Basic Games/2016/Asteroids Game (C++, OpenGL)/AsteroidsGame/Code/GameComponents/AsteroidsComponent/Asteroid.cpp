#include "Asteroid.h"

#include "GameComponents/Definitions.h"
#include "GameComponents/PlayerComponent.h"

USING_NS_ENGINE;

Asteroid::Asteroid(Vector3D size, Vector3D position)
	: BoundingBox(size, position)
{
	// initialize: directional move
	this->_moveDirectionType = static_cast<DirectionType>(rand() % DirectionType::DIRECTION_SIZE);

	this->_directionalMoveSpeed =
		rand() % (int)((ASTEROID_MAX_DIRECTIONAL_MOVE_SPEED - ASTEROID_MIN_DIRECTIONAL_MOVE_SPEED) * ASTEROID_DIRECTIONAL_MOVE_SPEED_PRECISION) /
			ASTEROID_DIRECTIONAL_MOVE_SPEED_PRECISION + ASTEROID_MIN_DIRECTIONAL_MOVE_SPEED;

	// initialize: rotation
	this->_rotation = Vector3D::ZERO;

	this->_rotationType = static_cast<RotationType>(rand() % RotationType::ROTATION_SIZE);

	this->_rotationSpeed = 
		rand() % (int)((ASTEROID_MAX_ROTATION_SPEED - ASTEROID_MIN_ROTATION_SPEED) * ASTEROID_ROTATION_SPEED_PRECISION) /
			ASTEROID_ROTATION_SPEED_PRECISION + ASTEROID_MIN_ROTATION_SPEED;
}

void Asteroid::draw()
{
	Draw::drawCube(this->_size, this->_position, Color::RED, this->_rotation);
}

void Asteroid::update()
{
	// update: directional move
	switch (this->_moveDirectionType)
	{
		case DirectionType::LEFT:
			this->_position.x -= this->_directionalMoveSpeed;
			break;
		case DirectionType::RIGHT:
			this->_position.x += this->_directionalMoveSpeed;
			break;
		default:
			break;
	}

	// update: rotation
	switch (this->_rotationType)
	{
		case RotationType::X_DESCENDING:
			this->_rotation.x -= this->_rotationSpeed;
			break;
		case RotationType::X_ASCENDING:
			this->_rotation.x += this->_rotationSpeed;
			break;
		case RotationType::Y_DESCENDING:
			this->_rotation.y -= this->_rotationSpeed;
			break;
		case RotationType::Y_ASCENDING:
			this->_rotation.y += this->_rotationSpeed;
			break;
		default:
			break;
	}

	// reset the value when full rotation
	if ((abs(this->_rotation.x) >= 360.f) || (abs(this->_rotation.y) >= 360.f))
		this->_rotation = Vector3D::ZERO;

	// update: falling move
	this->_position.y -= ASTEROID_FALLING_MOVE_SPEED;

	// check if there are any collisions with the 'PlayerComponent' model
	if (CollisionControl::isOnCollision2D(this, ((PlayerComponent*)Game::getComponent(PLAYER_COMPONENT_ID))->getSpaceship()))
		Game::resetAllComponents();
}

const Vector3D& Asteroid::getPosition()
{
	return this->_position;
}

const Vector3D& Asteroid::getSize()
{
	return this->_size;
}