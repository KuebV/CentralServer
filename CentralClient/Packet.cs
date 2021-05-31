using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Networking;

namespace CentralClient
{
    public class Packet : EasyPacket
    {
        public override void handleDataFromServer(byte[] data)
        {
            base.handleDataFromServer(data);
        }

        public byte[] writePacket(string Action, string SteamID)
        {
            List<byte> data = new List<byte>();
            writeString(Action, ref data);
            writeString(SteamID, ref data);

            return data.ToArray();
        }
    }
}
