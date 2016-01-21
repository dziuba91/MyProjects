#include <iostream>
#include <conio.h>

using namespace std;

struct LISTA
{
	int dana;
	LISTA *next;
} *poczatek;

void utworz_kolejke_priorytetowa();
void wprowadz_element();
void usun_element();
void ilosc_elementow();
void wyswietl();

int main ()
{
    char sprawdz = ' ';
    poczatek= NULL;

    while(sprawdz!='q')
    {
        system("cls");
	    cout << "WCISNIJ:" << endl;
	    cout << "a - Utworz kolejke priorytetow (punkt a)" <<endl;
	    cout << "b - Dodac element (punkt b)" <<endl;
	    cout << "c - Usun element minimalny (punkt c)" << endl;
	    cout << "d - Ilosc elementow (punkt d)" << endl;
        cout << "w - Wyswietl liste" <<endl;
	    cout << "q - EXIT" <<endl;

        cin >> sprawdz;

	    if      (sprawdz=='a') utworz_kolejke_priorytetowa();
        else if (sprawdz=='b') wprowadz_element();
	    else if (sprawdz=='c') usun_element();
	    else if (sprawdz=='d') ilosc_elementow();
	    else if (sprawdz=='w') wyswietl();              
    }
    
	cout << endl;
    system("PAUSE");
    return EXIT_SUCCESS;
}

void utworz_kolejke_priorytetowa() 
{	
	LISTA *usun;
	while(poczatek) //usuwanie starej kolejki
	{
		usun= poczatek;
		poczatek= poczatek->next;
		delete usun;
	}

	int n;
	cout << "Podaj ilosc elementow kolejki= ";
	cin >> n;
	
	srand(time(0));
	LISTA* poprzedni;

	for(int i= 0; i< n; i++)  //dodawanie elementow do nowej listy
	{
		LISTA *p= new LISTA; 
		p->dana= rand()%100+1;
		p->next= NULL;

		if (poczatek==NULL) // gdy kolejka pusta
		{
			poczatek= p;
		}
		else
		{
			for (LISTA *i= poczatek; i; i=i->next)
			{
				if(p->dana< i->dana) 
				{
					p->next= i;
					
                    if(i== poczatek)  //ustawiamy informacje o elemencie do poprzednika
						poczatek= p;
					else
						poprzedni->next= p;

					break;
				}

				if(i->next== NULL) //przypadek gdzy najwiekszy wedruje na koniec odrazu
				{
					i->next= p;
					break;
				}

				poprzedni= i;
			}			
		}
	}
}

void wprowadz_element() //dodawanie elementu na poczatek listy
{
	LISTA *p= new LISTA;
	int liczba;

	cout << "Podaj liczbe= ";
	cin >> liczba;

	p->dana= liczba;
	p->next= NULL;
	LISTA* poprzedni;

	if (poczatek==NULL)
	{
	    poczatek= p;
	}
	else
	{
		for (LISTA *i= poczatek; i; i=i->next)
		{
			if(p->dana< i->dana) 
			{
				p->next= i;
				if(i== poczatek)  //ustawiamy informacje o elemencie do poprzednika
                    poczatek= p;
				else
					poprzedni->next= p;
					
				break;
			}

			if(i->next== NULL) //przypadek gdzy najwiekszy wedruje na koniec odrazu
			{
				i->next= p;
				break;
			}

			poprzedni= i;
		}			
	}
}

void usun_element()
{
	LISTA *p; //zmienna pomocnicza;
	int usun; //wartosc do usuniecia
	usun= poczatek->dana;

	for (LISTA *i= poczatek; i;)
	{
		if (i->dana== usun)
		{
			p=i;
			i= i->next;
			poczatek= i;
			delete p;	
		}
		else break;
	}
}

void ilosc_elementow()
{
	int licznik=0;

	cout << "Lista zawiera: " << endl;

	for (LISTA *i= poczatek; i; i= i->next)
		licznik++;

	cout << licznik << " elementow" << endl;

	getch();
}

void wyswietl()
{
	cout << "Elementy listy: " << endl;

	for (LISTA *i= poczatek; i; i= i->next)
		cout << i->dana << " ";

	getch();
}
