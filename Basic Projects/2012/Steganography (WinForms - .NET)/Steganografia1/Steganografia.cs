using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using System;
using System.Drawing;
//using System.Text;


namespace Steganografia
{
    class Steganografia
    {
        string txt;
        Bitmap bmpWynik;
        byte[] tab_b;
        //bool koniec = false;
        private const int identyfikator = 0x6733DBA7;
        byte m_red;
        byte m_green;
        byte m_blue;
        int Red;
        int Green;
        int Blue;
        byte [] tab_masek = {1,2,4,8,16,32,64,128};
        byte[] zerowanie_maski = { 0xFF, 0xFE, 0xFC, 0xF8, 0xF0, 0xE0, 0xC0, 0x80, 0x00 };
        int size;

        public Steganografia(string txt, Image obraz, int red, int green, int blue)
        {
            this.txt = txt;

            this.m_red = tab_masek[red-1];
            this.m_green = tab_masek[green-1];
            this.m_blue = tab_masek [blue-1];

            this.Red = red;
            this.Green = green;
            this.Blue = blue;

            this.size = txt.Length;
            //Image Oryginal;
            //Oryginal= Image.FromFile(obraz);
            
            if(obraz!=null)
            bmpWynik=new Bitmap(obraz);//utwórz bitmapę taką samą jak oryginał i ją będziemy zmieniać
            //bmpWynik.
        }


        public Steganografia(Image obraz)
        {
            if (obraz != null)
                bmpWynik = new Bitmap(obraz);
        }


        public byte[] textToBits (string text)
        {
            tab_b = new byte[text.Length * 8];
            int mask;
            int litera; 
            int index = 0;
            int txt_length = text.Length;
            int end = 8;

            for (int i = 0; i < txt_length; i++)
            {
                mask = 0x80;
                litera = text[i];
                end = 8;
                //if (litera > 255) { mask = 0x8000; end = 16; } 

                for (int j = 0; j < end; j++)
                {
                    tab_b[index] = (byte)((litera & mask) > 0 ? 1 : 0);
                    index++;
                    mask >>= 1;
                }

            }  
            return tab_b;
        }


        public string bitsToText (byte []tab)
        {
            char litera;
            int index = 0;
            string text;
            byte bajt;
            int tab_length = tab.Length;
            char[] tab_text = new char[tab_length / 8];
            int index_tab_textu = 0;

            for (int i = 0; i < tab_length/8; i++)
            {
                bajt = tab[index];
                index++;

                for (int j = 0; j < 7; j++)
                {
                    bajt <<= 1;
                    bajt |= tab[index];
                    index++;
                }

                litera = (char)bajt;
                tab_text[index_tab_textu] = litera;
                index_tab_textu++;
            }

            text = new string(tab_text);
            //text = Convert.ToString(tab_text);

            return text;
        }


        public byte [] intToBits (int liczba)
        {    
            tab_b = new byte[32];

            uint mask = 0x80000000; // hex= 80 00 00 00


            for (int i = 0; i < 32; i++)
            {
                 tab_b[i] = (byte)((liczba & mask) > 0 ? 1 : 0);
                 mask >>= 1;
            }

            return tab_b;
        }


        public int bitsToInt (byte[] tab)
        {
            int liczba = (int)tab[0];

            for (int i = 1; i < 32; i++)
            {
                liczba <<= 1;
                liczba |= tab[i];
            }

            return liczba;
        }


        public byte[] scal_tablice_bitowe(byte[] tab1, byte[] tab2)
        {
            int rozmiar1= tab1.Length;
            int rozmiar2= tab2.Length;

            byte []tab = new byte[rozmiar1+rozmiar2];
            int index = 0;

            for (int i=0; i < rozmiar1; i++)
            {
                tab[index] = tab1[i];
                index++;
            }

            for (int i = 0; i < rozmiar2; i++)
            {
                tab[index] = tab2[i];
                index++;
            }

            return tab; 
        }


        public byte[] CreateInfoMask()
        {
            byte [] maskaInfo = new byte[11*8+2]; //MaskaInfo: 4 bajty-> identyfikator, 4 bajty-> ilość zakodowanego tekstu (w bajtach), 3 bajty-> maski bitowe dla kolorów + 2 żeby liczba była podzielna prze 3 dzięki temu łatwiej będzie odczytać maske kodując w systemie 3 bity na pixel
            int index = 0; 

            // identyfikator
            tab_b = intToBits (identyfikator);

            for (int i = 0; i < 32; i++)
            {
                maskaInfo[index] = tab_b[i];
                index++;
            }

            // rozmiar tekstu
            int rozmiar= txt.Length;

            tab_b = intToBits (rozmiar);

            for (int i = 0; i < 32; i++)
            {
                maskaInfo[index] = tab_b[i];
                index++;
            }

            // maski bitowe
            int maski = Red; //m_red?

            maski <<= 8;
            maski |= Green; //m_green?

            maski <<= 8;
            maski |= Blue; //m_blue?
            maski <<= 8;

            tab_b = intToBits (maski);

            for (int i = 0; i < 24; i++)
            {
                maskaInfo[index] = tab_b[i];
                index++;
            }

            maskaInfo[index] = 0x01;  // kodujemy dowolnie ostatnie 2 bity aby długość tablicy info była podzielna przez 3
            index++;
            maskaInfo[index] = 0x01;

            return maskaInfo;
        }


        public int[] ReadInfoMask() 
        {
            bool koniec = false;
            int [] maskaInfo = new int[5];
            int index = 0;
            int y = bmpWynik.Height;
            int x = bmpWynik.Width;
            byte R, G, B;
            Color pixel;
            int max_index = 8*11+2;
            byte[] bity = new byte[max_index];

           // int liczba;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    pixel = bmpWynik.GetPixel(j, i);
                    R = pixel.R;
                    G = pixel.G;
                    B = pixel.B;

                    //zerowanie wszystkich bitów oprócz ostatniego
                    R &= 0x01;
                    G &= 0x01;
                    B &= 0x01;

                    //zapisywanie otrzymanych bitow do tablicy bitów
                    bity[index] = R;
                    index++;

                    if (index >= max_index) { koniec = true; break; }


                    //2 bit
                    bity[index] = G;
                    index++;

                    if (index >= max_index) { koniec = true; break; }


                    //3 bit
                    bity[index] = B;
                    index++;

                    if (index >= max_index) { koniec = true; break; }

                }

                if (koniec == true) break;
            }


            // identyfikator
            int index2 = 0;
            tab_b = new byte[32];

            for (int i = 0; i < 32; i++)
            {
                tab_b[i] = bity[index2];
                index2++;
            }

            maskaInfo[0] = bitsToInt(tab_b);

            // rozmiar tekstu
            for (int i = 0; i < 32; i++)
            {
                tab_b[i] = bity[index2];
                index2++;
            }

            maskaInfo[1] = bitsToInt(tab_b);

            // maska R
            for (int i = 0; i < 32; i++)
            {
                if (i < 24) tab_b[i] = 0;
                else
                {
                    tab_b[i] = bity[index2];
                    index2++;
                }
            }

            maskaInfo[2] = bitsToInt(tab_b);

            // maska G
            for (int i = 24; i < 32; i++)
            {   
                    tab_b[i] = bity[index2];
                    index2++;
            }

            maskaInfo[3] = bitsToInt(tab_b);

            // maska B
            for (int i = 24; i < 32; i++)
            {
                tab_b[i] = bity[index2];
                index2++;
            }

            maskaInfo[4] = bitsToInt(tab_b);

            this.Red = tab_b[2]; //m_red
            this.Green = tab_b[3]; //m_green
            this.Blue = tab_b[4]; //m_blue

            this.size = tab_b[1];

            return maskaInfo;
        }


        public Bitmap pobierzObraz()
        {
            return bmpWynik;
        }
        

        public void zakoduj_w_obrazie()
        {
            bool koniec = false;

            byte[] bity1;
            byte[] bity2;
            bity2 = textToBits(txt);
            bity1 = CreateInfoMask();        

            byte[] bity;
            bity = scal_tablice_bitowe(bity1, bity2);

            int y = bmpWynik.Height;
            int x = bmpWynik.Width;
            int [] rozmiarBMP = new int [8];
            rozmiarBMP[0] = y * x * 3;
            Color pixel;
            //int index = 0;
            int index_kontrolny = 0;
            int max_index = bity.Length; 
            int bit= 0x0001;
            int mask;
            byte R,G,B;
            int kontrol = 1;
            int id;

            
            // indeksy
            int [] index = new int [8];
            index[0] =0;
       
            int granica = 1;
            if (Red >= Green && Red >= Blue) granica = Red;
            if (Green >= Red && Green >= Blue) granica = Green;
            if (Blue >= Green && Blue >= Red) granica = Blue;

            for (int k=1; k<=granica; k++)
            //if ((kontrol < Red || kontrol < Green || kontrol < Blue) && index[kontrol - 1] < max_index)
            {
                     index[k] = 0;
                     if (Red >= k) index[k]++;
                     if (Green >= k) index[k]++;
                     if (Blue >= k) index[k]++;
                    // index[k]--;
                     index[k] *= (x * y);
                     index[k] += index[k - 1];
            }
            kontrol =1;
           

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    pixel = bmpWynik.GetPixel(j, i);
                    R = pixel.R;
                    G = pixel.G;
                    B = pixel.B;

                    //zerowanie ostatniego bitu
                    //R &= 0xFE;
                    //G &= 0xFE;
                    //B &= 0xFE;

                    //1 bit
                    for (; ; )   // sprawdzamy ile bitów tej składowej możemy zakodować  
                    {
                        if (kontrol < Red && index[kontrol] < max_index)
                        {
                            kontrol++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    R &= zerowanie_maski[kontrol];
                    //id = index;
                    for (int k = 0; k < kontrol; k++)
                    {
                        mask = (bit & bity[index[k]]);
                        mask <<= k;
                        R |= (byte)mask;
                        index_kontrolny++;
                        index[k]++;
                  //      bit <<= 1;
                    }
                    //index++;
                    kontrol = 1;
                 //   bit = 0x0001;

                    if (index_kontrolny >= max_index) goto dalej;


                    //2 bit
                    for (; ; )   // sprawdzamy ile bitów tej składowej możemy zakodować  
                    {
                        if (kontrol < Green && index[kontrol] < max_index)
                        {
                            kontrol++;       
                        }
                        else
                        {
                            break;
                        }
                    }

                    G &= zerowanie_maski[kontrol];
                    //id = index;
                    for (int k = 0; k < kontrol; k++)
                    {
                        mask = (bit & bity[index[k]]);
                        mask <<= k;
                        G |= (byte)mask;
                        index_kontrolny++;
                        index[k]++;
                      //  id += rozmiarBMP[k];
                    }
                    //index++;
                    kontrol = 1;

                    if (index_kontrolny >= max_index) goto dalej;


                    //3 bit
                    for (; ; )   // sprawdzamy ile bitów tej składowej możemy zakodować  
                    {
                        if (kontrol < Blue && index[kontrol] < max_index)
                        {
                            kontrol++;
                        }
                        else
                        {
                            break;
                        }
                    }

                    B &= zerowanie_maski[kontrol];
                   // id = index;
                    for (int k = 0; k < kontrol; k++)
                    {
                        mask = (bit & bity[index[k]]);
                        mask <<= k;
                        B |= (byte)mask;
                        index_kontrolny++;
                        index[k]++;
                  //      id += rozmiarBMP[k];
                    }
                   // index++;
                    kontrol = 1;


                    dalej:
                    pixel = Color.FromArgb(R, G, B);
                    bmpWynik.SetPixel(j, i, pixel);

                    if (index_kontrolny >= max_index) { koniec = true; break; }
                }

                if (koniec == true) break;
            }
        }


        public string odkoduj_z_obrazu()
        {
                bool koniec = false;
   
                int y = bmpWynik.Height;
                int x = bmpWynik.Width;
                Color pixel;
                //int index = 0;
                int index2 = 0; //indexowanie dla pierwszych 30 pixeli
             //   string text_wynikowy;
                byte R, G, B, R0, G0, B0;
                int licz_pixele = 0;

                int [] maskaInfo = ReadInfoMask();
                int max_index = maskaInfo[1] * 8; // maksymalny indeks tabeli bitów
                Red = maskaInfo[2];
                Green = maskaInfo[3];
                Blue = maskaInfo[4];
                byte[] bity = new byte[max_index];
                //
                int kontrol = 1;
                int[] rozmiarBMP = new int[8];
                rozmiarBMP[0] = y * x * 3 - 90;
                int id;
                int index_kontrolny = 0;
                int bit = 0x0002;
                int mask;
                // + maski
            
                // indeksy
                int[] index = new int[8];
                index[0] = 0;
                index[1] = 3*x*y-90;
             
                int granica = 1;
                if (Red >= Green && Red >= Blue) granica = Red;
                if (Green >= Red && Green >= Blue) granica = Green;
                if (Blue >= Green && Blue >= Red) granica = Blue;

                for (int k = 2; k <= granica; k++)
                //if ((kontrol < Red || kontrol < Green || kontrol < Blue) && index[kontrol - 1] < max_index)
                {
                    index[k] = 0;
                    if (Red >= k) index[k]++;
                    if (Green >= k) index[k]++;
                    if (Blue >= k) index[k]++;
                  //  index[k]--;
                    index[k] *= (x * y);
                    index[k] += index[k - 1];
                }
                kontrol = 1;
           

                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        if (licz_pixele < 30) //30 pierwszych pixeli -> MaskaInfo, tu tylko usuwamy ostanie bity
                        {
                            pixel = bmpWynik.GetPixel(j, i);
                            R = pixel.R;
                            G = pixel.G;
                            B = pixel.B;

                            R0 = pixel.R;
                            G0 = pixel.G;
                            B0 = pixel.B;

                            //zerowanie ostatniego bitu
                            //R0 &= 0xFE; //!
                            //G0 &= 0xFE; //!
                            //B0 &= 0xFE; //!

                            //1 bit
                             for (; ; )   // sprawdzamy ile bitów tej składowej możemy zakodować  
                             {
                              if (kontrol < Red && index[kontrol] < max_index)
                              {
                                  kontrol++;
                              }
                              else
                              {
                                  break;
                              }
                             }

                 
                            R0 &= zerowanie_maski[kontrol];
                            //id = index2 + rozmiarBMP[0];
                            for (int k = 1; k < kontrol; k++)
                            {
                              mask = (bit & R);
                              mask >>= k;
                              bity[index[k]] = (byte)mask;
                              //R |= (byte)mask;
                              index_kontrolny++;
                              index[k]++;
                              //id += rozmiarBMP[k];
                              bit <<= 1;
                             }
                             index2++;
                             kontrol = 1;
                             bit = 0x0002;
                         

                            //2 bit
                             for (; ; )   // sprawdzamy ile bitów tej składowej możemy zakodować  
                             {
                              if (kontrol < Green && index[kontrol] < max_index)
                              {
                                  kontrol++;
                              }
                              else
                              {
                                  break;
                              }
                             }


                            G0 &= zerowanie_maski[kontrol];
                           // id = index2 + rozmiarBMP[0];
                            for (int k = 1; k < kontrol; k++)
                            {
                              mask = (bit & G);
                              mask >>= k;
                              bity[index[k]] = (byte)mask;
                              //R |= (byte)mask;
                              index[k]++;
                              index_kontrolny++;
                              //id += rozmiarBMP[k];
                              bit <<= 1;
                             }
                             index2++;
                             kontrol = 1;
                             bit = 0x0002;

                            //3 bit
                             for (; ; )   // sprawdzamy ile bitów tej składowej możemy zakodować  
                             {
                              if (kontrol < Blue && index[kontrol] < max_index)
                              {
                                  kontrol++;
                              }
                              else
                              {
                                  break;
                              }
                             }

                            B0 &= zerowanie_maski[kontrol];
                           // id = index2 + rozmiarBMP[0];
                            for (int k = 1; k < kontrol; k++)
                            {
                              mask = (bit & B);
                              mask >>= k;
                              bity[index[k]] = (byte)mask;
                              //R |= (byte)mask;
                              index[k]++;
                              index_kontrolny++;
                             // id += rozmiarBMP[k];
                              bit <<= 1;
                             }
                             index2++;
                             kontrol = 1;
                             bit = 0x0002;
                        

                            pixel = Color.FromArgb(R0, G0, B0);
                            bmpWynik.SetPixel(j, i, pixel);

                            licz_pixele++;   //musimy ominąć 30 pierwszych pixeli na których zakodowana jest MaskaInfo    
                            continue;
                        }
                     //   rozmiarBMP[0] = x * y * 3;
                        bit = 0x0001;
                        pixel = bmpWynik.GetPixel(j, i);
                        R = pixel.R;
                        G = pixel.G;
                        B = pixel.B;

                        R0 = R;
                        G0 = G;
                        B0 = B;

                        //zerowanie wszystkich bitów oprócz ostatniego w celu odczytania wiadomości
                        //R &= 0x01;//!
                        //G &= 0x01;//!
                        //B &= 0x01;//!

                        //zerowanie ostatniego bitu
                        //R0 &= 0xFE;
                        //G0 &= 0xFE;
                        //B0 &= 0xFE;

                        //zapisywanie otrzymanych bitow do tablicy bitów
                        //1 bit
                         for (; ; )   // sprawdzamy ile bitów tej składowej możemy zakodować  
                         {
                          if (kontrol < Red && index[kontrol] < max_index)
                          {
                              kontrol++;
                          }
                          else
                          {
                              break;
                          }
                         }

                       
                           R0 &= zerowanie_maski[kontrol];
                          // id = index;
                           for (int k = 0; k < kontrol; k++)
                           {
                             mask = (bit & R);
                             mask >>= k;
                             bity[index[k]] = (byte)mask;
                             //R |= (byte)mask;
                             index[k]++;
                             index_kontrolny++;
                            // id += rozmiarBMP[k];
                             bit <<= 1;
                           }
                           // index++;
                            kontrol = 1;
                            bit = 0x0001;
                        
                        //bity[index] = R;//!
                        //index++;//!

                        if (index_kontrolny >= max_index)  goto dalej;


                        //2 bit
                         for (; ; )   // sprawdzamy ile bitów tej składowej możemy zakodować  
                         {
                          if (kontrol < Green && index[kontrol] < max_index)
                          {
                              kontrol++;
                          }
                          else
                          {
                              break;
                          }
                         }

                        
                           G0 &= zerowanie_maski[kontrol];
                          // id = index;
                           for (int k = 0; k < kontrol; k++)
                           {
                             mask = (bit & G);
                             mask >>= k;
                             bity[index[k]] = (byte)mask;
                             //R |= (byte)mask;
                             index[k]++;
                             index_kontrolny++;
                             //id += rozmiarBMP[k];
                             bit <<= 1;
                           }
                          //  index++;
                            kontrol = 1;
                            bit = 0x0001;
                         
                        //bity[index] = G; //!
                        //index++; //!

                        if (index_kontrolny >= max_index)  goto dalej;


                        //3 bit
                
                         for (; ; )   // sprawdzamy ile bitów tej składowej możemy zakodować  
                         {
                          if (kontrol < Blue && index[kontrol] < max_index)
                          {
                              kontrol++;
                          }
                          else
                          {
                              break;
                          }
                         }
                 

                   
                           B0 &= zerowanie_maski[kontrol];
                          // id = index;
                           for (int k = 0; k < kontrol; k++)
                           {
                             mask = (bit & B);
                             mask >>= k;
                             bity[index[k]] = (byte)mask;
                             //R |= (byte)mask;
                             index[k]++;
                             index_kontrolny++;
                           //  id += rozmiarBMP[k];
                             bit <<= 1;
                           }
                          //  index++;
                            kontrol = 1;
                            bit = 0x0001;
                      
                        //bity[index] = B; //!
                        //index++; //!

                        dalej:
                        pixel = Color.FromArgb(R0, G0, B0);
                        bmpWynik.SetPixel(j, i, pixel);

                        if (index_kontrolny >= max_index) { koniec = true; break; }
                    }

                    if (koniec == true) break;
                }

                txt = bitsToText(bity);
                
                return txt;
        }


    }
}
