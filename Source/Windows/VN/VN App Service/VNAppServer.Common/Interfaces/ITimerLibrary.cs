using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VNAppServer.Common.Interfaces
{
    public interface ITimerLibrary
    {
        [DataMember]
        string FilePath { get; set; }

        [DataMember]
        List<ITimerConfig> TimerConfigs { get; set; }
    }
}