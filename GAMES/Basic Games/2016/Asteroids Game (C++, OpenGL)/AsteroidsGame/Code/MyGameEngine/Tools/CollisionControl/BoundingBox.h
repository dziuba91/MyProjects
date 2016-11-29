#ifndef __MYGAMEENGINE_TOOLS_COLLISIONCONTROL_BOUNDINGBOX_H__
#define __MYGAMEENGINE_TOOLS_COLLISIONCONTROL_BOUNDINGBOX_H__

#include "MyGameEngine/Extensions/Vector3D.h"

ENGINE_NS_BEGIN

class BoundingBox
{
public:
	BoundingBox(MyGameEngine::Vector3D size, MyGameEngine::Vector3D position);

	float getMinX();
	float getMaxX();
	float getMinY();
	float getMaxY();
	float getMinZ();
	float getMaxZ();

protected:
	MyGameEngine::Vector3D _size;
	MyGameEngine::Vector3D _position;
};

ENGINE_NS_END

#endif // __MYGAMEENGINE_TOOLS_COLLISIONCONTROL_BOUNDINGBOX_H__