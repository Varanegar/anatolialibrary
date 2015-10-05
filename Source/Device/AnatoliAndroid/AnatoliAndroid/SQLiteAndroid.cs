using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Anatoli.Anatoliclient;
using System.IO;
using SQLite;


namespace AnatoliAndroid
{
    class SQLiteAndroid : AnatoliSQLite
    {

        protected override SQLiteConnection GetConnection()
        {
            string path = FileAccessHelper.GetLocalFilePath("paa.db");
            var conn = new SQLite.SQLiteConnection(path);
            return conn;

        }
        public class FileAccessHelper
        {
            public static string GetLocalFilePath(string filename)
            {
                string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string dbPath = Path.Combine(path, filename);

                CopyDatabaseIfNotExists(dbPath, filename);

                return dbPath;
            }

            private static void CopyDatabaseIfNotExists(string dbPath, string fileName)
            {
                if (!File.Exists(dbPath))
                {
                    using (var br = new BinaryReader(Application.Context.Assets.Open(fileName)))
                    {
                        using (var bw = new BinaryWriter(new FileStream(dbPath, FileMode.Create)))
                        {
                            byte[] buffer = new byte[2048];
                            int length = 0;
                            while ((length = br.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                bw.Write(buffer, 0, length);
                            }
                        }
                    }
                }
            }
        }
    }
}