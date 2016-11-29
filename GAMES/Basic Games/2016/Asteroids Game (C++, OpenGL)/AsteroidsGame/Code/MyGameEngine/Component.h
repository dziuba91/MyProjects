#ifndef __MYGAMEENGINE_COMPONENT_H__
#define __MYGAMEENGINE_COMPONENT_H__

#include "Definitions.h"

ENGINE_NS_BEGIN

/**
* 'Components' are performed by the 'Game Engine'
* need to implement 'gameplay' of the game
*/
class Component
{
public:
	/** initialize 'Component' parameters */
	virtual void init();

	/** allow to use drawing functions */
	virtual void draw();

	/** update 'Component' parameters */
	virtual void update();

	/** reset selected parameters of the 'Component' to the initial state 
	* to call the function need to use: 'Draw::resetAllComponents()' from any game object
	*/
	virtual void reset();

	/** any operations before game will end */
	virtual void onExit();

	/** @Return true: 'id' is equal to identification number of the 'Component' */
	bool isComponentID(unsigned int id);

protected:
	/** need to access to the 'Component' from another object (if 'id' is greater than 0) */
	void setComponentID(unsigned int id);

private:
	unsigned int _ID = 0; // default component ID = 0
};

ENGINE_NS_END

#endif // __MYGAMEENGINE_COMPONENT_H__