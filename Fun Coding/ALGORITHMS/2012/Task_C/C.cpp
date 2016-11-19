#include <cstdlib>
#include <iostream>
#include <conio.h>

using namespace std;

class KOLEJKA
{
	struct LISTA
	{
		int *tab;
		int n;
		int suma;
		int max;
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

	void wprowadz(int *tab, int n, int suma, int max) 
	{
		LISTA *p= new LISTA;

		p->tab= tab;
		p->n= n;
		p->suma= suma;
		p->max= max;
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
	int i,j;
	KOLEJKA kolejka;
	int *tab;
	int **A;
	int n,pol,testy,suma=0,max=-1;
	// 

	cin >> testy;

	while (testy)
	{
		cin >> n;

		tab= new int[n+1];

		for (i=1; i<n+1; i++)
		{
			cin >> tab[i];
			suma+=tab[i];
			
			if (tab[i]>max) max= tab[i];
		}

		kolejka.wprowadz(tab, n, suma, max);
		suma=0;
		max=-1;
		testy--;
	}

	while (kolejka.pusty()== 0)
	{
		tab= kolejka.poczatek->tab;
		n= kolejka.poczatek->n;
		suma= kolejka.poczatek->suma;
		max= kolejka.poczatek->max;

		//
		pol= suma/2;

		if (max> pol) cout << suma- ((suma- max)*2);
		else if (max== pol) cout << suma- (max*2);
		else if (max< pol)
		{
			A= new int* [pol+1];
			for (i = 0; i < pol+1; i++)
				A[i] = new int[n+1];

			for (int i=1; i< pol+1; i++)
			{
				if (i<tab[1]) A[i][1]=0;
				else A[i][1]= tab[1];
			}

			for (int i=2; i< n+1; i++)
				for (int x=1; x< pol+1; x++)
				{
					int c=0;
					if (x- tab[i]>0)
					{
						c=A[x-tab[i]][i-1]+tab[i];
					}

					if (A[x][i-1]>= c)
					{
						A[x][i]= A[x][i-1];
					}
					else
					{
						A[x][i]= A[x-tab[i]][i-1]+tab[i];
					}
				}
			
			cout << suma- (A[pol][n]*2);

			for (i = 0; i < pol+1; i++)
				delete [] A[i];
				
			delete [] A;
		}
		delete [] tab;
		kolejka.usun();
			
		if (kolejka.pusty()==0)
			cout << endl;
	}

	return EXIT_SUCCESS;
}