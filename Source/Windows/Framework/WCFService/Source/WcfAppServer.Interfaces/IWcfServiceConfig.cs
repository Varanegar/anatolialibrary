using System.ServiceModel.Channels;

namespace WcfAppServer.Interfaces
{
    public interface IWcfServiceConfig
    {
        IWcfService WcfService { get; set; }

        string Address { get; set; }

        Binding Binding { get; set; }
    }
}