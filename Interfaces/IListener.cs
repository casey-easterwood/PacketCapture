using System;

namespace PacketCapture.Interfaces
{
    public interface IListener
    {
        event EventHandler<FrameReceivedEventArgs> FrameReceived;
        void Open();
        void StartCapture();
        void StopCapture();
        EthernetFrame GetFrame(int id);
    }
}