#ifndef __SHOOTCOMPONENT_BULLET_H__
#define __SHOOTCOMPONENT_BULLET_H__

#include "MyGameEngine.h"

/**
* represents 'Bullet' object
* control position of the object
* check collision (with 'Asteroid' object)
*/
class Bullet : public MyGameEngine::BoundingBox
{
public:
	Bullet(MyGameEngine::Vector3D size, MyGameEngine::Vector3D position);

	void draw();
	void update();

	const MyGameEngine::Vector3D& getSize();
	const MyGameEngine::Vector3D& getPosition();
};

#endif // __SHOOTCOMPONENT_BULLET_H__