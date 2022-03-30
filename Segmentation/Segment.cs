using PacketCapture.Segment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PacketCapture.Segmentation
{
    public struct Segment : ISegment
    {
        public short offset { get; }
        public short length { get; }

        public Segment(short offset, short length)
        {
            this.offset = offset;
            this.length = length;
        }
    }
}