/*
// **** //
 *		Program:			Szeœciany
 *		Autor:				Dziuba Tomasz
 *		Data wykonania:		28.12.2012
// **** //
*/



#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library


HDC			hDC=NULL;         // Prywatny kontekst u¿¹dzenia GDI
HGLRC		hRC=NULL;         // Kontekst rysuj¹cy
HWND		hWnd=NULL;         // Uchwyt naszego okna
HINSTANCE	hInstance;         // Instancja aplikacji

bool keys[256];         // Tablica klawiszy - wciœniêty czy nie
bool active=TRUE;         // Flaga - czy okno jest aktywne?
bool fullscreen=TRUE;         // Uruchom aplikacje na pe³nym ekranie

float odl = 3; //odelg³oœæ miêdzy kwadracikami
float oddalenie = -20;  //oddalenie widoku (oddalenie miejsca rysowania bry³y)
float kat = 35; //k¹]t rysowania bry³y 
int ilosc = 3; //wskazuje na iloœæ jednego rz¹du szeœcianów (liczba szeœcianów = ilosc * ilosc * ilosc) 

int tryb =1; //tryby poruszania siê
int tryb_3 =1;
int tryb_4 =1;

GLfloat rquad_1;         // K¹t obroty czworok¹ta ca³ego szeœcianu
GLfloat rquad_2;		 // K¹t obrotu poszczególnych szeœcianów

float color1 = 0.8;        //wskaŸniki kontroluj¹ce kolor na trzech szeœcianach
float color2 = 0;

GLfloat material[]={0.0, 0.0, 0.8, 0.0};			//kolory materia³ów
GLfloat material1[]={color1, 0.0, color2, 0.0};
GLfloat material2[]={0.0, color1, color2, 0.0};
GLfloat material3[]={color1, color1, color2, 0.0};
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

void DrawCube(float X)
{
	glBegin(GL_QUADS);         // Zacznij rysowanie szeœcianu
		glNormal3f( 0.0f, 0.0f, 1.0f);
		glVertex3f(-X, -X,  X);
		glVertex3f( X, -X,  X);
		glVertex3f( X,  X,  X);
		glVertex3f(-X,  X,  X);

		glNormal3f( 0.0f, 0.0f,-1.0f);
		glVertex3f(-X, -X, -X);
		glVertex3f(-X,  X, -X);
		glVertex3f( X,  X, -X);
		glVertex3f( X, -X, -X);

		glNormal3f( 0.0f, 1.0f, 0.0f);
		glVertex3f(-X,  X, -X);
		glVertex3f(-X,  X,  X);
		glVertex3f( X,  X,  X);
		glVertex3f( X,  X, -X);

		glNormal3f( 0.0f,-1.0f, 0.0f);
		glVertex3f(-X, -X, -X);
		glVertex3f( X, -X, -X);
		glVertex3f( X, -X,  X);
		glVertex3f(-X, -X,  X);

		glNormal3f( 1.0f, 0.0f, 0.0f);
		glVertex3f( X, -X, -X);
		glVertex3f( X,  X, -X);
		glVertex3f( X,  X,  X);
		glVertex3f( X, -X,  X);

		glNormal3f(-1.0f, 0.0f, 0.0f);
		glVertex3f(-X, -X, -X);
		glVertex3f(-X, -X,  X);
		glVertex3f(-X,  X,  X);
		glVertex3f(-X,  X, -X); 
    glEnd();         // Zakoñcz rysowanie czworok¹ta
}

int DrawGLScene(GLvoid)									// Here's Where We Do All The Drawing
{
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);         // Wyczyœæ ekran i bufor g³êbi

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
				
				if (k == 1 && i == j && i == 0)
				{
					glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material1);
					glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material1);
					glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material1);
				}
				else if (k == 1 && i == j && i == 1)
				{
					glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material2);
					glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material2);
					glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material2);
				}
				else if (k == 1 && i == j && i == 2)
				{
					glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material3);
					glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material3);
					glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material3);
				}
				else
				{
					glMaterialfv(GL_FRONT_AND_BACK, GL_AMBIENT, material);
					glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material);
					glMaterialfv(GL_FRONT_AND_BACK, GL_SPECULAR, material);
				}

				glTranslatef(0, 0, oddalenie);         // Oddal
				glRotatef(kat, 1.0, 0.0, 0.0 );    //Obróæ


				glRotatef(rquad_1,0,1.0f,0);         // Obróæ szeœcian wokó³ osi X, Y i Z

				glTranslatef(odl*a, odl*b, odl*c);
		
				glRotatef(rquad_2,0.0f,1.0f,0.0f);         // Obróæ szeœcian wokó³ osi X, Y i Z
				
				
				DrawCube(1.0);

				glTranslatef(odl,0.0f,0.0f);    
			}
		}
	}

	if (tryb == 1) rquad_1-=0.5f;         // Zmniejsz licznik obrotu czworok¹ta
	else if (tryb == 2) rquad_2-=1.0f;         // Zmniejsz licznik obrotu czworok¹ta
	else if (tryb == 3) 
		{
			if (tryb_3 == 1) odl-=0.02f;
			else odl +=0.03f;

			if (odl >= 4) tryb_3 = 1;
			if (odl <= 3) tryb_3 = 2;
		}
	else if (tryb == 4)
		{
			if (tryb_4 == 1) 
				{
					color1-=0.008;
					color2+=0.008;

					material1[0]=color1;
					material1[2]=color2;
					material2[1]=color1;
					material2[2]=color2;
					material3[0]=color1;
					material3[1]=color1;
					material3[2]=color2;
				}
			else
				{
					color1+=0.008;
					color2-=0.008;

					material1[0]=color1;
					material1[2]=color2;
					material2[1]=color1;
					material2[2]=color2;
					material3[0]=color1;
					material3[1]=color1;
					material3[2]=color2;
				}

			if (color2 >= 0.8) tryb_4 = 2;
			if (color2 <= 0.0) tryb_4 = 1;
		}
    
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

	MessageBox(NULL,"Prze³¹czanie trybów - klawisze: 1, 2, 3, 4", "Info - Keyboard",MB_OK);

	fullscreen=FALSE;							// Windowed Mode

	// Create Our OpenGL Window
	CreateGLWindow("PROJ 1",640,480,16,fullscreen);

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
		else		 							// If There Are No Messages
		{
			// Draw The Scene.  Watch For ESC Key And Quit Messages From DrawGLScene()
			if (active)								// Program Active?
			{
				if (keys[VK_ESCAPE])				// Was ESC Pressed?
				{
					done=TRUE;						// ESC Signalled A Quit
				}
/*				else if (keys[VK_RIGHT])     //kontrolki do testowania
				{
					rquad_1+=0.000001;
				}
				else if (keys[VK_LEFT])
				{
					rquad_1-=0.000001;
				}
				else if (keys[VK_UP])
				{
					rquad_2+=0.000001;
				}
				else if (keys[VK_DOWN])
				{
					rquad_2-=0.000001;
				}*/
				else if (keys['1'])
				{
					tryb = 1;
				}
				else if (keys['2'])
				{
					tryb = 2;
				}
				else if (keys['3'])
				{
					tryb = 3;
				}
				else if (keys['4'])
				{
					tryb = 4;
				}
				else
				{
					while ( GetTickCount() - time <= 20 ) {}
					DrawGLScene();					// Draw The Scene
					SwapBuffers(hDC);				// Swap Buffers (Double Buffering)
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
