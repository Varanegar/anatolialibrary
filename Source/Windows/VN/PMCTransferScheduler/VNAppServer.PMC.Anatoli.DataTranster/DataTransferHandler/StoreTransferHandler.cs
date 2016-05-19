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
    public class StoreTransferHandler
    {
        private static readonly string StoreDataType = "Store";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadStoreToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(StoreDataType);
                var dbData = StoreAdapter.Instance.GetAllStores(lastUpload);
                if (dbData != null)
                {
                    StoreRequestModel model = new StoreRequestModel();
                    model.storeData = dbData;
                    
                    string data = JsonConvert.SerializeObject(model);
                    string URI = serverURI + UriInfo.SaveStoreURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }
                else
                    log.Info("Null data to transfer " + serverURI);

                //dbData = StoreAdapter.Instance.GetAllStores(DateTime.MinValue);
                //if (dbData != null)
                //{
                //    StoreRequestModel model = new StoreRequestModel();
                //    model.storeData = dbData;
                    
                //    string data = JsonConvert.SerializeObject(model);
                //    string URI = serverURI + UriInfo.CheckDeletedStoreURI;
                //    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                //}

                Utility.SetLastUploadTime(StoreDataType, currentTime);


                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
