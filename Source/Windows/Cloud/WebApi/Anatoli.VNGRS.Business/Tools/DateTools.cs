using System;
using System.Globalization;

namespace TrackingMap.Common.Tools
{

    public class DateTools
    {
        public readonly static string DATE_SPLITTER = "/";
        public readonly static string TIME_SPLITTER = ":";

        public static string DateTimeToPersianDateTime(DateTime date)
        {
            var persianDate = new PersianCalendar();

            var year = Convert.ToString(persianDate.GetYear(date));
            var month = Convert.ToString(persianDate.GetMonth(date));
            var day = Convert.ToString(persianDate.GetDayOfMonth(date));
            var hour = Convert.ToString(persianDate.GetHour(date));
            var minuts = Convert.ToString(persianDate.GetMinute(date));
            var second = Convert.ToString(persianDate.GetSecond(date));
            

            if (month.Length < 2) month = "0" + month;
            if (day.Length < 2) day = "0" + day;

            var result = year + DATE_SPLITTER + month + DATE_SPLITTER + day+ " "+ hour + TIME_SPLITTER + minuts + TIME_SPLITTER + second;

            return result;
        }

        public static string DateTimeToPersianDate(DateTime date)
        {
            var persianDate = new PersianCalendar();

            var year = Convert.ToString(persianDate.GetYear(date));
            var month = Convert.ToString(persianDate.GetMonth(date));
            var day = Convert.ToString(persianDate.GetDayOfMonth(date));            

            if (month.Length < 2) month = "0" + month;
            if (day.Length < 2) day = "0" + day;

            var result = year + DATE_SPLITTER + month + DATE_SPLITTER + day;

            return result;
        }

        public static string PersianDateNow()
        {
            var persianDate = new PersianCalendar();
            var now = DateTime.Now;

            
            var year = Convert.ToString(persianDate.GetYear(now));
            var month = Convert.ToString(persianDate.GetMonth(now));
            var day = Convert.ToString(persianDate.GetDayOfMonth(now));           

            if (month.Length < 2) month = "0" + month;
            if (day.Length < 2) day = "0" + day;

            var result = year + DATE_SPLITTER + month + DATE_SPLITTER + day;

            return result;
        }
    }

    public enum DatePart { All, Year, Month, Day }
}