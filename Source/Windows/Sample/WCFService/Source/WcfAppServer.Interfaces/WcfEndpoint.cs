using System;
using System.Runtime.Serialization;
using WcfAppServer.Interfaces;

namespace WcfAppServer
{
    [DataContract]
    [Serializable]
    public class WcfEndpoint : IWcfEndpoint
    {
        #region IWcfEndpoint Members

        public string Address { get; private set; }
        public string Binding { get; private set; }
        public string Contract { get; private set; }

        public new string ToString()
        {
            return string.Format("A: {0}, B: {1} C: {2}",
                                 Address,
                                 Binding,
                                 Contract);
        }

        #endregion

        public static IWcfEndpoint Create(string address, string binding, string contract)
        {
            IWcfEndpoint wcfEndpoint = new WcfEndpoint
                                           {
                                               Address = address,
                                               Binding = binding,
                                               Contract = contract
                                           };
            return wcfEndpoint;
        }
    }
}