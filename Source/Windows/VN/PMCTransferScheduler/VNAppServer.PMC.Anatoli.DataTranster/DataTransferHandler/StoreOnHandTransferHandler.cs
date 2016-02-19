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
    public class StoreOnHandTransferHandler
    {
        private static readonly string StoreOnHandDataType = "StoreOnHand";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadStoreOnHandToServer(HttpClient client, string serverURI, string privateOwnerQueryString)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(StoreOnHandDataType);
                var dbData = StoreAdapter.Instance.GetAllStoreOnHands(lastUpload);
                if (dbData != null)
                {

                    string data = JsonConvert.SerializeObject(dbData);
                    string URI = serverURI + UriInfo.SaveStoreOnHandURI + privateOwnerQueryString;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client);
                }

                dbData = StoreAdapter.Instance.GetAllStoreOnHands(DateTime.MinValue);
                if (dbData != null)
                {

                    string data = JsonConvert.SerializeObject(dbData);
                    string URI = serverURI + UriInfo.SaveStoreOnHandURI + privateOwnerQueryString;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client);
                }

                Utility.SetLastUploadTime(StoreOnHandDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
