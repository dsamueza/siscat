using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

using Excel= Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace sisadoc.Tasks.Utility
{

     public class GeneradorExcel
    {
         public string CrearExcel(string path, string Spath, string mes, int persona, int periodo, int idmes)
         {
             //Reading from a binary Excel file ('97-2003 format; *.xls)
             //IExcelDataReader excelReader2003 = ExcelReaderFactory.CreateBinaryReader(stream);

             //Reading from a OpenXml Excel file (2007 format; *.xlsx)
             string nombreFull = "Reporte Docente_" + mes + ".xlsx";
             if (!Directory.Exists(path)) Directory.CreateDirectory(path);
             string pathFull = path + "\\" + "Reporte Docente_" + mes + ".xlsx";
             //Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

             //if (xlApp == null)
             //{
              
             //}


             //Excel.Workbook xlWorkBook;
             //Excel.Worksheet xlWorkSheet;
             //object misValue = System.Reflection.Missing.Value;
     
             //xlWorkBook = xlApp.Workbooks.Add(misValue);
             //xlWorkSheet = (Excel.Worksheet) xlWorkBook.Worksheets.get_Item(1);
             //xlWorkSheet.Cells[1, 1] = "Sheet 1 content";

             //xlWorkBook.SaveAs("d:\\csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
             //xlWorkBook.Close(true, misValue, misValue);
             //xlApp.Quit();

             //releaseObject(xlWorkSheet);
             //releaseObject(xlWorkBook);
             //releaseObject(xlApp);

             return nombreFull;
         }
         private void releaseObject(object obj)
         {
             try
             {
                 System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                 obj = null;
             }
             catch (Exception ex)
             {
                 obj = null;
               
             }
             finally
             {
                 GC.Collect();
             }
         } 
    }
}
