using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer.Common.Interfaces
{
    public interface ITimerJob 
    {
        string TimerId { get; set; }

        string TimerAssemblyName { get; set; }

        string TimerClassName { get; set; }
    }
}
