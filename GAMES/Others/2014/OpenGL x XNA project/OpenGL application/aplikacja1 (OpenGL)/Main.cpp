/*
// **** //
 *		Autor:				Dziuba Tomasz
 *		Data wykonania:		09.03.2014
// **** //
*/


#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <stdio.h>

int tryb = 1; //tryby poruszania siê

bool timePause = false;

#include "fps.h"
#include "timeCounter.h"
#include "test_parametrs_draw.h"

#include "draw_cube.h"

#include "t0.h"
#include "t1.h"
#include "t2.h"
#include "t4.h"
#include "t44.h"
#include "t3.h"
#include "t5.h"

#include "WINDOW_prop.h"


bool wait = FALSE;
//GLfloat speed1 = 0.5f;

bool trybCube = true;

T1 t1;
T2 t2;
T3 t3;
T4 t4;
T44 t44;
T5 t5;

TestParametrsDraw PARAM_DRAW;


int numberOfQuotes = 0;
/*
GLfloat material1[]={0.0, 0.0, 0.8, 0.0};			//NIEBIESKI
GLfloat material3[]={0.8, 0.0, 0.0, 0.0};			//ZIELONY
GLfloat material2[]={0.0, 0.8, 0.0, 0.0};			//CZERWONY
GLfloat material5[]={0.8, 0.8, 0.0, 0.0};			//¿Ó£TY
GLfloat material4[]={0.8, 0.3, 0.0, 0.0};			//POMARAÑCZOWY
GLfloat material6[]={0.8, 0.8, 0.8, 0.0};			//BIA£Y
GLfloat ambient[]={0.3, 0.3, 0.3, 0.0};			//Oœwietlenia
GLfloat diffuse[]={0.0, 0.0, 0.0, 0.5};
GLfloat position[]={15.0, 30.0, 0.0, 3.0}; 
GLfloat specular[]={0.0, 0.0, 0.0, 0.5};         // Wartoœci œwiat³a rozproszonego 
*/
						


int WINAPI WinMain(	HINSTANCE	hInstance,			// Instance
					HINSTANCE	hPrevInstance,		// Previous Instance
					LPSTR		lpCmdLine,			// Command Line Parameters
					int			nCmdShow)			// Window Show State
{
	MSG		msg;									// Windows Message Structure
	BOOL	done=FALSE;								// Bool Variable To Exit Loop
	

    numberOfQuotes=4;

	fullscreen=FALSE;							// Windowed Mode

	// Create Our OpenGL Window
	CreateGLWindow("OpenGL",windowHeight,windowWidth,16,fullscreen);
	DWORD time = GetTickCount();

	while(!done)									// Loop That Runs While done=FALSE
	{
		if (wait== FALSE) time = GetTickCount();

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
				/*
				else if (keys[VK_RIGHT] && wait == FALSE)     //kontrolki do testowania
				{
					tryb=1;
				//	speed1 = 0.5f;
				}
				else if (keys[VK_LEFT] && wait == FALSE)
				{
					tryb=2;
				//	speed1 = 0.5f;
				}
				else if (keys[VK_UP] && wait == FALSE)
				{
					tryb=3;
				//	speed1 = 0.5f;
				}
				else if (keys[VK_DOWN] && wait == FALSE)
				{
					tryb=4;			
				//	speed1 = 0.5f;
				}
				*/
				else if (keys['0'] && wait == FALSE)
				{
					tryb=0;
				}
				else if (keys['1'] && wait == FALSE)
				{
					tryb = 1;
					//InitGL1();
				}
				else if (keys['2'] && wait == FALSE)
				{
					tryb = 2;
				}
				else if (keys['3'] && wait == FALSE)
				{
					tryb = 3;
				}
				else if (keys['4'] && wait == FALSE)
				{
					tryb = 4;
				}
				else if (keys['5'] && wait == FALSE)
				{
					tryb = 5;
					//InitGL5();
				}
				else if (keys['6'] && wait == FALSE)
				{
					tryb = 6;
					//InitGL5();
				}
				/*
				else if (keys['6'] && wait == FALSE)
				{
					tryb = 6;
					InitGL5();
				}*/
				else if (keys[VK_SPACE] && wait == FALSE && tryb == 4)
				{
					if (trybCube) trybCube = false;
					else trybCube = true;

					wait = TRUE;
					//speed1-=0.0000001f;
				}
				else if (keys[VK_SPACE] && wait == FALSE && tryb == 4)
				{
					if (trybCube) trybCube = false;
					else trybCube = true;

					wait = TRUE;
					//speed1-=0.0000001f;
				}
				else if (keys[VK_CONTROL] && wait == FALSE)
				{
					if (timePause) timePause = false;
					else timePause = true;

					wait = TRUE;
					//speed1-=0.0000001f;
				}
				else if (keys['O'] && tryb == 4 && wait == FALSE)
				{
					if (sim == FALSE) sim = TRUE;
					else sim = FALSE;

					wait = TRUE;
				}
				else
				{
					if ( wait == TRUE && (GetTickCount() - time >= 200) ) { wait = FALSE; }
					countFPS(GetTickCount(), tryb);
					
					PARAM_DRAW.DrawGLScene();

					int startT = GetTickCount();

					if (tryb == 0)
					{
						//DrawGLScene0();	
					}
					else if (tryb == 1)
					{
						t1.keyboard1(keys);
						t1.DrawGLScene1();	
					}
					else if (tryb == 2)
					{
						t2.keyboard2(keys);
						t2.DrawGLScene2();	
					}
					else if (tryb == 3)
					{
						t3.keyboard3(keys);
						t3.DrawGLScene3();	
					}
					else if (tryb == 4)
					{
						if (trybCube)
						{
							t4.keyboard4(keys);
							t4.DrawGLScene4();
						}
						else
						{
							t44.keyboard44(keys);
							t44.DrawGLScene44();
						}
					}
					//else if (tryb == 5)
						//DrawGLScene55();					// Draw The Scene
					else if (tryb == 5)
					{
						t5.keyboard5(keys);
						t5.DrawGLScene5();
					}

					int stopT = GetTickCount();

					if (!timePause)
					{
						countTIME(GetTickCount(), stopT- startT, tryb);
					}

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
