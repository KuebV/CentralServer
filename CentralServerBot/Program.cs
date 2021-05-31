using Networking.Core;
using System;
using System.Threading;

namespace CentralServerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            new DiscordBots.MainBot().RunBotAsync().GetAwaiter().GetResult();
            Console.Read();
        }

        private static EasyClient client;
        private static Packet packet = new Packet();

        public static void SendPacketToServer(string Message)
        {
            string IP = "203.10.96.26";
            int Port = 7500;

            client = new EasyClient(IP, Port, packet);
            client.ConnectToServer();
            if (client.isConnected())
                Log.Debug("Connected to Server");

            else
                Log.Error("Failed connection to Server");

            string[] act = Message.Split('-');

            client.SendPacketToServer(packet.writePacket(act[0], act[1]));

            Thread.Sleep(3000);

            client.Disconnect();
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
