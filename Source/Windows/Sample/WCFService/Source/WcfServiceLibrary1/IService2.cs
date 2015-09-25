using System.ServiceModel;

namespace WcfServiceLibrary1
{
    [ServiceContract]
    public interface IService2
    {
        [OperationContract]
        void UnstableCode();

        [OperationContract]
        string GetAppDomainName();
    }
}