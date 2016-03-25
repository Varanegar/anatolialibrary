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
using Anatoli.ViewModels.StockModels;
using Anatoli.ViewModels;

namespace VNAppServer.PMC.Anatoli.DataTranster
{
    public class StockOnHandTransferHandler
    {
        private static readonly string StockOnHandDataType = "StockOnHand";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadStockOnHandToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(StockOnHandDataType);
                var stockList = StockAdapter.Instance.GetAllStocks(DateTime.MinValue);
                foreach(StockViewModel stock in stockList)
                {
                    var dbData = StockAdapter.Instance.GetAllStockOnHandsByStockId(lastUpload, stock.ID.ToString(), stock.StoreId.ToString());
                    if (dbData != null)
                    {
                        StockRequestModel request = new StockRequestModel() { stockActiveOnHandData = dbData };

                        string data = JsonConvert.SerializeObject(request);
                        string URI = serverURI + UriInfo.SaveStockOnHandURI;
                        var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                    }
                    else
                        log.Info("Null data to transfer " + serverURI);
    
                }
                Utility.SetLastUploadTime(StockOnHandDataType, currentTime);


                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
