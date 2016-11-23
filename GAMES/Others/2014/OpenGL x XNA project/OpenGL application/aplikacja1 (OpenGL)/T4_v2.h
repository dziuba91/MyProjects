#ifndef __T4_V2_H
#define __T4_V2_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>

class T4_v2
{
private:
	float przesuniecie44;
	float oddalenie44;
	float odl44;
	GLfloat speed44;
	GLfloat kat44;
	GLfloat rquadX_44;
	GLfloat rquadY_44;
	
	int trybObrotu44;

	GLfloat rotatuj44;

	//
	GLfloat rot44 [27][3];

	int rot_nr44 [27][1000];
	int licznik44 [27];

	int tab44[3][3];

	//
	GLfloat max44;
	bool wait44;

	int ilosc44;

	DWORD time44;

public:
	T4_v2();

private:
	void DrawColoredCube(float);

	void Tryb44(int);

	int InitGL44(GLvoid);

public:
	int DrawGLScene44();

	void keyboard44(bool *);
};

#endif