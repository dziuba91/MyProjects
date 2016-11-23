using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace aplikacja2__XNA_.BasicComponent
{
	class RubikCubeHelper
	{
		int trybObrotu4 = 0;

		Random rand;

		public readonly int [,] numb = {				 // Oznaczenie (numeracja) kwadracików 
						{1,1,2}, {1,1,0}, {1,1,1},
						{0,1,2}, {0,1,0}, {0,1,1},
						{2,1,2}, {2,1,0}, {2,1,1},

						{1,0,2}, {1,0,0}, {1,0,1},
						{0,0,2}, {0,0,0}, {0,0,1},
						{2,0,2}, {2,0,0}, {2,0,1},

						{1,2,2}, {1,2,0}, {1,2,1},
						{0,2,2}, {0,2,0}, {0,2,1},
						{2,2,2}, {2,2,0}, {2,2,1}
					};


		public float[,] rot { get; private set; }

		public int[,] rot_nr { get; private set; }
		public int[] licznik { get; private set; }

		int [,] tab;

		int [,] kostka =  {
						{0,1,2,
						 3,4,5,
						 6,7,8},

						 {9,10,11,
						  12,13,14,
						  15,16,17},
						 
						 {18,19,20,
						  21,22,23,
						  24,25,26}
					 };

		bool wait4 = false;
		float max4 = 0.0f;

		int k;

		public float rotatuj { get; set; }
		public float rotatuj_1 { get; set; }
		public float rotatuj_2 { get; set; }

		//
		public RubikCubeHelper()
		{
			this.rotatuj = 5.0f;
			this.rotatuj_1 = 5.0f;
			this.rotatuj_2 = 0.5f;

			rot = new float [27,3];
			rot_nr = new int [27,1000];
			licznik = new int [27];
			tab= new int [3,3];

			rand = new Random();
		}

		public void Rotate()
		{
			if (trybObrotu4 == 0) { max4 = 0.0f; wait4 = false; }
			else if (trybObrotu4 == 10)		// Tryb 10;
			{
				k = 2;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[k,j],1] -= rotatuj;
					}
					k--;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i, j] = kostka[k, j];
						}
						k--;
					}

					k = 2;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[k, j] = tab[j, l];
						}
						k--;
						l--;
					}
					
					//
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[k,j],1] = 0.0f;

							rot_nr[kostka[k,j],licznik[kostka[k,j]]] = 10;
							licznik[kostka[k,j]]++;
						}
						k--;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 11)		// Tryb 11;
			{
				k = 2;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[k,j + 3],1] -= rotatuj;
					}
					k--;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[k,j + 3];
						}
						k--;
					}

					k = 2;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[k,j + 3] = tab[j,l];
						}
						k--;
						l--;
					}

					//
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[k,j + 3],1] = 0.0f;

							rot_nr[kostka[k,j + 3],licznik[kostka[k,j + 3]]] = 10;
							licznik[kostka[k,j + 3]]++;
						}
						k--;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 12)		// Tryb 12
			{
				k = 2;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[k,j + 6],1] -= rotatuj;
					}
					k--;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[k,j + 6];
						}
						k--;
					}

					k = 2;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[k,j + 6] = tab[j,l];
						}
						k--;
						l--;
					}

					//
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[k,j + 6],1] = 0.0f;

							rot_nr[kostka[k,j + 6],licznik[kostka[k,j + 6]]] = 10;
							licznik[kostka[k,j + 6]]++;
						}
						k--;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 20)		// Tryb 20
			{
				for (int j = 0; j < 9; j++)
				{
					rot[kostka[0,j],2] -= rotatuj;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{

					int index = 0;
					for (int i = 0; i < 3; i++)
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[0,index];
							index++;
						}

					k = 2;
					index = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[0,index] = tab[j,k];
							index++;
						}
						k--;
					}

					for (int j = 0; j < 9; j++)
					{
						rot[kostka[0,j],2] = 0.0f;

						rot_nr[kostka[0,j],licznik[kostka[0,j]]] = 20;
						licznik[kostka[0,j]]++;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 21)		// Tryb 21
			{
				for (int j = 0; j < 9; j++)
				{
					rot[kostka[1,j],2] -= rotatuj;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{

					int index = 0;
					for (int i = 0; i < 3; i++)
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[1,index];
							index++;
						}

					k = 2;
					index = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[1,index] = tab[j,k];
							index++;
						}
						k--;
					}

					for (int j = 0; j < 9; j++)
					{
						rot[kostka[1,j],2] = 0.0f;

						rot_nr[kostka[1,j],licznik[kostka[1,j]]] = 20;
						licznik[kostka[1,j]]++;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 22)		// Tryb 22
			{
				for (int j = 0; j < 9; j++)
				{
					rot[kostka[2,j],2] -= rotatuj;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					int index = 0;
					for (int i = 0; i < 3; i++)
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[2,index];
							index++;
						}

					k = 2;
					index = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[2,index] = tab[j,k];
							index++;
						}
						k--;
					}

					for (int j = 0; j < 9; j++)
					{
						rot[kostka[2,j],2] = 0.0f;

						rot_nr[kostka[2,j],licznik[kostka[2,j]]] = 20;
						licznik[kostka[2,j]]++;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 30)		// Tryb 30 
			{
				k = 2;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[i,k],0] -= rotatuj;
						k += 3;
					}
					k = 2;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[i,k];
							k += 3;
						}
						k = 2;
					}

					k = 2;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[i,k] = tab[l,i];
							k += 3;
							l--;
						}
						k = 2;
						l = 2;
					}

					//
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[i,k],0] = 0.0f;

							rot_nr[kostka[i,k],licznik[kostka[i,k]]] = 30;
							licznik[kostka[i,k]]++;

							k += 3;
						}
						k = 2;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 31)		// Tryb 31 
			{
				k = 1;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[i,k],0] -= rotatuj;
						k += 3;
					}
					k = 1;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 1;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[i,k];
							k += 3;
						}
						k = 1;
					}

					k = 1;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[i,k] = tab[l,i];
							k += 3;
							l--;
						}
						k = 1;
						l = 2;
					}

					//
					k = 1;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[i,k],0] = 0.0f;

							rot_nr[kostka[i,k],licznik[kostka[i,k]]] = 30;
							licznik[kostka[i,k]]++;

							k += 3;
						}
						k = 1;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 32)		// Tryb 32
			{
				k = 0;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[i,k],0] -= rotatuj;
						k += 3;
					}
					k = 0;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[i,k];
							k += 3;
						}
						k = 0;
					}

					k = 0;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[i,k] = tab[l,i];
							k += 3;
							l--;
						}
						k = 0;
						l = 2;
					}

					//
					k = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[i,k],0] = 0.0f;

							rot_nr[kostka[i,k],licznik[kostka[i,k]]] = 30;
							licznik[kostka[i,k]]++;

							k += 3;
						}
						k = 0;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 42)		// Tryb 42; -> zmiana kierunku rotacji
			{
				k = 2;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[k,j],1] += rotatuj;
					}
					k--;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[k,j];
						}
						k--;
					}

					k = 2;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[k,j] = tab[l,i];	//
							l--;	//
						}
						k--;
						l = 2;	//
					}

					//
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[k,j],1] = 0.0f;

							rot_nr[kostka[k,j],licznik[kostka[k,j]]] = 40;		//
							licznik[kostka[k,j]]++;
						}
						k--;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 41)		// Tryb 41;
			{
				k = 2;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[k,j + 3],1] += rotatuj;
					}
					k--;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[k,j + 3];
						}
						k--;
					}

					k = 2;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[k,j + 3] = tab[l,i];	//
							l--;	//
						}
						k--;
						l = 2;	//
					}

					//
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[k,j + 3],1] = 0.0f;

							rot_nr[kostka[k,j + 3],licznik[kostka[k,j + 3]]] = 40;		//
							licznik[kostka[k,j + 3]]++;
						}
						k--;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 40)		// Tryb 40
			{
				k = 2;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[k,j + 6],1] += rotatuj;
					}
					k--;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[k,j + 6];
						}
						k--;
					}

					k = 2;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[k,j + 6] = tab[l,i];	//
							l--;	//
						}
						k--;
						l = 2;	//
					}

					//
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[k,j + 6],1] = 0.0f;

							rot_nr[kostka[k,j + 6],licznik[kostka[k,j + 6]]] = 40;
							licznik[kostka[k,j + 6]]++;
						}
						k--;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 52)		// Tryb 52
			{
				for (int j = 0; j < 9; j++)
				{
					rot[kostka[0,j],2] += rotatuj;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{

					int index = 0;
					for (int i = 0; i < 3; i++)
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[0,index];
							index++;
						}

					k = 2;
					index = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[0,index] = tab[k,i];	//
							k--;	//
							index++;
						}
						k = 2;	//
					}

					for (int j = 0; j < 9; j++)
					{
						rot[kostka[0,j],2] = 0.0f;

						rot_nr[kostka[0,j],licznik[kostka[0,j]]] = 50;
						licznik[kostka[0,j]]++;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 51)		// Tryb 51
			{
				for (int j = 0; j < 9; j++)
				{
					rot[kostka[1,j],2] += rotatuj;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{

					int index = 0;
					for (int i = 0; i < 3; i++)
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[1,index];
							index++;
						}

					k = 2;
					index = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[1,index] = tab[k,i];	//
							k--;	//
							index++;
						}
						k = 2;	//
					}

					for (int j = 0; j < 9; j++)
					{
						rot[kostka[1,j],2] = 0.0f;

						rot_nr[kostka[1,j],licznik[kostka[1,j]]] = 50;
						licznik[kostka[1,j]]++;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 50)		// Tryb 50
			{
				for (int j = 0; j < 9; j++)
				{
					rot[kostka[2,j],2] += rotatuj;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{

					int index = 0;
					for (int i = 0; i < 3; i++)
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[2,index];
							index++;
						}

					k = 2;
					index = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[2,index] = tab[k,i];
							k--;
							index++;
						}
						k = 2;
					}

					for (int j = 0; j < 9; j++)
					{
						rot[kostka[2,j],2] = 0.0f;

						rot_nr[kostka[2,j],licznik[kostka[2,j]]] = 50;
						licznik[kostka[2,j]]++;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 62)		// Tryb 62 
			{
				k = 2;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[i,k],0] += rotatuj;
						k += 3;
					}
					k = 2;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[i,k];
							k += 3;
						}
						k = 2;
					}

					k = 2;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[i,k] = tab[j,l];	//
							k += 3;
						}
						k = 2;
						l--;	//
					}

					//
					k = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[i,k],0] = 0.0f;

							rot_nr[kostka[i,k],licznik[kostka[i,k]]] = 60;
							licznik[kostka[i,k]]++;

							k += 3;
						}
						k = 2;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 61)		// Tryb 61 
			{
				k = 1;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[i,k],0] += rotatuj;
						k += 3;
					}
					k = 1;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 1;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[i,k];
							k += 3;
						}
						k = 1;
					}

					k = 1;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[i,k] = tab[j,l];	//
							k += 3;
						}
						k = 1;
						l--;
					}

					//
					k = 1;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[i,k],0] = 0.0f;

							rot_nr[kostka[i,k],licznik[kostka[i,k]]] = 60;
							licznik[kostka[i,k]]++;

							k += 3;
						}
						k = 1;
					}

					trybObrotu4 = 0;
				}
			}
			else if (trybObrotu4 == 60)		// Tryb 60
			{
				k = 0;
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						rot[kostka[i,k],0] += rotatuj;
						k += 3;
					}
					k = 0;
				}

				max4 += rotatuj;
				if (max4 == 90.0f)
				{
					k = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							tab[i,j] = kostka[i,k];
							k += 3;
						}
						k = 0;
					}

					k = 0;
					int l = 2;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							kostka[i,k] = tab[j,l];	//	
							k += 3;
						}
						k = 0;
						l--;	 //
					}

					//
					k = 0;
					for (int i = 0; i < 3; i++)
					{
						for (int j = 0; j < 3; j++)
						{
							rot[kostka[i,k],0] = 0.0f;

							rot_nr[kostka[i,k],licznik[kostka[i,k]]] = 60;
							licznik[kostka[i,k]]++;

							k += 3;
						}
						k = 0;
					}

					trybObrotu4 = 0;
				}
			}
		}

		public bool gameKeyboard(KeyboardState currentKeyboard, KeyboardState previousKeyboard)
		{
			if (currentKeyboard.IsKeyDown(Keys.Q))
			{
				if (!previousKeyboard.IsKeyDown(Keys.Q))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 10;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.W))
			{
				if (!previousKeyboard.IsKeyDown(Keys.W))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 11;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.E))
			{
				if (!previousKeyboard.IsKeyDown(Keys.E))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 12;
						wait4 = true;
					}
					return true;
				}
			}

			//
			if (currentKeyboard.IsKeyDown(Keys.A))
			{
				if (!previousKeyboard.IsKeyDown(Keys.A))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 20;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.S))
			{
				if (!previousKeyboard.IsKeyDown(Keys.S))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 21;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.D))
			{
				if (!previousKeyboard.IsKeyDown(Keys.D))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 22;
						wait4 = true;
					}
					return true;
				}
			}

			//
			if (currentKeyboard.IsKeyDown(Keys.Z))
			{
				if (!previousKeyboard.IsKeyDown(Keys.Z))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 30;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.X))
			{
				if (!previousKeyboard.IsKeyDown(Keys.X))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 31;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.C))
			{
				if (!previousKeyboard.IsKeyDown(Keys.C))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 32;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.R))
			{
				if (!previousKeyboard.IsKeyDown(Keys.R))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 40;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.T))
			{
				if (!previousKeyboard.IsKeyDown(Keys.T))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 41;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.Y))
			{
				if (!previousKeyboard.IsKeyDown(Keys.Y))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 42;
						wait4 = true;
					}
					return true;
				}
			}

			//
			if (currentKeyboard.IsKeyDown(Keys.F))
			{
				if (!previousKeyboard.IsKeyDown(Keys.F))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 50;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.G))
			{
				if (!previousKeyboard.IsKeyDown(Keys.G))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 51;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.H))
			{
				if (!previousKeyboard.IsKeyDown(Keys.H))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 52;
						wait4 = true;
					}
					return true;
				}
			}

			//
			if (currentKeyboard.IsKeyDown(Keys.V))
			{
				if (!previousKeyboard.IsKeyDown(Keys.V))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 60;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.B))
			{
				if (!previousKeyboard.IsKeyDown(Keys.B))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 61;
						wait4 = true;
					}
					return true;
				}
			}

			if (currentKeyboard.IsKeyDown(Keys.N))
			{
				if (!previousKeyboard.IsKeyDown(Keys.N))
				{
					if (wait4 == false)
					{
						trybObrotu4 = 62;
						wait4 = true;
					}
					return true;
				}
			}

			return false;
		}

		public void simulation()
		{
			if (wait4 == false)
			{
				int l1 = rand.Next(1, 7);
				int l2 = rand.Next(0, 3);

				trybObrotu4 = l1 * 10 + l2;
				wait4 = true;
			}
		}
	}
}
