using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using VNAppServer.Common.DTOs;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Configuration
{
    [ServiceContract]
    public class HardCodedConfigService : AbstractConfigService, IConfigService
    {
        public HardCodedConfigService()
        {
            //add first demo service
            IWcfServiceConfig wcfServiceConfig = GetWcfServiceConfig1();
            ListOfServiceConfigs.Add(wcfServiceConfig.WcfService.ServiceId, wcfServiceConfig);

            //add second demo service
            wcfServiceConfig = GetWcfServiceConfig2();
            ListOfServiceConfigs.Add(wcfServiceConfig.WcfService.ServiceId, wcfServiceConfig);

            //add third demo service
            wcfServiceConfig = GetWcfServiceConfig3();
            ListOfServiceConfigs.Add(wcfServiceConfig.WcfService.ServiceId, wcfServiceConfig);
        }

        #region IConfigService Members

        public new IWcfServiceConfig GetAppServerAdmin()
        {
            string address = "net.tcp://localhost:8050/VNAppServer/AppServer";
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
            IWcfServiceConfig wcfServiceConfig2 = GetWcfServiceConfig2();
            IWcfServiceConfig wcfServiceConfig3 = GetWcfServiceConfig3();

            // add configs to 1st library
            IWcfServiceLibrary wcfServiceLibrary1 = WcfServiceLibrary.Create(
                @".\DropFolderForLibraries\WcfServiceLibrary1.dll",
                new[] {wcfServiceConfig1, wcfServiceConfig2}
                );

            // add configs to 2nd library
            IWcfServiceLibrary wcfServiceLibrary2 = WcfServiceLibrary.Create(
                @".\DropFolderForLibraries\WcfServiceLibrary2.dll",
                new[] {wcfServiceConfig3}
                );

            List<IWcfServiceLibrary> wcfServiceLibraries = new List<IWcfServiceLibrary>(2)
                                                                {
                                                                    wcfServiceLibrary1,
                                                                    wcfServiceLibrary2
                                                                };
            return wcfServiceLibraries;
        }

        #endregion

        private static IWcfServiceConfig GetWcfServiceConfig1()
        {
            string address = "net.tcp://localhost:8731/WcfServiceLibrary1/Service1";
            Binding binding = WcfBindingHelper.GetInferredBinding(address, false);
            IWcfService wcfService = WcfService.Create("Service#1",
                                                       "WcfServiceLibrary1",
                                                       "Service1",
                                                       "WcfServiceLibrary1",
                                                       "IService1");

            return WcfServiceConfig.Create(
                address, binding, wcfService, false, true);
        }

        private static IWcfServiceConfig GetWcfServiceConfig2()
        {
            IWcfService wcfService;
            Binding binding;
            string address;

            // configure 2nd service
            address = "http://localhost:8732/WcfServiceLibrary1/Service2";
            binding = WcfBindingHelper.GetInferredBinding(address, false);
            wcfService = WcfService.Create("Service#2",
                                           "WcfServiceLibrary1",
                                           "Service2",
                                           "WcfServiceLibrary1",
                                           "IService2");

            return WcfServiceConfig.Create(
                address, binding, wcfService, true, true);
        }

        private static IWcfServiceConfig GetWcfServiceConfig3()
        {
            IWcfService wcfService;
            Binding binding;
            string address;

            // configure 3rd service
            address = "http://localhost:8733/WcfServiceLibrary1/Service2";
            binding = WcfBindingHelper.GetInferredBinding(address, false);
            wcfService = WcfService.Create("Service#3",
                                           "WcfServiceLibrary1",
                                           "Service2",
                                           "WcfServiceLibrary1",
                                           "IService2");

            return WcfServiceConfig.Create(
                address, binding, wcfService, true, true);
        }


        public List<ITimerLibrary> GetTimerLibraries()
        {
            throw new System.NotImplementedException();
        }
    }
}