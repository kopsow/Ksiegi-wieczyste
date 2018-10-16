using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoIt;

namespace Ksiegi_wieczyste
{
    class KW
    {

        private string path_FF = @"C:\Program Files (x86)\Mozilla Firefox\";
        private string param_FF = "https://przegladarka-ekw.ms.gov.pl/eukw_prz/KsiegiWieczyste/wyszukiwanieKW";
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Ścieżka do przeglądarki FireFox</param>
        /// <param name="param">Strona do uruchomienia</param>
        public int startFF(string dir,string param)
        {
            var iPID = AutoItX.Run("firefox.exe https://przegladarka-ekw.ms.gov.pl/eukw_prz/KsiegiWieczyste/wyszukiwanieKW", dir);

            if (AutoItX.WinWaitActive("EUKW - Prezentacja Księgi Wieczystej - Mozilla Firefox","30") == 1)
            {
                return 1;
            } else
            {
                throw new System.ArgumentException("Przekroczony limit czasu oczekiwania");
            }

        }

        public void wpiszNumerKW(string numer_kw)
        {
            if (AutoItX.WinActivate("EUKW - Prezentacja Księgi Wieczystej - Mozilla Firefox", "") == 1)
            {

            } else
            {
                throw new System.ArgumentException("Nie aktywne okno FF");
            }
        }

       

    }
}
