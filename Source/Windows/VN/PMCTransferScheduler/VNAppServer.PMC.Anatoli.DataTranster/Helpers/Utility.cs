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
        public static string CallServerService(string data, string URI, HttpClient client)
        {
            try
            {
                HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = client.PostAsync(URI, content).Result;
                return result.Content.ReadAsStringAsync().Result;
                return null;
            }
            catch (Exception ex)
            {
                log.Error("Fail CallServerService URI :" + URI, ex);
                return null;
            }
        }
        public static string CallServerService(List<ItemImageViewModel> dataList, HttpClient client, string URI)
        {
            dataList.ForEach(item =>
            {
                var requestContent = new MultipartFormDataContent();
                var imageContent = new ByteArrayContent(item.image);
                imageContent.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("image/jpeg");

                requestContent.Add(imageContent, item.BaseDataId + "-" + item.ID, item.BaseDataId + "-" + item.ID + ".png");
                var response = client.PostAsync(URI + "&imageId=" + item.UniqueId + "&imagetype=" + item.ImageType + "&token=" + item.BaseDataId, requestContent).Result;
            }
            );
            return null;
        }

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
