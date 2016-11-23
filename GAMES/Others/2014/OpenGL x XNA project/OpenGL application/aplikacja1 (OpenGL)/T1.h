#ifndef __T1_H
#define __T1_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>

class T1
{
private:
	float oddalenie1;
	GLfloat speed1;
	GLfloat kat1;
	GLfloat rquadX_1;
	GLfloat rquadY_1;

	int trybObrotu1;

public:
	T1();

public:
	int InitGL1(GLvoid);
	int DrawGLScene1();
	void keyboard1(bool *keys);
};

#endif