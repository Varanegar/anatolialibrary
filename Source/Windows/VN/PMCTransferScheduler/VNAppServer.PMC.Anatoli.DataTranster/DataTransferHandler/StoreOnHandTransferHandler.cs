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
    public class StoreOnHandTransferHandler
    {
        private static readonly string StoreOnHandDataType = "StoreOnHand";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadStoreOnHandToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(StoreOnHandDataType);
                var dbData = StoreAdapter.Instance.GetAllStoreOnHands(lastUpload);
                if (dbData != null)
                {
                    StoreRequestModel request = new StoreRequestModel() { storeActiveOnhandData = dbData };

                    string data = JsonConvert.SerializeObject(request);
                    string URI = serverURI + UriInfo.SaveStoreOnHandURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }

                //dbData = StoreAdapter.Instance.GetAllStoreOnHands(DateTime.MinValue);
                //if (dbData != null)
                //{

                //    StoreRequestModel request = new StoreRequestModel() { storeActiveOnhandData = dbData };

                //    string data = JsonConvert.SerializeObject(request);
                //    string URI = serverURI + UriInfo.CheckDeletedStoreOnHandURI;
                //    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                //}

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
