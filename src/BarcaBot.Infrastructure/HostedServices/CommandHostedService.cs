using System;
using System.Threading;
using System.Threading.Tasks;
using BarcaBot.Core.Interfaces;
using BarcaBot.Core.Models.Settings;
using BarcaBot.Infrastructure.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BarcaBot.Infrastructure.HostedServices
{
    public class CommandHostedService : IHostedService
    {
        private readonly ILogger<CommandHostedService> _logger;
        private readonly DiscordSettings _settings;
        private readonly IServiceProvider _provider;
        private readonly IBotService _botService;
        private readonly CommandService _commandService;

        public CommandHostedService(
            ILogger<CommandHostedService> logger,
            IServiceProvider provider, IBotService botService,
            CommandService commandService,
            DiscordSettings settings
            )
        {
            _logger = logger;
            _provider = provider;
            _botService = botService;
            _commandService = commandService;
            _settings = settings;
        }
        
        private Task LogCommand(LogMessage arg)
        {
            var message = $"{arg.Source}: {arg.Message}";
            if (arg.Exception is null)
            {
                _logger.Log(arg.Severity.ToLogLevel(), message);
            }
            else
            {
                _logger.Log(arg.Severity.ToLogLevel(), arg.Exception, message);
            }

            return Task.CompletedTask;
        }
        
        private Task BotMessageReceivedAsync(SocketMessage message)
        {
            if (message.Author.IsBot)
            {
                return Task.CompletedTask;
            }
            
            if (!(message is SocketUserMessage userMessage))
            {
                return Task.CompletedTask;
            }

            var argPos = 0;

            if (!userMessage.HasCharPrefix(_settings.Prefix, ref argPos))
            {
                return Task.CompletedTask;
            }

            _botService.ExecuteHandlerAsynchronously(
                handler: (client) =>
                {
                    var context = new SocketCommandContext(client, userMessage);
                    return _commandService.ExecuteAsync(context, argPos, _provider);
                },
                callback: async (result) =>
                {
                    if (result.IsSuccess)
                    {
                        return;
                    }

                    if (result.Error == CommandError.UnknownCommand)
                    {
                        return;
                    }

                    if (result.Error.HasValue)
                    {
                        await message.Channel.SendMessageAsync($":x: Error: {result.Error.Value}, {result.ErrorReason}");
                    }
                });
            
            return Task.CompletedTask;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _commandService.Log += LogCommand;
            _botService.DiscordClient.MessageReceived += BotMessageReceivedAsync;
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _botService.DiscordClient.MessageReceived -= BotMessageReceivedAsync;
            _botService.DiscordClient.Log -= LogCommand;
            return Task.CompletedTask;
        }
    }
}