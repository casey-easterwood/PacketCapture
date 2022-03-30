using PacketCapture.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketCapture
{
    public class Parser 
    {

        public static IPacket Parse(EthernetFrame frame)
        {
            IPacket packet = null;

            if(frame.Header.Type.Equals(Types.EthernetFrameType.IPv4))
            {
                packet = new IPv4();
                packet.parse(frame.Payload);
            }


            return packet;
        }
    }
}
