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
    public class MainProductGroupTransferHandler
    {
        private static readonly string MainProductGroupDataType = "MainProductGroup";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadMainProductGroupToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(MainProductGroupDataType);
                var dbData = ProductAdapter.Instance.GetAllMainProductGroups(lastUpload);
                if (dbData != null)
                {
                    ProductRequestModel model = new ProductRequestModel();
                    model.mainProductGroupData = dbData;

                    string data = JsonConvert.SerializeObject(model);
                    string URI = serverURI + UriInfo.SaveMainProductGroupURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }

                dbData = ProductAdapter.Instance.GetAllMainProductGroups(DateTime.MinValue);
                if (dbData != null)
                {
                    ProductRequestModel model = new ProductRequestModel();
                    model.mainProductGroupData = dbData;

                    string data = JsonConvert.SerializeObject(model);
                    string URI = serverURI + UriInfo.CheckDeletedMainProductGroupURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                } 
                Utility.SetLastUploadTime(MainProductGroupDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
