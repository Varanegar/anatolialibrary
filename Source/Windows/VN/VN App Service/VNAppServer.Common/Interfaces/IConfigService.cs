using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace VNAppServer.Common.Interfaces
{
    public interface IConfigService
    {
        [DataMember]
        Dictionary<string, IWcfServiceConfig> ListAllService { get; }
        [DataMember]
        Dictionary<string, ITimerConfig> ListAllTimer { get; }

        [OperationContract]
        IWcfServiceConfig GetAppServerAdmin();

        [OperationContract]
        IWcfServiceConfig GetWcfService(string id);

        [OperationContract]
        List<IWcfServiceLibrary> GetWcfServiceLibraries();

        [OperationContract]
        List<ITimerLibrary> GetTimerLibraries();
    }
}