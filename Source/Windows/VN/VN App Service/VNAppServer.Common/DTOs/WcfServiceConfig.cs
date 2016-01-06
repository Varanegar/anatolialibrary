using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Common.DTOs
{
    [DataContract]
    [Serializable]
    public sealed class WcfServiceConfig : IWcfServiceConfig
    {
        #region IWcfServiceConfig Members

        [DataMember]
        string IWcfServiceConfig.Address { get; set; }

        [DataMember]
        Binding IWcfServiceConfig.Binding { get; set; }

        [DataMember]
        IWcfService IWcfServiceConfig.WcfService { get; set; }

        [DataMember]
        bool IWcfServiceConfig.IncludeMex { get; set; }

        [DataMember]
        bool IWcfServiceConfig.IncludeExceptionDetailInFault { get; set; }

        #endregion

        #region Factory

        public static IWcfServiceConfig Create(string address, Binding binding, IWcfService wcfService, bool includeMex,
                                               bool includeExceptionDetailInFault)
        {
            IWcfServiceConfig wcfServiceConfig = new WcfServiceConfig();

            wcfServiceConfig.Address = address;
            wcfServiceConfig.Binding = binding;
            wcfServiceConfig.WcfService = wcfService;
            wcfServiceConfig.IncludeMex = includeMex;
            wcfServiceConfig.IncludeExceptionDetailInFault = includeExceptionDetailInFault;
            return wcfServiceConfig;
        }

        #endregion
    }
}