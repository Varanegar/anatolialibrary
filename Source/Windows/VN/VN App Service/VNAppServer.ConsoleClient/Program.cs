using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using Ninject;
using VNAppServer.Common.ClientProxies;
using VNAppServer.Common.Enums;
using VNAppServer.Common.Interfaces;
using VNAppServer.Configuration;
//using VNAppServer.ConsoleClient.ClientProxies;
using System.Data;

namespace VNAppServer.ConsoleClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var configService = IoC.Container.Get<IConfigService>();
                
                AppServerAdminClient adminClient = GetAdminClient(configService);
                WelcomeMessage(configService, adminClient);

                RunDashboard(configService, adminClient);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                WaitForUser();
            }
            finally
            {
                Console.WriteLine("Press [ENTER] to exit console client");
                Console.ReadKey();
            }
        }

        private static void WaitForUser()
        {
            Console.WriteLine("An exception occurred, press [ENTER] to continue");
            Console.ReadKey();
        }

        /*
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
        }*/
        //private static Service1Client GetService1Client(IConfigService configService)
        //{
        //    configService.GetWcfServiceLibraries();
        //    IWcfServiceConfig adminConfig = configService.GetWcfServiceLibraries().Find(x => x.WcfServiceConfigs[0].WcfService.ServiceId == "service#1").WcfServiceConfigs[0];
        //    Binding binding = adminConfig.Binding;
        //    var thirtyMinutes = new TimeSpan(0, 0, 30, 0);
        //    binding.OpenTimeout = thirtyMinutes;
        //    binding.ReceiveTimeout = thirtyMinutes;
        //    binding.SendTimeout = thirtyMinutes;
        //    var address = new EndpointAddress(adminConfig.Address);

        //    return new Service1Client(binding, address);
        //}

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

        private static void RunDashboard(
            IConfigService configService,
            AppServerAdminClient adminClient)
        {
            bool continueRunningDashboard = true;


            do
            {
                try
                {
                    string key = AskUserForNextAction();

                    ServerCommandResponse response;
                    string serviceId;
                    switch (key.ToLower())
                    {
                        case "1":
                            Console.Write("Enter the service name you wish to open:  ");
                            serviceId = Console.ReadLine();

                            response = adminClient.OpenService(serviceId);
                            Console.WriteLine("Service response: {0}", response);
                            break;

                        case "2":
                            Console.Write("Enter the service name you wish to close:  ");
                            serviceId = Console.ReadLine();
                            response = adminClient.CloseService(serviceId);
                            Console.WriteLine("Service response: {0}", response);
                            break;

                        case "3":
                            Console.Write("Enter the service name you wish to recycle:  ");
                            serviceId = Console.ReadLine();
                            response = adminClient.RecycleService(serviceId);
                            Console.WriteLine("Service response: {0}", response);
                            break;

                        case "4":
                            Console.Write("Enter the service name you wish to show the status of:  ");
                            serviceId = Console.ReadLine();
                            IServiceStatus status = adminClient.GetServiceStatus(serviceId);

                            if (status == null)
                            {
                                Console.WriteLine("Unknown service");
                                break;
                            }

                            Console.WriteLine("Service response: {0}", status.ToString());
                            break;

                        case "5":
                            IList<IServiceStatus> statuses = adminClient.GetServiceStatuses();

                            if (statuses == null)
                            {
                                Console.WriteLine("The WCF App Server does not currently contain any services");
                                break;
                            }

                            foreach (IServiceStatus serviceStatus in statuses)
                                Console.WriteLine(serviceStatus.ToString());
                            break;
                        case "6":
                            //IServiceStatus s1Status = adminClient.GetServiceStatus("service#1");
                            //ServerCommandResponse scr = ServerCommandResponse.NotSet;
                            
                            //if(s1Status.State != "Opened")
                            //{
                            //    scr = adminClient.OpenService("service#1");
                            //}
                            //else 
                            //    scr = ServerCommandResponse.Success;

                            //if(scr == ServerCommandResponse.Success)
                            //{
                            //    VSPDClient s1 = GetVSPDClient(configService);
                            //    //object dt = s1.ExecuteScalarFromDB(null, null);
                            //        object dt2 = s1.ExecuteReaderFromDB(null, null);
                            //    /*try
                            //    {*/
                            //    /*}
                            //    catch
                            //    { }
                            //    try
                            //    {
                            //    }
                            //    catch
                            //    { }
                            //    try
                            //    {
                            //        object dt = s1.ExecuteToDB(null, null);
                            //    }
                            //    catch
                            //    { }
                            //    */
                                //object dt2 = VSPDRunTemplate.Instance.ExecuteScalarFromDB("select count(*) from customer",
                                //        @"Password=totalcommander;Persist Security Info=True;User ID=PMC;Initial Catalog=S_CN;Data Source=.\db;");
                                //Console.WriteLine(dt2.ToString() + " - ok");//dt.TableName
                            
                            break;
                        case "q":
                            continueRunningDashboard = false;
                            Console.WriteLine("Quitting dashboard");

                            try
                            {
                                adminClient.Close();
                            }
                            catch
                            {
                                // don't worry if the host has already been shutdown
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid entry");
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.Write("Exception: {0}", e);
                    adminClient = GetAdminClient(configService);
                }

                Console.WriteLine();
            } while (continueRunningDashboard);
        }

        private static void WelcomeMessage(IConfigService configService,
                                           IAppServerAdmin adminClient)
        {
            string latestAdminServiceState = string.Empty,
                   adminServiceState = string.Empty;

            Console.WriteLine("Welcome to the VN WCF App Server Client Dashboard");
            Console.WriteLine();
            Console.WriteLine("Please wait while the WCF Server admin service starts up...");

            // allow host to start
            //Pause(10);

            do
            {
                latestAdminServiceState = adminClient.GetAdminServiceState();

                if (latestAdminServiceState != adminServiceState)
                    Console.WriteLine("  " + latestAdminServiceState);

                adminServiceState = latestAdminServiceState;

                if (latestAdminServiceState == "Aborted" ||
                    latestAdminServiceState == "Closed")
                    throw new Exception("Admin service state " + latestAdminServiceState);

                Thread.Sleep(1000);
            } while (latestAdminServiceState != "Opened");

            Console.WriteLine();
            Console.WriteLine("For this example the WCF App Server is pre-loaded with the following services:");

            foreach (var serviceConfig in configService.ListAllService)
                Console.WriteLine("  " + serviceConfig.Key);

            Console.WriteLine();
            Console.WriteLine("For further info on these services take a look at HardCodedConfigService.cs");
            Console.WriteLine(" or SQLiteConfigService.cs in the VNAppServer.Configuration project.");
            Console.WriteLine();
        }

        private static void Pause(int seconds)
        {
            for (int i = 0; i < seconds; i++)
                Thread.Sleep(1000);
        }

        private static string AskUserForNextAction()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("  1. Open service");
            Console.WriteLine("  2. Close service");
            Console.WriteLine("  3. Recycle service");
            Console.WriteLine("  4. Show specific service status");
            Console.WriteLine("  5. Show all available service statuses");
            Console.WriteLine("  Q. Quit");
            Console.Write("Enter 1 to 5:  ");

            string key = Console.ReadLine();
            Console.WriteLine();
            return key == null ? string.Empty : key.Trim();
        }
    }
}