using System.Collections.Generic;
using System.ServiceModel;
using WcfAppServer.Common.DTOs;
using WcfAppServer.Common.Enums;

namespace WcfAppServer.Common.Interfaces
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