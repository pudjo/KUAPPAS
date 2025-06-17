using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KUAPPAS.Formatting
{
    public static class Rupiah
    {
        public static string ToRupiah(this decimal angka)
        {
            return String.Format(CultureInfo.CreateSpecificCulture("id-id"), "Rp. {0:N}", angka);
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
