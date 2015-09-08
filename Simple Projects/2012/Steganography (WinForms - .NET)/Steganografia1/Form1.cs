using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System;
using System.IO;
//using System.Drawing;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
//using System.ComponentModel;
//using System.Windows.Forms;
//using System.Data; 

namespace Steganografia
{
    public partial class Form1 : Form
    {
        private const int identyfikator = 0x6733DBA7;
        string nazwa_oryginalny;
        string nazwa_zakodowany;
        string nazwa_text;
        int pamiec_cala = 0;
        int pamiec_zajeta = 0;

        int R=1;
        int G=1;
        int B=1;

        public Form1()
        {
            InitializeComponent();
        }

        private void ZaladujOrg_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                nazwa_oryginalny = openFileDialog1.FileName;
                OryObraz.Image = Image.FromFile(nazwa_oryginalny);
            }
            else return;

            string[] nazwa;
            nazwa = nazwa_oryginalny.Split('\\');
            nazwa_oryginalny = nazwa[nazwa.Length-1];

            int x, y;
            x = OryObraz.Image.Height;
            y = OryObraz.Image.Width;
            pamiec_cala = (x * y * (R + G + B) - 90) / 8;

            label1.Text = "Pozostało: " + pamiec_zajeta + "/" + pamiec_cala + "[znaków]";

            if (pamiec_zajeta > pamiec_cala)
            {
                progressBar1.Value = 0;
                label1.Text = "Brak wolnego miejsca!!!";
            }
            else
            {
                progressBar1.Value = pamiec_zajeta * 100 / pamiec_cala;
            }

            obrazInfo.Text = "\nNazwa: " + nazwa_oryginalny + "\n" + "Rozmiar: (" + x + "x" + y + ")\n" + "Ilość wolnego miejsca: " + pamiec_cala;
            toolStripStatusLabel1.Text = "Pomyślnie wprowadzono plik z obrazem: " + "'" + nazwa_oryginalny + "'";
        }

        private void ZaladujZak_Click(object sender, EventArgs e)
        {
            int liczba; // usuń to
            int liczba2;
            int x, y;

            DialogResult res = openFileDialog1.ShowDialog(this);
            if (res == DialogResult.OK)
            {
                nazwa_zakodowany = openFileDialog1.FileName;

                Steganografia sprawdzMaske = new Steganografia(Image.FromFile(nazwa_zakodowany));
                int [] maskaInfo = sprawdzMaske.ReadInfoMask();
                liczba = maskaInfo[1];
                if (maskaInfo[0] == identyfikator)
                {
                    ZakObraz.Image = Image.FromFile(nazwa_zakodowany);
                    x = ZakObraz.Image.Height;
                    y = ZakObraz.Image.Width;
                    liczba2 = (x * y * (maskaInfo[2] + maskaInfo[3] + maskaInfo[4])- 90) /8;
                }
                else
                {
                    MessageBox.Show("Brak zakodowanej informacji w podanym obrazie!\nZaładuj obraz zawierający ukryte dane.", "Brak ukrytych informacji", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else return;

            string[] nazwa;
            nazwa = nazwa_zakodowany.Split('\\');
            nazwa_zakodowany = nazwa[nazwa.Length - 1];


            pamiec_zajeta = liczba;
            pamiec_cala = liczba2;

            Wykres.Chart(new float[] { pamiec_zajeta, pamiec_cala - pamiec_zajeta });

            zakodowanyInfo.Text = "\nNazwa: " + nazwa_zakodowany + "\n" + "Rozmiar: (" + x + "x" + y + ")\n";
            toolStripStatusLabel1.Text = "Pomyślnie wprowadzono plik z obrazem: " + "'" + nazwa_oryginalny + "'";
      //      toolStripStatusLabel1.Text = Convert.ToString(liczba) + " " + Convert.ToString(liczba2);
        }

        private void ZalText_Click(object sender, EventArgs e)
        {
            //otwiera okno dialogowe Otwórz
            DialogResult res = openFileDialog2.ShowDialog();
            if (res == DialogResult.OK)
            {//jeśli kliknięto ok
                //BinaryReader fileB;
                //fileB = new BinaryReader(File.Open(openFileDialog2.FileName, FileMode.Open));
                
               // byte[] tab = new byte[]; 
                //tab = fileB.Read();
                //fileB.Close();

                StreamReader file;
                try
                {
                    nazwa_text = openFileDialog2.FileName;
                    //otwórz plik
                    file = new StreamReader(nazwa_text);
                    string tmp = file.ReadToEnd();//przeczytaj całą zawartość
                    text1.Text = tmp;//wstaw do pola tekstowego
                    file.Close();
                }
                catch
                {//wystąpił błąd podczas otwierania pliku
                    MessageBox.Show("Nie udało się otworzyć pliku: " + openFileDialog2.FileName, "Błąd otwarcia pliku", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else return;

            string[] nazwa;
            nazwa = nazwa_text.Split('\\');
            nazwa_text = nazwa[nazwa.Length - 1];

            tekstInfo.Text = "\nNazwa: " + nazwa_text + "\n" + "Wielkość: " + text1.Text.Length + " [bajty]\n";
            toolStripStatusLabel1.Text = "Pomyślnie wprowadzono plik z tekstem: " + "'" + nazwa_text + "'";
        }

        private void zakoduj_Click(object sender, EventArgs e)
        {
            //sprawdzonko
            if (text1.Text == "")
            {
                MessageBox.Show("Nie wszystkie pola zostały uzupełnione!!!", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //czy wybrano obrazek
            if (OryObraz.Image == null)
            {
                MessageBox.Show("Nie wszystkie pola zostały uzupełnione!!!", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (pamiec_zajeta > pamiec_cala)
            {
                MessageBox.Show("Brak wystarczającej ilości wolnej pamięci na nośniku!!!", "Brak miejsca", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Steganografia koduj = new Steganografia(text1.Text, OryObraz.Image, R, G, B);

            //tab_bit= koduj.textToBits();

            koduj.zakoduj_w_obrazie();

            Bitmap bmp = koduj.pobierzObraz();
            ZakObraz.Image = bmp;


            toolStripStatusLabel1.Text = "Pomyślnie zakodowano plik tekstowy: " + "'" + nazwa_text + "'" + " w obrazie: " + "'" + nazwa_oryginalny + "'";

            // Zapis na dysk
            string odp = MessageBox.Show("Zakończono proces kodowania.\nZapisać wynik na dysku?", "Zapisywanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
            if (odp == "Yes")
            {
                DialogResult res = saveFileDialog1.ShowDialog(this);
                if (res == DialogResult.OK)
                {
                    nazwa_zakodowany = saveFileDialog1.FileName;
                    bmp.Save(nazwa_zakodowany);
                }
                else return;

                string[] nazwa;
                nazwa = nazwa_zakodowany.Split('\\');
                nazwa_zakodowany = nazwa[nazwa.Length - 1];
            }
            else
            {
                nazwa_zakodowany = nazwa_oryginalny;
            }


            Wykres.Chart(new float[] { pamiec_zajeta, pamiec_cala - pamiec_zajeta });

            int x = ZakObraz.Image.Width;
            int y = ZakObraz.Image.Height;

            zakodowanyInfo.Text = "\nNazwa: " + nazwa_zakodowany + "\n" + "Rozmiar: (" + x + "x" + y + ")\n";
        }

        private void odkoduj_Click(object sender, EventArgs e)
        {
            if (ZakObraz.Image == null)
            {
                MessageBox.Show("Nie wszystkie pola zostały uzupełnione!!!", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Steganografia odkoduj = new Steganografia(ZakObraz.Image);

            string text = odkoduj.odkoduj_z_obrazu();
            text1.Text = text;

            Bitmap bmp = odkoduj.pobierzObraz();
            OryObraz.Image = bmp;


            toolStripStatusLabel1.Text = "Pomyślnie odkodowano wiadomość z obrazu: " + nazwa_zakodowany;


            // Zapis na dysk
            string odp = MessageBox.Show("Zakończono proces odkodowania.\nZapisać wynik na dysku?", "Zapisywanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question).ToString();
            if (odp == "Yes")
            {
                DialogResult res = saveFileDialog1.ShowDialog(this);
                DialogResult res1 = saveFileDialog2.ShowDialog(this);

                if (res == DialogResult.OK)
                {
                    nazwa_oryginalny = saveFileDialog1.FileName;
                    bmp.Save(nazwa_oryginalny);

                    nazwa_text = saveFileDialog2.FileName;
                    //nazwa_text = nazwa_oryginalny;
                    //string[] podziel_string = nazwa_text.Split('.');
                    //nazwa_text = podziel_string[0] + ".pdf";

                    StreamWriter file;
                    try
                    {
                    //    nazwa_text = saveFileDialog1.FileName;
                        //otwórz plik
                        file = new StreamWriter(nazwa_text);
                        file.Write(text);//zapisz tekst
                        file.Close();
                    }
                    catch
                    {//wystąpił błąd podczas otwierania pliku
                        MessageBox.Show("Nie udało się zapisać pliku: " + saveFileDialog1.FileName, "Błąd zapisu pliku", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else return;

                string[] nazwa;
                nazwa = nazwa_oryginalny.Split('\\');
                nazwa_oryginalny = nazwa[nazwa.Length - 1];

                nazwa = nazwa_text.Split('\\');
                nazwa_text = nazwa[nazwa.Length - 1];
            }
            else
            {
                nazwa_oryginalny = nazwa_zakodowany;

                string[] podziel_string = nazwa_zakodowany.Split('.');
                nazwa_text = podziel_string[0] + ".txt";
            }

            int x = OryObraz.Image.Width;
            int y = OryObraz.Image.Height; 

            obrazInfo.Text = "\nNazwa: " + nazwa_oryginalny + "\n" + "Rozmiar: (" + x + "x" + y + ")\n" + "Ilość wolnego miejsca: " + pamiec_cala;
            tekstInfo.Text = "\nNazwa: " + nazwa_text + "\n" + "Wielkość: " + text1.Text.Length + " [bajty]\n";
  //          toolStripStatusLabel1.Text = "Pomyślnie odkodowano z obrazu: " + "'" + nazwa_zakodowany + "' pliki: " +  "'" + nazwa_oryginalny + "' oraz '" + nazwa_text + "'";
        }

        private void text1_TextChanged(object sender, EventArgs e)
        {
            pamiec_zajeta = text1.Text.Length;
            label1.Text = "Pozostało: " + pamiec_zajeta + "/" + pamiec_cala + "[znaków]";

            if (pamiec_zajeta > pamiec_cala)
            {
                progressBar1.Value = 0;
                label1.Text = "Brak wolnego miejsca!!!";
            }
            else
            {
                if (pamiec_cala!=0)
                progressBar1.Value = pamiec_zajeta * 100 / pamiec_cala;
            }
        }

        private void ustawSposóbKodowanieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ustawienia ustaw = new Ustawienia(R,G,B);
            ustaw.ShowDialog();
            R = ustaw.R;
            G = ustaw.G;
            B = ustaw.B;

            if (OryObraz.Image != null)
            {
                int x, y;
                x = OryObraz.Image.Height;
                y = OryObraz.Image.Width;
                pamiec_cala = (x * y * (R + G + B) - 90) / 8;

                label1.Text = "Pozostało: " + pamiec_zajeta + "/" + pamiec_cala + "[znaków]";

                if (pamiec_zajeta > pamiec_cala)
                {
                    progressBar1.Value = 0;
                    label1.Text = "Brak wolnego miejsca!!!";
                }
                else
                {
                    if (pamiec_cala != 0)
                        progressBar1.Value = pamiec_zajeta * 100 / pamiec_cala;
                }
            }

            toolStripStatusLabel1.Text = "Zmieniono ustawienia kodowania-> R: " + R + " G: " + G + " B: " + B;
        }

        private void wyczyśćWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OryObraz.Image = null;
            ZakObraz.Image = null;
            pamiec_cala = 0;
            text1.Text = "";
            obrazInfo.Text = "";
            tekstInfo.Text = "";
            Wykres.Image = null;
            zakodowanyInfo.Text = "";

            R = 1;
            G = 1;
            B = 1;

            progressBar1.Value = 0;
            toolStripStatusLabel1.Text = "Wyczyszczono wszystkie okna";
            //ResetControls();
            //EnableControls(true);
        }

        private void oProgramieToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

    }
}
