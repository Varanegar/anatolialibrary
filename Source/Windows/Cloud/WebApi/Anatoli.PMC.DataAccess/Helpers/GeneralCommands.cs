using Anatoli.PMC.DataAccess.Helpers.Entity;
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

        public static int GetId(DataContext context, string tableName)
        {
            return context.Execute<PMCGetIdEntity>("EXEC GetId '" + tableName + "' , @Id output").Id;
        }
        public static int GetFiscalYearId(DataContext context)
        {
            if (context == null) context = new DataContext();
            return context.First<int>(DBQuery.GetFiscalYearId());
        }
    }
}
