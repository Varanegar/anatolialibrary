using log4net;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer.Anatoli.PMC.Scheduler.Interface
{
    public abstract class IAnatoliJob 
    {
        protected static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string ServerURIKey = "ServerURI";
        private readonly string PrivateOwnerIdKey = "PrivateOwnerId";
        public string ServerURI { get; set; }
        public string PrivateOwnerId { get; set; }
        public string GetPrivateOwnerQueryString()
        {
            return "?privateOwnerId=" + PrivateOwnerId;
        }

        public void GetServerInfo(JobDataMap dataMap)
        {
            ServerURI = dataMap.GetString(ServerURIKey);
            PrivateOwnerId = dataMap.GetString(PrivateOwnerIdKey);
        }
    }
}
