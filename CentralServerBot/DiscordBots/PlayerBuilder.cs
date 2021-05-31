using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using CentralServerBot.NewFolder;


namespace CentralServerBot.DiscordBots
{
    public class PlayerBuilder
    {
        public EmbedBuilder GetPlayer(string ID, bool forceView = false)
        {
            EmbedBuilder embed = new EmbedBuilder();
            PlayerRetreive pr = new PlayerRetreive();

            if (!pr.playerExists(ID) && forceView == false)
            {
                embed.Title = "Player has not logged on to the server or doesn't exist!";
                embed.WithColor(Color.Red);
                return embed;
            }

            if (!pr.getEnabled(ID) && forceView == false)
            {
                embed.Title = "Player has disabled their stats!";
                embed.WithColor(Color.Red);
                return embed;
            }

            if (!pr.getDNT(ID) || forceView == true)
            {

                TimeSpan t = TimeSpan.FromSeconds(pr.getPlaytime(ID));
                string timePlayed = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
                embed.Title = $"{pr.getNickname(ID)}'s Profile";
                embed.AddField("Nickname :", pr.getNickname(ID));
                embed.AddField("Steam64ID :", ID);
                embed.AddField("Kills: ", pr.getKills(ID));
                embed.AddField("Deaths: ", pr.getDeaths(ID));
                embed.AddField("Playtime: ", timePlayed);
                embed.AddField("Group: ", pr.getGroup(ID));
                embed.AddField("Last Online: ", pr.getLastOnline(ID));

                embed.WithColor(Color.Green);
                return embed;
            }
            else
            {
                embed.Title = "User has Do-Not-Track Enabled!";
                embed.WithColor(Color.Red);
                return embed;
            }

            
        }

        public EmbedBuilder PlayerInfo(string ID)
        {
            EmbedBuilder embed = new EmbedBuilder();
            PlayerRetreive pr = new PlayerRetreive();

            if (!pr.playerExists(ID))
            {
                embed.Title = "Player has not logged on to the server or doesn't exist!";
                embed.WithColor(Color.Red);
                return embed;
            }

            TimeSpan t = TimeSpan.FromSeconds(pr.getPlaytime(ID));
            string timePlayed = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            embed.Title = $"{pr.getNickname(ID)}'s Profile";
            embed.AddField("Nickname :", pr.getNickname(ID));
            embed.AddField("Steam64ID :", ID);
            embed.AddField("Playtime: ", timePlayed);
            embed.AddField("Group: ", pr.getGroup(ID));
            embed.AddField("Last Online: ", pr.getLastOnline(ID));

            embed.WithColor(Color.Green);
            return embed;
        }

        public EmbedBuilder FullInfo(string ID)
        {
            EmbedBuilder embed = new EmbedBuilder();
            PlayerRetreive pr = new PlayerRetreive();

            if (!pr.playerExists(ID))
            {
                embed.Title = "Player has not logged on to the server or doesn't exist!";
                embed.WithColor(Color.Red);
                return embed;
            }

            TimeSpan t = TimeSpan.FromSeconds(pr.getPlaytime(ID));
            string timePlayed = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms", t.Hours, t.Minutes, t.Seconds, t.Milliseconds);
            embed.Title = $"{pr.getNickname(ID)}'s Profile";
            embed.AddField("Nickname :", pr.getNickname(ID));
            embed.AddField("Steam64ID :", ID);
            embed.AddField("Playtime: ", timePlayed);
            embed.AddField("Group: ", pr.getGroup(ID));
            embed.AddField("Last Online: ", pr.getLastOnline(ID));
            embed.AddField("IP Address: ", pr.getIP(ID));

            embed.WithColor(Color.Green);
            return embed;
        }
    }
}
