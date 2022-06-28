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

            return String.Format("[CLImage2D: Id={0}, ContextId={1}, Width={2}, Height={3}, Channels={4}, ReadWrite={5}, DataType={6}, Error={7}]",
                Id, Context.Id, Width, Height, Channels, read_write, DataType, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="channels"></param>
        /// <returns></returns>
        public static CLImage2D CreateFloatImage2D(CLContext context, uint width, uint height, uint channels)
        {
            var param = CLImageParameters2D.FloatImage(width, height, channels);
            return new CLImage2D(context, param, CL_READ_WRITE.READ_WRITE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static CLImage2D CreateImage2D(CLContext context, CLImageParameters2D param)
        {
            return new CLImage2D(context, param, CL_READ_WRITE.READ_WRITE);
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
        /// <param name="param"></param>
        private void Create(CLContext context, CLImageParameters2D param, CL_READ_WRITE rw)
        {
            Flags = CreateImageFlags(rw, param.HasSource);
            Create(Context, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="param"></param>
        /// <param name="flags"></param>
        private void Create(CLContext context, CLImageParameters2D param, CL_MEM_FLAGS flags)
        {
            if (param.HasSource)
            {
                flags |= CL_MEM_FLAGS.COPY_HOST_PTR;
                flags &= ~CL_MEM_FLAGS.ALLOC_HOST_PTR;
            }
            else
            {
                flags &= ~CL_MEM_FLAGS.COPY_HOST_PTR;
                flags |= CL_MEM_FLAGS.ALLOC_HOST_PTR;
            }
                
            Flags = flags;
            Create(Context, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="param"></param>
        private void Create(CLContext context, CLImageParameters2D param)
        {
            ResetErrorCode();
            Width = param.Width;
            Height = param.Height;
            Channels = param.Channels;
            ChannelOrder = param.ChannelOrder;
            ChannelType = param.ChannelType;
            MemType = CL_MEM_OBJECT_TYPE.IMAGE2D;
            DataType = param.DataType;
            DataSize = CL.SizeOf(DataType);
            Length = param.SourceLength;

            if (!param.IsValidSize())
            {
                Error = ERROR_INVALID_SOURCE_SIZE;
                return;
            }

            if (param.CheckChannelData)
            {
                if (!param.IsValidChannel())
                {
                    Error = ERROR_INVALID_CHANNEL_ORDER_TYPE;
                    return;
                }

                if (!param.IsValidDataType())
                {
                    Error = ERROR_INVALID_DATA_TYPE;
                    return;
                }
            }

            var format = param.CreateImageFormat();
            var desc = param.CreateImageDescription();
            CL_ERROR error;

            Id = CL.CreateImage(context.Id, Flags, format, desc, ByteSize, param.Source, out error);
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
        /// <returns></returns>
        public CLImage2D Copy(CLCommand cmd)
        {
            return Copy(cmd, new CLPoint3t(), Region);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="src_origin"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public CLImage2D Copy(CLCommand cmd, CLPoint3t src_origin, CLRegion3t region)
        {
            CheckImage(this);

            var param = new CLImageParameters2D(this);
            param.Width = (uint)region.Size.x;
            param.Height = (uint)region.Size.y;

            var dst = new CLImage2D(Context, param, Flags);
            CheckImage(dst);

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
        public void Copy(CLCommand cmd, CLImage2D dst, CLPoint3t src_origin, CLRegion3t region)
        {
            CheckCommand(cmd);
            CheckImage(this);
            CheckImage(dst);
            CheckRegion(this, new CLRegion3t(src_origin + region.Origion, region.Size));
            CheckRegion(dst, region);

            cl_event e;
            var error = CL.EnqueueCopyImage(cmd.Id, Id, dst.Id, src_origin, region,
                0, null, out e);

            Error = error.ToString();
        }

    }
}