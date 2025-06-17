using System;
using System.Globalization;
using System.Text.RegularExpressions;
namespace KUAPPAS
{
    public static class Rupiah
    {
       
            public static string ToRupiah(this decimal angka)
            {
                return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "{0:N}", angka);
            }
            /**
             * // Usage example: //
             * int angka = 10000000;
             * System.Console.WriteLine(angka.ToRupiah()); // -> Rp. 10.000.000
             */

            public static decimal ToAngka(this string rupiah)
            {
                return decimal.Parse(Regex.Replace(rupiah, @",.*|\D", ""));
            }
            /**
             * // Usage example: //
             * string rupiah = "Rp 10.000.123,00";
             * System.Console.WriteLine(rupiah.ToAngka()); // -> 10000123
             */
        }
    
}
