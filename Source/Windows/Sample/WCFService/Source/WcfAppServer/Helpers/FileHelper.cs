using System;
using System.IO;
using WcfAppServer.Logging;

namespace WcfAppServer.Helpers
{
    public static class FileHelper
    {
        public static void CopyFile(string sourceFile, string destinationFile)
        {
            LoggingService.Logger.Trace(string.Format("Copying {0} to {1}", sourceFile,
                                                      destinationFile));

            if (File.Exists(destinationFile))
            {
                LoggingService.Logger.Trace("File already exists, deleting file");

                try
                {
                    File.Delete(destinationFile);
                    LoggingService.Logger.Trace("File deleted");
                }
                catch
                {
                    var message = string.Format("Could not delete file {0}, please make sure your service logon account has the correct priviledges", destinationFile);
                    LoggingService.Logger.Error(message);
                    throw;
                }
            }

            try
            {
                File.Copy(sourceFile, destinationFile);
                LoggingService.Logger.Trace("Copy complete");
            }
            catch
            {
                var message = string.Format("Could not copy file from {0} to {1}, please make sure your service logon account has the correct priviledges", sourceFile, destinationFile);
                LoggingService.Logger.Error(message);
                throw;
            }
        }
    }
}