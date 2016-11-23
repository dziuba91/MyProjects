#include "T1.h"
#include "DrawCube.h"

T1::T1()
{
	oddalenie1 = -9;
	speed1 = 0.06f;
	kat1 = 0;
	rquadX_1 = 0.0f;
	rquadY_1 = 0.0f;

	trybObrotu1 = 1;
}

int T1::InitGL1(GLvoid)										// All Setup For OpenGL Goes Here
{
	GLfloat ambient1[]={0.3, 0.3, 0.3, 0.0};				//Oœwietlenia
	GLfloat diffuse1[]={0.0, 0.0, 0.0, 0.5};
	GLfloat position1[]={15.0, 30.0, 0.0, 3.0}; 
	GLfloat specular1[]={0.0, 0.0, 0.0, 0.5}; 

	glLightfv(GL_LIGHT0, GL_AMBIENT, ambient1);
	glLightfv(GL_LIGHT0, GL_POSITION, position1);
	glLightfv(GL_LIGHT0, GL_SPECULAR, specular1);

	glEnable(GL_LIGHTING);
	glEnable(GL_LIGHT0); 
	glDisable(GL_FOG);

	return TRUE;										// Initialization Went OK
}

int T1::DrawGLScene1()									// Here's Where We Do All The Drawing
{
	InitGL1();

	glTranslatef(0, 1.2f, oddalenie1);		   // Oddal
	glRotatef(kat1, 1.0, 0.0, 0.0 );			// K¹t
	
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

	return TRUE;		 // Wszystko ok
}

void T1::keyboard1(bool *keys)
{
	if (keys[VK_RIGHT])		//kontrolki do testowania
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