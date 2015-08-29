//#include "screen.h"
#include "telesim.h"
#include <stdio.h>

/*
	main function show the general logics of this simulation
	no command line argument is needed
*/
int main(void){
	telephone myphones[TELE_SIZE]; 	// declare 1 instance
	trunk mytrunks[TRUNK_SIZE];

	initPhones(myphones); 	// initialize the phones
	initTrunks(mytrunks);	// initialize trunks

	drawPhones();	// display application window
	simulate(myphones, mytrunks);	// run simulate
}
