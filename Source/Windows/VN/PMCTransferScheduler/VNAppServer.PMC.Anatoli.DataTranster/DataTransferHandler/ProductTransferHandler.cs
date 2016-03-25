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
    public class ProductTransferHandler
    {
        private static readonly string ProductDataType = "Product";
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void UploadProductToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(ProductDataType);
                var dbData = ProductAdapter.Instance.GetAllProducts(lastUpload);
                {
                    ProductRequestModel request = new ProductRequestModel() { productData = dbData };
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    string data = js.Serialize(request);
                    string URI = serverURI + UriInfo.SaveProductURI;// +privateOwnerQueryString;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }

                dbData = ProductAdapter.Instance.GetAllProducts(DateTime.MinValue);
                if (dbData != null)
                {
                    ProductRequestModel request = new ProductRequestModel() { productData = dbData };
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    string data = js.Serialize(request);
                    string URI = serverURI + UriInfo.CheckDeletedProductURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }

                Utility.SetLastUploadTime(ProductDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch(Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
        public static void UploadProductSupplierToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(ProductDataType + "Suppliers");
                var dbData = ProductAdapter.Instance.GetAllProductSuppliers(lastUpload);
                {
                    ProductRequestModel request = new ProductRequestModel() { productSupplierData = dbData };
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    string data = js.Serialize(request);
                    string URI = serverURI + UriInfo.SaveProductSupplierURI;// +privateOwnerQueryString;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }

                Utility.SetLastUploadTime(ProductDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch (Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }
        }
        public static void UploadProductCharValueToServer(HttpClient client, string serverURI, string privateOwnerId, string dataOwner, string dataOwnerCenter)
        {
            try
            {
                log.Info("Start CallServerService URI ");
                var currentTime = DateTime.Now;
                var lastUpload = Utility.GetLastUploadTime(ProductDataType + "CharValues");
                var dbData = ProductAdapter.Instance.GetAllProductCharValues(lastUpload);
                {
                    ProductRequestModel request = new ProductRequestModel() { productCharData = dbData };
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    js.MaxJsonLength = Int32.MaxValue;
                    string data = js.Serialize(request);
                    string URI = serverURI + UriInfo.SaveProductCharValueURI;
                    var result = ConnectionHelper.CallServerServicePost(data, URI, client, privateOwnerId, dataOwner, dataOwnerCenter);
                }

                Utility.SetLastUploadTime(ProductDataType, currentTime);

                log.Info("Completed CallServerService ");
            }
            catch (Exception ex)
            {
                log.Error("Failed CallServerService ", ex);
            }

        }
    }
}
