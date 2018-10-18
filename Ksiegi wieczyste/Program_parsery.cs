using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AutoIt;
using HtmlAgilityPack;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Text.RegularExpressions;

namespace htmlPARSE
{
    class Program
    {
        static void dzial2(string result)
        {
            Console.WriteLine("DZIAŁ 2");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            try
            {
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//td[@class='csNDBDane']");
                
                var list = links.ToList();
                foreach (var node in list)
                {
                    node.Attributes.RemoveAll();
                    string[] test = node.InnerHtml.Split(new[] { Environment.NewLine },StringSplitOptions.None);
                    //Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(Regex.Replace(test[0], "<.*?>", String.Empty));
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("-------------");
                }
            }
            catch (Exception)
            {

                Console.WriteLine("ERROR");
            }
        }

        static void powierzchnia(string input)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            try
            {
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//td[@class='csBDane' and @width='45%' and @colspan='45' ]");
                //Console.WriteLine("znaleziono: " + links.Count);
                //Console.WriteLine(links[links.Count-1].InnerHtml);
                foreach (var item in links)
                {
                    if (item.InnerHtml.Contains("HA"))
                    {
                        Console.WriteLine(item.InnerHtml);
                    }
                }
            }
            catch (Exception)
            {
               
                Console.WriteLine("błąd");
            }
        }

        static void listaDzialek(string input,string kw)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            try
            {
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis' and @cellspacing='0' and @width='100%']");
                HtmlNodeCollection rows =  links[2].SelectNodes("td[@class='csBDane' and @width='45%' and @colspan='45' ]");
                foreach(var item in rows)
                {
                    
                    Console.WriteLine("Działka: "+item.InnerHtml);
                   
                }
            }
            catch (Exception)
            {
               
                throw;
            }
        }
        static void dzial1o(string result)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);
            try
            {
                Console.WriteLine("DZIAŁ 1-o");
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis']");
                var res = links[5].SelectNodes("//td[@class='csNDBDane']//b");
                var dane = links[5].SelectNodes("//td[@class='csDane']");
                Console.
                   BackgroundColor = ConsoleColor.Yellow;
                Console.WriteLine(res[4].InnerHtml);

                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(dane[88].FirstChild.InnerHtml);
                var test = dane[88];
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("-------------");
            }
            catch (Exception)
            {

                Console.WriteLine("błąd");
            }

        }
        static void Main2(string[] args)
        {

            
            SqlConnection conn = new SqlConnection();
            //Console.Write("Podaj Hasło:");

            string password = "koperski82!";
            while (false)
            {
                var key = System.Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                password += key.KeyChar;
            }

            conn.ConnectionString = "Server=172.16.30.251;Database=Siemkowice;User Id=SA;Password ="+password+"; ";

            try
            {
                conn.Open();
            }
            catch (Exception)
            {

                Console.WriteLine("Błąd połączenia z bazą");
            }
            //Console.Clear();
            //Console.WriteLine(conn.State);
            SqlCommand com = new SqlCommand("SELECT dzial_1o,kw FROM eukw where dzial_1o !='DZIAŁ I-O - OZNACZENIE NIERUCHOMOŚCI'", conn);
            SqlDataReader result = com.ExecuteReader();
            Console.ForegroundColor = ConsoleColor.Blue;
            
            while(result.Read())
            {

                //Console.ForegroundColor = ConsoleColor.Black;
                //powierzchnia(result.GetString(0));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.GetString(1));
                Console.ForegroundColor = ConsoleColor.Yellow;
                listaDzialek(result.GetString(0),result.GetString(1));
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Powierzchnia: ");
                powierzchnia(result.GetString(0));
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("--------------");
            }
            //
            // 
           
            conn.Close();
            //Console.WriteLine(conn.State);
            Console.ReadKey(); 
        }
    }
}
