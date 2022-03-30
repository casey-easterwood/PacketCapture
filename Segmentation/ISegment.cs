namespace PacketCapture.Segment
{
    public interface ISegment
    {
        short length { get; }
        short offset { get; }
    }
}