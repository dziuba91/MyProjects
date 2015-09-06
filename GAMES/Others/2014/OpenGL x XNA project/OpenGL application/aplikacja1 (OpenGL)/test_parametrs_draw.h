#ifndef __TEST_PARAMETRS_DRAW_H		// prevent from multiple compiling
#define __TEST_PARAMETRS_DRAW_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
//#include <gl\glut.h>
#include <math.h>
#include <stdio.h>


class TestParametrsDraw
{

private:
	float oddalenie0;
	float maxWindow;

public:
	TestParametrsDraw()
	{
		oddalenie0 = -1.0f;
		maxWindow = 0.415f;
	}

private:
	int InitGL0(GLvoid)										// All Setup For OpenGL Goes Here
	{
		glShadeModel(GL_SMOOTH);							// Enable Smooth Shading
		glClearColor(0.0f, 0.0f, 0.0f, 0.5f);				// Black Background
		glClearDepth(1.0f);									// Depth Buffer Setup
		glEnable(GL_DEPTH_TEST);							// Enables Depth Testing
		glDepthFunc(GL_LEQUAL);								// The Type Of Depth Testing To Do
		glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);	// Really Nice Perspective Calculations


		//glLightfv(GL_LIGHT0, GL_AMBIENT, ambient);
		//glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuse);
		//glLightfv(GL_LIGHT0, GL_POSITION, position);
		//glLightfv(GL_LIGHT0, GL_SPECULAR, specular);

		glDisable(GL_LIGHTING);
		glDisable(GL_LIGHT0); 
		
		glDisable(GL_TEXTURE_2D);
		//glDisable(GL_LIGHT0);
	
		return TRUE;										// Initialization Went OK
	}

	void drawString0 (void * font, char * s, float x, float y, float z){
	     unsigned int i, j;
	     glRasterPos3f(x, y, z);

		 //strcpy(quote[0],"Luke, I am your father!.");
		 //numberOfQuotes = 1;

	     for (i = 0; i < strlen (s); i++)
		 {
			  //glColor3f(0,255,255);
	          glutBitmapCharacter (font, s[i]);
		 }
	}

public:
	int DrawGLScene()									// Here's Where We Do All The Drawing
	{
		glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT); 
	
		glLoadIdentity();

		InitGL0();

		float areaHeight = (2* maxWindow)/4;

		glColor3f(1.0f,1.0f,1.0f);

		glTranslatef(0, 0.0f, oddalenie0); 

		glBegin(GL_POLYGON);//begin drawing of polygon
			glVertex3f(maxWindow,-maxWindow,0.0f);
			glVertex3f(maxWindow,-maxWindow+areaHeight,0.0f);
			glVertex3f(-maxWindow,-maxWindow+areaHeight,0.0f);
			glVertex3f(-maxWindow,-maxWindow,0.0f);
		glEnd();//end drawing of polygon
	
		glColor3f(0.0f,0.0f,0.0f);

		char buf1 [100];
		sprintf(buf1,"TRYB: %d", tryb);
		drawString0(GLUT_BITMAP_HELVETICA_18, buf1, -maxWindow+0.1f, -maxWindow+areaHeight-0.01f, 0.1f);

		char buf2 [100];
		sprintf(buf2,"fps= %d",fps);
		drawString0(GLUT_BITMAP_HELVETICA_18, buf2, -maxWindow+0.1f, -maxWindow+areaHeight-0.045f, 0.1f);
	
		char buf3 [100];
		if (!timePause)
			sprintf(buf3,"section1= %d[ms]", meanTime);
		else
			sprintf(buf3,"section1= %d[ms]   (pause)", meanTime);
		drawString0(GLUT_BITMAP_HELVETICA_18, buf3, -maxWindow+0.1f, -maxWindow+areaHeight-0.08f, 0.1f);

		char buf4 [100];
		sprintf(buf4,"             (t1= %d; tn= %d; tmin= %d; tmax= %d)", startTime, stopTime, minTime, maxTime);
		drawString0(GLUT_BITMAP_HELVETICA_18, buf4, -maxWindow+0.1f, -maxWindow+areaHeight-0.11f, 0.1f);
	
	    return TRUE;         // Wszystko ok
	}
};

#endif