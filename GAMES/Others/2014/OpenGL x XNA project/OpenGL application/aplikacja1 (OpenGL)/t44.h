#ifndef __T44_H		// prevent from multiple compiling
#define __T44_H

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <math.h>
#include <stdio.h>


	GLfloat material1[]={0.0, 0.0, 0.8, 0.0};			//NIEBIESKI
	GLfloat material3[]={0.8, 0.0, 0.0, 0.0};			//ZIELONY
	GLfloat material2[]={0.0, 0.8, 0.0, 0.0};			//CZERWONY
	GLfloat material5[]={0.8, 0.8, 0.0, 0.0};			//¿Ó£TY
	GLfloat material4[]={0.8, 0.3, 0.0, 0.0};			//POMARAÑCZOWY
	GLfloat material6[]={0.8, 0.8, 0.8, 0.0};			//BIA£Y
	/*
	GLfloat LightAmbient44[]=		{ 0.5f, 0.5f, 0.5f, 1.0f };
	GLfloat LightDiffuse44[]=		{ 1.0f, 1.0f, 1.0f, 1.0f };
	GLfloat LightPosition44[]=	{ 0.0f, 0.0f, 2.0f, 1.0f };
	*/
	GLfloat ambient44[]={0.3, 0.3, 0.3, 0.0};			//Oœwietlenia
	GLfloat diffuse44[]={0.0, 0.0, 0.0, 0.5};
	GLfloat position44[]={15.0, 30.0, 0.0, 3.0}; 
	GLfloat specular44[]={0.0, 0.0, 0.0, 0.5};         // Wartoœci œwiat³a rozproszonego 

	
	int numb44 [27][3] =  {							 // Oznaczenie (numeracja) kwadracików 
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
	
	int kostka44 [3][9] =  {
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


class T44
{

private:
	float przesuniecie44;
	float oddalenie44;
	float odl44;
	GLfloat speed44;
	GLfloat kat44; //35
	GLfloat rquadX_44;
	GLfloat rquadY_44;
	
		
	int trybObrotu44;

	//bool loadTexture = false;

	GLfloat rotatuj44;

	//GLuint	texture[6];


	GLfloat rot44 [27][3];
	//GLfloat rot2 [27][3];

	int rot_nr44 [27][1000];
	int licznik44 [27];

	int tab44[3][3];


	GLfloat max44;
	bool wait44;

	int ilosc44;

	DWORD time44;

public:
	T44()
	{
		przesuniecie44 = 3.5f;
		oddalenie44 = -35;
		odl44 = 2.5f;
		speed44 = 0.2f;
		kat44 = 0; //35
		rquadX_44 = 0.0f;
		rquadY_44 = 0.0f;
	
		
		trybObrotu44 = 1;

		//bool loadTexture = false;

		rotatuj44=1.0f;

		//GLuint	texture[6];


		rot44 [27][3];
		//GLfloat rot2 [27][3];

		rot_nr44 [27][1000];
		licznik44 [27];

		tab44[3][3];


		max44;
		wait44 = false;

		ilosc44 = 3;

		time44 = GetTickCount();
	}

private:
	void DrawColoredCube(float X)
	{
		glBegin(GL_QUADS);         // Zacznij rysowanie szeœcianu
		
			glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material1);
			glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material1);
			glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material1);

			glNormal3f( 0.0f, 0.0f, 1.0f);
			glVertex3f(-X, -X,  X);
			glVertex3f( X, -X,  X);
			glVertex3f( X,  X,  X);
			glVertex3f(-X,  X,  X);
	

			glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material2);
			glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material2);
			glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material2);
	
			glNormal3f( 0.0f, 0.0f,-1.0f);
			glVertex3f(-X, -X, -X);
			glVertex3f(-X,  X, -X);
			glVertex3f( X,  X, -X);
			glVertex3f( X, -X, -X);
	

			glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material3);
			glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material3);
			glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material3);
			
			glNormal3f( 0.0f, 1.0f, 0.0f);
			glVertex3f(-X,  X, -X);
			glVertex3f(-X,  X,  X);
			glVertex3f( X,  X,  X);
			glVertex3f( X,  X, -X);
	
		
			glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material4);
			glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material4);
			glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material4);
	
			glNormal3f( 0.0f,-1.0f, 0.0f);
			glVertex3f(-X, -X, -X);
			glVertex3f( X, -X, -X);
			glVertex3f( X, -X,  X);
			glVertex3f(-X, -X,  X);
		

			glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material5);
			glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material5);
			glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material5);
	
			glNormal3f( 1.0f, 0.0f, 0.0f);
			glVertex3f( X, -X, -X);
			glVertex3f( X,  X, -X);
			glVertex3f( X,  X,  X);
			glVertex3f( X, -X,  X);
			

			glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material6);
			glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material6);
			glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material6);
	
			glNormal3f(-1.0f, 0.0f, 0.0f);
			glVertex3f(-X, -X, -X);
			glVertex3f(-X, -X,  X);
			glVertex3f(-X,  X,  X);
			glVertex3f(-X,  X, -X); 
		glEnd();         // Zakoñcz rysowanie czworok¹ta
	}

	void Tryb44(int x)
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

	int InitGL44(GLvoid)										// All Setup For OpenGL Goes Here
	{
		//glClearColor(0.0f, 0.0f, 0.0f, 0.5f);				// Black Background

		glEnable(GL_TEXTURE_2D);							// Enable Texture Mapping ( NEW )

		//glDisable(GL_LIGHT0);
		//glDisable(GL_LIGHTING);
		glLightfv(GL_LIGHT0, GL_AMBIENT, ambient44);
		//glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuse);
		glLightfv(GL_LIGHT0, GL_POSITION, position44);
		glLightfv(GL_LIGHT0, GL_SPECULAR, specular44);
		/*
		glLightfv(GL_LIGHT0, GL_AMBIENT, LightAmbient44);		// Setup The Ambient Light
		glLightfv(GL_LIGHT0, GL_DIFFUSE, LightDiffuse44);		// Setup The Diffuse Light
		glLightfv(GL_LIGHT0, GL_POSITION,LightPosition44);	// Position The Light
		*/
		//glDisable(GL_LIGHT0);
		glEnable(GL_LIGHT0);								// Enable Light One
		glEnable(GL_LIGHTING);
		glDisable(GL_FOG);
		
		return TRUE;										// Initialization Went OK
	}

public:
	int DrawGLScene44()									// Here's Where We Do All The Drawing
	{
		InitGL44();
	
		glEnable(GL_TEXTURE_2D);

		for(int k=0; k<ilosc44; k++)
		{
			for(int i=0; i<ilosc44; i++)
			{
				for(int j=0; j<ilosc44; j++)
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

					glTranslatef(0, przesuniecie44, oddalenie44);         // Oddal
					glRotatef(kat44, 1.0, 0.0, 0.0 );    //Obróæ

					glRotatef(rquadY_44,1.0f,0,0); 
					glRotatef(rquadX_44,0,1.0f,0);        

				
				
					//
					for (int s= 0; s< 27; s++)
						if (k==numb44[s][0] && j==numb44[s][1] && i==numb44[s][2])
						{	
							glRotatef(rot44[s][0],0.0f,0.0f,1.0f);    
							glRotatef(rot44[s][1],0.0f,1.0f,0.0f);
							glRotatef(rot44[s][2],1.0f,0.0f,0.0f);

							for (int z= licznik44[s]-1; z>=0; z--)
								Tryb44(rot_nr44[s][z]);	
						}
				
				

					glTranslatef(odl44*a, odl44*b, odl44*c);
				

					DrawColoredCube(1.0f);

				}
			}
		}

		if (trybObrotu44 == 0) {	speed44 = 0.2f;   max44 = 0.0f;   wait44 = FALSE;	  }
		else if (trybObrotu44 == 1) 
		{
			rquadX_44+=speed44;  
		//		speed44 = 0.5f;
		}	
		else if (trybObrotu44 == 2) 
		{
			rquadX_44-=speed44;   
		//		speed44 = 0.5f;
		}
		else if (trybObrotu44 == 3) 
		{	
			rquadY_44-=speed44;
		//		speed44 = 0;
		}
		else if (trybObrotu44 == 4) 
		{	
			rquadY_44+=speed44;
		//		speed44= 0;
		}
		else if (trybObrotu44 == 10)		// Tryb 10;
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[k][j]][1]-=rotatuj44;
					}
					k--;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[k][j];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[k][j]= tab44[j][l];
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
						rot44[kostka44[k][j]][1]=0.0f;

						rot_nr44 [kostka44[k][j]] [licznik44[kostka44[k][j]]] = 10;
						licznik44[kostka44[k][j]]++;		
					}
					k--;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 11)		// Tryb 11;
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[k][j+3]][1]-=rotatuj44;
					}
					k--;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[k][j+3];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[k][j+3]= tab44[j][l];
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
						rot44[kostka44[k][j+3]][1]=0.0f;

						rot_nr44 [kostka44[k][j+3]] [licznik44[kostka44[k][j+3]]] = 10;
						licznik44[kostka44[k][j+3]]++;		
					}
					k--;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 12)		// Tryb 12
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[k][j+6]][1]-=rotatuj44;
					}
					k--;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[k][j+6];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[k][j+6]= tab44[j][l];
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
						rot44[kostka44[k][j+6]][1]=0.0f;

						rot_nr44 [kostka44[k][j+6]] [licznik44[kostka44[k][j+6]]] = 10;
						licznik44[kostka44[k][j+6]]++;		
					}
					k--;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 20)		// Tryb 20
		{	
			for (int j= 0; j<9; j++)
			{
				rot44[kostka44[0][j]][2]-=rotatuj44;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab44[i][j]= kostka44[0][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[0][index]= tab44[j][k];
						index++;
					}
					k--;
				}
		
				for (int j= 0; j<9; j++)
					{
						rot44[kostka44[0][j]][2]= 0.0f;

						rot_nr44 [kostka44[0][j]] [licznik44[kostka44[0][j]]] = 20;
						licznik44[kostka44[0][j]]++;
					}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 21)		// Tryb 21
		{	
			for (int j= 0; j<9; j++)
			{
				rot44[kostka44[1][j]][2]-=rotatuj44;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab44[i][j]= kostka44[1][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[1][index]= tab44[j][k];
						index++;
					}
					k--;
				}
		
				for (int j= 0; j<9; j++)
					{
						rot44[kostka44[1][j]][2]= 0.0f;

						rot_nr44 [kostka44[1][j]] [licznik44[kostka44[1][j]]] = 20;
						licznik44[kostka44[1][j]]++;
					}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 22)		// Tryb 22
		{	
			for (int j= 0; j<9; j++)
			{
				rot44[kostka44[2][j]][2]-=rotatuj44;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab44[i][j]= kostka44[2][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[2][index]= tab44[j][k];
						index++;
					}
					k--;
				}
		
				for (int j= 0; j<9; j++)
					{
						rot44[kostka44[2][j]][2]= 0.0f;

						rot_nr44 [kostka44[2][j]] [licznik44[kostka44[2][j]]] = 20;
						licznik44[kostka44[2][j]]++;
					}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 30)		// Tryb 30 
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[i][k]][0]-=rotatuj44;
						k+=3;
					}
					k=2;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[i][k];
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
						kostka44[i][k]= tab44[l][i];
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
						rot44[kostka44[i][k]][0]=0.0f;

						rot_nr44 [kostka44[i][k]] [licznik44[kostka44[i][k]]] = 30;
						licznik44[kostka44[i][k]]++;	

						k+=3;
					}
					k=2;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 31)		// Tryb 31 
		{	
			int k = 1;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[i][k]][0]-=rotatuj44;
						k+=3;
					}
					k=1;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 1;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[i][k];
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
						kostka44[i][k]= tab44[l][i];
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
						rot44[kostka44[i][k]][0]=0.0f;

						rot_nr44 [kostka44[i][k]] [licznik44[kostka44[i][k]]] = 30;
						licznik44[kostka44[i][k]]++;	

						k+=3;
					}
					k=1;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 32)		// Tryb 32
		{	
			int k = 0;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[i][k]][0]-=rotatuj44;
						k+=3;
					}
					k=0;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[i][k];
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
						kostka44[i][k]= tab44[l][i];  
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
						rot44[kostka44[i][k]][0]=0.0f;

						rot_nr44 [kostka44[i][k]] [licznik44[kostka44[i][k]]] = 30;
						licznik44[kostka44[i][k]]++;	

						k+=3;
					}
					k=0;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 42)		// Tryb 42; -> zmiana kierunku rotacji
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[k][j]][1]+=rotatuj44;
					}
					k--;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[k][j];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[k][j]= tab44[l][i];	//
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
						rot44[kostka44[k][j]][1]=0.0f;

						rot_nr44 [kostka44[k][j]] [licznik44[kostka44[k][j]]] = 440;		//
						licznik44[kostka44[k][j]]++;		
					}
					k--;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 41)		// Tryb 41;
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[k][j+3]][1]+=rotatuj44;
					}
					k--;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[k][j+3];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[k][j+3]= tab44[l][i];	//
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
						rot44[kostka44[k][j+3]][1]=0.0f;

						rot_nr44 [kostka44[k][j+3]] [licznik44[kostka44[k][j+3]]] = 440;		//
						licznik44[kostka44[k][j+3]]++;		
					}
					k--;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 40)		// Tryb 40
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[k][j+6]][1]+=rotatuj44;
					}
					k--;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[k][j+6];
					}
					k--;
				}

				k = 2;
				int l = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[k][j+6]= tab44[l][i];	//
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
						rot44[kostka44[k][j+6]][1]=0.0f;

						rot_nr44 [kostka44[k][j+6]] [licznik44[kostka44[k][j+6]]] = 440;
						licznik44[kostka44[k][j+6]]++;		
					}
					k--;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 52)		// Tryb 52
		{	
			for (int j= 0; j<9; j++)
			{
				rot44[kostka44[0][j]][2]+=rotatuj44;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab44[i][j]= kostka44[0][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[0][index]= tab44[k][i];	//
						k--;	//
						index++;
					}
					k=2;	//
				}
		
				for (int j= 0; j<9; j++)
					{
						rot44[kostka44[0][j]][2]= 0.0f;

						rot_nr44 [kostka44[0][j]] [licznik44[kostka44[0][j]]] = 50;
						licznik44[kostka44[0][j]]++;
					}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 51)		// Tryb 51
		{	
			for (int j= 0; j<9; j++)
			{
				rot44[kostka44[1][j]][2]+=rotatuj44;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab44[i][j]= kostka44[1][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[1][index]= tab44[k][i];	//
						k--;	//
						index++;
					}
					k=2;	//
				}
		
				for (int j= 0; j<9; j++)
					{
						rot44[kostka44[1][j]][2]= 0.0f;

						rot_nr44 [kostka44[1][j]] [licznik44[kostka44[1][j]]] = 50;
						licznik44[kostka44[1][j]]++;
					}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 50)		// Tryb 50
		{	
			for (int j= 0; j<9; j++)
			{
				rot44[kostka44[2][j]][2]+=rotatuj44;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				
				int index = 0;
				for (int i= 0; i<3; i++)
					for (int j= 0; j<3; j++)
						{
							tab44[i][j]= kostka44[2][index];
							index++;
						}

				int k = 2;
				index = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						kostka44[2][index]= tab44[k][i];
						k--;
						index++;
					}
					k=2;
				}
		
				for (int j= 0; j<9; j++)
					{
						rot44[kostka44[2][j]][2]= 0.0f;

						rot_nr44 [kostka44[2][j]] [licznik44[kostka44[2][j]]] = 50;
						licznik44[kostka44[2][j]]++;
					}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 62)		// Tryb 62 
		{	
			int k = 2;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[i][k]][0]+=rotatuj44;
						k+=3;
					}
					k=2;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 2;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[i][k];
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
						kostka44[i][k]= tab44[j][l];	//
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
						rot44[kostka44[i][k]][0]=0.0f;

						rot_nr44 [kostka44[i][k]] [licznik44[kostka44[i][k]]] = 60;
						licznik44[kostka44[i][k]]++;	

						k+=3;
					}
					k=2;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 61)		// Tryb 61 
		{	
			int k = 1;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[i][k]][0]+=rotatuj44;
						k+=3;
					}
					k=1;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 1;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[i][k];
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
						kostka44[i][k]= tab44[j][l];	//
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
						rot44[kostka44[i][k]][0]=0.0f;

						rot_nr44 [kostka44[i][k]] [licznik44[kostka44[i][k]]] = 60;
						licznik44[kostka44[i][k]]++;	

						k+=3;
					}
					k=1;
				}

				trybObrotu44 = 0;
			}
		}
		else if (trybObrotu44 == 60)		// Tryb 60
		{	
			int k = 0;
			for (int i= 0; i<3; i++)
			{
					for (int j= 0; j<3; j++)
					{
						rot44[kostka44[i][k]][0]+=rotatuj44;
						k+=3;
					}
					k=0;
			}
		
			max44 += rotatuj44;


			if (max44 == 90.0f) 
			{
				int k = 0;
				for (int i= 0; i<3; i++)
				{
					for (int j= 0; j<3; j++)
					{
						tab44[i][j]= kostka44[i][k];
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
						kostka44[i][k]= tab44[j][l];	//  
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
						rot44[kostka44[i][k]][0]=0.0f;

						rot_nr44 [kostka44[i][k]] [licznik44[kostka44[i][k]]] = 60;
						licznik44[kostka44[i][k]]++;	

						k+=3;
					}
					k=0;
				}

				trybObrotu44 = 0;
			}
		}
	
		glDisable(GL_TEXTURE_2D);

		return TRUE;         // Wszystko ok
	}

	void keyboard44(bool *keys)
	{
		//if (wait44== FALSE) time44 = GetTickCount();

		if (keys[VK_RIGHT] && wait44 == FALSE)     //kontrolki do testowania
		{
			trybObrotu44=1;
		}
		else if (keys[VK_LEFT] && wait44 == FALSE)
		{
			trybObrotu44=2;
		}
		else if (keys[VK_UP] && wait44 == FALSE)
		{
			trybObrotu44=3;
		}
		else if (keys[VK_DOWN] && wait44 == FALSE)
		{
			trybObrotu44=4;			
		}
		else if (keys['L'] && wait44 == FALSE)
		{
			speed44+=0.0000005f;
		}
		else if (keys['P'] && wait44 == FALSE)
		{
			speed44-=0.0000005f;
		}
		else if (keys['Q'] && wait44 == FALSE)
		{
			trybObrotu44 = 10;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['W'] && wait44 == FALSE)
		{
			trybObrotu44 = 11;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['E'] && wait44 == FALSE)
		{
			trybObrotu44 = 12;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['A'] && wait44 == FALSE)
		{
			trybObrotu44 = 20;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['S'] && wait44 == FALSE)
		{
			trybObrotu44 = 21;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['D'] && wait44 == FALSE)
		{
			trybObrotu44 = 22;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['Z'] && wait44 == FALSE)
		{
			trybObrotu44 = 30;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['X'] && wait44 == FALSE)
		{
			trybObrotu44 = 31;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['C'] && wait44 == FALSE)
		{
			trybObrotu44 = 32;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['T'] && wait44 == FALSE)
		{
			trybObrotu44 = 40;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['Y'] && wait44 == FALSE)
		{
			trybObrotu44 = 41;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['U'] && wait44 == FALSE)
		{
			trybObrotu44 = 42;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['G'] && wait44 == FALSE)
		{
			trybObrotu44 = 50;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['H'] && wait44 == FALSE)
		{
			trybObrotu44 = 51;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['J'] && wait44 == FALSE)
		{
			trybObrotu44 = 52;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['B'] && wait44 == FALSE)
		{
			trybObrotu44 = 60;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['N'] && wait44 == FALSE)
		{
			trybObrotu44 = 61;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}
		else if (keys['M'] && wait44 == FALSE)
		{
			trybObrotu44 = 62;
			wait44 = TRUE;
			//speed1-=0.0000001f;
		}

		//if ( wait44 == TRUE && (GetTickCount() - time44 >= 20) ) { wait44 = FALSE; }
		//while ( GetTickCount() - time <= 20 ) {}
	}
};

#endif