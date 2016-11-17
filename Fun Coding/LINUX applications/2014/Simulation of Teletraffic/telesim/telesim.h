#ifndef __TELESIM_H
#define __TELESIM_H
#define TIMEROW 1	// first row show time and other info
#define TELE_START_ROW 2	// starting row to display phones
#define TELE_PER_ROW 20		// number of phones in one row
#define PB 3 	// phone block - size o1 a phone block
#define TELE_SIZE 100	// total number of phones
#define TRUNK_SIZE 10	// total number of trunks
#define SIMU_TIME 3600	// total simulate time (in second)
#define CALL_PROB 0.001		// probability to make a call
enum TELE_STATE {IDLE, BUSY};	// state enumeration

typedef struct{
	int ID;
	int state;
	int remaind_time;
}telephone;	// telephone structure definition

typedef struct{
	int ID;
	int state;
	int tele_ID;
}trunk;		// trunk structure definition

// function prototype
void initPhones(telephone []);
void initTrunks(trunk []);
int getTrunk(trunk [], int);
void releaseTrunk(trunk [], int);
void drawPhones(void);
void simulate(telephone *, trunk *);
int updatePhones(telephone []);
void trunkState(telephone *, trunk *);
void showInfo(int, int, int, int);
#endif
