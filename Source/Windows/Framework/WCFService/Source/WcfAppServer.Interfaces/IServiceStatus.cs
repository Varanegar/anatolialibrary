using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfAppServer.Interfaces
{
    public interface IServiceStatus
    {
        [DataMember]
        string ServiceId { get; }

        [DataMember]
        string State { get; }

        [DataMember]
        IList<IWcfEndpoint> Endpoints { get; }

        string ToString();
    }
}