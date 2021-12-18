using FPLCompanion.ApplicationServices.Requests.Player.Commands;
using FPLCompanion.Data.ViewModels;
using FPLCompanion.DataService.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var mediator =
                        scope.ServiceProvider
                            .GetRequiredService<IMediator>();
                mediator.Send(new ImportPlayerDataCommand());
            }
        }
    }
}
