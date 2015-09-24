using System.Runtime.Serialization;

namespace WcfAppServer.Interfaces
{
    public interface IWcfEndpoint
    {
        [DataMember]
        string Address { get; }

        [DataMember]
        string Binding { get; }

        [DataMember]
        string Contract { get; }

        string ToString();
    }
}