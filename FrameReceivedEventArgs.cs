using System;

namespace PacketCapture
{
    public class FrameReceivedEventArgs : EventArgs
    {
        public EthernetFrame Frame { get; set; }
    }
}
