
#include "test_parameters_draw.h"

	TestParametrsDraw::TestParametrsDraw(FPS * fps1, TimeCounter * time1)
	{
		oddalenie0 = -1.0f;
		maxWindow = 0.415f;

		this->fps1 = fps1;
		this->time1 = time1;
	}

	int TestParametrsDraw::InitGL0(GLvoid)										// All Setup For OpenGL Goes Here
	{
		glShadeModel(GL_SMOOTH);							// Enable Smooth Shading
		glClearColor(0.0f, 0.0f, 0.0f, 0.5f);				// Black Background
		glClearDepth(1.0f);									// Depth Buffer Setup
		glEnable(GL_DEPTH_TEST);							// Enables Depth Testing
		glDepthFunc(GL_LEQUAL);								// The Type Of Depth Testing To Do
		glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);	// Really Nice Perspective Calculations

		glDisable(GL_LIGHTING);
		glDisable(GL_LIGHT0); 
		
		glDisable(GL_TEXTURE_2D);
	
		return TRUE;										// Initialization Went OK
	}

	void TestParametrsDraw::drawString0 (void * font, char * s, float x, float y, float z){
	     unsigned int i, j;
	     glRasterPos3f(x, y, z);

	     for (i = 0; i < strlen (s); i++)
		 {
			  //glColor3f(0,255,255);
	          glutBitmapCharacter (font, s[i]);
		 }
	}

	int TestParametrsDraw::DrawGLScene(int tryb, bool timePause)									// Here's Where We Do All The Drawing
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
		sprintf(buf2,"fps= %d",fps1->fps);
		drawString0(GLUT_BITMAP_HELVETICA_18, buf2, -maxWindow+0.1f, -maxWindow+areaHeight-0.045f, 0.1f);
	
		char buf3 [100];
		if (!timePause)
			sprintf(buf3,"section1= %d[ms]", time1->averageTime);
		else
			sprintf(buf3,"section1= %d[ms]   (pause)", time1->averageTime);
		drawString0(GLUT_BITMAP_HELVETICA_18, buf3, -maxWindow+0.1f, -maxWindow+areaHeight-0.08f, 0.1f);

		char buf4 [100];
		sprintf(buf4,"             (t1= %d; tn= %d; tmin= %d; tmax= %d)", time1->startTime, time1->stopTime, time1->minTime, time1->maxTime);
		drawString0(GLUT_BITMAP_HELVETICA_18, buf4, -maxWindow+0.1f, -maxWindow+areaHeight-0.11f, 0.1f);
	
	    return TRUE;         // Wszystko ok
	}