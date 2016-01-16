#include <cstdlib>
#include <iostream>
#include <conio.h>
#include <iomanip>
#include <cmath>

using namespace std;


bool gauss(int n, double ** a, double *X)
{
  	int i, j, k, max;
	double t;
	for (i = 0; i < n; ++i) {
		max = i;
		for (j = i + 1; j < n; ++j)
			if (fabs(a[j][i]) > fabs(a[max][i]))
				max = j;
		
		for (j = 0; j < n + 1; ++j) {
			t = a[max][j];
			a[max][j] = a[i][j];
			a[i][j] = t;
		}
		
		for (j = n; j >= i; --j)
			for (k = i + 1; k < n; ++k)
				a[k][j] -= a[k][i]/a[i][i] * a[i][j];
    }		
				
//
    
	for (i = n - 1; i >= 0; --i) {
        if (a[i][i]==0) return false;
		a[i][n] = a[i][n] / a[i][i];
		a[i][i] = 1;
		for (j = i - 1; j >= 0; --j) {
			a[j][n] -= a[j][i] * a[i][n];
			a[j][i] = 0;
		}
	}

//
	
	for (i = 0; i < n; ++i) {
			X[i]= a[i][n];
	}
	
	return true;
}


main ()
{
	int W,H,k;
	int i,j,y,x;
	double **tab;
	double **A, *X;
	int **poz;
	int liczba= 0, n, r;
	int licznik= 1;
	bool sprawdz=false;
    
for (;;)
{
		cin >> W >> H >> k;

		if (W==0 && H==0 && k==0) break;
        else if (sprawdz==true) 
        {
         cout << endl << endl;
        }
        sprawdz= true;
        
		tab= new double* [H];
		for (i = 0; i < H; i++) 
	    {
			tab[i] = new double[W];
        }

		for (i = 0; i < H; i++)
		{	for (j = 0; j < W; j++)
			{
				cin >> tab[i][j];
			}
		}

/////////////

	poz= new int* [H];
	for (i = 0; i < H; i++) 
	{
		poz[i] = new int[W];
    }

	for (i = 0; i < H; i++)
	{	for (j = 0; j < W; j++)
		{
			poz[i][j]= liczba;
			liczba++;
		}
	}
	liczba= 0;

	n= H* W;

	X= new double [n];

	A= new double* [n];
	for (i = 0; i < n; i++) 
	{
		A[i] = new double[n+1];
    }
	
	for (i = 0; i < n; i++)
	{	for (j = 0; j < n+1; j++)
		{
			A[i][j]= 0;
		}
	}


	for (y = 0; y < H; y++)
	{	for (x = 0; x < W; x++)
		{
			A[poz[y][x]][poz[y][x]]= 1;
			//
			i= y;
			r= k;

			while(r>0)
			{
				for (j= x-r; j< x; j++, i--)
					if (i>=0 && j>=0 && i<H && j<W) 
					{
						A[poz[y][x]][poz[i][j]]= 1;
						licznik++;
					}

				for (j= x; j< x+r; j++, i++)
					if (i>=0 && j>=0 && i<H && j<W) 
					{
						A[poz[y][x]][poz[i][j]]= 1;
						licznik++;
					}

				for (j= x+r; j> x; j--, i++)
				    if (i>=0 && j>=0 && i<H && j<W) 
					{
						A[poz[y][x]][poz[i][j]]= 1;
						licznik++;
					}

				for (j= x; j> x-r; j--, i--)
					if (i>=0 && j>=0 && i<H && j<W) 
					{
						A[poz[y][x]][poz[i][j]]= 1;
						licznik++;
					}
				r--;
			}
			//
			A[poz[y][x]][n]= tab[y][x]*licznik;
			licznik=1;
		}
	}

	//###################################

	if (gauss(n,A,X))
	{
		for (i = 0; i < H; i++)
		{	for (j = 0; j < W; j++)
			{
				cout.setf(ios::fixed);
                cout.precision(2);
				cout.width( 8 );
				cout << right << X[poz[i][j]] ;
			}
		        if (i+1<H)
				{
					cout << endl;
				}
		}	
	} 
    else
	{
        cout << 0;
    }

	for (i = 0; i < H; i++) 
	{
		delete [] tab[i];
		delete [] poz[i];
	}
        delete [] tab;
		delete [] poz;

	for (int i = 0; i < n; i++)
    {
         delete [] A[i];
    }
        delete [] A;

		delete [] X;
}

    return EXIT_SUCCESS;
}

