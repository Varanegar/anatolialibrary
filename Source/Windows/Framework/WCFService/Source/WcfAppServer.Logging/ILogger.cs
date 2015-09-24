using System;

namespace WcfAppServer.Logging
{
    public interface ILogger
    {
        void Trace(string message);
        void Debug(string message);
        void Info(string message);
        void Warn(string message);
        void Error(string message);
        void Error(Exception e);
        void Fatal(string message);
        void Fatal(Exception e);
    }
}