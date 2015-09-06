#ifndef __T4_H		// prevent from multiple compiling
#define __T4_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>


	GLfloat LightAmbient[]=		{ 0.5f, 0.5f, 0.5f, 1.0f };
	GLfloat LightDiffuse[]=		{ 1.0f, 1.0f, 1.0f, 1.0f };
	GLfloat LightPosition[]=	{ 0.0f, 0.0f, 2.0f, 1.0f };
	GLfloat material0[]=		{0.0, 0.0, 0.0, 1.0};
	GLfloat materialDif[]=		{0.8, 0.8, 0.8, 1.0};
	GLfloat materialAmb[]=		{0.2, 0.2, 0.2, 1.0};

	//
	int numb [27][3] =  {							 // Oznaczenie (numeracja) kwadracików 
							{1,1,2}, {1,1,0}, {1,1,1},
							{0,1,2}, {0,1,0}, {0,1,1},
							{2,1,2}, {2,1,0}, {2,1,1},

							{1,0,2}, {1,0,0}, {1,0,1},
							{0,0,2}, {0,0,0}, {0,0,1},
							{2,0,2}, {2,0,0}, {2,0,1},

							{1,2,2}, {1,2,0}, {1,2,1},
							{0,2,2}, {0,2,0}, {0,2,1},
							{2,2,2}, {2,2,0}, {2,2,1}
						};

	int kostka [3][9] =  {
							{0,1,2,
							 3,4,5,
							 6,7,8},

							 {9,10,11,
							  12,13,14,
							  15,16,17},
						 
							 {18,19,20,
							  21,22,23,
							  24,25,26}
						 };
	
	bool sim;


class T4
{

private:
	float przesuniecie4;
	float oddalenie4;
	float odl4;
	GLfloat speed4;
	GLfloat kat4; //35
	GLfloat rquadX_4;
	GLfloat rquadY_4;


	int trybObrotu4;

	bool loadTexture;

	GLfloat rotatuj;

	GLuint	texture[6];

	GLfloat rot [27][3];
	//GLfloat rot2 [27][3];

	int rot_nr [27][1000];
	int licznik [27];
	
	int tab[3][3];

	GLfloat max4;
	bool wait4;

	int ilosc4;

public:
	T4()
	{
		przesuniecie4 = 2.0f;
		oddalenie4 = -21;
		odl4 = 2.0f;
		speed4 = 0.3f;
		kat4 = 0; //35
		rquadX_4 = 0.0f;
		rquadY_4 = 0.0f;


		trybObrotu4 = 1;

		loadTexture = false;

		rotatuj=1.5f;

		texture[6];

		rot [27][3];
		//GLfloat rot2 [27][3];

		rot_nr [27][1000];
		licznik [27];
	
		tab[3][3];

		max4;
		wait4 = false;

		ilosc4 = 3;

		sim = false;
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

	int LoadGLTextures()									// Load Bitmaps And Convert To Textures
	{
		int Status=FALSE;									// Status Indicator

		AUX_RGBImageRec *TextureImage[1];					// Create Storage Space For The Texture

		memset(TextureImage,0,sizeof(void *)*1);           	// Set The Pointer To NULL

		char text[20];
		// Load The Bitmap, Check For Errors, If Bitmap's Not Found Quit
		for (int i = 0; i < 6; i++)
		{
			sprintf(text,"Data/%d.bmp",i+1);
			if (TextureImage[0]=LoadBMP(text))
			{
				Status=TRUE;									// Set The Status To TRUE
	
				glGenTextures(1, &texture[i]);					// Create The Texture
	
				// Typical Texture Generation Using Data From The Bitmap
				glBindTexture(GL_TEXTURE_2D, texture[i]);
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
		}

		return Status;										// Return The Status
	}

	void DrawTexturedCube()
	{
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, materialAmb);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, materialDif);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material0);

		glBindTexture(GL_TEXTURE_2D, texture[0]);

		glBegin(GL_QUADS);
			// Front Face
			glNormal3f( 0.0f, 0.0f, 1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f(-1.0f, -1.0f,  1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f( 1.0f, -1.0f,  1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f( 1.0f,  1.0f,  1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f(-1.0f,  1.0f,  1.0f);
		glEnd();

		glBindTexture(GL_TEXTURE_2D, texture[1]);

		glBegin(GL_QUADS);
			// Back Face
			glNormal3f( 0.0f, 0.0f,-1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f(-1.0f, -1.0f, -1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f(-1.0f,  1.0f, -1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f( 1.0f,  1.0f, -1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f( 1.0f, -1.0f, -1.0f);
		glEnd();

		glBindTexture(GL_TEXTURE_2D, texture[2]);

		glBegin(GL_QUADS);
			// Top Face
			glNormal3f( 0.0f, 1.0f, 0.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f(-1.0f,  1.0f, -1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f(-1.0f,  1.0f,  1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f( 1.0f,  1.0f,  1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f( 1.0f,  1.0f, -1.0f);
		glEnd();

		glBindTexture(GL_TEXTURE_2D, texture[3]);

		glBegin(GL_QUADS);
			// Bottom Face
			glNormal3f( 0.0f,-1.0f, 0.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f(-1.0f, -1.0f, -1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f( 1.0f, -1.0f, -1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f( 1.0f, -1.0f,  1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f(-1.0f, -1.0f,  1.0f);
		glEnd();

		glBindTexture(GL_TEXTURE_2D, texture[4]);

		glBegin(GL_QUADS);
			// Right face
			glNormal3f( 1.0f, 0.0f, 0.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f( 1.0f, -1.0f, -1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f( 1.0f,  1.0f, -1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f( 1.0f,  1.0f,  1.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f( 1.0f, -1.0f,  1.0f);
		glEnd();

		glBindTexture(GL_TEXTURE_2D, texture[5]);
	
		glBegin(GL_QUADS);
			// Left Face
			glNormal3f(-1.0f, 0.0f, 0.0f);
			glTexCoord2f(0.0f, 0.0f); glVertex3f(-1.0f, -1.0f, -1.0f);
			glTexCoord2f(1.0f, 0.0f); glVertex3f(-1.0f, -1.0f,  1.0f);
			glTexCoord2f(1.0f, 1.0f); glVertex3f(-1.0f,  1.0f,  1.0f);
			glTexCoord2f(0.0f, 1.0f); glVertex3f(-1.0f,  1.0f, -1.0f);
		glEnd();
	}

	void Tryb4(int x)
	{
			if (x == 10) 
					glRotatef(-90.0f,0.0f,1.0f,0.0f);	

			else if (x == 20) 
					glRotatef(-90.0f,1.0f,0.0f,0.0f);

			else if (x == 30) 
					glRotatef(-90.0f,0.0f,0.0f,1.0f);

			else if (x == 40) 
					glRotatef(+90.0f,0.0f,1.0f,0.0f);

			else if (x == 50) 
					glRotatef(+90.0f,1.0f,0.0f,0.0f);

			else if (x == 60) 
					glRotatef(+90.0f,0.0f,0.0f,1.0f);
	}

	int InitGL4(GLvoid)										// All Setup For OpenGL Goes Here
	{
		if (! loadTexture)
		{
			LoadGLTextures();
			loadTexture = true;
		}

		//glClearColor(0.0f, 0.0f, 0.0f, 0.5f);				// Black Background

		glEnable(GL_TEXTURE_2D);							// Enable Texture Mapping ( NEW )

		//glDisable(GL_LIGHT0);
		//glDisable(GL_LIGHTING);
		glLightfv(GL_LIGHT0, GL_AMBIENT, LightAmbient);		// Setup The Ambient Light
		glLightfv(GL_LIGHT0, GL_DIFFUSE, LightDiffuse);		// Setup The Diffuse Light
		glLightfv(GL_LIGHT0, GL_POSITION,LightPosition);	// Position The Light
		//glDisable(GL_LIGHT0);
		glEnable(GL_LIGHT0);								// Enable Light One
		glEnable(GL_LIGHTING);
		glDisable(GL_FOG);
	
		return TRUE;										// Initialization Went OK
	}
	
	void simulation()
	{
		if (wait4 == FALSE)
		{
			int l1 = rand() % 6 + 1;
			int l2 = rand() % 3;

			trybObrotu4 = l1 * 10 + l2;
			wait4 = TRUE;
		}
	}

public:
	int DrawGLScene4()									// Here's Where We Do All The Drawing
	{
		InitGL4();
	
		glEnable(GL_TEXTURE_2D);

		for(int k=0; k<ilosc4; k++)
		{
			for(int i=0; i<ilosc4; i++)
			{
				for(int j=0; j<ilosc4; j++)
				{
					int a=j;
					if (a==2) a=-1;
					if (a>2) { a--; a= a*(-1); }
				
					int b=k;
					if (b==2) b=-1;
					if (b>2) { b--; b= b*(-1); }
				
					int c=i;
					if (c==2) c=-1;
					if (c>2) { c--; c= c*(-1); }


					glLoadIdentity();

					glTranslatef(0, przesuniecie4, oddalenie4);         // Oddal
					glRotatef(kat4, 1.0, 0.0, 0.0 );    //Obróæ

					glRotatef(rquadY_4,1.0f,0,0); 
					glRotatef(rquadX_4,0,1.0f,0);        

				
				
					//
					for (int s= 0; s< 27; s++)
						if (k==numb[s][0] && j==numb[s][1] && i==numb[s][2])
						{	
							glRotatef(rot[s][0],0.0f,0.0f,1.0f);    
							glRotatef(rot[s][1],0.0f,1.0f,0.0f);
							glRotatef(rot[s][2],1.0f,0.0f,0.0f);

							for (int z= licznik[s]-1; z>=0; z--)
								Tryb4(rot_nr[s][z]);	
						}
				
				

					glTranslatef(odl4*a, odl4*b, odl4*c);
				

					DrawTexturedCube();

				}
			}
		}

		if (trybObrotu4 == 0) {	speed4 = 0.3f;   max4 = 0.0f;   wait4 = FALSE;	  }
		else if (trybObrotu4 == 1) 
		{
			rquadX_4+=speed4;  
		//		speed4 = 0.5f;
		}	
		else if (trybObrotu4 == 2) 
		{
			rquadX_4-=speed4;   
		//		speed4 = 0.5f;
		}
		else if (trybObrotu4 == 3) 
		{	
			rquadY_4-=speed4;
		//		speed4 = 0;
		}
		else if (trybObrotu4 == 4) 
		{	
			rquadY_4+=speed4;
		//		speed4= 0;
		}
		else if (trybObrotu4 == 10)		// Tryb 10;
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j]][1]-=rotatuj;
					}
					k--;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[k][j];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[k][j]= tab[j][l];
					}
					k--;
					l--;
				}	
				
				//
				k =2;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j]][1]=0.0f;

						rot_nr [kostka[k][j]] [licznik[kostka[k][j]]] = 10;
						licznik[kostka[k][j]]++;		
					}
					k--;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 11)		// Tryb 11;
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j+3]][1]-=rotatuj;
					}
					k--;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[k][j+3];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[k][j+3]= tab[j][l];
					}
					k--;
					l--;
				}	
				
				//
				k =2;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j+3]][1]=0.0f;

						rot_nr [kostka[k][j+3]] [licznik[kostka[k][j+3]]] = 10;
						licznik[kostka[k][j+3]]++;		
					}
					k--;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 12)		// Tryb 12
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j+6]][1]-=rotatuj;
					}
					k--;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[k][j+6];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[k][j+6]= tab[j][l];
					}
					k--;
					l--;
				}	
				
				//
				k =2;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j+6]][1]=0.0f;

						rot_nr [kostka[k][j+6]] [licznik[kostka[k][j+6]]] = 10;
						licznik[kostka[k][j+6]]++;		
					}
					k--;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 20)		// Tryb 20
		{	
			for (int j= 0; j<9; j++)
			{
				rot[kostka[0][j]][2]-=rotatuj;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab[i][j]= kostka[0][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[0][index]= tab[j][k];
						index++;
					}
					k--;
				}
		
				for (int j= 0; j<9; j++)
					{
						rot[kostka[0][j]][2]= 0.0f;

						rot_nr [kostka[0][j]] [licznik[kostka[0][j]]] = 20;
						licznik[kostka[0][j]]++;
					}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 21)		// Tryb 21
		{	
			for (int j= 0; j<9; j++)
			{
				rot[kostka[1][j]][2]-=rotatuj;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab[i][j]= kostka[1][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[1][index]= tab[j][k];
						index++;
					}
					k--;
				}
		
				for (int j= 0; j<9; j++)
					{
						rot[kostka[1][j]][2]= 0.0f;

						rot_nr [kostka[1][j]] [licznik[kostka[1][j]]] = 20;
						licznik[kostka[1][j]]++;
					}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 22)		// Tryb 22
		{	
			for (int j= 0; j<9; j++)
			{
				rot[kostka[2][j]][2]-=rotatuj;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab[i][j]= kostka[2][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[2][index]= tab[j][k];
						index++;
					}
					k--;
				}
		
				for (int j= 0; j<9; j++)
					{
						rot[kostka[2][j]][2]= 0.0f;

						rot_nr [kostka[2][j]] [licznik[kostka[2][j]]] = 20;
						licznik[kostka[2][j]]++;
					}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 30)		// Tryb 30 
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]-=rotatuj;
						k+=3;
					}
					k=2;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[i][k];
						k+=3;
					}
					k=2;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[i][k]= tab[l][i];
						k+=3;
						l--;	
					}
					k=2;
					l=2;
				}	
				
				//
				k= 2;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]=0.0f;

						rot_nr [kostka[i][k]] [licznik[kostka[i][k]]] = 30;
						licznik[kostka[i][k]]++;	

						k+=3;
					}
					k=2;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 31)		// Tryb 31 
		{	
			int k = 1;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]-=rotatuj;
						k+=3;
					}
					k=1;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 1;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[i][k];
						k+=3;
					}
					k=1;
				}

				k = 1;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[i][k]= tab[l][i];
						k+=3;
						l--;	
					}
					k=1;
					l=2;
				}	
				
				//
				k= 1;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]=0.0f;

						rot_nr [kostka[i][k]] [licznik[kostka[i][k]]] = 30;
						licznik[kostka[i][k]]++;	

						k+=3;
					}
					k=1;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 32)		// Tryb 32
		{	
			int k = 0;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]-=rotatuj;
						k+=3;
					}
					k=0;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[i][k];
						k+=3;
					}
					k=0;
				}

				k = 0;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[i][k]= tab[l][i];  
						k+=3;
						l--;	
					}
					k=0;
					l=2;     
				}	
				
				//
				k= 0;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]=0.0f;

						rot_nr [kostka[i][k]] [licznik[kostka[i][k]]] = 30;
						licznik[kostka[i][k]]++;	

						k+=3;
					}
					k=0;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 42)		// Tryb 42; -> zmiana kierunku rotacji
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j]][1]+=rotatuj;
					}
					k--;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[k][j];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[k][j]= tab[l][i];	//
						l--;	//
					}
					k--;
					l=2;	//
				}	
				
				//
				k =2;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j]][1]=0.0f;

						rot_nr [kostka[k][j]] [licznik[kostka[k][j]]] = 40;		//
						licznik[kostka[k][j]]++;		
					}
					k--;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 41)		// Tryb 41;
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j+3]][1]+=rotatuj;
					}
					k--;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[k][j+3];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[k][j+3]= tab[l][i];	//
						l--;	//
					}
					k--;
					l=2;	//
				}	
				
				//
				k =2;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j+3]][1]=0.0f;

						rot_nr [kostka[k][j+3]] [licznik[kostka[k][j+3]]] = 40;		//
						licznik[kostka[k][j+3]]++;		
					}
					k--;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 40)		// Tryb 40
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j+6]][1]+=rotatuj;
					}
					k--;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[k][j+6];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[k][j+6]= tab[l][i];	//
						l--;	//
					}
					k--;
					l=2;	//
				}	
				
				//
				k =2;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[k][j+6]][1]=0.0f;

						rot_nr [kostka[k][j+6]] [licznik[kostka[k][j+6]]] = 40;
						licznik[kostka[k][j+6]]++;		
					}
					k--;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 52)		// Tryb 52
		{	
			for (int j= 0; j<9; j++)
			{
				rot[kostka[0][j]][2]+=rotatuj;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab[i][j]= kostka[0][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[0][index]= tab[k][i];	//
						k--;	//
						index++;
					}
					k=2;	//
				}
		
				for (int j= 0; j<9; j++)
					{
						rot[kostka[0][j]][2]= 0.0f;

						rot_nr [kostka[0][j]] [licznik[kostka[0][j]]] = 50;
						licznik[kostka[0][j]]++;
					}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 51)		// Tryb 51
		{	
			for (int j= 0; j<9; j++)
			{
				rot[kostka[1][j]][2]+=rotatuj;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab[i][j]= kostka[1][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[1][index]= tab[k][i];	//
						k--;	//
						index++;
					}
					k=2;	//
				}
		
				for (int j= 0; j<9; j++)
					{
						rot[kostka[1][j]][2]= 0.0f;

						rot_nr [kostka[1][j]] [licznik[kostka[1][j]]] = 50;
						licznik[kostka[1][j]]++;
					}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 50)		// Tryb 50
		{	
			for (int j= 0; j<9; j++)
			{
				rot[kostka[2][j]][2]+=rotatuj;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab[i][j]= kostka[2][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[2][index]= tab[k][i];
						k--;
						index++;
					}
					k=2;
				}
		
				for (int j= 0; j<9; j++)
					{
						rot[kostka[2][j]][2]= 0.0f;

						rot_nr [kostka[2][j]] [licznik[kostka[2][j]]] = 50;
						licznik[kostka[2][j]]++;
					}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 62)		// Tryb 62 
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]+=rotatuj;
						k+=3;
					}
					k=2;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[i][k];
						k+=3;
					}
					k=2;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[i][k]= tab[j][l];	//
						k+=3;
					}
					k=2;
					l--;	//
				}	
				
				//
				k= 2;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]=0.0f;

						rot_nr [kostka[i][k]] [licznik[kostka[i][k]]] = 60;
						licznik[kostka[i][k]]++;	

						k+=3;
					}
					k=2;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 61)		// Tryb 61 
		{	
			int k = 1;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]+=rotatuj;
						k+=3;
					}
					k=1;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 1;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[i][k];
						k+=3;
					}
					k=1;
				}

				k = 1;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[i][k]= tab[j][l];	//
						k+=3;
					}
					k=1;
					l--;
				}	
				
				//
				k= 1;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]=0.0f;

						rot_nr [kostka[i][k]] [licznik[kostka[i][k]]] = 60;
						licznik[kostka[i][k]]++;	

						k+=3;
					}
					k=1;
				}

				trybObrotu4 = 0;
			}
		}
		else if (trybObrotu4 == 60)		// Tryb 60
		{	
			int k = 0;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]+=rotatuj;
						k+=3;
					}
					k=0;
			}
		
			max4 += rotatuj;


			if (max4 == 90.0f) 
			{
				int k = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab[i][j]= kostka[i][k];
						k+=3;
					}
					k=0;
				}

				k = 0;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka[i][k]= tab[j][l];	//  
						k+=3;
					}
					k=0;
					l--;     //
				}	
				
				//
				k= 0;
				for (int i= 0; i<3; i++)
				{	
					for (int j= 0; j<3; j++)
					{
						rot[kostka[i][k]][0]=0.0f;

						rot_nr [kostka[i][k]] [licznik[kostka[i][k]]] = 60;
						licznik[kostka[i][k]]++;	

						k+=3;
					}
					k=0;
				}

				trybObrotu4 = 0;
			}
		}
	
		glDisable(GL_TEXTURE_2D);

		return TRUE;         // Wszystko ok
	}

	void keyboard4(bool *keys)
	{
		//DWORD time = GetTickCount();

		if (keys[VK_RIGHT])     //kontrolki do testowania
		{
			trybObrotu4=1;
		}
		else if (keys[VK_LEFT])
		{
			trybObrotu4=2;
		}
		else if (keys[VK_UP])
		{
			trybObrotu4=3;
		}
		else if (keys[VK_DOWN])
		{
			trybObrotu4=4;			
		}
		else if (keys['L'] && wait4 == FALSE)
		{
			speed4+=0.0000005f;
		}
		else if (keys['P'] && wait4 == FALSE)
		{
			speed4-=0.0000005f;
		}
		else if (keys['Q'] && wait4 == FALSE)
		{
			trybObrotu4 = 10;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['W'] && wait4 == FALSE)
		{
			trybObrotu4 = 11;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['E'] && wait4 == FALSE)
		{
			trybObrotu4 = 12;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['A'] && wait4 == FALSE)
		{
			trybObrotu4 = 20;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['S'] && wait4 == FALSE)
		{
			trybObrotu4 = 21;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['D'] && wait4 == FALSE)
		{
			trybObrotu4 = 22;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['Z'] && wait4 == FALSE)
		{
			trybObrotu4 = 30;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['X'] && wait4 == FALSE)
		{
			trybObrotu4 = 31;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['C'] && wait4 == FALSE)
		{
			trybObrotu4 = 32;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['T'] && wait4 == FALSE)
		{
			trybObrotu4 = 40;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['Y'] && wait4 == FALSE)
		{
			trybObrotu4 = 41;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['U'] && wait4 == FALSE)
		{
			trybObrotu4 = 42;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['G'] && wait4 == FALSE)
		{
			trybObrotu4 = 50;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['H'] && wait4 == FALSE)
		{
			trybObrotu4 = 51;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['J'] && wait4 == FALSE)
		{
			trybObrotu4 = 52;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['B'] && wait4 == FALSE)
		{
			trybObrotu4 = 60;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['N'] && wait4 == FALSE)
		{
			trybObrotu4 = 61;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['M'] && wait4 == FALSE)
		{
			trybObrotu4 = 62;
			wait4 = TRUE;
			//speed1-=0.0000001f;
		}

		if (sim) simulation();
		//while ( GetTickCount() - time <= 20 ) {}
	}
};

#endif