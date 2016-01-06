using System;
using System.Runtime.Serialization;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Common.DTOs
{
    [DataContract]
    [Serializable]
    public sealed class WcfService : IWcfService
    {
        private string _serviceId;

        #region IWcfService Members

        [DataMember]
        string IWcfService.ServiceId
        {
            get { return _serviceId; }
            set { _serviceId = value.ToLower(); }
        }

        [DataMember]
        string IWcfService.ServiceAssemblyName { get; set; }

        [DataMember]
        string IWcfService.ServiceClassName { get; set; }

        [DataMember]
        string IWcfService.ContractAssemblyName { get; set; }

        [DataMember]
        string IWcfService.ContractClassName { get; set; }

        #endregion

        #region Factory

        public static IWcfService Create(string serviceId,
                                         string serviceAssemblyName,
                                         string serviceClassName,
                                         string contractAssemblyName,
                                         string contractClassName)
        {
            IWcfService wcfService = new WcfService();
            wcfService.ServiceId = serviceId;
            wcfService.ServiceAssemblyName = serviceAssemblyName;
            wcfService.ServiceClassName = serviceClassName;
            wcfService.ContractAssemblyName = contractAssemblyName;
            wcfService.ContractClassName = contractClassName;

            return wcfService;
        }

        #endregion
    }
}