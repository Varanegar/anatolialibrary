using Anatoli.ViewModels.StockModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using VNAppServer.Anatoli.Common;
using VNAppServer.Anatoli.SCM.Scheduler;

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
                string ServerURI = "http://localhost:59822";
                string privateOwnerQueryString = "?privateOwnerId=" + "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                string privateOwnerId = "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C";
                log.Info("Start Transfer Data Job");
                var oauthClient = new OAuth2Client(new Uri(ServerURI + "/oauth/token"));
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(10);

                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("anatoli", "anatoli@vn@87134", "3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result; //, "foo bar"
                if (oauthresult.AccessToken != null)
                {
                    client.SetBearerToken(oauthresult.AccessToken);
                    var result = new CalcSCMProcess().CalcSCM(client, oauthresult, Guid.Parse("1462A36B-9AB0-41AB-88F1-AAD152A7E425"), ServerURI, privateOwnerQueryString, privateOwnerId);
                    string data = JsonConvert.SerializeObject(result);

                    ConnectionHelper.CallServerServicePost(data, ServerURI + UriInfo.SaveStockProductRequestURI + privateOwnerQueryString, client);

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
