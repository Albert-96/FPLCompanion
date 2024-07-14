using FPLCompanion.HostedServices.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FPLCompanion.HostedServices
{
    public class SchedulerConfigContext
    {
        private readonly ILogger<SchedulerConfigContext> _logger;
        private ISchedulerFactory _schedulerFactory { get; set; }

        public SchedulerConfigContext(
            ILogger<SchedulerConfigContext> logger,
            ISchedulerFactory schedulerFactory)
        {
            _logger = logger;
            _schedulerFactory = schedulerFactory;
        }

        public async Task ShutdownScheduler()
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.Shutdown();
        }

        public async Task RegisterJob<T>() where T : IJob
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            IJobDetail job = JobBuilder.Create<T>()
                .WithIdentity("importFPLJob", "importFPLGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("importFPLTrigger", "importFPLGroup")
                .StartAt(DateTime.Now.AddMinutes(1))
                .WithSimpleSchedule(x => x
                    .WithIntervalInHours(12)
                    .RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
