using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.Anatoliclient
{
    public abstract class AnatoliSQLite
    {
        protected SQLiteConnection _connection;
        public SQLiteConnection Connection
        {
            get { return _connection; }
        }
        public AnatoliSQLite()
        {
            _connection = GetConnection();
        }
        protected abstract SQLiteConnection GetConnection();
        public bool TableExists(String tableName)
        {
            try
            {
                var info = _connection.GetTableInfo(tableName);
                return (info.Count > 0);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
