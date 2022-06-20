using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// 
    /// </summary>
    public class CLImage2D : CLImage
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="data"></param>
        public CLImage2D(CLContext context, CLImageData2D data)
            : base(context, data.Source)
        {
            Create(context, data);
        }

        /// <summary>
        /// 
        /// </summary>
        public uint Width { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public uint Height { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var read_write = "";
            read_write += CanRead ? "T/" : "F/";
            read_write += CanWrite ? "T" : "F";

            return String.Format("[CLImage2D: Id={0}, ContextId={1}, Width={2}, Height={3}, Channels={4}, ReadWrite={5}, Error={6}]",
                Id, Context.Id, Width, Height, Channels, read_write, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="data"></param>
        private void Create(CLContext context, CLImageData2D data)
        {
            if(data.Source == null)
            {
                Error = ERROR_SOURCE_DATA_IS_NULL;
                return;
            }    

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
                Error = ERROR_INVALID_SOURCE_SIZE;
                return;
            }

            if (!data.IsValidChannel())
            {
                Error = ERROR_INVALID_CHANNEL_ORDER_TYPE;
                return;
            }

            if(!data.IsValidArrayData())
            {
                Error = ERROR_INVALID_DATA_TYPE;
                return;
            }

            /*
            if(!data.ImageFormatIsSupported(context.Id, format, out error))
            {
                if (error != CL_ERROR.SUCCESS)
                    Error = error.ToString();
                else
                    Error = ERROR_CHANNEL_FORMAT_NOT_SUPPORTED;

                return;
            }
            */

            Id = CL.CreateImage(context.Id, Flags, format, desc, data.Source, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }
    }
}