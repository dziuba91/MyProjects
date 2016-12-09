#ifndef __MOVEMENTS_H__
#define __MOVEMENTS_H__

#include "cocos2d.h"
#include "Definitions.h"

EXT_NS_BEGIN

/**
* control position of the object in the defined movements actions:
* a) custom movement called 'fall_Movement' describe object bevaviour when there is no user interaction
* b) 'touch_Movement' adapting physics of the mathematical pendulum behaviour (on user interaction action)
*/
class Movements
{
public:
	Movements(cocos2d::Point startPosition);

	/** movement method when touch action happen */
	void touch_Movement();

	/** custom movement when there is no interaction with the game */
	void fall_Movement();

	/** actualize speed of the 'fall' movement ('fall_Movement()' function) */
	void actualizeFallingSpeed();

	/** actualize parameters of the movement function when touch interaction happen */
	void setTouchPosition(cocos2d::Vec2);

	/** @Return: current position of an object in the 'movement'*/
	const cocos2d::Point& getPoint();

private:
	/** need to compute 'on touch movement' behaviour (pendulum movement) */
	float f(float y);

	// @Variables for the physics calculations:
	float _x; // * coordinates 'X' of 'main point' (position of the object in movement)
	float _y; // * coordinates 'Y' of 'main point' (position of the object in movement)
	float _x0;	// * coordinates 'X' of start pendulum point ('point zero'/ 'touch position')
	float _y0; // * coordinates 'Y' of start pendulum point ('point zero'/ 'touch position')
	float _alpha; // * calculation parameter (an angle)
	float _l; // * calculation parameter (length beetwen 'main point' and 'touch point')
	float _w = .1f; // * calculation parameter
	float _k1, _k2; // * calculation parameters

	float _vy; // * velocity in the 'Y-coordinates' direction ('fall_Movement()')
	float _vx; // * velocity in the 'X-coordinates' direction ('fall_Movement()')
};

EXT_NS_END

#endif // __MOVEMENTS_H__