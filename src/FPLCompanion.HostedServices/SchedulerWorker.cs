using FPLCompanion.ApplicationServices.Requests.General.Commands;
using FPLCompanion.HostedServices.Jobs;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FPLCompanion.HostedServices
{
    public class SchedulerWorker : IHostedService
    {
        private readonly ILogger<SchedulerWorker> _logger;
        private readonly SchedulerConfigContext _schedulerConfig;

        public SchedulerWorker(
            ILogger<SchedulerWorker> logger,
            SchedulerConfigContext schedulerConfig)
        {
            _logger = logger;
            _schedulerConfig = schedulerConfig;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _schedulerConfig.RegisterJob<ImportFplDataJob>();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _schedulerConfig.ShutdownScheduler();
        }
    }
}
