using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using WcfAppServer.Interfaces;

namespace WcfAppServer
{
    [DataContract]
    [Serializable]
    public class ServiceStatus : IServiceStatus
    {
        #region IServiceStatus Members

        public string ServiceId { get; private set; }
        public string State { get; private set; }
        public IList<IWcfEndpoint> Endpoints { get; private set; }

        public new string ToString()
        {
            return string.Format(" ServiceId: {0}, State: {1}",
                                 ServiceId,
                                 State);
        }

        #endregion

        public static IServiceStatus Create(string serviceId, CommunicationState state)
        {
            IServiceStatus serviceStatus = new ServiceStatus
                                               {
                                                   ServiceId = serviceId,
                                                   State = state.ToString(),
                                                   Endpoints = new List<IWcfEndpoint>(0)
                                               };
            return serviceStatus;
        }
    }
}