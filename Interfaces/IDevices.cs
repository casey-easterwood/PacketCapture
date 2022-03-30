using SharpPcap;
using System.Collections.Generic;

namespace PacketCapture.Interfaces
{
    public interface IDevices
    {
        IEnumerable<ICaptureDevice> GetConnectedDevices();
        ICaptureDevice GetDevice(string name);
    }
}
