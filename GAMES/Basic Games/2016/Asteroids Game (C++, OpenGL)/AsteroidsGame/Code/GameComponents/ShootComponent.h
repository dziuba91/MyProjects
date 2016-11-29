#ifndef __SHOOT_COMPONENT_H__
#define __SHOOT_COMPONENT_H__

#include "MyGameEngine.h"

#include "ShootComponent/Bullet.h"

/**
* support operations on list of 'Bullet' objects
*
* @Draw: 'Bullet' objects (if need)
* @Update: number of 'Bullet' to display
*/
class ShootComponent : public MyGameEngine::Component
{
public:
	virtual void init();
	virtual void draw();
	virtual void update();
	virtual void reset();
	virtual void onExit();

private:
	/** add new 'Bullet' object to the list */
	void addBullet();
	
	/** remove first 'Bullet' object ('index' = 0) on the list */
	void removeBullet();

	void clearBulletList();

	//
	std::vector<Bullet*> _bulletList;

	DWORD _lastShootTime; // * time in millisecond of last 'addBullet()' function call
};

#endif // __SHOOT_COMPONENT_H__