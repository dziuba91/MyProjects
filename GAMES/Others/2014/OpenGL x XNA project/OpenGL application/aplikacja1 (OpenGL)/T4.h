#ifndef __T4_H
#define __T4_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>

class T4
{
private:
	bool sim;

	float przesuniecie4;
	float oddalenie4;
	float odl4;
	GLfloat speed4;
	GLfloat kat4;
	GLfloat rquadX_4;
	GLfloat rquadY_4;

	int trybObrotu4;

	bool loadTexture;

	GLfloat rotatuj;

	GLuint	texture[6];

	GLfloat rot [27][3];

	int rot_nr [27][1000];
	int licznik [27];
	
	int tab[3][3];

	GLfloat max4;
	bool wait4;

	bool wait_sim;
	int time_sim;

	int ilosc4;
	
public:
	T4();

private:
	AUX_RGBImageRec *LoadBMP(char *);

	int LoadGLTextures();

	void DrawTexturedCube();

	void Tryb4(int);

	int InitGL4(GLvoid);
	
	void simulation();

public:
	int DrawGLScene4();

	void keyboard4(bool *keys);
};

#endif