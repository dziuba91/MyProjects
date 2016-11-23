#ifndef __T3_H
#define __T3_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>

class T3
{
private:
	float oddalenie3;
	GLfloat speed3;
	GLfloat kat3;
	GLfloat rquadX_3;
	GLfloat rquadY_3;

	int trybObrotu3;

	bool loadTexture3;

	GLuint	texture3;

	bool fog;

	DWORD time3;
	bool wait3;

public:
	T3();

private:
	AUX_RGBImageRec *LoadBMP(char *);

	int LoadGLTextures3();

	void DrawCube3();

	int InitGL3(GLvoid);

public:
	int DrawGLScene3();

	void keyboard3(bool *);
};

#endif