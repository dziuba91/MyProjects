#ifndef __T2_H
#define __T2_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>

class T2
{
private:
	float przesuniecie2;
	float oddalenie2;
	GLfloat speed2;
	GLfloat kat2;
	GLfloat rquadX_2;
	GLfloat rquadY_2;

	int trybObrotu2;

	int ilosc;
	float odl;

public:
	T2();

private:
	int InitGL2(GLvoid);

public:
	int DrawGLScene2();
	void keyboard2(bool *keys);
};

#endif