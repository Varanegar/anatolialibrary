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
        public static void CreateScheduler(IScheduler scheduler, string jobId, string groupdId, string triggerId, string cronExpression, Type jobType, string serverURI, string OwnerKey, string DataOwnerKey, string DataOwnerCenterKey)
        {
            IJobDetail job = JobBuilder.Create(jobType)
                .WithIdentity(jobId, groupdId)
                .UsingJobData("ServerURI", serverURI)
                .UsingJobData("OwnerKey", OwnerKey)
                .UsingJobData("DataOwnerKey", DataOwnerKey)
                .UsingJobData("DataOwnerCenterKey", DataOwnerCenterKey)
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
