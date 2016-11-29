#include "Application.h"

USING_NS_ENGINE;

HGLRC Application::_hRC = NULL;
HWND Application::_hWnd = NULL;
HINSTANCE Application::_hInstance;
HDC	Application::_hDC = NULL;

bool Application::_isActive = false;
bool Application::_keys[256];

int Application::_windowWidth;
int Application::_windowHeight;

GLvoid Application::resizeGLScene(GLsizei width, GLsizei height)
{
	if (height == 0) height = 1; // prevent a divide by zero

	// reset the current viewport
	glViewport(0, 0, width, height);

	glMatrixMode(GL_PROJECTION);						// select the Projection Matrix
	glLoadIdentity();									// reset the Projection Matrix

	// set coordinate system for the Window
	glOrtho(-width/2, width / 2, -height/2, height/2, 0.1, 400.0);

	glMatrixMode(GL_MODELVIEW);							// select the Modelview Matrix
	glLoadIdentity();									// reset the Modelview Matrix
}

int Application::initGL()
{
	glShadeModel(GL_SMOOTH);							// enable smooth shading
	glClearColor(0.f, 0.f, 0.f, 0.f);					// black background
	glClearDepth(1.0f);									// depth buffer setup
	glEnable(GL_DEPTH_TEST);							// enables depth testing
	glDepthFunc(GL_LEQUAL);								// the type of depth testing to do
	glHint(GL_PERSPECTIVE_CORRECTION_HINT, GL_NICEST);	// really nice Perspective Calculations

	GLfloat ambient[] = { 0.0, 0.85, 0.85, 1.0 };		
	GLfloat position[] = { 0.75, 0.75, 0.0, 0.0 };
	GLfloat specular[] = { 0.0, 0.0, 0.0, 0.5 };

	glLightfv(GL_LIGHT0, GL_AMBIENT, ambient);			// set 'ambient' light
	glLightfv(GL_LIGHT0, GL_POSITION, position);		// set 'position' light
	glLightfv(GL_LIGHT0, GL_SPECULAR, specular);		// set 'specular' light

	glEnable(GL_LIGHTING);
	glEnable(GL_LIGHT0); 

	return TRUE;										// initialization went OK
}

GLvoid Application::killWindow()						// properly kill the Window
{
	if (_hRC)											// do we have a Rendering Context?
	{
		if (!wglMakeCurrent(NULL, NULL))				// are we able to release the DC and RC contexts?
		{
			MessageBox(NULL, "Release Of DC And RC Failed.", "SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION);
		}

		if (!wglDeleteContext(_hRC))					// are we able to delete the RC?
		{
			MessageBox(NULL, "Release Rendering Context Failed.", "SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION);
		}
		_hRC = NULL;
	}

	if (_hDC && !ReleaseDC(_hWnd, _hDC))				// are we able to release the DC
	{
		MessageBox(NULL, "Release Device Context Failed.", "SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION);
		_hDC = NULL;
	}

	if (_hWnd && !DestroyWindow(_hWnd))					// are we able to destroy the Window?
	{
		MessageBox(NULL, "Could Not Release hWnd.", "SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION);
		_hWnd = NULL;
	}

	if (!UnregisterClass("OpenGL", _hInstance))			// are we able to unregister class
	{
		MessageBox(NULL, "Could Not Unregister Class.", "SHUTDOWN ERROR", MB_OK | MB_ICONINFORMATION);
		_hInstance = NULL;
	}
}

bool Application::createWindow(int width, int height, char* title)
{
	_windowWidth = width;
	_windowHeight = height;

	Application app;

	GLuint pixelFormat;									// holds the results after searching for a match
	WNDCLASS wc;										// Windows Class structure
	DWORD dwExStyle;									// Window Extended Style
	DWORD dwStyle;										// Window Style
	
	RECT windowRect;									// grabs rectangle upper left / lower right values
	windowRect.left = (long)0;							// set left value to 0
	windowRect.right = (long)width;						// set right value to requested width
	windowRect.top = (long)0;							// set top value to 0
	windowRect.bottom = (long)height;					// set bottom value to requested height

	_hInstance = GetModuleHandle(NULL);					// grab an instance for our Window
	wc.style = CS_HREDRAW | CS_VREDRAW | CS_OWNDC;		// redraw on size, and own DC for Window.
	wc.lpfnWndProc = (WNDPROC)wndProc;					// WndProc handles messages
	wc.cbClsExtra = 0;									// no extra Window data
	wc.cbWndExtra = 0;									// no extra Window data
	wc.hInstance = _hInstance;							// set the Instance
	wc.hIcon = LoadIcon(NULL, IDI_WINLOGO);				// load the default icon
	wc.hCursor = LoadCursor(NULL, IDC_ARROW);			// load the arrow pointer
	wc.hbrBackground = NULL;							// no background required for GL
	wc.lpszMenuName = NULL;								// we don't want a Menu
	wc.lpszClassName = "OpenGL";						// set the class name

	// attempt to register the Window class
	if (!RegisterClass(&wc))
	{
		MessageBox(NULL, "Failed To Register The Window Class.", "ERROR", MB_OK | MB_ICONEXCLAMATION);
		return FALSE;
	}

	dwExStyle = WS_EX_APPWINDOW | WS_EX_WINDOWEDGE;					// Window Extended Style
	dwStyle = WS_SYSMENU | WS_CAPTION | WS_MINIMIZEBOX;				// Windows Style

	// adjust Window to true requested size
	AdjustWindowRectEx(&windowRect, dwStyle, FALSE, dwExStyle);

	// create the Window
	if (!(_hWnd = CreateWindowEx(
		dwExStyle,											// extended style for the Window
		"OpenGL",											// class name
		title,												// Window title
		dwStyle |											// defined Window style
		WS_CLIPSIBLINGS |									// required Window style
		WS_CLIPCHILDREN,									// required Window style
		(GetSystemMetrics(SM_CXSCREEN) - width) / 2,		// Window position
		(GetSystemMetrics(SM_CYSCREEN) - height) / 2,		// Window position
		windowRect.right - windowRect.left,					// calculate Window width
		windowRect.bottom - windowRect.top,					// calculate Window height
		NULL,												// no parent Window
		NULL,												// no Menu
		_hInstance,											// Instance
		NULL)))												// don't pass anything to WM_CREATE
	{
		killWindow();
		MessageBox(NULL, "Window Creation Error.", "ERROR", MB_OK | MB_ICONEXCLAMATION);
		return FALSE;
	}

	static PIXELFORMATDESCRIPTOR pfd =				// 'pfd' tells Windows how we want things to be
	{
		sizeof(PIXELFORMATDESCRIPTOR),				// size of this pixel format descriptor
		1,											// version number
		PFD_DRAW_TO_WINDOW |						// format must support Window
		PFD_SUPPORT_OPENGL |						// format must Support OpenGL
		PFD_DOUBLEBUFFER,							// must support Double Buffering
		PFD_TYPE_RGBA,								// request an RGBA format
		32,											// select our color depth (number Of bits to use for color: 8/16/24/32)
		0, 0, 0, 0, 0, 0,							// color bits ignored
		0,											// no Alpha Buffer
		0,											// Shift Bit ignored
		0,											// no Accumulation Buffer
		0, 0, 0, 0,									// Accumulation Bits ignored
		16,											// 16Bit Z-buffer (Depth Buffer)  
		0,											// no Stencil Buffer
		0,											// no Auxiliary Buffer
		PFD_MAIN_PLANE,								// main drawing layer
		0,											// reserved
		0, 0, 0										// Layer Masks ignored
	};

	if (!(_hDC = GetDC(_hWnd)))						// did we get a Device Context?
	{
		killWindow();								// reset the Display
		MessageBox(NULL, "Can't Create A GL Device Context.", "ERROR", MB_OK | MB_ICONEXCLAMATION);
		return FALSE;
	}

	if (!(pixelFormat = ChoosePixelFormat(_hDC, &pfd)))	// did Windows find a matching Pixel Format?
	{
		killWindow();								// reset the Display
		MessageBox(NULL, "Can't Find A Suitable PixelFormat.", "ERROR", MB_OK | MB_ICONEXCLAMATION);
		return FALSE;
	}

	if (!SetPixelFormat(_hDC, pixelFormat, &pfd))	// are we able to set the Pixel Format?
	{
		killWindow();								// reset the Display
		MessageBox(NULL, "Can't Set The PixelFormat.", "ERROR", MB_OK | MB_ICONEXCLAMATION);
		return FALSE;
	}

	if (!(_hRC = wglCreateContext(_hDC)))			// are we able to get a Rendering Context?
	{
		killWindow();								// reset the Display
		MessageBox(NULL, "Can't Create A GL Rendering Context.", "ERROR", MB_OK | MB_ICONEXCLAMATION);
		return FALSE;
	}

	if (!wglMakeCurrent(_hDC, _hRC))				// try to activate the Rendering Context
	{
		killWindow();								// reset the Display
		MessageBox(NULL, "Can't Activate The GL Rendering Context.", "ERROR", MB_OK | MB_ICONEXCLAMATION);
		return FALSE;
	}

	ShowWindow(_hWnd, SW_SHOW);						// show the Window
	SetForegroundWindow(_hWnd);						// slightly higher priority
	SetFocus(_hWnd);								// sets keyboard focus to the Window
	app.resizeGLScene(width, height);				// set up our perspective GL Screen

	if (!app.initGL())								// initialize our newly created GL Window
	{
		killWindow();								// reset the Display
		MessageBox(NULL, "Initialization Failed.", "ERROR", MB_OK | MB_ICONEXCLAMATION);
		return FALSE;
	}

	return TRUE;									// Success
}

LRESULT CALLBACK Application::wndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	switch (uMsg)									// check for Windows messages
	{
		case WM_ACTIVATE:							// watch for Window Activate Message
		{
			if (!HIWORD(wParam))					// check minimization state
			{
				_isActive = true;					// program is active
			}
			else
			{
				_isActive = false;					// program is no longer active
			}

			return 0;
		}

		case WM_SYSCOMMAND:							// intercept system commands
		{
			switch (wParam)							// check system calls
			{
				case SC_SCREENSAVE:					// Screensaver trying to start?
				case SC_MONITORPOWER:				// monitor trying to enter Powersave?
				return 0;							// prevent from happening
			}

			break;
		}

		case WM_CLOSE:								// did we receive a Close Message?
		{
			PostQuitMessage(0);						// send a Quit Message
			return 0;
		}

		case WM_KEYDOWN:							// is a Key being held down?
		{
			_keys[wParam] = true;
			return 0;
		}

		case WM_KEYUP:								// has a Key been released?
		{
			_keys[wParam] = false;
			return 0;
		}
	}

	// pass all unhandled messages to DefWindowProc
	return DefWindowProc(hWnd, uMsg, wParam, lParam);
}

bool Application::isKeyDown(const unsigned char keyCode)
{
	return _keys[keyCode];
}

const Dimension& Application::getWindowSize()
{
	return Dimension(_windowWidth, _windowHeight);
}