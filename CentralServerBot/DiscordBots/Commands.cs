using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace CentralServerBot.DiscordBots
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("Help"), Alias("help")]
        public async Task GetHelp()
        {
            HelpMenu output = new HelpMenu();
            EmbedBuilder builder = output.GetHelpOutput();
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("Info"), Alias("info")]
        public async Task GetInfo()
        {
            EmbedBuilder builder = new EmbedBuilder();
            builder.Title = "Info";
            builder.AddField("About", "Central Server Bot is a bot made by the Peanut's Laboratory Development Team made for the TCP Servers");
            builder.AddField("Additional Info", "Written in C#");
            builder.WithFooter("KuebV#0111");
            builder.WithColor(Color.Blue);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("Ping"), Alias("ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("Pong.");
        }

        [Command("Reservedslot"), Alias("reservedslot")]
        public async Task ReservedSlot([Remainder]string SteamID)
        {
            if (Context.Guild.Id != 794682876179382302)
            {
                await Context.Channel.SendMessageAsync("You do not have permission to send this command!");
                return;
            }
            await Context.Channel.SendMessageAsync($"Adding {SteamID} to the Reserved Slot File");
            Program.SendPacketToServer($"RESERVE-{SteamID}");
        }

        [Command("Addmute"), Alias("addmute")]
        public async Task AddMute([Remainder] string SteamID)
        {
            if (Context.Guild.Id != 794682876179382302)
            {
                await Context.Channel.SendMessageAsync("You do not have permission to send this command!");
                return;
            }
            await Context.Channel.SendMessageAsync($"Added {SteamID} to the Mute File");
            Program.SendPacketToServer($"MUTE-{SteamID}");
        }

        [Command("Removemute"), Alias("removemute")]
        public async Task RemoveMute([Remainder] string SteamID)
        {
            if (Context.Guild.Id != 794682876179382302)
            {
                await Context.Channel.SendMessageAsync("You do not have permission to send this command!");
                return;
            }
            await Context.Channel.SendMessageAsync($"Removed {SteamID} from the Mute File");
            Program.SendPacketToServer($"UNMUTE-{SteamID}");
        }

        [Command("Itemrequest"), Alias("itemrequest")]
        public async Task ItemRequest([Remainder] string SteamID)
        {
            if (Context.Guild.Id != 794682876179382302)
            {
                await Context.Channel.SendMessageAsync("You do not have permission to send this command!");
                return;
            }
            await Context.Channel.SendMessageAsync($"Reset {SteamID};s Item Request Cooldown");
            Program.SendPacketToServer($"IRCOOLDOWN-{SteamID}");
        }

        [Command("Rolerequest"), Alias("rolerequest")]
        public async Task RoleRequest([Remainder] string SteamID)
        {
            if (Context.Guild.Id != 794682876179382302)
            {
                await Context.Channel.SendMessageAsync("You do not have permission to send this command!");
                return;
            }
            await Context.Channel.SendMessageAsync($"Reset {SteamID};s Role Request Cooldown");
            Program.SendPacketToServer($"RRCOOLDOWN-{SteamID}");
        }

        [Command("player")]
        public async Task Player([Remainder] string SteamID)
        {
            PlayerBuilder player = new PlayerBuilder();
            EmbedBuilder builder = player.GetPlayer(SteamID);
            await Context.Channel.SendMessageAsync("", false, builder.Build());
        }

        [Command("serversecurity")]
        public async Task ServerSecurity([Remainder] string Command = "NA")
        {
            if (Context.Guild.Id != 794682876179382302)
            {
                await Context.Channel.SendMessageAsync("Incorrect Guild!");
                return;
            }
            string[] args = Command.Split(" ");
            PlayerBuilder player = new PlayerBuilder();
            if (args.Length < 1 || Command.Equals("NA"))
            {
                EmbedBuilder builder = new EmbedBuilder();
                builder.Color = Discord.Color.Red;
                builder.Title = "Central Server Help Command";
                builder.Description = "```Syntax : !serversecurity <subcommand>```";
                builder.AddField("__Subcommands:__", "**playerinfo** - Retreives their full player info\n" +
                    "**fullinfo** - Restricted to certain roles and people");
                await Context.Channel.SendMessageAsync("", false, builder.Build());
                return;
            }

            switch (args.GetValue(0).ToString().ToUpper())
            {
                case "PLAYERINFO":
                    EmbedBuilder builder = player.PlayerInfo(args.GetValue(1).ToString());
                    await Context.Channel.SendMessageAsync("", false, builder.Build());
                    return;
                case "FULLINFO":
                    if (Context.User is SocketGuildUser user)
                    {
                        if (user.Roles.Any(r => r.Name == "Level 4 Access"))
                        {
                            EmbedBuilder build = player.FullInfo(args.GetValue(1).ToString());
                            await Context.User.SendMessageAsync("", false, build.Build());
                            await Context.Channel.SendMessageAsync("I sent you the Full Information on that user!");
                            return;
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("You do not have permission to use this command!");
                            return;
                        }
                    }
                    break;
                default:
                    await Context.Channel.SendMessageAsync("Unknown Subcommand!");
                    await Context.Channel.SendMessageAsync(args.GetValue(0).ToString().ToUpper());
                    return;
            }
        }
    }
}
