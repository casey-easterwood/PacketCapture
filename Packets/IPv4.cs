using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PacketCapture.Packets
{
    public class IPv4 : IPacket
    {
        public Dictionary<String, String> Fields { get; set; }

        public IPv4()
        {
            Fields = new();
        }

        public void parse(byte[] data)
        {
            byte[] DestinationAddressData = data.Skip(12).Take(4).ToArray();
            byte[] SourceAddressData = data.Skip(16).Take(4).ToArray();

            IPAddress Desination = new(DestinationAddressData);
            IPAddress Source = new(SourceAddressData);

            Fields["Destination IP"] = Desination.ToString();
            Fields["Source IP"] = Source.ToString();
        }
    }
}
