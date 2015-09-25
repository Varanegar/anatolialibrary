using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfAppServer.Common.Interfaces
{
    public interface IWcfEndpoint
    {
        [DataMember]
        string Address { get; }

        [DataMember]
        string Binding { get; }

        [DataMember]
        string Contract { get; }

        [OperationContract]
        string ToString();
    }
}