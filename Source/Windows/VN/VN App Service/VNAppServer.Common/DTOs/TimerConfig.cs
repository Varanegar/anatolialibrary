using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Common.DTOs
{
    public sealed class TimerConfig : ITimerConfig
    {
        #region ITimerConfig Members

        [DataMember]
        string ITimerConfig.CronExpression { get; set; }

        [DataMember]
        string ITimerConfig.GroupId { get; set; }

        [DataMember]
        string ITimerConfig.JobId { get; set; }

        [DataMember]
        string ITimerConfig.TriggerId { get; set; }

        [DataMember]
        ITimerJob ITimerConfig.TimerJob { get; set; }

        #endregion

        #region Factory

        public static ITimerConfig Create(string cronExpression, string groupId, string triggerId, string jobId, ITimerJob timerJob)
        {
            ITimerConfig timerConfig = new TimerConfig();

            timerConfig.CronExpression = cronExpression;
            timerConfig.GroupId = groupId;
            timerConfig.JobId= jobId;
            timerConfig.TriggerId = triggerId;
            timerConfig.TimerJob = timerJob;
            return timerConfig;
        }

        #endregion
    }
}