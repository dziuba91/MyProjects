#include "Dimension.h"

USING_NS_ENGINE;

Dimension::Dimension()
{
	this->width = 0.f;
	this->height = 0.f;
}

Dimension::Dimension(float width, float height)
{
	this->width = width;
	this->height = height;
}

Dimension::Dimension(int width, int height)
{
	this->width = (float)width;
	this->height = (float)height;
}