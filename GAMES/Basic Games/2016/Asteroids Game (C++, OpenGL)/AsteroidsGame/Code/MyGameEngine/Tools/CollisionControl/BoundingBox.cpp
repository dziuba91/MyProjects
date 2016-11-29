#include "BoundingBox.h"

USING_NS_ENGINE;

BoundingBox::BoundingBox(Vector3D size, Vector3D position)
{
	this->_size = size;
	this->_position = position;
}

float BoundingBox::getMinX()
{
	return this->_position.x - this->_size.x/2;
}

float BoundingBox::getMaxX()
{
	return this->_position.x + this->_size.x/2;
}

float BoundingBox::getMinY()
{
	return this->_position.y - this->_size.y/2;
}

float BoundingBox::getMaxY()
{
	return this->_position.y + this->_size.y/2;
}

float BoundingBox::getMinZ()
{
	return this->_position.z - this->_size.z/2;
}

float BoundingBox::getMaxZ()
{
	return this->_position.z + this->_size.z/2;
}