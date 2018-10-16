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
        public static List<string> dzial_1o(string kw, string input)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);

            string[] result = new string[10];
            List<string> wynik = new List<string>();
            List<string> ostatnie_dwa = new List<string>();
            wynik.Add(kw);
            ostatnie_dwa.Add(kw);
            try
            {
                HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//table[@class='tbOdpis']");
                var informacje_o_mapach_1 = links[links.Count - 1].SelectNodes("//td[@class='csNDBDane']//b");
              
                foreach (HtmlNode item in informacje_o_mapach_1)
                {
                    wynik.Add(Regex.Replace(item.InnerHtml, "<.*?>", String.Empty));
                }

                if (wynik.Count >2)
                {
                    var test = wynik.Reverse<string>().Take(2);
                    foreach (string s in test)
                    {
                        ostatnie_dwa.Add(s);
                    }

                    return ostatnie_dwa;
                } else
                {
                    return wynik;
                }
               
                // var dane = links[links.Count - 1].SelectNodes("//td[@class='csDane']");
                //string wynik = Regex.Replace(res[res.Count - 1].InnerHtml, "<.*?>", String.Empty);

            }
            catch (Exception ex)
            {
                wynik.Add(ex.ToString());
                return wynik;
            }

        }


    }
}
