#ifndef __MYGAMEENGINE_EXTENSIONS_COLOR_H__
#define __MYGAMEENGINE_EXTENSIONS_COLOR_H__

#include <windows.h>
#include <gl/GL.h>

#include "MyGameEngine/Definitions.h"

ENGINE_NS_BEGIN

/**
* declaration of color parameters used in the project
*/
struct Color
{
	static const GLfloat BLUE[];
	static const GLfloat RED[];
	static const GLfloat YELLOW[];
};

ENGINE_NS_END

#endif // __MYGAMEENGINE_EXTENSIONS_COLOR_H__