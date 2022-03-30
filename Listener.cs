using PacketCapture.Interfaces;
using PacketDotNet;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketCapture
{
    public class Listener : IDisposable, IListener
    {
        private LinkedList<EthernetFrame> frames;

        private readonly ICaptureDevice device;

        public event EventHandler<FrameReceivedEventArgs> FrameReceived;

        private int frameCounter = 0;

        public Listener(ICaptureDevice device)
        {
            this.frames = new();
            this.device = device;
            this.device.OnPacketArrival += OnPacketArrived;
        }

        public void Open()
        {
            if (this.device != null)
                this.device.Open();
        }

        public void StartCapture()
        {
            if (this.device != null)
                this.device.StartCapture();
        }

        public void StopCapture()
        {
            if (this.device != null)
                this.device.StopCapture();
        }

        private int GetNextId()
        {
            return frameCounter++;
        }

        private void OnPacketArrived(object sender, SharpPcap.PacketCapture e)
        {
            var packet = e.GetPacket();

            EthernetFrame frame = new(GetNextId(), packet);
            
            FrameReceivedEventArgs args = new();
            
            args.Frame = frame;
            
            frames.AddLast(frame);
            
            FrameReceived.Invoke(this, args);

        }

        public EthernetFrame GetFrame(int id)
        {
            var frame = frames.Where(p => p.Id == id).FirstOrDefault();

            return frame;
        }

        public void Dispose()
        {
            if (device.Started)
                device.StopCapture();
        }
    }
}
