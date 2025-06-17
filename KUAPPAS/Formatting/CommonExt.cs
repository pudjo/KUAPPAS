using System;
using System.IO;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Formatting
{
    public static class CommonExt
    {

        static string[] satuan = new string[10] { "nol", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan" };
        static string[] belasan = new string[10] { "sepuluh", "sebelas", "dua belas", "tiga belas", "empat belas", "lima belas", "enam belas", "tujuh belas", "delapan belas", "sembilan belas" };
        static string[] puluhan = new string[10] { "", "", "dua puluh", "tiga puluh", "empat puluh", "lima puluh", "enam puluh", "tujuh puluh", "delapan puluh", "sembilan puluh" };
        static string[] ribuan = new string[5] { "", "ribu", "juta", "milyar", "triliyun" };
        static  string[] bulanpanjang = new string[12] { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "November", "Desember" };
        static string[] bulanpendek = new string[12] { "Jan", "Feb", "Mar", "Apr", "Mei", "Jun", "Jul", "Agus", "Sept", "Okt", "Nov", "Des" };
        
        
        public static string ToTanggalIndonesia(this DateTime d, bool pendak = false )
        {
            string tanggal = "";
            tanggal = d.Day.ToString("00") + " " + bulanpanjang[d.Month - 1] + " ";
            if (pendak== false)
                tanggal = tanggal + d.Year.ToString();

            return tanggal;
        }
        public static string ToTanggalIndonesiaLengkap(this DateTime d, bool pendak = false)
        {
            string tanggal = "";
            tanggal = d.Day.ToString("00") + " " + bulanpendek[d.Month - 1] + " ";
            if (pendak == false)
                tanggal = tanggal + d.Year.ToString();
            else 
                tanggal = tanggal + ": " + d.Hour.ToString() + "-" + d.Minute.ToString();
            return tanggal;
        }

        public static string ImageToBase64(Image image,
          System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        public static  bool IsDigitsOnly(this string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
