using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication2
{
    public class CWICZENIE
    {
        private static int [] ID_filmu = 
        {
            /// <summary>
            /// ID film ćwiczenie1 (tryb: treningowy, ćwiczenie: 1)
            1,
            /// </summary>
            ///

            /// <summary>
            /// ID film ćwiczenie2 (tryb: treningowy, ćwiczenie: 2)
            4,
            /// </summary>
            ///

            /// <summary>
            /// ID film ćwiczenie3 (tryb: treningowy, ćwiczenie: 3)
            7,
            /// </summary>
            ///

            /// <summary>
            /// ID film ćwiczenie4 (tryb: treningowy, ćwiczenie: 4)
            10,
            /// </summary>
            ///

            /// <summary>
            /// ID film ćwiczenie5 (tryb: treningowy, ćwiczenie: 5)
            14,
            /// </summary>
            ///

            /// <summary>
            /// ID film ćwiczenie6 (tryb: treningowy, ćwiczenie: 6)
            17,
            /// </summary>
            ///

            /// <summary>
            /// ID film ćwiczenie7 (tryb: kontroli ruchów, ćwiczenie: 1)
            20,
            /// </summary>
            ///

            /// <summary>
            /// ID film ćwiczenie8 (tryb: kontroli ruchów, ćwiczenie: 2)
            21,
            /// </summary>
            ///

            /// ID film ćwiczenie9 (tryb: kontroli ruchów, ćwiczenie: 3)
            22,
            /// </summary>
            ///

            /// <summary>
            /// ID film ćwiczenie10 (tryb: kontroli ruchów, ćwiczenie: 4)
            23,
            /// </summary>
            ///

            /// ID film ćwiczenie11 (tryb: kontroli ruchów, ćwiczenie: 5)
            24,
            /// </summary>
            ///

            /// <summary>
            /// ID film ćwiczenie12 (tryb: kontroli ruchów, ćwiczenie: 6)
            25
            /// </summary>
            ///
        };

        private static string [] opis =
        {
            /// <summary>
            /// Opis ćwiczenie1 (tryb: treningowy, ćwiczenie: 1)
            "Ćwiczenie: wykonaj krok w prawo, dostaw lewą stopę. Następnie wykonaj krok w lewo, dostaw prawą stopę (każdy dostawienie stopy kończy jedno powtórzenie ćwiczenia). Wykonując krok w bok ręce zgięte w łokciu pod kątem ok. 90 stopni podnieś przed siebie na wysokość klatki piersiowej. Dostawiając stopę, odchyl ręce w kierunku bioder.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie2 (tryb: treningowy, ćwiczenie: 2)
            "Ćwiczenie: wykonaj krok prawą stopą skosem na prawo przed siebie, przenieś cały ciężar ciała na prawą stopę, jednocześnie wysuwając lewą wyprostowaną w łokciu rękę do przodu równolegle do stopy. Podnieś lewą nogę zgiętą w kolanie przed siebie, jednocześnie wysuwając prawą wyprostowaną w łokciu rękę do przodu równolegle do lewej nogi, a lewą rękę schowaj do tyłu. Każde wysunięcie kolana jest powtórzeniem ćwiczenia. Analogicznie wykonaj ćwiczenie w lewo (ćwiczy prawe kolano).",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie3 (tryb: treningowy, ćwiczenie: 3)
            "Ćwiczenie: wykonaj krok w prawo, lewą nogę zegnij w kolanie tak, aby pięta dotknęła pośladka. Następnie postaw stopę na podłożu (krok w lewo), prawą nogę zegnij w kolanie tak, aby pięta dotknęła pośladka. Wykonując krok w bok ręce zgięte w łokciu pod kątem ok. 90 stopni podnieś przed siebie na wysokość klatki piersiowej. Zginając kolano, odchyl ręce w kierunku bioder. Każde dotknięcie piętą pośladka, to jedno powtórzenie.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie4 (tryb: treningowy, ćwiczenie: 4)
            "Ćwiczenie: wykonaj krok w prawo, podnieś ręce wyprostowane nad głowę. Lewą nogę zegnij w kolanie i unieś skosem na wysokość brzucha, równocześnie obie ręce na wyprostowanych łokciach skieruj na wysokość kostek. Postaw nogę na podłożu i powtórz ćwiczenie drugą nogą. Każde podniesienie kolana kończy jedno powtórzenie.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie5 (tryb: treningowy, ćwiczenie: 5)
            "Ćwiczenie: wykonaj krok do przodu prawą nogą. Podnieś lewą nogę zgiętą w kolanie przed siebie, jednocześnie wysuwając prawą zgiętą w łokciu rękę do przodu (tak by łokciem niemal dotykać kolana, skręcając lekko tułów), a lewą rękę schowaj wyprostowaną do tyłu. Wykonaj lewą nogą krok w tył, następnie dostaw prawą. Wykonaj to ćwiczenie zaczynając od lewej nogi. Każde dostawienie stopy kończy jedno powtórzenie.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie6 (tryb: treningowy, ćwiczenie: 6)
            "Ćwiczenie: wykonaj prawą nogą krok po skosie do przodu, rękę lewą ugiętą w łokciu wysuń przed siebie na wysokość klatki piersiowej. Wykonaj lewą nogą krok tak, by z prawą były w jednej linii, tworząc rozkrok na lekko ugiętych kolanach. Jednocześnie zamień rękę lewą na prawą. Wróć do pozycji wyjściowej kolejnymi dwoma krokami, wymachując nadal rękoma. Złączenie stóp kończy powtórzenie.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie7 (tryb: kontroli ruchów, ćwiczenie: 1)
            "Ćwiczenie: unieś prawą wyprostowaną rękę łukiem nad głowę, przechyl całe ciało w lewo, tak by poczuć naciąganie prawego boku. Pamiętaj o tym, by nie ruszać bioder. Wróć do pozycji wyjściowej. Analogicznie wykonaj ćwiczenie na lewą rękę. Powtórz ćwiczenie X razy.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie8 (tryb: kontroli ruchów, ćwiczenie: 2)
            "Ćwiczenie: ugnij kolana, ustawiając miednicę jak gdybyś chciał/a usiąść na krześle. Tułów lekko wychyl do przodu. Kolana nie powinny wychodzić poza linię stóp. Przy składzie ramiona wyciągnij do przodu. Wróć do pozycji wyjściowej. Powtórz ćwiczenie X razy.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie9 (tryb: kontroli ruchów, ćwiczenie: 3)
            "Ćwiczenie: ugnij lekko kolana tak, by poczuć pracę ud (to nie jest ćwiczenie rozciągające). Wróć do pozycji wyjściowej. Powtórz ćwiczenie X razy.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie8 (tryb: kontroli ruchów, ćwiczenie: 4)
            "Ćwiczenie: ugnij prawe kolano tak, by niemal dotykało podłoża. Wyprostuj kolano. Powtórz  4 krotnie. Wróć do pozycji wyjściowej. Analogicznie wykonaj ćwiczenie drugą nogą. Całość powtórz 4 razy.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie7 (tryb: kontroli ruchów, ćwiczenie: 5)
            "Ćwiczenie: przechyl tułów w prawo o ok. 45 stopni (tak by poczuć naciąganie lewego boku). Wróć do pozycji wyjściowej. Powtórz w/w czynności 8 razy. Następnie analogicznie wykonaj ćwiczenie przechylając tułów w lewo. Całość powtórz 4 krotnie.",
            /// </summary>
            ///

            /// <summary>
            /// Opis ćwiczenie8 (tryb: kontroli ruchów, ćwiczenie: 6)
            "Ćwiczenie: złam się w pasie (plecy wyprostowane) tak, by między tułowiem i nogami powstał kąt 90 stopni, ręce wyprostowane z dłońmi splecionymi trzymaj na wysokości tułowia przed sobą, patrz przed siebie. Wykonaj skłon w dół. Wróć do poprzedniej pozycji. Następnie wyprostuj się z rękoma uniesionymi nad głową i nadal splecionymi dłońmi. Całość powtórz 4 krotnie."
            /// </summary>
            ///
        };


        public int pobierz_id_filmu(int tryb, int cwiczenie_nr)
        {
            int index = 0;

            if (tryb == 1 && cwiczenie_nr == 1)
            {
                index = 0;
            }
            else if (tryb == 1 && cwiczenie_nr == 2)
            {
                index = 1;
            }
            else if (tryb == 1 && cwiczenie_nr == 3)
            {
                index = 2;
            }
            else if (tryb == 1 && cwiczenie_nr == 4)
            {
                index = 3;
            }
            else if (tryb == 1 && cwiczenie_nr == 5)
            {
                index = 4;
            }
            else if (tryb == 1 && cwiczenie_nr == 6)
            {
                index = 5;
            }
            else if (tryb == 2 && cwiczenie_nr == 1)
            {
                index = 6;
            }
            else if (tryb == 2 && cwiczenie_nr == 2)
            {
                index = 7;
            }
            else if (tryb == 2 && cwiczenie_nr == 3)
            {
                index = 8;
            }
            else if (tryb == 2 && cwiczenie_nr == 4)
            {
                index = 9;
            }
            else if (tryb == 2 && cwiczenie_nr == 5)
            {
                index = 10;
            }
            else if (tryb == 2 && cwiczenie_nr == 6)
            {
                index = 11;
            }

            return ID_filmu[index];
        }

        public string pobierz_opis(int tryb, int cwiczenie_nr)
        {
            int index = 0;

            if (tryb == 1 && cwiczenie_nr == 1)
            {
                index = 0;
            }
            else if (tryb == 1 && cwiczenie_nr == 2)
            {
                index = 1;
            }
            else if (tryb == 1 && cwiczenie_nr == 3)
            {
                index = 2;
            }
            else if (tryb == 1 && cwiczenie_nr == 4)
            {
                index = 3;
            }
            else if (tryb == 1 && cwiczenie_nr == 5)
            {
                index = 4;
            }
            else if (tryb == 1 && cwiczenie_nr == 6)
            {
                index = 5;
            }
            else if (tryb == 2 && cwiczenie_nr == 1)
            {
                index = 6;
            }
            else if (tryb == 2 && cwiczenie_nr == 2)
            {
                index = 7;
            }
            else if (tryb == 2 && cwiczenie_nr == 3)
            {
                index = 8;
            }
            else if (tryb == 2 && cwiczenie_nr == 4)
            {
                index = 9;
            }
            else if (tryb == 2 && cwiczenie_nr == 5)
            {
                index = 10;
            }
            else if (tryb == 2 && cwiczenie_nr == 6)
            {
                index = 11;
            }

            return opis[index];
        }
    }
}
