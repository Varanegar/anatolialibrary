using log4net;
using System;
using System.ServiceProcess;
using VNAppServer.Common.Interfaces;
using VNAppServer.Configuration;

namespace VNAppServer
{
    public class VNApplicationServer : ServiceBase
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private AppServer _appServer;

        protected override void OnStart(string[] args)
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();

                LogStartupMessage();

                IConfigService config = new SQLiteConfigService();

                _appServer = new AppServer(config);
                
                _appServer.StartUpServer();
            }
            catch (Exception e)
            {
                log.Error(e);
                
                this.Stop();
            }
            finally
            {
                log.Info("Leaving VN Application Server Service OnStart");
            }
        }

        private void LogStartupMessage()
        {
            log.Info("**************************************************");
            log.Info("VN Application Server Service starting up");
            log.Info("**************************************************");
        }

        protected override void OnStop()
        {
            try
            {
                LogShutdownMessage();

                if (_appServer != null)
                    _appServer.ShutdownServer();
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            finally
            {
                log.Info("Leaving VN Application Server Service OnStop");
            }
        }

        private void LogShutdownMessage()
        {
            log.Info("**************************************************");
            log.Info("VN Application Server Service stopping");
            log.Info("**************************************************");
        }

        public new void Dispose()
        {
            _appServer.Dispose();
        }
    }
}