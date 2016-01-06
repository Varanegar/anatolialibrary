using System.Collections.Generic;
using System.ServiceModel;
using VNAppServer.Common.DTOs;
using VNAppServer.Common.Enums;

namespace VNAppServer.Common.Interfaces
{
    [ServiceContract]
    public interface IAppServerAdmin
    {
        [OperationContract]
        string GetAdminServiceState();

        [OperationContract]
        ServerCommandResponse OpenService(string id);

        [OperationContract]
        ServerCommandResponse CloseService(string id);

        [OperationContract]
        ServerCommandResponse RecycleService(string id);

        [OperationContract]
        [ServiceKnownType(typeof (ServiceStatus))]
        [ServiceKnownType(typeof (WcfEndpoint))]
        IList<IServiceStatus> GetServiceStatuses();

        [OperationContract]
        [ServiceKnownType(typeof (ServiceStatus))]
        [ServiceKnownType(typeof (WcfEndpoint))]
        IServiceStatus GetServiceStatus(string id);
    }
}