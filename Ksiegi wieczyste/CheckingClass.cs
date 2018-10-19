using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoIt;
using AutoItX3Lib;
using HtmlAgilityPack;
using System.IO;


namespace Ksiegi_wieczyste
{
    class CheckingClass
    {
        private static string zamkniecie_ksiegi;

        public static  string DataZamknieciaKsiegi
        {
            get { return zamkniecie_ksiegi; }
           
        }

        public static bool czyKwZamknieta()
        {
            int czas_przerwy = 500;
            string naglowek_wynik_wyszukiwania= "EUKW - Prezentacja Księgi Wieczystej - Mozilla Firefox";
            string naglowek_kod_html = "";
            AutoItX.WinActivate(naglowek_wynik_wyszukiwania);
            if (AutoItX.WinActive(naglowek_wynik_wyszukiwania) == 1)
            {
                System.Drawing.Rectangle win_pos = AutoItX.WinGetPos(naglowek_wynik_wyszukiwania, "");
                AutoItX.MouseMove(win_pos.X + win_pos.Width / 2, win_pos.Y + win_pos.Height / 2);
                AutoItX.Sleep(czas_przerwy);
                AutoItX.Send("^u");
                AutoItX.Sleep(czas_przerwy);
                AutoItX.WinActivate(naglowek_kod_html);
                if (AutoItX.WinWaitActive(naglowek_kod_html, "")==1)
                {
                    AutoItX.Send("^a");
                    AutoItX.Sleep(czas_przerwy);
                    AutoItX.Send("^c");
                    AutoItX.Sleep(czas_przerwy);
                    AutoItX.Send("^{F4}");
                } else
                {
                    throw new System.Exception("Brak otwartego okna z kodem");
                }
            } else
            {
                throw new System.Exception("Brak otwartej zakłądki z Wynikiem wyszukiwania księgi wieczystej");
            }


            string input = System.Windows.Forms.Clipboard.GetText();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//div[@class='left']");
            bool result;

            if (links.Count >0)
            {
                //Pobieram 5 w kolejnoci elemnt powinien zawierać informację o dacie zamknięcia księgi
                string data_zamkniecia_ksiegi = links[4].InnerText;
                
                //Sprawdzamy czy księga jest zamknięnta
                if (data_zamkniecia_ksiegi.Contains("---"))
                {
                    result = false;
                } else
                {
                    zamkniecie_ksiegi = data_zamkniecia_ksiegi;
                    result = true;
                }
            } else
            {
                result = false;
            }
            
            return result;
        }
    }
}
