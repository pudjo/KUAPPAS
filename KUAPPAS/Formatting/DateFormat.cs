using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//http://www.extensionmethod.net/csharp/datetime

namespace Formatting
{
    public static class DateFormat
    {
        public static DateTime GetLastDayOfMonth(this DateTime dateTime)
        {
            DateTime d = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1);

            return d.AddDays(-1);// new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);

        }
        public static DateTime GetLastDayOfMonth(this DateTime dateTime, int month)
        {

            return new DateTime(dateTime.Year, month, 1).AddMonths(1).AddDays(-1);

        }
        public static Boolean IsBetween(this DateTime dt, DateTime startDate, DateTime endDate, Boolean compareTime = false)
        {
            return compareTime ?
               dt >= startDate && dt <= endDate :
               dt.Date >= startDate.Date && dt.Date <= endDate.Date;
        }
        public static int Triwulan(this DateTime dt)
        {
            int ret = 1;
            switch (dt.Month)
            {
                case 1:
                    ret= 1;
                    break;
                case 2:
                    ret= 1;
                    break;
                case 3:
                    ret= 1;
                    break;
                case 4:
                    ret = 2;
                    break;
                case 5:
                    ret = 2;
                    break;
                case 6:
                    ret = 2;
                    break;
                case 7:
                    ret = 3;
                    break;
                case 8:
                    ret = 3;
                    break;
                case 9:
                    ret = 3;
                    break;
                case 10:
                    ret = 4;
                    break;
                case 11:
                    ret = 4;
                    break;
                case 12:
                    ret = 4;
                    break;                    
            }
            return ret;

        }
        /// <summary>
        /// DateDiff in SQL style. 
        /// Datepart implemented: 
        ///     "year" (abbr. "yy", "yyyy"), 
        ///     "quarter" (abbr. "qq", "q"), 
        ///     "month" (abbr. "mm", "m"), 
        ///     "day" (abbr. "dd", "d"), 
        ///     "week" (abbr. "wk", "ww"), 
        ///     "hour" (abbr. "hh"), 
        ///     "minute" (abbr. "mi", "n"), 
        ///     "second" (abbr. "ss", "s"), 
        ///     "millisecond" (abbr. "ms").
        ///     // Gets the total days from 01/01/2000.
        ////DateTime dt = new DateTime(2000, 01, 01);
        ////Int64 days = dt.DateDiff("day", DateTime.Now);
        ////// Gets the total hours from 01/01/2000.
        ////Int64 hours = dt.DateDiff("hour", DateTime.Now);
        /// </summary>
        /// <param name="DatePart"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public static Int64 DateDiff(this DateTime StartDate, String DatePart, DateTime EndDate)
        {
            Int64 DateDiffVal = 0;
            System.Globalization.Calendar cal = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar;
            TimeSpan ts = new TimeSpan(EndDate.Ticks - StartDate.Ticks);
            switch (DatePart.ToLower().Trim())
            {
                #region year
                case "year":
                case "yy":
                case "yyyy":
                    DateDiffVal = (Int64)(cal.GetYear(EndDate) - cal.GetYear(StartDate));
                    break;
                #endregion

                #region quarter
                case "quarter":
                case "qq":
                case "q":
                    DateDiffVal = (Int64)((((cal.GetYear(EndDate)
                                        - cal.GetYear(StartDate)) * 4)
                                        + ((cal.GetMonth(EndDate) - 1) / 3))
                                        - ((cal.GetMonth(StartDate) - 1) / 3));
                    break;
                #endregion

                #region month
                case "month":
                case "mm":
                case "m":
                    DateDiffVal = (Int64)(((cal.GetYear(EndDate)
                                        - cal.GetYear(StartDate)) * 12
                                        + cal.GetMonth(EndDate))
                                        - cal.GetMonth(StartDate));
                    break;
                #endregion

                #region day
                case "day":
                case "d":
                case "dd":
                    DateDiffVal = (Int64)ts.TotalDays;
                    break;
                #endregion

                #region week
                case "week":
                case "wk":
                case "ww":
                    DateDiffVal = (Int64)(ts.TotalDays / 7);
                    break;
                #endregion

                #region hour
                case "hour":
                case "hh":
                    DateDiffVal = (Int64)ts.TotalHours;
                    break;
                #endregion

                #region minute
                case "minute":
                case "mi":
                case "n":
                    DateDiffVal = (Int64)ts.TotalMinutes;
                    break;
                #endregion

                #region second
                case "second":
                case "ss":
                case "s":
                    DateDiffVal = (Int64)ts.TotalSeconds;
                    break;
                #endregion

                #region millisecond
                case "millisecond":
                case "ms":
                    DateDiffVal = (Int64)ts.TotalMilliseconds;
                    break;
                #endregion

                default:
                    throw new Exception(String.Format("DatePart \"{0}\" is unknown", DatePart));
            }
            return DateDiffVal;
        }
        public static string ToFriendlyDateString(this DateTime Date)
        {
            string FormattedDate = "";
            if (Date.Date == DateTime.Today)
            {
                FormattedDate = "Today";
            }
            else if (Date.Date == DateTime.Today.AddDays(-1))
            {
                FormattedDate = "Yesterday";
            }
            else if (Date.Date > DateTime.Today.AddDays(-6))
            {
                // *** Show the Day of the week
                FormattedDate = Date.ToString("dddd").ToString();
            }
            else
            {
                FormattedDate = Date.ToString("MMMM dd, yyyy");
            }

            //append the time portion to the output
            FormattedDate += " @ " + Date.ToString("t").ToLower();
            return FormattedDate;
        }
        public static DateTime EndOfTheMonth(this DateTime date)
        {
            var endOfTheMonth = new DateTime(date.Year, date.Month, 1)
                .AddMonths(1)
                .AddDays(-1);

            return endOfTheMonth;
        }
        public static DateTime BeginningOfTheMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        public static DateTime GetCurrentDate(this object source)
        {
            return DateTime.Now;
        }

        //internal static object GetDateTime(object p)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
