#include "fps.h"

FPS::FPS()
{
	fps = 0;
	count = 0;
	Time0 = 0;
}

void FPS::countFPS(float time, int tryb)
{
	count ++;

	if (time -Time0 >= 1000)
	{
		fps = count;
		count = 0;

		Time0 = time;

		for (int i = FPS_ARRAY_LENGHT - 2; i >= 0; i--)
		{
			fpsArray[i+1] = fpsArray[i];
			fpsArray_X[i+1] = fpsArray_X[i];

			if (tryb == 1) 
			{
				fpsArray1[i+1] = fpsArray1[i];
				fpsArray1_X[i+1] = fpsArray1_X[i]; 
			}
			else if (tryb == 2) 
			{
				fpsArray2[i+1] = fpsArray2[i];
				fpsArray2_X[i+1] = fpsArray2_X[i];
			}
			else if (tryb == 3) 
			{
				fpsArray3[i+1] = fpsArray3[i];
				fpsArray3_X[i+1] = fpsArray3_X[i];
			}
			else if (tryb == 4) 
			{
				fpsArray4[i+1] = fpsArray4[i];
				fpsArray4_X[i+1] = fpsArray4_X[i];
			}
			else if (tryb == 5) 
			{
				fpsArray5[i+1] = fpsArray5[i];
				fpsArray5_X[i+1] = fpsArray5_X[i];
			}
		}

		fpsArray[0] = fps;
		fpsArray_X[0] = time;

		if (tryb == 1) 
		{
			fpsArray1[0] = fps;
			fpsArray1_X[0] = time;
		}
		else if (tryb == 2) 
		{
			fpsArray2[0] = fps;
			fpsArray2_X[0] = time;
		}
		else if (tryb == 3) 
		{
			fpsArray3[0] = fps;
			fpsArray3_X[0] = time;
		}
		else if (tryb == 4) 
		{
			fpsArray4[0] = fps;
			fpsArray4_X[0] = time;
		}
		else if (tryb == 5) 
		{
			fpsArray5[0] = fps;
			fpsArray5_X[0] = time;
		}
	}
}