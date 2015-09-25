using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfAppServer.Interfaces
{
    public interface IWcfServiceLibrary
    {
        [DataMember]
        string FilePath { get; set; }

        [DataMember]
        IList<IWcfServiceConfig> WcfServiceConfigs { get; set; }
    }
}