#ifndef __TIMECOUNTER_H
#define __TIMECOUNTER_H

#include <windows.h>		// Header File For Windows
#include <math.h>
#include <stdio.h>

class TimeCounter
{
public:
	static int const TIME_ARRAY_LENGHT = 100;

	int timeArray[TIME_ARRAY_LENGHT];
	int timeArray1[TIME_ARRAY_LENGHT];
	int timeArray2[TIME_ARRAY_LENGHT];
	int timeArray3[TIME_ARRAY_LENGHT];
	int timeArray4[TIME_ARRAY_LENGHT];
	int timeArray5[TIME_ARRAY_LENGHT];

	int averageTime;
	int startTime;
	int stopTime;
	int maxTime;
	int minTime;

	int numPeriodTime;
	int countTime;
	int Mmax;
	int Mmin;
	
	int timet0;

public:
	TimeCounter();

	void countTIME(int timeAll, int time, int tryb);
};

#endif