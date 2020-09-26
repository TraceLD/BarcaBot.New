using System;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Settings;
using BarcaBot.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using TraceLd.PlotlySharp;

namespace BarcaBot.Infrastructure.Startup
{
    public static class ChartsSetup
    {
        public static void AddCharts(this IServiceCollection services)
        {
            services.AddSingleton<Func<PlotlyCredentials>>(provider =>
            {
                var settings = provider.GetRequiredService<ApisSettings>().Plotly;
                PlotlyCredentials Provider() => new PlotlyCredentials
                {
                    Username = settings.Username,
                    Token = settings.Token
                };
                return Provider;
            });
            services.AddHttpClient<PlotlyClient>();
            services.AddScoped<IChartService, ChartService>();
        }
    }
}