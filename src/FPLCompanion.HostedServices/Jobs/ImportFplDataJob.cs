using FPLCompanion.ApplicationServices.Requests.General.Commands;
using FPLCompanion.DataService.Abstractions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FPLCompanion.HostedServices.Jobs
{
    [DisallowConcurrentExecution]
    public class ImportFplDataJob : IJob
    {
        private readonly ILogger<ImportFplDataJob> _logger;
        private readonly ImportFplData _job;

        public ImportFplDataJob(
            ILogger<ImportFplDataJob> logger,
            ImportFplData job)
        {
            _logger = logger;
            _job = job;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                _logger.LogInformation("Scheduled job started.");
                _job.Handle();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Failed job - " + ex.Message);
            }
        }
    }
}
