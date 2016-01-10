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
    public class ProductTransferHandler
    {
        private static readonly string ProductDataType = "Product";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadProductToServer(HttpClient client, string serverURI, string privateOwnerQueryString)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(ProductDataType);
                var dbData = ProductAdapter.Instance.GetAllProducts(lastUpload);
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                string data = js.Serialize(dbData);
                string URI = serverURI + UriInfo.SaveProductURI + privateOwnerQueryString;
                var result = ConnectionHelper.CallServerServicePost(data, URI, client);
                if(result != null )
                    Utility.SetLastUploadTime(ProductDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
