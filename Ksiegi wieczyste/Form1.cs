using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoIt;
using AutoItX3Lib;
using System.Data.SqlClient;
using HtmlAgilityPack;

namespace Ksiegi_wieczyste
{
    public partial class Form1 : Form
    {
        private string _dzial_III = null;
        private string _dzial_II = null;
        private string _dzial_Isp = null;
        private string _dzial_Io = null;
        
       
       
        public string dzial_III
        {
            get { return _dzial_III; }
            set { _dzial_III = value; }
        }

        public string dzial_II
        {
            get { return _dzial_II; }
            set { _dzial_II = value; }
        }

        public string dzial_Isp
        {
            get { return _dzial_Isp; }
            set { _dzial_Isp = value; }
        }

        public string dzial_Io
        {
            get { return _dzial_Io; }
            set { _dzial_Io = value; }
        }



        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            if(AutoItX.WinActivate("EUKW - Prezentacja Księgi Wieczystej - Mozilla Firefox", "") == 1)
            {
                MessageBox.Show("okno aktywne");
            } else
            {
                DialogResult dialogResult = MessageBox.Show("Czy uruchomić stronę EKW?", "Komunikat", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var iPID = AutoItX.Run(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe https://przegladarka-ekw.ms.gov.pl/eukw_prz/KsiegiWieczyste/wyszukiwanieKW?komunikaty=true&kontakt=true&okienkoSerwisowe=false", "", 1);
                }
            }
        }


        private void wyszukajKsiege(string _kw)
        {
            string[] separator = {@"/" };
            string[] result = _kw.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                if (AutoItX.WinActivate("EUKW - Prezentacja Księgi Wieczystej - Mozilla Firefox", "") == 1)
                {
                    string numer_ksiegi = result[1];
                    string cyfra_kontrolna = result[2];
                    var notification = new System.Windows.Forms.NotifyIcon()
                    {
                        Visible = true,
                        Icon = System.Drawing.SystemIcons.Information,
                        BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
                        BalloonTipTitle = "AutoIT - Ksiei Wieczyste",
                        BalloonTipText = "Trwa pobieranie danych z księgi wieczystej: SR2W/" + numer_ksiegi + '/' + cyfra_kontrolna,
                    };
                    notification.ShowBalloonTip(5000);

                    //Przesuń i wpisz sąd
                    AutoItX.MouseMove(2433, 464, 1);
                    AutoItX.Sleep(500);
                    AutoItX.MouseClick();
                    AutoItX.Send("SR2W");
                    AutoItX.Send("{TAB}");
                    AutoItX.Sleep(500);

                    //Wpisz numer księgi            
                    AutoItX.Send(numer_ksiegi);
                    AutoItX.Send("{TAB}");
                    AutoItX.Sleep(500);

                    //Wpisz cyfrę kontrolną
                    AutoItX.Send(cyfra_kontrolna);
                    AutoItX.Sleep(500);

                    //przesuń nad google captcha
                    AutoItX.MouseMove(2433, 546);
                    AutoItX.Sleep(500);
                    AutoItX.MouseClick();
                    AutoItX.Sleep(4000);

                    //wyszukaj księge
                    AutoItX.MouseMove(2933, 685);
                    AutoItX.Sleep(500);
                    AutoItX.MouseClick();
                    AutoItX.Sleep(6000);

                    //Przeglądanie aktualnej treści KW
                    AutoItX.MouseMove(2229, 797);
                    AutoItX.Sleep(500);
                    AutoItX.MouseClick();
                    AutoItX.Sleep(5000);

                    //Dzial III
                    AutoItX.MouseMove(2512, 219);
                    AutoItX.MouseClick();
                    AutoItX.Sleep(600);
                    AutoItX.Send("^u");
                    AutoItX.Sleep(600);
                    AutoItX.Send("^a");
                    AutoItX.Sleep(500);
                    AutoItX.Send("^c");
                    AutoItX.Send("^{F4}");
                    AutoItX.Sleep(1000);
                    dzial_III = Clipboard.GetText();
                    AutoItX.Sleep(500);

                    //DZIAL II
                    AutoItX.MouseMove(2349, 219);
                    AutoItX.Sleep(500);
                    AutoItX.MouseClick();
                    AutoItX.Sleep(500);
                    AutoItX.Send("^u");
                    AutoItX.Sleep(600);
                    AutoItX.Send("^a");
                    AutoItX.Sleep(500);
                    AutoItX.Send("^c");
                    AutoItX.Send("^{F4}");
                    AutoItX.Sleep(1000);
                    dzial_II = Clipboard.GetText();
                    AutoItX.Sleep(500);

                    //DZIAL I-sp
                    AutoItX.MouseMove(2176, 219);
                    AutoItX.Sleep(500);
                    AutoItX.MouseClick();
                    AutoItX.Sleep(500);
                    AutoItX.Send("^u");
                    AutoItX.Sleep(600);
                    AutoItX.Send("^a");
                    AutoItX.Sleep(500);
                    AutoItX.Send("^c");
                    AutoItX.Send("^{F4}");
                    AutoItX.Sleep(1000);
                    dzial_Isp = Clipboard.GetText();
                    AutoItX.Sleep(500);

                    //DZIAL I-O
                    AutoItX.MouseMove(1970, 219);
                    AutoItX.Sleep(500);
                    AutoItX.MouseClick();
                    AutoItX.Sleep(500);
                    AutoItX.Send("^u");
                    AutoItX.Sleep(600);
                    AutoItX.Send("^a");
                    AutoItX.Sleep(500);
                    AutoItX.Send("^c");
                    AutoItX.Send("^{F4}");
                    AutoItX.Sleep(1000);
                    dzial_Io = Clipboard.GetText();
                    AutoItX.Sleep(500);


                    //Powrót do początku

                    AutoItX.Send("{END}");
                    AutoItX.MouseMove(1970, 946);
                    AutoItX.Sleep(500);
                    AutoItX.MouseClick();
                    AutoItX.Sleep(1500);
                    AutoItX.Send("{END}");
                    AutoItX.Sleep(500);
                    AutoItX.MouseMove(2662, 748);
                    AutoItX.Sleep(500);
                    AutoItX.MouseClick();

                    richTextBox1.AppendText(dzial_III);
                    richTextBox2.AppendText(dzial_II);
                    richTextBox3.AppendText(dzial_Isp);
                    richTextBox4.AppendText(dzial_Io);

                    StringBuilder numerKsiegi = new StringBuilder();
                    numerKsiegi.Append("SR2W/");
                    numerKsiegi.Append(numer_ksiegi);
                    numerKsiegi.Append("/");
                    numerKsiegi.Append(cyfra_kontrolna);

                    dodajKsiegeDoBazy(numerKsiegi.ToString(), dzial_III, dzial_II, dzial_Isp, dzial_Io);

                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Czy uruchomić stronę EKW?", "Komunikat", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var iPID = AutoItX.Run(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe https://przegladarka-ekw.ms.gov.pl/eukw_prz/KsiegiWieczyste/wyszukiwanieKW?komunikaty=true&kontakt=true&okienkoSerwisowe=false", "", 1);
                    }
                }
            }
            catch (Exception x)
            {
                
                throw;
            }
        }

        private enum kolor
        {
            bialy = 15069691,
            tlo = 8554114


        }
        private void sprawdzTlo(kolor kolor_tla)
        {
            int wsp_x = AutoItX.MouseGetPos().X;
            int wsp_y = AutoItX.MouseGetPos().Y;
            int pobrany_kolor_tla = AutoItX.PixelGetColor(wsp_x, wsp_y);

            int licznik = 0;
            while((int)kolor_tla == pobrany_kolor_tla)
            {
                AutoItX.Sleep(700);
                licznik += 1;

                if (licznik >5)
                {
                    licznik = 0;
                    DialogResult dialogResult = MessageBox.Show("Kliknij powrót i wybierz ok", "Błąd", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {


                    } else
                    {
                        this.Close();
                    }
                }
            }
            

        }

        //Wyszukuje przycisk powrót, szuka dopóki nie znajdzie koloru od tla
        private void szukajPowrot()
        {
            int kolorTla = 8554114;
            int x = 1970;
            int y = 946;

            int pobranyKolorTla = AutoItX.PixelGetColor(x, y);
            if (pobranyKolorTla != kolorTla)
            {
                AutoItX.MouseClick();
            }
            else
            {
                for (int i = 0; i < 25; i += 2)
                {
                    AutoItX.MouseMove(x, y -= i);

                    //pozycja startowa
                    int x_prawo = x;
                    int x_lewo = x;

                    //przesuwamy w prawo i lewo
                    for (int j = 0; j < 6; j++)
                    {
                        AutoItX.MouseMove(x_prawo += j, y);
                        int x_aktualna = AutoItX.MouseGetPos().X;
                        int y_aktualna = AutoItX.MouseGetPos().Y;
                        if (kolorTla != AutoItX.PixelGetColor(x_aktualna, y_aktualna))
                        {
                            AutoItX.MouseClick();
                            break;
                        }

                        AutoItX.MouseMove(x_lewo -= j, y);
                        x_aktualna = AutoItX.MouseGetPos().X;
                        y_aktualna = AutoItX.MouseGetPos().Y;

                        if (kolorTla != AutoItX.PixelGetColor(x_aktualna, y_aktualna))
                        {
                            AutoItX.MouseClick();
                            break;
                        }
                    }

                }
            }

        }
        private void PobierzEukw(string[] kw)
        {
            if (AutoItX.WinActivate("EUKW - Prezentacja Księgi Wieczystej - Mozilla Firefox", "") == 1)
            {

                int licznik = 0;
                while(sprawdzZnajdz() != "Znajdź księgę wieczystą po kryteriach ")
                {

                    AutoItX.Sleep(500);
                    licznik += 1;

                    if (licznik==5)
                    {
                        licznik = 0;
                        throw new System.Exception("ZA dlugo");
                    }
                }

                
                string numer_ksiegi = kw[1];
                string cyfra_kontrolna = kw[2];
                var notification = new System.Windows.Forms.NotifyIcon()
                {
                    Visible = true,
                    Icon = System.Drawing.SystemIcons.Information,
                    BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
                    BalloonTipTitle = "AutoIT - Ksiei Wieczyste",
                    BalloonTipText = "Trwa pobieranie danych z księgi wieczystej: SR2W/" + numer_ksiegi + '/' + cyfra_kontrolna,
                };
                notification.ShowBalloonTip(5000);

                //Przesuń i wpisz sąd
                AutoItX.MouseMove(2433, 464, 1);
                AutoItX.Sleep(500);
                AutoItX.MouseClick();
                AutoItX.Send("SR2W");
                AutoItX.Send("{TAB}");
                AutoItX.Sleep(500);

                //Wpisz numer księgi            
                AutoItX.Send(numer_ksiegi);
                AutoItX.Send("{TAB}");
                AutoItX.Sleep(500);

                //Wpisz cyfrę kontrolną
                AutoItX.Send(cyfra_kontrolna);
                AutoItX.Sleep(700);

                //przesuń nad google captcha
                AutoItX.MouseMove(2433, 546);
                AutoItX.Sleep(500);
                AutoItX.MouseClick();
                AutoItX.Sleep(4000);
                int colorPixel=0;
                colorPixel = AutoItX.PixelGetColor(2435, 549);
                while (colorPixel != 40533)
                {
                    colorPixel = AutoItX.PixelGetColor(2435, 549);
                    AutoItX.Sleep(500);
                    licznik += 1;
                    if (licznik == 5)
                    {
                        licznik = 0;
                        DialogResult dialogResult = MessageBox.Show("Rozwiąż captche i wybierz ok", "captha do rozwiazania", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            colorPixel = 40533;

                        }

                    }
                }
               
                //wyszukaj księge
                AutoItX.MouseMove(2933, 685);
                AutoItX.Sleep(500);
                AutoItX.MouseClick();
                AutoItX.Sleep(6000);

                //sprawdzanie czy wczytala sie ksiega
                colorPixel = AutoItX.PixelGetColor(2527, 537);
                while (colorPixel == 16382457)
                {
                    colorPixel = AutoItX.PixelGetColor(2527, 537);
                    AutoItX.Sleep(500);
                    licznik += 1;
                    if (licznik == 15)
                    {
                        licznik = 0;
                        throw new System.Exception("Za dlugo");

                    }
                }

                //wspolrzedne 2643,324 dla aktywnej captchy

                //Sprawdzamy czy KW nie jest zamknięta
                bool kw_zamknieta = CheckingClass.czyKwZamknieta();
                if ( kw_zamknieta== true)
                {
                    goto KsiegaZamknieta;
                }

                //Przeglądanie aktualnej treści KW
                UstawPrzegladanie();
                
                AutoItX.Sleep(500);
                AutoItX.MouseClick();
                AutoItX.Sleep(5000);

                //Czekam na wczytanie księgi
                colorPixel = AutoItX.PixelGetColor(1959,123);
                while (colorPixel != 8554114)
                {
                    colorPixel = AutoItX.PixelGetColor(1959, 123);
                    AutoItX.Sleep(500);
                    licznik += 1;
                    if (licznik == 15)
                    {
                        licznik = 0;
                        throw new System.Exception("Za dlugo na wczytanie księgi");

                    }
                }

                //Dzial III

                ///Sprawdz czy poprawny dzial
                ///
                AutoItX.MouseMove(2512, 219);
                AutoItX.MouseClick();
                AutoItX.Sleep(800);
                while (sprawdzDzial() != "DZIAŁ III - PRAWA, ROSZCZENIA I OGRANICZENIA")
                {
                    AutoItX.Sleep(800);
                    licznik += 1;
                    if (licznik == 15)
                    {
                        licznik = 0;
                        DialogResult dialogResult = MessageBox.Show("Kliknij dzial III i wybierz OK", "DZIAL III problem", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            

                        }

                    }
                }
                
                
                AutoItX.Send("^u");
                AutoItX.Sleep(1000);
                AutoItX.Send("^a");
                AutoItX.Sleep(1000);
                AutoItX.Send("^c");
                AutoItX.Sleep(800);
                AutoItX.Send("^{F4}");
                AutoItX.Sleep(1000);
                dzial_III = Clipboard.GetText();
                AutoItX.Sleep(1500);

               
                //DZIAL II
                AutoItX.MouseMove(2349, 219);
                AutoItX.Sleep(800);
                AutoItX.MouseClick();
                while (sprawdzDzial() != "DZIAŁ II - WŁASNOŚĆ")
                {
                    AutoItX.Sleep(800);
                    licznik += 1;
                    if (licznik == 15)
                    {
                        licznik = 0;
                        DialogResult dialogResult = MessageBox.Show("Kliknij dzial II i wybierz OK", "DZIAL II problem", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {


                        } else
                        {
                            this.Close();
                        }

                    }
                }
                AutoItX.Sleep(800);
                AutoItX.Send("^u");
                AutoItX.Sleep(800);
                AutoItX.Send("^a");
                AutoItX.Sleep(800);
                AutoItX.Send("^c");
                AutoItX.Sleep(800);
                AutoItX.Send("^{F4}");
                AutoItX.Sleep(1000);
                dzial_II = Clipboard.GetText();
                AutoItX.Sleep(1500);

                //DZIAL I-sp
                AutoItX.MouseMove(2176, 219);
                AutoItX.Sleep(800);
                AutoItX.MouseClick();
                AutoItX.Sleep(800);

                while (sprawdzDzial() != "DZIAŁ I-SP - SPIS PRAW ZWIĄZANYCH Z WŁASNOŚCIĄ")
                {
                    AutoItX.Sleep(500);
                    licznik += 1;
                    if (licznik == 15)
                    {
                        licznik = 0;
                        DialogResult dialogResult = MessageBox.Show("Kliknij dzial I-SP i wybierz OK", "DZIAL I problem", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {


                        }
                        else
                        {
                            this.Close();
                        }

                    }
                }
                AutoItX.Send("^u");
                AutoItX.Sleep(800);
                AutoItX.Send("^a");
                AutoItX.Sleep(800);
                AutoItX.Send("^c");
                AutoItX.Sleep(800);
                AutoItX.Send("^{F4}");
                AutoItX.Sleep(1000);
                dzial_Isp = Clipboard.GetText();
                AutoItX.Sleep(1500);

                //DZIAL I-O
               
                AutoItX.MouseMove(1970, 219);
                AutoItX.Sleep(800);
                AutoItX.MouseClick();
                AutoItX.Sleep(800);
                while (sprawdzDzial() != "DZIAŁ I-O - OZNACZENIE NIERUCHOMOŚCI")
                {
                    AutoItX.Sleep(800);
                    licznik += 1;
                    if (licznik == 15)
                    {
                        licznik = 0;

                        DialogResult dialogResult = MessageBox.Show("Kliknij dzial I-O i wybierz OK", "DZIAL I-O problem", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {


                        }
                        else
                        {
                            this.Close();
                        }

                    }
                }
                AutoItX.Send("^u");
                AutoItX.Sleep(800);
                AutoItX.Send("^a");
                AutoItX.Sleep(800);
                AutoItX.Send("^c");
                AutoItX.Sleep(800);
                AutoItX.Send("^c");
                AutoItX.Sleep(800);
                AutoItX.MouseClick();
                AutoItX.Send("^{F4}");
                AutoItX.Sleep(1000);
                dzial_Io = Clipboard.GetText();
                AutoItX.Sleep(1500);


                //Powrót do początku

                AutoItX.Send("{END}");
                AutoItX.MouseMove(1970, 946);

                szukajPowrot();
                /*AutoItX.Sleep(800);
                AutoItX.MouseClick();*/
                AutoItX.Sleep(1500);
                AutoItX.Send("{END}");
                AutoItX.Sleep(800);
                AutoItX.MouseMove(2566,577);
                sprawdzTlo(kolor.bialy);
                AutoItX.MouseMove(2662, 748);
                AutoItX.Sleep(800);
                AutoItX.MouseClick();

                //Powrót do kryteriów
                
                AutoItX.Sleep(1500);

                AutoItX.Send("{END}");
                AutoItX.MouseMove(2661, 747);

                AutoItX.Sleep(500);
               
                AutoItX.MouseClick();

                AutoItX.Sleep(6000);

                

                StringBuilder numerKsiegi = new StringBuilder();
                numerKsiegi.Append("SR2W/");
                numerKsiegi.Append(numer_ksiegi);
                numerKsiegi.Append("/");
                numerKsiegi.Append(cyfra_kontrolna);

                //dodajKsiegeDoBazy(numerKsiegi.ToString(), dzial_III, dzial_II, dzial_Isp, dzial_Io);
                DodajKsiegeDoBazySQL(numerKsiegi.ToString(), dzial_III, dzial_II, dzial_Isp, dzial_Io);

                KsiegaZamknieta:
                if (kw_zamknieta== true)
                {
                    string data_zamkniecia_kw = CheckingClass.DataZamknieciaKsiegi;
                    DodajKsiegeDoBazySQL("SR2W/" + numer_ksiegi + "/" + cyfra_kontrolna, data_zamkniecia_kw, "NULL", "NULL", "NULL");
                }
               

            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Czy uruchomić stronę EKW?", "Komunikat", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    var iPID = AutoItX.Run(@"C:\Program Files (x86)\Mozilla Firefox\firefox.exe https://przegladarka-ekw.ms.gov.pl/eukw_prz/KsiegiWieczyste/wyszukiwanieKW?komunikaty=true&kontakt=true&okienkoSerwisowe=false", "", 1);
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            int licznik = 0;
            while (sprawdzZnajdz() != "Znajdź księgę wieczystą po kryteriach ")
            {
                licznik += 1;

                if (licznik == 5)
                {
                    licznik = 0;
                    throw new System.Exception("Za dlugo");
                    
                }
            }
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=145.239.91.163;Database=polskie_znaki;User Id=SA;Password=koperski82!;";

            try
            {
                conn.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "SELECT distinct(kw) from ponownie_do_pobrania where kw not in ('SR2W/00005985/7')";
               

                SqlDataReader dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    string kw = dr.GetString(0).Replace(" ", string.Empty);
                    string[] numer_kw = kw.Split("/".ToCharArray());
                    PobierzEukw(numer_kw);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                
                conn.Close();
            }


        }
        private void parsujDzial2()
        {
            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(@"c:\Users\Bartek\Desktop\wsad.html");
            var test23 = doc.DocumentNode.SelectSingleNode("//*[@class='csNDBDane']");
            MessageBox.Show(test23.ToString());

        }
        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=192.168.50.150;Database=polskie_znaki;User Id=SA;Password=koperski82!;";

            SqlCommand comm = new SqlCommand();
            comm.CommandText = "SELECT kw,dzial_1o FROM eukw WHERE id not in (34,39,57) ";
            comm.Connection = conn;
            conn.Open();
            SqlDataReader dr = comm.ExecuteReader();
            List<List<string>> wynik = new List<List<string>>();
            while (dr.Read())
            {
                
                wynik.Add(Parsery.dzial_1o(dr.GetString(0), dr.GetString(1)));
            }
            conn.Close();


        }
        protected void dzial3_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText(dzial_III.ToString());
        }

        private void dodajKsiegeDoBazy(string nr_kw, string dz3,string dz2,string dz1sp,string dz1o)
        {
            try
            {
                SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
                csb.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=C:\Users\Bartek\source\repos\Ksiegi wieczyste\Ksiegi wieczyste\KW.mdf;Integrated Security=True";
                SqlConnection conn = new SqlConnection(csb.ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand();
                string select = "SELECT * FROM kw";
                string insert = "INSERT INTO kw (kw,dz_3,dz_2,dz_1sp,dz_1o) VALUES (@nr_kw,@dz_3,@dz_2,@dz_1sp,@dz_1o)";
                comm.Connection = conn;
                comm.CommandText = insert;

                comm.Parameters.AddWithValue("@nr_kw", nr_kw);
                comm.Parameters.AddWithValue("@dz_3", dz3);
                comm.Parameters.AddWithValue("@dz_2", dz2);
                comm.Parameters.AddWithValue("@dz_1sp", dz1sp);
                comm.Parameters.AddWithValue("@dz_1o", dz1o);

                var result = comm.ExecuteNonQuery();

                comm.CommandText = select;
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append(reader.GetInt32(0));
                        sb.Append(reader.GetValue(1));
                        sb.Append(reader.GetValue(3));
                        sb.Append("\n");
                        richTextBox1.AppendText(sb.ToString());
                    }
                }



                conn.Close();
            }
            catch (Exception x)
            {

                MessageBox.Show(x.ToString());
            }
        }

        public void DodajKsiegeDoBazySQL(string nr_kw, string dz3, string dz2, string dz1sp, string dz1o)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=145.239.91.163;Database=polskie_znaki;User Id=SA;Password=koperski82!;";

            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            conn.Open();
            comm.CommandText = "INSERT INTO eukw (kw,dzial_1o,dzial_1sp,dzial2,dzial3) VALUES (@kw,@d1,@d1a,@d2,@d3)";

            SqlParameter[] parametry = {

                new SqlParameter("@kw", nr_kw),
                new SqlParameter("@d1", dz1o),
                new SqlParameter("@d1a", dz1sp),
                new SqlParameter("@d2", dz2),
                new SqlParameter("@d3", dz3)

                };
            foreach (SqlParameter param in parametry)
            {
                comm.Parameters.Add(param);
            }

            try
            {

                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } finally
            {
                conn.Close();
            }
        }

        private string  sprawdzDzial()
        {
            AutoItX.MouseMove(2492, 241);
            AutoItX.MouseClick("LEFT", 2492, 241, 3);
            AutoItX.Send("^c");

            AutoItX.Sleep(400);
            AutoItX.MouseClick("RIGHT");

            AutoItX.Sleep(400);
            AutoItX.MouseClick("LEFT");
            string naglowek = Clipboard.GetText();
            return naglowek;
        }

        private string sprawdzWynik()
        {
            AutoItX.MouseMove(2127, 329);
            AutoItX.MouseClick();
            AutoItX.MouseClick();
            AutoItX.MouseClick();
            AutoItX.Send("^c");
            AutoItX.MouseClick("RIGHT");
            AutoItX.Sleep(500);
            AutoItX.MouseClick("LEFT");
            string naglowek = Clipboard.GetText();

            return naglowek.TrimStart();
        }

        private string sprawdzZnajdz()
        {
            AutoItX.MouseMove(2233, 329);
            AutoItX.MouseClick();
            AutoItX.MouseClick();
            AutoItX.MouseClick();

            AutoItX.Sleep(500);
            AutoItX.Send("^c");
            AutoItX.MouseClick("RIGHT");
            AutoItX.Sleep(500);
            AutoItX.MouseClick("LEFT");
            string naglowek = Clipboard.GetText();

            return naglowek.TrimStart();
        }

        private void UstawPrzegladanie()
        {
            int licznik = 0;
            if (AutoItX.WinActivate("EUKW - Prezentacja Księgi Wieczystej - Mozilla Firefox", "") == 1)
            {
                AutoItX.MouseMove(2210, 606);

                string tekst = "";
                int kolumna = 606;
                while (tekst != @"

Przeglądanie treści księgi wieczystej ")
                {
                    kolumna += 20;
                    AutoItX.MouseMove(2210, kolumna);
                    AutoItX.MouseClick("LEFT", 2210, kolumna, 3);
                    AutoItX.Send("^c");
                    licznik += 1;
                    tekst = Clipboard.GetText();

                    if (licznik == 500)
                    {
                        throw new System.Exception("Za dlugo");
                    }
                }


                AutoItX.MouseMove(2210, kolumna + 70);


            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            
            DatabaseClass db = new DatabaseClass("145.239.91.163", "tomek", "koperski82!", "polskie_znaki");
            if (db.Polacz() == true)
            {
                SqlDataReader dr = db.Pobierz("SELECT  dzial3,kw from eukw where kw ='SR2W/00002950/2'");
                while(dr.Read())
                {
                    //string wynik = Parsery.numery_dzialek(dr.GetString(1),dr.GetString(0));
                    string wynik = Parsery.podstawa_wpisu(dr.GetString(0),dr.GetString(1));
                    richTextBox1.AppendText(wynik.Substring(0,10) + Environment.NewLine);
                }
            }
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'polskie_znakiDataSet.eukw' . Możesz go przenieść lub usunąć.
            
            Timer t1 = new Timer();
            t1.Interval = 50;
            t1.Tick += new EventHandler(timer1_Tick);
            t1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //richTextBox1.Clear();
           // richTextBox1.AppendText("X:"+Cursor.Position.X.ToString() + "Y:"+Cursor.Position.Y.ToString());
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            richTextBox1.AppendText("X:" + Cursor.Position.X.ToString() + "Y:" + Cursor.Position.Y.ToString());
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=192.168.50.150;Database=polskie_znaki;User Id=SA;Password=koperski82!;";

            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            conn.Open();
            comm.Parameters.Add(new SqlParameter("@kw", comboBox1.SelectedValue));
            comm.CommandText = "SELECT dzial3 FROM eukw where kw=@kw";
            string htmlBody = comm.ExecuteScalar().ToString();
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.DocumentText = htmlBody;

            conn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
