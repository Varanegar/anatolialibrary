using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WcfAppServer.Interfaces
{
    public interface IConfigService : IExtensibleDataObject
    {
        IList<IWcfServiceLibrary> GetWcfServiceLibraries();
    }
}