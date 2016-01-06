using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNAppServer
{
    public static class SchedulerFactory
    {
        public static void CreateScheduler(IScheduler scheduler, string jobId, string groupdId, string triggerId, string cronExpression, Type jobType, string serverURI, string privateOwnerId)
        {
            IJobDetail job = JobBuilder.Create(jobType)
                .WithIdentity(jobId, groupdId)
                .UsingJobData("ServerURI", serverURI)
                .UsingJobData("PrivateOwnerId", privateOwnerId)
                .Build();

            IScheduleBuilder schedule = CronScheduleBuilder.CronSchedule(cronExpression); 

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerId, groupdId)
                .StartNow()
                .WithSchedule(schedule)
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}
