using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;

namespace CentralServer
{
    public class Packet : EasyPacket
    {
        private static EasyServer server;
        public override void handleDataFromClient(byte[] data, int userID)
        {
            base.handleDataFromClient(data, userID);
            string action = readString(data);
            string steamID = readString(data);

            Log.Info($"Action Received: {action}");
            Log.Info($"SteamID: {steamID}");
            switch (action)
            {
                case "RESERVE":
                    Log.Info($"Adding {steamID} to Reserved Slots");
                    Actions.ReservedSlots(steamID);
                    break;
                case "MUTE":
                    Log.Info($"Adding {steamID} to Mute List");
                    Actions.AddMute(steamID);
                    break;
                case "UNMUTE":
                    Log.Info($"Removing {steamID} to Unmute List");
                    Actions.RemoveMute(steamID);
                    break;
                case "IRCOOLDOWN":
                    Log.Info($"Removing Item Request Cooldown for {steamID}");
                    Actions.ResetCooldownIR(steamID);
                    break;
                case "RRCOOLDOWN":
                    Log.Info($"Removing Role Request Cooldown for {steamID}");
                    Actions.ResetCooldownRR(steamID);
                    break;
                case "KILL":
                    Environment.Exit(0);
                    break;
                default:
                    Log.Error("Unknown Action");
                    break;
            }



        }


    }
}
