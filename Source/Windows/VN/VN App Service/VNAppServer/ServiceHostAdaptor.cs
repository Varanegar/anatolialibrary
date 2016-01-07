using log4net;
using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using VNAppServer.Common.DTOs;
using VNAppServer.Common.Enums;
using VNAppServer.Common.Interfaces;
using VNAppServer.Configuration;
using VNAppServer.Interfaces;

namespace VNAppServer
{
    public class ServiceActionEventArgs : EventArgs
    {
        public readonly ServiceAction Action;

        public ServiceActionEventArgs(ServiceAction action)
        {
            Action = action;
        }
    }

    public delegate void ServiceHostAdaptorEventHandler(object sender, ServiceActionEventArgs e);

    public sealed class ServiceHostAdaptor : MarshalByRefObject, IDisposable, IServiceHostAdaptor
    {
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private ServiceHost _serviceHost;

        public ServiceHostAdaptor()
        {

            Aborted += OnAction;
            Aborting += OnActionBegin;
            Closed += OnAction;
            Closing += OnActionBegin;
            Opened += OnAction;
            Opening += OnActionBegin;
            Created += OnCreated;
        }

        #region IDisposable Members

        public void Dispose()
        {
            log.Info(string.Format("Service being disposed... {0}", ServiceId));
            Close();
            ((IDisposable)_serviceHost).Dispose();
        }

        #endregion

        #region IServiceHostAdaptor Members

        public event ServiceHostAdaptorEventHandler Aborted;
        public event ServiceHostAdaptorEventHandler Aborting;
        public event ServiceHostAdaptorEventHandler Closed;
        public event ServiceHostAdaptorEventHandler Closing;
        public event ServiceHostAdaptorEventHandler Opened;
        public event ServiceHostAdaptorEventHandler Opening;
        public event ServiceHostAdaptorEventHandler Created;

        //#endregion

        public string ServiceId { get; set; }

        public AppDomain GetAppDomain()
        {
            return AppDomain.CurrentDomain;
        }

        public void CreateHost(string serviceId, Object singletonInstance, Type contractType, Uri[] baseAddresses,
                               Stream bindingStream, bool addMex, bool includeExceptionDetailsInFaults)
        {
            _serviceHost = new ServiceHost(singletonInstance, baseAddresses);
            SetupServiceEndpointAndBehaviour(serviceId, bindingStream, contractType, addMex,
                                             includeExceptionDetailsInFaults);
            Created(this, null);
        }

        public void CreateHost(string serviceId, Type serviceType, Type contractType, Uri[] baseAddresses,
                               Stream bindingStream, bool addMex, bool includeExceptionDetailsInFaults)
        {
            _serviceHost = new ServiceHost(serviceType, baseAddresses);
            SetupServiceEndpointAndBehaviour(serviceId, bindingStream, contractType, addMex,
                                             includeExceptionDetailsInFaults);
            Created(this, null);
        }

        public void Open()
        {
            if (_serviceHost.State != CommunicationState.Created &&
                _serviceHost.State != CommunicationState.Closed)
                return;

            Opening(this, new ServiceActionEventArgs(ServiceAction.Opening));

            _serviceHost.Open();

            Opened(this, new ServiceActionEventArgs(ServiceAction.Opened));
        }

        public void Close()
        {
            if (_serviceHost.State != CommunicationState.Opened)
                return;

            Closing(this, new ServiceActionEventArgs(ServiceAction.Closing));

            _serviceHost.Close();

            Closed(this, new ServiceActionEventArgs(ServiceAction.Closed));
        }

        public void Abort()
        {
            Aborting(this, new ServiceActionEventArgs(ServiceAction.Aborting));

            _serviceHost.Abort();

            Aborted(this, new ServiceActionEventArgs(ServiceAction.Aborted));
        }

        public void AddAllMexEndPoints()
        {
            var metadataBehavior = _serviceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (metadataBehavior == null)
            {
                metadataBehavior = new ServiceMetadataBehavior();
                _serviceHost.Description.Behaviors.Add(metadataBehavior);
            }

            foreach (Uri baseAddress in _serviceHost.BaseAddresses)
            {
                Binding binding = null;
                switch (baseAddress.Scheme)
                {
                    case "net.tcp":
                        binding = MetadataExchangeBindings.CreateMexTcpBinding();
                        break;

                    case "net.pipe":
                        binding = MetadataExchangeBindings.CreateMexNamedPipeBinding();
                        break;

                    case "http":
                        binding = MetadataExchangeBindings.CreateMexHttpBinding();
                        metadataBehavior.HttpGetEnabled = true;
                        break;

                    case "https":
                        binding = MetadataExchangeBindings.CreateMexHttpsBinding();
                        metadataBehavior.HttpGetEnabled = true;
                        break;
                }

                if (binding != null)
                    _serviceHost.AddServiceEndpoint(typeof (IMetadataExchange), binding, "MEX");
            }
        }

        public bool IncludeExceptionDetailInFaults
        {
            set
            {
                if (_serviceHost.State == CommunicationState.Opened)
                    throw new FaultException<InvalidOperationException>(
                        new InvalidOperationException("Host is already opened"));

                var debuggingBehavior = _serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
                debuggingBehavior.IncludeExceptionDetailInFaults = value;
            }
            get
            {
                var debuggingBehavior = _serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>();
                return debuggingBehavior.IncludeExceptionDetailInFaults;
            }
        }

        public bool HasMexEndpoint
        {
            get
            {
                return
                    _serviceHost.Description.Endpoints.Any(
                        endpoint => endpoint.Contract.ContractType == typeof (IMetadataExchange));
            }
        }

        public string State
        {
            get { return _serviceHost.State.ToString(); }
        }

        public IServiceStatus Status
        {
            get
            {
                IServiceStatus serviceStatus = ServiceStatus.Create(ServiceId, _serviceHost.State);

                foreach (ServiceEndpoint serviceEndpoint in _serviceHost.Description.Endpoints)
                {
                    IWcfEndpoint wcfEndpoint = WcfEndpoint.Create(serviceEndpoint.Address.ToString(),
                                                                  serviceEndpoint.Binding.Name,
                                                                  serviceEndpoint.Contract.Name);

                    serviceStatus.Endpoints.Add(wcfEndpoint);
                }
                return serviceStatus;
            }
        }

        #endregion

        private void SetupServiceEndpointAndBehaviour(string serviceId, Stream bindingStream, Type contractType,
                                                      bool addMex, bool includeExceptionDetailsInFaults)
        {
            ServiceId = serviceId;
            AddServiceEndpoint(bindingStream, contractType);

            //if (addMex)
            //    AddAllMexEndPoints();

            if (includeExceptionDetailsInFaults)
                IncludeExceptionDetailInFaults = true;
        }

        private void AddServiceEndpoint(Stream bindingStream, Type contractType)
        {
            Binding binding = WcfBindingHelper.Deserialize(bindingStream);
            _serviceHost.AddServiceEndpoint(contractType, binding, "");
        }

        // By overriding this method no lease will be created
        public override Object InitializeLifetimeService()
        {
            #region How to create a lease object (commented out code)

            //ILease lease = (ILease) base.InitializeLifetimeService();
            //if (lease.CurrentState == LeaseState.Initial)
            //{
            //    lease.InitialLeaseTime = TimeSpan.FromDays(7);
            //    lease.SponsorshipTimeout = TimeSpan.FromDays(7);
            //    lease.RenewOnCallTime = TimeSpan.FromDays(7);
            //}
            //return lease;

            #endregion

            return null;
        }

        public void OnCreated(object o, ServiceActionEventArgs e)
        {
            log.Info(string.Format("Service {0} created with the following endpoints:",
                                                      ((IServiceHostAdaptor) o).ServiceId));

            IServiceStatus status = ((IServiceHostAdaptor) o).Status;

            foreach (IWcfEndpoint endpoint in status.Endpoints)
                log.Info(endpoint.ToString());
        }

        public static void OnAction(object o, ServiceActionEventArgs e)
        {
            log.Info(string.Format("WCF Application Service: {0} {1}",
                                                     ((IServiceHostAdaptor) o).ServiceId, e.Action));
        }

        public static void OnActionBegin(object o, ServiceActionEventArgs e)
        {
            log.Info(string.Format("WCF Application Service: {0} {1}",
                                                      ((IServiceHostAdaptor) o).ServiceId, e.Action));
        }
    }
}