using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Player;
using BarcaBot.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Infrastructure.HostedServices
{
    public class PlayerHostedService : IHostedService
    {
        private Timer _timer;
        private readonly ILogger<PlayerHostedService> _logger;

        public IServiceProvider Services { get; }
        
        public PlayerHostedService(ILogger<PlayerHostedService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Player Hosted Service is starting...");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(2));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                _logger.LogInformation("Player Hosted Service: Updating...");
                
                using var scope = Services.CreateScope();
                var scopedClient = scope.ServiceProvider.GetRequiredService<IApiFootballService>();
                var players = (await scopedClient.GetPlayerDtosAsync())
                    .AsPlayers();
                var scopedPlayerService = scope.ServiceProvider.GetRequiredService<IPlayerService>();
                
                foreach (var player in players)
                {
                    await scopedPlayerService.UpsertAsync(player);
                }
                
                _logger.LogInformation("Player Hosted Service: Update completed successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Player Hosted Service: Error while updating players");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Player Hosted Service is stopping...");

            _timer.Change(Timeout.Infinite, 0);
            
            return Task.CompletedTask;
        }
    }
}