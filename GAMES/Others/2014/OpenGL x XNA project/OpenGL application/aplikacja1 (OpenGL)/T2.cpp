#include "T2.h"
#include "DrawCube.h"

T2::T2()
{
	przesuniecie2 = 4.0f;
	oddalenie2 = -45;
	speed2 = 0.1f;
	kat2 = 0; //35
	rquadX_2 = 0.0f;
	rquadY_2 = 0.0f;

	trybObrotu2 = 1;

	ilosc=7;
	odl = 3.0f;
}

int T2::InitGL2(GLvoid)										// All Setup For OpenGL Goes Here
{
	GLfloat ambient2[]={0.3, 0.3, 0.3, 0.0};			//Oœwietlenia
	GLfloat diffuse2[]={0.0, 0.0, 0.0, 0.5};
	GLfloat position2[]={15.0, 30.0, 0.0, 3.0}; 
	GLfloat specular2[]={0.0, 0.0, 0.0, 0.5};  
	
	glLightfv(GL_LIGHT0, GL_AMBIENT, ambient2);
	glLightfv(GL_LIGHT0, GL_POSITION, position2);
	glLightfv(GL_LIGHT0, GL_SPECULAR, specular2);

	glEnable(GL_LIGHTING);
	glEnable(GL_LIGHT0); 
	glDisable(GL_FOG);
	
	return TRUE;										// Initialization Went OK
}

int T2::DrawGLScene2()									// Here's Where We Do All The Drawing
{
	InitGL2();

	int pol = ilosc/2;
	
	for(int k=0; k<ilosc; k++)
	{
		for(int i=0; i<ilosc; i++)
		{
			for(int j=0; j<ilosc; j++)
			{
				int a=j;
				if (a>pol) 
				{
					a-=pol;
					a*=-1;
				}
				//if (a>2) { a--; a= a*(-1); }
				
				int b=k;
				if (b>pol) 
				{
					b-=pol;
					b*=-1;
				}
				//if (b>2) { b--; b= b*(-1); }
				
				int c=i;
				if (c>pol) 
				{
					c-=pol;
					c*=-1;
				}
				//if (c>2) { c--; c= c*(-1); }


				glLoadIdentity();

				glTranslatef(0, przesuniecie2, oddalenie2);         // Oddal
				glRotatef(kat2, 1.0, 0.0, 0.0 );            // K¹t
	
				glRotatef(rquadX_2,0,1.0f,0); 
				glRotatef(rquadY_2,1.0f,0,0); 


				glTranslatef(odl*a, odl*b, odl*c);

				DrawCube(1.0);

				//glTranslatef(odl,0.0f,0.0f);    
			}
		}
	}

	if (trybObrotu2 == 2) 
		rquadX_2-=speed2;
	if (trybObrotu2 == 1) 
		rquadX_2+=speed2;
	if (trybObrotu2 == 3) 
		rquadY_2-=speed2;
	if (trybObrotu2 == 4) 
		rquadY_2+=speed2;

	DrawCube(1.0);

	return TRUE;         // Wszystko ok
}

void T2::keyboard2(bool *keys)
{
	if (keys[VK_RIGHT])     //kontrolki do testowania
	{
		trybObrotu2=1;
	}
	else if (keys[VK_LEFT])
	{
		trybObrotu2=2;
	}
	else if (keys[VK_UP])
	{
		trybObrotu2=3;
	}
	else if (keys[VK_DOWN])
	{
		trybObrotu2=4;			
	}
}