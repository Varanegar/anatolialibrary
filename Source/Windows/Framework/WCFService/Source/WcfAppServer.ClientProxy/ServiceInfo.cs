using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace WcfAppServer.ClientProxy
{
    public class ServiceInfo
    {
        public ServiceInfo(Binding binding, EndpointAddress address)
        {
            this.address = address;
            this.binding = binding;
        }

        public Binding binding = null;
        public EndpointAddress address = null;
    }
}
