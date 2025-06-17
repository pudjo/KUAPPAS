using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
namespace KUAPPAS
{
    public static class Excelutlity
    {

public static void GenerateExcel(DataGridView dataTable, string path)
{
 
 
// create a excel app along side with workbook and worksheet and give a name to it
 Excel.Application excelApp = new Excel.Application();
 Excel.Workbook excelWorkBook = excelApp.Workbooks.Add();
 Excel._Worksheet xlWorksheet = excelWorkBook.Sheets[1];
 Excel.Range xlRange = xlWorksheet.UsedRange;
 
   // add all the columns
  for (int i = 1; i < dataTable.Columns.Count + 1; i++)
  {
    //  excelWorkSheet.Cells[1, i] = dataTable.Columns[i - 1].ColumnName;
  }
  // add all the rows
  for (int j = 0; j < dataTable.Rows.Count; j++)
  {
      for (int k = 0; k < dataTable.Columns.Count; k++)
   {
    //   excelWorkSheet.Cells[j + 2, k + 1] = dataTable.Rows[j].ItemArray[k].ToString();
   }
  }
 }




    }
}
