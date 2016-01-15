
#include "time_counter.h"

TimeCounter::TimeCounter()
{
	averageTime = 0;
	startTime = 0;
	stopTime = 0;
	maxTime = 0;
	minTime = 0;

	numPeriodTime = 0;
	countTime = 0;
	Mmax = 0;
	Mmin = 0;
}

void TimeCounter::countTIME(int timeAll, int time, int tryb)
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
          averageTime = countTime / numPeriodTime;
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
	
          timeArray[0] = averageTime;

          if (tryb == 1)
          {
               timeArray1[0] = averageTime;
          }
          else if (tryb == 2)
          {
               timeArray2[0] = averageTime;
          }
          else if (tryb == 3)
          {
               timeArray3[0] = averageTime;
          }
          else if (tryb == 4)
          {
               timeArray4[0] = averageTime;
          }
          else if (tryb == 5)
          {
               timeArray5[0] = averageTime;
          }
     }
}