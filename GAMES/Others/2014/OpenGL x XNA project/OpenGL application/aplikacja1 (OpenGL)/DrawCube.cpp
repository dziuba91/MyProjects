#include "DrawCube.h"

static GLfloat material[]={0.0, 0.0, 0.8, 0.0};			//kolory materia³ów

void DrawCube(float X)
{
	glBegin(GL_QUADS);		   // Zacznij rysowanie szeœcianu
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material);

		glNormal3f( 0.0f, 0.0f, 1.0f);
		glVertex3f(-X, -X,	X);
		glVertex3f( X, -X,	X);
		glVertex3f( X,	X,	X);
		glVertex3f(-X,	X,	X);

		glNormal3f( 0.0f, 0.0f,-1.0f);
		glVertex3f(-X, -X, -X);
		glVertex3f(-X,	X, -X);
		glVertex3f( X,	X, -X);
		glVertex3f( X, -X, -X);
		
		glNormal3f( 0.0f, 1.0f, 0.0f);
		glVertex3f(-X,	X, -X);
		glVertex3f(-X,	X,	X);
		glVertex3f( X,	X,	X);
		glVertex3f( X,	X, -X);

		glNormal3f( 0.0f,-1.0f, 0.0f);
		glVertex3f(-X, -X, -X);
		glVertex3f( X, -X, -X);
		glVertex3f( X, -X,	X);
		glVertex3f(-X, -X,	X);

		glNormal3f( 1.0f, 0.0f, 0.0f);
		glVertex3f( X, -X, -X);
		glVertex3f( X,	X, -X);
		glVertex3f( X,	X,	X);
		glVertex3f( X, -X,	X);

		glNormal3f(-1.0f, 0.0f, 0.0f);
		glVertex3f(-X, -X, -X);
		glVertex3f(-X, -X,	X);
		glVertex3f(-X,	X,	X);
		glVertex3f(-X,	X, -X); 
	glEnd();		 // Zakoñcz rysowanie czworok¹ta
}