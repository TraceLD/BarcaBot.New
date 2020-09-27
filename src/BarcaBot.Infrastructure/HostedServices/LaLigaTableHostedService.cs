using System;
using System.Threading;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Http;
using BarcaBot.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Infrastructure.HostedServices
{
    public class LaLigaTableHostedService : IHostedService
    {
        private Timer _timer;
        private readonly ILogger<LaLigaTableHostedService> _logger;

        public IServiceProvider Services { get; }
        
        public LaLigaTableHostedService(ILogger<LaLigaTableHostedService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(LaLigaTableHostedService)}] Starting...");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                _logger.LogInformation($"[{nameof(LaLigaTableHostedService)}] Updating...");
                
                using var scope = Services.CreateScope();
                var scopedClient = scope.ServiceProvider.GetRequiredService<IFootballDataService>();
                var tableEntries = (await scopedClient.GetLaLigaStandings())
                    .AsTablePositions();
                var scopedLaLigaTableService = scope.ServiceProvider.GetRequiredService<ILaLigaTableService>();
                
                foreach (var tableEntry in tableEntries)
                {
                    await scopedLaLigaTableService.UpsertAsync(tableEntry);
                }
                
                _logger.LogInformation($"[{nameof(LaLigaTableHostedService)}] Update completed successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[{nameof(LaLigaTableHostedService)}] Error while updating.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(LaLigaTableHostedService)}] Stopping...");

            _timer.Change(Timeout.Infinite, 0);
            
            return Task.CompletedTask;
        }
    }
}