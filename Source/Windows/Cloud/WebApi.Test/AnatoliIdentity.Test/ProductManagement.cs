using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace ClientApp
{
    public static class ProductManagement
    {
        public static void ProductGroupTest()
        {
            using (var context = new DataContext())
            {
                using (IDataReader dr = context.Query("select * from users"))
                {
                    while (dr.Read())
                    {
                    }
                }
            }
        }
    }
}
