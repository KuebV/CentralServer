using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace CentralServerBot.DiscordBots
{
    public class MainBot
    {
        private static DiscordSocketClient _client;
        public static DiscordSocketClient Client => _client ?? (_client = new DiscordSocketClient());
        private CommandService _commands;
        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            _client.MessageReceived += Client_MessageReceived;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            var Token = "";
            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();
            await Task.Delay(3000);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;

            if (_client != null && Message != null)
            {
                int ArgPos = 0;
                var _context = new SocketCommandContext(_client, Message);
                var prefix = "!";
                if (Message.HasStringPrefix(prefix, ref ArgPos))
                {
                    var Result = await _commands.ExecuteAsync(_context, ArgPos, null);
                    if (_context.Message == null || _context.Message.Content == "")
                        return;
                    if (_context.User.IsBot)
                        return;
                }
            }
        }
    }
}
