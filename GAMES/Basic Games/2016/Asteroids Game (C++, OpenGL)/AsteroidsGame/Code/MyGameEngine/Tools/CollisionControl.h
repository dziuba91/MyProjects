#ifndef __MYGAMEENGINE_TOOLS_COLLISIONCONTROL_H__
#define __MYGAMEENGINE_TOOLS_COLLISIONCONTROL_H__

#include "CollisionControl/BoundingBox.h"

ENGINE_NS_BEGIN

/**
* support controlling 'Bounding Box' collisions
*/
class CollisionControl
{
public:
	/** check collision beetwen two objects of 'BoundingBox' class
	* operation in two-dimensional coordinate system
	*/
	static bool isOnCollision2D(BoundingBox* boundingBoxA, BoundingBox* boundingBoxB);
};

ENGINE_NS_END

#endif // __MYGAMEENGINE_TOOLS_COLLISIONCONTROL_H__