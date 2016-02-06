using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using VNAppServer.Anatoli.PMC.Scheduler.Interface;
using VNAppServer.Common.Interfaces;
using VNAppServer.PMC.Anatoli.DataTranster;

namespace VNAppServer.Anatoli.PMC.Scheduler
{
    public class UploadLatestDataForAnatoliPOS : IAnatoliJob, IJob    
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                GetServerInfo(context.JobDetail.JobDataMap);
                log.Info("Start Transfer Data Job");
                var oauthClient = new OAuth2Client(new Uri(ServerURI + "/oauth/token"));
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(3);

                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("AnatoliMobileApp", "Anatoli@App@Vn", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result; //, "foo bar"
                if (oauthresult.AccessToken != null)
                {
                    client.SetBearerToken(oauthresult.AccessToken);
                    client.Timeout = TimeSpan.FromMilliseconds(120 * 60 * 1000);

                    log.Info("Transfer supplier");
                    SupplierTransferHandler.UploadSupplierToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Transfer manufacture");
                    ManufactureTransferHandler.UploadManufactureToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Transfer product group");
                    ProductGroupTransferHandler.UploadProductGroupToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Transfer char type");
                    CharTypeTransferHandler.UploadCharTypeToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Transfer char group");
                    CharGroupTransferHandler.UploadCharGroupToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Transfer ciry region");
                    CityRegionTransferHandler.UploadCityRegionToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Transfer store");
                    StoreTransferHandler.UploadStoreToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Transfer product");
                    ProductTransferHandler.UploadProductToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Transfer store price list");
                    StorePriceListTransferHandler.UploadStorePriceListToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Transfer store onhand");
                    StoreOnHandTransferHandler.UploadStoreOnHandToServer(client, ServerURI, GetPrivateOwnerQueryString());
                    log.Info("Completed Transfer Data Job");
                }
                else
                    log.Error("Login Failed user : AnatoliMobileApp");
            }
            catch(Exception ex)
            {
                log.Error("Sync job failed ", ex);
            }
        }
    }
}
