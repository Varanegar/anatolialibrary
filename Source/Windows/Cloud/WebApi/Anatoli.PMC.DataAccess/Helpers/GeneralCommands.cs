using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace Anatoli.PMC.DataAccess.Helpers
{
    public class GeneralCommands
    {
        public static DateTime GetServerDateTime(DataContext context)
        {
            return context.GetValue<DateTime>("SELECT GETDATE() AS ServerDate");
        }
    }
}
