using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WcfAppServer.Common.Interfaces
{
    public interface IConfigService
    {
        [DataMember]
        Dictionary<string, IWcfServiceConfig> ListAll { get; }

        [OperationContract]
        IWcfServiceConfig GetAppServerAdmin();

        [OperationContract]
        IWcfServiceConfig GetWcfService(string id);

        [OperationContract]
        List<IWcfServiceLibrary> GetWcfServiceLibraries();
    }
}