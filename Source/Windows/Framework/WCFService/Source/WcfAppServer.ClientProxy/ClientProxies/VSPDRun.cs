using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfAppServer.Configuration;
using WcfAppServer.Common.Interfaces;
using System.ServiceModel.Channels;
using System.ServiceModel;
using WcfAppServer.Common.ClientProxies;
using System.Data;
using WcfAppServer.Common.Enums;

namespace WcfAppServer.ClientProxy.ClientProxies
{
   
    public sealed class     
    {
        private static VSPDRun instance = null;
        private SQLiteConfigService configService;
        private AppServerAdminClient adminClient;

        private VSPDRun()
        {
            configService = new SQLiteConfigService();
             adminClient = GetAdminClient(configService);

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

        private static VSPDClient GetVSPDClient(IConfigService configService)
        {
            configService.GetWcfServiceLibraries();
            IWcfServiceConfig adminConfig = configService.GetWcfServiceLibraries().Find(x => x.WcfServiceConfigs[0].WcfService.ServiceId == "service#1").WcfServiceConfigs[0];
            Binding binding = adminConfig.Binding;
            var thirtyMinutes = new TimeSpan(0, 0, 30, 0);
            binding.OpenTimeout = thirtyMinutes;
            binding.ReceiveTimeout = thirtyMinutes;
            binding.SendTimeout = thirtyMinutes;
            var address = new EndpointAddress(adminConfig.Address);

            return new VSPDClient(binding, address);
        }

        public static VSPDRun Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VSPDRun();
                }
                return instance;
            }
        }

        public string ExecuteScalarFromDB(string SQLCommand, string aConnectionString)
        {
            IServiceStatus s1Status = adminClient.GetServiceStatus("service#1");
            ServerCommandResponse scr = ServerCommandResponse.NotSet;
                            
            if(s1Status.State != "Opened")
            {
                scr = adminClient.OpenService("service#1");
            }
            else 
                scr = ServerCommandResponse.Success;

            if (scr == ServerCommandResponse.Success)
            {
                return GetVSPDClient(configService).ExecuteScalarFromDB(SQLCommand, aConnectionString);
            }
            return null;
        }

        public string ExecuteToDB(string Query, string aConnectionString)
        {
            IServiceStatus s1Status = adminClient.GetServiceStatus("service#1");
            ServerCommandResponse scr = ServerCommandResponse.NotSet;
                            
            if(s1Status.State != "Opened")
            {
                scr = adminClient.OpenService("service#1");
            }
            else 
                scr = ServerCommandResponse.Success;

            if (scr == ServerCommandResponse.Success)
            {
                return GetVSPDClient(configService).ExecuteToDB(Query, aConnectionString);
            }
            return null;
        }

        public DataTable ExecuteReaderFromDB(string Query, string aConnectionString)
        {
            IServiceStatus s1Status = adminClient.GetServiceStatus("service#1");
            ServerCommandResponse scr = ServerCommandResponse.NotSet;
                            
            if(s1Status.State != "Opened")
            {
                scr = adminClient.OpenService("service#1");
            }
            else 
                scr = ServerCommandResponse.Success;

            if (scr == ServerCommandResponse.Success)
            {
                return GetVSPDClient(configService).ExecuteReaderFromDB(Query, aConnectionString).Tables[0];
            }
            else
                return null;
        }
    } 
}
