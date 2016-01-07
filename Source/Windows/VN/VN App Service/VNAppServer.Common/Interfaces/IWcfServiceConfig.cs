using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace VNAppServer.Common.Interfaces
{
    public interface IWcfServiceConfig
    {
        [DataMember]
        IWcfService WcfService { get; set; }

        [DataMember]
        string Address { get; set; }

        [DataMember]
        Binding Binding { get; set; }

        [DataMember]
        bool IncludeMex { get; set; }

        [DataMember]
        bool IncludeExceptionDetailInFault { get; set; }
    }
}