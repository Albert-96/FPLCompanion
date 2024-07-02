using FPLCompanion.ApplicationServices.Requests.General.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FPLCompanion.HostedServices
{
    public class PremierLeagueApiWorker : BackgroundService
    {
        private readonly ILogger<PremierLeagueApiWorker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PremierLeagueApiWorker(ILogger<PremierLeagueApiWorker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new ImportFplData());
            }
        }
    }
}
