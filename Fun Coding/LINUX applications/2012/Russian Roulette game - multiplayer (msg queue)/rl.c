/*
*	 #gcc  rosyjska_ruletka.c  -lncurses  -orosyjska_ruletka.o
*	 #./rosyjska_ruletka.o
*/

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/msg.h>
#include <ncurses.h>
#include <time.h>

#define PERM 0666	  // prawa dostepu
#define KLUCZ_KOM(key_t) 1231
#define KLUCZ_KOM2(key_t) 1232

//---- struktura dla komunikatow ----		  
struct m_komunikat {
	long typ;
	int cos;
	char zn;
};

char	*liczba="123";
char	zn;
char	zn2;
int		i=0;
int		pkt[6]={1000,2000,4000,10000,20000,40000};
char	tab[10][30];
char	a;
int		k;
char	X[10];
int		XI[10];
char	zmienna2[30];
int		zmienna;
char	wyn[10][30];
char	nick[30];
char	S[10];
int		SI[2];
int		proc[10];
char	stat[14][50];
int		urzytkownik;
int		kom, kon, kos;
int		pauza=0;
int		kolejka;
char	plusik='+';
char	gwiazdka='o';
char	*strzaleczka="->";
char	*minusiki="--";
bool	wyjdz=0;

struct m_komunikat msg;

//--------------------------------------------------------------------------------------

void losuj()
{
	srand(time(0));
	i= rand()%6;
	
	FILE *fp; 
	fp= fopen("liczba.txt","w");
	fprintf(fp,"%d",i);
	fclose(fp);
}

void koniec_gry(int gracz,int punkty) //zmienna gracz określa zawodnika który wygrał
{
	clear();
	refresh();
	  
	if(urzytkownik== gracz) 
	{	  
		move(8,6);  printw("KONIEC ROZGRYWKI");
		if (gracz==1) { move(9,6);  printw("GRACZ: 2    -> UMARL!"); }
		if (gracz==2) { move(9,6);  printw("GRACZ: 1    -> UMARL!"); }
	}

	if(urzytkownik!= gracz) 
	{
		move(8,20);  printw("BANG!!!");
		move(9,6);  printw("-> UMARLES");
	}

	move(12,20);  printw("WYGRYWA GRACZ: %d",gracz);

	if(urzytkownik== gracz) 
	{
		move(13,20);  printw("GRATULUJEMY WYGRANEJ!"); 
		move(15,20);  printw("Twoje osiagniecie punktowe: %d", punkty); 
	}
	
	if(urzytkownik!= gracz) { move(13,20);  printw("NIESTETY PRZEGRALES!"); }

	move(20,40);  printw("1. wyjdz");
	refresh();

	for(;;)
	{
		move(23,13);
		zn2=getch();
	 
		if (zn2=='1')
		{
			break;
		}
		else
		{
			move (20,49); printw("	 ");
		}
	}
}

void runda_40(int punkty, int punkty2)
{
	clear();
	refresh();

	move(3,3);  printw("OSIAGNIETA ZOSTAŁA 40 RUNDA!!!");
	move(6,6);  printw("WYNIKI:");
	move(8,6);  printw("GRACZ 1: %d",punkty);
	move(9,6);  printw("GRACZ 2: %d",punkty2);
	
	if(punkty>punkty2) 
	{
		move(12,20);  printw("WYGRYWA GRACZ: 1");
		if(urzytkownik==1) { move(13,20);  printw("GRATULUJEMY WYGRANEJ!"); }
		if(urzytkownik==2) { move(13,20);  printw("PRZEGRALES!"); }
	}

	if(punkty<punkty2) 
	{
		move(12,20);  printw("WYGRYWA GRACZ: 2");
		if(urzytkownik==1) { move(13,20);  printw("PRZEGRALES!");}
		if(urzytkownik==2) { move(13,20);  printw("GRATULUJEMY WYGRANEJ!"); }
	}

	if(punkty==punkty2) move(12,20);  printw("REMIS!");

	move(20,40);	printw("1. wyjdz");
	refresh();

	for(;;)
	{
		move(23,13);
		zn2=getch();
	 
		if (zn2=='1')
		{
			break;
		}
		else
		{
			move (20,49); printw("   ");
		}
	}
}

void brak_przeciwnika()
{
	clear();
	refresh();
	move(12,15); printw("Przeciwnik oposcił gre!");
	move(13,15); printw("Brak przeciwnika!");
	
	move(15,15); printw("Za chwile gra sie wylaczy...");
	refresh();

	sleep(3);	 
}

//
void GRA()
{
	int strzal=0;
	int runda=1;
	int punkty=0;
	int punkty2=0;

	//(pierwsze losowanie)
	if (urzytkownik==1) 
	{
		losuj();
		kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
		msg.typ=5; //losowanie
		msg.cos=i;
		msgsnd(kos, &msg, sizeof(msg),0);
	}

	if (urzytkownik==2)
	{
		msg.typ=0;
		kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
		kon=1;
		while(kon)
		{
			msgrcv(kos,&msg,sizeof(msg),0 ,0);

			switch(msg.typ) {
				case 5:
					i= msg.cos;
					kon= 0;
				break;
			 
				default:
					break;
			}	
		}
	}
	
	//
	move(2,5);   printw("ROSYJSKA RULETKA");
	move(4,20);  printw("RUNDA: %d",runda);
	move(5,20);  printw("STRZAL nr (pkt): %d (%d)     ",strzal+1,pkt[strzal]);
	move(6,20);  printw("KOLEJKA GRACZA nr: %d",kolejka);
	move(7,1);   printw("GRACZ nr. 1");
	move(8,1);   printw("PUNKTY: %d	 ",punkty);
	move(7,52);  printw("GRACZ nr. 2");
	move(8,52);  printw("PUNKTY: %d	  ",punkty2);
  
	move(11,3);  printw("strzal 1 -");
	move(12,3);  printw("strzal 2 -");
	move(13,3);  printw("strzal 3 -");
	move(14,3);  printw("strzal 4 -");
	move(15,3);  printw("strzal 5 -");
	move(16,3);  printw("strzal 6 -");

	move(20,4);  printw("1. strzal\t2. zakrec\t3. rezygnuj\t4. wyjdz");

	for(;;)
	{
		if (urzytkownik==kolejka) { move(strzal+11,15);	 printw("%s",strzaleczka); }
		if (urzytkownik!=kolejka) { move(strzal+11,15);	 printw("%s",minusiki); }

		refresh();

		if (urzytkownik==kolejka)
		{
			zn2=getch();	
			msg.typ=7; //wysyłanie wiadomości znakowej
			msg.zn=zn2;

			if (urzytkownik==1) kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
			if (urzytkownik==2) kos=msgget(KLUCZ_KOM2, PERM | IPC_CREAT);
	  
			msgsnd(kos, &msg, sizeof(msg),0);
		}
	
		if (urzytkownik!=kolejka)
		{
			msg.typ=0;
			if (urzytkownik==1) kos=msgget(KLUCZ_KOM2, PERM | IPC_CREAT);
			if (urzytkownik==2) kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
			
			kon=1;
			while(kon)
			{
				msgrcv(kos,&msg,sizeof(msg),0 ,0);

				switch(msg.typ) {
					case 7:
						zn2= msg.zn;
						kon= 0;
					break;
		  
					case 9:
						kon= 0;
						wyjdz= 1;
					break;
			 
					default:
						break;
				}	
			}
		}

		if (wyjdz==1) { brak_przeciwnika(); break; }

		if (zn2=='1')
		{
			move(strzal+11,15); printw(".  ");
			refresh();
			sleep(1);
			move(strzal+11,16); printw(". ");
			refresh();
			sleep(1);
			move(strzal+11,17); printw(".");
			refresh();
			sleep(1);

			if(strzal==i)
			{
				move(strzal+11,15); printw("   BANG!!!");
				refresh();
				sleep(3);
				
				if (kolejka==1) koniec_gry(2,punkty2);
				if (kolejka==2) koniec_gry(1,punkty);
				break;
			}

			if(strzal!=i)
			{
				if (urzytkownik== kolejka)
				{
					move(strzal+11,15);	 printw("   %c",plusik);
				}
				else
				{
					move(strzal+11,15);	 printw("   %c",gwiazdka);
				}
	  
				if (kolejka == 1) punkty= punkty+ pkt[strzal];
				if (kolejka == 2) punkty2= punkty2+ pkt[strzal];

				refresh();
				runda++;

				if (runda==41) {runda_40(punkty,punkty2); break;}

				strzal++;
				if (kolejka==1) kolejka=2;
				else kolejka=1;

				move(4,20);  printw("RUNDA: %d",runda);
				move(5,20);  printw("STRZAL nr (pkt): %d (%d)     ",strzal+1,pkt[strzal]);
				move(6,20);  printw("KOLEJKA GRACZA nr: %d",kolejka);
				move(8,1);  printw("PUNKTY: %d     ",punkty);
				move(8,52);  printw("PUNKTY: %d     ",punkty2);
			}
		}
		else if (zn2=='2')
		{
			if (kolejka==urzytkownik) 
			{
				losuj();
				if (urzytkownik==1) kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
				if (urzytkownik==2) kos=msgget(KLUCZ_KOM2, PERM | IPC_CREAT);
				msg.typ=5; //losowanie
				msg.cos=i;
				msgsnd(kos, &msg, sizeof(msg),0);
			}

			if (kolejka!=urzytkownik)
			{
				msg.typ=0;
				if (urzytkownik==1) kos=msgget(KLUCZ_KOM2, PERM | IPC_CREAT);
				if (urzytkownik==2) kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
				
				kon=1;
				while(kon)
				{
					msgrcv(kos,&msg,sizeof(msg),0 ,0);

					switch(msg.typ) {
						case 5:
							i= msg.cos;
							kon= 0;
						break;
			 
						default:
							break;
					}	
				}
			}

			if (kolejka==1) punkty= punkty-1000;
			if (kolejka==2) punkty2= punkty2-1000;

			move(11,3);  printw("strzal 1 -      ");
			move(12,3);  printw("strzal 2 -      ");
			move(13,3);  printw("strzal 3 -      ");
			move(14,3);  printw("strzal 4 -      ");
			move(15,3);  printw("strzal 5 -      ");
			move(16,3);  printw("strzal 6 -      ");
	
			refresh();
			runda++;
	
			if (kolejka==1) kolejka=2;
			else kolejka=1;
	
			if (runda==41) {runda_40(punkty,punkty2); break;}

			strzal=0;

			move(4,20);  printw("RUNDA: %d",runda);
			move(5,20);  printw("STRZAL nr (pkt): %d (%d)     ",strzal+1,pkt[strzal]);
			move(6,20);  printw("KOLEJKA GRACZA nr: %d",kolejka);
			move(8,1);  printw("PUNKTY: %d     ",punkty);
			move(8,52);  printw("PUNKTY: %d     ",punkty2);		
		}
		else if (zn2=='3')
		{
			if (kolejka==1) punkty= punkty-5000;
			if (kolejka==2) punkty2= punkty2-5000;

			refresh();
			runda++;
	
			if (kolejka==1) kolejka=2;
			else kolejka=1;
	
			if (runda==41) {runda_40(punkty,punkty2); break;}

			move(4,20);  printw("RUNDA: %d",runda);
			move(5,20);  printw("STRZAL nr (pkt): %d (%d)     ",strzal+1,pkt[strzal]);
			move(6,20);  printw("KOLEJKA GRACZA nr: %d",kolejka);
			move(8,1);  printw("PUNKTY: %d     ",punkty);
			move(8,52);  printw("PUNKTY: %d     ",punkty2);
		}
		else if (zn2=='4')
		{
			if (urzytkownik==1) kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
			if (urzytkownik==2) kos=msgget(KLUCZ_KOM2, PERM | IPC_CREAT);
			msg.typ=9; //sygnał wyłączenia
			msgsnd(kos, &msg, sizeof(msg),0);
			break;
		}
		else
		{
			move(strzal+9,15); printw("     ");
		}
	}
}

//-----------------------------------------------------------------------------

 void koniec(){
	curs_set(1);
	clear();
	refresh();
	endwin();
	exit(0); 
}


// =================================================

int main(int agrc, char **argv) {
  
	urzytkownik=atoi(argv[1]);

	if (urzytkownik!=1 && urzytkownik!=2) 
	{
		printf ("Niepoprawne parametry uruchomienia programu: możesz 	jedynie urochomić program jako urzytkownik 1 lub 2\n");
		sleep (3);
		exit(1);
	}
	
	printf ("Zostałeś zalogowany jako urzytkownik:%d\n",urzytkownik);


	if (urzytkownik==1)
	{
		kom=msgget(KLUCZ_KOM, 0);
		printf ("Oczekiwanie na drugiego gracza...\n");
	
		msg.typ=0;
		kos=msgget(KLUCZ_KOM2, PERM | IPC_CREAT);
		
		kon=1;
		while(kon)
		{
			msgrcv(kos,&msg,sizeof(msg),0 ,0);

			switch(msg.typ) {
				case 3:
					printf ("Gracz nr. 2 został zalogowany.\n\n");
					kon= 0;
				break;
			 
				default:
					break;
			}	
		}

		kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
		msg.typ=3;
		msgsnd(kos, &msg, sizeof(msg),0);
	}

	if (urzytkownik==2)
	{
		kom=msgget(KLUCZ_KOM2, 0);
		printf ("Oczekiwanie na drugiego gracza...\n");

		kos=msgget(KLUCZ_KOM2, PERM | IPC_CREAT);
		msg.typ=3;
		msgsnd(kos, &msg, sizeof(msg),0);

		msg.typ=0;
		kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
		
		kon=1;
		while(kon)
		{
			msgrcv(kos,&msg,sizeof(msg),0 ,0);

			switch(msg.typ) {
				case 3:
					printf ("Gracz nr. 1 został zalogowany.\n\n");
					kon= 0;
				break;
			 
				default:
					break;
			}	
		}	
	}

	if (urzytkownik==1) //ustalanie kolejki
	{
		srand(time(0));
		kolejka= rand()%2+1;
  
		kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
		msg.typ=4; //ustala kolejność
		msg.cos=kolejka;
		msgsnd(kos, &msg, sizeof(msg),0);
	}

	if (urzytkownik==2) //ustalanie kolejki
	{
		msg.typ=0;
		kos=msgget(KLUCZ_KOM, PERM | IPC_CREAT);
		
		kon=1;
		while(kon)
		{
			msgrcv(kos,&msg,sizeof(msg),0 ,0);

			switch(msg.typ)
			{
				case 4:
					kon= 0;
					kolejka=msg.cos;
				break;
			 
				default:
					break;
			}	
		}
	}

	printf ("Zaczyna gracz numer: %d\n",kolejka);
	printf ("Za chwile rozpocznie się rozgrywka...\n");
	sleep(3);

	initscr();
	curs_set(0);
	clear();

	GRA();

	//  
	if (urzytkownik==1) { kom=msgget(KLUCZ_KOM, PERM | IPC_CREAT); }
	if (urzytkownik==2) { kom=msgget(KLUCZ_KOM2, PERM | IPC_CREAT); }

	msgctl(kom, IPC_RMID, 0);

	koniec();
	
	return 0; 
}
