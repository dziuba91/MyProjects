#include <iostream>
#include <cmath>

using namespace std;

enum Direction {RIGHT, LEFT, UP, DOWN};

int MIN = 0;
int X2, Y2;

void move(Direction d, int * cube, int sum, int x1, int y1);
void set_min(int * cube, int x1, int y1);
int * move_cube(Direction d, int * cube);
void set_next_moves(Direction d, int * cube, int sum, int x1, int y1);

main ()
{
	int Z;
	int X1, Y1;
	int CUBE[6];
	
	cin >> Z;

	for (;Z>0;Z--)
	{
		for (int i = 0; i < 6; i++) cin >> CUBE[i];

		cin >> X1;
		cin >> Y1;
		cin >> X2;
		cin >> Y2;
		
		set_min(CUBE, X1, Y1);
		
		move(LEFT, CUBE, 0, X1, Y1);
		move(RIGHT, CUBE, 0, X1, Y1);
		if (Y1>1) move(DOWN, CUBE, 0, X1, Y1);
		if (Y1<4) move(UP, CUBE, 0, X1, Y1);
		
		cout << MIN << endl;
	}
	
	return EXIT_SUCCESS;
}

////
void move(Direction d, int * cube, int sum, int x1, int y1)
{
	switch (d)
	{
		case UP: 
			y1++;
		break;
		
		case DOWN:
			y1--;
		break;
		
		case RIGHT:
			x1++;
		break;
		
		case LEFT:
			x1--;
		break;
		
		default: break;
	}

	cube = move_cube(d, cube);
	sum += cube[0];
	 
	if (sum < MIN)
	{
		if ((x1==X2)&&(y1==Y2))
		{
			MIN = sum;
			
			delete cube;
		}
		else
			set_next_moves(d, cube, sum, x1, y1);
	}
	else
		delete cube;
}

void set_next_moves(Direction d, int * cube, int sum, int x1, int y1)
{
	Direction prev = (d==LEFT)?RIGHT:(d==RIGHT)?LEFT:(d==UP)?DOWN:UP;
	 
	if (prev != LEFT)
		move(LEFT, cube, sum, x1, y1);
	 
	if (prev != RIGHT)
		move(RIGHT, cube, sum, x1, y1);
	 
	if ((prev != UP) && (y1<4))
		move(UP, cube, sum, x1, y1);
	 
	if ((prev != DOWN) && (y1>1))
		move(DOWN, cube, sum, x1, y1);
		
	delete cube;
}

int * move_cube(Direction d, int * cube)
{
	int tmp;
	 
	int * A = new int[6];
	for (int i=0; i<6; i++) A[i] = cube[i];
	 
	if (d == LEFT)
	{
		tmp = A[0];
		A[0] = A[2];
		A[2] = A[5];
		A[5] = A[3];
		A[3] = tmp;
	}
	else if (d == RIGHT)
	{
		tmp = A[0];
		A[0] = A[3];
		A[3] = A[5];
		A[5] = A[2];
		A[2] = tmp;
	}
	else if (d == UP)
	{
		tmp = A[0];
		A[0] = A[1];
		A[1] = A[5];
		A[5] = A[4];
		A[4] = tmp;
	}
	else  // DOWN
	{
		tmp = A[0];
		A[0] = A[4];
		A[4] = A[5];
		A[5] = A[1];
		A[1] = tmp;
	}
	 
	return A;
}

void set_min(int * cube, int x1, int y1)
{
	int * A = new int[6];;
	for (int i=0; i<6; i++) A[i]=cube[i];
	 
	for (; y1 < Y2; y1++) // go up
	{
		int * tmp = A;
		A = move_cube(UP, A);
		delete tmp;
		 
		MIN += A[0];
	}
	 
	for (; y1 > Y2; y1--) // go down
	{
		int * tmp = A;
		A = move_cube(DOWN, A);
		delete tmp;
		 
		MIN += A[0];
	}
	 
	for (; x1 < X2; x1++) // go right
	{
		int * tmp = A;
		A = move_cube(RIGHT, A);
		delete tmp;
		 
		MIN += A[0];
	}
	 
	for (; x1 > X2; x1--) // go left
	{
		int * tmp = A;
		A = move_cube(LEFT, A);
		delete tmp;
		 
		MIN += A[0];
	}
	 
	delete A;
}