using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace WpfApplication2
{
	public class MAGICZNE_ZAKLECIE
	{
		private int rodzaj_zaklecia;

		private static string[] opis = 
		{
			/// <summary>
			/// opis Magii 1
			"W wyniku magii przywołałeś smoka! \nSmok spalił Ci ekran co zmieniło jego barwę."
			/// </summary>
			///
		};

		private static int[] ID_film = 
		{
			/// <summary>
			/// ID film (magia 1)
			13
			/// </summary>
			///
		};

		private static int[] ID_muzyka = 
		{
			/// <summary>
			/// ID muzyka (magia 1)
			30
			/// </summary>
			///
		};

		public void wybierz_zaklecie(MediaElement m1, MediaElement m2, Canvas c1, Label l1)
		{
			Random r1 = new Random(System.DateTime.Now.Millisecond);
			rodzaj_zaklecia = r1.Next(1, ID_film.Length);

			l1.Content = opis[rodzaj_zaklecia - 1];
			efekt_zaklecia(c1);
			odtwarzaj_muzyke(m2);
			wyswietl_film(m1);
		}

		public void efekt_zaklecia(Canvas c1)
		{
			if (rodzaj_zaklecia == 1)
			{
				c1.Background = Brushes.DarkRed;
			}
		}

		private void odtwarzaj_muzyke(MediaElement m2)
		{
			MUZYKA muz = new MUZYKA();
			muz.wlacz_muzyke(m2, ID_muzyka[rodzaj_zaklecia - 1]);
		}

		private void wyswietl_film(MediaElement m1)
		{
			FILM film = new FILM();
			film.wlacz_film(m1, ID_film[rodzaj_zaklecia - 1]);
		}
	}
}
