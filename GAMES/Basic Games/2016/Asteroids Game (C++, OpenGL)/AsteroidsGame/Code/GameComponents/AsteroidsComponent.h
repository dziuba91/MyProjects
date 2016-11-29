#ifndef __ASTEROIDS_COMPONENT_H__
#define __ASTEROIDS_COMPONENT_H__

#include "MyGameEngine.h"

#include "AsteroidsComponent/Asteroid.h"

/**
* support operations on list of 'Asteroid' objects
*
* @Draw: 'Asteroid' objects
* @Update: number of 'Asteroid' to display
*/
class AsteroidsComponent : public MyGameEngine::Component
{
public:
	virtual void init();
	virtual void draw();
	virtual void update();
	virtual void reset();
	virtual void onExit();

	/** allow an interaction from another component ('ShootComponent')
	* destroy 'Asteroid' object represented by 'index' parameter
	*/
	void shootToAsteroid(int index);

	/** need for synchronize an operations beetwen components (e.g.: models 'position', 'size') 
	* @Return: array of 'Asteroid' objects (which inherits 'BoundingBox' class) 
	*/
	const std::vector<Asteroid*>& getAsteroidList();

private:
	void addAsteroid();
	void removeAsteroid(int index);
	void clearAsteroidList();

	std::vector<Asteroid*> _asteroidList;

	int _asteroidsPerRound; // * number of maximum 'Asteroid' objects for the round
};

#endif // __ASTEROIDS_COMPONENT_H__