using System;
using System.IO;
using System.Reflection;
using System.ServiceModel.Channels;
using VNAppServer.Configuration;
using VNAppServer.Interfaces;

namespace VNAppServer
{
    public static class ServiceHostFactory
    {
        public static IServiceHostAdaptor CreateHost(string serviceId, Object singletonInstance,
                                                     Type contractType, Uri[] baseAddresses, Binding binding,
                                                     bool addMex, bool includeExceptionDetailsInFaults)
        {
            var serviceHostAdaptor = new ServiceHostAdaptor();
            Stream bindingStream = WcfBindingHelper.SerializeToStream(binding);
            serviceHostAdaptor.CreateHost(serviceId,
                                          singletonInstance,
                                          contractType,
                                          baseAddresses,
                                          bindingStream,
                                          addMex,
                                          includeExceptionDetailsInFaults);
            return serviceHostAdaptor;
        }

        public static IServiceHostAdaptor CreateAppDomainHost(string appDomainName, string serviceId, Type serviceType,
                                                              Type contractType, Uri[] baseAddresses, Binding binding,
                                                              bool addMex, bool includeExceptionDetailsInFaults)
        {
            string assemblyName = Assembly.GetAssembly(typeof (ServiceHostAdaptor)).FullName;
            AppDomain appDomain = AppDomain.CreateDomain(appDomainName);
            var _serviceHostAdaptor =
                appDomain.CreateInstanceAndUnwrap(assemblyName, typeof (ServiceHostAdaptor).ToString()) as
                ServiceHostAdaptor;
            
            Stream bindingStream = WcfBindingHelper.SerializeToStream(binding);

            _serviceHostAdaptor.CreateHost(serviceId, serviceType, contractType, baseAddresses, bindingStream, addMex,
                                           includeExceptionDetailsInFaults);

            return _serviceHostAdaptor;
        }
    }
}