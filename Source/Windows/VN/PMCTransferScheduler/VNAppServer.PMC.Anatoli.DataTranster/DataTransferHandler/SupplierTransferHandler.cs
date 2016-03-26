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
    public class SupplierTransferHandler
    {
        private static readonly string SupplierDataType = "Supplier";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadSupplierToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(SupplierDataType);
                var dbData = SupplierAdapter.Instance.GetAllSuppliers(lastUpload);
                if (dbData != null)
                {
                    GeneralRequestModel model = new GeneralRequestModel();
                    model.supplierData = dbData;

                    string data = JsonConvert.SerializeObject(model);
                    string URI = serverURI + UriInfo.SaveSupplierURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }
                
                dbData = SupplierAdapter.Instance.GetAllSuppliers(DateTime.MinValue);
                if (dbData != null)
                {
                    GeneralRequestModel model = new GeneralRequestModel();
                    model.supplierData = dbData;

                    string data = JsonConvert.SerializeObject(model);
                    string URI = serverURI + UriInfo.CheckDeletedSupplierURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }
                Utility.SetLastUploadTime(SupplierDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch (Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
    }
}
