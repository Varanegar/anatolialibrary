using System;
using System.IO;
using TrackingMap.Common.Enum;

namespace TrackingMap.Service.BL
{
   
    public class LogService
    {
        public static void InsertLog(string str, string key = "", ELogLevel type = ELogLevel.INFO)
        {
            if (type >= DefaultValue.GetLogLevel())
            {                
                string path = DefaultValue.GetLogPath() + DateTime.Now.ToString("yyyy.MM.dd")+".txt";

                TextWriter tw;
            
                if (!File.Exists(path))
                {
                    var file = File.Create(path);
                    tw = new StreamWriter(file);
                }
                else
                {
                    tw = new StreamWriter(path, true);
                }
                tw.WriteLine(DateTime.Now.ToString("hh:mm:ss ")  + type.ToString() + " " + key + ": " + str);
                tw.Close();
            }

        }



    }
}
