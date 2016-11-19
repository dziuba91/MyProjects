#include <math.h>
#include <omp.h>

float krok = 0.0005f;
float gornaGranica = 2.0f;
float dolnaGranica = -2.0f;

float f(float x)
{
	return sin(x);
}

////
float * wynikX;
float * wynikY;
float wynikCalkowania = 0;
float wynikCalkowaniaOMP = 0;
float wynikCalkowaniaOMP2 = 0;
float wynikCalkowania2 = 0;
int iloscElementow;

float maxY=0;
float maxX=0;
float minY=0;
float minX=0;

//float czas1, czas2;

void integral(float a, float b, float c)
{
	dolnaGranica = a;
	gornaGranica = b;
	krok = c;

	iloscElementow = (int)(abs(gornaGranica - dolnaGranica)/krok);
	float iloscElementow2 = (abs(gornaGranica - dolnaGranica)/krok);
	if (abs(iloscElementow2 - (float)iloscElementow) != 0.0f)
		iloscElementow++;

	wynikX = new float [iloscElementow];
	wynikY = new float [iloscElementow];

	float i, pF, pI;
	int index = 0;
	bool calkuj = false;

	//float czasStart, czasStop; 
	//czasStart = GetTickCount();

	for (i = dolnaGranica; i <= gornaGranica; i+=krok)
	{
		wynikY[index] = f(i);
		wynikX[index] = i;

		if (wynikY[index]>maxY) 
		{
			maxY = wynikY[index];
			maxX = wynikX[index];
		}
		else if (wynikY[index]<minY)
		{
			minY = wynikY[index];
			minX = wynikX[index];
		}

		if (calkuj == true)
		{
			wynikCalkowania += 0.5*(wynikY[index]+pF)*krok;
		}
		
		calkuj = true;

		pF = wynikY[index];
		pI = i;

		index++;
	}

	if (i != gornaGranica)
	{
		wynikX[index]= gornaGranica;
		wynikY[index] = f (gornaGranica);
		
		if (wynikY[index]>maxY) 
		{
			maxY = wynikY[index];
			maxX = wynikX[index];
		}
		else if (wynikY[index]<minY)
		{
			minY = wynikY[index];
			minX = wynikX[index];
		}

		wynikCalkowania += 0.5*(wynikY[index]+pF)*krok;
	}

	//czasStop = GetTickCount();
	//czas1 = czasStop - czasStart;

	// OpenMP
	index=0;
	calkuj = false;
	bool calkuj2 = false;
	int j, k;
	float krok2 = dolnaGranica;

	//czasStart = GetTickCount();

	int granica1 = (int)iloscElementow/2;
	float krok4 = dolnaGranica + granica1*krok;

	#pragma omp sections
	{
		#pragma omp section
		{
			for (j = 0; j <= granica1; j++)
			{
				wynikY[j] = f(krok2);
				wynikX[j] = krok2;

				if (wynikY[index]>maxY) 
				{
					maxY = wynikY[index];
					maxX = wynikX[index];
				}
				else if (wynikY[index]<minY)
				{
					minY = wynikY[index];
					minX = wynikX[index];
				}

				if (calkuj == true)
				{
					wynikCalkowaniaOMP += 0.5*(wynikY[j]+pF)*krok;
				}
		
				calkuj = true;

				pF = wynikY[j];
				pI = krok2;

				index++;
				krok2+=krok;
			}
		}

		#pragma omp section
		{
			for (k = granica1; k < iloscElementow; k++)
			{
				wynikY[k] = f(krok4);
				wynikX[k] = krok4;

				if (wynikY[index]>maxY) 
				{
					maxY = wynikY[index];
					maxX = wynikX[index];
				}
				else if (wynikY[index]<minY)
				{
					minY = wynikY[index];
					minX = wynikX[index];
				}

				if (calkuj2 == true)
				{
					wynikCalkowaniaOMP += 0.5*(wynikY[k]+pF)*krok;
				}
		
				calkuj2 = true;

				pF = wynikY[k];
				pI = krok4;

				index++;
				
				if (k+1 < iloscElementow)
					krok4+=krok;
			}
		}
	}
	
	float krok3= abs(krok4-gornaGranica);
	wynikCalkowaniaOMP += 0.5*(wynikY[k]+pF)*krok3;

	index=0;
	calkuj = false;
	krok2 = dolnaGranica;
	
	#pragma omp parallel for
	for (j = 0; j < iloscElementow; j++)
	{
		wynikY[index] = f(krok2);
		wynikX[index] = krok2;

		if (wynikY[index]>maxY) 
		{
			maxY = wynikY[index];
			maxX = wynikX[index];
		}
		else if (wynikY[index]<minY)
		{
			minY = wynikY[index];
			minX = wynikX[index];
		}

		if (calkuj == true)
		{
			wynikCalkowaniaOMP2 += 0.5*(wynikY[index]+pF)*krok;
		}
		
		calkuj = true;

		pF = wynikY[index];
		pI = krok2;

		index++;
		if (k+1 < iloscElementow)
		krok2+=krok;
	}
	
	krok3= abs(krok2-gornaGranica);
	wynikCalkowaniaOMP2 += 0.5*(wynikY[index]+pF)*krok3;

	index=0;
	calkuj = false;
	krok2 = dolnaGranica;
	
	for (j = 0; j < iloscElementow; j++)
	{
		wynikY[index] = f(krok2);
		wynikX[index] = krok2;

		if (wynikY[index]>maxY) 
		{
			maxY = wynikY[index];
			maxX = wynikX[index];
		}
		else if (wynikY[index]<minY)
		{
			minY = wynikY[index];
			minX = wynikX[index];
		}

		if (calkuj == true)
		{
			wynikCalkowania2 += 0.5*(wynikY[index]+pF)*krok;
		}
		
		calkuj = true;

		pF = wynikY[index];
		pI = krok2;

		index++;
		if (j+1 < iloscElementow)
		krok2+=krok;
	}
	
	krok3= abs(krok2-gornaGranica);
	wynikCalkowania2 += 0.5*(wynikY[index]+pF)*krok3;

	krok2+=krok3;
	wynikY[index] = f(krok2);
	wynikX[index] = krok2;
		
	//czasStop = GetTickCount();
	//czas2 = czasStop - czasStart;
}