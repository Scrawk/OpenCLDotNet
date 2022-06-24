using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Events;
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
            Region = new CLRegion3t(0, 0, Width, Height);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="param"></param>
        /// <param name="flags"></param>
        public CLImage2D(CLContext context, CLImageParameters2D param, CL_MEM_FLAGS flags)
            : base(context)
        {
            Create(context, param, flags);
            Region = new CLRegion3t(0, 0, Width, Height);
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

            Flags = CreateFlags(rw);
            Create(Context, data, source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="data"></param>
        /// <param name="flags"></param>
        private void Create(CLContext context, CLImageParameters2D data, CL_MEM_FLAGS flags)
        {
            //source data needs to be null for write only images
            var source = flags.HasFlag(CL_MEM_FLAGS.WRITE_ONLY) ? null : data.Source;

            Flags = flags;
            Create(Context, data, source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="data"></param>
        /// <param name="source"></param>
        private void Create(CLContext context, CLImageParameters2D data, Array source)
        {
            ResetErrorCode();
            Width = data.Width;
            Height = data.Height;
            Channels = data.Channels;
            ChannelOrder = data.ChannelOrder;
            ChannelType = data.ChannelType;
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

            if (source == null)
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
            CL_ERROR error;

            Id = CL.CreateImage(context.Id, Flags, format, desc, ByteSize, source, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="src_origin"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        /// <exception cref="InvalidObjectExeception"></exception>
        public CLImage2D Copy(CLCommandQueue cmd, CLPoint3t src_origin, CLRegion3t region)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Image is not valid.");

            var param = new CLImageParameters2D(this);
            param.Width = (uint)region.Size.x;
            param.Height = (uint)region.Size.y;

            var dst = new CLImage2D(Context, param, Flags);
            if (!dst.IsValid)
                throw new InvalidObjectExeception("Dst image is not valid.");

            Copy(cmd, dst, src_origin, region);

            return dst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dst"></param>
        /// <param name="src_origin"></param>
        /// <param name="region"></param>
        /// <exception cref="InvalidObjectExeception"></exception>
        public void Copy(CLCommandQueue cmd, CLImage2D dst, CLPoint3t src_origin, CLRegion3t region)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Image is not valid.");

            if (!dst.IsValid)
                throw new InvalidObjectExeception("Dst image is not valid.");

            CheckCommand(cmd);
            CheckRegion(this, new CLRegion3t(src_origin + region.Origion, region.Size));
            CheckRegion(dst, region);

            cl_event e;
            var error = CL.EnqueueCopyImage(cmd.Id, Id, dst.Id, src_origin, region,
                0, null, out e);

            Error = error.ToString();
        }


    }
}