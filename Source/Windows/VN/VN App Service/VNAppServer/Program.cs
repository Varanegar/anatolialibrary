using log4net;
using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;

namespace VNAppServer
{
    internal static class Program
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
                log.Error("Program.Main");
                log.Error(e);
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