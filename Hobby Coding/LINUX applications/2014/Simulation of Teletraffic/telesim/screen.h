#ifndef __SCREEN_H		// prevent from multiple compiling
#define __SCREEN_H

#define HL "\u2500"	// horizontal line
#define VL "\u2502"	// vertical line
#define TLC "\u250c"	// top left corner
#define TRC "\u2510"	// top right corner
#define BLC "\u2514"	// bottom left corner
#define BRC "\u2518"	// bottom right corner

enum COLORS{BLACK=30,RED,GREEN,YELLOW,BLUE,MAGENTA,CYAN,WHITE};		// color enumeration
#define bg(a) a+10 // macro definition

// function prototype
void setColor(short, short);
void resetColor(void);
void gotoxy(short, short);
void clearScreen(void);
void drawFrame(short, short, short, short, short);

#endif
