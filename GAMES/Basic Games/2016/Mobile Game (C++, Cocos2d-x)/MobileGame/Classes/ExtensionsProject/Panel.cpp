#include "Panel.h"

USING_NS_CC;
USING_NS_EXT;

Rectangle::Rectangle()
{
	this->centerPosition = Point::ZERO;
	this->size = Size::ZERO;
}

Rectangle::Rectangle(Point p, Size s)
{
	this->centerPosition = p;
	this->size = s;
}

//
Panel::Panel(char* name, Point position, Size size, int id)
{
	this->_name = name;
	this->_rectangle = Rectangle(position, size);
	this->_ID = id;
}

const Point& Panel::getCenterPosition() 
{ 
	return this->_rectangle.centerPosition; 
};

const Size& Panel::getSize() 
{ 
	return this->_rectangle.size;
};

char* Panel::getName() 
{ 
	return this->_name;
};

int Panel::getID() 
{ 
	return this->_ID;
};