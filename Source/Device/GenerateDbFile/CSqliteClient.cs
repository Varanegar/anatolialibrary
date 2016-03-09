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
#if (DEBUG)
            string path = "D:\\Varanegar\\PAA\\AnatoliGit\\Source\\Device\\db\\paa.db";
#endif
#if(!DEBUG)
            string path = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).ToString() , "db\\paa.db");
#endif
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
