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

namespace VNAppServer.PMC.Anatoli.DataTranster
{
    public class StockOnHandTransferHandler
    {
        private static readonly string StockOnHandDataType = "StockOnHand";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadStockOnHandToServer(HttpClient client, string serverURI, string privateOwnerQueryString)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(StockOnHandDataType);
                var dbData = StockAdapter.Instance.GetAllStockOnHands(lastUpload);
                if (dbData != null)
                {
                    string data = JsonConvert.SerializeObject(dbData);
                    string URI = serverURI + BaseInfo.SaveStockOnHandURI + privateOwnerQueryString;
                    var result = Utility.CallServerService(data, URI, client);
                    Utility.SetLastUploadTime(StockOnHandDataType, currentTime);
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
