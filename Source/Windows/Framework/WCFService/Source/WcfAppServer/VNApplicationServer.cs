using System;
using System.ServiceProcess;
using WcfAppServer.Common.Interfaces;
using WcfAppServer.Configuration;
using WcfAppServer.Logging;

namespace WcfAppServer
{
    public class VNApplicationServer : ServiceBase
    {
        private AppServer _appServer;

        protected override void OnStart(string[] args)
        {
            try
            {
                LogStartupMessage();

                IConfigService config = new SQLiteConfigService();

                _appServer = new AppServer(config);
                
                _appServer.StartUpServer();
            }
            catch (Exception e)
            {
                LoggingService.Logger.Error(e);
                
                this.Stop();
            }
            finally
            {
                LoggingService.Logger.Trace("Leaving VN Application Server Service OnStart");
            }
        }

        private void LogStartupMessage()
        {
            LoggingService.Logger.Trace("**************************************************");
            LoggingService.Logger.Trace("VN Application Server Service starting up");
            LoggingService.Logger.Trace("**************************************************");
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
                LoggingService.Logger.Error(e);
            }
            finally
            {
                LoggingService.Logger.Trace("Leaving VN Application Server Service OnStop");
            }
        }

        private void LogShutdownMessage()
        {
            LoggingService.Logger.Trace("**************************************************");
            LoggingService.Logger.Trace("VN Application Server Service stopping");
            LoggingService.Logger.Trace("**************************************************");
        }

        public new void Dispose()
        {
            _appServer.Dispose();
        }
    }
}