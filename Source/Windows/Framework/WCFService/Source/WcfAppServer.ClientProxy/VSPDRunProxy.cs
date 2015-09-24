using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfAppServer.Common.ClientProxies;
using WcfAppServer.Common.Interfaces;
using System.ServiceModel.Channels;
using System.ServiceModel;
using WcfAppServer.ClientProxy.ClientProxies;
using WcfAppServer.Common.Enums;
using System.Data;

namespace WcfAppServer.ClientProxy
{
    
    public sealed class VSPDRunProxy
    {
        private static VSPDRunProxy instance = null;
        private POSHardCodedConfigService configService;
        private AppServerAdminClient adminClient;

        private VSPDRunProxy()
        {
            if (VNApplicationServer == null)
            {
                VNApplicationServer = GetServerName();
            }
            configService = new POSHardCodedConfigService(VNApplicationServer);
            adminClient = GetAdminClient(configService);

        }

        
        public static string VNApplicationServer = null;
        private string GetServerName()
        {
            DataTable datatable = new DataTable();
            DataColumn c;
            c = datatable.Columns.Add("CompanyName");
            c.DataType = typeof(string);
            c = datatable.Columns.Add("ConnectionString");
            c.DataType = typeof(string);

            try
            {
                string ServerName = "";

                DataSet ds = new DataSet();
                ds.ReadXml(AppDomain.CurrentDomain.BaseDirectory + @"\Databases.xml");

                DataTable dt = ds.Tables["add"];

                if (dt.Columns.Contains("CompanySettings_id"))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["key"].ToString() == "ServerName")
                            ServerName = dr["value"].ToString();
                    }
                    return ServerName;
                }
            }
            catch (Exception e)
            {
            }
            return "";
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

        public static VSPDRunProxy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VSPDRunProxy();
                }
                return instance;
            }
        }


        private static string GetErrorMessage(string aMSG)
        {
            if ((aMSG.IndexOf("A network-related or instance-specific error occurred while establishing a connection to SQL Server. The ") != -1)
                ||
                (aMSG.IndexOf(" requested by the login. The login failed.") != -1)
                )
            {
                return ("به دلیل عدم برقراری ارتباط با ستاد، امکان انجام عملیات مورد نظر، وجود ندارد.");
            }
            else
            {
                return "خطا در برقراری ارتباط با سرور، لطفا دوباره سعی فرمایید" + "\r\n" + aMSG;
            }
        }


        public string ExecuteScalarFromDB(string SQLCommand, string aConnectionString)
        {
            try
            {
                IServiceStatus s1Status = adminClient.GetServiceStatus("service#1");
                ServerCommandResponse scr = ServerCommandResponse.NotSet;


                if (s1Status.State != "Opened")
                {
                    scr = adminClient.OpenService("service#1");
                }
                else
                    scr = ServerCommandResponse.Success;

                if (scr == ServerCommandResponse.Success)
                {

                    return GetVSPDClient(configService).ExecuteScalarFromDB(SQLCommand, aConnectionString);

                }
            }
            catch (Exception e3)
            {
                adminClient.Abort();
                adminClient = GetAdminClient(configService);
/*
                if (
                                   (e3.GetType().FullName == "System.ServiceModel.CommunicationObjectFaultedException")
                                   ||
                                   (e3.GetType().FullName == "System.ServiceModel.CommunicationException")
                                   )
                {
                    adminClient.Abort();
                    adminClient = GetAdminClient(configService);
                    throw new Exception("خطا در برقراری ارتباط با سرور، لطفا دوباره سعی فرمایید");

                } */
                throw new Exception(GetErrorMessage(e3.Message));

            }
            return null;
        }

        public string ExecuteToDB(string Query, string aConnectionString)
        {
            try
            {
                IServiceStatus s1Status = adminClient.GetServiceStatus("service#1");
                ServerCommandResponse scr = ServerCommandResponse.NotSet;
                if (s1Status.State != "Opened")
                {
                    scr = adminClient.OpenService("service#1");
                }
                else
                    scr = ServerCommandResponse.Success;

                if (scr == ServerCommandResponse.Success)
                {
                    try
                    {
                        return GetVSPDClient(configService).ExecuteToDB(Query, aConnectionString);
                    }
                    catch (Exception e3)
                    {
                        throw new Exception(GetErrorMessage(e3.Message));
                    }
                }

            }
            catch (Exception e3)
            {
                adminClient.Abort();
                adminClient = GetAdminClient(configService);
               /*
                if (
                                   (e3.GetType().FullName == "System.ServiceModel.CommunicationObjectFaultedException")
                                   ||
                                   (e3.GetType().FullName == "System.ServiceModel.CommunicationException")
                                   )
                {
                    adminClient.Abort();
                    adminClient = GetAdminClient(configService);
                    throw new Exception("خطا در برقراری ارتباط با سرور، لطفا دوباره سعی فرمایید");

                }*/ 
                throw new Exception(GetErrorMessage(e3.Message));
            }

            return null;
        }

        public DataSet ExecuteReaderFromDB(string Query, string aConnectionString)
        {
            try
            {
               


                IServiceStatus s1Status = adminClient.GetServiceStatus("service#1");
                ServerCommandResponse scr = ServerCommandResponse.NotSet;

                if (s1Status.State != "Opened")
                {
                    scr = adminClient.OpenService("service#1");
                }
                else
                    scr = ServerCommandResponse.Success;

                if (scr == ServerCommandResponse.Success)
                {
                    try
                    {
                        return GetVSPDClient(configService).ExecuteReaderFromDB(Query, aConnectionString);

                    }
                    catch (Exception e3)
                    {

                        throw new Exception(GetErrorMessage(e3.Message));

                    }
                }
                else
                    return null;
            }


            catch (Exception e3)
            {
                adminClient.Abort();
                adminClient = GetAdminClient(configService);

                /*if (
                                   (e3.GetType().FullName == "System.ServiceModel.CommunicationObjectFaultedException")
                                   ||
                                   (e3.GetType().FullName == "System.ServiceModel.CommunicationException")
                                   )
                {
                    adminClient.Abort();
                    adminClient = GetAdminClient(configService);
                    throw new Exception("خطا در برقراری ارتباط با سرور، لطفا دوباره سعی فرمایید");

                } 
                 */
                throw new Exception(GetErrorMessage(e3.Message));
            }
        }
    }
}
