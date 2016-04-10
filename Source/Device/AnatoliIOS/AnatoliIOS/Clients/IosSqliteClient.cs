using Anatoli.Framework.AnatoliBase;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AnatoliIOS.Clients
{
    class IosSqliteClient : AnatoliSQLite
    {
        public override SQLite.SQLiteConnection GetConnection()
        {
            if (System.IO.File.Exists("paa.db"))
            {
                var conn = new SQLite.SQLiteConnection("paa.db");
                return conn;
            }
            throw new FileNotFoundException("Database file not found");
        }
    }
}
