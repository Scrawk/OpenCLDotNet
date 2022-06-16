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

            if(!data.IsValidSize())
            {
                Error = "INVALID_SOURCE_SIZE";
                return;
            }

            if (!data.IsValidChannel())
            {
                Error = "INVALID_CHANNEL_ORDER_TYPE";
                return;
            }

            if(!data.IsValidArrayData())
            {
                Error = "INVALID_DATA_TYPE";
                return;
            }

            /*
            if(!data.ImageFormatIsSupported(context.Id, format, out error))
            {
                if (error != CL_ERROR.SUCCESS)
                    Error = error.ToString();
                else
                    Error = "CHANNEL_FORMAT_NOT_SUPPORTED";

                return;
            }
            */

            Id = CL.CreateImage(context.Id, Flags, format, desc, data.Source, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }


        }
    }
}