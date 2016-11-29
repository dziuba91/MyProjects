#include "Vector3D.h"

USING_NS_ENGINE;

Vector3D::Vector3D()
{
	this->x = 0.f;
	this->y = 0.f;
	this->z = 0.f;
}

Vector3D::Vector3D(float x, float y, float z)
{
	this->x = x;
	this->y = y;
	this->z = z;
}

Vector3D::Vector3D(int x, int y, int z)
{
	this->x = (float)x;
	this->y = (float)y;
	this->z = (float)z;
}

Vector3D::Vector3D(float val)
{
	this->x = val;
	this->y = val;
	this->z = val;
}

Vector3D::Vector3D(int val)
{
	this->x = (float)val;
	this->y = (float)val;
	this->z = (float)val;
}

const Vector3D Vector3D::ZERO = Vector3D::Vector3D(0, 0, 0);

float Vector3D::maxValue()
{
	return (((this->x >= this->y) && (this->x >= this->z)) ? this->x : ((this->y >= this->z) ? this->y : this->z));
}

Vector3D Vector3D::operator/(int val)
{
	if (val != 0) 
		return Vector3D(this->x/val, this->y/val, this->z/val);

	return Vector3D();
}