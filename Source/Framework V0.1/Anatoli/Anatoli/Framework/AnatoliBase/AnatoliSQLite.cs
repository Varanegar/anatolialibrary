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
            SQLiteCommand cmd = _connection.CreateCommand("SELECT * FROM sqlite_master WHERE type = 'table' AND name = @name", new object[] { tableName });
            return (cmd.ExecuteScalar<int>() != null);
        }
    }
}
