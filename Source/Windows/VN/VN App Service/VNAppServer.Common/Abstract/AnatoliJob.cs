using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Thinktecture.IdentityModel.Client;
using VNAppServer.Anatoli.PMC.Scheduler.Interface;

namespace VNAppServer.Common.Abstract
{
    public abstract class AnatoliJob : IAnatoliJob, IJob 
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                GetServerInfo(context.JobDetail.JobDataMap);
                log.Info("Start Transfer Data Job");
                var oauthClient = new OAuth2Client(new Uri(ServerURI + "/oauth/token"));
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(10);

                var oauthresult = oauthClient.RequestResourceOwnerPasswordAsync("AnatoliMobileApp", "Anatoli@App@Vn", "79A0D598-0BD2-45B1-BAAA-0A9CF9EFF240,3EEE33CE-E2FD-4A5D-A71C-103CC5046D0C").Result; 
                if (oauthresult.AccessToken != null)
                {
                    client.SetBearerToken(oauthresult.AccessToken);


                }
                else
                    log.Error("Login Failed user : AnatoliMobileApp");
            }
            catch (Exception ex)
            {
                log.Error("Sync job failed ", ex);
            }
        }

        public abstract void ExecTimerMethod(IJobExecutionContext context, HttpClient client, TokenResponse oauthResult);
    }
}
