#ifndef __PANEL_H__
#define __PANEL_H__

#include "cocos2d.h"
#include "Definitions.h"

EXT_NS_BEGIN

/**
* standard 'rectangle' structure for the project
* define for 'Panel' class, of current project
*/
struct Rectangle
{
	Rectangle();
	Rectangle(cocos2d::Point, cocos2d::Size);

	cocos2d::Point centerPosition;
	cocos2d::Size size;
};

/**
* define standard 'Panel' data type
* identify the 'panel'
*/
class Panel
{
public:
	Panel(char* name, cocos2d::Point position, cocos2d::Size size, int id);

	const cocos2d::Point& getCenterPosition();
	const cocos2d::Size& getSize();

	/** @Return: name for the related component identification */
	char* getName();

	/** @Return: collision 'body' identification */
	int getID();

private:
	char* _name; // * identify the object
	Rectangle _rectangle; // * represents size and position of the 'panel'

	int _ID; // * identify the collision body
};

EXT_NS_END

#endif // __PANEL_H__