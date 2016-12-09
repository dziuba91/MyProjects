#include "Movements.h"

USING_NS_CC;
USING_NS_EXT;

Movements::Movements(Point startPosition)
{
	this->_x = startPosition.x;
	this->_y = startPosition.y;
}

void Movements::touch_Movement()
{
	_k1 = f(_w);
	_k2 = f(_w + (1 / 2) * SPEED * _k1);
	_w += _k2 * SPEED;

	_alpha += _w * SPEED;

	_x = -_l * sin(_alpha);
	_y = -_l * cos(_alpha);

	_x += _x0;
	_y += _y0;
}

float Movements::f(float y)
{
	return (-(G / _l) * sin(_alpha));
}

void Movements::fall_Movement()
{
	_x += _vx;

	_vy -= (G / 250);
	_y += _vy;
}

void Movements::actualizeFallingSpeed()
{
	Point prevPoint = Point(_x, _y);

	touch_Movement();

	_vx = _x - prevPoint.x;
	_vy = _y - prevPoint.y;
}

void Movements::setTouchPosition(Vec2 touchPoint)
{
	_x0 = touchPoint.x;
	_y0 = touchPoint.y;

	_l = sqrt(pow((_x0 - _x), 2) + pow((_y0 - _y), 2));

	_alpha = atan((_x - _x0) / (_y - _y0));
	if (_y > _y0) _alpha += PI;

	_w = .1f;
}

const Point& Movements::getPoint()
{
	return Point(_x, _y);
}