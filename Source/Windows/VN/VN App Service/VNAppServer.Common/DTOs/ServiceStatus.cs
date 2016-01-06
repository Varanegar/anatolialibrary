using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Common.DTOs
{
    [DataContract]
    [Serializable]
    public sealed class ServiceStatus : IServiceStatus
    {
        #region IServiceStatus Members

        [DataMember]
        public string ServiceId { get; private set; }

        [DataMember]
        public string State { get; private set; }

        [DataMember]
        public IList<IWcfEndpoint> Endpoints { get; private set; }

        [OperationContract]
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