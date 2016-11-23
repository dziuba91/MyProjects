using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aplikacja2__XNA_.Helper
{
	class TimeCounter
	{
		private int TIME_ARRAY_LENGHT = 100;

		public int[] timeArray;
		public int[] timeArray1;
		public int[] timeArray2;
		public int[] timeArray3;
		public int[] timeArray4;
		public int[] timeArray5;

		public int meanTime = 0;
		public int numPeriodTime = 0;
		public int startTime = 0;
		public int stopTime = 0;
		public int maxTime = 0;
		public int minTime = 0;

		int countTime = 0;
		int max = 0;
		int min = 0;

		int time0 = 0;

		public TimeCounter()
		{
			timeArray = new int[TIME_ARRAY_LENGHT];
			timeArray1 = new int[TIME_ARRAY_LENGHT];
			timeArray2 = new int[TIME_ARRAY_LENGHT];
			timeArray3 = new int[TIME_ARRAY_LENGHT];
			timeArray4 = new int[TIME_ARRAY_LENGHT];
			timeArray5 = new int[TIME_ARRAY_LENGHT];
		}

		public void countTIME(int timeAll, int time, int tryb)
		{
			if (numPeriodTime == 0)
			{
				startTime = time;
				max = time;
				min = time;
			}

			if (time > max)
			{
				max = time;
			}

			if (time < min)
			{
				min = time;
			}

			countTime += time;
			numPeriodTime++;

			if (Math.Abs(timeAll - time0) >= 1)
			{
				meanTime = countTime / numPeriodTime;
				stopTime = time;
				maxTime = max;
				minTime = min;

				time0 = timeAll;
				countTime = 0;
				numPeriodTime = 0;

				max = 0;
				min = 0;

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
	}
}
