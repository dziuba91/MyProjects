#include "t5.h"

	T5::T5(FPS * fps1)
	{
		ustawParametry = true;
	
		przesun = 0.4f;

		tryb5 = 0; 

		showAxisValue = true;

		this->fps1 = fps1;
	}

	int T5::InitGL5(GLvoid)										// All Setup For OpenGL Goes Here
	{
		glClearColor(0.9f, 0.9f, 0.9f, 0.5f);

		glDisable(GL_LIGHTING);
		glDisable(GL_LIGHT0);
		glDisable(GL_FOG);
			
		return TRUE;										// Initialization Went OK
	}

	void T5::drawString (void * font, char * s, float x, float y, float z){
		unsigned int i, j;
		glRasterPos3f(x, y, z);

		 for (i = 0; i < strlen (s); i++)
		 {
			  //glColor3f(0,255,255);
			  glutBitmapCharacter (font, s[i]);
		 }
	}

	float T5::skaluj (float liczba, float max)
	{
		return (1.6f/max)*liczba;
	}

	void T5::set_max()
	{
		max = 0;
	
		for (int i = 0; i < fps1->FPS_ARRAY_LENGHT; i++)
		{
			if (tryb5 == 0)
			{
				if (max< fps1->fpsArray[i])
				{
					max= fps1->fpsArray[i];
				}
			}
			else if (tryb5 == 1)
			{
				if (max< fps1->fpsArray1[i])
				{
					max= fps1->fpsArray1[i];
				}	
			}
			else if (tryb5 == 2)
			{
				if (max< fps1->fpsArray2[i])
				{
					max= fps1->fpsArray2[i];
				}
			}
			else if (tryb5 == 3)
			{
				if (max< fps1->fpsArray3[i])
				{
					max= fps1->fpsArray3[i];
				}
			}
			if (tryb5 == 4)
			{
				if (max< fps1->fpsArray4[i])
				{
					max= fps1->fpsArray4[i];
				}
			}
			if (tryb5 == 5)
			{
				if (max< fps1->fpsArray5[i])
				{
					max= fps1->fpsArray5[i];
				}
			}
			if (tryb5 == 10)
			{
				int granica = i / 20;
					
				if (granica == 0)
				{
					if (max< fps1->fpsArray1[i])
					{
						max= fps1->fpsArray1[i];
					}
				}
				else if (granica == 1)
				{
					if (max< fps1->fpsArray2[i-20])
					{
						max= fps1->fpsArray2[i-20];
					}
				}
				else if (granica == 2)
				{
					if (max< fps1->fpsArray3[i-40])
					{
						max= fps1->fpsArray3[i-40];
					}
				}
				else if (granica == 3)
				{
					if (max< fps1->fpsArray4[i-60])
					{
						max= fps1->fpsArray4[i-60];
					}
				}
				else if (granica == 4)
				{
					if (max< fps1->fpsArray5[i-80])
					{
						max= fps1->fpsArray5[i-80];
					}
				}
			}
		}
	}

	int T5::DrawGLScene5()									// Here's Where We Do All The Drawing
	{
		InitGL5();
		
		float oddalenie = -3;
		float offset = -0.9f;

		glTranslatef(0, przesun, oddalenie);		 // Oddal
	
		glColor3f(0.0f,0.0f,0.0f); //blue color

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

		for (j = 0; j < fps1->FPS_ARRAY_LENGHT; j++)
		{
			if (tryb5 == 0)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fps1->fpsArray[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 1)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fps1->fpsArray1[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 2)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fps1->fpsArray2[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 3)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fps1->fpsArray3[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 4)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fps1->fpsArray4[j],max) + offset;

				glBegin(GL_POINTS); //starts drawing of points
					glVertex3f(pointX,pointY,0.0f);//upper-right corner
				glEnd();
			}
			else if (tryb5 == 5)
			{
				pointX = (j + 1)*0.025f - 1.2f;
				pointY = skaluj(fps1->fpsArray5[j],max) + offset;

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
					pointY = skaluj(fps1->fpsArray1[licznik],max) + offset;
				}
				else if (granica == 1)
				{
					glColor3f(1.0f,0.0f,1.0f);
					pointY = skaluj(fps1->fpsArray2[licznik],max) + offset;
				}
				else if (granica == 2)
				{
					glColor3f(0.5f,0.5f,0.0f);
					pointY = skaluj(fps1->fpsArray3[licznik],max) + offset;
				}
				else if (granica == 3)
				{
					glColor3f(0.0f,0.5f,0.0f);
					pointY = skaluj(fps1->fpsArray4[licznik],max) + offset;
				}
				else if (granica == 4)
				{
					glColor3f(0.0f,0.5f,0.5f);
					pointY = skaluj(fps1->fpsArray5[licznik],max) + offset;
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

		glColor3f(0.3f,0.3f,0.3f);

		char buf1 [100];
		sprintf(buf1,"%.f",max);
		drawString(GLUT_BITMAP_HELVETICA_18, buf1, 0.0f, skaluj(max,max)+0.03f + offset, 0);
	
		glBegin(GL_LINES);
			glVertex3f(-1.37f,skaluj(max,max) + offset,0.0f);
			glVertex3f(1.37f,skaluj(max,max) + offset,0.0f);
		glEnd();

		glColor3f(0.0f,0.0f,1.0f);
	
		char buf3 [100];
		if (tryb5 == 0) sprintf(buf3,"TRYB = 0");
		else sprintf(buf3,"TRYB = %.d",tryb5);
		drawString(GLUT_BITMAP_HELVETICA_18, buf3, -1.4f, 1.05f, 0.1f);
	

		return TRUE;		 // Wszystko ok
	}

	void T5::keyboard5(bool *keys)
	{
		if (keys['Q'])	   //kontrolki do testowania
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