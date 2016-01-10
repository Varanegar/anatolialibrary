using Anatoli.ViewModels.BaseModels;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Thunderstruck;

namespace VNAppServer.Anatoli.PMC.Helpers
{
    public static class Utility
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        

        public static DateTime GetLastUploadTime(string dataType)
        {
            DateTime uploadTime = DateTime.MinValue;
            using (var context = new DataContext())
            {
                var sql = "select LastUpdateTime from AnatoliUpdateData where DataType ='" + dataType + "'";
                log.Info(sql);
                var lastUpload = context.First<AnatoliUpdateData>("select LastUpdateTime from AnatoliUpdateData where DataType ='" + dataType + "'");
                if (lastUpload != null)
                {
                    return lastUpload.LastUpdateTime;
                }
            }
            log.Info("Data type " + dataType + " does not have last upload");
            return uploadTime;
        }

        public static void SetLastUploadTime(string dataType, DateTime uploadDate)
        {
            using (var context = new DataContext(Transaction.No))
            {
                var data = context.First<AnatoliUpdateData>("select LastUpdateTime from AnatoliUpdateData where DataType ='" + dataType + "' order by LastUpdateTime desc");
                if (data == null)
                {
                    context.Execute(@"INSERT INTO [dbo].[AnatoliUpdateData] ([DataType] ,[LastUpdateTime]) VALUES 
                                        ('" + dataType + "', '" + uploadDate.ToString("yyyy/MM/dd HH:mm:ss") + "')");
                }
                else
                    context.Execute("update AnatoliUpdateData set LastUpdateTime ='" + uploadDate.ToString("yyyy/MM/dd HH:mm:ss") + "'" +
                                   " where DataType ='" + dataType + "'");
                log.Info("Data type " + dataType + " last upload updated");
            }
        }

        public class AnatoliUpdateData
        {
            public DateTime LastUpdateTime { get; set; }
            public string DataType { get; set; }
        }
    }
}
