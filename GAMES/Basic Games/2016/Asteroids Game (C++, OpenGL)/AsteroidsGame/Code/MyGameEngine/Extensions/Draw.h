#ifndef __MYGAMEENGINE_EXTENSIONS_DRAW_H__
#define __MYGAMEENGINE_EXTENSIONS_DRAW_H__

#include "MyGameEngine/Definitions.h"
#include "Vector3D.h"
#include "Color.h"

ENGINE_NS_BEGIN

/**
* draw standard geometric shapes (by use OpenGL functions)
*/
class Draw
{
public:
	static void drawCube(Vector3D size, Vector3D position, const GLfloat* color);

	static void drawCube(Vector3D size, Vector3D position, const GLfloat* color, Vector3D rotation);

private:
	static void cube(Vector3D size, const GLfloat* color);
};

ENGINE_NS_END

#endif // __MYGAMEENGINE_EXTENSIONS_DRAW_H__