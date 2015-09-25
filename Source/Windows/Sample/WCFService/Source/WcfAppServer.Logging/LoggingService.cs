using System;

namespace WcfAppServer.Logging
{
    public static class LoggingService
    {
        private static ILogger _logger;

        public static ILogger Logger
        {
            get { return _logger ?? new NLogLogger(); }
            set { _logger = value; }
        }

        public static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error((Exception) e.ExceptionObject);
        }
    }
}