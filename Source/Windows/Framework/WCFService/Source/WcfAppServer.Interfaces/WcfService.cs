using WcfAppServer.Interfaces;

namespace WcfAppServer
{
    public class WcfService : IWcfService
    {
        #region IWcfService Members

        string IWcfService.ServiceId { get; set; }
        string IWcfService.ServiceAssemblyName { get; set; }
        string IWcfService.ServiceClassName { get; set; }
        string IWcfService.ContractAssemblyName { get; set; }
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