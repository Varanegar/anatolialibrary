using System;
using System.Collections.Generic;
using System.Linq;

namespace TrackingMap.Common.Tools
{
    public class GeneralTools
    {
        public static string IntListTostring(List<int> list)
        {
            return list.Aggregate("", (current, i) => current + (i + ','));
        }

        public static string GuidListTostring(List<Guid> list)
        {
            var str = list.Aggregate("", (current, i) => current + (i.ToString() + ','));
            if (str.Length > 0)
                str = str.Remove(str.Length - 1);
            return str;
        }

        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }

        public static string FormatNumber(string number, bool changeToSmall)
        {
            var numberDecimal = decimal.Parse(number);
            if (changeToSmall == false)
                return numberDecimal.ToString("#,##0");
            else
            {
                if (numberDecimal < 1000)
                    return numberDecimal.ToString();
                else if (numberDecimal < 1000000)
                    return ((int)(numberDecimal / 1000)).ToString() + " K";
                else if (numberDecimal < 1000000000)
                    return ((int)(numberDecimal / 1000000)).ToString() + " M";
                else if (numberDecimal < 1000000000000)
                    return ((int)(numberDecimal / 1000000000)).ToString() + " B";
                else
                    return "N/A";
            }
        }
        
    }
}
