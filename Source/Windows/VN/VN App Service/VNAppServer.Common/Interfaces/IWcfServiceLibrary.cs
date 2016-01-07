using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VNAppServer.Common.Interfaces
{
    public interface IWcfServiceLibrary
    {
        [DataMember]
        string FilePath { get; set; }

        [DataMember]
        List<IWcfServiceConfig> WcfServiceConfigs { get; set; }
    }
}