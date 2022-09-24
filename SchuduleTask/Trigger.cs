using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace SchuduleTask
{
    public class Trigger
    {
        private async Task<IScheduler> SchedulerStart()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            IScheduler sched = await schedFact.GetScheduler();
            if (!sched.IsStarted)
                sched.Start();

            return sched;
        }
        public async void JobTrigger()
        {
            IScheduler sched = await SchedulerStart();

            IJobDetail Jobs = JobBuilder.Create<Job>().WithIdentity("Job", null).Build();

            ISimpleTrigger TriggerGorev = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("Job").StartAt(DateTime.Now).WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever()).Build();
            sched.ScheduleJob(Jobs, TriggerGorev);
        }
    }
}
