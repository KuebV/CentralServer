using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Networking;



namespace CentralClient
{
    class Program
    {
        private static EasyClient client;
        private static Packet packet = new Packet();

        static void Main(string[] args)
        {
            StartLaunch();
        }

        private static void StartLaunch()
        {
            string homeDir = Directory.GetCurrentDirectory();
            string cfgDir = Path.Combine(homeDir, "Config");
            if (!Directory.Exists(cfgDir))
            {
                Log.Error("Creating Config Directory...");
                Directory.CreateDirectory(cfgDir);
            }

            string startupConfig = Path.Combine(cfgDir, "startup.json");
           
            if (!File.Exists(startupConfig))
            {
                Log.Debug("Would you like to save this config for easier login?");
                string ans = Console.ReadLine();
                if (ans.ToLower().Contains("y"))
                {

                    Log.Info("Please enter the IP :");
                    string ip = Console.ReadLine();

                    Log.Info("Enter the Port :");
                    string port = Console.ReadLine();

                    Config cfg = new Config
                    {
                        Port = Convert.ToInt32(port),
                        IP = ip
                    };

                    Log.Error("Creating Startup Config File");
                    StreamWriter sw = File.CreateText(startupConfig);
                    sw.Write(JsonConvert.SerializeObject(cfg, Formatting.Indented));
                    sw.Close();

                    ConnectToServer(ip, Convert.ToInt32(port));
                }
                else
                {
                    Log.Info("Please enter the IP :");
                    string ip = Console.ReadLine();

                    Log.Info("Enter the Port :");
                    string port = Console.ReadLine();

                    ConnectToServer(ip, Convert.ToInt32(port));
                }
            }
            else
            {
                try
                {
                    StreamReader sr = File.OpenText(startupConfig);
                    Config configRead = JsonConvert.DeserializeObject<Config>(sr.ReadToEnd());
                    sr.Close();

                    Log.Info("You have previously logged into the Central Server. Do 'login' to login to the Central Server.");
                    string log = Console.ReadLine();
                    if (log.Equals("login"))
                        ConnectToServer(configRead.IP, configRead.Port);
                    else if (log.Equals("logout"))
                    {
                        Log.Info("Logging out of the Central Server");
                        File.Delete(startupConfig);
                    }
                    else
                    {
                        Log.Info("Incorrect Usage");
                    }
                    
                }
                catch (Exception e)
                {
                    Log.Error("Failed to Load Config");
                }
            }
        }

        private static void ConnectToServer(string IP, int Port)
        {
            client = new EasyClient(IP, Port, packet);
            client.ConnectToServer();
            if (client.isConnected())
                Log.Debug("Connected to Server");

            else
                Log.Error("Failed connection to Server");
                
            bool cont = true;
            while (cont)
            {
                Log.Info("Message to Central Server : ");
                string fuck = Console.ReadLine();
                string[] act = fuck.Split('-');

                client.SendPacketToServer(packet.writePacket(act[0], act[1]));
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
