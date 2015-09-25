using System;
using System.Text;

namespace WcfAppServer.Logging
{
    public static class ExceptionHelper
    {
        public static string Format(Exception e)
        {
            if (e == null)
                e = new Exception("Unknown exception");

            var sb = new StringBuilder();

            sb.AppendLine(string.Format("Exception message: {0}", e.Message));
            sb.AppendLine(string.Format("   Occurred in: {0}.{1}", e.Source, e.TargetSite));
            sb.AppendLine(string.Format("   Stack trace: {0}", e.StackTrace));

            if (e.InnerException != null)
                sb.AppendLine(string.Format("   Inner exception: {0}", Format(e.InnerException)));

            return sb.ToString();
        }
    }
}