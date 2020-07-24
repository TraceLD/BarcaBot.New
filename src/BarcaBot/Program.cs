using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Settings;
using BarcaBot.Infrastructure;
using BarcaBot.Infrastructure.HostedServices;
using BarcaBot.Infrastructure.Services;
using BarcaBot.Infrastructure.Services.Http;
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
using MongoDB.Driver;

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
                    services.Configure<ApisSettings>(context.Configuration.GetSection(nameof(ApisSettings)));
                    services.AddSingleton<ApisSettings>(provider =>
                        provider.GetRequiredService<IOptions<ApisSettings>>().Value);
                    services.Configure<DiscordSettings>(context.Configuration.GetSection(nameof(DiscordSettings)));
                    services.AddSingleton<DiscordSettings>(provider =>
                        provider.GetRequiredService<IOptions<DiscordSettings>>().Value);
                    services.Configure<DatabaseSettings>(context.Configuration.GetSection(nameof(DatabaseSettings)));
                    services.AddSingleton<DatabaseSettings>(provider =>
                        provider.GetRequiredService<IOptions<DatabaseSettings>>().Value);

                    services.AddHttpClient<IApiFootballService, ApiFootballService>();
                    services.AddHttpClient<FootballDataService>();

                    services.AddSingleton<IMongoClient>(provider =>
                    {
                        var config = provider.GetRequiredService<DatabaseSettings>();
                        return new MongoClient(config.ConnectionString);
                    });
                    services.AddScoped<IMongoDatabase>(provider =>
                    {
                        var config = provider.GetRequiredService<DatabaseSettings>();
                        var client = provider.GetRequiredService<IMongoClient>();
                        return client.GetDatabase(config.DatabaseName);
                    });
                    
                    services.AddScoped<IPlayerService, PlayerService>();
                    services.AddSingleton<IBotService, BotHostedService>();
                    services.AddSingleton<ICountryEmojiService, CountryEmojiService>();

                    var commandService = new CommandService(new CommandServiceConfig
                    {
                        CaseSensitiveCommands = false,
                        DefaultRunMode = RunMode.Sync,
                        LogLevel = LogSeverity.Verbose
                    });
                    services.AddSingleton(provider =>
                    {
                        commandService.AddModulesAsync(Assembly.GetEntryAssembly(), provider);
                        return commandService;
                    });
                    
                    services.AddHostedService(provider => provider.GetRequiredService<IBotService>());
                    services.AddHostedService<CommandHostedService>();
                    //services.AddHostedService<PlayerHostedService>();
                })
                .UseSerilog();
    }
}
