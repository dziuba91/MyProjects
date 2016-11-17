#include "screen.h"
#include <stdio.h>

/*
	function drawFrame shows a rectangular frame on screen using
	UNICODE output.
	arguments:
		short x: starting x-coordinate of the frame
		short y: starting y-coordinate of the frame
		short w: frame width (including borders)
		short h: frame height
		short color: frame border color
	return: none
*/
void drawFrame(short x, short y, short w, short h, short color)
{
	size_t i;
	gotoxy(x, y);	// go to correct start position
	setColor(color, bg(BLACK));	// set color of the frame
	printf("%s", TLC);	// draw top left corner
	for(i=0; i<w-2; ++i) printf("%s", HL);	// draw top line until width (w) value
	printf("%s", TRC);	// draw top right corner
	for(i=0; i<h-2; ++i){		// draw right and left lines until height (h) valeu
		gotoxy(x, y+1+i);
		printf("%s", VL);	// draw vertical line
		gotoxy(x+w-1, y+1+i);
		printf("%s", VL);
	} // for
	gotoxy(x, y+h-1);
	printf("%s", BLC);	// draw bottom left corner
	for(i=0; i<w-2; ++i) printf("%s", HL);	// draw bottom line until width (w) value
	printf("%s", BRC);	// draw bottom right corner

	resetColor();	// set color back to default
} // function

/*
	function gotoxy set cursor position on the frame.
	arguments:
		short x: x-coordinate position of the frame
		short y: y-coordinate position of the frame
	return: none
*/
void gotoxy(short x, short y)
{
	printf("\033[%d;%dH", y, x);	// set cursor in correct position
	fflush(stdout);
} // function

/*
	function setColor setting the color of characters and background
	behind those character which been displaying after function will
	executed.
	arguments:
		short fg: color of character
		short bg: color of the background behind character
	return: none
*/
void setColor(short fg, short bg){
        printf("\033[%d;%d;1m", fg, bg);	// set character and background color
        fflush(stdout);
} // function

/*
	function resetColor sstting color of the characters and background
	behind those characters to default.
	arguments: none
	return: none
*/
void resetColor(void){
	printf("\033[0m");	// set colors to default
	fflush(stdout);
} // function

/*
	function clearScreen clear all the thing which was displaying on the screen -
	screen of the frame after this operation will be empty.
	arguments: none
	return: none
*/
void clearScreen(void){
	printf("\033[2J");	// clear screen
	fflush(stdout);
} // function

