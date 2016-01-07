using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace VNAppServer.Common.Interfaces
{
    public interface IServiceStatus
    {
        [DataMember]
        string ServiceId { get; }

        [DataMember]
        string State { get; }

        [DataMember]
        IList<IWcfEndpoint> Endpoints { get; }

        [OperationContract]
        string ToString();
    }
}