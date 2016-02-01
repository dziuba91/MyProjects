#include <Windows.h>
#include <omp.h>

int i=0;

void sortowanie_babelkowe1(int tab[], int n)
{
	int x;

	for(int i=0; i<n-1; i++)
		{	for (int j=0; j<n-1; j++)
			{
				if (tab[j]> tab[j+1])
				{
					x= tab[j];
					tab[j]= tab[j+1];
					tab[j+1]= x;
				}
			}
		}
}

void sortowanie_babelkowe2(int tab[], int n)
{
	int x;

	#pragma omp parallel for
		for(int i=0; i<n-1; i++)
			{	
				for (int j=0; j<n-1; j++)
				{
					if (tab[j]> tab[j+1])
					{
						x= tab[j];
						tab[j]= tab[j+1];
						tab[j+1]= x;
					}
				}
			}
}

extern "C" int __declspec(dllexport) Balabala()
{
	return i;
}

extern "C" int __declspec(dllexport) Add(bool a)
{
	int startTick;
	int endTick;

	int n = 10000;

	int *tab= new int[n];
	int *tabW1= new int[n];
	int *tabW2= new int[n];

	for (int i=0; i<n; i++)
	{
		tab[i]= rand()%1000;
		tabW1[i]= tab[i];
		tabW2[i]= tab[i];
	}

	if (a)
	{
		startTick = GetTickCount();
			sortowanie_babelkowe1(tabW1,n);
		endTick = GetTickCount();
	}
	else
	{
		startTick = GetTickCount();
			sortowanie_babelkowe2(tabW2,n);
		endTick = GetTickCount();
	}

	i++;

    return endTick-startTick;
	//return 1;
}