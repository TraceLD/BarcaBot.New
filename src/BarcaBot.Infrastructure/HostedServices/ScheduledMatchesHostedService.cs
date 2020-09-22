using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Interfaces.Http;
using BarcaBot.Core.Models;
using BarcaBot.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BarcaBot.Infrastructure.HostedServices
{
    public class ScheduledMatchesHostedService : IHostedService
    {
        private Timer _timer;
        private readonly ILogger<ScheduledMatchesHostedService> _logger;

        public IServiceProvider Services { get; }
        
        public ScheduledMatchesHostedService(ILogger<ScheduledMatchesHostedService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(ScheduledMatchesHostedService)}] Starting...");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            try
            {
                _logger.LogInformation($"[{nameof(ScheduledMatchesHostedService)}] Updating...");
                
                using var scope = Services.CreateScope();
                var scopedClient = scope.ServiceProvider.GetRequiredService<IFootballDataService>();
                var matches = (await scopedClient.GetScheduledMatches()).Matches.Take(5).ToList();
                var scheduledMatches = matches.Select((match, i) => new ScheduledMatch
                {
                    Id = i,
                    UpdatedAt = match.UpdatedAt,
                    Competition = match.Competition,
                    Season = match.Season,
                    UtcDate = match.UtcDate,
                    HomeTeam = match.HomeTeam,
                    AwayTeam = match.AwayTeam
                });

                var scopedScheduledMatchService = scope.ServiceProvider.GetRequiredService<IScheduledMatchService>();
                
                foreach (var scheduledMatch in scheduledMatches)
                {
                    await scopedScheduledMatchService.UpsertAsync(scheduledMatch);
                }
                
                _logger.LogInformation($"[{nameof(ScheduledMatchesHostedService)}] Update completed successfully.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"[{nameof(ScheduledMatchesHostedService)}] Error while updating LaLiga table.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"[{nameof(ScheduledMatchesHostedService)}] Stopping...");

            _timer.Change(Timeout.Infinite, 0);
            
            return Task.CompletedTask;
        }
    }
}