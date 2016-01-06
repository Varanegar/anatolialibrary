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
    public class ProductGroupTransferHandler
    {
        private static readonly string ProductGroupDataType = "ProductGroup";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadProductGroupToServer(HttpClient client, string serverURI, string privateOwnerQueryString)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(ProductGroupDataType);
                var dbData = ProductAdapter.Instance.GetAllProductGroups(lastUpload);

                string data =JsonConvert.SerializeObject(dbData);
                string URI = serverURI + BaseInfo.SaveProductGroupURI + privateOwnerQueryString;
                var result = Utility.CallServerService(data, URI, client);
                Utility.SetLastUploadTime(ProductGroupDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
