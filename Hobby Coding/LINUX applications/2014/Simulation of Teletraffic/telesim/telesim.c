#include <stdio.h>
#include <stdlib.h>
#include "telesim.h"
#include "screen.h"
#include <unistd.h>
#include <time.h>

/*
	function initPhones initialize members of telephone struct array
	to starting values (e.g. mark all telephone to IDLE).
	arguments:
		telephone[] t: array of telephone struct
	return: none;
*/
void initPhones(telephone t[]){
	int i;
	for(i=0; i<TELE_SIZE; ++i){
		t[i].ID = i+1;		// ID from 1 to TELE_SIZE
		t[i].state = IDLE;	// state is IDLE
		t[i].remaind_time = 0;	// remaining time is 0
	} // for
} // function

/*
	function initTrunks initialize members of trunk struct array
	to starting values (e.g. mark all trunk the IDLE).
	arguments:
		trunk[] tk: array of trunk struct
	return: none;
*/
void initTrunks(trunk tk[])
{
	int i;
	for(i=0; i<TRUNK_SIZE; ++i){
		tk[i].ID = i+1;		// ID from 1 to TRUNK_SIZE
		tk[i].state = IDLE;	// state is IDLE
		tk[i].tele_ID = 0;	// set ID of telephone to default (0)
	} // for
} // function

/*
	function drawPhones draw basic simulation screen - display all frames
	in white color.
	arguments: none
	return: none
*/
void drawPhones(void){
	int i, j;	// loop counters
	clearScreen();	// clear the screen
	for(i=0; i<TELE_SIZE/TELE_PER_ROW; ++i){	// for 5 rows
		for(j=0; j<TELE_PER_ROW; ++j){		// TELE_PER_ROW (20) colums
			drawFrame(1+j*PB, TELE_START_ROW+PB*i, PB, PB, WHITE);	// draw white frame
		} // for(j)
	} // for(i)
} // function

/*
	function updatePhones draw and update new simulation screen based on
	telephone struct member state value - if state is BUSY draw red frame,
	in another case set frame to white color.
	arguments:
		telephone[] t: array of telephone struct
	return:
		int: number of telephone with state is BUSY
*/
int updatePhones(telephone t[])
{
	int i, j, busy_phones=0;	// busy_phones count how many
	for (i=0; i<TELE_SIZE/TELE_PER_ROW; ++i){
		for (j=0; j<TELE_PER_ROW; ++j){
			if(t[j+i*TELE_PER_ROW].state == BUSY){
				drawFrame(1+j*PB, TELE_START_ROW+PB*i, PB, PB, RED);	// draw frame in red color
				busy_phones++;	// BUSY number increments
			} // if
			else{
				drawFrame(1+j*PB, TELE_START_ROW+PB*i, PB, PB, WHITE);	// draw frame in white color
			} // else
		} // for(j)
	} // for(i)
	return busy_phones;	// return number of BUSY phones
} // function

/*
	function getTrunk find trunk with IDLE state and reserve it (set
	state to BUSY) for current telephone.
	arguments:
		trunk[] tk: array of trunk struct
		int tid: telephone ID to reserve trunk for current telephone
	return:
		int: ID of found trunk (if not found return 0)
*/
int getTrunk(trunk tk[], int tid){
	int i;
	for (i=0; i<TRUNK_SIZE; ++i){
		if (tk[i].state == IDLE){	// we got a free trunk
			tk[i].state = BUSY;	// set trunk to BUSY
			tk[i].tele_ID = tid;	// set struct member tele_ID for current telephone ID

			return tk[i].ID;	// trunk ID (>0) is returned
		}  // if
	}  // for

	return 0; 	// return 0 if don't got any free trunk
}  // function

/*
	function releaseTrunk set trunk back to IDLE (which was in use for
	telephone ID (tid)) - current trunk after operation is free again.
	arguments:
		trunk[] tk: array of trunk struct
		int tid: telephone ID to release trunk which was in use for current telephone
	return: none;
*/
void releaseTrunk(trunk tk[], int tid){
	int i;
	for(i=0; i<TRUNK_SIZE; ++i){	// check all trunk array
		if(tk[i].tele_ID == tid){	// we found trunk which used current telephone
			tk[i].tele_ID = 0;	// release ID of telephone back to default (0)
			tk[i].state = IDLE;	// set state back to IDLE
		} // if
	} // for
} // function

/*
	function simulate control all of this simulation in run - control
	time, decide when call set to BUSY, control trunks and respond of
	every struct (telephon, trunks) members values.
	arguments:
		telephone[] t: array of telephone struct
		trunk[] tk: array of trunk struct
	return: none;
*/
void simulate(telephone t[], trunk tk[]){
	int i, j, blocked = 0, succeeded=0; // loop counters
	double p;
	srand(time(NULL));	// pleas randomize the rand function here
	for (i=0; i<SIMU_TIME; ++i){	// for one hour of simulate
		for (j=0; j<TELE_SIZE; ++j){	// for each phone
			if(t[j].state == IDLE){
				p = (double)rand()/RAND_MAX;	//generate a random number
				if (p<CALL_PROB){	// a call is generated
					if(getTrunk(tk, t[j].ID) >0){	// we have got a free trunk
						t[j].state = BUSY;	// set call to busy
						t[j].remaind_time = 30 + rand()%171; // random call time
						succeeded++;	// count succeeded calls
					}
					else{	// no free trunk, the call is blocked
						blocked++;	// increment the block counter
					}
				} // if
			} // if
			else // the phone is busy
			{
				t[j].remaind_time--;	// decrease remained time
				if (t[j].remaind_time == 0) {	// release trunk if remained time equal to 0
					t[j].state = IDLE;	// call is released
					releaseTrunk(tk, t[j].ID);	 // release the trunk
				} // if
			}
		} // for(j)
		int busy = updatePhones(t);	// update the screen
		showInfo(i, busy, blocked, succeeded);	// to display information about time, number of telephon in busy,
							// blocked calls and succeded
		trunkState(t, tk);	// to display trunk state

		sleep(1);	// set one unit time to 1 second
	} // for(i)
} // function

/*
	function showInfo display basic information on the screen like:
	simulation time, number of telephone in busy, blocked calls and
	succeded.
	arguments:
		int t: simulation time
		int b: number of busy calls
		int bl: number of blocked calls
		int s: number of secceded calls
	return: none;
*/
void showInfo(int t, int b, int bl, int s){ // blocked and successfull
	gotoxy(6, TIMEROW);	// display on the top of the screen
	printf("Time: %4d, Busy: %3d, Blocked: %3d, Succeded: %3d", t, b, bl, s);
} // function

/*
	function trunkState display informations about trunk, like:
	number of current trunk, which telephone use it and how many
	time remained to end current call (if trunk state is idle just
	display "IDLE").
	arguments:
		telephone[] t: array of telephone struct
		trunk[] tk: array of trunk struct
	return: none;
*/
void trunkState(telephone *t, trunk *tk){
	int i;
	for (i=0; i<TRUNK_SIZE; ++i){	// display information for all trunks
		gotoxy(1, 20+i);	// go to correct line
		printf("\033[2K");	// clear the line
		printf("Trunk %2d: ", tk[i].ID);	// print ID of the trunk
		if (tk[i].state == IDLE) printf( "IDLE");	// if state of the trunk is idle just display "IDLE"
		else printf("used by Phone %3d, remained time: %3d", tk[i].tele_ID, t[tk[i].tele_ID-1].remaind_time);	// display trunk struct members value if not idle
		puts("");
	} // for
} // function
