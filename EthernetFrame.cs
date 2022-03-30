using PacketCapture.Types;
using SharpPcap;
using System;
using PacketCapture.Segmentation;

namespace PacketCapture
{
    public class EthernetFrame
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public byte[] Payload { get; set; }
        public PosixTimeval Timeval { get; internal set; }
        public int Length { get; internal set; }

        public EthernetHeader Header;

        public EthernetFrame(int id, SharpPcap.RawCapture packetCapture)
        {
            Id = id;
            Timeval = packetCapture.Timeval;
            Length = packetCapture.PacketLength;
            Data = packetCapture.Data;
            Payload = Segmentation.Reader.Read(Segmentation.Ethernet.Payload, Data);

            Header = new EthernetHeader(packetCapture.Data);
        }

        public override string ToString()
        {
            return $"Type: {Header.Type} Source: {Header.SourceAddress} Destination: {Header.DestinationAddress} ";
        }
    }

    public class EthernetHeader
    {
        byte[] _EthernetTypeData;
        public byte[] EthernetTypeData
        {
            get { return _EthernetTypeData; }
        }
        public MacAddress SourceAddress { get; set; }
        public MacAddress DestinationAddress { get; set; }
        public EthernetFrameType Type
        {
            get
            {
                Array.Reverse(_EthernetTypeData);

                EthernetFrameType type = (EthernetFrameType) BitConverter.ToUInt16(_EthernetTypeData);

                return type;
            }
        }
        
        public EthernetHeader(byte[] data)
        {
            _EthernetTypeData = new byte[2];

            DestinationAddress = new MacAddress(Reader.Read(Ethernet.DestinationAddress, data)); ;
            
            SourceAddress = new MacAddress(Reader.Read(Ethernet.SourceAddress, data));

            _EthernetTypeData  = Reader.Read(Ethernet.EtherType, data);
        }
    }

    public struct MacAddress
    {
        public byte[] value { get; set; }

        public override string ToString()
        {
            return BitConverter.ToString(value);
        }

        public MacAddress(byte[] data)
        {
            value = data;
        }
    }
}
