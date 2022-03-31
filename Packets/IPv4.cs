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
            byte[] VersionIHLData           = data.Take(1).ToArray();
            byte[] DSCPECNData              = data.Skip(1).Take(1).ToArray();
            byte[] TotalLengthData          = data.Skip(2).Take(2).Reverse().ToArray();
            byte[] IdentificationData       = data.Skip(4).Take(2).Reverse().ToArray();
            byte[] ProtocolData             = data.Skip(9).Take(1).ToArray();
            byte[] DestinationAddressData   = data.Skip(12).Take(4).ToArray();
            byte[] SourceAddressData        = data.Skip(16).Take(4).ToArray();

            IPAddress Desination = new(DestinationAddressData);
            IPAddress Source = new(SourceAddressData);

            var version             = (VersionIHLData[0] & 0xF0) >> 4; //Left 
            var IHL                 = (VersionIHLData[0] & 0x0F);      //Right
            var DSCP                = (DSCPECNData[0] & 0xFC) >> 4; 
            var ECN                 = (DSCPECNData[0] & 0x03);      
            var TotalLength         = BitConverter.ToUInt16(TotalLengthData);
            var Identification      = BitConverter.ToUInt16(IdentificationData);
            IPV4Protocol protocol   = (IPV4Protocol) (ProtocolData[0] & 0x00FF);

            Fields["Version"]           = Convert.ToString(version, 2).PadLeft(4, '0');
            Fields["IHL"]               = Convert.ToString(IHL, 2).PadLeft(4, '0');
            Fields["DSCP"]              = Convert.ToString(DSCP, 2).PadLeft(6, '0');
            Fields["ECN"]               = Convert.ToString(ECN, 2).PadLeft(2, '0');
            Fields["TotalLength"]       = TotalLength.ToString();
            Fields["Identification"]    = Identification.ToString();
            Fields["Destination IP"]    = Desination.ToString();
            Fields["Source IP"]         = Source.ToString();
            Fields["Protocol"]          = protocol.ToString();
        }
    }
}
