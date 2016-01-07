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
                string ServerURI = "http://localhost/";
                string privateOwner = "?privateOwnerId=" + "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                log.Info("Start Transfer Data Job");
                var oauthClient = new OAuth2Client(new Uri(ServerURI + "/oauth/token"));
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(10);

                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("AnatoliMobileApp", "Anatoli@App@Vn", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result; //, "foo bar"
                if (oauthresult.AccessToken != null)
                {
                    client.SetBearerToken(oauthresult.AccessToken);

                    log.Info("Transfer fiscal year");
                    FiscalYearTransferHandler.UploadFiscalYearToServer(client, ServerURI, privateOwner);
                    log.Info("Transfer supplier");
                    SupplierTransferHandler.UploadSupplierToServer(client, ServerURI, privateOwner);
                    log.Info("Transfer manufacture");
                    ManufactureTransferHandler.UploadManufactureToServer(client, ServerURI, privateOwner);
                    log.Info("Transfer main product group");
                    MainProductGroupTransferHandler.UploadMainProductGroupToServer(client, ServerURI, privateOwner);
                    log.Info("Transfer store");
                    StoreTransferHandler.UploadStoreToServer(client, ServerURI, privateOwner);
                    log.Info("Transfer stock");
                    StockTransferHandler.UploadStockToServer(client, ServerURI, privateOwner);
                    log.Info("Transfer product");
                    ProductTransferHandler.UploadProductToServer(client, ServerURI, privateOwner);
                    log.Info("Transfer stock product");
                    StockProductTransferHandler.UploadStockProductToServer(client, ServerURI, privateOwner);
                    log.Info("Transfer stock hand");
                    StockOnHandTransferHandler.UploadStockOnHandToServer(client, ServerURI, privateOwner);
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
