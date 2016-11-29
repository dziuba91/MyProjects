#include "CollisionControl.h"

USING_NS_ENGINE;

bool CollisionControl::isOnCollision2D(BoundingBox* objA, BoundingBox* objB)
{
	if (objA->getMinX() > objB->getMaxX() || objA->getMaxX() < objB->getMinX()
		|| objA->getMinY() > objB->getMaxY() || objA->getMaxY() < objB->getMinY()) // do not collide?
			return false;

	return true; // else: collision detected
}