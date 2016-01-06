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

                string data =JsonConvert.SerializeObject(dbData);
                string URI = serverURI + BaseInfo.SaveStoreOnHandURI + privateOwnerQueryString;
                var result = Utility.CallServerService(data, URI, client);
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
