using System;

namespace WcfAppServer.Interfaces.Exceptions
{
    [Serializable]
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException(string serviceId)
            : base(serviceId)
        {
        }

        public ServiceNotFoundException(string serviceId, Exception innerException)
            : base(serviceId, innerException)
        {
        }
    }
}