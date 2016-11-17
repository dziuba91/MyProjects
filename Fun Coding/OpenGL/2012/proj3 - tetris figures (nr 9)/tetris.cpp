/*
// **** //
 *		Program:			Tetris
 *		Autor:				Dziuba Tomasz
 *		Data wykonania:		13.01.2013
// **** //
*/



#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <ctime>


HDC			hDC=NULL;         // Prywatny kontekst u¿¹dzenia GDI
HGLRC		hRC=NULL;         // Kontekst rysuj¹cy
HWND		hWnd=NULL;         // Uchwyt naszego okna
HINSTANCE	hInstance;         // Instancja aplikacji

bool keys[256];         // Tablica klawiszy - wciœniêty czy nie
bool active=TRUE;         // Flaga - czy okno jest aktywne?
bool fullscreen=TRUE;         // Uruchom aplikacje na pe³nym ekranie

bool grawitacja = FALSE;
bool grawitacja2 = FALSE;
bool wait = FALSE;
bool wait2 =FALSE;
bool stop = FALSE;
int sciana=0;
int lewa_max=-4;
int prawa_max=4;
int podloga=0;
int max_podl=10;
float licznik = 0.0;
float licznik2 = 0.0;
float odl1 = 0;
float sufit = 12;
float odl2 = 12;
float odl3 = 0; 
float oddalenie = -25;  //oddalenie widoku (oddalenie miejsca rysowania bry³y)
float kat1 = 0.0; //k¹t rysowania bry³y 
float kat2 = 0.0; //k¹t rysowania bry³y 
//int ilosc = 3; //wskazuje na iloœæ jednego rz¹du szeœcianów (liczba szeœcianów = ilosc * ilosc * ilosc) 

int fig = 1;
int kolor = 1;
float odl = 2.1; //odelg³oœæ miêdzy kwadracikami
float obrot = 0.0; //k¹t rysowania figury 

int tryb =0; //tryby poruszania siê
//int tryb_3 =1;
//int tryb_4 =1;

//GLfloat rquad_1;         // K¹t obroty czworok¹ta ca³ego szeœcianu
//GLfloat rquad_2;		 // K¹t obrotu poszczególnych szeœcianów

//float color1 = 0.8;        //wskaŸniki kontroluj¹ce kolor na trzech szeœcianach
//float color2 = 0;

int fig_odl [4];
int fig_tab [8][4] =  {
						 {0,0,0,0},

						 {2,0,1,0},

						 {0,1,1,0},
						 
						 {1,0,1,1},

						 {1,1,1,0},

						 {1,1,1,0},
						 
						 {1,0,1,1},

						 {1,1,1,0}
					 };

GLfloat material1[]={0.0, 0.0, 0.8, 0.0};			//NIEBIESKI
GLfloat material3[]={0.8, 0.0, 0.0, 0.0};			//ZIELONY
GLfloat material2[]={0.0, 0.8, 0.0, 0.0};			//CZERWONY
GLfloat material5[]={0.8, 0.8, 0.0, 0.0};			//¿Ó£TY
GLfloat material4[]={0.8, 0.3, 0.0, 0.0};			//POMARAÑCZOWY
GLfloat material6[]={0.8, 0.8, 0.8, 0.0};			//BIA£Y
//GLfloat material[]={0.0, 0.0, 0.8, 0.0};			//kolory materia³ów
//GLfloat material1[]={color1, 0.0, color2, 0.0};
//GLfloat material2[]={0.0, color1, color2, 0.0};
//GLfloat material3[]={color1, color1, color2, 0.0};
GLfloat ambient[]={0.3, 0.3, 0.3, 0.0};			//Oœwietlenia
GLfloat diffuse[]={0.0, 0.0, 0.0, 0.5};
GLfloat position[]={15.0, 30.0, 0.0, 3.0}; 
GLfloat specular[]={0.0, 0.0, 0.0, 0.5};         // Wartoœci œwiat³a rozproszonego 


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
	//glLightfv(GL_LIGHT0, GL_DIFFUSE, diffuse);
	glLightfv(GL_LIGHT0, GL_POSITION, position);
	glLightfv(GL_LIGHT0, GL_SPECULAR, specular);

	glEnable(GL_LIGHTING);
	glEnable(GL_LIGHT0); 
	
	return TRUE;										// Initialization Went OK
}

void Color (int numer)
{
	if (numer == 1)
	{
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material1);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material1);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material1);
	}
	if (numer == 2)
	{
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material2);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material2);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material2);
	}
	if (numer == 3)
	{
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material3);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material3);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material3);
	}
	if (numer == 4)
	{
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material4);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material4);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material4);
	}
	if (numer == 5)
	{
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material5);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material5);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material5);
	}
	if (numer == 6)
	{
		glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material6);
		glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material6);
		glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material6);
	}
}

void Draw(float X)
{
//		float X = 1.0;
		float A = X- 0.15;
		glBegin(GL_QUADS);         // Zacznij rysowanie szeœcianu
			// (kwadrat) przednia
			glNormal3f( 0.0f, 0.0f, 1.0f);
			glVertex3f(-A, -A,  X);
			glVertex3f( A, -A,  X);
			glVertex3f( A,  A,  X);
			glVertex3f(-A,  A,  X);

				// (prostok¹t) pomiêdzy górn¹ i przedni¹
				glNormal3f( 0.0f, 1.0f, 1.0f);
				glVertex3f( A,  A,  X);
				glVertex3f(-A,  A,  X);
				glVertex3f(-A,  X,  A);
				glVertex3f( A,  X,  A);
				////

				// (prostok¹t) pomiêdzy doln¹ i przedni¹
				glNormal3f( 0.0f,-1.0f, 1.0f);
				glVertex3f(-A, -A,  X);
				glVertex3f( A, -A,  X);
				glVertex3f( A, -X,  A);
				glVertex3f(-A, -X,  A);
				////

				// (prostok¹t) pomiêdzy prawym i przedni¹
				glNormal3f( 1.0f, 0.0f, 1.0f);
				glVertex3f( A, -A,  X);
				glVertex3f( A,  A,  X);
				glVertex3f( X,  A,  A);
				glVertex3f( X, -A,  A);
				////

				// (prostok¹t) pomiêdzy lew¹ i przedni¹
				glNormal3f(-1.0f, 0.0f, 1.0f);
				glVertex3f(-A, -A,  X);
				glVertex3f(-A,  A,  X);
				glVertex3f(-X,  A,  A);
				glVertex3f(-X, -A,  A);
				////
					
			// (kwadrat) tylnia
			glNormal3f( 0.0f, 0.0f,-1.0f);
			glVertex3f(-A, -A, -X);
			glVertex3f(-A,  A, -X);
			glVertex3f( A,  A, -X);
			glVertex3f( A, -A, -X);
			
				// (prostok¹t) pomiêdzy górn¹ i tylni¹
				glNormal3f( 0.0f, 1.0f,-1.0f);
				glVertex3f( A,  A, -X);
				glVertex3f(-A,  A, -X);
				glVertex3f(-A,  X, -A);
				glVertex3f( A,  X, -A);
				////

				// (prostok¹t) pomiêdzy doln¹ i tylni¹
				glNormal3f( 0.0f,-1.0f,-1.0f);
				glVertex3f(-A, -A, -X);
				glVertex3f( A, -A, -X);
				glVertex3f( A, -X, -A);
				glVertex3f(-A, -X, -A);
				////

				// (prostok¹t) pomiêdzy prawym i tylni¹
				glNormal3f( 1.0f, 0.0f,-1.0f);
				glVertex3f( A, -A, -X);
				glVertex3f( A,  A, -X);
				glVertex3f( X,  A, -A);
				glVertex3f( X, -A, -A);
				////

				// (prostok¹t) pomiêdzy lew¹ i tylni¹
				glNormal3f(-1.0f, 0.0f,-1.0f);
				glVertex3f(-A, -A, -X);
				glVertex3f(-A,  A, -X);
				glVertex3f(-X,  A, -A);
				glVertex3f(-X, -A, -A);
				////

			// (kwadrat) górna
			glNormal3f( 0.0f, 1.0f, 0.0f);
			glVertex3f(-A,  X, -A);
			glVertex3f(-A,  X,  A);
			glVertex3f( A,  X,  A);
			glVertex3f( A,  X, -A);

			// (kwadrat) dolna
			glNormal3f( 0.0f,-1.0f, 0.0f);
			glVertex3f(-A, -X, -A);
			glVertex3f( A, -X, -A);
			glVertex3f( A, -X,  A);
			glVertex3f(-A, -X,  A);

			// (kwadrat) prawy
			glNormal3f( 1.0f, 0.0f, 0.0f);
			glVertex3f( X, -A, -A);
			glVertex3f( X,  A, -A);
			glVertex3f( X,  A,  A);
			glVertex3f( X, -A,  A);
			
				// (prostok¹t) pomiêdzy prawym i górn¹
				glNormal3f( 1.0f, 1.0f, 0.0f);
				glVertex3f( A,  X, -A);
				glVertex3f( A,  X,  A);
				glVertex3f( X,  A,  A);
				glVertex3f( X,  A, -A);
				////

				// (prostok¹t) pomiêdzy prawym i doln¹
				glNormal3f( 1.0f,-1.0f, 0.0f);
				glVertex3f( X, -A, -A);
				glVertex3f( X, -A,  A);
				glVertex3f( A, -X,  A);
				glVertex3f( A, -X, -A);
				////

			// (kwadrat) lewy
			glNormal3f(-1.0f, 0.0f, 0.0f);
			glVertex3f(-X, -A, -A);
			glVertex3f(-X, -A,  A);
			glVertex3f(-X,  A,  A);
			glVertex3f(-X,  A, -A); 

				// (prostok¹t) pomiêdzy lewym i górn¹
				glNormal3f(-1.0f, 1.0f, 0.0f);
				glVertex3f(-A,  X, -A);
				glVertex3f(-A,  X,  A);
				glVertex3f(-X,  A,  A);
				glVertex3f(-X,  A, -A);
				////

				// (prostok¹t) pomiêdzy lewym i doln¹
				glNormal3f(-1.0f,-1.0f, 0.0f);
				glVertex3f(-X, -A, -A);
				glVertex3f(-X, -A,  A);
				glVertex3f(-A, -X,  A);
				glVertex3f(-A, -X, -A);
				////
		glEnd();         // Zakoñcz rysowanie czworok¹ta

		
					// ^^^ TRÓJK¥TY: ^^^ //
				glBegin(GL_TRIANGLES);  
					// PRZÓD:
					// (trójk¹t) przedni¹ -> lewy górny róg (góra, przód, lewy)
					glNormal3f(-1.0f, 1.0f, 1.0f);
					glVertex3f(-X,  A,  A);
					glVertex3f(-A,  X,  A);
					glVertex3f(-A,  A,  X);
					////
					
					// (trój¹t) przednia -> prawy górny róg (góra, przód, prawy)
					glNormal3f( 1.0f, 1.0f, 1.0f);
					glVertex3f( X,  A,  A);
					glVertex3f( A,  X,  A);
					glVertex3f( A,  A,  X);
					////

					// (trójk¹t) przedni¹ -> lewy dolny róg (dó³, przód, lewy)
					glNormal3f(-1.0f,-1.0f, 1.0f);
					glVertex3f(-X, -A,  A);
					glVertex3f(-A, -X,  A);
					glVertex3f(-A, -A,  X);
					////

					// (trójk¹t) przedni¹ -> prawy dolny róg (dó³, przód, prawy)
					glNormal3f( 1.0f,-1.0f, 1.0f);
					glVertex3f( X, -A,  A);
					glVertex3f( A, -X,  A);
					glVertex3f( A, -A,  X);
					////

					// TY£:
					// (trójk¹t) tylnia -> lewy górny róg (ty³, góra, lewy)
					glNormal3f(-1.0f, 1.0f,-1.0f);
					glVertex3f(-X,  A, -A);
					glVertex3f(-A,  X, -A);
					glVertex3f(-A,  A, -X);
					////
					
					// (trój¹t) tylnia -> prawy górny róg (ty³, góra, prawy)
					glNormal3f( 1.0f, 1.0f,-1.0f);
					glVertex3f( X,  A, -A);
					glVertex3f( A,  X, -A);
					glVertex3f( A,  A, -X);
					////

					// (trójk¹t) tylnia -> lewy dolny róg (ty³, dó³, lewy)
					glNormal3f(-1.0f,-1.0f,-1.0f);
					glVertex3f(-X, -A, -A);
					glVertex3f(-A, -X, -A);
					glVertex3f(-A, -A, -X);
					////

					// (trójk¹t) tylnia -> prawy dolny róg (ty³, dó³, prawy)
					glNormal3f( 1.0f,-1.0f,-1.0f);
					glVertex3f( X, -A, -A);
					glVertex3f( A, -X, -A);
					glVertex3f( A, -A, -X);
					////
				glEnd();         // Zakoñcz rysowanie trójk¹t
					
}

void Figura(int figura) 
{

		if (figura == 1)  // 4
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if(i == 0) glTranslatef( 0.0, -odl, 0.0);
				if(i == 1) glTranslatef( 0.0, 2*odl, 0.0);
				if(i == 2) glTranslatef( 0.0, odl, 0.0);
				//if(i == 3) glTranslatef( 0.0, -2*odl, 0.0);
			}
		}
		if (figura == 2)  // 6
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);

				if (i != 1) glTranslatef( 0.0,-odl, 0.0);
				if (i == 1) glTranslatef( odl, odl, 0.0);
				//if (i > 1) glTranslatef( 2.5, 2.05, 0.0);
			}
		}
		if (figura == 3)  // 4
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if(i == 0) glTranslatef( 0.0, -odl, 0.0);
				if(i == 1) glTranslatef( -odl, odl, 0.0);
				if(i == 2) glTranslatef( 0.0, odl, 0.0);
			}
		}
		if (figura == 4)  // 2
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if(i == 0) glTranslatef( 0.0, -odl, 0.0);
				if(i == 1) glTranslatef( odl, odl, 0.0);
				if(i == 2) glTranslatef( 0.0, odl, 0.0);
			}
		}
		if (figura == 5)  // 1
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if(i == 0) glTranslatef( 0.0, -odl, 0.0);
				if(i == 1) glTranslatef( 0.0,2*odl, 0.0);
				if(i == 2) glTranslatef( odl, 0.0, 0.0);
			}
		}
		if (figura == 6)  // 3
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if(i == 0) glTranslatef( 0.0, -odl, 0.0);
				if(i == 1) glTranslatef( 0.0,2*odl, 0.0);
				if(i == 2) glTranslatef(-odl, 0.0, 0.0);
			}
		}
		if (figura == 7)  // 7
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if(i == 0) glTranslatef( 0.0, -odl, 0.0);
				if(i == 1) glTranslatef( 0.0,2*odl, 0.0);
				if(i == 2) glTranslatef( odl,-odl, 0.0);
			}
		} 

	/*
		if (figura == 1)  // 4
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				glTranslatef( 0.0, odl, 0.0);
			}
		}
		if (figura == 2)  // 6
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if (i != 1) glTranslatef( 0.0, odl, 0.0);
				if (i == 1) glTranslatef( odl,-odl, 0.0);
				//if (i > 1) glTranslatef( 2.5, 2.05, 0.0);
			}
		}
		if (figura == 3)  // 4
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if (i != 1) glTranslatef( 0.0, odl, 0.0);
				if (i == 1) glTranslatef( -odl, 0.0, 0.0);
				//if (i > 1) glTranslatef( 2.5, 2.05, 0.0);
			}
		}
		if (figura == 4)  // 2
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if (i != 1) glTranslatef( 0.0, odl, 0.0);
				if (i == 1) glTranslatef( odl, 0.0, 0.0);
				//if (i > 1) glTranslatef( 2.5, 2.05, 0.0);
			}
		}
		if (figura == 5)  // 1
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if (i != 2) glTranslatef( 0.0, odl, 0.0);
				if (i == 2) glTranslatef( odl, 0.0, 0.0);
				//if (i > 1) glTranslatef( 2.5, 2.05, 0.0);
			}
		}
		if (figura == 6)  // 3
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if (i != 2) glTranslatef( 0.0, odl, 0.0);
				if (i == 2) glTranslatef( -odl, 0.0, 0.0);
				//if (i > 1) glTranslatef( 2.5, 2.05, 0.0);
			}
		}
		if (figura == 7)  // 7
		{
			for (int i = 0; i < 4; i++)	
			{
				Draw(1.0);
				if (i != 2) glTranslatef( 0.0, odl, 0.0);
				if (i == 2) glTranslatef( odl, -odl, 0.0);
				//if (i > 1) glTranslatef( 2.5, 2.05, 0.0);
			}
		} */
}

int DrawGLScene(GLvoid)									// Here's Where We Do All The Drawing
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);         // Wyczyœæ ekran i bufor g³êbi

		glLoadIdentity();
			
		Color (kolor);


		glTranslatef(0, 0, oddalenie);         // Oddal
		
		glRotatef(kat1, 1.0, 0.0, 0.0 );    //Obróæ
		glRotatef(kat2, 0.0, 1.0, 0.0 );    //Obróæ

		
	//	glRotatef(rquad_1,0,1.0f,0);         // Obróæ szeœcian wokó³ osi X, Y i Z

		glTranslatef(odl1, odl2, odl3);

		glRotatef(obrot, 0.0, 0.0, 1.0 );    //Obróæ

	//	glRotatef(rquad_2,0.0f,1.0f,0.0f);         // Obróæ szeœcian wokó³ osi X, Y i Z
		
		Figura (fig);

		if (podloga  + fig_odl[2]== max_podl) stop = TRUE;
		if (tryb == 0) {} 
		if (stop == TRUE ) {}
	    else if (grawitacja2 == TRUE) 
		{
		//	if (licznik2 == 0.0) podloga++;

			odl2-=0.7f;
			licznik2 += 1.0;
			//tryb = 0;
			if (licznik2 == 3.0)
			{
				grawitacja2 = FALSE;
				//	kat2+=5.5f;
		//		odl2-=0.3f;
				podloga++;
				licznik2= 0.0;
				wait = FALSE;
				wait2 = FALSE;
				//tryb= 0;
			}
		}	
		else if (grawitacja == TRUE) 
		{
	//		if (licznik2 == 0.0) podloga++;

			odl2-=0.35f;
			licznik2 += 1.0;
			//tryb = 0;
			if (licznik2 == 6.0)
			{
				grawitacja = FALSE;
				//	kat2+=5.5f;
				podloga++;
				licznik2= 0.0;
				wait2 = FALSE;
			}
		}
		if (podloga  + fig_odl[2]== max_podl) stop = TRUE;

		if (tryb == 1		&& sciana + fig_odl[0] <= prawa_max && sciana - fig_odl[2] >= lewa_max			//zakaz obrotu jak wybiegnie za œciane
							&& podloga + fig_odl[1]<= max_podl)				//zakaz obrotu jak za blisko pod³ogi (przyp 1)
			{     // kierunek obrotu ->
				if (licznik == 0.0)
				{
					int tmp = fig_odl[3];
					//
					for (int i= 3; i> 0; i--)
					{
						fig_odl[i] = fig_odl[i-1];
					}
				    fig_odl[0] = tmp;
				}
				
				obrot-=10.0f;
				licznik+=10.0;

				if (licznik == 90.0)
				{
					tryb = 0;
					wait = FALSE;
					licznik = 0.0;
				}
			}
		else if (tryb == 2		 && sciana - fig_odl[0] >= lewa_max && sciana + fig_odl[2] <= prawa_max 	
								 && podloga + fig_odl[3]<= max_podl) 
			{	 // kierunek obrotu: <-
				if (licznik == 0.0)
				{
					int tmp = fig_odl[0];
					//
					for (int i= 0; i< 3 ; i++)
					{
						fig_odl[i] = fig_odl[i+1];
					}
				    fig_odl[3] = tmp;
				}
				

				obrot+=10.0f;
				licznik+=10.0;

				if (licznik == 90.0)
				{
					tryb = 0;
					wait = FALSE;
					licznik = 0.0;
				//	kat2+=5.5f; h
				}
			}
		else if (tryb == 3 &&  sciana - fig_odl[3] != lewa_max) 
			{
				odl1-=0.35f;
				licznik+=1.0;

				if (licznik == 6.0)
				{
					tryb = 0;
					wait = FALSE;
					licznik = 0.0;
					sciana--;
				//	kat2+=5.5f;
				}
			}
		else if (tryb == 4  && sciana + fig_odl[1] != prawa_max) 
			{
				odl1+=0.35f;
				licznik+=1.0;

				if (licznik == 6.0)
				{
					tryb = 0;
					wait = FALSE;
					licznik = 0.0;
				//	kat2+=5.5f;
					sciana++;
				}
			}
		else	wait = FALSE;
	
    
    return TRUE;         // Wszystko ok
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
				fullscreen=FALSE;		// Windowed Mode Selected.  Fullscreen = FALSE
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

	//Wy³¹czenie tryby fullscrean
/*	// Ask The User Which Screen Mode They Prefer
	if (MessageBox(NULL,"Would You Like To Run In Fullscreen Mode?", "Start FullScreen?",MB_YESNO|MB_ICONQUESTION)==IDNO)
	{
		fullscreen=FALSE;							// Windowed Mode
	}*/

//	MessageBox(NULL,"Prze³¹czanie trybów - klawisze: 1, 2, 3, 4", "Info - Keyboard",MB_OK);

	fullscreen=FALSE;							// Windowed Mode

	// Create Our OpenGL Window
	CreateGLWindow("PROJ 9",640,480,16,fullscreen);

	DrawGLScene();	
	SwapBuffers(hDC);				// Swap Buffers (Double Buffering)// Draw The Scene


	odl2 = sufit;

	srand( time( NULL ) );
	fig= (rand()%7) + 1;

	srand( time( NULL ) );
	kolor= (rand()%6) + 1;

	for (int i = 0; i < 4; i++)		fig_odl[i]= fig_tab[fig][i];


	DWORD time1 = GetTickCount();

	while(!done)									// Loop That Runs While done=FALSE
	{
		DWORD time2 = GetTickCount();

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
		else		 							// If There Are No Messages
		{
			// Draw The Scene.  Watch For ESC Key And Quit Messages From DrawGLScene()
			if (active)								// Program Active?
			{
				
				if (keys[VK_ESCAPE])				// Was ESC Pressed?
				{
					done=TRUE;						// ESC Signalled A Quit
				}
				else if (keys[VK_SPACE] && wait2 == FALSE)				// Was SPACE Pressed?
				{
					grawitacja2 = TRUE;
					wait = TRUE;
					wait2 = TRUE;
				//	podloga++;
				}
				//else
				//{
				//	grawitacja2 = FALSE;
				//}
				else if (keys[VK_RETURN])				// Was ENT Pressed?
				{
					sciana =0;
					podloga =0;
					odl1 =0;
					odl2 = sufit;
					obrot= 0.0f;
					tryb=0;
					licznik=0.0;
					licznik2= 0.0;

					grawitacja= FALSE;
					grawitacja2= FALSE;
					stop = FALSE;
					wait = FALSE;
					wait2 = FALSE;

					srand( time( NULL ) );
					fig= (rand()%7) + 1;

					srand( time( NULL ) );
					kolor= (rand()%6) + 1;

					for (int i = 0; i < 4; i++)		fig_odl[i]= fig_tab[fig][i];
				}
				else if (keys['0'])
				{
				//	tryb = 0;
				//	grawitacja = FALSE;
					stop = TRUE;
				}
		/*		else if (keys['1'])
				{
					tryb = 1;
				}*/
				else if (keys['W'] && wait == FALSE && stop == FALSE)
				{
					tryb = 1;
					wait = TRUE;

		//			DrawGLScene();					// Draw The Scene
		//			SwapBuffers(hDC);				// Swap Buffers (Double Buffering)
				}
				else if (keys['S'] && wait == FALSE && stop == FALSE)
				{
					tryb = 2;
					wait = TRUE;

		//			DrawGLScene();					// Draw The Scene
		//			SwapBuffers(hDC);				// Swap Buffers (Double Buffering)
				}
				else if (keys['A'] && wait == FALSE && stop == FALSE)
				{
					tryb = 3;
					wait = TRUE;

	//				DrawGLScene();					// Draw The Scene
	//				SwapBuffers(hDC);				// Swap Buffers (Double Buffering)
				}
				else if (keys['D'] && wait == FALSE && stop == FALSE)
				{
					tryb = 4;
					wait = TRUE;

		//			DrawGLScene();					// Draw The Scene
		//			SwapBuffers(hDC);				// Swap Buffers (Double Buffering)
				}

				else if ( GetTickCount() - time2 >= 10 )
				{				
					DrawGLScene();					// Draw The Scene
					SwapBuffers(hDC);				// Swap Buffers (Double Buffering)
				}

				

				if ( GetTickCount() - time1 >= 1000 && grawitacja2 == FALSE)
				{	
//					tryb = 1;
					grawitacja = TRUE;
					wait2 = TRUE;
			//		podloga++;
		//			DrawGLScene();					// Draw The Scene
		//			SwapBuffers(hDC);				// Swap Buffers (Double Buffering)

					time1 = GetTickCount();
				//	while ( GetTickCount() - time <= 600 ) {}
				}
			

				if (keys[VK_RIGHT])     //kontrolki do testowania
				{
					kat2-=0.000001;
		//			kat2+=5.5f;
				}
				if (keys[VK_LEFT])
				{
					kat2+=0.000001;
		//			kat2-=5.5f;
				}
				if (keys[VK_UP])
				{
					kat1+=0.000001;
	//				kat1+=5.5f;
				}
				if (keys[VK_DOWN])
				{
					kat1-=0.000001;
		//			kat1-=5.5f;
				}
			}

			//Kod zmieniaj¹cy okno na tryb pe³noekranowy wy³¹czony //uniemo¿liwienie trybu fullscrean//
/*			if (keys[VK_F1])						// Is F1 Being Pressed?   
			{
				keys[VK_F1]=FALSE;					// If So Make Key FALSE
				KillGLWindow();						// Kill Our Current Window
				fullscreen=!fullscreen;				// Toggle Fullscreen / Windowed Mode
				// Recreate Our OpenGL Window
				if (!CreateGLWindow("NeHe's OpenGL Framework",640,480,16,fullscreen))
				{
					return 0;						// Quit If Window Was Not Created
				}
			}*/
		}
	}

	// Shutdown
	KillGLWindow();									// Kill The Window
	return (msg.wParam);							// Exit The Program
}
