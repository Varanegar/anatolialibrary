using System;

namespace WcfAppServer.Logging
{
    public class MicrosoftLogger : ILogger
    {
        #region ILogger Members

        void ILogger.Trace(string message)
        {
            //Trace.WriteLine();
            throw new NotImplementedException();
        }

        void ILogger.Debug(string message)
        {
            throw new NotImplementedException();
        }

        void ILogger.Info(string message)
        {
            throw new NotImplementedException();
        }

        void ILogger.Warn(string message)
        {
            throw new NotImplementedException();
        }

        void ILogger.Error(string message)
        {
            throw new NotImplementedException();
        }

        void ILogger.Error(Exception e)
        {
            throw new NotImplementedException();
        }

        void ILogger.Fatal(string message)
        {
            throw new NotImplementedException();
        }

        void ILogger.Fatal(Exception e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}