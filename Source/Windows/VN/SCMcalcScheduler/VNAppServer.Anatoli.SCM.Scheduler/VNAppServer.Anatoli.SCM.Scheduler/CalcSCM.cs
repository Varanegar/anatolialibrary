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
using VNAppServer.Common.Abstract;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Anatoli.SCM.Scheduler
{
    public class CalcSCMJob : AnatoliJob
    {
        public static readonly string SCMRequestType = "SCMRequestType";
        public override void ExecTimerMethod(IJobExecutionContext context, HttpClient client, TokenResponse oauthResult)
        {
            try
            {
                var process = new CalcSCMProcess();
                string reuquestTypeId = context.JobDetail.JobDataMap.GetString(CalcSCMJob.SCMRequestType);
                var data = process.CalcSCM(client, oauthResult, Guid.Parse(reuquestTypeId), ServerURI, GetPrivateOwnerQueryString(), PrivateOwnerId);
                process.UploadDataToServer(client, ServerURI, GetPrivateOwnerQueryString(), data);

            }
            catch(Exception ex)
            {
                log.Error("Can not calc SCM process", ex);
            }
        }
    }
}
