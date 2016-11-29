#ifndef __MYGAMEENGINE_EXTENSIONS_VECTOR3D_H__
#define __MYGAMEENGINE_EXTENSIONS_VECTOR3D_H__

#include "MyGameEngine/Definitions.h"

ENGINE_NS_BEGIN

struct Vector3D
{
	Vector3D();
	Vector3D(float x, float y, float z);
	Vector3D(int x, int y, int z);
	Vector3D(float val);
	Vector3D(int val);

	float x;
	float y;
	float z;

	static const Vector3D ZERO;

	float maxValue();

	Vector3D operator/(int);
};

ENGINE_NS_END

#endif // __MYGAMEENGINE_EXTENSIONS_VECTOR3D_H__