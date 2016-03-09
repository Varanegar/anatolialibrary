using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Framework.AnatoliBase
{
    public abstract class AnatoliSQLite
    {
        public AnatoliSQLite()
        {

        }
        public abstract SQLiteConnection GetConnection();
        public bool TableExists(SQLiteConnection connection, String tableName)
        {
            try
            {
                var info = connection.GetTableInfo(tableName);
                return (info.Count > 0);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
