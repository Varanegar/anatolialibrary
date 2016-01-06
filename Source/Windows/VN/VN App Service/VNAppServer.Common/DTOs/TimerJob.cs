using System;
using System.Runtime.Serialization;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Common.DTOs
{
    public sealed class TimerJob : ITimerJob
    {
        private string _timerId;

        #region ITimerJob Members

        [DataMember]
        string ITimerJob.TimerId
        {
            get { return _timerId; }
            set { _timerId = value.ToLower(); }
        }

        [DataMember]
        string ITimerJob.TimerAssemblyName { get; set; }

        [DataMember]
        string ITimerJob.TimerClassName { get; set; }

        #endregion

        #region Factory

        public static ITimerJob Create(string serviceId,
                                         string serviceAssemblyName,
                                         string serviceClassName)
        {
            ITimerJob timer = new TimerJob();
            timer.TimerId = serviceId;
            timer.TimerAssemblyName = serviceAssemblyName;
            timer.TimerClassName = serviceClassName;

            return timer;
        }

        #endregion
    }
}