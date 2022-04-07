using PacketCapture.Types;
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
            var version             = (data[0] & 0xF0) >> 4; //Left 
            var IHL                 = (data[0] & 0x0F);      //Right
            var DSCP                = (data[1] & 0xFC) >> 2; 
            var ECN                 = (data[1] & 0x03);      
            var TotalLength         = BitConverter.ToUInt16(data.Skip(2).Take(2).Reverse().ToArray());
            var Identification      = (BitConverter.ToUInt16(data.Skip(4).Take(2).Reverse().ToArray())) & 0x1FFF;
            var Flags               = (data[6] & 0xE0) >> 5;
            var FragmentionOffset   = BitConverter.ToUInt16(data.Skip(6).Take(2).ToArray());
            var TimeToLive          = (data[9] & 0xFF00) >> 8;
            IPV4Protocol protocol   = (IPV4Protocol) (data[9] & 0x00FF);
            IPAddress Desination    = new(data.Skip(12).Take(4).ToArray());
            IPAddress Source        = new(data.Skip(16).Take(4).ToArray());
            

            Fields["Version"]           = Convert.ToString(version, 2).PadLeft(4, '0');
            Fields["IHL"]               = IHL.ToString(); // Convert.ToString(IHL, 2).PadLeft(4, '0');
            Fields["DSCP"]              = Convert.ToString(DSCP, 2).PadLeft(6, '0');
            Fields["ECN"]               = Convert.ToString(ECN, 2).PadLeft(2, '0');
            Fields["TotalLength"]       = TotalLength.ToString();
            Fields["Identification"]    = Identification.ToString();
            Fields["Flags"]             = Flags.ToString().PadLeft(2, '0');
            Fields["FragmentionOffset"] = FragmentionOffset.ToString();
            Fields["TimeToLive"]        = TimeToLive.ToString();
            Fields["Destination IP"]    = Desination.ToString();
            Fields["Source IP"]         = Source.ToString();
            Fields["Protocol"]          = protocol.ToString();
        }
    }
}
