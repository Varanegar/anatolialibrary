using System;

namespace WcfServiceLibrary1
{
    public class Service2 : IService2
    {
        #region IService2 Members

        public string GetAppDomainName()
        {
            return AppDomain.CurrentDomain.FriendlyName;
        }

        public void UnstableCode()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}