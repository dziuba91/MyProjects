#ifndef __T5_H
#define __T5_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include "fps.h"

class T5
{
	FPS * fps1;

private:
	bool ustawParametry;

	float max;
	float maxPrzedzial;
	
	float przesun;

	int tryb5; 

	bool showAxisValue;

public:
	T5(FPS *);

private:
	int InitGL5(GLvoid);

	void drawString (void *, char *, float, float, float);

	float skaluj (float liczba, float max);

	void set_max();

public:
	int DrawGLScene5();

	void keyboard5(bool *keys);
};

#endif