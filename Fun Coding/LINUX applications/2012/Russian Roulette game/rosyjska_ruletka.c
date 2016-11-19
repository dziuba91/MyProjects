/*
*	 #gcc  rosyjska_ruletka.c  -lncurses  -orosyjska_ruletka.o
*	 #./rosyjska_ruletka.o
*/

#include <stdio.h>
#include <errno.h>
#include <stdlib.h>
#include <string.h>
#include <sys/types.h>
#include <signal.h>
#include <ncurses.h>
#include <time.h>

char	*liczba="123";
char	zn;
char	zn2;
int		i=0;
int		pkt[6]={1000,2000,4000,10000,20000,40000};
int		licz=0;
char	tab[10][30];
char	*wyn1;
char	*wyn2;
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

//---------------------------------------------------------------------------------------------
 
void ekran()
{
	move(2,5);  printw("ROSYJSKA RULETKA");  
	move(4,3);  printw("1.\t GRAJ");
	move(5,3);  printw("2.\t Wyniki");
	move(6,3);  printw("3.\t Statystyki");
	move(7,3);  printw("4.\t Wyjdź");
}

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

int sprawdz_miejsce(int score)
{
	int j,w,g;	
	
	FILE *fp;
	fp= fopen("wyniki.txt","r");
	
	for (k=0;k<10;k++)
	{
		w=0;
		g=0;
		while ((a=getc(fp))!='\n')
		{
			if (w==2 && a!='\t') {
				X[g]=a; g++;		
			}

			if(a=='\t') w=2;	 
		}
		
		XI[k]= atoi(X);
		for(j=0;j<10;j++) X[j]='\0';
	}

	fclose(fp);

	for (k=11;k>=1;k--)
	{
		if (k==1) {return 1;}

		if(XI[k-2]>score)
		{
			return k;
		}
	}
}

void statystyka1(int x)
{	
	int j=0,w=0,z=0,g=0,y=0;

	FILE *fp;
	fp= fopen("statystyka1.txt","r");

	while((a=getc(fp))!=EOF)
	{	
		if (w==0)
		{
			tab[z][g]= a;
			g++;
			if (a=='\n') {z++; g=0;}
		}
		else
		{
			while ((a=getc(fp))!='\n')
			{
				if (a==EOF) break;

				if (a!='\t')
				{
					S[j]=a;
					j++;
				}
			}
			SI[y]=atoi(S);
			for(j=0;j<10;j++) S[j]='\0';
			
			y++;
			j=0;
			w=0;
			g=0;
			z++;
		}
		
		if (a=='>') w=1;
	}
	
	fclose(fp);

	SI[x]++;
	
	FILE *fs;
	fs= fopen("statystyka1.txt","w");
	
	j=0;
	for (k=0;k<6;k++)
	{
		if (k<4)
		{
			fprintf(fs,"%s",tab[k]);
		} 
		else
		{
			fprintf(fs,"%s\t\t%d\n",tab[k],SI[j]);
			j++;
		}
	}

	fclose(fs);
	
	for (k=0;k<10;k++)
	{
		for(j=0;j<30;j++) tab[k][j]='\0';
	}
}

void statystyka2(int x)
{	
	int j=0,w=0,z=0,g=0,y=0;

	FILE *fp;
	fp= fopen("statystyka2.txt","r");

	while((a=getc(fp))!=EOF)
	{	
		if (w==0)
		{
			stat[z][g]= a;
			g++;
		
			if (a=='\n') {z++; g=0;}
		}
		else
		{
			while ((a=getc(fp))!='\n')
			{
				if (a==EOF) break;

				if (a!='\t')
				{
					X[j]=a;
					j++;
				}
			}
			XI[y]=atoi(X);
			for(j=0;j<10;j++) X[j]='\0';
			
			y++;
			j=0;
			w=0;
			g=0;
			z++;
		}

		if (a=='>') w=1;
	}
	
	fclose(fp);

	if(x>=1 && x<=4) XI[0]++;
	if(x>=5 && x<=8) XI[1]++;
	if(x>=9 && x<=12) XI[2]++;
	if(x>=13 && x<=16) XI[3]++;
	if(x>=17 && x<=20) XI[4]++;
	if(x>=21 && x<=24) XI[5]++;
	if(x>=25 && x<=28) XI[6]++;
	if(x>=29 && x<=32) XI[7]++;
	if(x>=33 && x<=36) XI[8]++;
	if(x>=37 && x<=40) XI[9]++;
	
	FILE *fs;
	fs= fopen("statystyka2.txt","w");
	
	j=0;
	for (k=0;k<14;k++)
	{
		if (k<4)
		{
			fprintf(fs,"%s",stat[k]);
		} 
		else
		{
			fprintf(fs,"%s\t\t%d\n",stat[k],XI[j]);
			j++;
		}
	}

	fclose(fs);
	
	for (k=0;k<14;k++)
	{
		for(j=0;j<50;j++) stat[k][j]='\0';
	}
}

void zapis_wyniku(char *nick, int punkty)
{
	int j,w,g;	
	
	FILE *fp;
	fp= fopen("wyniki.txt","r");
	
	for (k=0;k<10;k++)
	{
		j=0;
		w=0;
		g=0;
		while ((a=getc(fp))!='\n')
		{
			if (a=='\t') w=0;

			if (w==1) {
				tab[k][j]=a;
				j++;
			}

			if (w==2 && a!='\t') {
				X[g]=a; g++;		
			}

			if(a==' ') w=1;
			if(a=='\t') w=2;	 
		}
		XI[k]= atoi(X);
		for(j=0;j<10;j++) X[j]='\0';
	}

	fclose(fp);
	
	for (k=9;k>=0;k--)
	{
		if(XI[k]<punkty)
		{
			zmienna= XI[k];
			for(j=0;j<30;j++) zmienna2[j]='\0'; 
			for(j=0;j<30;j++) zmienna2[j]= tab[k][j];
			
			XI[k]= punkty;
			for(j=0;j<30;j++) tab[k][j]='\0';
			for(j=0;j<30;j++) tab[k][j]= nick[j];

			if(k!=9)
			{
				XI[k+1]=zmienna;
				for(j=0;j<30;j++) tab[k+1][j]='\0';
				for(j=0;j<30;j++) tab[k+1][j]=zmienna2[j];
			}
		}
		else {break;}
	}

	FILE *fg;
	fg= fopen("wyniki.txt","w");
	for (k=0;k<10;k++)
	{
		fprintf(fg,"%d. %s\t\t%d\n",k+1,tab[k],XI[k]);
	}

	fclose(fg);
	
	for (k=0;k<10;k++)
	{
		for(j=0;j<30;j++) tab[k][j]='\0';
	}
}

void runda_40(int punkty, int runda)
{
	clear();
	int j=0;
	punkty= punkty+5000;

	move(3,3);	printw("GRATULUJEMY!!! PRZESZEDLES 40 RUNDE!!! -> +5000pkt nagrody");
	move(6,6);	printw("TWOJ WYNIK:");
	move(8,6);	printw("RUNDA: %d",runda);
	move(9,6);	printw("PUNKTY: %d",punkty);

	if (punkty>0)
	{
		if (sprawdz_miejsce(punkty)<=10)
		{
			move(10,6);	 printw("MIEJSCE: %d",sprawdz_miejsce(punkty));
			move(12,6);	 printw("PODAJ NICK: ");
			refresh();
			move(13,6);
			
			while ((a=getch())!='\n')
			{
				if (a==' ') break;
				nick[j]=a; j++;
			}
			refresh();
			zapis_wyniku (nick,punkty);
			statystyka1(1);
	
			for(j=0;j<30;j++) zmienna2[j]='\0';

			move(15,6); printw(".");
			refresh();
			sleep(1);
			move(15,7); printw(".");
			refresh();
			sleep(1);
			move(15,8); printw(".");
			refresh();
			sleep(1); 
			move(15,11); printw("Dokonano zapisu!!!");
			refresh();
			sleep(3);  
		}
		else
		{
			move(10,6);	 printw("MIEJSCE: po za pierwsza 10");
			move(12,6);	 printw("Twoj wynik nie kwalifikuje sie do zapisu!!!");
			move(18,4);	 printw("1. powrot");
			refresh();

			while (1) 
			{
				move (18,13);
				zn=getch();  

				if (zn=='1') {break;}
				else
				{
					move (18,13); printw("   ");
				}
			}
		}
	}
	else
	{
		move(12,6);	printw("NIEDOSTATECZNA LICZBA PUNKTOW!!!");
		move(18,4);	printw("1. powrot");
		refresh();
		
		while (1) 
		{
			move (18,13);
			zn=getch();  

			if (zn=='1') {break;}
			else
			{
				move (18,13); printw("   ");
			}
		}
	}

	clear();
	refresh();
	ekran();
}

//
void GRA()
{
	int strzal=0;
	int runda=1;
	int punkty=0;

	clear();
	refresh();
  
	losuj();
	
	move(2,5);  printw("ROSYJSKA RULETKA");
	move(4,1);  printw("RUNDA: %d",runda);
	move(5,1);  printw("STRZAL nr (pkt): %d (%d)	   ",strzal+1,pkt[strzal]);
	move(6,1);  printw("PUNKTY: %d	 ",punkty);

	move(9,3);  printw("strzal 1 -");
	move(10,3);  printw("strzal 2 -");
	move(11,3);  printw("strzal 3 -");
	move(12,3);  printw("strzal 4 -");
	move(13,3);  printw("strzal 5 -");
	move(14,3);  printw("strzal 6 -");

	move(18,4);  printw("1. strzal\t2. zakrec\t3. rezygnuj\t4. powrot");

	for(;;)
	{
		move(strzal+9,15);	printw("<-");
		zn2=getch();

		if (zn2=='1')
		{
			move(strzal+9,15); printw(".  ");
			refresh();
			sleep(1);
			move(strzal+9,16); printw(". ");
			refresh();
			sleep(1);
			move(strzal+9,17); printw(".");
			refresh();
			sleep(1);

			if(strzal==i)
			{
				move(strzal+9,15); printw("	BANG!!!");
				statystyka1(0);
				statystyka2(runda);
				refresh();
				sleep(3);
				
				punkty= punkty- pkt[strzal]*2;
				clear();
				refresh();
				ekran();
				break;
			}

			if(strzal!=i)
			{
				move(strzal+9,15);  printw("   +");
				punkty= punkty+ pkt[strzal];

				refresh();
				runda++;
				if (runda==41) {runda_40(punkty,runda-1); break;}

				strzal++;
				move(4,1);  printw("RUNDA: %d",runda);
				move(5,1);  printw("STRZAL nr (pkt): %d (%d)	   ",strzal+1,pkt[strzal]);
				move(6,1);  printw("PUNKTY: %d	 ",punkty);
			}
		}
		else if (zn2=='2')
		{
			losuj();
			punkty= punkty-1000;

			move(9,3);  printw("strzal 1 -      ");
			move(10,3);  printw("strzal 2 -      ");
			move(11,3);  printw("strzal 3 -      ");
			move(12,3);  printw("strzal 4 -      ");
			move(13,3);  printw("strzal 5 -      ");
			move(14,3);  printw("strzal 6 -      ");
	
			refresh();
			runda++;
	
			if (runda==41) {runda_40(punkty,runda-1); break;}

			strzal=0;

			move(4,1);	printw("RUNDA: %d",runda);
			move(5,1);	printw("STRZAL nr (pkt): %d (%d)     ",strzal+1,pkt[strzal]);
			move(6,1);	printw("PUNKTY: %d     ",punkty);
		}
		else if (zn2=='3')
		{
			clear();
			int j=0;

			punkty= punkty-5000;

			move(3,3);	printw("Zrezygnowales z dalszej gry -> -5000pkt kary");
			move(6,6);	printw("TWOJ WYNIK:");
			move(8,6);	printw("RUNDA: %d",runda);
			move(9,6);	printw("PUNKTY: %d",punkty);

			if (punkty>0)
			{
				if (sprawdz_miejsce(punkty)<=10)
				{
					move(10,6);	 printw("MIEJSCE: %d",sprawdz_miejsce(punkty));
					move(12,6);	 printw("PODAJ NICK: ");
					refresh();
					move(13,6);
					
					while ((a=getch())!='\n')
					{
						if (a==' ') break;
						nick[j]=a; j++;
					}
					refresh();
					zapis_wyniku (nick,punkty);
					statystyka1(1);
	
					for(j=0;j<30;j++) zmienna2[j]='\0';

					move(15,6); printw(".");
					refresh();
					sleep(1);
					move(15,7); printw(".");
					refresh();
					sleep(1);
					move(15,8); printw(".");
					refresh();
					sleep(1); 
					move(15,11); printw("Dokonano zapisu!!!");
					refresh();
					sleep(3);  
				}
				else
				{
					move(10,6);	 printw("MIEJSCE: po za pierwsza 10");
					move(12,6);	 printw("Twoj wynik nie kwalifikuje sie do zapisu!!!");	
					move(18,4);	 printw("1. powrot");
					refresh();

					while (1) 
					{
						move (18,13);
						zn=getch();  

						if (zn=='1') {break;}
						else
						{
							move (18,13); printw("   ");
						}
					}
				}
			}
			else
			{
				move(12,6);	printw("NIEDOSTATECZNA LICZBA PUNKTOW!!!");
				move(18,4);	printw("1. powrot");
				refresh();
				
				while (1) 
				{
					move (18,13);
					zn=getch();  

					if (zn=='1') {break;}
					else
					{
						move (18,13); printw("   ");
					}
				}
			}
			
			clear();
			refresh();
			ekran();
			break;
		}
		else if (zn2=='4')
		{
			clear();
			refresh();
			ekran();
			break;
		}
		else
		{
			move(strzal+9,15); printw("     ");
		}
	}
}

//-----------------------------------------------------------------------------

void koniec()
{
	curs_set(1);
	clear();
	refresh();
	endwin();
	exit(0); 
}

void wyniki()
{
	clear();
	refresh();
	
	int j;	

	FILE *fp;
	fp= fopen("wyniki.txt","r");
	
	for (k=0;k<10;k++)
	{
		j=0;
		while ((a=getc(fp))!='\n')
		{
			tab[k][j]=a;
			j++;
		}
	}

	fclose(fp);

	for (k=0;k<10;k++)
	{
		move(k+4,3);  printw("%s",tab[k]);
	}
	
	for (k=0;k<10;k++)
	{
		for(j=0;j<30;j++) tab[k][j]='\0';
	}

	//
	move(18,4);	 printw("1. powrot");

	for(;;)
	{
		move (18,13);
		zn2=getch();
	 
		if (zn2=='1')
		{
			clear();
			refresh();
			ekran();
			break;
		}
		else
		{
			move (18,13); printw("	 ");
		}
	}
} 

void statystyki()
{
	clear();
	refresh();
	
	int j;	

	FILE *fp;
	fp= fopen("statystyka1.txt","r");
	
	for (k=0;k<6;k++)
	{
		j=0;
		while ((a=getc(fp))!='\n')
		{
			tab[k][j]=a;
			j++;
		}
	}

	fclose(fp);

	for (k=0;k<6;k++)
	{
		move(k,3);	printw("%s",tab[k]);
	}
	
	for (k=0;k<6;k++)
	{
		for(j=0;j<30;j++) tab[k][j]='\0';
	}

	//
	FILE *fs;
	fs= fopen("statystyka2.txt","r");
	
	for (k=0;k<14;k++)
	{
		j=0;
		while ((a=getc(fs))!='\n')
		{
			stat[k][j]=a;
			j++;
		}
	}

	fclose(fs);

	move(7,0);	printw("##################################################################");
	
	for (k=0;k<14;k++)
	{
		move(k+8,3);  printw("%s",stat[k]);
	}
	
	for (k=0;k<14;k++)
	{
		for(j=0;j<50;j++) stat[k][j]='\0';
	}

	//
	refresh();
	move(23,4);	 printw("1. powrot");
	
	for(;;)
	{
		move(23,13);
		zn2=getch();
	 
		if (zn2=='1')
		{
			clear();
			refresh();
			ekran();
			break;
		}
		else
		{
			move (23,13); printw("	 ");
		}
	}
} 

// =================================================
int main(int agrc, char **argv)
{  
	initscr();
	curs_set(0);
	clear();
	refresh();

	ekran();


	while (1) 
	{
		move (8,0);
		zn=getch();	

		if (zn=='1') {GRA();}
		else if (zn=='2') {wyniki();}
		else if (zn=='3') {statystyki();}
		else if (zn=='4') {break;}
		else
		{
			move (8,0); printw("   ");
		}
	}

	refresh();   

	koniec();
	return (0); 
}
