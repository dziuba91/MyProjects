#ifndef __T3_H		// prevent from multiple compiling
#define __T3_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>
//#include "draw_cube.h"

class T3
{

private:
	float oddalenie3;
	GLfloat speed3;
	GLfloat kat3; //35
	GLfloat rquadX_3;
	GLfloat rquadY_3;

	int trybObrotu3;

	bool loadTexture3;

	GLuint	texture3;

	bool fog;

	DWORD time3;
	bool wait3;

public:
	T3()
	{
		oddalenie3 = -9;
		speed3 = 0.06f;
		kat3 = 0; //35
		rquadX_3 = 0.0f;
		rquadY_3 = 0.0f;

		trybObrotu3 = 1;

		loadTexture3= false;

		texture3;

		fog = false;

		time3 = GetTickCount();
		wait3 = FALSE;
	}

private:
	AUX_RGBImageRec *LoadBMP(char *Filename)				// Loads A Bitmap Image
	{
		FILE *File=NULL;									// File Handle

		if (!Filename)										// Make Sure A Filename Was Given
		{
			return NULL;									// If Not Return NULL
		}

		File=fopen(Filename,"r");							// Check To See If The File Exists

		if (File)											// Does The File Exist?
		{
			fclose(File);									// Close The Handle
			return auxDIBImageLoad(Filename);				// Load The Bitmap And Return A Pointer
		}

		return NULL;										// If Load Failed Return NULL
	}

	int LoadGLTextures3()									// Load Bitmaps And Convert To Textures
	{
		int Status=FALSE;									// Status Indicator

		AUX_RGBImageRec *TextureImage[1];					// Create Storage Space For The Texture

		memset(TextureImage,0,sizeof(void *)*1);           	// Set The Pointer To NULL

		char text[20];
		// Load The Bitmap, Check For Errors, If Bitmap's Not Found Quit

		sprintf(text,"Data/texture.bmp");
		if (TextureImage[0]=LoadBMP(text))
		{
			Status=TRUE;									// Set The Status To TRUE

			glGenTextures(1, &texture3);					// Create The Texture

				// Typical Texture Generation Using Data From The Bitmap
			glBindTexture(GL_TEXTURE_2D, texture3);
			glTexImage2D(GL_TEXTURE_2D, 0, 3, TextureImage[0]->sizeX, TextureImage[0]->sizeY, 0, GL_RGB, GL_UNSIGNED_BYTE, TextureImage[0]->data);
			glTexParameteri(GL_TEXTURE_2D,GL_TEXTURE_MIN_FILTER,GL_LINEAR);
			glTexParameteri(GL_TEXTURE_2D,GL_TEXTURE_MAG_FILTER,GL_LINEAR);
		}

		if (TextureImage[0])									// If Texture Exists
		{
			if (TextureImage[0]->data)							// If Texture Image Exists
			{
				free(TextureImage[0]->data);					// Free The Texture Image Memory
			}

			free(TextureImage[0]);								// Free The Image Structure
		}

		return Status;										// Return The Status
	}

	void DrawCube3()
	{
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, materialAmb);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, materialDif);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material0);

		glBindTexture(GL_TEXTURE_2D, texture3);

		glBegin(GL_QUADS);
			// Front Face
			glNormal3f( 0.0f, 0.0f, 1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f(-1.0f, -1.0f,  1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f( 1.0f, -1.0f,  1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f( 1.0f,  1.0f,  1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f(-1.0f,  1.0f,  1.0f);
		glEnd();

		glBegin(GL_QUADS);
			// Back Face
			glNormal3f( 0.0f, 0.0f,-1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f(-1.0f, -1.0f, -1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f(-1.0f,  1.0f, -1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f( 1.0f,  1.0f, -1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f( 1.0f, -1.0f, -1.0f);
		glEnd();

		glBegin(GL_QUADS);
			// Top Face
			glNormal3f( 0.0f, 1.0f, 0.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f(-1.0f,  1.0f, -1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f(-1.0f,  1.0f,  1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f( 1.0f,  1.0f,  1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f( 1.0f,  1.0f, -1.0f);
		glEnd();

		glBegin(GL_QUADS);
			// Bottom Face
			glNormal3f( 0.0f,-1.0f, 0.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f(-1.0f, -1.0f, -1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f( 1.0f, -1.0f, -1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f( 1.0f, -1.0f,  1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f(-1.0f, -1.0f,  1.0f);
		glEnd();

		glBegin(GL_QUADS);
			// Right face
			glNormal3f( 1.0f, 0.0f, 0.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f( 1.0f, -1.0f, -1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f( 1.0f,  1.0f, -1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f( 1.0f,  1.0f,  1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f( 1.0f, -1.0f,  1.0f);
		glEnd();

		glBegin(GL_QUADS);
			// Left Face
			glNormal3f(-1.0f, 0.0f, 0.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f(-1.0f, -1.0f, -1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f(-1.0f, -1.0f,  1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f(-1.0f,  1.0f,  1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f(-1.0f,  1.0f, -1.0f);
		glEnd();
	}

	int InitGL3(GLvoid)										// All Setup For OpenGL Goes Here
	{
		if (! loadTexture3)
		{
			LoadGLTextures3();
			loadTexture3 = true;
		}

		glEnable(GL_TEXTURE_2D);							// Enable Texture Mapping ( NEW )

		//glDisable(GL_LIGHT0);
		//glDisable(GL_LIGHTING);
		glLightfv(GL_LIGHT0, GL_AMBIENT, LightAmbient);		// Setup The Ambient Light
		glLightfv(GL_LIGHT0, GL_DIFFUSE, LightDiffuse);		// Setup The Diffuse Light
		glLightfv(GL_LIGHT0, GL_POSITION,LightPosition);	// Position The Light
		//glDisable(GL_LIGHT0);
		glEnable(GL_LIGHT0);								// Enable Light One
		glEnable(GL_LIGHTING);
	
		if (fog)
		{
			GLfloat	fogColor[] = {0.5f,0.5f,0.5f,1.0f};		// Fog Color

			glClearColor(0.5f, 0.5f, 0.5f, 1.0f);				// Black Background

			glFogi(GL_FOG_MODE, GL_LINEAR);
			glFogfv(GL_FOG_COLOR, fogColor);					// Set Fog Color
			glFogf(GL_FOG_DENSITY, 0.35f);						// How Dense Will The Fog Be
			glHint(GL_FOG_HINT, GL_DONT_CARE);					// Fog Hint Value
			glFogf(GL_FOG_START, 7.0f);							// Fog Start Depth
			glFogf(GL_FOG_END, 10.0f);							// Fog End Depth
			glEnable(GL_FOG);									// Enables GL_FOG
		}
		else glDisable(GL_FOG);

		return TRUE;										// Initialization Went OK
	}

public:
	int DrawGLScene3()									// Here's Where We Do All The Drawing
	{
		InitGL3();

		glTranslatef(0, 1.2f, oddalenie3);         // Oddal
		glRotatef(kat3, 1.0, 0.0, 0.0 );            // K¹t
	
		glRotatef(rquadX_3,0,1.0f,0); 
		glRotatef(rquadY_3,1.0f,0,0); 

		if (trybObrotu3 == 2) 
			rquadX_3-=speed3;
		if (trybObrotu3 == 1) 
			rquadX_3+=speed3;
		if (trybObrotu3 == 3) 
			rquadY_3-=speed3;
		if (trybObrotu3 == 4) 
			rquadY_3+=speed3;

		DrawCube3();

	    return TRUE;         // Wszystko ok
	}

	void keyboard3(bool *keys)
	{
		if (wait3== FALSE) time3 = GetTickCount();

		if (keys[VK_RIGHT])     //kontrolki do testowania
		{
			trybObrotu3=1;
		}
		else if (keys[VK_LEFT])
		{
			trybObrotu3=2;
		}
		else if (keys[VK_UP])
		{
			trybObrotu3=3;
		}
		else if (keys[VK_DOWN])
		{
			trybObrotu3=4;			
		}
		else if (keys['F'] && !wait3)
		{
			if (fog) fog = false;
			else fog = true;
	
			wait3 = TRUE;
		}

		if ( wait3 == TRUE && (GetTickCount() - time3 >= 200) ) { wait3 = FALSE; }
	}
};

#endif