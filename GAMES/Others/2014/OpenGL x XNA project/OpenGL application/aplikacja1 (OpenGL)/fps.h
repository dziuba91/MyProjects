#pragma once

#include <windows.h>		// Header File For Windows
#include <math.h>
#include <stdio.h>

class FPS
{
public:
	static const int FPS_ARRAY_LENGHT = 100;

	int fpsArray[FPS_ARRAY_LENGHT];
	int fpsArray1[FPS_ARRAY_LENGHT];
	int fpsArray2[FPS_ARRAY_LENGHT];
	int fpsArray3[FPS_ARRAY_LENGHT];
	int fpsArray4[FPS_ARRAY_LENGHT];
	int fpsArray5[FPS_ARRAY_LENGHT];

	int fpsArray_X[FPS_ARRAY_LENGHT];
	int fpsArray1_X[FPS_ARRAY_LENGHT];
	int fpsArray2_X[FPS_ARRAY_LENGHT];
	int fpsArray3_X[FPS_ARRAY_LENGHT];
	int fpsArray4_X[FPS_ARRAY_LENGHT];
	int fpsArray5_X[FPS_ARRAY_LENGHT];

	int fps;
	int count;
	float Time0;

public:
	FPS();

	void countFPS(float, int);
};