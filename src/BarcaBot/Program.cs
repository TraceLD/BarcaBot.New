using System;
using System.IO;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Settings;
using BarcaBot.Infrastructure.Services;
using BarcaBot.Infrastructure.Startup;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Compact;
using TraceLd.PlotlySharp;

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

                    services.AddSingleton<ICountryEmojiService, CountryEmojiService>();

                    services.AddDataAccessServices();
                    services.AddStatsApis();
                    services.AddCharts();
                    //services.AddAutoUpdaters();
                    services.AddEmbedServices();
                    services.AddDiscordBot();
                })
                .UseSerilog();
    }
}
