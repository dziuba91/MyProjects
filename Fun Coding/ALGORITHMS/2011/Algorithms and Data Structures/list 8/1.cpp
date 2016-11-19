#include <iostream>
#include <conio.h>

using namespace std;

struct LISTA
{
	int dana;
	LISTA *next;
} *poczatek;

void utworz_liste();
void wprowadz_element();
void dadaj_element_przed_zadanym();
void dadaj_element_po_zadanym();
void usun_element();
void znajdz_element();
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
		cout << "a - Utworz liste (punkt a)" <<endl;
		cout << "b - Dodac element (dodatkowe)" <<endl;
		cout << "c - Dodaj elemnt przed zadanym (punkt b)" << endl;
		cout << "d - Dodaj element po zadanym (punkt c)" << endl;
		cout << "e - Usun zadany element (punkt d)" << endl;
		cout << "f - Znajdz element (punkt e)" << endl;
		cout << "g - Ilosc elementow (punkt f)" << endl;
		cout << "w - Wyswietl liste" <<endl;
		cout << "q - EXIT" <<endl;

		cin >> sprawdz;

		if		(sprawdz=='a') utworz_liste();
		else if (sprawdz=='b') wprowadz_element();
		else if (sprawdz=='c') dadaj_element_przed_zadanym();
		else if (sprawdz=='d') dadaj_element_po_zadanym();
		else if (sprawdz=='e') usun_element();
		else if (sprawdz=='f') znajdz_element();
		else if (sprawdz=='g') ilosc_elementow();
		else if (sprawdz=='w') wyswietl();			   
	}
	
	cout << endl;
	system("PAUSE");
	return EXIT_SUCCESS;
}

void utworz_liste() 
{	
	LISTA *usun;
	while(poczatek) //usuwanie starej listy
	{
		usun= poczatek;
		poczatek= poczatek->next;
		delete usun;
	}

	int n;
	cout << "Podaj ilosc elementow listy= ";
	cin >> n;
	
	srand(time(0));
	for(int i= 0; i< n; i++)  //dodawanie elementow do nowej listy
	{
		LISTA *p= new LISTA; 
		p->dana= rand()%100+1;
		p->next= poczatek;
		poczatek= p;
	}
}

void wprowadz_element() //dodawanie elementu na poczatek listy
{
	LISTA *p= new LISTA;
	int liczba;

	cout << "Podaj liczbe= ";
	cin >> liczba;

	p->dana= liczba;
	p->next= poczatek;
	poczatek= p;
}

void dadaj_element_przed_zadanym()
{
	LISTA *p= new LISTA;
	LISTA *poprzedni;
	poprzedni= NULL;
	int x, liczba;
	cout << "Podaj element listy= ";
	cin >> x;

	for (LISTA *i= poczatek; i; i= i->next)
	{
		if(i->dana== x)
		{
			cout << "Podaj liczbe= ";
			cin >> liczba;
			if (poprzedni== NULL)  //gdy pierwszy element listy 
			{
				p->dana= liczba;
				p->next= poczatek;
				poczatek= p;
				break;
			}
			else
			{
				poprzedni->next= p;
				p->dana= liczba;
				p->next= i;
				break;
			}
		}

		if(i->next== NULL)
		{
			cout << "Podales elemnt ktorego nie ma na liscie!";
			getch();
		}
		poprzedni= i;
	}
}

void dadaj_element_po_zadanym()
{
	LISTA *p= new LISTA;
	LISTA *nastepny;
	int x, liczba;
	cout << "Podaj element listy= ";
	cin >> x;

	for (LISTA *i= poczatek; i; i= i->next)
	{
		nastepny= i->next;
		if(i->dana== x)
		{
			cout << "Podaj liczbe= ";
			cin >> liczba;
			i->next= p;
			p->dana= liczba;
			p->next= nastepny;
			break;
		}

		if(i->next== NULL)
		{
			cout << "Podales elemnt ktorego nie ma na liscie!";
			getch();
		}
	}
}

void usun_element()
{
	LISTA *poprzedni;
	LISTA *p; //zmienna pomocnicza;
	poprzedni= NULL;
	int x, sprawdz=0;
	cout << "Podaj element listy= ";
	cin >> x;

	for (LISTA *i= poczatek; i; )
	{
		if(i->dana== x)
		{
			if (poprzedni== NULL)	//gdy jestesmy na poczatku listy
			{
				p=i;
				i= i->next;
				poczatek= i;
				delete p;
				
				sprawdz=1;
			}
			else
			{
				p=i;
				i= i->next;
				poprzedni->next= i;
				delete p;
				
				sprawdz=1;
			}
		}
		else 
		{
			poprzedni= i;
			i= i->next;
		}
	}

	if(sprawdz== 0)
	{
		cout << "Podales elemnt ktorego nie ma na liscie!";
		getch();
	}
}

void znajdz_element()
{
	int x, licznik=0, sprawdz=0;
	cout << "Podaj element listy= ";
	cin >> x;

	cout << "W liscie element znajduje sie na pozycji: "<< endl;
	for (LISTA *i= poczatek; i; i= i->next)
	{
		licznik++;
		if(i->dana== x)
		{
			cout << licznik << endl;
			sprawdz=1;
		}
	}

	if (sprawdz==0)
		cout << "Brak danego elementu w liscie!" << endl;
		
	getch();
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