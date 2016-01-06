using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using VNAppServer.Common.Enums;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Common.ClientProxies
{
    public class AppServerAdminClient : ClientBase<IAppServerAdmin>, IAppServerAdmin
    {
        public AppServerAdminClient()
        {
        }

        public AppServerAdminClient(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        #region IAppServerAdmin Members

        public string GetAdminServiceState()
        {
            return Channel.GetAdminServiceState();
        }

        public ServerCommandResponse OpenService(string id)
        {
            return Channel.OpenService(id);
        }

        public ServerCommandResponse CloseService(string id)
        {
            return Channel.CloseService(id);
        }

        public ServerCommandResponse RecycleService(string id)
        {
            return Channel.RecycleService(id);
        }

        public IList<IServiceStatus> GetServiceStatuses()
        {
            return Channel.GetServiceStatuses();
        }

        public IServiceStatus GetServiceStatus(string id)
        {
            return Channel.GetServiceStatus(id);
        }

        #endregion
    }
}