using System;
using System.Threading;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Http;
using BarcaBot.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Infrastructure.HostedServices.Updaters
{
    public class PlayersHostedService : IHostedService
    {
        private Timer _timer;
        private readonly ILogger<PlayersHostedService> _logger;

        public IServiceProvider Services { get; }
        
        public PlayersHostedService(ILogger<PlayersHostedService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(PlayersHostedService)}] Starting...");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(2));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                _logger.LogInformation($"[{nameof(PlayersHostedService)}] Updating...");
                
                using var scope = Services.CreateScope();
                var scopedClient = scope.ServiceProvider.GetRequiredService<IApiFootballService>();
                var players = (await scopedClient.GetPlayerDtosAsync())
                    .AsPlayers();
                var scopedPlayerService = scope.ServiceProvider.GetRequiredService<IPlayerService>();
                
                foreach (var player in players)
                {
                    await scopedPlayerService.UpsertAsync(player);
                }
                
                _logger.LogInformation($"[{nameof(PlayersHostedService)}] Update completed successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[{nameof(PlayersHostedService)}] Error while updating.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(PlayersHostedService)}] Stopping...");

            _timer.Change(Timeout.Infinite, 0);
            
            return Task.CompletedTask;
        }
    }
}