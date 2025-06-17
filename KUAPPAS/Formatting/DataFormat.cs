using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using DTO;
namespace Formatting
{
    public static class DataFormat
    {

        private static string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
        private static string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
        private static string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
        private static string[] ribuan = new string[5] { "", "ribu", "juta", "milyar", "triliyun" };



       private static  string[] bulanpanjang = new string[12] { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };
        public static string DateToDB(string date)
        {
            DateTime dt = Convert.ToDateTime(date);
            return dt.ToString("MMddyyyy");
        }

        public static string GetDBDate(string date)
        {
            return GetDateTime(date).ToShortDateString();
        }

        public static string GetDBDate(object date)
        {
            return GetDateTime(date).ToShortDateString();
        }

        public static string GetDBDate(DateTime date)
        {
            return date.ToShortDateString();
        }
        public static string GetSQLDate(DateTime date)
        {
            return date.ToString("mm/dd/yyyy");
        }

        public static string FormatTanggal(this DateTime d)
        {
            string sValue = "";
            sValue = d.Day.ToString() + " " + bulanpanjang[d.Month - 1] + " " + d.Year.ToString();
            return sValue;

        }
        public static string GetCurrentDate()
        {
            DateTime dt = System.DateTime.Now;
            return dt.ToString("MMddyyyy");
        }

       
        public static string DateToDisp(string date)
        {
            string[] dateString = new string[3];
            DateTime dt = Convert.ToDateTime(date);
            dateString = dt.ToString("dd MMM yyyy").Split(Convert.ToChar(" "));
            return dateString[0] + " " + dateString[1] + ", " + dateString[2];
        }
       
        public static string GetDateFromDBDate(string date)
        {
            string dateReturn = string.Empty;
            if (date.Trim().Length < 8)
                date = "0" + date.Trim();

            string month = date.Substring(0, 2);
            string date1 = date.Substring(2, 2);
            string year = date.Substring(4);
            dateReturn = month + "/" + date1 + "/" + year;

            dateReturn = DateToDisp(dateReturn);
            return dateReturn;
        }

        public static string GetMonth(string date)
        {
            string dateToDB = DateToDB(date);
            return dateToDB.Substring(0, 2);
        }

        public static string GetYear(string date)
        {
            string dateToDB = DateToDB(date);
            return dateToDB.Substring(4, 4);
        }

        public static string GetDate(string date)
        {
            string dateToDB = DateToDB(date);
            return dateToDB.Substring(2, 2);
        }

        #region Core Formatting Metods
        public static bool IsValidDate(string date)
        {
            bool retValue = false;
            DateTime result = new DateTime();
            if (DateTime.TryParse(date, out result))
                retValue = true;

            return retValue;
        }

        public static bool IsValidDate(object date)
        {
            bool retValue = false;
            DateTime result = new DateTime();

            if (date != null)
            {
                if (DateTime.TryParse(date.ToString(), out result))
                    retValue = true;
            }

            return retValue;
        }

        public static DateTime GetDateTime(string date)
        {
            DateTime retValue = new DateTime();
            if (IsValidDate(date))
                retValue = Convert.ToDateTime(date);

            return retValue;
        }

        public static DateTime GetDateTime(object date)
        {
            DateTime retValue = new DateTime();

            if (IsValidDate(date))
                retValue = Convert.ToDateTime(date);
            else
                retValue = new DateTime(2016, 1, 1);

            return retValue;
        }


        public static bool IsNumeric(object value)
        {
            bool retValue = false;

            if (value != null)
                retValue = IsNumeric(value.ToString());

            return retValue;
        }

        public static bool IsNumeric(string value)
        {
            bool retValue = false;
            double result = 0;
            if (value != null)
                retValue = double.TryParse(value, out result);

            return retValue;
        }


        public static bool IsInteger(object value)
        {
            bool retValue = false;

            if (value != null)
                retValue = IsInteger(value.ToString());

            return retValue;
        }

        public static bool IsInteger(string value)
        {
            bool retValue = false;
            int result = 0;
            if (value != null)
                retValue = int.TryParse(value, out result);

            return retValue;
        }

        public static bool IsBoolean(string value)
        {
            bool retValue = false;
            bool result = false;
            if (value != null)
                retValue = Boolean.TryParse(value, out result);

            return retValue;
        }


        public static bool IsBoolean(object value)
        {
            bool retValue = false;

            if (value != null)
                retValue = IsBoolean(value.ToString());

            return retValue;
        }

        public static string GetString(object value)
        {
            try
            {
                string retValue = string.Empty;
                if (value != null)

                    retValue = value.ToString();

                return retValue;
            } catch (Exception ex){
                return "0";
            }
        }


        public static Int16 GetSingle(object value)
        {
            try
            {
                Int16 retValue = 0;

                if (IsBoolean(value))
                {
                    if ((bool)value == true)
                        return 1;
                    else
                        return 0;


                }
                if (IsInteger(value))
                    retValue = Convert.ToInt16(value);

                return retValue;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static Int16 BitToSingle(object value)
        {
            Int16 retValue = 0;

            if (IsBoolean(value) && Convert.ToBoolean(value)==true )
                retValue = 1;

            return retValue;
        }

        public static DateTime GetDate(object value)
        {
            DateTime retValue = new DateTime();
            if (IsValidDate(value))
                retValue = Convert.ToDateTime(value);

            return retValue;
        }

        public static Int16 GetSingle(string value)
        {
            Int16 retValue = 0;

            if (IsInteger(value) && Convert.ToInt32(value) <= Int16.MaxValue)
                retValue = Convert.ToInt16(value);
            return retValue;
        }

        public static int GetInteger(object value)
        {
            int retValue = 0;
            if (value == null)
                return 0;

            if (IsInteger(value))
                retValue = Convert.ToInt32(value);

            return retValue;
        }

        public static string GetKode(int IDUrusan, int IDDinas, int idProgram, int IDKegiatan, int IDRekening, int _jenis, ProfileProgramKegiatan profPrgKrg, ProfileRekening profRek)
        {

            try
            {

                string sUrusan = "";
                if (IDUrusan > 0 && IDUrusan.ToString().Length == 3)
                {
                    sUrusan = IDUrusan.ToString().Substring(0, 1) + "." + IDUrusan.ToString().Substring(1, 2);
                }

                string sDinas = "";

                if (IDDinas > 0 && IDDinas.ToString().Length == 7)
                {
                    sDinas = IDDinas.ToKodeDinas();//.ToString().Substring(0, 1) + "." + IDDinas.ToString().Substring(1, 2) + "." + IDDinas.ToString().Substring(3, 2);
                }
                string sProgram = "";
                if (idProgram > 0 && idProgram.ToString().Length == 5)
                {
                    sProgram = idProgram.ToSimpleKodeProgram();// ToString().Trim().Substring(3, 2);
                }
                else
                {
                    if (_jenis != 3)
                        sProgram = ".00";
                }
                string sKegiatan = "";
                if (IDKegiatan > 0 && IDKegiatan.ToString().Trim().Length == profPrgKrg.LENKEG + IDUrusan.ToString().Length)
                {
                    //sKegiatan = IDKegiatan.ToString().Trim().Substring(5, 3);
                    sKegiatan = IDKegiatan.ToSimpleKodeKegiatan();
                }
                else
                {
                    if (_jenis != 3)
                        sKegiatan = ".000";
                }
                string sRekening = "";
                string s1 = "";
                string s2 = "";
                string s3 = "";
                string s4 = "";
                string s5 = "";

                if (IDRekening > 0 && IDRekening.ToString().Length == profRek.LEN5)
                {
                    s1 = IDRekening.ToString().Substring(0, 1);
                    if (DataFormat.GetInteger(IDRekening.ToString().Substring(1)) > 0)
                    {
                        s2 = IDRekening.ToString().Substring(1, 1);
                    }
                    if (DataFormat.GetInteger(IDRekening.ToString().Substring(2)) > 0)
                    {
                        s3 = IDRekening.ToString().Substring(2, 1);
                    }
                    if (DataFormat.GetInteger(IDRekening.ToString().Substring(3)) > 0)
                    {
                        s4 = IDRekening.ToString().Substring(3, 2);
                    }
                    if (DataFormat.GetInteger(IDRekening.ToString().Substring(5)) > 0)
                    {
                        s5 = IDRekening.ToString().Substring(5, profRek.Kode5);
                    }
                    sRekening = s1;
                    sRekening = sRekening + (s2.Length > 0 ? "." + s2 : "");
                    sRekening = sRekening + (s3.Length > 0 ? "." + s3 : "");
                    sRekening = sRekening + (s4.Length > 0 ? "." + s4 : "");
                    sRekening = sRekening + (s5.Length > 0 ? "." + s5 : "");
                }


                string sRet = "";
                sRet = sUrusan;
                if (sDinas.Length > 0)
                {
                    sRet = sRet + "." + sDinas;
                }
                if (sProgram.Length > 0)
                {
                    sRet = sRet + "." + sProgram;
                }
                if (sKegiatan.Length > 0)
                {
                    sRet = sRet + "." + sKegiatan;
                }
                if (sRekening.Length > 0)
                {
                    sRet = sRet + "." + sRekening;
                }


                return sRet;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public static string GetKode90(int IDUrusan, int IDDinas,int idProgram, int IDKegiatan,long IDSubKegiatan, long IDRekening, int _jenis, ProfileProgramKegiatan profPrgKrg, ProfileRekening profRek)
        {

            try
            {

                string sUrusan = "";
                if (IDUrusan > 0 && IDUrusan.ToString().Length == 3)
                {
                    sUrusan = IDUrusan.ToString().Substring(0, 1) + "." + IDUrusan.ToString().Substring(1, 2);
                }

                string sDinas = "";

                if (IDDinas > 0 && IDDinas.ToString().Length == 7)
                {
                    sDinas = IDDinas.ToKodeDinas ();//.ToString().Substring(0, 1) + "." + IDDinas.ToString().Substring(1, 2) + "." + IDDinas.ToString().Substring(3, 2);
                }
                string sProgram = "";
                if (idProgram > 0 && idProgram.ToString().Length == 5)
                {
                    sProgram = idProgram.ToSimpleKodeProgram();// ToString().Trim().Substring(3, 2);
                } else {
                    if (_jenis != 3)
                        sProgram = ".00"; 
                }
                string sKegiatan = "";
                if (IDKegiatan > 0 && IDKegiatan.ToString().Trim().Length == profPrgKrg.LENKEG + IDUrusan.ToString().Length  )

                {
                    //sKegiatan = IDKegiatan.ToString().Trim().Substring(5, 3);
                    sKegiatan = IDKegiatan.ToSimpleKodeKegiatan();
                }
                else
                {
                    if (_jenis !=  3)
                    sKegiatan = ".000"; 
                }
                string sSubKegiatan = "";
                if (IDSubKegiatan > 0 && IDSubKegiatan.ToString().Trim().Length == 10)
                {
                    //sKegiatan = IDKegiatan.ToString().Trim().Substring(5, 3);
                    sSubKegiatan = IDSubKegiatan.ToString().Substring(8);
                }
                else
                {
                    if (_jenis != 3)
                        sSubKegiatan = ".00";
                }

                string sRekening = "";
                string s1 = "";
                string s2 = "";
                string s3 = "";
                string s4 = "";
                string s5 = "";
                string s6 = "";
             
                if (IDRekening > 0 && IDRekening.ToString().Length == profRek.LEN6)
                {
                   
                    s1 = IDRekening.ToString().Substring(0, 1);

                    if (DataFormat.GetLong(IDRekening.ToString().Substring(1)) > 0)
                    {
                        s2 = IDRekening.ToString().Substring(1, 1);
                    }
                    if (DataFormat.GetInteger(IDRekening.ToString().Substring(2)) > 0)
                    {
                        s3 = IDRekening.ToString().Substring(2, 2);
                    }
                    if (DataFormat.GetInteger(IDRekening.ToString().Substring(4)) > 0)
                    {
                        s4 = IDRekening.ToString().Substring(4, 2);
                    }
                    if (DataFormat.GetInteger(IDRekening.ToString().Substring(6)) > 0)
                    {
                        s5 = IDRekening.ToString().Substring(6, profRek.Kode5);
                    }
                    if (DataFormat.GetInteger(IDRekening.ToString().Substring(8)) > 0)
                    {
                        s6 = IDRekening.ToString().Substring(8);
                    }

                    sRekening = s1;
                    sRekening = sRekening + (s2.Length > 0 ? "." + s2 : "");
                    sRekening = sRekening + (s3.Length > 0 ? "." + s3 : "");
                    sRekening = sRekening + (s4.Length > 0 ? "." + s4 : "");
                    sRekening = sRekening + (s5.Length > 0 ? "." + s5 : "");
                    sRekening = sRekening + (s6.Length > 0 ? "." + s6 : "");
                }

                
                string sRet = "";
                sRet = sUrusan;
                if (sDinas.Length > 0)
                {
                    sRet = sRet + "." + sDinas;
                }
                if (sProgram.Length > 0)
                {
                    sRet = sRet + "." + sProgram;
                }
                if (sKegiatan.Length > 0)
                {
                    sRet = sRet + "." + sKegiatan;
                }
                if (sSubKegiatan.Length > 0)
                {
                    sRet = sRet + "." + sSubKegiatan;
                }
                

                if (sRekening.Length > 0)
                {
                    sRet = sRet + "." + sRekening;
                }

                
                return sRet;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public static long GetLong(object value)
        {
            Int64 retValue = 0;

            if (IsNumeric(value))
                retValue = Convert.ToInt64(value);

            return retValue;
        }
        public static int  ToInteger(string   value)
        {
            return GetInteger(value);
        }

        public static long ToLong(string value)
        {
            return GetLong(value);
        }
        public static long GetLong(string value)
        {
            long retValue = 0;

            bool canConvert = long.TryParse(value, out retValue);

            if (canConvert)
                return retValue;

            return retValue;
        }


        public static int GetInteger(string value)
        {
            int retValue = 0;

            if (IsInteger(value))
                retValue = Convert.ToInt32(value);

            return retValue;
        }

        public static decimal GetDecimal(object value)
        {
            decimal retValue = 0;
            if (value == null)
            {
                return 0;
            }
            if (IsNumeric(value))
            {
                retValue = Convert.ToDecimal(value);
            }
            else
            {
             //   MessageBox.Show(value.ToString());
            }

            //if (IsNumeric(value))
            //retValue= decimal.Parse(Regex.Replace(value.ToString(), @",.*|\D", ""));

            return retValue;
        }
        //public static decimal GetDecimal(object value)
        //{
        //    decimal retValue = 0;

        //    if (IsNumeric(value))
        //    {
        //        retValue = Convert.ToDecimal(value);
        //    }
        //    else
        //    {
        //    }

            
        //    return retValue;
        //}

        public static decimal GetDecimal(string value)
        {
            decimal retValue = 0;
            try
            {
                if (IsNumeric(value))
                {
                    retValue = Convert.ToDecimal(value);
                }
                else
                {
                    retValue = value.UangToDecimal();

                    // MessageBox.Show(value);

                }


                return retValue;
            }
            catch {
                return 0;
            }
        }
        public static string ToSQLFormat(this DateTime value)
        {

            return "'" + value.Month.ToString().Trim() + "/" + value.Day.ToString().Trim() + "/" + value.Year.ToString().Trim() + "'";

        }
        public static string ToSQLFormat2Angka(this DateTime value)
        {
          
                return "'" + value.Month.ToString("00").Trim() + "/" + value.Day.ToString("00").Trim() + "/" + value.Year.ToString().Trim() + "'";

          
        }
        public static double GetDouble(object value)
        {
            double retValue = 0;

            if (IsNumeric(value))
                retValue = Convert.ToDouble(value);

            return retValue;
        }


        public static double GetDouble(string value)
        {
            double retValue = 0;

            if (IsNumeric(value))
                retValue = Convert.ToDouble(value);

            return retValue;
        }

        public static bool GetBoolean(object value)
        {
            bool retValue = false;

            if (IsBoolean(value))
                retValue = Convert.ToBoolean(value);

            return retValue;
        }

        public static bool GetBoolean(string value)
        {
            bool retValue = false;

            if (IsBoolean(value))
                retValue = Convert.ToBoolean(value);

            return retValue;
        }
        public static int ToKodeKategori(this string strKOde)
        {
            if (strKOde.Length >=7)
            {
                return Convert.ToInt32(strKOde.Substring(0, 1));
            }
            else
                return 0;

        }
        public static int ToKodeSKPD(this string strKOde)
        {
            if (strKOde.Length >= 7)
            {
                return Convert.ToInt32(strKOde.Substring(3, 2));
            }
            else
                return 0;

        }
        public static int ToKodeUrusan(this string strKOde)
        {
            if (strKOde.Length >= 7)
            {
                return Convert.ToInt32(strKOde.Substring(1, 2));
            }
            else
                return 0;

        }
        //public static string ToKodeUrusan(this int strKOde)
        //{
        //    string s = strKOde.ToString();
        //    if (s.Length < 3)
        //        return "";
        //    return s.Substring(0, 1) + "." + s.Substring(1, 2);
            
        //}
        public static int  ToKodeKategoriPelaksana (this string strKOde)
        {
            if (strKOde.Length > 1)
            {
                return Convert.ToInt32(strKOde.Substring(0, 1));
            }
            else
                return 0;

        }
        public static int ToKodeUrusanPelaksana(this string strKOde)
        {
            if (strKOde.Length >=3 )
            {
                return Convert.ToInt32(strKOde.Substring(1, 2));
            }
            else
                return 0;

        }
        public static int ToKodeProgram(this string strKOde)
        {
            if (strKOde.Length >= 5)
            {
                return Convert.ToInt32(strKOde.Substring(3, 2));
            }
            else
                return 0;

        }
        public static int ToKodeKegiatan(this string strKOde )
        {
            if (strKOde.Length >= 8)
            {
                return Convert.ToInt32(strKOde.Substring(5, 3));
            }
            else
                return 0;

        }
        public static int ToKodeSubKegiatan(this string strKOde)
        {
            if (strKOde.Length >= 10)
            {
                return Convert.ToInt32(strKOde.Substring(8));
            }
            else
                return 0;

        }




        public static decimal UangToDecimal(this string valUang)
        {
            decimal retValue = 0;
            bool negatif = false;
            //decimal.Parse("$45.00", NumberStyles.Currency, MyNFI);
            try
            {
                if (valUang.Substring(0, 1) == "-")
                {
                    valUang = valUang.Substring(1);
                    negatif = true;
                }
                if (valUang.Trim().Length == 0)
                {
                    valUang = "0";
                }
                if (IsNumeric(valUang))
                {
                    retValue = Convert.ToDecimal(valUang);
                }
                else
                {
                   // string _group = ",";
                    string val = valUang.Replace(".", "");
                    val = val.Replace(",", ".");
                    retValue = decimal.Parse(val, NumberStyles.Currency);
                }
                if (negatif)
                retValue = -1 * retValue;
                else
                    retValue = retValue;
                return retValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;

            }





        }

        public static string FormatUang(this decimal sUang)
        {

            if (sUang == null)
                sUang = 0;
            if (sUang == 0)
                sUang = 0;            

            
              CultureInfo cultureInfo = new CultureInfo("en-US");
              string s = "";            
              return string.Format(cultureInfo, "{0:C}", sUang).Replace("$", "");
        }
        public static string ToRupiahInReport(this decimal value)
        {
           // return "";
            try
            {
                string strVal = "";

                CultureInfo culture = new CultureInfo("id-ID");
                string sNilai = value.ToString().Trim();
                int idxRIbuan = 0;

                sNilai = sNilai.Replace("-", "");
                string pecahan = "00";

                if (sNilai.IndexOf(".") > 0 || sNilai.IndexOf(",") > 0)
                {
                    int pos = sNilai.IndexOf(".") > sNilai.IndexOf(",") ? sNilai.IndexOf(".") : sNilai.IndexOf(",");

                    pecahan = sNilai.Substring(pos + 1);
                    if (pecahan.Length >= 2)
                    {
                        pecahan = pecahan.Substring(0, 2);
                    }
                    else
                    {
                        pecahan = pecahan + "0";
                    }
                    sNilai = sNilai.Substring(0, pos);
                }
                for (int i = sNilai.ToString().Trim().Length - 1; i >= 0; i--)
                {


                    if (idxRIbuan == 3)
                    {
                        strVal = "." + strVal;
                        idxRIbuan = 0;
                    }
                    strVal = sNilai.Substring(i, 1) + strVal;
                    idxRIbuan++;
                }

                if (strVal.Substring(0, 1) == ".")
                    strVal = strVal.Substring(1);
                strVal = strVal + "," + pecahan;

                // strVal = value.ToString("C", culture).Replace("Rp","");
                if (strVal.Substring(0, 1) == ".")
                    strVal = strVal.Substring(1);

                if (strVal.Substring(0, 1) == ".")
                    strVal = strVal.Substring(1);

                if (strVal.Trim().Substring(0, 2) == "-.")
                {
                    strVal = "-" + strVal.Substring(2);
                }
                if (value < 0)
                {

                    strVal = "(" + strVal.Replace("-", "") + ")";

                }
                return strVal;

            } catch(Exception ex){
                return "";

            }
        }
        public static string ToRupiahInReportNoSen(this decimal value)
        {
            string strVal = ""; ;
            CultureInfo culture = new CultureInfo("id-ID");
            string sNilai = value.ToString().Trim();
            int idxRIbuan = 0;


            string pecahan = "00";

            if (sNilai.IndexOf(".") > 0 || sNilai.IndexOf(",") > 0)
            {
                int pos = sNilai.IndexOf(".") > sNilai.IndexOf(",") ? sNilai.IndexOf(".") : sNilai.IndexOf(",");

                pecahan = sNilai.Substring(pos + 1);
                if (pecahan.Length >= 2)
                {
                    pecahan = pecahan.Substring(0, 2);
                }
                else
                {
                    pecahan = pecahan + "0";
                }
                sNilai = sNilai.Substring(0, pos);
            }
            for (int i = sNilai.ToString().Trim().Length - 1; i >= 0; i--)
            {


                if (idxRIbuan == 3)
                {
                    strVal = "." + strVal;
                    idxRIbuan = 0;
                }
                strVal = sNilai.Substring(i, 1) + strVal;
                idxRIbuan++;
            }

            if (strVal.Substring(0, 1) == ".")
                strVal = strVal.Substring(1);

            //strVal = strVal ;

            // strVal = value.ToString("C", culture).Replace("Rp","");
            if (DataFormat.GetInteger(pecahan) > 0)
                strVal = strVal + "," + pecahan;

            if (value < 0)
            {
                strVal = "(" + strVal.Replace("-", "") + ")";

            }
            return strVal;


        }
        public  static string  FormatUang(ref DataGridViewCell _cell)
        {

            decimal s = DataFormat.GetDecimal(_cell.Value);
            CultureInfo cultureInfo = new CultureInfo("en-US");

            _cell.Value= string.Format(cultureInfo, "{0:C}", s).Replace("$", "");
            return string.Format(cultureInfo, "{0:C}", s).Replace("$", "");

          

        }
        public static  string FormatUangKeDecimal(ref DataGridViewCell _cell)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            //CultureInfo cultureInfo = new CultureInfo("id-id");

            string s = DataFormat.GetString(_cell.Value);           


            string comma = System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberGroupSeparator;
            string sent= System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberDecimalSeparator;// NumberGroupSeparator;
            _cell.Value = s.Replace(comma, "");
            return s.Replace(comma, "");
            

        }

        public static string GetMoney(decimal myMoney)
        {
            //CultureInfo cultureInfo = new CultureInfo("en-US");
            //return string.Format(cultureInfo, "{0:C}", myMoney).Replace("$","");

            CultureInfo cultureInfo = new CultureInfo("id-id");
            return string.Format(cultureInfo, "{0:C}", myMoney).Replace("Rp", "");

        }
        public static string GetStringOfMoney(string  sMoney)
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");            
            
            string comma = System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.NumberGroupSeparator;
            return sMoney.Replace(comma,"");           
        }
        private static string FormatUang(DataGridViewCell _cell)
        {
         
            decimal s = DataFormat.GetDecimal(_cell.Value);            
            return GetMoney(s);

        }

        public static decimal FormatUangReportKeDecimal(this string s)
        {
            string strVal = ""; ;
            CultureInfo culture = new CultureInfo("id-ID");
            string sNilai = s;
            int idxRIbuan = 0;
            bool negatif = false;
            if (s.Length == 0)
                return 0;
            if (s.Substring(0, 1) == "(")
            {
               s= s.Replace("(", "");
                s=s.Replace(")", "");

                negatif=true;
            }

            if (s.Trim() == "" || s.Trim() == "0")
            {
                return 0;

            }
            if (s.Length < 4)
            {
                
                strVal = negatif?"-"+s : s;
                return UangToDecimal(strVal);

                
            }


            char a = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            char b = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator );




            if (s.Substring(s.Trim().Length - 3, 1) == b.ToString())
            {
                s = s.Replace(b.ToString(), "#");
                s = s.Replace(a.ToString(), "");
                strVal = s.Replace("#", a.ToString());


            }
            else
            {
                // Jika decimal part sudah sama
                strVal = s.Replace(b.ToString(), "");
                //strVal = strVal.Replace(",", ".");
            }

            strVal = negatif?"-"+strVal : strVal ;

            return UangToDecimal(strVal);

        }

        public static decimal FormatUangReportKeDecimal(this object o)
        {
            try
            {
                string strVal = ""; ;
                CultureInfo culture = new CultureInfo("id-ID");
                string sNilai = GetString(o);
                string s = sNilai;

                int idxRIbuan = 0;
                bool negatif = false;
                if (s.Length == 0)
                    return 0;
                if (s.Length < 4)
                    return UangToDecimal(s);

                if (s.Substring(0, 1) == "(")
                {
                    s = s.Replace("(", "");
                    s = s.Replace(")", "");

                    negatif = true;
                }
                if (s.Trim() == "" || s.Trim() == "0")
                {
                    return 0;

                }

                char a = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                char b = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator);

                if (s.Substring(s.Trim().Length - 3, 1) == b.ToString())
                {
                    s = s.Replace(b.ToString(), "#");
                    s = s.Replace(a.ToString(), "");
                    strVal = s.Replace("#", a.ToString());


                }
                else
                {
                    // Jika decimal part sudah sama
                    strVal = s.Replace(b.ToString(), "");
                    //strVal = strVal.Replace(",", ".");
                }

                strVal = negatif ? "-" + strVal : strVal;

                return UangToDecimal(strVal);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kesalahan format Uang ke Desimal " + o.ToString());
                return 0;

            }
        }


        #endregion

        public static string ToKodeProgram(this int number, bool simple = false )
        {
            string s = number.ToString();
            if (number > 0)
            {
                if (s.Length != 5)
                {
                    return s;
                }
                else
                {
                    string retVal = "";
                    if (simple == false)
                    {
                        retVal = s.Substring(0, 1) + "." + s.Substring(1, 2) + "." + s.Substring(3);
                    }
                    else
                    {
                        retVal = s.Substring(3);
                    }
                    return retVal;
                }
            }
            {
                s = "";
                return s;
            }
            

        }
        public static string ToKodeDinas(this int number)
        {
           
                string s = number.ToString().Trim();
                if (s.Length != 7)
                {
                    return s;
                }
                else
                {
                    //string retVal = "";
                    //retVal = s.Substring(0, 1) + "-" + s.Substring(1, 2) + "-0-0-" + s.Substring(3, 2);
                    //if (s.Substring(5, 2) != "00")
                    //{
                    //    retVal = retVal + "." + s.Substring(5, 2);
                    //}

                    string retVal = "";
                    retVal = s.Substring(0, 1) + "." + s.Substring(1, 2) + "." + s.Substring(3, 2);
                    if (s.Substring(5, 2) != "00")
                    {
                        retVal = retVal + "." + s.Substring(5, 2);
                    }
                    return retVal;
                }
           

        }
        public static string Terbilang(this Decimal d)
        {


            string strHasil = "";
            Decimal frac = d - Decimal.Truncate(d);

            if (Decimal.Compare(frac, 0.0m) != 0)
                strHasil = Terbilang(Decimal.Round(frac * 100)) + " sen";
            else
                strHasil = "rupiah";
            int xDigit = 0;
            int xPosisi = 0;

            string strTemp = Decimal.Truncate(d).ToString();
            for (int i = strTemp.Length; i > 0; i--)
            {
                string tmpx = "";
                xDigit = Convert.ToInt32(strTemp.Substring(i - 1, 1));
                xPosisi = (strTemp.Length - i) + 1;
                switch (xPosisi % 3)
                {
                    case 1:
                        bool allNull = false;
                        if (i == 1)
                            tmpx = satuan[xDigit] + " ";
                        else if (strTemp.Substring(i - 2, 1) == "1")
                            tmpx = belasan[xDigit] + " ";
                        else if (xDigit > 0)
                            tmpx = satuan[xDigit] + " ";
                        else
                        {
                            allNull = true;
                            if (i > 1)
                                if (strTemp.Substring(i - 2, 1) != "0")
                                    allNull = false;
                            if (i > 2)
                                if (strTemp.Substring(i - 3, 1) != "0")
                                    allNull = false;
                            tmpx = "";
                        }

                        if ((!allNull) && (xPosisi > 1))
                            if ((strTemp.Length == 4) && (strTemp.Substring(0, 1) == "1"))
                                tmpx = "se" + ribuan[(int)Decimal.Round(xPosisi / 3m)] + " ";
                            else
                                tmpx = tmpx + ribuan[(int)Decimal.Round(xPosisi / 3)] + " ";
                        strHasil = tmpx + strHasil;
                        break;
                    case 2:
                        if (xDigit > 0)
                            strHasil = puluhan[xDigit] + " " + strHasil;
                        break;
                    case 0:
                        if (xDigit > 0)
                            if (xDigit == 1)
                                strHasil = "seratus " + strHasil;
                            else
                                strHasil = satuan[xDigit] + " ratus " + strHasil;
                        break;
                }
            }
            strHasil = strHasil.Trim().ToLower();
            if (strHasil.Length > 0)
            {
                strHasil = strHasil.Substring(0, 1).ToUpper() +
                  strHasil.Substring(1, strHasil.Length - 1);
            }
            return strHasil;
        }
        public static string ToKodeUrusan(this int number)
        {
            string s = number.ToString().Trim();
            if (s.Length != 3)
            {
                return s;
            }
            else
            {
                string retVal = "";
                if (s.Substring(1, 2) == "00")
                {
                    retVal = s.Substring(0, 1);
                }
                else
                {
                    retVal = s.Substring(0, 1) + "." + s.Substring(1, 2);
                }
                return retVal;
            }
        }

        public static string ToSimpleKodeProgram(this int number)
        {
            string s = number.ToString();
            if (s.Length != 5)
            {
                return s;
            }
            else
            {
                string retVal = "";
                retVal = s.Substring(3, 2);
                return retVal;
            }
        }
        public static string ToSimpleKodeKegiatan(this int number)
        {
            string s = number.ToString();
            if (s.Length <7 )
            {
                return s;
            }
            else
            {
                string retVal = "";
                //retVal = s.Substring(5, 3);
                retVal = s.Substring(5);
                return retVal;
            }
            //return  number.ToString().PadLeft((int)Math.Log10(Math.Abs(x < 0 ? x * 10 : x)) + 1, '0')

        }
        public static string ToSimpleKodeSubKegiatan(this long  number)
        {
            //xxxxxxxxyy
            string s = number.ToString();
            if (s.Length < 10)
            {
                return s;
            }
            else
            {
                string retVal = "";
                //retVal = s.Substring(5, 3);
                retVal = s.Substring(8);
                return retVal;
            }
            //return  number.ToString().PadLeft((int)Math.Log10(Math.Abs(x < 0 ? x * 10 : x)) + 1, '0')

        }

        public static string ToKodeKegiatan(this int number, ProfileProgramKegiatan oProfile)
        {
            string s = number.ToString();
            if (s.Length != 8)
            {
                return s;
            }
            else
            {
                string retVal = "";
                retVal = s.Substring(0, 1) + "." + s.Substring(1, 2) + "." + s.Substring(3, 2) + "." + s.Substring(5, oProfile.KodeKegiatan );
                return retVal;
            }
            //return  number.ToString().PadLeft((int)Math.Log10(Math.Abs(x < 0 ? x * 10 : x)) + 1, '0')

        }
        public static string ToKodeKegiatan(this int number,bool simple = false  )
        {
            string s = number.ToString();
            if (s.Length != 8)
            {
                return s;
            }
            else
            {
                string retVal = "";
                if (simple == false)
                {
                    retVal = s.Substring(0, 1) + "." + s.Substring(1, 2) + "." + s.Substring(3, 2) +
                        "." + s.Substring(5);
                }
                else
                {
                    retVal = s.Substring(5);
                }
                return retVal;
            }
            //return  number.ToString().PadLeft((int)Math.Log10(Math.Abs(x < 0 ? x * 10 : x)) + 1, '0')

        }
        public static string RemoveDigits(string key)
        {
            return Regex.Replace(key, @"\d", "");
        }
        public static string ToKodeSubKegiatan(this long number, bool simple = false )
        {
            string s = number.ToString();
            if (s.Length < 10)
            {
                return s;
            }
            else
            {
                string retVal = "";
                if (simple == false)
                {
                    retVal = s.Substring(0, 1) + "." + s.Substring(1, 2) + "." + s.Substring(3, 2) +
                    "." + s.Substring(5, 3) + "." + s.Substring(8);
                }
                else
                {
                    retVal = s.Substring(8);
                }
                return retVal;
            }
            //return  number.ToString().PadLeft((int)Math.Log10(Math.Abs(x < 0 ? x * 10 : x)) + 1, '0')

        }
        
       public static string IntToStringWithLeftPad(this int number, int totalWidth)
       {
           string lRet = number.ToString().PadLeft(totalWidth, '0');

           return lRet;// number.ToString().PadLeft(totalWidth, '0');
                //return  number.ToString().PadLeft((int)Math.Log10(Math.Abs(x < 0 ? x * 10 : x)) + 1, '0')

       }
       public static string LongToStringWithLeftPad(this long  number, int totalWidth)
       {
           return number.ToString().PadLeft(totalWidth, '0');
           //return  number.ToString().PadLeft((int)Math.Log10(Math.Abs(x < 0 ? x * 10 : x)) + 1, '0')

       }
       public static int KodeKategoriPelaksana(this int idUrusan)
       {
           if (idUrusan.ToString().Length < 1)
               return 0;
           return GetInteger(idUrusan.ToString().Substring(0, 1));

       }
       public static int KodeUrusanPelaksana(this int idUrusan)
       {
           if (idUrusan.ToString().Length < 3)
               return 0;
           return GetInteger(idUrusan.ToString().Substring(1,2));

       }
       public static int KodeUK(this int idDInas)
       {
           if (idDInas.ToString().Length < 7)
               return 0;
           return GetInteger(idDInas.ToString().Substring(5, 2));

       }
       public static int KodeKategori(this int idDInas)
       {
           if (idDInas.ToString().Length < 7)
               return 0;
           return GetInteger(idDInas.ToString().Substring(0, 1));

       }
       public static int KodeUrusan(this int idDInas)
       {
           if (idDInas.ToString().Length < 7)
               return 0;
           return GetInteger(idDInas.ToString().Substring(1, 2));

       }
       public static int KodeSKPD(this int idDInas)
       {
           if (idDInas.ToString().Length < 7)
               return 0;
           return GetInteger(idDInas.ToString().Substring(3, 2));
       }
       public static int KodeProgram(this int idPrg)
       {
           if (idPrg.ToString().Length < 5)
               return 0;
           return GetInteger(idPrg.ToString().Substring(3, 2));
       }
       public static int KodeKegiatan(this int idKeg)
       {
           if (idKeg.ToString().Length < 7)
               return 0;

           return GetInteger(idKeg.ToString().Substring(5));
       }
       public static int KodeKegiatan(this long idKeg)
       {
           if (idKeg.ToString().Length < 7)
               return 0;

           return GetInteger(idKeg.ToString().Substring(5));
       }
 
       public static string ToKodeRekening(this long _pIDRekening, ProfileRekening oProfile)
       {
           try
           {
               string sKode = _pIDRekening.ToString();
               string sRet;
               if (sKode.Length < 2)
               {
                   return "";
               }
               sRet = sKode.Substring(0, oProfile.LEN1 );

               if (sKode.Substring(oProfile.LEN1, oProfile.Kode2) != oProfile.FORMAT2 )
               {
                   sRet = sRet + "." + sKode.Substring(oProfile.LEN1, oProfile.Kode2);// sKode.Substring(1, 1);
                   if (sKode.Substring(oProfile.LEN2, oProfile.Kode3) != oProfile.FORMAT3)
                    
                   {
                       sRet = sRet + "." + sKode.Substring(oProfile.LEN2, oProfile.Kode3);// sKode.Substring(2, 1);
                       if (sKode.Substring(oProfile.LEN3, oProfile.Kode4) != oProfile.FORMAT4)
                       {
                           sRet = sRet + "." + sKode.Substring(oProfile.LEN3, oProfile.Kode4);// sKode.Substring(3, 2);
                           if (sKode.Substring(oProfile.LEN4, oProfile.Kode5) != oProfile.FORMAT5)
                       
                           {
                               sRet = sRet + "." + sKode.Substring(oProfile.LEN4, oProfile.Kode5);// sKode.Substring(3, 2);
                                if (sKode.Substring(oProfile.LEN5, oProfile.Kode6) != oProfile.FORMAT6)
                               {
                                   sRet = sRet + "." + sKode.Substring(oProfile.LEN5, oProfile.Kode6);


                               }
                           }
                       }
                   }
               }
               return sRet;
           }
           catch (Exception ex)
           {
               return _pIDRekening.ToString();
           }
       }
       public static string ToKodeRekening(this long _pIDRekening, int root= 6 )
       {
                   
               string sKode = _pIDRekening.ToString();
               string sRet=sKode;
               string sTambahan = "";

             
              // '1234567890'
               switch (root) 
               {
                   case 1 :
                       sRet = sKode.Substring(0,1);
                       break;
                   case 2:
                       sKode=sKode.Substring(0, 2);
                       sRet = sKode.Insert(1, ".");
                       break;
                   case 3:
                       sKode = sKode.Substring(0, 4);
                       sRet = sKode.Insert(2, ".").Insert(1, ".");
                       break;
                   case 4:
                       sKode = sKode.Substring(0, 6);
                       sRet = sKode.Insert(4, ".").Insert(2, ".").Insert(1, ".");
                       break;
                   case 5:
                       sKode = sKode.Substring(0, 8);
                       sRet = sKode.Insert(6, ".").Insert(4, ".").Insert(2, ".").Insert(1, ".");
                       break;
                   case 6:
                       if (sKode.Length == 7)
                       {
                           sRet = sKode.Insert(5, ".").Insert(3, ".").Insert(2, ".").Insert(1, ".");
                       }
                       else
                       {
                           sRet = sKode.Insert(8, ".").Insert(6, ".").Insert(4, ".").Insert(2, ".").Insert(1, ".");
                       }
                       break;
              }

               
               return sRet;
       }
       
       public static string ToKodeUrusan(this long _pIDUrusan)
       {
           string sRet;
           if (_pIDUrusan > 0)
           {
               string sKode = _pIDUrusan.ToString();
           
               sRet = sKode.Substring(0, 1);
               sRet = sRet + "." + sKode.Substring(1, 2);
           }
           else
           {
               sRet = "";

           }
           return sRet;

       }

       public static string ToKodeDinas(this long _pIDInas , bool pp90 = false)
       {
           try
           {
               string sKode = _pIDInas.ToString();
               string sRet;
                   
               //if (pp90)
               //{
                   //string sKode = _pIDInas.ToString();
                   //string sRet;
                   sRet = sKode.Substring(0, 1); // Kategori           
                   sRet = sRet + "." + sKode.Substring(1, 2);// Urusan
                   sRet = sRet + "." + sKode.Substring(3, 2); // SKPD
                   if (sKode.Substring(5, 2) != "00")
                   {
                       sRet = sRet + "." + sKode.Substring(5, 2); // SKPD
                   }
               //}
               //else
               //{
               //    List<PelaksanaUrusan> lst = new List<PelaksanaUrusan>();
                   
               //}
               return sRet;
           }
           catch (Exception ex)
           {
               
               return _pIDInas.ToString();
    
           }
       }
       public static string MakeSpace(this Single berapaKali)
       {
           string sRet = " ";
           for (int i = 0; i < berapaKali; i++)
           {
               sRet = sRet + "    ";
           }
           return sRet;
       }

       public static string GetProsentase(decimal d1, decimal d2)
       {
           try
           {
               if (d1 == 0)
               {
                   return "0";
               }
               if (d1 == d2)
               {
                   return "100";
               }

               if (d2 == 0)
               {
                   return "100";
               }
               decimal pembilang = d1 - d2;
               if (d1 < 0)
                   d1 = d1;
               //decimal x = pembilang / d2;
               decimal x =  d1/d2;

               if (x < 0)
                   return "(" + (x * 100).ToString("##.##").Replace("-", "") + ")";
               else
                   return (x * 100).ToString("##.##");
               //return (d2 / d1).ToString("##.##");
           }
           catch (Exception ex)
           {
               return "0";
           }
       }
       public static string UpperFirst(string value)
       {
           string retValue = "";
           if (value.Length == 0)
               return "";
           string firstChar = value.Substring(0, 1).ToUpper();
           retValue = firstChar.Trim() + value.Substring(1);


           return retValue;
       }
       public static decimal GetSelisih(decimal pAng, decimal pReal, decimal bAng, decimal bReal)
       {

           return (pReal - pAng - (bReal - bAng));

       }
       //public static int KodeUrusan(int d1)
       //{
       //    if (d1 == 0 || d1< 100)
       //    {
       //        return 0;
       //    }

       //    return GetInteger(d1.ToString().Substring(1,2));


       //}
       //public static int KodeSKPD(int d1)
       //{
       //    if (d1 == 0 || d1 < 10000)
       //    {
       //        return 0;
       //    }

       //    return GetInteger(d1.ToString().Substring(3, 2));


       //}
       public static int KodekategoriPelaksana(int d1)
       {
           if (d1 == 0 || d1 < 100)
           {
               return 0;
           }

           return GetInteger(d1.ToString().Substring(0, 1));


       }
       public static string TampilanSubKegiatan(this long id)
       {
           if (id > 0)
           {
               string s = id.ToString().Insert(8, ".").Insert(5, ".").Insert(3, ".").Insert(1, ".");
               return s;
           }
           else
               return "";

       }
       public static string GetProsentaseRealisasi(decimal d1, decimal d2)
       {
           if (d1 == d2 && d1 > 0)
               return "100";

           if (d2 == 0)
           {
               return "100";
           }
           return ((d1 / d2) * 100).ToString("##.##");
        
       }
       public static  string GetKode90(int ID, int IDurusan, int iddinas, int idProgram, int idKegiatan, long idSubKegiatan, int _level)
       {
           try
           {
               string s;
               if (_level == 1)
               {
                   return ID.ToString();
               }
               if (_level == 2)
               {
                   return IDurusan.ToKodeUrusan();
               }

               if (_level == 3)
               {
                   return IDurusan.ToKodeUrusan() + "." + iddinas.ToKodeDinas();
               }
               //   s = idProgram.ToString().Substring(0, 1) + "." + idProgram.ToString().Substring(1, 2) + "." + idProgram.ToString().Substring(3, 2);

               if (_level == 4)
               {
                   return IDurusan.ToKodeUrusan() + "." + iddinas.ToKodeDinas() + "." + (idProgram % 100).ToString();

               }
               if (_level == 5)
               {
                   return IDurusan.ToKodeUrusan() + "." + iddinas.ToKodeDinas() + "." + (idProgram % 100).ToString() + "." +
                       (idKegiatan % 1000).ToString().Substring(0, 1) + "." + (idKegiatan % 1000).ToString().Substring(1);

               }
               if (_level == 6)
               {
                   return IDurusan.ToKodeUrusan() + "." + iddinas.ToKodeDinas() + "." + (idProgram % 100).ToString() + "." +
                       (idKegiatan % 1000).ToString().Substring(0, 1) + "." + (idKegiatan % 1000).ToString().Substring(1) + "." + (idSubKegiatan % 100).ToString();

               }

               return "";
           }
           catch (Exception ex)
           {
               
               return "";
           }

       }
       public static string ToReference(this int index, int panjang, string fixdepan = "")
       {
           int panjangsudahdigunakan = fixdepan.Length;
           int panjangangkabaru = panjang - panjangsudahdigunakan;
           return (fixdepan + index.ToString().PadLeft(panjangangkabaru, '0')).Trim();



       }
        //"2021-12-22

       public static DateTime ToDate(this string  sDate)
       {
           int year= GetInteger(sDate.Substring(0,4));
           int month= GetInteger(sDate.Substring(5,2));
           int day= GetInteger(sDate.Substring(8,2));
           DateTime retval= new DateTime(year,month,day );
           return retval;

       }
        //public static int KodeUrusanPelaksana(int d1)
       //{
       //    if (d1 == 0 || d1 < 100)
       //    {
       //        return 0;
       //    }

       //    return GetInteger(d1.ToString().Substring(1, 2));
       //}
       //public static int KodeProgram(int d1)
       //{
       //    if (d1 == 0 || d1 < 10000)
       //    {
       //        return 0;
       //    }

       //    return GetInteger(d1.ToString().Substring(3, 2));
       //}
       //public static int KodeKegiatan(long d1)
       //{
       //    if (d1 == 0 || d1 < 10000)
       //    {
       //        return 0;
       //    }

       //    return GetInteger(d1.ToString().Substring(3, 2));
       //}
       //public static int KodeKategori(int d1)
       //{
       //    if (d1 == 0 || d1 < 100)
       //    {
       //        return 0;
       //    }

       //    return GetInteger(d1.ToString().Substring(0,1));


       //}
    //   public static string inputUang(string T ){
    //       int selx;
    //       int TAMBAH;
    //       if (T == "")
    //       {

    //       }

    ////If T.Text = "" Then
    ////ElseIf IsNumeric(T.Text) Then
    ////    If Left(T.Text, 1) = "," Then T.Text = Mid(T.Text, 2)
    ////    selx = T.SelStart
    ////    TAMBAH = Len(T.Text)
        
    ////    T.Text = fMoney(T.Text)
        
    ////    If selx > TAMBAH - 3 Then
    ////        T.Text = fMoney(CCur(T.Text), True)
    ////        TAMBAH = 0
    ////    Else
    ////        TAMBAH = Len(T.Text) - TAMBAH
    ////    End If
    ////    selx = selx + TAMBAH
    ////    If (selx < 0) Then selx = 0
    ////    T.Text = fMoney(T.Text, True)
    ////    T.SelStart = selx
    ////Else
    ////    MsgBox "Input Anda harus berupa angka !", , "Error Input"

    ////    If Len(T) > 0 And T.SelStart > 0 Then
    ////        T.SelStart = T.SelStart - 1
    ////    Else
    ////        T.SelStart = 0
    ////    End If
    ////    T.SelLength = 1
    ////End If
    ////T.SelStart = Len(T.Text)
    //       return "";
    
    //   }


    }
}
