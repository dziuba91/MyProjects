#ifndef __T1_H		// prevent from multiple compiling
#define __T1_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>
//#include "draw_cube.h"

class T1
{

private:
	float oddalenie1;
	GLfloat speed1;
	GLfloat kat1; //35
	GLfloat rquadX_1;
	GLfloat rquadY_1;

	int trybObrotu1;

public:
	T1()
	{
		oddalenie1 = -9;
	    speed1 = 0.06f;
	    kat1 = 0; //35
	    rquadX_1 = 0.0f;
	    rquadY_1 = 0.0f;

		trybObrotu1 = 1;
	}

public:
	int InitGL1(GLvoid)										// All Setup For OpenGL Goes Here
	{
		//glShadeModel(GL_SMOOTH);							// Enable Smooth Shading
		//glClearColor(0.0f, 0.0f, 0.0f, 0.5f);				// Black Background
		//glClearDepth(1.0f);									// Depth Buffer Setup
		//glEnable(GL_DEPTH_TEST);							// Enables Depth Testing
		//glDepthFunc(GL_LEQUAL);								// The Type Of Depth Testing To Do
		//glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);	// Really Nice Perspective Calculations

		GLfloat ambient1[]={0.3, 0.3, 0.3, 0.0};			//Oœwietlenia
		GLfloat diffuse1[]={0.0, 0.0, 0.0, 0.5};
		GLfloat position1[]={15.0, 30.0, 0.0, 3.0}; 
		GLfloat specular1[]={0.0, 0.0, 0.0, 0.5}; 

		glLightfv(GL_LIGHT0, GL_AMBIENT, ambient1);
		//glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuse1);
		glLightfv(GL_LIGHT0, GL_POSITION, position1);
		glLightfv(GL_LIGHT0, GL_SPECULAR, specular1);

		glEnable(GL_LIGHTING);
		glEnable(GL_LIGHT0); 
		glDisable(GL_FOG);
		//glDisable(GL_LIGHT0);
	
		return TRUE;										// Initialization Went OK
	}

	int DrawGLScene1()									// Here's Where We Do All The Drawing
	{
		InitGL1();

		glTranslatef(0, 1.2f, oddalenie1);         // Oddal
		glRotatef(kat1, 1.0, 0.0, 0.0 );            // K¹t
	
		glRotatef(rquadX_1,0,1.0f,0); 
		glRotatef(rquadY_1,1.0f,0,0); 

		if (trybObrotu1 == 2) 
			rquadX_1-=speed1;
		if (trybObrotu1 == 1) 
			rquadX_1+=speed1;
		if (trybObrotu1 == 3) 
			rquadY_1-=speed1;
		if (trybObrotu1 == 4) 
			rquadY_1+=speed1;

		DrawCube(1.0);

	    return TRUE;         // Wszystko ok
	}

	void keyboard1(bool *keys)
	{
		if (keys[VK_RIGHT])     //kontrolki do testowania
		{
			trybObrotu1=1;
		}
		else if (keys[VK_LEFT])
		{
			trybObrotu1=2;
		}
		else if (keys[VK_UP])
		{
			trybObrotu1=3;
		}
		else if (keys[VK_DOWN])
		{
			trybObrotu1=4;			
		}
	}
};

#endif