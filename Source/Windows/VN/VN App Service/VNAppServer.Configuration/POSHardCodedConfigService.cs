using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using VNAppServer.Common.DTOs;
using VNAppServer.Common.Interfaces;
using VNAppServer.Configuration;

namespace VNAppServer.ClientProxy
{
    [ServiceContract]
    public class POSHardCodedConfigService : AbstractConfigService, IConfigService
    {
        public string ip;
        public POSHardCodedConfigService(string sIp)
        {
            //add first demo service
            ip = sIp;
            IWcfServiceConfig wcfServiceConfig = GetWcfServiceConfig1();
            ListOfServiceConfigs.Add(wcfServiceConfig.WcfService.ServiceId, wcfServiceConfig);

        }

        #region IConfigService Members

        public new IWcfServiceConfig GetAppServerAdmin()
        {
            string address = "net.tcp://" + ip + ":8050/VNAppServer/AppServer";
            Binding binding = WcfBindingHelper.GetInferredBinding(address, false);

            IWcfService wcfService = WcfService.Create(
                "WCF App Server Admin Service",
                "VNAppServer",
                "AppServer",
                "VNAppServer",
                "IAppServer");

            IWcfServiceConfig wcfServiceConfig = WcfServiceConfig.Create(
                address, binding, wcfService, false, true);

            return wcfServiceConfig;
        }

        public new List<IWcfServiceLibrary> GetWcfServiceLibraries()
        {
            // configure 1st service
            IWcfServiceConfig wcfServiceConfig1 = GetWcfServiceConfig1();

            // add configs to 1st library
            IWcfServiceLibrary wcfServiceLibrary1 = WcfServiceLibrary.Create(
                @"VSPD.dll",
                new[] {wcfServiceConfig1, wcfServiceConfig1}
                );

            List<IWcfServiceLibrary> wcfServiceLibraries = new List<IWcfServiceLibrary>(2)
                                                                {
                                                                    wcfServiceLibrary1
                                                                };
            return wcfServiceLibraries;
        }

        #endregion

        private IWcfServiceConfig GetWcfServiceConfig1()
        {
            string address = "net.tcp://" + ip + ":8731/VSPD/VSPDCls";
            Binding binding = WcfBindingHelper.GetInferredBinding(address, false);
            IWcfService wcfService = WcfService.Create("Service#1",
                                                       "VSPD",
                                                       "VSPDBCls",
                                                       "VSPD",
                                                       "IVSPDBCls");

            return WcfServiceConfig.Create(
                address, binding, wcfService, false, true);
        }



        public List<ITimerLibrary> GetTimerLibraries()
        {
            throw new System.NotImplementedException();
        }
    }
}