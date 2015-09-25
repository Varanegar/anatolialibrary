using System.Collections.Generic;
using System.ServiceModel;
using WcfAppServer.Interfaces.Exceptions;

namespace WcfAppServer.Interfaces
{
    [ServiceContract]
    public interface IAppServer
    {
        [OperationContract]
        [FaultContract(typeof (ServiceNotFoundException))]
        bool OpenService(string id);

        [OperationContract]
        [FaultContract(typeof (ServiceNotFoundException))]
        bool CloseService(string id);

        [OperationContract]
        IList<IServiceStatus> GetServiceStatuses();

        [OperationContract]
        IServiceStatus GetServiceStatus(string serviceId);

        [OperationContract]
        IList<string> GetServiceStatusesToString();

        [OperationContract]
        IList<string> GetServiceStatusToString(string serviceId);
    }
}