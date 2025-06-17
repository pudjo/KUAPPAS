using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using DTO;
using System.Net.Http;


namespace KUAPPAS
{
    public static class UIExt
    {
        
        public static int GetID(this ComboBox cb)
        {
            List<ListItemData> items = new List<ListItemData>();

            for (int i = 0; i < cb.Items.Count; i++)
            {
                ListItemData li = (ListItemData)cb.Items[i];
                if (li.ItemText == cb.Text)
                {
                    return li.Itemdata;
                }
            }
            return 0;


        }
        public static string ReplaceUnicode(this string c)
        {
            return Regex.Replace(c, @"[^\u0000-\u007F]+", string.Empty);
            
        }
        public static string ToKodeBarang(this string c)
        {
            string retStr;

            retStr = "";

            if (c == null)
            {
                return "";
            }
            if (c.Length > 9)
            {
                c = c.Replace(".", "");
                string k1 = c.Substring(0, 1);
                string k2 = c.Substring(1, 2);
                string k3 = c.Substring(3, 2);
                string k4 = c.Substring(5, 2);
                string k5 = c.Substring(7);

                retStr = k1;

                if (Convert.ToInt16(k2) > 0)
                {
                    retStr = retStr + "." + k2;
                }
                if (Convert.ToInt16(k3) > 0)
                {
                    retStr = retStr + "." + k3;
                }

                if (Convert.ToInt16(k4) > 0)
                {
                    retStr = retStr + "." + k4;
                }

                if (Convert.ToInt16(k5) > 0)
                {
                    retStr = retStr + "." + k5;
                }
            }
            return retStr;
        }

        public static string ToKodeLokasi(this string c)
        {
            string retStr;
            retStr = "";
            if (c == null)
            {
                return "";
            }


            if (c.Length == 6)
            {
                retStr = c.Substring(0, 1) + "." + c.Substring(1, 2) + "." + c.Substring(3, 3);
            }
            if (c.Length == 9)
            {
                retStr = c.Substring(0, 1) + "." + c.Substring(1, 2) + "." + c.Substring(3, 3) + "." + c.Substring(6, 3);
            }

            return retStr;
        }
        public static string ToKodeRekening(this string c)
        {
            string retStr;
            retStr = "";
            if (c == null)
            {
                return "";
            }
            string k1 = "";
            string k2 = "";
            string k3 = "";
            string k4 = "";
            string k5 = "";
            string k6 = "";

            if (c.Length == 3)
            {
                k1 = c.Substring(0, 1);
                k2 = c.Substring(1, 1);
                k3 = c.Substring(2, 1);
            }

            if (c.Length == 5)
            {
                k1 = c.Substring(0, 1);
                k2 = c.Substring(1, 1);
                k3 = c.Substring(2, 1);
                k4 = c.Substring(3, 2);
            }

            if (c.Length >= 7)
            {
                k1 = c.Substring(0, 1);
                k2 = c.Substring(1, 1);
                k3 = c.Substring(2, 1);
                k4 = c.Substring(3, 2);
                k5 = c.Substring(5);
            }

            retStr = k1 + "." + k2 + "." + k3;
            retStr = retStr + (k4.Length > 0 ? "." + k4 : "");

            retStr = retStr + (k5.Length > 0 ? "." + k5 : "");
            return retStr;
        
        }

        public static string ToKodeRekening(this string c, Single root)
        {
            string retStr;
            retStr = "";
            if (c == null)
            {
                return "";
            }
            string k1 = "";
            string k2 = "";
            string k3 = "";
            string k4 = "";
            string k5 = "";
            string k6 = "";
            if (root == 1)
            {
                k1 = c.Substring(0, 1);
            }
            if (root == 2)
            {
                k1 = c.Substring(0, 1);
                k2 = c.Substring(1, 1);
            }
            if (root == 3)
            {
                k1 = c.Substring(0, 1);
                k2 = c.Substring(1, 1);
                k3 = c.Substring(2, 2);
            }
            if (root == 4)
            {
                k1 = c.Substring(0, 1);
                k2 = c.Substring(1, 1);
                k3 = c.Substring(2, 2);
                k4 = c.Substring(4, 2);
            }
            if (root == 5)
            {
                k1 = c.Substring(0, 1);
                k2 = c.Substring(1, 1);
                k3 = c.Substring(2, 2);
                k4 = c.Substring(4, 2);
                k5 = c.Substring(6, 2);
            }

            if (root >= 6)
            {
                k1 = c.Substring(0, 1);
                k2 = c.Substring(1, 1);
                k3 = c.Substring(2, 2);
                k4 = c.Substring(4, 2);
                k5 = c.Substring(6, 2);
                k6 = c.Substring(8, 4);

            }

            retStr = k1 + "." + k2 + "." + k3;
            retStr = retStr + (k4.Length > 0 ? "." + k4 : "");

            retStr = retStr + (k5.Length > 0 ? "." + k5 : "");
            retStr = retStr + (k6.Length > 0 ? "." + k6 : "");

            return retStr;
        }
        public static bool IsDateTime(this string txtDate)
        {
            DateTime tempDate;

            return DateTime.TryParse(txtDate, out tempDate) ? true : false;
        }
        public static string ToIDBarang(this string c)
        {
            string retStr;
            retStr = "";
            c = c.Replace(".", "");
            //long lID = 0;
            switch (c.Length)
            {
                case 1:
                    retStr = (Convert.ToInt64(c) * 1000000000).ToString();
                    break;
                case 3:
                    retStr = (Convert.ToInt64(c) * 10000000).ToString();
                    break;
                case 5:
                    retStr = (Convert.ToInt64(c) * 100000).ToString();
                    break;
                case 7:
                    retStr = (Convert.ToInt64(c) * 1000).ToString();
                    break;
                case 10:
                    retStr = c;
                    break;

            }
            return retStr;
        }

        public static string ValuEx(this DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                return cell.Value.ToString();
            }
            else
            {
                return "";
            }
        }
        public static string ToStringEx(this DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                return cell.Value.ToString();
            }
            else
            {
                return "";
            }
        }
        public static int ToIntEx(this DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                return Convert.ToInt32(cell.Value.ToString());
            }
            else
            {
                return 0;
            }
        }
        public static int ToSingleEx(this DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                return Convert.ToInt16(cell.Value.ToString());
            }
            else
            {
                return 0;
            }
        }
        public static long ToLongEx(this DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                return Convert.ToInt64(cell.Value.ToString());
            }
            else
            {
                return 0;
            }
        }
        public static decimal ToDecimalEx(this DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                return Convert.ToDecimal(cell.Value.ToString().Replace(".", ""));
            }
            else
            {
                return 0;
            }
        }
        public static double ToDoublelEx(this DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                return Convert.ToDouble(cell.Value.ToString());
            }
            else
            {
                return 0;
            }
        }
        public static DateTime ToDateEx(this DataGridViewCell cell)
        {
            if (cell.Value != null)
            {
                return Convert.ToDateTime(cell.Value.ToString());
            }
            else
            {
                return new DateTime(1950, 1, 1);
            }
        }
        public static string ToMoney(this decimal value)
        {
            return value.ToString("C").Replace("$", "").Replace("Rp", "");
        }
        
        public static void FormatGridView(this DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            
            foreach (DataGridViewColumn col in dgv.Columns)
            {

              //  col.HeaderCell.Style.BackColor = Color.LightSkyBlue;// DarkBlue;// LightSteelBlue;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
               // col.HeaderCell.Style.Font = new Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            }

        }

        public static void FormatHeader(this DataGridView gV, bool bresize = true )
        {
            gV.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            gV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gV.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSeaGreen ;
          
            gV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gV.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            gV.BorderStyle = BorderStyle.FixedSingle ;
            gV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            gV.ColumnHeadersHeight = 30;
           // gV.Left = 0;

            //if (bresize == true)
            //{
               // gV.Width = gV.Parent.Width;
                gV.EnableHeadersVisualStyles = false;
           // }
            foreach (DataGridViewColumn col in gV.Columns)
            {

                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }
        public static void FormatHeader(this TreeGridView tgV, bool bresize = true)
        {
            tgV.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            tgV.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            tgV.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;

            tgV.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tgV.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            tgV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            tgV.BorderStyle = BorderStyle.FixedSingle;
            tgV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            tgV.ColumnHeadersHeight = 40;
            // gV.Left = 0;

            //if (bresize == true)
            //{
            // gV.Width = gV.Parent.Width;
            tgV.EnableHeadersVisualStyles = false;
            // }
            foreach (DataGridViewColumn col in tgV.Columns)
            {

                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            }
        }
        public static void Expand(this Panel panel, int minHeight,int maxHeight)
        {
            minHeight = 0;
            int h = GetPanelHeight(panel);

            if (panel.Tag== "unexpanded"){
                for (int x = 0; x< h-minHeight; x++ ){
                    panel.Height = x;
                }
                panel.Tag = "expanded";
            }
            else
            {
                for (int x = h; x > minHeight; x--)
                {
                    panel.Height = x;
                }
                panel.Tag = "unexpanded";

            }
            
        }
        private static int GetPanelHeight(Panel panel)
        {
            int Heigt=0;
            foreach (Button btn in panel.Controls)
            {

                Heigt = Heigt + btn.Height;

            }
            return Heigt;


        }

        // PasteInData pastes clipboard data into the grid passed to it.
        public static void PasteInData(this DataGridView dgv, DataGridViewRow rowToCopied =null)
        {
            char[] rowSplitter = { '\n', '\r' };  // Cr and Lf.
            char columnSplitter = '\t';         // Tab.

            IDataObject dataInClipboard = Clipboard.GetDataObject();

            string stringInClipboard =
                dataInClipboard.GetData(DataFormats.Text).ToString();

            string[] rowsInClipboard = stringInClipboard.Split(rowSplitter,
                StringSplitOptions.RemoveEmptyEntries);

            int r = dgv.SelectedCells[0].RowIndex;
            int c = dgv.SelectedCells[0].ColumnIndex;

            if (dgv.Rows.Count < (r + rowsInClipboard.Length))                
                dgv.Rows.Add(r + rowsInClipboard.Length - dgv.Rows.Count);

            // Loop through lines:

            int iRow = 0;
            while (iRow < rowsInClipboard.Length)
            {
                // Split up rows to get individual cells:

                string[] valuesInRow =
                    rowsInClipboard[iRow].Split(columnSplitter);

                // Cycle through cells.
                // Assign cell value only if within columns of grid:

                int jCol = 0;
                while (jCol < valuesInRow.Length)
                {
                    if ((dgv.ColumnCount - 1) >= (c + jCol))
                        dgv.Rows[r + iRow].Cells[c + jCol].Value =
                        valuesInRow[jCol];

                    jCol += 1;
                } // end while

                iRow += 1;
            } // end while
        } // PasteInData
        public static IEnumerable<TreeNode> FlattenTree(this TreeView tv)
        {
            return FlattenTree(tv.Nodes);
        }
        public static RemoteConnection Decrypt (this  RemoteConnection rc){
           // RemoteConnection retRCon = new RemoteConnection();
            if (rc != null)
            {
                rc.UserID = AesOperation.DecryptString(GlobalVar.Key, rc.UserID);
                rc.Password = AesOperation.DecryptString(GlobalVar.Key, rc.Password);
            }
            //else
            //{
            //    rc.UserID = "";
            //    rc.Password = "";


            //}
            return rc;
        
          }
        public static IEnumerable<TreeNode> FlattenTree(this TreeNodeCollection coll)
        {
            return coll.Cast<TreeNode>()
                        .Concat(coll.Cast<TreeNode>()
                                    .SelectMany(x => FlattenTree(x.Nodes)));
        }
    }
}
