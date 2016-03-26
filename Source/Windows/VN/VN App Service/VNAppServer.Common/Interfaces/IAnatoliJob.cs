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
        private readonly string OwnerKeyId = "OwnerKey";
        private readonly string DataOwnerKeyId = "DataOwnerKey";
        private readonly string DataOwnerCenterKeyId = "DataOwnerCenterKey";
        public string ServerURI { get; set; }
        public string OwnerKey { get; set; }
        public string DataOwnerKey { get; set; }
        public string DataOwnerCenterKey { get; set; }

        public void GetServerInfo(JobDataMap dataMap)
        {
            ServerURI = dataMap.GetString(ServerURIKey);
            OwnerKey = dataMap.GetString(OwnerKeyId);
            DataOwnerKey = dataMap.GetString(DataOwnerKeyId);
            DataOwnerCenterKey = dataMap.GetString(DataOwnerCenterKeyId);
        }
    }
}
