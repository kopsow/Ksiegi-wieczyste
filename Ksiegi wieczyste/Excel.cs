using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using OfficeOpenXml;


namespace Ksiegi_wieczyste
{
    class Excel
    {

        public static  void test()
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Worksheet1");
                excel.Workbook.Worksheets.Add("Worksheet2");
                excel.Workbook.Worksheets.Add("Worksheet3");

                FileInfo excelFile = new FileInfo(@"D:\test.xlsx");
                excel.SaveAs(excelFile);
            }


        }
       
    }
}
