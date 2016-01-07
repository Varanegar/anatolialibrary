using System.Runtime.Serialization;

namespace VNAppServer.Common.Interfaces
{
    public interface IWcfService
    {
        [DataMember]
        string ServiceId { get; set; }

        [DataMember]
        string ServiceAssemblyName { get; set; }

        [DataMember]
        string ServiceClassName { get; set; }

        [DataMember]
        string ContractAssemblyName { get; set; }

        [DataMember]
        string ContractClassName { get; set; }
    }
}