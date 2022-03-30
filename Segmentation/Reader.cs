using PacketCapture.Segment;
using System.Linq;

namespace PacketCapture.Segmentation
{
    public static class Reader
    {
        public static byte[] Read(ISegment segment,  byte[] data)
        {
            short lengthMax = segment.length;

            if(segment.length > data.Length)
                lengthMax = (short) data.Length;

            if (segment.offset == 0)
                return data.Take(lengthMax).ToArray();

            return data.Skip(segment.offset).Take(lengthMax).ToArray();
        }
    }
}