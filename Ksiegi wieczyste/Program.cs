﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ksiegi_wieczyste
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            /*Application.Run(new MainForm());
            
            NotifyIcon ni = new NotifyIcon();
            
            System.Drawing.Icon icon = new System.Drawing.Icon(@"c:\Users\Bartek\Pictures\Custom-Icon-Design-Pretty-Office-2-Basic-data.ico");
            ni.Icon = icon;*/


        }
    }
}
