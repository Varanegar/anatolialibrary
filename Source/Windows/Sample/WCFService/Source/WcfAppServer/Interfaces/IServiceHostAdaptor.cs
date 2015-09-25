using System;
using System.IO;
using WcfAppServer.Common.Interfaces;

namespace WcfAppServer.Interfaces
{
    public interface IServiceHostAdaptor
    {
        string ServiceId { get; set; }
        bool IncludeExceptionDetailInFaults { set; get; }
        bool HasMexEndpoint { get; }
        string State { get; }
        IServiceStatus Status { get; }
        event ServiceHostAdaptorEventHandler Aborted;
        event ServiceHostAdaptorEventHandler Aborting;
        event ServiceHostAdaptorEventHandler Closed;
        event ServiceHostAdaptorEventHandler Closing;
        event ServiceHostAdaptorEventHandler Opened;
        event ServiceHostAdaptorEventHandler Opening;
        event ServiceHostAdaptorEventHandler Created;
        AppDomain GetAppDomain();

        void CreateHost(string serviceId, Object singletonInstance, Type contractType, Uri[] baseAddresses,
                        Stream bindingStream, bool addMex, bool includeExceptionDetailsInFaults);

        void CreateHost(string serviceId, Type serviceType, Type contractType, Uri[] baseAddresses, Stream bindingStream,
                        bool addMex, bool includeExceptionDetailsInFaults);

        void Open();
        void Close();
        void Abort();
        void AddAllMexEndPoints();
    }
}