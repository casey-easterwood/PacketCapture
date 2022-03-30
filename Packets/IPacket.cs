using System.Collections.Generic;

namespace PacketCapture.Packets
{
    public interface IPacket
    {
        Dictionary<string, string> Fields { get; set; }

        void parse(byte[] data);
    }
}