using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using VNAppServer.PMC.Anatoli.DataTranster;

namespace ServiceTestApplication
{
    class Program
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            try
            {
                var restul = log4net.Config.XmlConfigurator.Configure();
                //string ServerURI = "http://localhost:8081/";
                //string ServerURI = "http://46.209.104.2:7000/";
                //string ServerURI = "http://217.218.53.71:8090/";
                //string ServerURI = "http://192.168.0.160:8081/";
                string OwnerKey = "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240";
                string DataOwnerKey = "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                string DataOwnerCenterKey = "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                log.Info("Start Transfer Data Job");
                var oauthClient = new OAuth2Client(new Uri(ServerURI + "/oauth/token"));
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(10);

                //var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("anatoli@varanegar.com", "anatoli@vn@87134", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result; //, "foo bar"
                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("AnatoliMobileApp", "Anatoli@App@Vn", "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240,3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result;
                if (oauthresult.AccessToken != null)
                {
                    client.SetBearerToken(oauthresult.AccessToken);
                    //client.

                    //log.Info("Transfer fiscal year");
                    //FiscalYearTransferHandler.UploadFiscalYearToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer supplier");
                    //SupplierTransferHandler.UploadSupplierToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer manufacture");
                    //ManufactureTransferHandler.UploadManufactureToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer main product group");
                    //MainProductGroupTransferHandler.UploadMainProductGroupToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer product group");
                    //ProductGroupTransferHandler.UploadProductGroupToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerKey);
                    //log.Info("Transfer char type");
                    //CharTypeTransferHandler.UploadCharTypeToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerKey);
                    //log.Info("Transfer char group");
                    //CharGroupTransferHandler.UploadCharGroupToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerKey);
                    //log.Info("Transfer ciry region");
                    //CityRegionTransferHandler.UploadCityRegionToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerKey);
                    //log.Info("Transfer store");
                    StoreTransferHandler.UploadStoreToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer stock");
                    //StockTransferHandler.UploadStockToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer product");
                    //ProductTransferHandler.UploadProductToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer product supplier");
                    //ProductTransferHandler.UploadProductSupplierToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerKey);
                    //log.Info("Transfer product char value");
                    //ProductTransferHandler.UploadProductCharValueToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerKey);
                    //log.Info("Transfer stock product");
                    //StockProductTransferHandler.UploadStockProductToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer store price list");
                    //StorePriceListTransferHandler.UploadStorePriceListToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerKey);
                    //log.Info("Transfer store onhand");
                    StoreOnHandTransferHandler.UploadStoreOnHandToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerKey);
                    //log.Info("Transfer stock hand");
                    //StockOnHandTransferHandler.UploadStockOnHandToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer Product Group Picture");
                    //ProductGroupPictureTransferHandler.UploadProductGroupPictureToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer Product Picture");
                    //ProductPictureTransferHandler.UploadProductPictureToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer Store Picture");
                    //StorePictureTransferHandler.UploadStorePictureToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    //log.Info("Transfer new customers");
                    //CustomerTransferHandler.UploadCustomerToServer(client, ServerURI, OwnerKey, DataOwnerKey, DataOwnerCenterKey);
                    log.Info("Completed Transfer Data Job");
                }
                else
                    log.Error("Login Failed user : AnatoliMobileApp");
            }
            catch (Exception ex)
            {
                log.Error("Sync job failed ", ex);
            }
        }
    }
}
