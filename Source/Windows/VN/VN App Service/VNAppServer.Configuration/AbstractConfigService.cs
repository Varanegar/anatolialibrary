using System.Collections.Generic;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Configuration
{
    public class AbstractConfigService
    {
        internal readonly Dictionary<string, IWcfServiceConfig> ListOfServiceConfigs;
        internal readonly Dictionary<string, ITimerConfig> ListOfTimerConfigs;
        public IWcfServiceConfig GetAppServerAdmin;
        public List<IWcfServiceLibrary> GetWcfServiceLibraries;
        public List<ITimerLibrary> GetTimterLibraries;

        public AbstractConfigService()
        {
            ListOfServiceConfigs = new Dictionary<string, IWcfServiceConfig>();
            ListOfTimerConfigs = new Dictionary<string, ITimerConfig>();
        }

        public Dictionary<string, IWcfServiceConfig> ListAllService
        {
            get { return ListOfServiceConfigs; }
        }

        public Dictionary<string, ITimerConfig> ListAllTimer
        {
            get { return ListOfTimerConfigs; }
        }

        public IWcfServiceConfig GetWcfService(string id)
        {
            return ListOfServiceConfigs[id];
        }

        public ITimerConfig GetTimer(string id)
        {
            return ListOfTimerConfigs[id];
        }
    }
}