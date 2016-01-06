using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using VNAppServer.Common.Interfaces;

namespace VNAppServer.Common.DTOs
{
    public sealed class TimerLibrary : ITimerLibrary
    {
        #region ITimerLibrary Members

        [DataMember]
        string ITimerLibrary.FilePath { get; set; }

        [DataMember]
        List<ITimerConfig> ITimerLibrary.TimerConfigs { get; set; }

        #endregion

        #region Factory

        public static ITimerLibrary Create(string filePath,
                                                params ITimerConfig[] timerConfigs)
        {
            ITimerLibrary timerLibrary = new TimerLibrary();
            
            timerLibrary.FilePath = filePath;
            
            timerLibrary.TimerConfigs = new List<ITimerConfig>();

            if (timerConfigs != null)
                ((List<ITimerConfig>) timerLibrary.TimerConfigs).AddRange(timerConfigs);

            return timerLibrary;
        }

        #endregion
    }
}