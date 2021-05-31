using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Networking;
using Newtonsoft.Json;

namespace CentralServer
{
    public class Program
    {

        private static EasyServer server;
        private static Packet packet;
        static void Main(string[] args)
        {
            StartProcess();
        }

        private static void StartProcess()
        {
            string homeDir = Directory.GetCurrentDirectory();
            string cfgDir = Path.Combine(homeDir, "Config");
            if (!Directory.Exists(cfgDir))
            {
                Log.Error("Creating Config Directory...");
                Directory.CreateDirectory(cfgDir);
            }

            string startupConfig = Path.Combine(cfgDir, "startup.json");
            Config cfg = new Config
            {
                Port = 7500,
                IdleMode = true,
                IdleModeTimer = 15
            };
            if (!File.Exists(startupConfig)){
                Log.Error("Creating Startup Config File");
                StreamWriter sw = File.CreateText(startupConfig);
                sw.Write(JsonConvert.SerializeObject(cfg, Formatting.Indented));
                sw.Close();
            }
            else
            {
                try
                {
                    StreamReader sr = File.OpenText(startupConfig);
                    Config configRead = JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
                    sr.Close();

                    Log.Info($"Port : {configRead.Port}");
                    Log.Info($"IdleMode : {configRead.IdleModeTimer} seconds");

                    StartServer(configRead.Port, configRead.IdleModeTimer);
                }
                catch(Exception e)
                {
                    Log.Error($"Failed to Load Config : {e}");
                }
            }

        }
        private static void StartServer(int Port, int IdleMode)
        {
            string hostName = Dns.GetHostName();
            Console.WriteLine(hostName);
            string IP = Dns.GetHostByName(hostName).AddressList[0].ToString();

            packet = new Packet();

            Log.Info("Starting Server...");
            server = new EasyServer(10, Port, packet);
            server.StartServer();
            Log.Debug($"Server on IP : {IP}");

            string message = Console.ReadLine();
            if (message.Equals("stop"))
            {
                server.Stop();
                Environment.Exit(0);
            }
            
        }


    }

    public class Log
    {
        public static void Info(string Text) =>
            ColoredConsole.WriteLine(Text, ConsoleColor.Cyan);

        public static void Error(string Text) =>
            ColoredConsole.WriteLine(Text, ConsoleColor.Red);

        public static void Debug(string Text) =>
            ColoredConsole.WriteLine(Text, ConsoleColor.Green);
    }
}
