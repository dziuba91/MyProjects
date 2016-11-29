#ifndef __PLAYERCOMPONENT_SPACESHIP_H__
#define __PLAYERCOMPONENT_SPACESHIP_H__

#include "MyGameEngine.h"

/**
* represents 'Spaceship' object
* control position of the object
*/
class Spaceship : public MyGameEngine::BoundingBox
{
public:
	Spaceship(MyGameEngine::Vector3D size, MyGameEngine::Vector3D position);

	enum Direction { LEFT, RIGHT };

	void reset();
	void draw();

	/** allow to change position of the object in X-axis ('right/ left move') */
	void move(Direction direction);

	const MyGameEngine::Vector3D& getPosition();

private:
	MyGameEngine::Vector3D _startPosition;
};

#endif // __PLAYERCOMPONENT_SPACESHIP_H__