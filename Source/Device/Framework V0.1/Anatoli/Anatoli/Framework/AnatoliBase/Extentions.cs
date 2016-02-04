using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    public static class Extentions
    {
        public static string ToCurrency(this double value)
        {
            return Math.Round(value / 10, 0).ToString("N0");
        }
        public static string ToCurrency(this decimal value)
        {
            return Math.Round(value / 10, 0).ToString("N0");
        }
    }
}
