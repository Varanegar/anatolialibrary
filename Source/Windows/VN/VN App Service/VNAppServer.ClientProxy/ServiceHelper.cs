using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VNAppServer.Common.ClientProxies;
using VNAppServer.Common.Enums;
using VNAppServer.Common.Interfaces;
using VNAppServer.Configuration;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;

namespace VNAppServer.ClientProxy
{
    public class ServiceHelper
    {
        private static ServiceInfo GetServiceInfo(IConfigService configService, string serviceId)
        {
            configService.GetWcfServiceLibraries();
            IWcfServiceConfig serviceConfig = configService.GetWcfServiceLibraries().Find(x => x.WcfServiceConfigs[0].WcfService.ServiceId == serviceId).WcfServiceConfigs[0];
            Binding binding = serviceConfig.Binding;
            var thirtyMinutes = new TimeSpan(0, 0, 30, 0);
            binding.OpenTimeout = thirtyMinutes;
            binding.ReceiveTimeout = thirtyMinutes;
            binding.SendTimeout = thirtyMinutes;
            var address = new EndpointAddress(serviceConfig.Address);

            return new ServiceInfo(binding, address);
        }

        private static AppServerAdminClient GetAdminClient(IConfigService configService)
        {
            IWcfServiceConfig adminConfig = configService.GetAppServerAdmin();
            Binding binding = adminConfig.Binding;
            var thirtyMinutes = new TimeSpan(0, 0, 30, 0);
            binding.OpenTimeout = thirtyMinutes;
            binding.ReceiveTimeout = thirtyMinutes;
            binding.SendTimeout = thirtyMinutes;
            var address = new EndpointAddress(adminConfig.Address);

            return new AppServerAdminClient(binding, address);
        }

        private static bool isAdminServiceLive(AppServerAdminClient adminClient)
        {
            
            string adminServiceState = string.Empty;

            do
            {
                adminServiceState = adminClient.GetAdminServiceState();

                if (adminServiceState == "Aborted" ||
                    adminServiceState == "Closed")
                    return false;

                Thread.Sleep(100);
            } while (adminServiceState != "Opened");
            return true;

        }

        public static ServerCommandResponse isRequestedServiceLive(string serviceId, AppServerAdminClient adminClient)
        {
            IServiceStatus s1Status = adminClient.GetServiceStatus(serviceId);
            ServerCommandResponse scr = ServerCommandResponse.NotSet;
                            
            if(s1Status.State != "Opened")
            {
                scr = adminClient.OpenService(serviceId);
            }
            else 
                scr = ServerCommandResponse.Success;
            return scr;
        }

        public static ServiceInfo GetServiceInfo(string serviceId, string ip)
        {
            SQLiteConfigService configService = new SQLiteConfigService();
            //POSHardCodedConfigService configService = new POSHardCodedConfigService(ip);
            AppServerAdminClient adminClient = GetAdminClient(configService);
            if (isAdminServiceLive(adminClient))
            {
                ServerCommandResponse scr = isRequestedServiceLive(serviceId, adminClient);
                if (scr == ServerCommandResponse.Success)
                {
                    return GetServiceInfo(configService, serviceId);

                }
            }
            return null;
        }
    }
}
