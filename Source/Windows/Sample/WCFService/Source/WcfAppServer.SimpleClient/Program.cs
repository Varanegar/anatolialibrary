using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfAppServer.ClientProxy;
using WcfAppServer.ClientProxy.ClientProxies;

namespace WcfAppServer.SimpleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceInfo si = ServiceHelper.GetServiceInfo("service#1", "127.0.0.1");
            if (si != null)
            {
                //Service1Client s1Client = new Service1Client(si.binding, si.address);
             //   Console.WriteLine(s1Client.GetData(12));
               // string s = Console.ReadLine();
            }
        }
    }
}
