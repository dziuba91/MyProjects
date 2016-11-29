#include "Bullet.h"

#include "GameComponents/AsteroidsComponent.h"
#include "GameComponents/Definitions.h"

USING_NS_ENGINE;

Bullet::Bullet(Vector3D size, Vector3D position)
	: BoundingBox(size, position)
{ }

void Bullet::update()
{
	this->_position.y += BULLET_MOVE_SPEED;

	// check if there are any collisions with the 'AsteroidsComponent' models ('Asteroid')
	std::vector<Asteroid*> tmp = ((AsteroidsComponent*)Game::getComponent(ASTEROIDS_COMPONENT_ID))->getAsteroidList();
	for (int i=0; i<tmp.size(); i++)
		if (CollisionControl::isOnCollision2D(this, tmp[i]))
		{
			((AsteroidsComponent*)Game::getComponent(ASTEROIDS_COMPONENT_ID))->shootToAsteroid(i);
			break;
		}
}

void Bullet::draw()
{
	Draw::drawCube(this->_size, this->_position, Color::YELLOW);
}

const Vector3D& Bullet::getSize()
{
	return this->_size;
}

const Vector3D& Bullet::getPosition()
{
	return this->_position;
}