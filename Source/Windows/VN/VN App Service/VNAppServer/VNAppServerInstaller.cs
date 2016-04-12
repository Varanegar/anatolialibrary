using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace VNAppServer
{
    [RunInstaller(true)]
    public class VNAppServerInstaller : Installer
    {
        public VNAppServerInstaller()
        {

            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetAssembly(typeof(VNAppServerInstaller)).Location);
            var ServiceNameStr = (config.AppSettings.Settings["ServiceName"].Value == null ? "VNApplicationServerV2" : config.AppSettings.Settings["ServiceName"].Value.ToString());
            var DisplayNameStr = (config.AppSettings.Settings["DisplayName"].Value == null ? "VN Application Server V2" : config.AppSettings.Settings["DisplayName"].Value.ToString());
            var serviceProcessInstaller = new ServiceProcessInstaller
                                              {
                                                  Account = ServiceAccount.LocalSystem,
                                                  Password = null,
                                                  Username = null
                                              };

            var serviceInstaller = new ServiceInstaller
                                       {
                                           DisplayName = DisplayNameStr,
                                           ServiceName = ServiceNameStr,
                                           StartType = ServiceStartMode.Automatic
                                       };

            Installers.AddRange(new Installer[]
                                    {
                                        serviceProcessInstaller,
                                        serviceInstaller
                                    });
        }
    }
}