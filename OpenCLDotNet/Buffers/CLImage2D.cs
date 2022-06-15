using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public class CLImage2D : CLImage
    {
        public CLImage2D(CLContext context, uint width, uint height, float[] data)
            : base(context)
        {
            Width = width;
            Height = height;
            ChannelOrder = CL_CHANNEL_ORDER.R;
            ChannelType = CL_CHANNEL_TYPE.FLOAT;
            Channels = EnumUtil.GetNumChannels(ChannelOrder);

            var flags = CL_MEM_FLAGS.READ_WRITE | CL_MEM_FLAGS.USE_HOST_PTR;
            Create(context, flags, data);
        }

        public CLImage2D(CLContext context, CL_MEM_FLAGS flags, uint width, uint height, float[] data)
            : base(context)
        {
            Width = width;
            Height = height;
            ChannelOrder = CL_CHANNEL_ORDER.R;
            ChannelType = CL_CHANNEL_TYPE.FLOAT;
            Channels = EnumUtil.GetNumChannels(ChannelOrder);

            Create(context, flags, data);
        }

        public uint Width { get; private set; }

        public uint Height { get; private set; }

        public uint Channels { get; private set; }

        public CL_CHANNEL_ORDER ChannelOrder { get; private set; }

        public CL_CHANNEL_TYPE ChannelType { get; private set; }

        public override string ToString()
        {
            var read_write = "";
            read_write += CanRead ? "T/" : "F/";
            read_write += CanWrite ? "T" : "F";

            return String.Format("[CLImage2D: Id={0}, ContextId={1}, Width={2}, Height={3}, Channels={4}, ReadWrite={5}, Error={6}]",
                Id.Value, Context.Id.Value, Width, Height, Channels, read_write, Error);
        }

        private void Create(CLContext context, CL_MEM_FLAGS flags, float[] data)
        {
            Error = "NONE";
            Flags = flags;

            uint width = Width;
            uint height = Height;   
            uint row_pitch = Channels * sizeof(float) * Width;
            uint slice_pitch = row_pitch * Height;

            CLImageFormat format = new CLImageFormat()
            {
                Order = ChannelOrder,
                Type = ChannelType
            };

            CLImageDescription des = new CLImageDescription()
            {
                Type = CL_MEM_OBJECT_TYPE.IMAGE2D,
                Width = width,
                Height = height,
                Depth = 0,
                ArraySize = 0,
                RowPitch = row_pitch,
                SlicePitch = slice_pitch,
                NumMipLevels = 0,
                NumSamples = 0,
                Buffer = Id,
            };

            CL_ERROR error;
            Id = CL.CreateImage(context.Id, flags, format, des, data, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }


        }
    }
}
