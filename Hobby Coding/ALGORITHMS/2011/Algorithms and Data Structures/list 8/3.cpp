#include <iostream>
#include <conio.h>

using namespace std;

struct LISTA
{
	int dana;
	LISTA *next;
} *poczatek, *koniec;

bool czy_pusta();
void wprowadz_element();
void usun_element();
void znajdz_element();
void ilosc_elementow();
void wyswietl();

int main ()
{
	char sprawdz = ' ';
	poczatek= NULL;
	koniec= NULL;

	while(sprawdz!='q')
	{
		system("cls");
		cout << "WCISNIJ:" << endl;
		cout << "a - Dodac element na koniec" <<endl;
		cout << "b - Usun pierwszy element(punkt b)" << endl;
		cout << "c - Ilosc elementow (punkt c)" << endl;
		cout << "d - Czy kolejka pusta? (punkt d)" << endl;
		cout << "e - Znajdz element (punkt e)" << endl;
		cout << "w - Wyswietl kolejke" <<endl;
		cout << "q - EXIT" <<endl;

		cin >> sprawdz;

		if      (sprawdz=='a') wprowadz_element();
		else if (sprawdz=='b') usun_element();
		else if (sprawdz=='c') ilosc_elementow();
		else if (sprawdz=='d') {
									if (czy_pusta())
										cout << "TAK";
									else 
										cout << "NIE";
                                      
									getch();
                               }
		else if (sprawdz=='e') znajdz_element();
		else if (sprawdz=='w') wyswietl();
    }
    
	cout << endl;
	system("PAUSE");
	return EXIT_SUCCESS;
}

bool czy_pusta()
{
	if (poczatek==NULL)
		return 1;
	else 
		return 0;
}

void wprowadz_element() //dodawanie elementu na poczatek listy
{
	LISTA *p= new LISTA;
	int liczba;

	cout << "Podaj liczbe= ";
	cin >> liczba;

	p->dana= liczba;
	p->next= NULL;

	if (czy_pusta()) //gdy pusta
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

void usun_element()
{
	if (czy_pusta()==0)
	{
		LISTA *p;
		p= poczatek;
		poczatek= poczatek->next;
		delete p;
	}
}

void znajdz_element()
{
	int x;
	cout << "Podaj element kolejki= ";
	cin >> x;
	
	for (LISTA *i= poczatek; i; i= i->next)
	{
		if(i->dana== x)
		{
			cout << "W kolejce istnieje podany element!" << endl;
			break;
		}

		if (i->next==NULL)
			cout << "Brak danego elementu w kolce!" << endl;
	}

	getch();
}

void ilosc_elementow()
{
	int licznik=0;

	cout << "Kolejka zawiera: " << endl;

	for (LISTA *i= poczatek; i; i= i->next)
		licznik++;

	cout << licznik << " elementow" << endl;

	getch();
}

void wyswietl()
{
	cout << "Elementy kolejki: " << endl;

	for (LISTA *i= poczatek; i; i= i->next)
		cout << i->dana << " ";

	getch();
}
