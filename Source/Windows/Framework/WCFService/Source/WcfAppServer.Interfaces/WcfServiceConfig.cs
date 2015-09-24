using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using WcfAppServer.Interfaces;

namespace WcfAppServer
{
    [DataContract]
    public class WcfServiceConfig : IWcfServiceConfig
    {
        #region IWcfServiceConfig Members

        [DataMember]
        string IWcfServiceConfig.Address { get; set; }

        [DataMember]
        Binding IWcfServiceConfig.Binding { get; set; }

        [DataMember]
        IWcfService IWcfServiceConfig.WcfService { get; set; }

        #endregion

        #region Factory

        public static IWcfServiceConfig Create(string address, Binding binding, IWcfService wcfService)
        {
            IWcfServiceConfig wcfServiceConfig = new WcfServiceConfig();

            wcfServiceConfig.Address = address;
            wcfServiceConfig.Binding = binding;
            wcfServiceConfig.WcfService = wcfService;

            return wcfServiceConfig;
        }

        #endregion
    }
}