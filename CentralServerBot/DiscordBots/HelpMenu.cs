using System;
using System.Collections.Generic;
using System.Text;
using Discord;

namespace CentralServerBot.DiscordBots
{
    public class HelpMenu
    {
        public EmbedBuilder GetHelpOutput()
        {
            EmbedBuilder embed = new EmbedBuilder();

            embed.Title = "Central Server Help Menu";
            embed.AddField("**__Commands : __**", "**ping** - Pings the bot and looks for a response\n" +
                                                  "**info** - Gives a quick rundown and info about the bot\n" +
                                                  "**help** - You're already here\n" +
                                                  "**reservedslot** - Add a user to a reserved slot\n" +
                                                  "**addmute** - Add a mute to a user\n" +
                                                  "**removemute** - Remove a mute from a user\n" +
                                                  "**rolerequest** - Remove a role request cooldown\n" +
                                                  "**itemrequest** - Remove a Item Request Cooldown\n");

            embed.WithColor(Color.Blue);
            return embed;
        }
    }
}
