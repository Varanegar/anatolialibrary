using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace VNAppServer.Common.Interfaces
{
    public interface ITimerConfig
    {
        [DataMember]
        ITimerJob TimerJob { get; set; }

        [DataMember]
        string CronExpression { get; set; }

        [DataMember]
        string GroupId { get; set; }

        [DataMember]
        string JobId { get; set; }

        [DataMember]
        string TriggerId { get; set; }

    }
}