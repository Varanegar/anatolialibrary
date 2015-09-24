using System.Collections.Generic;
using WcfAppServer.Common.Interfaces;

namespace WcfAppServer.Configuration
{
    public class AbstractConfigService
    {
        internal readonly Dictionary<string, IWcfServiceConfig> ListOfServiceConfigs;
        public IWcfServiceConfig GetAppServerAdmin;
        public List<IWcfServiceLibrary> GetWcfServiceLibraries;

        public AbstractConfigService()
        {
            ListOfServiceConfigs = new Dictionary<string, IWcfServiceConfig>();
        }

        public Dictionary<string, IWcfServiceConfig> ListAll
        {
            get { return ListOfServiceConfigs; }
        }

        public IWcfServiceConfig GetWcfService(string id)
        {
            return ListOfServiceConfigs[id];
        }
    }
}