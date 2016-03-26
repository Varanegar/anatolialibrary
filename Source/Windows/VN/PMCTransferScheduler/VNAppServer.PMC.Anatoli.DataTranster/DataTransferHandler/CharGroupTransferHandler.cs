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
    public class CharGroupTransferHandler
    {
        private static readonly string CharGroupDataType = "CharGroup";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadCharGroupToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(CharGroupDataType);
                var dbData = CharGroupAdapter.Instance.GetAllCharGroups(lastUpload);
                if (dbData != null)
                {
                    ProductRequestModel model = new ProductRequestModel();
                    model.charGroupData = dbData;

                    string data = JsonConvert.SerializeObject(model);
                    string URI = serverURI + UriInfo.SaveCharGroupURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }


                dbData = CharGroupAdapter.Instance.GetAllCharGroups(DateTime.MinValue);
                if (dbData != null)
                {
                    ProductRequestModel model = new ProductRequestModel();
                    model.charGroupData = dbData;

                    string data = JsonConvert.SerializeObject(model);
                    string URI = serverURI + UriInfo.CheckDeletedCharGroupURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }                
                Utility.SetLastUploadTime(CharGroupDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
