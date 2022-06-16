using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public struct CLImageData2D
    {

        public uint Width;

        public uint Height;

        internal uint Channels;

        public CL_CHANNEL_ORDER ChannelOrder;

        public CL_CHANNEL_TYPE ChannelType;

        public CL_MEM_FLAGS Flags;

        public Array Source;

        public override string ToString()
        {
            return String.Format("[CLImageData: Width={0}, Height={1}, Order={2}, Type={3}]",
                Width, Height, ChannelOrder, ChannelType);
        }

        internal CLImageFormat CreateImageFormat()
        {
            var format = new CLImageFormat();
            format.ChannelOrder = ChannelOrder;
            format.ChannelType = ChannelType;

            return format;
        }

        internal CLImageDescription CreateImageDescription()
        {
            Channels = CL.GetNumChannels(ChannelOrder);

            uint size = CL.SizeOf(ChannelType);
            uint width = Width;
            uint height = Height;
            uint row_pitch = Channels * size * Width;
            uint slice_pitch = 0; // row_pitch * Height;

            var des = new CLImageDescription();
            des.MemType = CL_MEM_OBJECT_TYPE.IMAGE2D;
            des.Width = width;
            des.Height = height;
            des.Depth = 0;
            des.ArraySize = 0;
            des.RowPitch = row_pitch;
            des.SlicePitch = slice_pitch;
            des.NumMipLevels = 0;
            des.NumSamples = 0;
            //des.Buffer = 0;

            return des;
        }

    }
}
