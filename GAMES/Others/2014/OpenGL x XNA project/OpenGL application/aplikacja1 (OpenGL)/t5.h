#ifndef __T5_H		// prevent from multiple compiling
#define __T5_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>


class T5
{

private:
	bool ustawParametry;

	float max;
	float maxPrzedzial;
	
	float przesun;

	int tryb5; 

	bool showAxisValue;

public:
	T5()
	{
		ustawParametry = true;

		max;
		maxPrzedzial;
	
		przesun = 0.4f;

		tryb5 = 0; 

		showAxisValue = true;
	}

private:
	int InitGL5(GLvoid)										// All Setup For OpenGL Goes Here
	{
		glClearColor(0.9f, 0.9f, 0.9f, 0.5f);
		//glShadeModel(GL_SMOOTH);							// Enable Smooth Shading
		//glClearColor(255.0f, 255.0f, 255.0f, 0.5f);				// Black Background
		//glClearDepth(1.0f);									// Depth Buffer Setup
		//glEnable(GL_DEPTH_TEST);							// Enables Depth Testing
		//glDepthFunc(GL_LEQUAL);								// The Type Of Depth Testing To Do
		//glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);	// Really Nice Perspective Calculations
		
		//glLightfv(GL_LIGHT0, GL_AMBIENT, ambient);
		//glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuse);
		//glLightfv(GL_LIGHT0, GL_POSITION, position);
		//glLightfv(GL_LIGHT0, GL_SPECULAR, specular);
			
		//glEnable(GL_LIGHTING);
		//glEnable(GL_LIGHT0); 
		glDisable(GL_LIGHTING);
		glDisable(GL_LIGHT0);
		glDisable(GL_FOG);
			
		return TRUE;										// Initialization Went OK
	}

	/*
	int InitGL5(GLvoid)										// All Setup For OpenGL Goes Here
	{
		glShadeModel(GL_SMOOTH);							// Enable Smooth Shading
		glClearColor(255.0f, 255.0f, 255.0f, 0.5f);				// Black Background
		glClearDepth(1.0f);									// Depth Buffer Setup
		glEnable(GL_DEPTH_TEST);							// Enables Depth Testing
		glDepthFunc(GL_LEQUAL);								// The Type Of Depth Testing To Do
		glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);	// Really Nice Perspective Calculations
	
		glEnable(GL_LIGHTING);
		glEnable(GL_LIGHT0); 
		
		return TRUE;										// Initialization Went OK
	}
	*/

	void drawString (void * font, char * s, float x, float y, float z){
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

	float skaluj (float liczba, float max)
	{
		return (1.6f/max)*liczba;
	}

	void set_max()
	{
		max = 0;
	
		for (int i = 0; i < FPS_ARRAY_LENGHT; i++)
		{
			if (tryb5 == 0)
			{
				if (max< fpsArray[i])
				{
					max= fpsArray[i];
				}
			}
			else if (tryb5 == 1)
			{
				if (max< fpsArray1[i])
				{
					max= fpsArray1[i];
				}	
			}
			else if (tryb5 == 2)
			{
				if (max< fpsArray2[i])
				{
					max= fpsArray2[i];
				}
			}
			else if (tryb5 == 3)
			{
				if (max< fpsArray3[i])
				{
					max= fpsArray3[i];
				}
			}
			if (tryb5 == 4)
			{
				if (max< fpsArray4[i])
				{
					max= fpsArray4[i];
				}
			}
			if (tryb5 == 5)
			{
				if (max< fpsArray5[i])
				{
					max= fpsArray5[i];
				}
			}
			if (tryb5 == 10)
			{
				int granica = i / 20;
					
				if (granica == 0)
				{
					if (max< fpsArray1[i])
					{
						max= fpsArray1[i];
					}
				}
				else if (granica == 1)
				{
					if (max< fpsArray2[i-20])
					{
						max= fpsArray2[i-20];
					}
				}
				else if (granica == 2)
				{
					if (max< fpsArray3[i-40])
					{
						max= fpsArray3[i-40];
					}
				}
				else if (granica == 3)
				{
					if (max< fpsArray4[i-60])
					{
						max= fpsArray4[i-60];
					}
				}
				else if (granica == 4)
				{
					if (max< fpsArray5[i-80])
					{
						max= fpsArray5[i-80];
					}
				}
			}
		}
	}

public:
	int DrawGLScene5()									// Here's Where We Do All The Drawing
	{
		InitGL5();
		
		float oddalenie = -3;
		float offset = -0.9f;
	
		//glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT); 

		//glLoadIdentity();
	

		glTranslatef(0, przesun, oddalenie);         // Oddal
	
		glColor3f(0.0f,0.0f,0.0f); //blue color
    
		//drawString(GLUT_BITMAP_HELVETICA_18, "RysujemyStart!", -1.2f, 1.0f, 0);

	    glPointSize(3.0f);//set point size to 10 pixels
    
		// wykres X
		glBegin(GL_LINES);
		 	glVertex3f(1.4f,offset,0.0f);
			glVertex3f(-1.4f,offset,0.0f);
		glEnd();

		float arrow = 0.06f;
		glBegin(GL_LINES);
			glVertex3f(1.4f,offset,0.0f);
			glVertex3f(1.4f - arrow,offset + arrow,0.0f);
		glEnd();

		glBegin(GL_LINES);
			glVertex3f(1.4f,offset,0.0f);
			glVertex3f(1.4f - arrow,offset - arrow,0.0f);
		glEnd();

		// wykres Y
		glBegin(GL_LINES);
			glVertex3f(-1.37f,1.0f,0.0f);
			glVertex3f(-1.37f,-1.1f,0.0f);
		glEnd();

		glBegin(GL_LINES);
			glVertex3f(-1.37f,1.0f,0.0f);
			glVertex3f(-1.37f + arrow,1.0f - arrow,0.0f);
		glEnd();

		glBegin(GL_LINES);
			glVertex3f(-1.37f,1.0f,0.0f);
			glVertex3f(-1.37f - arrow,1.0f - arrow,0.0f);
		glEnd();


		int j;
		float przesun = 0.0f;
		float i;
		bool setLine = false;
		float pointY, pointX;
		float previousPointY, previousPointX;
		int granica;
		int licznik = 0;
	
		if(ustawParametry || tryb5 == 0 || tryb5 == 5 || tryb5 == 10)
		{
			set_max();

			ustawParametry = false;
		}

		glColor3f(0.65f,0.65f,0.65f);

		if (showAxisValue && max > 3)
		{
			int ilosc_przedzialow = 6;
			float przedzial = 1.6f/ ilosc_przedzialow;
			char buf11 [100];

			for (int i = 1; i< ilosc_przedzialow; i++)
			{
				sprintf(buf11,"%.d",(int)max/ilosc_przedzialow*i);
				drawString(GLUT_BITMAP_HELVETICA_12, buf11, -1.52f, przedzial*i + offset + 0.025f, 0);
					
				glBegin(GL_LINES);
					glVertex3f(-1.42f,przedzial*i + offset,0.0f);
					glVertex3f(1.3f,przedzial*i + offset,0.0f);
				glEnd();
			}
		}

		glColor3f(0.0f,0.0f,1.0f);

		for (j = 0; j < FPS_ARRAY_LENGHT; j++)
		{
			//char buf[10];
			if (tryb5 == 0)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fpsArray[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 1)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fpsArray1[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 2)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fpsArray2[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 3)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fpsArray3[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 4)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fpsArray4[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 5)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fpsArray5[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 10)
			{
				granica = j/20;
				pointX = (j + 1)*0.025f - 1.2f;
				char buf10 [100];
					
				if (granica == 0)
				{
					glColor3f(0.5f,0.0f,0.5f); 
					pointY = skaluj(fpsArray1[licznik],max) + offset;
				}
				else if (granica == 1)
				{
					glColor3f(1.0f,0.0f,1.0f);
					pointY = skaluj(fpsArray2[licznik],max) + offset;
				}
				else if (granica == 2)
				{
					glColor3f(0.5f,0.5f,0.0f);
					pointY = skaluj(fpsArray3[licznik],max) + offset;
				}
				else if (granica == 3)
				{
					glColor3f(0.0f,0.5f,0.0f);
					pointY = skaluj(fpsArray4[licznik],max) + offset;
				}
				else if (granica == 4)
				{
					glColor3f(0.0f,0.5f,0.5f);
					pointY = skaluj(fpsArray5[licznik],max) + offset;
				}

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
	
				if (licznik != 0)
				{
					glBegin(GL_LINES);
						glVertex3f(pointX,pointY,0.0f);
						glVertex3f(previousPointX,previousPointY,0.0f);
					glEnd();
				}
				else if (granica !=0)
				{
					glColor3f(0.6f,0.6f,0.6f);
						
					glBegin(GL_LINES);
						glVertex3f((pointX+previousPointX)/2,skaluj(max,max) + offset,0.0f);
						glVertex3f((pointX+previousPointX)/2,offset,0.0f);
					glEnd();
				}

				if (licznik == 9)
				{
					sprintf(buf10,"%.d",granica +1);
					drawString(GLUT_BITMAP_HELVETICA_18, buf10, pointX, offset - 0.12f, 0);
				}

				licznik++;
				if (licznik == 20) licznik = 0;
			}

			if (setLine && tryb5 != 10)
		    {
	            glBegin(GL_LINES);
					glVertex3f(pointX,pointY,0.0f);
					glVertex3f(previousPointX,previousPointY,0.0f);
				glEnd();
			}

			previousPointX = pointX;
			previousPointY = pointY;
			setLine = true;
		}	

		/*
		glColor3f(0.0f,0.0f,0.0f);
			
		char buf [100];
		sprintf(buf,"%.3f",wynikX[iloscElementow]);
		if (skaluj(point,max,trybSkalowania)>0)
			drawString(GLUT_BITMAP_HELVETICA_18, buf, 1.05f, -0.12f, 0);
		else
			drawString(GLUT_BITMAP_HELVETICA_18, buf, 1.05f, 0.08f, 0);
		*/
			
		glColor3f(0.3f,0.3f,0.3f);

		char buf1 [100];
		sprintf(buf1,"%.f",max);
		drawString(GLUT_BITMAP_HELVETICA_18, buf1, 0.0f, skaluj(max,max)+0.03f + offset, 0);
	
		glBegin(GL_LINES);
			glVertex3f(-1.37f,skaluj(max,max) + offset,0.0f);
			glVertex3f(1.37f,skaluj(max,max) + offset,0.0f);
		glEnd();


		/*
		glColor3f(0.6f,0.6f,0.6f);

		char buf2 [100];
		sprintf(buf2,"%.3f",minY);
		drawString(GLUT_BITMAP_HELVETICA_18, buf2, skaluj(minX,max,trybSkalowania)+przesun, skaluj(minY,max,trybSkalowania)-0.09f, 0);
	
		glBegin(GL_LINES);
			glVertex3f(-1.37f,skaluj(minY,max,trybSkalowania),0.0f);
			glVertex3f(1.37f,skaluj(minY,max,trybSkalowania),0.0f);
		glEnd();
		*/

		glColor3f(0.0f,0.0f,1.0f);
	
		char buf3 [100];
		if (tryb5 == 0) sprintf(buf3,"TRYB = 0");
		else sprintf(buf3,"TRYB = %.d",tryb5);
		drawString(GLUT_BITMAP_HELVETICA_18, buf3, -1.4f, 1.05f, 0.1f);
	

	    return TRUE;         // Wszystko ok
	}

	void keyboard5(bool *keys)
	{
		if (keys['Q'])     //kontrolki do testowania
		{
			tryb5=0;
			ustawParametry = true;
		}
		else if (keys['W'])
		{
			tryb5=1;
			ustawParametry = true;
		}
		else if (keys['E'])
		{
			tryb5=2;
			ustawParametry = true;
		}
		else if (keys['R'])
		{
			tryb5=3;	
			ustawParametry = true;
		}
		else if (keys['T'])
		{
			tryb5=4;		
			ustawParametry = true;
		}
		else if (keys['Y'])
		{
			tryb5=5;		
			ustawParametry = true;
		}
		else if (keys['Z'])
		{
			tryb5=10;		
			ustawParametry = true;
		}
	}
};

#endif