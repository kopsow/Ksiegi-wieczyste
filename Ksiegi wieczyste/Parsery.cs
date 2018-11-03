using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Ksiegi_wieczyste
{
    class Parsery
    {
        
        public static string numery_dzialek(string input,string kw)
        {
            HtmlDocument doc = new HtmlDocument();
            List<string> lista_dzialek = new List<string>();

            doc.LoadHtml(input);
            try
            {
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis' and @cellspacing='0' and @width='100%']");
                HtmlNodeCollection rows = links[2].SelectNodes("td[@class='csBDane' and @width='45%' and @colspan='45' ]");

                //Sprawdzam czy w ogóle istnieją numery działek
                if (rows != null)
                {
                    foreach (var item in rows)
                    {
                        lista_dzialek.Add(item.InnerHtml);
                    }
                } else
                {
                    lista_dzialek.Add("BRAK NUMERÓW DZIAŁEK");
                }
               
            }
            catch (Exception)
            {

                throw new System.Exception("Bład");
            }
            return String.Join(",", lista_dzialek.ToArray()); 
        }

        public static string powierzchnia(string input)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            string powierzchnia = "BRAK";
            
            try
            {
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//td[@class='csBDane' and @width='45%' and @colspan='45' ]");

                if (links != null)
                {
                    foreach (var item in links)
                    {
                        if (item.InnerHtml.Contains("HA"))
                        {
                            powierzchnia = item.InnerHtml;
                        }
                    }
                } else
                {
                    powierzchnia = "BRAK";
                }
                
            }
            catch (Exception)
            {

                throw new System.Exception("Błąd podczas pobierania powierzchni");
            }
            return powierzchnia;
        }

        public static string rodzaj_nieruchomosci(string input)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            string wynik = null;
            try
            {
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis']");
                HtmlNodeCollection rows = links[2].SelectNodes("td[@class='csDane' and @width='45%' and @colspan='45']");
                if (rows.Count >= 3 && rows[1].InnerText == "Sposób korzystania")
                {
                    wynik = rows[2].InnerText;
                } else if (rows.Count >= 4 && rows[3].InnerText == "Sposób korzystania")
                {
                    wynik = rows[4].InnerText;
                } else
                {
                    if (rows.Count == 2)
                    {
                        rows = links[4].SelectNodes("td[@class='csDane' and @width='45%' and @colspan='45']");
                        return rows[1].InnerText;
                    }
                    else if (rows.Count == 3)
                    {
                        if (rows[1].InnerText == "Sposób korzystania")
                        {
                            wynik = rows[2].InnerText;
                        }
                        else
                        {
                            rows = links[4].SelectNodes("td[@class='csDane' and @width='45%' and @colspan='45']");
                            return rows[1].InnerText;
                        }
                    }
                    else if (rows.Count > 3)
                    {
                        rows = links[4].SelectNodes("td[@class='csDane' and @width='45%' and @colspan='45']");
                        return rows[1].InnerText;
                    }
                }

                
            }
            catch (Exception ex)
            {
                wynik = "BŁĄD";
                return "BŁĄD";
                
            }

            return wynik;
        }

        public static string informacje_o_mapach(string input)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);

            string[] result = new string[10];
            List<string> wynik = new List<string>();
            List<string> ostatnie_dwa = new List<string>();
            try
            {
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis']");
                var informacje_o_mapach_1 = links[links.Count - 1].SelectNodes("//td[@class='csNDBDane']//b");

                foreach (HtmlNode item in informacje_o_mapach_1)
                {
                    wynik.Add(Regex.Replace(item.InnerHtml, "<.*?>", String.Empty));
                }

                if (wynik.Count > 2)
                {
                    var test = wynik.Reverse<string>().Take(2);
                    foreach (string s in test)
                    {
                        ostatnie_dwa.Add(s);
                    }

                    return String.Join(",", ostatnie_dwa.ToArray());
                }
                else
                {

                    return String.Join(",", wynik.ToArray());
                }



            }
            catch (Exception ex)
            {
                wynik.Add(ex.ToString());
                return String.Join(",", wynik.ToArray());
            }

        }

        public static string wlasciel(string input)
        {
            List<string> wynik = new List<string>();

            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(input);
           // doc.Load(@"D:\dzial3.html");
            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis' and @cellspacing='0' and @width='100%']");
            HtmlNodeCollection rows = links[2].SelectNodes("td[@class='csDane' and @width='45%' and @colspan='45' ]");

           if (rows !=null)
            {

                for (int i=1;i<rows.Count;i+=2)
                {
                    wynik.Add(rows[i].InnerText);
                }
                /*switch (rows.Count)
                {
                    case 2:
                        wynik.Add(rows[1].InnerText);
                        break;
                    case 4:
                        wynik.Add(rows[1].InnerText);
                        wynik.Add(rows[3].InnerText);
                        break;
                    case 6:
                        wynik.Add(rows[1].InnerText);
                        wynik.Add(rows[3].InnerText);
                        wynik.Add(rows[5].InnerText);
                        break;
                    case 8:
                        wynik.Add(rows[1].InnerText);
                        wynik.Add(rows[3].InnerText);
                        wynik.Add(rows[5].InnerText);
                        wynik.Add(rows[7].InnerText);
                        break;
                    case 10:
                        wynik.Add(rows[1].InnerText);
                        wynik.Add(rows[3].InnerText);
                        wynik.Add(rows[5].InnerText);
                        wynik.Add(rows[7].InnerText);
                        wynik.Add(rows[9].InnerText);
                        break;
                    default:
                        wynik.Add("BŁAD SWITCH");
                        break;
                }*/
            } else
            {
                wynik.Add("BŁAD NULL");
            }
           
            

            return String.Join(",", wynik.ToArray());
        }

        public static string podstawa_ustalenia_danych(string input)
        {
            List<string> wynik = new List<string>();

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis' and @cellspacing='0' and @width='100%']");
            HtmlNodeCollection rows = links[links.Count - 1].SelectNodes("td[@class='csNDBDane' and @width='90%' and @colspan='90' ]/b");
            

            //podstawa ustalenia danych
            
            return rows[rows.Count-1].InnerText;
        }
        public static string podstawa_wpisu(string input,string kw)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            List<string> podstawa_wpisu = new List<string>();
            HtmlNodeCollection lista_wpisow = doc.DocumentNode.SelectNodes("//*[contains(., 'BRAK WPISÓW')]");
            HtmlNodeCollection ostrzenie = doc.DocumentNode.SelectNodes("//*[contains(., 'OSTRZEŻENIE')]");


            if ( lista_wpisow == null && ostrzenie == null)
            {
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis' and @cellspacing='0' and @width='100%']");
                //HtmlNodeCollection rows = links[1].SelectNodes("td[@class='csBDane' and @width='45%' and @colspan='45' ]");
                HtmlNodeCollection rows = links[1].SelectNodes("td[@class='csBDane' and @width='45%' and @colspan='45' ]");
                if (rows.Count == 1)
                {
                    return "SAMA MIGRACJA";
                } else if (rows.Count == 2)
                {
                    HtmlNodeCollection rows2 = links[1].SelectNodes("td[@class='csDane' and @width='45%' and @colspan='45' ]");
                    podstawa_wpisu.Add(rows[1].InnerText);
                    podstawa_wpisu.Add(rows2[3].InnerText);

                    if (rows2.Count == 5)
                    {
                        podstawa_wpisu.Add(rows2[4].InnerText);
                    }
                    
                    if (rows2.Count ==6)
                    {
                        podstawa_wpisu.Add(rows2[5].InnerText);
                    }
                    return String.Join(" ", podstawa_wpisu.ToArray()); 
                }
                else 
                {
                    HtmlNodeCollection rows2 = links[1].SelectNodes("td[@class='csDane' and @width='45%' and @colspan='45' ]");
                    podstawa_wpisu.Add(rows[3].InnerText);
                    podstawa_wpisu.Add(rows2[3].InnerText);
                    podstawa_wpisu.Add(rows2[4].InnerText);
                    return String.Join(" ", podstawa_wpisu.ToArray()); 
                }
            } else if (ostrzenie!=null)
            {

                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis' and @cellspacing='0' and @width='100%']");
                HtmlNodeCollection csDane = links[1].SelectNodes("td[@class='csDane' and @width='45%' and @colspan='45' ]");
                return csDane[3].InnerText;
            }
            else
            {
                return "BRAK WPISÓW";
            }
              

           

        }
       

    }
}
