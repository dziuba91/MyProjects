using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WpfApplication2
{
    public class TRYB_TRENINGOWY
    {
        public bool [] cwiczenie_status = { false, false, false, false, false, false, false };

        private int [] ID_filmu = { 0, 0, 0, 0, 0, 0, 0 };
        public int czas_treningu = 0;
        private int liczba_powtorzen_cwiczenia = 0;

        private int powtorzenie_index = 0;
        private int cwiczenie_index = 0;

        public void utworz_cwiczenie(int cwiczenie)
        {
            if (cwiczenie_status[cwiczenie])
            {
                cwiczenie_status[cwiczenie] = false;
            }
            else
            {
                cwiczenie_status[cwiczenie] = true;
            }

            if (cwiczenie_status[cwiczenie])
            {
                CWICZENIE cw = new CWICZENIE();

                ID_filmu [cwiczenie]= cw.pobierz_id_filmu(1, cwiczenie);
            }
        }

        public void aktualizuj_parametry_treningu(int parametr, int wartosc)
        {
            if (parametr - 10 == 1 || parametr - 10 == 2 || parametr - 10 == 3)
            {
                liczba_powtorzen_cwiczenia = wartosc;
            }
            else if (parametr - 20 == 1 || parametr - 20 == 2 || parametr - 20 == 3)
            {
                czas_treningu = wartosc;
            }
            else
            {
                liczba_powtorzen_cwiczenia = 0;
                czas_treningu = 0;
            }
        }

        public bool wlacz_trening()
        {
            for (int i = 1; i < 7; i++)
            {
                if (cwiczenie_status[i] == true && liczba_powtorzen_cwiczenia != 0 && czas_treningu != 0)
                {
                    powtorzenie_index = liczba_powtorzen_cwiczenia;
                    return true;
                }
            }

            return false;
        }

        public void wyswietl_liste_filmow(MediaElement me)
        {
            int i;

            etykieta:
            for (i = cwiczenie_index; i < 7; i++)
            {
                if (cwiczenie_status[i] == true)
                {
                    cwiczenie_index = i;
                    break;
                }
            }
            if (i == 7)
            {
                cwiczenie_index = 0;
                goto etykieta;
            }

            FILM f = new FILM();
            f.wlacz_film(me, ID_filmu[cwiczenie_index]);

            powtorzenie_index--;
            if (powtorzenie_index == 0)
            {
                powtorzenie_index = liczba_powtorzen_cwiczenia;
                cwiczenie_index++;
            }
        }
    }
}
