using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace TrackingMap.Common.Tools
{
    public class DbUtility
    {

        public static string GetConnectionString(string name)
        {
            //ConnectionStringSettings mySetting = ConfigurationManager.ConnectionStrings[name];
            //return mySetting.ToString();            
            return "salam";
        }

        public static string[] ParseCommands(string filePath, bool throwExceptionIfNonExists)
        {
            if (!File.Exists(filePath))
            {
                if (throwExceptionIfNonExists)
                    throw new ArgumentException(string.Format("Specified file doesn't exist - {0}", filePath));
                else
                    return new string[0];
            }


            var statements = new List<string>();
            using (var stream = File.OpenRead(filePath))
            using (var reader = new StreamReader(stream))
            {
                var statement = "";
                while ((statement = ReadNextStatementFromStream(reader)) != null)
                {
                    statements.Add(statement);
                }
            }

            return statements.ToArray();
        }

        private static string ReadNextStatementFromStream(StreamReader reader)
        {
            var sb = new StringBuilder();

            string lineOfText;

            while (true)
            {
                lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    if (sb.Length > 0)
                        return sb.ToString();
                    else
                        return null;
                }

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                    break;

                sb.Append(lineOfText + Environment.NewLine);
            }

            return sb.ToString();
        }

        public static object SetDbNull(object val)
        {
            if (val != null) return val;
            return DBNull.Value; 
        }
        public static object IsNull(object val, object def)
        {
            if (val != null) return val;
            return def;
        }
    }


}
