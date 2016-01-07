using log4net;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using VNAppServer.Common.Enums;
using VNAppServer.Common.Interfaces;
using VNAppServer.Interfaces;

namespace VNAppServer
{
    [ServiceBehavior(
        ConcurrencyMode = ConcurrencyMode.Single,
        InstanceContextMode = InstanceContextMode.Single
        )]
    public sealed class AppServer : IAppServerAdmin, IDisposable
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IConfigService _configService;
        private IServiceHostAdaptor _adminServiceHost;
        private IScheduler scheduler;

        public AppServer(IConfigService configService)
        {
            _configService = configService;
        }

        private List<IServiceHostAdaptor> AppServiceHosts { get; set; }

        #region IAppServerAdmin Members

        public string GetAdminServiceState()
        {
            return _adminServiceHost.State;
        }

        public ServerCommandResponse OpenService(string id)
        {
            IServiceHostAdaptor appServiceHost = GetAppServiceHost(id);

            if (appServiceHost == null)
                return ServerCommandResponse.ServiceUnknown;

            appServiceHost.Open();

            return ServerCommandResponse.Success;
        }


        public ServerCommandResponse CloseService(string id)
        {
            IServiceHostAdaptor appServiceHost = GetAppServiceHost(id);

            if (appServiceHost == null)
                return ServerCommandResponse.ServiceUnknown;

            CloseAndRecreateServiceHost(id, appServiceHost);

            return ServerCommandResponse.Success;
        }

        public ServerCommandResponse RecycleService(string id)
        {
            IServiceHostAdaptor appServiceHost = GetAppServiceHost(id);

            if (appServiceHost == null)
                return ServerCommandResponse.ServiceUnknown;

            appServiceHost = CloseAndRecreateServiceHost(id, appServiceHost);

            appServiceHost.Open();

            return ServerCommandResponse.Success;
        }

        public IList<IServiceStatus> GetServiceStatuses()
        {
            if (AppServiceHosts == null)
                return null;

            IList<IServiceStatus> serviceStatuses = new List<IServiceStatus>(AppServiceHosts.Count);

            foreach (IServiceHostAdaptor t in AppServiceHosts)
                serviceStatuses.Add(t.Status);

            return serviceStatuses;
        }

        public IServiceStatus GetServiceStatus(string id)
        {
            IServiceHostAdaptor appServiceHost = GetAppServiceHost(id);
            if (appServiceHost == null)
                return null;

            return appServiceHost.Status;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            ShutdownServer();

            if (_adminServiceHost != null)
            {
                ((IDisposable) _adminServiceHost).Dispose();
            }
        }

        #endregion

        private IServiceHostAdaptor CloseAndRecreateServiceHost(string id, IServiceHostAdaptor appServiceHost)
        {
            var appDomain = appServiceHost.GetAppDomain();

            appServiceHost.Close();

            AppServiceHosts.Remove(appServiceHost);

            ((IDisposable) appServiceHost).Dispose();

            if (appDomain != null) AppDomain.Unload(appDomain);

            var serviceConfig = _configService.GetWcfService(id);
            
            appServiceHost = CreateAppDomainHost(serviceConfig);
            
            AppServiceHosts.Add(appServiceHost);
            
            return appServiceHost;
        }

        private IServiceHostAdaptor GetAppServiceHost(string serviceId)
        {
            foreach (IServiceHostAdaptor appServiceHost in AppServiceHosts)
            {
                if (appServiceHost == null)
                {
                    log.Info("The admin service has detected a service has left the WCF application pool");
                    continue;
                }

                if (appServiceHost.ServiceId.ToLower() == serviceId.ToLower())
                    return appServiceHost;
            }

            return null;
        }

        private void OpenAdminService()
        {
            IWcfServiceConfig adminConfig = _configService.GetAppServerAdmin();

            if (adminConfig == null)
                throw new Exception("The configuration service failed to provide an Admin config");

            Type contractType = typeof(IAppServerAdmin);

            var baseAddresses = new[] { new Uri(adminConfig.Address) };

            _adminServiceHost = ServiceHostFactory.CreateHost("Admin Service",
                                                                this,
                                                                contractType,
                                                                baseAddresses,
                                                                adminConfig.Binding,
                                                                adminConfig.IncludeMex,
                                                                adminConfig.IncludeExceptionDetailInFault);
            _adminServiceHost.Open();
        }

        public void StartUpServer()
        {
            var restul = log4net.Config.XmlConfigurator.Configure();

            OpenAdminService();

            StartupSchedulerService();

            StartupApplicationServices();
        }

        private void StartupSchedulerService()
        {
            // Grab the Scheduler instance from the Factory 
            scheduler = StdSchedulerFactory.GetDefaultScheduler();

            // and start it off
            scheduler.Start();

            IList<ITimerLibrary> timerLibraries = CallConfigTimer();

            if (timerLibraries.Count == 0)
                log.Info("There were no WcfServiceLibraries returned by the config service");

            foreach (ITimerLibrary timerLibrary in timerLibraries)
            {
                foreach (ITimerConfig config in timerLibrary.TimerConfigs)
                {
                    CreateTimerJob(scheduler, config, ConfigurationManager.AppSettings["ServerURI"].ToString()
                        , ConfigurationManager.AppSettings["PrivateOwnerId"].ToString());
                }
            }
        }
        private void StartupApplicationServices()
        {
            AppServiceHosts = new List<IServiceHostAdaptor>();

            IList<IWcfServiceLibrary> wcfServiceLibraries = CallConfigService();

            if (wcfServiceLibraries.Count==0) 
                log.Info("There were no WcfServiceLibraries returned by the config service");

            foreach (IWcfServiceLibrary wcfServiceLibrary in wcfServiceLibraries)
            {
                //CopyLibraryLocal(wcfServiceLibrary.FilePath);

                foreach (IWcfServiceConfig config in wcfServiceLibrary.WcfServiceConfigs)
                {
                    IServiceHostAdaptor appServiceHost = CreateAppDomainHost(config);
                    AppServiceHosts.Add(appServiceHost);
                }
            }
        }

        private IList<IWcfServiceLibrary> CallConfigService()
        {
            IList<IWcfServiceLibrary> serviceLibraries = _configService.GetWcfServiceLibraries();

            // linq to check here that all serviceIds are unique
            // throw exception if not unique

            return serviceLibraries;
        }

        private IList<ITimerLibrary> CallConfigTimer()
        {
            IList<ITimerLibrary> serviceLibraries = _configService.GetTimerLibraries();

            // linq to check here that all serviceIds are unique
            // throw exception if not unique

            return serviceLibraries;
        }

        public void ShutdownServer()
        {
            log.Info("Shutting down server");

            if (AppServiceHosts != null)
            {
                log.Info("Closing application service hosts");

                foreach (IServiceHostAdaptor appServiceHost in AppServiceHosts)
                {
                    if (appServiceHost != null)
                        appServiceHost.Close();
                }
            }

            if (_adminServiceHost != null)
            {
                log.Info("Closing admin service host");
            
                _adminServiceHost.Close();
            }

            if (scheduler != null && !scheduler.IsShutdown)
                scheduler.Shutdown();
        }

        private static IServiceHostAdaptor CreateAppDomainHost(IWcfServiceConfig config)
        {
            IWcfService service = config.WcfService;

            Assembly assembly = Assembly.ReflectionOnlyLoad(service.ServiceAssemblyName);

            Type serviceType = GetTypeFromAssembly(service.ServiceAssemblyName, service.ServiceClassName, assembly);

            Type contractType = GetTypeFromAssembly(service.ContractAssemblyName, service.ContractClassName, assembly);

            // currently using ServiceId as the app domain name
            string appDomainName = string.Format("AppDomain for service {0} {1}", serviceType.FullName, Guid.NewGuid());

            var baseAddresses = new[] { new Uri(config.Address) };

            IServiceHostAdaptor appDomainHost =
                ServiceHostFactory.CreateAppDomainHost(appDomainName,
                                                       config.WcfService.ServiceId,
                                                       serviceType,
                                                       contractType,
                                                       baseAddresses,
                                                       config.Binding,
                                                       config.IncludeMex,
                                                       config.IncludeExceptionDetailInFault);

            return appDomainHost;
        }

        private static void CreateTimerJob(IScheduler scheduler, ITimerConfig config, string serverURI, string privateOwnerId)
        {
            ITimerJob service = config.TimerJob;

            Assembly assembly = Assembly.Load(service.TimerAssemblyName);

            Type timerType = GetTypeFromAssembly(service.TimerAssemblyName, service.TimerClassName, assembly);

            // currently using ServiceId as the app domain name
            string appDomainName = string.Format("AppDomain for timer {0} {1}", timerType.FullName, Guid.NewGuid());

            SchedulerFactory.CreateScheduler(scheduler,  appDomainName,
                                                    config.GroupId,
                                                    config.TriggerId,
                                                    config.CronExpression,
                                                    timerType,
                                                    serverURI,
                                                    privateOwnerId);
        }

        private static Type GetTypeFromAssembly(string assemblyName, string typeName, Assembly assembly)
        {
            return assembly.GetType(string.Format("{0}.{1}", assemblyName, typeName));
        }
    }
}