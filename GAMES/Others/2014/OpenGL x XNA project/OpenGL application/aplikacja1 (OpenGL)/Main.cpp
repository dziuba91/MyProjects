/*
// **** //
 *		Autor:				Dziuba Tomasz
 *		Data wykonania:		09.03.2014 (aktualizacja: 2015)
// **** //
*/

#include <windows.h>		// Header File For Windows
#include <gl\gl.h>			// Header File For The OpenGL32 Library
#include <gl\glu.h>			// Header File For The GLu32 Library
#include <gl\glaux.h>		// Header File For The Glaux Library
#include <gl\glut.h>
#include <stdio.h>

#include "Fps.h"
#include "TimeCounter.h"
#include "TextArea.h"

#include "T1.h"
#include "T2.h"
#include "T4.h"
#include "T4_v2.h"
#include "T3.h"
#include "T5.h"

#include "WINDOW_prop.h" // window settings code

bool wait = FALSE;
bool trybCube = true;

FPS fps1;
TimeCounter time1;

T1 t1;
T2 t2;
T3 t3;
T4 t4;
T4_v2 t4_2;
T5 t5(&fps1);

TextArea PARAM_DRAW(&fps1, &time1);

int numberOfQuotes = 0;

int tryb = 1; //tryby poruszania siê
bool timePause = false;

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
				else if (keys['0'] && wait == FALSE)
				{
					tryb=0;
				}
				else if (keys['1'] && wait == FALSE)
				{
					tryb = 1;
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
				}
				else if (keys['6'] && wait == FALSE)
				{
					tryb = 6;
				}
				else if (keys[VK_SPACE] && wait == FALSE && tryb == 4)
				{
					if (trybCube) trybCube = false;
					else trybCube = true;

					wait = TRUE;
				}
				else if (keys[VK_SPACE] && wait == FALSE && tryb == 4)
				{
					if (trybCube) trybCube = false;
					else trybCube = true;

					wait = TRUE;
				}
				else if (keys[VK_CONTROL] && wait == FALSE)
				{
					if (timePause) timePause = false;
					else timePause = true;

					wait = TRUE;
				}
				else if (keys['O'] && tryb == 4 && wait == FALSE)
				{
					if (t4.sim == FALSE) t4.sim = TRUE;
					else t4.sim = FALSE;

					wait = TRUE;
				}
				else
				{
					if ( wait == TRUE && (GetTickCount() - time >= 200) ) { wait = FALSE; }
					fps1.countFPS(GetTickCount(), tryb);
					
					PARAM_DRAW.DrawGLScene(tryb, timePause);

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
							t4_2.keyboard44(keys);
							t4_2.DrawGLScene44();
						}
					}
					else if (tryb == 5)
					{
						t5.keyboard5(keys);
						t5.DrawGLScene5();
					}

					int stopT = GetTickCount();

					if (!timePause)
					{
						time1.countTIME(GetTickCount(), stopT- startT, tryb);
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
