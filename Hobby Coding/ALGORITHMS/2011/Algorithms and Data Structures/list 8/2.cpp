#include <iostream>

using namespace std;

struct LISTA
{
	int dana;
	LISTA *next;
} *poczatek;

void utworz_liste(int n);
void wyswietl();

int main ()
{
	int n, k, licznik=1;
	poczatek=NULL;
	
	cout << "Podaj ilosc elementow listy= ";
	cin >> n;
	
	utworz_liste(n);
	wyswietl();

	LISTA *poprzedni;
	for (LISTA *i= poczatek; ;i= i->next)
	{
		if(i->next== NULL)
		{
			poprzedni= i; //do szukania elementu dla k=1
			i->next= poczatek;
			break;
		}
	}

	cout << endl << "Podaj k= ";
	cin >> k;

	LISTA *p; //pomocnicza zmienna

	for (LISTA *i= poczatek; ;)
	{
		if (i==i->next)
		{
			cout << "Pozosta³y element:" << endl;
			cout << i->dana << endl;
			break;
		}

		if (licznik==k)
		{
			cout << i->dana << endl;
			p=i;
			i= i->next;
			poprzedni->next= i;
			delete p;

			licznik= 1;
		}
		else 
		{
			licznik++;
			poprzedni= i;
			i= i->next;
		}
	}

	cout << endl;
	system("PAUSE");
	return EXIT_SUCCESS;
}

void utworz_liste(int n) //dodawanie elementu na poczatek listy
{	
	srand(time(0));
	for(int i= 0; i< n; i++)
	{
		LISTA *p= new LISTA;
		p->dana= rand()%100+1;
		p->next= poczatek;
		poczatek= p;
	}
}

void wyswietl()
{
	cout << "Elementy listy: " << endl;

	for (LISTA *i= poczatek; i; i= i->next)
		cout << i->dana << " ";
}
