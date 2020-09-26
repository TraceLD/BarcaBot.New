using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Scorer = BarcaBot.Core.Models.Scorer;
using ScorerDto = BarcaBot.Core.Models.FootballData.Scorers.Scorer;

namespace BarcaBot.Infrastructure.HostedServices
{
    public class LaLigaScorersHostedService : IHostedService
    {
        private Timer _timer;
        private readonly ILogger<LaLigaScorersHostedService> _logger;

        public IServiceProvider Services { get; }
        
        public LaLigaScorersHostedService(ILogger<LaLigaScorersHostedService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(LaLigaScorersHostedService)}] Starting...");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                _logger.LogInformation($"[{nameof(LaLigaScorersHostedService)}] Updating...");
                
                using var scope = Services.CreateScope();
                var scopedClient = scope.ServiceProvider.GetRequiredService<IFootballDataService>();
                var scorers = (await scopedClient.GetLaLigaScorers()).Scorers;
                var dbScorers = scorers.Select((scorer, i) => new Scorer
                {
                    Id = i,
                    UpdatedAt = DateTime.UtcNow,
                    Name = scorer.Player.Name,
                    Nationality = scorer.Player.Nationality,
                    Team = scorer.Team,
                    Goals = scorer.NumberOfGoals
                });
                
                var scopedLaLigaScorersService = scope.ServiceProvider.GetRequiredService<ILaLigaScorersService>();
                
                foreach (var dbScorer in dbScorers)
                {
                    await scopedLaLigaScorersService.UpsertAsync(dbScorer);
                }
                
                _logger.LogInformation($"[{nameof(LaLigaScorersHostedService)}] Update completed successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[{nameof(LaLigaScorersHostedService)}] Error while updating LaLiga table.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(LaLigaScorersHostedService)}] Stopping...");

            _timer.Change(Timeout.Infinite, 0);
            
            return Task.CompletedTask;
        }
    }
}