using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Ksiegi_wieczyste
{
    public class DatabaseClass
    {
        public string server  { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string databse { get; set; }
        public string command_text { get; set; }
        private SqlConnection conn { get; set; }
        private SqlCommand comm  { get; set; }

        public DatabaseClass(string adres_serwera,string nazwa_uzytkownika,string haslo,string baza)
        {
            this.server = adres_serwera;
            this.login = nazwa_uzytkownika;
            this.password = haslo;
            this.databse = baza;
            this.comm = null;
            this.conn = null;
        }

        public bool Polacz()
        {
            conn = new SqlConnection();
            try
            {
                
                conn.ConnectionString = "Server=" + server + ";Database=" + databse + ";User Id=" + login + ";Password=" + password + ";";
                conn.Open();
            }
            catch (Exception)
            {

                throw;
            }

            if (conn.State==System.Data.ConnectionState.Open)
            {
                return true;
                
            } else
            {
                return false;
            }
        }

        public SqlDataReader Pobierz(string comand)
        {
            if (comm == null)
            {
                comm = new SqlCommand();
                comm.Connection = conn;
            }
            comm.CommandText = comand;
            this.command_text = comand;
            SqlDataReader dr = comm.ExecuteReader();

            return dr;
        }

        public SqlDataReader Pobierz()
        {
            
            if (comm == null)
            {
                comm = new SqlCommand();
                comm.Connection = conn;
            }
            
            comm.CommandText = this.command_text;

            SqlDataReader dr = comm.ExecuteReader();

            return dr;
        }

        public void Wstaw(string parametr)
        {
            SqlCommand com_insert = new SqlCommand();
            com_insert.Connection = conn;            
            com_insert.CommandText = "INSERT INTO protokol (numery_dzialki) VALUES (@numery_dzialki) ";
            com_insert.Parameters.Add(new SqlParameter("numery_dzialki", parametr));
            com_insert.ExecuteNonQuery();
        }

        public int AktualizaujNumeryDzialek(string numery_dzialek, string kw)
        {
            SqlCommand comm_update = new SqlCommand();            
            comm_update.Connection = conn;
            comm_update.CommandText = "UPDATE protokol SET numer_dzialki=@numer_dzialki WHERE nazwa_nieruchomosci=@kw";
            comm_update.Parameters.Add(new SqlParameter("numer_dzialki", numery_dzialek));
            comm_update.Parameters.Add(new SqlParameter("kw", kw));
            return  comm_update.ExecuteNonQuery();
        }

        public int AktualizujPolePowierzchni(string pole_dzialek, string kw)
        {
            SqlCommand com_update = new SqlCommand();
            com_update.Connection = conn;
            com_update.CommandText = "UPDATE protokol SET pole_powierzchni=@pole_powierzchni WHERE nazwa_nieruchomosci=@kw";
            com_update.Parameters.Add(new SqlParameter("pole_powierzchni", pole_dzialek));
            com_update.Parameters.Add(new SqlParameter("kw", kw));
            return com_update.ExecuteNonQuery();
        }

        public int AktualizujInformacjeOmapach(string mapy, string kw)
        {
            SqlCommand com_update = new SqlCommand();
            com_update.Connection=conn;
            com_update.CommandText = "UPDATE protokol SET informacje_o_mapach=@mapy WHERE nazwa_nieruchomosci=@kw";
            com_update.Parameters.Add(new SqlParameter("mapy", mapy));
            com_update.Parameters.Add(new SqlParameter("kw", kw));
            return com_update.ExecuteNonQuery();
        }

        public int AktualizujPodstaweWpisu(string podstawa,string kw)
        {
            SqlCommand com_update = new SqlCommand();
            com_update.Connection = conn;
            com_update.CommandText = "UPDATE protokol SET podstawa_wpisu=@podstawa WHERE nazwa_nieruchomosci=@kw";
            com_update.Parameters.Add(new SqlParameter("podstawa", podstawa));
            com_update.Parameters.Add(new SqlParameter("kw", kw));
            return com_update.ExecuteNonQuery();
        }
        
        public int AktualizujRodzajNieruchomosci(string rodzaj,string kw)
        {
            SqlCommand com_update = new SqlCommand();
            com_update.Connection = conn;
            com_update.CommandText = "UPDATE protokol SET rodzaj_nieruchomosci=@rodzaj WHERE nazwa_nieruchomosci=@kw";
            if (rodzaj != null )
            {
                com_update.Parameters.Add(new SqlParameter("rodzaj", rodzaj));
            } else
            {
                com_update.Parameters.Add(new SqlParameter("rodzaj","BŁĄD NULL"));
            }
            
            com_update.Parameters.Add(new SqlParameter("kw", kw));
            return com_update.ExecuteNonQuery();
        }

        public int AktualizujPodtaweUstaleniaDanych(string podstawa,string kw)
        {
            SqlCommand com_update = new SqlCommand();
            com_update.Connection = conn;
            com_update.CommandText = "UPDATE protokol SET podstawa=@podstawa WHERE nazwa_nieruchomosci=@kw";
            com_update.Parameters.Add(new SqlParameter("podstawa", podstawa));
            com_update.Parameters.Add(new SqlParameter("kw", kw));
            return com_update.ExecuteNonQuery();
        }

        public int AktualizujWlasciciela(string wlasciciel,string kw)
        {
            SqlCommand com_update = new SqlCommand();
            com_update.Connection = conn;
            com_update.CommandText = "UPDATE protokol SET wlasciciel=@wlasciciel WHERE nazwa_nieruchomosci=@kw";
            com_update.Parameters.Add(new SqlParameter("wlasciciel", wlasciciel));
            com_update.Parameters.Add(new SqlParameter("kw", kw));
            return com_update.ExecuteNonQuery();
        }
    
    }
}
