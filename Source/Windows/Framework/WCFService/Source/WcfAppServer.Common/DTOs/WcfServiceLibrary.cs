using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WcfAppServer.Common.Interfaces;

namespace WcfAppServer.Common.DTOs
{
    [DataContract]
    [Serializable]
    public sealed class WcfServiceLibrary : IWcfServiceLibrary
    {
        #region IWcfServiceLibrary Members

        [DataMember]
        string IWcfServiceLibrary.FilePath { get; set; }

        [DataMember]
        List<IWcfServiceConfig> IWcfServiceLibrary.WcfServiceConfigs { get; set; }

        #endregion

        #region Factory

        public static IWcfServiceLibrary Create(string filePath,
                                                params IWcfServiceConfig[] wcfServiceConfigs)
        {
            IWcfServiceLibrary wcfServiceLibrary = new WcfServiceLibrary();
            
            wcfServiceLibrary.FilePath = filePath;
            
            wcfServiceLibrary.WcfServiceConfigs = new List<IWcfServiceConfig>();

            if (wcfServiceConfigs != null)
                ((List<IWcfServiceConfig>) wcfServiceLibrary.WcfServiceConfigs).AddRange(wcfServiceConfigs);

            return wcfServiceLibrary;
        }

        #endregion
    }
}