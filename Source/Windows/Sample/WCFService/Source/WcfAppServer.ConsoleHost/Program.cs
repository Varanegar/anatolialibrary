using System;
using WcfAppServer.Configuration;

namespace WcfAppServer.ConsoleHost
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                WelcomeMessage();

                using (var appServer = new AppServer(new SQLiteConfigService()))
                {
                    appServer.StartUpServer();

                    WaitForUser();

                    appServer.ShutdownServer();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                WaitForUser();
            }
            finally
            {
                Console.WriteLine("Press [ENTER] to exit the console host");
                Console.ReadKey();
            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the VN WCF App Server");
            Console.WriteLine();
        }

        private static void WaitForUser()
        {
            Console.WriteLine("Press [ENTER] to shutdown app server");
            Console.ReadKey();
        }
    }
}