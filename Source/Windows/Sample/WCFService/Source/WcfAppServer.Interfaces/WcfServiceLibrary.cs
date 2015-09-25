using System.Collections.Generic;
using WcfAppServer.Interfaces;

namespace WcfAppServer
{
    public class WcfServiceLibrary : IWcfServiceLibrary
    {
        #region IWcfServiceLibrary Members

        string IWcfServiceLibrary.FilePath { get; set; }
        IList<IWcfServiceConfig> IWcfServiceLibrary.WcfServiceConfigs { get; set; }

        #endregion

        #region Factory

        public static IWcfServiceLibrary Create(string filePath, IList<IWcfServiceConfig> wcfServiceConfigs)
        {
            IWcfServiceLibrary wcfServiceLibrary = new WcfServiceLibrary();
            wcfServiceLibrary.FilePath = filePath;
            wcfServiceLibrary.WcfServiceConfigs = wcfServiceConfigs;
            return wcfServiceLibrary;
        }

        #endregion
    }
}