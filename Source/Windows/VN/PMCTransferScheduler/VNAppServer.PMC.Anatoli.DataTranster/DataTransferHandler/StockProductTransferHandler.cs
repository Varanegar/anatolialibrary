using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Anatoli.PMC.DataAccess.DataAdapter;
using VNAppServer.Anatoli.PMC.Helpers;
using VNAppServer.Anatoli.Common;

namespace VNAppServer.PMC.Anatoli.DataTranster
{
    public class StockTransferHandler
    {
        private static readonly string StockDataType = "Stock";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadStockToServer(HttpClient client, string serverURI, string pirvateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(StockDataType);
                var dbData = StockAdapter.Instance.GetAllStocks(lastUpload);
                if (dbData != null)
                {
                    string data = JsonConvert.SerializeObject(dbData);
                    string URI = serverURI + UriInfo.SaveStockURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client);
                    Utility.SetLastUploadTime(StockDataType, currentTime);
                }
                else
                    log.Info("Null data to transfer " + serverURI);


                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
