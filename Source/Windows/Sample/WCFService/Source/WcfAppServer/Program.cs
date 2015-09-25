using System;
using System.Diagnostics;
using System.ServiceProcess;
using WcfAppServer.Logging;

namespace WcfAppServer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main()
        {
            ServiceBase[] ServicesToRun;
            var vnApplicationServer = new VNApplicationServer();
            ServicesToRun = new ServiceBase[]
                                {
                                    vnApplicationServer
                                };
            try
            {
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception e)
            {
                LoggingService.Logger.Trace("Program.Main");
                LoggingService.Logger.Error(e);
                vnApplicationServer.ExitCode = 1;
                vnApplicationServer.Stop();
            }
            finally
            {
                vnApplicationServer.Dispose();
            }
        }
    }
}