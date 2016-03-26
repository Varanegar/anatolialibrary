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
using Anatoli.ViewModels;

namespace VNAppServer.PMC.Anatoli.DataTranster
{
    public class StockProductTransferHandler
    {
        private static readonly string StockProductDataType = "StockProduct";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadStockProductToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(StockProductDataType);
                var dbData = StockAdapter.Instance.GetAllStockProducts(lastUpload);
                if (dbData != null)
                {
                    StockRequestModel request = new StockRequestModel() { stockProductData = dbData };

                    string data = JsonConvert.SerializeObject(request);
                    string URI = serverURI + UriInfo.SaveStockProductURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                    Utility.SetLastUploadTime(StockProductDataType, currentTime);
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
