#include <cstdlib>
#include <iostream>
#include <conio.h>
#include <math.h>

using namespace std;

class KOLEJKA
{
	struct LISTA
	{
		int L;
		int U;
		LISTA *next;
	}; 

public:
	LISTA *poczatek, *koniec;

public:
	KOLEJKA()
	{
		poczatek= NULL;
		koniec= NULL;
	}

public:
	bool pusty()
	{
		if (poczatek==NULL) return 1;
		else return 0;
	}

	void wprowadz(int L, int U) 
	{
		LISTA *p= new LISTA;

		p->L= L;
		p->U= U;
		p->next= NULL;

		if (pusty())
		{
			koniec= p;
			poczatek= koniec;
		}
		else
		{
			koniec->next= p;
			koniec= p;
		}
	}
	
	void usun()
	{
		if (pusty()==0)
		{
			LISTA *p;
			p= poczatek;
			poczatek= poczatek->next;
			delete p;
		}
	}
};

int main ()
{
	int L,U,A,x=0,y=0;
	int i,j;
	int nie_pierwsze;
	KOLEJKA kolejka;
	bool *tab;
	int *indeksy;
	double tmp;
	int n, m, NP=0;
	int S;
	// 

	for (;;)
	{
		cin >> L >> U;

		if (L==-1 && U==-1) break;

		kolejka.wprowadz(L,U);
	}
	//

	while (kolejka.pusty()== 0)
	{
		L= kolejka.poczatek->L;
		U= kolejka.poczatek->U;

		if (U<2)
		{
			cout << L << " " << U << " " << x << " " << y;
		}
		else
		{
			tab= new bool [U+1];

			for (i=2; i<=U; i++)
			{
				tab[i]= false;
			}

			for (i=2; i*i<=U; i++)
			{
				if (tab[i]== true) continue;

				for (j=2*i; j<=U; j+=i)
				{
					if (tab[j]== false && j>=L)
					{
						NP++;
						tab[j]= true;
					}		
				}
			}

			if (L>=2)	
			{
				nie_pierwsze=NP;
				A= L;
				if(L==2) y=1;
			}
			else	
			{
				nie_pierwsze=2-L+NP;
				A= 2;
				y=1;
			}
	
			x= U-L+1-nie_pierwsze;

			for (i= U; i>=2; i--)
			{
				if (tab[i]== false)
				{
					tmp= i;
					break;
				}
			}

			for (i=(int)sqrt(tmp); i>=1; i--)
			{
				for (j=i-1; j>=1; j--)
				{
					if (i*i+j*j>=A && i*i+j*j<=tmp && tab[i*i +j*j]==false)
					{
						y++;
					}
				}
			}
		
			cout << L << " " << U << " " << x << " " << y;

			NP= 0;
			delete [] tab;
		}

		kolejka.usun();
		x= 0;
		y= 0;
		
		if(kolejka.pusty()==0)
			cout << endl;
	}
	
	return EXIT_SUCCESS;
}
