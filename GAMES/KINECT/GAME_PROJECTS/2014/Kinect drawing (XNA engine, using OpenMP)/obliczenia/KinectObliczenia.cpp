#include <Windows.h>
#include <omp.h>
#include <iostream>
#include <cstdlib>

extern "C" int __declspec(dllexport) kinus(float * tX, float *tY, int *tI, int p1, int p2)
{
	int id = -1;
	int algorytmy = 3;
	int * spr = new int [algorytmy];
	for (int i=0; i< algorytmy; i++) spr[i]=0;

	#pragma omp parallel for
	for (int k = 0; k< algorytmy; k++)
		for (int i = 0; i < p2; i++)
			for (int j = 1; j<tI[i]; j++)
			{
				if (k==0) // weryfikacja 1
				{
					if (i==0)
					{
						if (tX[j-1]<=tX[j] && tY[j-1]>=tY[j]) spr[k]++;
					}

					if (i==1)
					{
						int pl = j +tI[i-1];
						if (tX[pl-1]<=tX[pl] && tY[pl-1]<=tY[pl]) spr[k]++;
					}
				}

				if (k==1) // weryfikacja 2
				{
					if (i==0)
					{
						if (tX[j-1]<=tX[j] && std::abs(tY[0]-tY[j])<20) spr[k]++;
					}

					if (i==1)
					{
						int pl1 = j +tI[i-1];
						if (tX[pl1-1]<=tX[pl1] && std::abs(tY[tI[i-1]+2]-tY[pl1])<20) spr[k]++;
					}

					if (i==2)
					{
						int pl2 = j +tI[0]+tI[1];
						if (tX[pl2-1]<=tX[pl2] && std::abs(tY[tI[0]+tI[1]+2]-tY[pl2])<20) spr[k]++;
					}
				}

				if (k==2) // weryfikacja 3
				{
					if (i==0)
					{
						if (tX[j-1]<=tX[j] && std::abs(tY[0]-tY[j])<20) spr[k]++;
					}

					if (i==1)
					{
						int pl1 = j +tI[i-1];
						if (tY[pl1-1]<=tY[pl1] && std::abs(tX[tI[i-1]+2]-tX[pl1])<20) spr[k]++;
					}

					if (i==2)
					{
						int pl2 = j +tI[0]+tI[1];
						if (tX[pl2-1]<=tX[pl2] && tY[pl2-1]>=tY[pl2]) spr[k]++;
					}

					if (i==3)
					{
						int pl3 = j +tI[0]+tI[1]+tI[2];
						if (tX[pl3-1]<=tX[pl3] && tY[pl3-1]<=tY[pl3]) spr[k]++;
					}
				}
			}

	int procent = (int)(spr[1]*100/p1);

	// warunki konieczne akceptacji algorytmu 1
	if (p2>=2 && tY[0]-70>tY[tI[0]-1] &&
		tY[tI[0]-1]+5 < tY[tI[0]+1] &&
		tX[tI[0]-1] > tX[tI[0]+1])
	{
		procent = (int)(spr[0]*100/p1);

		if (procent > 87) return procent + 10000;
		else return procent;
	}
	else if (p2>=3 && tX[0]+50<tX[tI[0]-1] &&  // warunki konieczne akceptacji algorytmu 2
		tY[0]+5 < tY[tI[0]+2] &&
		tY[tI[0]]+5 < tY[tI[0]+tI[1]+2])
	{
		procent = (int)(spr[1]*100/p1);
		if (procent > 87) return procent + 20000;
		else return procent;
	}
	else if (p2>=4 && tX[0]+50<tX[tI[0]-1])	 // warunki konieczne akceptacji algorytmu 3
	{
		procent = (int)(spr[2]*100/p1);
		if (procent > 87) return procent + 30000;
		else return procent;
	}

	return -1 * procent;
}