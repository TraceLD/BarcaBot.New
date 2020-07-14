using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;

using Discord;
using Discord.Commands;

using BarcaBot.DataModels.Core;
using BarcaBot.Services.Hosted;

namespace BarcaBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = CreateLogger();
            
            try
            {
                Log.Information("Starting the host");

                using var builtHost = CreateHostBuilder(args).Build();
                await builtHost.StartAsync();
                await builtHost.WaitForShutdownAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static Logger CreateLogger()
            => new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    new RenderedCompactJsonFormatter(),
                    Path.Combine("logs", "log.clef"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 3
                    )
                .CreateLogger();
        
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder => builder.AddCommandLine(args))
                .ConfigureAppConfiguration(builder => builder.AddYamlFile("appsettings.yml", optional: false))
                .ConfigureServices((context, services) =>
                {
                    services.Configure<Settings>(context.Configuration);

                    services.AddHttpClient("apifootball", (provider, client) =>
                    {
                        var config = provider.GetRequiredService<IOptions<Settings>>().Value;
                        client.BaseAddress = new Uri("https://api-football-v1.p.rapidapi.com/v2/");
                        client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");
                        client.DefaultRequestHeaders.Add("X-RapidAPI-Key", config.Apis.ApiFootball.Token);
                    });
                    services.AddHttpClient("footballdata", (provider, client) =>
                    {
                        var config = provider.GetRequiredService<IOptions<Settings>>().Value;
                        client.BaseAddress = new Uri("https://api.football-data.org/v2/");
                        client.DefaultRequestHeaders.Add("X-Auth-Token", config.Apis.FootballData.Token);
                    });
                    
                    services.AddSingleton<IBotService, BotHostedService>();
                    services.AddHostedService(provider => provider.GetRequiredService<IBotService>());
                    
                    services.AddSingleton(provider => new CommandService(new CommandServiceConfig
                    {
                        CaseSensitiveCommands = false,
                        DefaultRunMode = RunMode.Sync,
                        LogLevel = LogSeverity.Verbose
                    }));
                    services.AddHostedService<CommandHostedService>();
                })
                .UseSerilog();
    }
}
