using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WpfApplication2
{
	public class KONTROLA_ROCHOW
	{
		private int cwiczenie_nr;
		private int wynik = 0;
		private int wynik2 = 0;
		private int etap = 0;
		private bool [] etap_status = null;
		private double punkt;
		private int[] wynik_etap;
		private bool wynik_accepted = false;

		public KONTROLA_ROCHOW(int cwiczenie_nr)
		{
			this.cwiczenie_nr = cwiczenie_nr;
		}

		public int wygeneruj_wynik(Point [] p_tab)
		{
			if (cwiczenie_nr == 1)
			{
				if (etap_status == null)
				{
					etap_status = new bool[3];
					etap_status[0] = false;
					etap_status[1] = false;
					etap_status[2] = false;

					wynik_etap = new int[3];
					wynik_etap[0] = 0;
					wynik_etap[1] = 0;
					wynik_etap[2] = 0;
				}

				wynik= algorytm_kontroli1(p_tab);
			}

			return wynik;
		}

		public int algorytm_kontroli1(Point [] points)
		{
			if (etap == 0 && etap_status[0] && (points[1].X <= points[3].X && points[2].X < points[4].X)) //lewa ręka za lewą nogą
			{
				if (points[1].Y <= points[5].Y && points[2].Y > points[5].Y) // lewa reka nad HipCenter
				{
					etap++;
					punkt = points[1].Y;

					if (!wynik_accepted)
					{
						wynik += 5;
					}
					else
					{
						wynik2 += 5;
					}
				}

				return wynik;
			}

			if (etap == 0)
			{
				int wynik_etap = 0;
				if (points[1].Y > points[5].Y && points[2].Y > points[5].Y)
				{
					wynik_etap = 10;

					if (points[1].X < points[5].X && points[2].X > points[5].X)
					{
						wynik_etap = 20;

						if (points[1].X > points[3].X && points[2].X < points[4].X)
						{
							wynik_etap = 30;
						}
					}
					etap_status[0] = true;
				}
				else
				{
					etap_status[0] = false;
				}

				if (!wynik_accepted)
				{
					wynik = wynik_etap;
				}
				else
				{
					wynik2 = wynik_etap;
				}
			}

			if (etap == 1 && (points[1].Y > punkt)) //gdy lewa reka pod staraym punktem (reka w dol)
			{
				etap--;
				if (!wynik_accepted)
				{
					wynik = 0;
				}
				else
				{
					wynik2 = 0;
				}

				wynik_etap[1] = 0;

				return wynik;
			}

			if (etap == 1)
			{
				punkt = points[1].Y;

				//1 6 7
				double sprawdz= (double)(points[1].Y + points[6].Y + points[7].Y) / 3 - points[6].Y;
				int wynik_bierzacy=0;

				if (sprawdz < 2.5)
				{
					wynik_bierzacy = 30;
				}
				else if (sprawdz < 5.0)
				{
					wynik_bierzacy = 20;
				}
				else if (sprawdz < 10.0)
				{
					wynik_bierzacy = 10;
				}
				
				if (wynik_bierzacy > wynik_etap[1])
				{
					if (!wynik_accepted)
					{
						wynik -= wynik_etap[1];
						wynik_etap[1] = wynik_bierzacy;
						wynik += wynik_etap[1];
					}
					else
					{
						wynik2 -= wynik_etap[1];
						wynik_etap[1] = wynik_bierzacy;
						wynik2 += wynik_etap[1];
					}
				}

				if (points[1].Y < points[0].Y) // nad głową
				{
					etap++;

					if (!wynik_accepted)
					{
						wynik += 5;
					}
					else
					{
						wynik2 += 5;
					}
					punkt = points[1].Y;
				}

				return wynik;
			}

			if (etap == 2 && (points[1].X < punkt)) //gdy lewa reka pod staraym punktem (reka w dol)
			{
				etap=0;
				if (!wynik_accepted)
				{
					wynik = 0;
				}
				else
				{
					wynik2 = 0;
				}

				wynik_etap[2] = 0;

				return wynik;
			}

			if (etap == 2)
			{
				punkt = points[1].X;

				//1 6 7
				double sprawdz = (double)(points[1].X + points[6].X + points[7].X) / 3 - points[6].X;
				int wynik_bierzacy = 0;

				if (sprawdz < 2.5)
				{
					wynik_bierzacy = 20;
				}
				else if (sprawdz < 5.0)
				{
					wynik_bierzacy = 10;
				}
				else if (sprawdz < 10.0)
				{
					wynik_bierzacy = 5;
				}

				if (wynik_bierzacy > wynik_etap[2])
				{
					if (!wynik_accepted)
					{
						wynik -= wynik_etap[2];
						wynik_etap[2] = wynik_bierzacy;
						wynik += wynik_etap[2];
					}
					else
					{
						wynik2 -= wynik_etap[2];
						wynik_etap[2] = wynik_bierzacy;
						wynik2 += wynik_etap[2];
					}
				}

				if (points[1].X > points[0].X) // za głową
				{
					if (wynik_accepted && wynik2 > wynik)
					{
						wynik = wynik2;
					}
					wynik_accepted = true;
					
					punkt = points[1].Y;

					if (points[1].X > points[4].X)
					{
						wynik += 10;
						etap++;
					}
				}

				return wynik;
			}

			if (etap == 3)
			{
				wynik2 = 0;

				wynik_etap[0] = 0;
				wynik_etap[1] = 0;
				wynik_etap[2] = 0;
			}

			return wynik;
		}
	}
}
