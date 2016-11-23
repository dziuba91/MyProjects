using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace WpfApplication2
{
	public class TRYB_KONTROLI_RUCHOW
	{
		KONTROLA_ROCHOW kr = null;

		private int ID_film;
		public int ID_cwiczenie;
		public int wynik_cwiczenia;
		public int czas_treningu = 0;

		public void utworz_cwiczenie(int cwiczenie)
		{
			ID_cwiczenie = cwiczenie;

			CWICZENIE cw = new CWICZENIE();
			ID_film = cw.pobierz_id_filmu(2, cwiczenie);
		}

		public void wyswietl_film(MediaElement me)
		{
			FILM f = new FILM();
			f.wlacz_film(me, ID_film);
		}

		public bool analiza_cwiczenia(ProgressBar progres, Point [] p)
		{
			if (czas_treningu == 0 && ID_cwiczenie == 1)
			{
				czas_treningu = 60;
				return false;
			}
			else if (czas_treningu == 0 && ID_cwiczenie == 2)
			{
				czas_treningu = 180;
				return false;
			}
			else if (czas_treningu == 0 && ID_cwiczenie == 3)
			{
				czas_treningu = 180;
				return false;
			}
			else if (czas_treningu == 0 && ID_cwiczenie == 4)
			{
				czas_treningu = 180;
				return false;
			}
			else if (czas_treningu == 0 && ID_cwiczenie == 5)
			{
				czas_treningu = 180;
				return false;
			}
			else if (czas_treningu == 0 && ID_cwiczenie == 6)
			{
				czas_treningu = 180;
				return false;
			}

			if (kr == null)
			{
				kr = new KONTROLA_ROCHOW(ID_cwiczenie);
			}

			wynik_cwiczenia = kr.wygeneruj_wynik(p);
			progres.Value = wynik_cwiczenia;

			if (wynik_cwiczenia == 100)
			{
				return true;
			}

			return false;
		}
	}
}
