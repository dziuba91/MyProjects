#ifndef __ASTEROIDSCOMPONENT_ASTEROID_H__
#define __ASTEROIDSCOMPONENT_ASTEROID_H__

#include "MyGameEngine.h"

/**
* represents 'Asteroid' object
* control rotation and position of the object
* check collision (with 'Spaceship' object)
*/
class Asteroid : public MyGameEngine::BoundingBox
{
public:
	Asteroid(MyGameEngine::Vector3D size, MyGameEngine::Vector3D position);

	void draw();
	void update();

	const MyGameEngine::Vector3D& getSize();
	const MyGameEngine::Vector3D& getPosition();

private:
	enum DirectionType { LEFT, RIGHT, DIRECTION_SIZE };
	DirectionType _moveDirectionType;
	float _directionalMoveSpeed; // * speed of basic left/ right move

	enum RotationType { X_ASCENDING, X_DESCENDING, Y_ASCENDING, Y_DESCENDING, ROTATION_SIZE };
	RotationType _rotationType;
	float _rotationSpeed;

	MyGameEngine::Vector3D _rotation; // * represents current rotation state of the object
};

#endif // __ASTEROIDSCOMPONENT_ASTEROID_H__