using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using WcfAppServer.Common.Enums;
using WcfAppServer.Common.Interfaces;
using WcfAppServer.Helpers;
using WcfAppServer.Interfaces;
using WcfAppServer.Logging;

namespace WcfAppServer
{
    [ServiceBehavior(
        ConcurrencyMode = ConcurrencyMode.Single,
        InstanceContextMode = InstanceContextMode.Single
        )]
    public sealed class AppServer : IAppServerAdmin, IDisposable
    {
        private readonly IConfigService _configService;
        private IServiceHostAdaptor _adminServiceHost;

        public AppServer(IConfigService configService)
        {
            AppDomain.CurrentDomain.UnhandledException += LoggingService.UnhandledExceptionTrapper;
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
                    LoggingService.Logger.Info("The admin service has detected a service has left the WCF application pool");
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
            OpenAdminService();

            StartupApplicationServices();
        }

        private void StartupApplicationServices()
        {
            AppServiceHosts = new List<IServiceHostAdaptor>();

            IList<IWcfServiceLibrary> wcfServiceLibraries = CallConfigService();

            if (wcfServiceLibraries.Count==0) 
                LoggingService.Logger.Info("There were no WcfServiceLibraries returned by the config service");

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

        public void ShutdownServer()
        {
            LoggingService.Logger.Trace("Shutting down server");

            if (AppServiceHosts != null)
            {
                LoggingService.Logger.Trace("Closing application service hosts");

                foreach (IServiceHostAdaptor appServiceHost in AppServiceHosts)
                {
                    if (appServiceHost != null)
                        appServiceHost.Close();
                }
            }

            if (_adminServiceHost != null)
            {
                LoggingService.Logger.Trace("Closing admin service host");
            
                _adminServiceHost.Close();
            }
        }

        private static void CopyLibraryLocal(string sourceFile)
        {
            string sourceFilename = Path.GetFileName(sourceFile);
            
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            
            string destinationFile = Path.Combine(currentPath, sourceFilename);

            FileHelper.CopyFile(sourceFile, destinationFile);
        }

        private static IServiceHostAdaptor CreateAppDomainHost(IWcfServiceConfig config)
        {
            IWcfService service = config.WcfService;
            
            Assembly assembly = Assembly.ReflectionOnlyLoad(service.ServiceAssemblyName);
            
            Type serviceType = GetTypeFromAssembly(service.ServiceAssemblyName, service.ServiceClassName, assembly);
            
            Type contractType = GetTypeFromAssembly(service.ContractAssemblyName, service.ContractClassName, assembly);

            // currently using ServiceId as the app domain name
            string appDomainName = string.Format("AppDomain for service {0} {1}", serviceType.FullName, Guid.NewGuid());

            var baseAddresses = new[] {new Uri(config.Address)};

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

        private static Type GetTypeFromAssembly(string assemblyName, string typeName, Assembly assembly)
        {
            return assembly.GetType(string.Format("{0}.{1}", assemblyName, typeName));
        }
    }
}