using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace VNAppServer
{
    [RunInstaller(true)]
    public class WcfApplicationServerInstaller : Installer
    {
        public WcfApplicationServerInstaller()
        {
            var serviceProcessInstaller = new ServiceProcessInstaller
                                              {
                                                  Account = ServiceAccount.LocalSystem,
                                                  Password = null,
                                                  Username = null
                                              };

            var serviceInstaller = new ServiceInstaller
                                       {
                                           DisplayName = "VN Application Server V2",
                                           ServiceName = "VNApplicationServerV2",
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