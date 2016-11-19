/*
// **** //
 *		Program:			Kostka Rubika
 *		Autor:				Dziuba Tomasz
 *		Data wykonania:		31.12.1012
// **** //
*/

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library

HDC			hDC=NULL;		// Prywatny kontekst u¿¹dzenia GDI
HGLRC		hRC=NULL;		// Kontekst rysuj¹cy
HWND		hWnd=NULL;		// Uchwyt naszego okna
HINSTANCE	hInstance;		// Instancja aplikacji

bool keys[256];				// Tablica klawiszy - wciœniêty czy nie
bool active=TRUE;		  	// Flaga - czy okno jest aktywne?
bool fullscreen=TRUE;		// Uruchom aplikacje na pe³nym ekranie

bool wait = FALSE;
float odl = 2.1; 			//odelg³oœæ miêdzy kwadracikami
float oddalenie = -15;		//oddalenie widoku (oddalenie miejsca rysowania bry³y)
GLfloat kat = 35.0f; 		//k¹]t rysowania bry³y 
GLfloat speed1 = 0.5f;
int ilosc = 3; 				//wskazuje na iloœæ jednego rz¹du szeœcianów (liczba szeœcianów = ilosc * ilosc * ilosc) 

int tryb =1; 				//tryby poruszania siê

GLfloat rquad_1;		 	// K¹t obroty czworok¹ta ca³ego szeœcianu

GLfloat rotatuj=10.0f;
GLfloat max;

GLfloat material1[]={0.0, 0.0, 0.8, 0.0};			//NIEBIESKI
GLfloat material3[]={0.8, 0.0, 0.0, 0.0};			//ZIELONY
GLfloat material2[]={0.0, 0.8, 0.0, 0.0};			//CZERWONY
GLfloat material5[]={0.8, 0.8, 0.0, 0.0};			//¿Ó£TY
GLfloat material4[]={0.8, 0.3, 0.0, 0.0};			//POMARAÑCZOWY
GLfloat material6[]={0.8, 0.8, 0.8, 0.0};			//BIA£Y
GLfloat ambient[]={0.3, 0.3, 0.3, 0.0};				//Oœwietlenia
GLfloat diffuse[]={0.0, 0.0, 0.0, 0.5};
GLfloat position[]={15.0, 30.0, 0.0, 3.0}; 
GLfloat specular[]={0.0, 0.0, 0.0, 0.5};		 	// Wartoœci œwiat³a rozproszonego 

int numb [27][3] =	{							 	// Oznaczenie (numeracja) kwadracików 
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

GLfloat rot [27][3];

int rot_nr [27][1000];
int licznik [27];

int tab[3][3];

int kostka [3][9] =	 {
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
						
LRESULT	CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);	// Declaration For WndProc

GLvoid ReSizeGLScene(GLsizei width, GLsizei height)		// Resize And Initialize The GL Window
{
	if (height==0)										// Prevent A Divide By Zero By
	{
		height=1;										// Making Height Equal One
	}

	glViewport(0,0,width,height);						// Reset The Current Viewport

	glMatrixMode(GL_PROJECTION);						// Select The Projection Matrix
	glLoadIdentity();									// Reset The Projection Matrix

	// Calculate The Aspect Ratio Of The Window
	gluPerspective(45.0f,(GLfloat)width/(GLfloat)height,0.1f,100.0f);

	glMatrixMode(GL_MODELVIEW);							// Select The Modelview Matrix
	glLoadIdentity();									// Reset The Modelview Matrix
}

int InitGL(GLvoid)										// All Setup For OpenGL Goes Here
{
	glShadeModel(GL_SMOOTH);							// Enable Smooth Shading
	glClearColor(0.0f, 0.0f, 0.0f, 0.5f);				// Black Background
	glClearDepth(1.0f);									// Depth Buffer Setup
	glEnable(GL_DEPTH_TEST);							// Enables Depth Testing
	glDepthFunc(GL_LEQUAL);								// The Type Of Depth Testing To Do
	glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);	// Really Nice Perspective Calculations
	
	glLightfv(GL_LIGHT0, GL_AMBIENT, ambient);
	glLightfv(GL_LIGHT0, GL_POSITION, position);
	glLightfv(GL_LIGHT0, GL_SPECULAR, specular);

	glEnable(GL_LIGHTING);
	glEnable(GL_LIGHT0); 
	
	return TRUE;										// Initialization Went OK
}

void DrawCube(float X)
{
	glBegin(GL_QUADS);		   // Zacznij rysowanie szeœcianu
		
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material1);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material1);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material1);

		glNormal3f( 0.0f, 0.0f, 1.0f);
		glVertex3f(-X, -X,	X);
		glVertex3f( X, -X,	X);
		glVertex3f( X,	X,	X);
		glVertex3f(-X,	X,	X);


		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material2);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material2);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material2);

		glNormal3f( 0.0f, 0.0f,-1.0f);
		glVertex3f(-X, -X, -X);
		glVertex3f(-X,	X, -X);
		glVertex3f( X,	X, -X);
		glVertex3f( X, -X, -X);


		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material3);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material3);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material3);
		
		glNormal3f( 0.0f, 1.0f, 0.0f);
		glVertex3f(-X,	X, -X);
		glVertex3f(-X,	X,	X);
		glVertex3f( X,	X,	X);
		glVertex3f( X,	X, -X);

		
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material4);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material4);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material4);

		glNormal3f( 0.0f,-1.0f, 0.0f);
		glVertex3f(-X, -X, -X);
		glVertex3f( X, -X, -X);
		glVertex3f( X, -X,	X);
		glVertex3f(-X, -X,	X);
		

		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material5);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material5);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material5);

		glNormal3f( 1.0f, 0.0f, 0.0f);
		glVertex3f( X, -X, -X);
		glVertex3f( X,	X, -X);
		glVertex3f( X,	X,	X);
		glVertex3f( X, -X,	X);
		

		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material6);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material6);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material6);

		glNormal3f(-1.0f, 0.0f, 0.0f);
		glVertex3f(-X, -X, -X);
		glVertex3f(-X, -X,	X);
		glVertex3f(-X,	X,	X);
		glVertex3f(-X,	X, -X);
		
	glEnd();		 // Zakoñcz rysowanie czworok¹ta
}

void Tryb(int x)
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

int DrawGLScene(GLvoid)									// Here's Where We Do All The Drawing
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);			// Wyczyœæ ekran i bufor g³êbi

	for(int k=0; k<ilosc; k++)
	{
		for(int i=0; i<ilosc; i++)
		{
			for(int j=0; j<ilosc; j++)
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

				glTranslatef(0, 0, oddalenie);			// Oddal
				glRotatef(kat, 1.0, 0.0, 0.0 );			//Obróæ

				glRotatef(rquad_1,0,1.0f,0);		

				//
				for (int s= 0; s< 27; s++)
					if (k==numb[s][0] && j==numb[s][1] && i==numb[s][2])
					{	
						glRotatef(rot[s][0],0.0f,0.0f,1.0f);	
						glRotatef(rot[s][1],0.0f,1.0f,0.0f);
						glRotatef(rot[s][2],1.0f,0.0f,0.0f);

						for (int z= licznik[s]-1; z>=0; z--)
							Tryb(rot_nr[s][z]);	
					}				

				glTranslatef(odl*a, odl*b, odl*c);
				
				DrawCube(1.0);
			}
		}
	}

	if (tryb == 0) {	speed1 = 0.5f;	 max = 0.0f;   wait = FALSE;	  }
	else if (tryb == 1) 
	{
		rquad_1-=speed1;
	}	
	else if (tryb == 2) 
	{
		rquad_1+=speed1;
	}
	else if (tryb == 3) 
	{	
		kat+=speed1;
	}
	else if (tryb == 4) 
	{	
		kat-=speed1;
	}
	else if (tryb == 10)		// Tryb 10;
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 11)		// Tryb 11;
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 12)		// Tryb 12
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 20)		// Tryb 20
	{	
		for (int j= 0; j<9; j++)
		{
			rot[kostka[0][j]][2]-=rotatuj;
		}
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 21)		// Tryb 21
	{	
		for (int j= 0; j<9; j++)
		{
			rot[kostka[1][j]][2]-=rotatuj;
		}
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 22)		// Tryb 22
	{	
		for (int j= 0; j<9; j++)
		{
			rot[kostka[2][j]][2]-=rotatuj;
		}
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 30)		// Tryb 30 
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 31)		// Tryb 31 
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
		max += rotatuj;

		if (max == 90.0f) 
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

					rot_nr[kostka[i][k]] [licznik[kostka[i][k]]] = 30;
					licznik[kostka[i][k]]++;	
					k+=3;
				}
				k=1;
			}
			tryb = 0;
		}
	}
	else if (tryb == 32)		// Tryb 32
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 42)		// Tryb 42; -> zmiana kierunku rotacji
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 41)		// Tryb 41;
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 40)		// Tryb 40
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 52)		// Tryb 52
	{	
		for (int j= 0; j<9; j++)
		{
			rot[kostka[0][j]][2]+=rotatuj;
		}
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 51)		// Tryb 51
	{	
		for (int j= 0; j<9; j++)
		{
			rot[kostka[1][j]][2]+=rotatuj;
		}
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 50)		// Tryb 50
	{	
		for (int j= 0; j<9; j++)
		{
			rot[kostka[2][j]][2]+=rotatuj;
		}
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 62)		// Tryb 62 
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 61)		// Tryb 61 
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
		max += rotatuj;

		if (max == 90.0f) 
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
			tryb = 0;
		}
	}
	else if (tryb == 60)		// Tryb 60
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
		max += rotatuj;

		if (max == 90.0f) 
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
				l--;	 //
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
			tryb = 0;
		}
	}
	
	return TRUE;		 // Wszystko ok
}

GLvoid KillGLWindow(GLvoid)								// Properly Kill The Window
{
	if (fullscreen)										// Are We In Fullscreen Mode?
	{
		ChangeDisplaySettings(NULL,0);					// If So Switch Back To The Desktop
		ShowCursor(TRUE);								// Show Mouse Pointer
	}

	if (hRC)											// Do We Have A Rendering Context?
	{
		if (!wglMakeCurrent(NULL,NULL))					// Are We Able To Release The DC And RC Contexts?
		{
			MessageBox(NULL,"Release Of DC And RC Failed.","SHUTDOWN ERROR",MB_OK | MB_ICONINFORMATION);
		}

		if (!wglDeleteContext(hRC))						// Are We Able To Delete The RC?
		{
			MessageBox(NULL,"Release Rendering Context Failed.","SHUTDOWN ERROR",MB_OK | MB_ICONINFORMATION);
		}
		hRC=NULL;										// Set RC To NULL
	}

	if (hDC && !ReleaseDC(hWnd,hDC))					// Are We Able To Release The DC
	{
		MessageBox(NULL,"Release Device Context Failed.","SHUTDOWN ERROR",MB_OK | MB_ICONINFORMATION);
		hDC=NULL;										// Set DC To NULL
	}

	if (hWnd && !DestroyWindow(hWnd))					// Are We Able To Destroy The Window?
	{
		MessageBox(NULL,"Could Not Release hWnd.","SHUTDOWN ERROR",MB_OK | MB_ICONINFORMATION);
		hWnd=NULL;										// Set hWnd To NULL
	}

	if (!UnregisterClass("OpenGL",hInstance))			// Are We Able To Unregister Class
	{
		MessageBox(NULL,"Could Not Unregister Class.","SHUTDOWN ERROR",MB_OK | MB_ICONINFORMATION);
		hInstance=NULL;									// Set hInstance To NULL
	}
}

/*	This Code Creates Our OpenGL Window.  Parameters Are:					*
 *	title			- Title To Appear At The Top Of The Window				*
 *	width			- Width Of The GL Window Or Fullscreen Mode				*
 *	height			- Height Of The GL Window Or Fullscreen Mode			*
 *	bits			- Number Of Bits To Use For Color (8/16/24/32)			*
 *	fullscreenflag	- Use Fullscreen Mode (TRUE) Or Windowed Mode (FALSE)	*/
BOOL CreateGLWindow(char* title, int width, int height, int bits, bool fullscreenflag)
{
	GLuint		PixelFormat;			// Holds The Results After Searching For A Match
	WNDCLASS	wc;						// Windows Class Structure
	DWORD		dwExStyle;				// Window Extended Style
	DWORD		dwStyle;				// Window Style
	RECT		WindowRect;				// Grabs Rectangle Upper Left / Lower Right Values
	WindowRect.left=(long)0;			// Set Left Value To 0
	WindowRect.right=(long)width;		// Set Right Value To Requested Width
	WindowRect.top=(long)0;				// Set Top Value To 0
	WindowRect.bottom=(long)height;		// Set Bottom Value To Requested Height

	fullscreen=fullscreenflag;			// Set The Global Fullscreen Flag

	hInstance			= GetModuleHandle(NULL);				// Grab An Instance For Our Window
	wc.style			= CS_HREDRAW | CS_VREDRAW | CS_OWNDC;	// Redraw On Size, And Own DC For Window.
	wc.lpfnWndProc		= (WNDPROC) WndProc;					// WndProc Handles Messages
	wc.cbClsExtra		= 0;									// No Extra Window Data
	wc.cbWndExtra		= 0;									// No Extra Window Data
	wc.hInstance		= hInstance;							// Set The Instance
	wc.hIcon			= LoadIcon(NULL, IDI_WINLOGO);			// Load The Default Icon
	wc.hCursor			= LoadCursor(NULL, IDC_ARROW);			// Load The Arrow Pointer
	wc.hbrBackground	= NULL;									// No Background Required For GL
	wc.lpszMenuName		= NULL;									// We Don't Want A Menu
	wc.lpszClassName	= "OpenGL";								// Set The Class Name

	if (!RegisterClass(&wc))									// Attempt To Register The Window Class
	{
		MessageBox(NULL,"Failed To Register The Window Class.","ERROR",MB_OK|MB_ICONEXCLAMATION);
		return FALSE;											// Return FALSE
	}
	
	if (fullscreen)												// Attempt Fullscreen Mode?
	{
		DEVMODE dmScreenSettings;								// Device Mode
		memset(&dmScreenSettings,0,sizeof(dmScreenSettings));	// Makes Sure Memory's Cleared
		dmScreenSettings.dmSize=sizeof(dmScreenSettings);		// Size Of The Devmode Structure
		dmScreenSettings.dmPelsWidth	= width;				// Selected Screen Width
		dmScreenSettings.dmPelsHeight	= height;				// Selected Screen Height
		dmScreenSettings.dmBitsPerPel	= bits;					// Selected Bits Per Pixel
		dmScreenSettings.dmFields=DM_BITSPERPEL|DM_PELSWIDTH|DM_PELSHEIGHT;

		// Try To Set Selected Mode And Get Results.  NOTE: CDS_FULLSCREEN Gets Rid Of Start Bar.
		if (ChangeDisplaySettings(&dmScreenSettings,CDS_FULLSCREEN)!=DISP_CHANGE_SUCCESSFUL)
		{
			// If The Mode Fails, Offer Two Options.  Quit Or Use Windowed Mode.
			if (MessageBox(NULL,"The Requested Fullscreen Mode Is Not Supported By\nYour Video Card. Use Windowed Mode Instead?","NeHe GL",MB_YESNO|MB_ICONEXCLAMATION)==IDYES)
			{
				fullscreen=FALSE;		// Windowed Mode Selected.	Fullscreen = FALSE
			}
			else
			{
				// Pop Up A Message Box Letting User Know The Program Is Closing.
				MessageBox(NULL,"Program Will Now Close.","ERROR",MB_OK|MB_ICONSTOP);
				return FALSE;									// Return FALSE
			}
		}
	}

	if (fullscreen)												// Are We Still In Fullscreen Mode?
	{
		dwExStyle=WS_EX_APPWINDOW;								// Window Extended Style
		dwStyle=WS_POPUP;										// Windows Style
		ShowCursor(FALSE);										// Hide Mouse Pointer
	}
	else
	{
		dwExStyle=WS_EX_APPWINDOW | WS_EX_WINDOWEDGE;			// Window Extended Style
		dwStyle=WS_OVERLAPPEDWINDOW;							// Windows Style
	}

	AdjustWindowRectEx(&WindowRect, dwStyle, FALSE, dwExStyle);		// Adjust Window To True Requested Size

	// Create The Window
	if (!(hWnd=CreateWindowEx(	dwExStyle,							// Extended Style For The Window
								"OpenGL",							// Class Name
								title,								// Window Title
								dwStyle |							// Defined Window Style
								WS_CLIPSIBLINGS |					// Required Window Style
								WS_CLIPCHILDREN,					// Required Window Style
								0, 0,								// Window Position
								WindowRect.right-WindowRect.left,	// Calculate Window Width
								WindowRect.bottom-WindowRect.top,	// Calculate Window Height
								NULL,								// No Parent Window
								NULL,								// No Menu
								hInstance,							// Instance
								NULL)))								// Dont Pass Anything To WM_CREATE
	{
		KillGLWindow();								// Reset The Display
		MessageBox(NULL,"Window Creation Error.","ERROR",MB_OK|MB_ICONEXCLAMATION);
		return FALSE;								// Return FALSE
	}

	static	PIXELFORMATDESCRIPTOR pfd=				// pfd Tells Windows How We Want Things To Be
	{
		sizeof(PIXELFORMATDESCRIPTOR),				// Size Of This Pixel Format Descriptor
		1,											// Version Number
		PFD_DRAW_TO_WINDOW |						// Format Must Support Window
		PFD_SUPPORT_OPENGL |						// Format Must Support OpenGL
		PFD_DOUBLEBUFFER,							// Must Support Double Buffering
		PFD_TYPE_RGBA,								// Request An RGBA Format
		bits,										// Select Our Color Depth
		0, 0, 0, 0, 0, 0,							// Color Bits Ignored
		0,											// No Alpha Buffer
		0,											// Shift Bit Ignored
		0,											// No Accumulation Buffer
		0, 0, 0, 0,									// Accumulation Bits Ignored
		16,											// 16Bit Z-Buffer (Depth Buffer)  
		0,											// No Stencil Buffer
		0,											// No Auxiliary Buffer
		PFD_MAIN_PLANE,								// Main Drawing Layer
		0,											// Reserved
		0, 0, 0										// Layer Masks Ignored
	};
	
	if (!(hDC=GetDC(hWnd)))							// Did We Get A Device Context?
	{
		KillGLWindow();								// Reset The Display
		MessageBox(NULL,"Can't Create A GL Device Context.","ERROR",MB_OK|MB_ICONEXCLAMATION);
		return FALSE;								// Return FALSE
	}

	if (!(PixelFormat=ChoosePixelFormat(hDC,&pfd)))	// Did Windows Find A Matching Pixel Format?
	{
		KillGLWindow();								// Reset The Display
		MessageBox(NULL,"Can't Find A Suitable PixelFormat.","ERROR",MB_OK|MB_ICONEXCLAMATION);
		return FALSE;								// Return FALSE
	}

	if(!SetPixelFormat(hDC,PixelFormat,&pfd))		// Are We Able To Set The Pixel Format?
	{
		KillGLWindow();								// Reset The Display
		MessageBox(NULL,"Can't Set The PixelFormat.","ERROR",MB_OK|MB_ICONEXCLAMATION);
		return FALSE;								// Return FALSE
	}

	if (!(hRC=wglCreateContext(hDC)))				// Are We Able To Get A Rendering Context?
	{
		KillGLWindow();								// Reset The Display
		MessageBox(NULL,"Can't Create A GL Rendering Context.","ERROR",MB_OK|MB_ICONEXCLAMATION);
		return FALSE;								// Return FALSE
	}

	if(!wglMakeCurrent(hDC,hRC))					// Try To Activate The Rendering Context
	{
		KillGLWindow();								// Reset The Display
		MessageBox(NULL,"Can't Activate The GL Rendering Context.","ERROR",MB_OK|MB_ICONEXCLAMATION);
		return FALSE;								// Return FALSE
	}

	ShowWindow(hWnd,SW_SHOW);						// Show The Window
	SetForegroundWindow(hWnd);						// Slightly Higher Priority
	SetFocus(hWnd);									// Sets Keyboard Focus To The Window
	ReSizeGLScene(width, height);					// Set Up Our Perspective GL Screen

	if (!InitGL())									// Initialize Our Newly Created GL Window
	{
		KillGLWindow();								// Reset The Display
		MessageBox(NULL,"Initialization Failed.","ERROR",MB_OK|MB_ICONEXCLAMATION);
		return FALSE;								// Return FALSE
	}

	return TRUE;									// Success
}

LRESULT CALLBACK WndProc(	HWND	hWnd,			// Handle For This Window
							UINT	uMsg,			// Message For This Window
							WPARAM	wParam,			// Additional Message Information
							LPARAM	lParam)			// Additional Message Information
{
	switch (uMsg)									// Check For Windows Messages
	{
		case WM_ACTIVATE:							// Watch For Window Activate Message
		{
			if (!HIWORD(wParam))					// Check Minimization State
			{
				active=TRUE;						// Program Is Active
			}
			else
			{
				active=FALSE;						// Program Is No Longer Active
			}

			return 0;								// Return To The Message Loop
		}

		case WM_SYSCOMMAND:							// Intercept System Commands
		{
			switch (wParam)							// Check System Calls
			{
				case SC_SCREENSAVE:					// Screensaver Trying To Start?
				case SC_MONITORPOWER:				// Monitor Trying To Enter Powersave?
				return 0;							// Prevent From Happening
			}
			break;									// Exit
		}

		case WM_CLOSE:								// Did We Receive A Close Message?
		{
			PostQuitMessage(0);						// Send A Quit Message
			return 0;								// Jump Back
		}

		case WM_KEYDOWN:							// Is A Key Being Held Down?
		{
			keys[wParam] = TRUE;					// If So, Mark It As TRUE
			return 0;								// Jump Back
		}

		case WM_KEYUP:								// Has A Key Been Released?
		{
			keys[wParam] = FALSE;					// If So, Mark It As FALSE
			return 0;								// Jump Back
		}

		case WM_SIZE:								// Resize The OpenGL Window
		{
			ReSizeGLScene(LOWORD(lParam),HIWORD(lParam));  // LoWord=Width, HiWord=Height
			return 0;								// Jump Back
		}
	}

	// Pass All Unhandled Messages To DefWindowProc
	return DefWindowProc(hWnd,uMsg,wParam,lParam);
}

int WINAPI WinMain(	HINSTANCE	hInstance,			// Instance
					HINSTANCE	hPrevInstance,		// Previous Instance
					LPSTR		lpCmdLine,			// Command Line Parameters
					int			nCmdShow)			// Window Show State
{
	MSG		msg;									// Windows Message Structure
	BOOL	done=FALSE;								// Bool Variable To Exit Loop

	fullscreen=FALSE;							// Windowed Mode

	// Create Our OpenGL Window
	CreateGLWindow("PROJ 5",640,480,16,fullscreen);

	while(!done)									// Loop That Runs While done=FALSE
	{
		DWORD time = GetTickCount();

		if (PeekMessage(&msg,NULL,0,0,PM_REMOVE))	// Is There A Message Waiting?
		{
			if (msg.message==WM_QUIT)				// Have We Received A Quit Message?
			{
				done=TRUE;							// If So done=TRUE
			}
			else									// If Not, Deal With Window Messages
			{
				TranslateMessage(&msg);				// Translate The Message
				DispatchMessage(&msg);				// Dispatch The Message
			}
		}
		else									// If There Are No Messages
		{
			// Draw The Scene.	Watch For ESC Key And Quit Messages From DrawGLScene()
			if (active)								// Program Active?
			{
				if (keys[VK_ESCAPE])				// Was ESC Pressed?
				{
					done=TRUE;						// ESC Signalled A Quit
				}
				else if (keys[VK_RIGHT] && wait == FALSE)	  //kontrolki do testowania
				{
					tryb=1;
				}
				else if (keys[VK_LEFT] && wait == FALSE)
				{
					tryb=2;
				}
				else if (keys[VK_UP] && wait == FALSE)
				{
					tryb=3;
				}
				else if (keys[VK_DOWN] && wait == FALSE)
				{
					tryb=4;
				}
				else if (keys['0'] && wait == FALSE)
				{
					tryb=0;
				}
				else if (keys['1'] && wait == FALSE)
				{
					tryb = 1;
				}
				else if (keys['L'] && wait == FALSE)
				{
					speed1+=0.0000005f;
				}
				else if (keys['P'] && wait == FALSE)
				{
					speed1-=0.0000005f;
				}
				else if (keys['Q'] && wait == FALSE)
				{
					tryb = 10;
					wait = TRUE;
				}
				else if (keys['W'] && wait == FALSE)
				{
					tryb = 11;
					wait = TRUE;
				}
				else if (keys['E'] && wait == FALSE)
				{
					tryb = 12;
					wait = TRUE;
				}
				else if (keys['A'] && wait == FALSE)
				{
					tryb = 20;
					wait = TRUE;
				}
				else if (keys['S'] && wait == FALSE)
				{
					tryb = 21;
					wait = TRUE;
				}
				else if (keys['D'] && wait == FALSE)
				{
					tryb = 22;
					wait = TRUE;
				}
				else if (keys['Z'] && wait == FALSE)
				{
					tryb = 30;
					wait = TRUE;
				}
				else if (keys['X'] && wait == FALSE)
				{
					tryb = 31;
					wait = TRUE;
				}
				else if (keys['C'] && wait == FALSE)
				{
					tryb = 32;
					wait = TRUE;
				}
				else if (keys['T'] && wait == FALSE)
				{
					tryb = 40;
					wait = TRUE;
				}
				else if (keys['Y'] && wait == FALSE)
				{
					tryb = 41;
					wait = TRUE;
				}
				else if (keys['U'] && wait == FALSE)
				{
					tryb = 42;
					wait = TRUE;
				}
				else if (keys['G'] && wait == FALSE)
				{
					tryb = 50;
					wait = TRUE;
				}
				else if (keys['H'] && wait == FALSE)
				{
					tryb = 51;
					wait = TRUE;
				}
				else if (keys['J'] && wait == FALSE)
				{
					tryb = 52;
					wait = TRUE;
				}
				else if (keys['B'] && wait == FALSE)
				{
					tryb = 60;
					wait = TRUE;
				}
				else if (keys['N'] && wait == FALSE)
				{
					tryb = 61;
					wait = TRUE;
				}
				else if (keys['M'] && wait == FALSE)
				{
					tryb = 62;
					wait = TRUE;
				}
				else
				{
					while ( GetTickCount() - time <= 20 ) {}
					DrawGLScene();					// Draw The Scene
					SwapBuffers(hDC);				// Swap Buffers (Double Buffering)
				}
			}
		}
	}

	// Shutdown
	KillGLWindow();									// Kill The Window
	return (msg.wParam);							// Exit The Program
}
