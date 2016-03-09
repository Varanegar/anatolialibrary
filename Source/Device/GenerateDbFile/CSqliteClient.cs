using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenerateDbFile
{
    class CSqliteClient : Anatoli.Framework.AnatoliBase.AnatoliSQLite
    {
        public override SQLiteConnection GetConnection()
        {
            string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).ToString() , "db\\paa.db");
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
