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
    public class MainProductGroupTransferHandler
    {
        private static readonly string MainProductGroupDataType = "MainProductGroup";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadMainProductGroupToServer(HttpClient client, string serverURI, string privateOwnerQueryString)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(MainProductGroupDataType);
                var dbData = ProductAdapter.Instance.GetAllMainProductGroups(lastUpload);

                string data =JsonConvert.SerializeObject(dbData);
                string URI = serverURI + UriInfo.SaveMainProductGroupURI + privateOwnerQueryString;
                var result = ConnectionHelper.CallServerServicePost(data, URI, client);
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
