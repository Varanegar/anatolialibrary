using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using WcfAppServer.Common.Enums;
using WcfAppServer.Common.Interfaces;
using WcfServiceLibrary1;

namespace WcfAppServer.ConsoleClient.ClientProxies
{
    public class Service1Client : ClientBase<IService1>, IService1
    {
        public Service1Client()
        {
        }

        public Service1Client(Binding binding, EndpointAddress address)
            : base(binding, address)
        {
        }

        public string GetData(int value)
        {
            return Channel.GetData(value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            return Channel.GetDataUsingDataContract(new CompositeType());
        }
    }
}