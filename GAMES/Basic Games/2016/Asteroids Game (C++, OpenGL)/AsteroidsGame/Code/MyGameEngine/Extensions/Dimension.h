#ifndef __MYGAMEENGINE_EXTENSIONS_DIMENSION_H__
#define __MYGAMEENGINE_EXTENSIONS_DIMENSION_H__

#include "MyGameEngine/Definitions.h"

ENGINE_NS_BEGIN

struct Dimension
{
	Dimension();
	Dimension(float width, float height);
	Dimension(int width, int height);

	float width;
	float height;
};

ENGINE_NS_END

#endif // __MYGAMEENGINE_EXTENSIONS_DIMENSION_H__