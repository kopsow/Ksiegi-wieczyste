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

        public static bool czyKwZamknieta(string html)
        {
            string input = html;
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(input);
            HtmlNodeCollection links = doc.DocumentNode.SelectNodes("//div[@class='left']");
            bool result;

            if (links.Count >0)
            {
                string data_zamkniecia_ksiegi = links[4].InnerText;
                
                //Sprawdzamy czy księga jest zamknięnta
                if (data_zamkniecia_ksiegi.Contains("---"))
                {
                    result = false;
                } else
                {
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
