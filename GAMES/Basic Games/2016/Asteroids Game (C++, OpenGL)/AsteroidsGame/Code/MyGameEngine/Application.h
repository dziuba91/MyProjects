#ifndef __MYGAMEENGINE_APPLICATION_H__
#define __MYGAMEENGINE_APPLICATION_H__

#include <windows.h>
#include <gl/GL.h>

#include "Definitions.h"
#include "Extensions/Dimension.h"

ENGINE_NS_BEGIN

/**
* create application Window
* initialize OpenGL (camera perspective, lights, etc.)
* set Window coordinate system (top-left corner: -width/2, height/2; bottom-right corner: width/2, -height/2)
*/
class Application
{
protected:
	/** the code creates OpenGL Window
	* @param width - width of the GL Window
	* @param height - height of the GL Window
	* @param title - title to appear at the top of the window
	*/
	static bool createWindow(int width, int height, char* title);

	/** reset the Display before exit */
	static GLvoid killWindow();

public:
	static bool isKeyDown(const unsigned char keyCode);

	static const Dimension& getWindowSize();

private:
	/** all setup for OpenGL */
	int initGL();

	/** resize and initialize the GL Window */
	GLvoid resizeGLScene(GLsizei width, GLsizei height);

	/** 
	* @param hWnd - handle for this Window
	* @param uMsg - message for this Window
	* @param wParam - additional message information
	* @param lParam - additional message information
	*/
	static LRESULT CALLBACK wndProc(HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam);

	//
	static HGLRC _hRC;					// * kontekst rysujący
	static HWND _hWnd;					// * uchwyt naszego okna
	static HINSTANCE _hInstance;		// * instancja aplikacji

	static int _windowWidth;
	static int _windowHeight;

	static bool _keys[256];				// * tablica klawiszy - wcisniety czy nie

protected:
	static HDC _hDC;					// * prywatny kontekst urzadzenia GDI

	static bool _isActive;				// * flaga - czy okno jest aktywne?
};

ENGINE_NS_END

#endif // __MYGAMEENGINE_APPLICATION_H__