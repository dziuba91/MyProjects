#ifndef __TIMECOUNTER_H		// prevent from multiple compiling
#define __TIMECOUNTER_H

#include <windows.h>		// Header File For Windows
#include <math.h>
#include <stdio.h>

int const TIME_ARRAY_LENGHT = 100;

int timeArray[TIME_ARRAY_LENGHT];
int timeArray1[TIME_ARRAY_LENGHT];
int timeArray2[TIME_ARRAY_LENGHT];
int timeArray3[TIME_ARRAY_LENGHT];
int timeArray4[TIME_ARRAY_LENGHT];
int timeArray5[TIME_ARRAY_LENGHT];

int meanTime = 0;
int startTime = 0;
int stopTime = 0;
int maxTime = 0;
int minTime = 0;

int numPeriodTime = 0;
int countTime = 0;
int Mmax = 0;
int Mmin = 0;
	
int timet0;

void countTIME(int timeAll, int time, int tryb)
{
     if (numPeriodTime == 0)
     {
          startTime = time;
          Mmax = time;
          Mmin = time;
     }

     if (time > Mmax)
     {
          Mmax = time;
     }
	
     if (time < Mmin)
     {
          Mmin = time;
     }

     countTime += time;
     numPeriodTime++;

     if (timeAll - timet0 >= 1000)
     {
          meanTime = countTime / numPeriodTime;
          stopTime = time;
          maxTime = Mmax;
          minTime = Mmin;

          timet0 = timeAll;
          countTime = 0;
          numPeriodTime = 0;
	
		  Mmax = 0;
		  Mmin = 0;

          for (int i = TIME_ARRAY_LENGHT - 2; i >= 0; i--)
          {
               timeArray[i + 1] = timeArray[i];
	
               if (tryb == 1)
               {
                   timeArray1[i + 1] = timeArray1[i];
               }
               else if (tryb == 2)
               {
                   timeArray2[i + 1] = timeArray2[i];
               }
               else if (tryb == 3)
               {
                   timeArray3[i + 1] = timeArray3[i];
               }
               else if (tryb == 4)
               {
                   timeArray4[i + 1] = timeArray4[i];
               }
               else if (tryb == 5)
               {
                   timeArray5[i + 1] = timeArray5[i];
               }
          }
	
          timeArray[0] = meanTime;

          if (tryb == 1)
          {
               timeArray1[0] = meanTime;
          }
          else if (tryb == 2)
          {
               timeArray2[0] = meanTime;
          }
          else if (tryb == 3)
          {
               timeArray3[0] = meanTime;
          }
          else if (tryb == 4)
          {
               timeArray4[0] = meanTime;
          }
          else if (tryb == 5)
          {
               timeArray5[0] = meanTime;
          }
     }
}

#endif