using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Common.DTOs
{
    [DataContract]
    [Serializable]
    public sealed class WcfEndpoint : IWcfEndpoint
    {
        #region IWcfEndpoint Members

        [DataMember]
        public string Address { get; private set; }

        [DataMember]
        public string Binding { get; private set; }

        [DataMember]
        public string Contract { get; private set; }

        [OperationContract]
        public new string ToString()
        {
            return string.Format("A: {0}, B: {1} C: {2}",
                                 Address,
                                 Binding,
                                 Contract);
        }

        #endregion

        public static IWcfEndpoint Create(string address, string binding, string contract)
        {
            IWcfEndpoint wcfEndpoint = new WcfEndpoint
                                           {
                                               Address = address,
                                               Binding = binding,
                                               Contract = contract
                                           };
            return wcfEndpoint;
        }
    }
}