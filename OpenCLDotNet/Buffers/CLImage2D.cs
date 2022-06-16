using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public class CLImage2D : CLImage
    {
        public CLImage2D(CLContext context, CLImageData2D data)
            : base(context)
        {
            Create(context, data);
        }

        public uint Width { get; private set; }

        public uint Height { get; private set; }

        public override string ToString()
        {
            var read_write = "";
            read_write += CanRead ? "T/" : "F/";
            read_write += CanWrite ? "T" : "F";

            return String.Format("[CLImage2D: Id={0}, ContextId={1}, Width={2}, Height={3}, Channels={4}, ReadWrite={5}, Error={6}]",
                Id.Value, Context.Id.Value, Width, Height, Channels, read_write, Error);
        }

        private void Create(CLContext context, CLImageData2D data)
        {
            var format = data.CreateImageFormat();
            var desc = data.CreateImageDescription();

            CL_ERROR error;
            ResetErrorCode();
            Width = data.Width;
            Height = data.Height;
            Channels = data.Channels;
            ChannelOrder = data.ChannelOrder;
            ChannelType = data.ChannelType;
            Flags = data.Flags;
            MemType = desc.MemType;

            if(Width * Height * Channels != data.Source.Length)
            {
                Error = "INVALID_SOURCE_SIZE";
                return;
            }

            if (!CL.IsValidChannelType(data.ChannelOrder, data.ChannelType))
            {
                Error = "INVALID_CHANNEL_ORDER_TYPE";
                return;
            }

            if(!CL.IsValidArrayData(ChannelType, data.Source))
            {
                Error = "INVALID_DATA_TYPE";
                return;
            }

            var key = new CLImageFormatKey(Context.Id, Flags, desc.MemType);
            if(!CL.ImageFormatIsSupported(key, format, out error))
            {
                if (error != CL_ERROR.SUCCESS)
                    Error = error.ToString();
                else
                    Error = "CHANNEL_FORMAT_NOT_SUPPORTED";

                return;
            }

            Id = CL.CreateImage(context.Id, Flags, format, desc, data.Source, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }


        }
    }
}