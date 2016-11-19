#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>
#include <omp.h>
#include "calka.h"

float oddalenie = -3; //oddalenie widoku (oddalenie miejsca rysowania bry³y)

int numberOfQuotes=0;
bool goFunction = false;
bool calkuj = true;

void drawString (void * font, char * s, float x, float y, float z)
{
	unsigned int i, j;
	glRasterPos3f(x, y, z);

	for (i = 0; i < strlen (s); i++)
	{
		//glColor3f(0,255,255);
		glutBitmapCharacter (font, s[i]);
	}
}

float skaluj (float liczba, float max, int tryb)
{
	if (tryb == 0)
		return (2.0f/max)*liczba;
	if (tryb == 1)
		return (1.0f/max)*liczba;
}

int DrawGLScene(int tryb, float a, float b, float c)
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT); 
	glLoadIdentity();

	glTranslatef(0, 0.0f, oddalenie);

	glColor3f(0.0f,0.0f,0.0f); //blue color

	glPointSize(5.0f); //set point size to 10 pixels

	glBegin(GL_LINES);
	  glVertex3f(1.4f,0.0f,0.0f);
	  glVertex3f(-1.4f,0.0f,0.0f);
	glEnd();

	bool wykresY = false;

	if (dolnaGranica > 0 && gornaGranica > 0)
	{
		glBegin(GL_LINES);
			glVertex3f(-1.37f,1.0f,0.0f);
			glVertex3f(-1.37f,-1.0f,0.0f);
		glEnd();

		wykresY = true;
	}
	else if (dolnaGranica < 0 && gornaGranica < 0)
	{
		glBegin(GL_LINES);
			glVertex3f(1.37f,1.0f,0.0f);
			glVertex3f(1.37f,-1.0f,0.0f);
		glEnd();

		wykresY = true;
	}

	float lastPoint = 0;
	float lastSpace = 0;
	bool once = false;
	int j;
	float max = gornaGranica- dolnaGranica;
	float przesun = 0.0f;
	float point;
	float i;
	bool miganie= true;

	if (gornaGranica<dolnaGranica)
		return false;

	if (calkuj == true)
	{
		integral(a,b,c);
		calkuj = false;
	}

	float trybSkalowania = 0;

	if (maxY>max/2)
	{
		max = maxY;
		trybSkalowania = 1;
	}
	else if (abs(minY)>max/2)
	{
		max = abs(minY);
		trybSkalowania = 1;
	}

	for (j = 0; j <= iloscElementow; j++)
	{
		point = wynikY[j];
		i = wynikX[j];

		if(once)
		{
			if ((lastPoint>0 && point <0) || (lastPoint<0 && point >0))
			{
				glColor3f(0.0f,0.0f,1.0f);

				float a = (lastPoint- point)/(lastSpace- i);
				float b = (-lastSpace*a) + lastPoint;
				float y0 = - b/a;

				glBegin(GL_POLYGON);//begin drawing of polygon
					glVertex3f(skaluj(lastSpace,max,trybSkalowania)+przesun,0.0f,0.0f);
					glVertex3f(skaluj(lastSpace,max,trybSkalowania)+przesun,skaluj(lastPoint,max,trybSkalowania),0.0f);
					glVertex3f(skaluj(y0,max,trybSkalowania)+przesun,0.0f,0.0f);
				glEnd();//end drawing of polygon

				glBegin(GL_POLYGON);//begin drawing of polygon
					glVertex3f(skaluj(y0,max,trybSkalowania)+przesun,0.0f,0.0f);
					glVertex3f(skaluj(i,max,trybSkalowania)+przesun,0.0f,0.0f);
					glVertex3f(skaluj(i,max,trybSkalowania)+przesun,skaluj(point,max,trybSkalowania),0.0f);
				glEnd();//end drawing of polygon
			}
			else if (lastPoint == 0)
			{
				glColor3f(0.0f,1.0f,0.0f);

				glBegin(GL_POLYGON);//begin drawing of polygon
					glVertex3f(skaluj(lastSpace,max,trybSkalowania)+przesun,0.0f,0.0f);
					glVertex3f(skaluj(i,max,trybSkalowania)+przesun,0.0f,0.0f);
					glVertex3f(skaluj(i,max,trybSkalowania)+przesun,skaluj(point,max,trybSkalowania),0.0f);
				glEnd();//end drawing of polygon
			}
			else if (point == 0)
			{
				glColor3f(1.0f,0.0f,0.0f);

				glBegin(GL_POLYGON);//begin drawing of polygon
					glVertex3f(skaluj(lastSpace,max,trybSkalowania)+przesun,skaluj(lastPoint,max,trybSkalowania),0.0f);
					glVertex3f(skaluj(lastSpace,max,trybSkalowania)+przesun,0.0f,0.0f);
					glVertex3f(skaluj(i,max,trybSkalowania)+przesun,0.0f,0.0f);
				glEnd();//end drawing of polygon
			}
			else
			{
				if (tryb==1)
				{
					if (point > 0) glColor3f(0.0f,1.0f,0.0f);
					else glColor3f(1.0f,0.0f,0.0f);
				}
				else if (tryb==2)
				{
					if (miganie)
					{
						if (point > 0) glColor3f(0.0f,1.0f,0.0f);
						else glColor3f(1.0f,0.0f,0.0f);
						miganie = false;
					}
					else
					{
						if (point > 0) glColor3f(1.0f,0.0f,0.0f);
						else glColor3f(0.0f,1.0f,0.0f);
						miganie = true;
					}
				}
				else glColor3f(0.0f,0.0f,0.0f);

				glBegin(GL_POLYGON);//begin drawing of polygon
					glVertex3f(skaluj(lastSpace,max,trybSkalowania)+przesun,0.0f,0.0f);
					glVertex3f(skaluj(lastSpace,max,trybSkalowania)+przesun,skaluj(lastPoint,max,trybSkalowania),0.0f);
					glVertex3f(skaluj(i,max,trybSkalowania)+przesun,skaluj(point,max,trybSkalowania),0.0f);
					glVertex3f(skaluj(i,max,trybSkalowania)+przesun,0.0f,0.0f);
				glEnd();//end drawing of polygon
			}
		}
		else
		{
			if (skaluj(i,max,trybSkalowania)!=-1.0f)
			{
				przesun = -1.0f - skaluj(i,max,trybSkalowania);
			}

			char buf [100];
			sprintf(buf,"%.3f",i);
			if (skaluj(point,max,trybSkalowania)>0)
				drawString(GLUT_BITMAP_HELVETICA_18, buf, -1.25f, -0.12f, 0);
			else
				drawString(GLUT_BITMAP_HELVETICA_18, buf, -1.25f, 0.08f, 0);
		}
		once = true;

		//
		if (!wykresY)
		{
			if (point == 0 && i == 0)
			{
				glBegin(GL_LINES);
					glVertex3f(skaluj(i,max,trybSkalowania)+przesun,1.0f,0.0f);
					glVertex3f(skaluj(i,max,trybSkalowania)+przesun,-1.0f,0.0f);
				glEnd();

				wykresY = true;
			}
			else if (lastPoint == 0 && lastSpace == 0)
			{
				glBegin(GL_LINES);
					glVertex3f(skaluj(lastSpace,max,trybSkalowania)+przesun,1.0f,0.0f);
					glVertex3f(skaluj(lastSpace,max,trybSkalowania)+przesun,-1.0f,0.0f);
				glEnd();

				wykresY = true;
			}
			else if (lastSpace < 0 && i > 0)
			{
				float a = (lastPoint- point)/(lastSpace- i);
				float b = (-lastSpace*a) + lastPoint;
				float y0 = - b/a;

				glBegin(GL_LINES);
					glVertex3f(skaluj(y0,max,trybSkalowania)+przesun,1.0f,0.0f);
					glVertex3f(skaluj(y0,max,trybSkalowania)+przesun,-1.0f,0.0f);
				glEnd();

				wykresY = true;
			}
		}

		lastPoint = point;
		lastSpace = i;
	}

	//
	glColor3f(0.0f,0.0f,0.0f);

	char buf [100];
	sprintf(buf,"%.3f",wynikX[iloscElementow]);
	if (skaluj(point,max,trybSkalowania)>0)
		drawString(GLUT_BITMAP_HELVETICA_18, buf, 1.05f, -0.12f, 0);
	else
		drawString(GLUT_BITMAP_HELVETICA_18, buf, 1.05f, 0.08f, 0);

	//
	glColor3f(0.6f,0.6f,0.6f);

	char buf1 [100];
	sprintf(buf1,"%.3f",maxY);
	drawString(GLUT_BITMAP_HELVETICA_18, buf1, skaluj(maxX,max,trybSkalowania)+przesun, skaluj(maxY,max,trybSkalowania)+0.03f, 0);

	glBegin(GL_LINES);
		glVertex3f(-1.37f,skaluj(maxY,max,trybSkalowania),0.0f);
		glVertex3f(1.37f,skaluj(maxY,max,trybSkalowania),0.0f);
	glEnd();

	//
	glColor3f(0.6f,0.6f,0.6f);

	char buf2 [100];
	sprintf(buf2,"%.3f",minY);
	drawString(GLUT_BITMAP_HELVETICA_18, buf2, skaluj(minX,max,trybSkalowania)+przesun, skaluj(minY,max,trybSkalowania)-0.09f, 0);

	glBegin(GL_LINES);
		glVertex3f(-1.37f,skaluj(minY,max,trybSkalowania),0.0f);
		glVertex3f(1.37f,skaluj(minY,max,trybSkalowania),0.0f);
	glEnd();

	//
	glColor3f(0.0f,0.0f,1.0f);

	char buf3 [100];
	sprintf(buf3,"(P1) = %.3f",wynikCalkowania);
	drawString(GLUT_BITMAP_HELVETICA_18, buf3, -1.4f, 1.05f, 0.1f);

	//
	glColor3f(0.0f,0.0f,1.0f);

	char buf4 [100];
	sprintf(buf4,"(P2-OMP) = %.3f",wynikCalkowaniaOMP);
	drawString(GLUT_BITMAP_HELVETICA_18, buf4, -0.5f, 1.05f, 0.1f);

	//
	glColor3f(0.0f,0.0f,1.0f);

	char buf5 [100];
	sprintf(buf5,"(P3) = %.3f",wynikCalkowania2);
	drawString(GLUT_BITMAP_HELVETICA_18, buf5, 0.7f, 1.05f, 0.1f);

	//
	glColor3f(1.0f,0.0f,0.0f);

	char buf6 [100];
	sprintf(buf6,"(P4-OMP2) = %.3f",wynikCalkowaniaOMP2);
	drawString(GLUT_BITMAP_HELVETICA_18, buf6, -1.4f, -1.05f, 0.1f);

	return TRUE;		 // Wszystko ok
}