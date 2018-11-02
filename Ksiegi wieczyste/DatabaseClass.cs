using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Ksiegi_wieczyste
{
    class DatabaseClass
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
    }
}
