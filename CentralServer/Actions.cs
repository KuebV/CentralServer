using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer
{
    public class Actions
    {
        public static void ReservedSlots(string SteamID)
        {
            AddReservedSlot("7777", SteamID);
            AddReservedSlot("7778", SteamID);
            AddReservedSlot("7779", SteamID);
        }

        private static void AddReservedSlot(string PortDir, string SteamID)
        {
            string Servers = Path.Combine(SCPDir(), "servers");
            string port = Path.Combine(Servers, PortDir);
            string reservedSlotsFile = Path.Combine(port, "UserIDReservedSlots.txt");

            StreamWriter sw = new StreamWriter(reservedSlotsFile, append: true);
            sw.WriteLine($"\n{SteamID}");
            sw.Close();
        }

        private static string SCPDir()
        {
            string homeDir = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string getSCPServer = Path.Combine(homeDir, "SCPServer");
            return getSCPServer;
        }

        public static void AddMute(string SteamID)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string SCPFolder = Path.Combine(appData, "SCP Secret Laboratory");
            string logs = Path.Combine(SCPFolder, "SCPKillLogs");
            string muteLog = Path.Combine(logs, $"muted-players.txt");

            if (File.ReadAllText(muteLog).Contains(SteamID))
            {
                Log.Error("User already exists within Mute File");
                return;
            }
            else
            {
                StreamWriter sw = new StreamWriter(muteLog, append : true);
                sw.WriteLine($"\n{SteamID}");
                sw.Close();
            }
        }

        public static void RemoveMute(string SteamID)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string SCPFolder = Path.Combine(appData, "SCP Secret Laboratory");
            string logs = Path.Combine(SCPFolder, "SCPKillLogs");
            string muteLog = Path.Combine(logs, $"muted-players.txt");
            if (File.ReadAllText(muteLog).Contains(SteamID))
            {
                var tempFile = Path.GetTempFileName();
                var linesToKeep = File.ReadLines(muteLog).Where(l => l != SteamID);

                File.WriteAllLines(tempFile, linesToKeep);

                File.Delete(muteLog);
                File.Move(tempFile, muteLog);
                return;
            }
        }

        public static void ResetCooldownIR(string SteamID)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string IRDir = Path.Combine(appData, "ItemRequest");
            string playerFile = Path.Combine(IRDir, $"{SteamID}.txt");
            if (File.Exists(playerFile))
                File.Delete(playerFile);
        }

        public static void ResetCooldownRR(string SteamID)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string IRDir = Path.Combine(appData, "RoleRequest");
            string playerFile = Path.Combine(IRDir, $"{SteamID}.txt");
            if (File.Exists(playerFile))
                File.Delete(playerFile);
        }
    }
}
