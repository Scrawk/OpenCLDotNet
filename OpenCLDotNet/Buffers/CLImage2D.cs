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
        /// <param name="rw"></param>
        /// <param name="param"></param>
        public CLImage2D(CLContext context, CLImageParameters2D param, CL_READ_WRITE rw)
            : base(context)
        {
            Create(context, param, rw);
            Region = new CLImageRegion(0, 0, Width, Height);
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
        internal override uint RowPitch => Channels * DataSize * Width;

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
        /// <param name="param"></param>
        /// <returns></returns>
        public static CLImage2D CreateReadImage2D(CLContext context, CLImageParameters2D param)
        {
            return new CLImage2D(context, param, CL_READ_WRITE.READ);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static CLImage2D CreateWriteImage2D(CLContext context, CLImageParameters2D param)
        {
            return new CLImage2D(context, param, CL_READ_WRITE.WRITE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rw"></param>
        /// <param name="data"></param>
        private void Create(CLContext context, CLImageParameters2D data, CL_READ_WRITE rw)
        {
            //source data needs to be null for write only images
            var source = rw == CL_READ_WRITE.WRITE ? null : data.Source;

            CL_ERROR error;
            ResetErrorCode();
            Width = data.Width;
            Height = data.Height;
            Channels = data.Channels;
            ChannelOrder = data.ChannelOrder;
            ChannelType = data.ChannelType;
            Flags = CreateFlags(rw);
            MemType = CL_MEM_OBJECT_TYPE.IMAGE2D;
            DataType = data.DataType;
            DataSize = CL.SizeOf(DataType);
            Length = source == null ? data.DataLength : (uint)source.Length;

            if (!data.IsValidSize())
            {
                Error = ERROR_INVALID_SOURCE_SIZE;
                return;
            }

            if (!data.IsValidChannel())
            {
                Error = ERROR_INVALID_CHANNEL_ORDER_TYPE;
                return;
            }

            if(source == null)
            {
                if (!data.IsValidDataType())
                {
                    Error = ERROR_INVALID_DATA_TYPE;
                    return;
                }
            }
            else
            {
                if (!data.IsValidArrayData())
                {
                    Error = ERROR_INVALID_DATA_TYPE;
                    return;
                }
            }

            var format = data.CreateImageFormat();
            var desc = data.CreateImageDescription();

            Id = CL.CreateImage(context.Id, Flags, format, desc, ByteSize, source, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }

    }
}