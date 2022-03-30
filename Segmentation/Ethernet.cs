namespace PacketCapture.Segmentation
{
    public static class Ethernet
    {
        public static Segment DestinationAddress = new Segment(0, 6);
        public static Segment SourceAddress = new Segment(6, 6);
        public static Segment EtherType = new Segment(12, 2);
        public static Segment Payload = new Segment(14, short.MaxValue);
    }
}