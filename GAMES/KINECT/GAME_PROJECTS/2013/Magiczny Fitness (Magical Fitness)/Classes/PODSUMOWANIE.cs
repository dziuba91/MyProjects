using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WpfApplication2
{
	public class PODSUMOWANIE
	{
		private int czas_start;
		private int czas_stop;
		private int wynik = 0;
		private int tryb = 0;

		public PODSUMOWANIE(int czas1, int czas2)
		{
			this.czas_start = czas2;
			this.czas_stop = czas1;

			this.tryb = 1;
		}

		public PODSUMOWANIE(int czas1, int czas2, int wynik)
		{
			this.czas_start = czas2;
			this.czas_stop = czas1;
			this.wynik = wynik;

			this.tryb = 2;
		}

		public void wygeneruj_podsumowanie(Label [] l_tab) //tryb1
		{
			l_tab[0].Content = czas_start;
			l_tab[1].Content = (czas_start - czas_stop);

			l_tab[2].Content = analiza_trybu();
		}

		public void wygeneruj_podsumowanie(Label[] l_tab, ProgressBar pb) //tryb2
		{
			l_tab[0].Content = czas_start;
			l_tab[1].Content = (czas_start - czas_stop);

			l_tab[2].Content = wynik;

			l_tab[3].Content = analiza_trybu();

			pb.Value = wynik;
		}

		private string analiza_trybu()
		{
			string napis=null;

			if (tryb == 1)
			{
				if (czas_stop == 0)
				{
					napis = "W pełni ukończyłeś zaplanowany trening. \nGRATULUJEMY!";
				}
				else
				{
					napis = "Nie ukończyłeś całego zaplanowanego treningu. \nSpróbuj ponownie.!";
				}
			}
			else if (tryb == 2)
			{
				if (wynik < 30)
				{
					napis = "Niepoprawnie wykonane zadanie! \nNastępnym razem popraw się.";
				}
				else if (wynik < 50)
				{
					napis = "Popełniłeś dużo błędów! \nSpróbuj ponownie.";
				}
				else if (wynik < 75)
				{
					napis = "Popełniłeś kilka błędów! \nSpróbuj poprawić swój wynik.";
				}
				else if (wynik < 95)
				{
					napis = "Zadanie wykonane poprawnie! \nPobij swój wynik.";
				}
				else
				{

					napis = "Zadanie wykonane rewelacyjnie! \nGRATULUJEMY. Wykonałeś Magiczne Zaklęcie.";
				}
			}

			return napis;
		}

		public MAGICZNE_ZAKLECIE utworz_magiczne_zaklecie(MediaElement m1, MediaElement m2, Canvas c1, Label l1)
		{
			MAGICZNE_ZAKLECIE magia = new MAGICZNE_ZAKLECIE();
			magia.wybierz_zaklecie(m1,m2,c1,l1);

			return magia;
		}
	}
}
