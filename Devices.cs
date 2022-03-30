using PacketCapture.Interfaces;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PacketCapture 
{
    public class Devices : IDevices
    {
        public IEnumerable<ICaptureDevice> GetConnectedDevices()
        {
            CaptureDeviceList deviceList = CaptureDeviceList.Instance;

            IEnumerable<ICaptureDevice> devices = CaptureDeviceList.Instance.Where(d => d.MacAddress != null);

            return devices;
        }

        public ICaptureDevice GetDevice(string name)
        {
            CaptureDeviceList deviceList = CaptureDeviceList.Instance;

            var device = deviceList.Where(d => d.Name == name).FirstOrDefault();

            return device;
        }
    }
}
