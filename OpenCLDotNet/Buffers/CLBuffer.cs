using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Events;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    /// <summary>
    /// A buffer object stores a one-dimensional collection of elements. 
    /// Elements of a buffer object can be a scalar data type (such as an int, float), 
    /// vector data type, or a user-defined structure.
    /// </summary>
    public class CLBuffer : CLMemObject
    {
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rw"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        public CLBuffer(CLContext context, CL_READ_WRITE rw, CL_DATA_TYPE type, uint length)
            : base(context)
        {
            Create(context, rw, type, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rw"></param>
        /// <param name="data"></param>
        /// <param name="type"></param>
        public CLBuffer(CLContext context, CL_READ_WRITE rw, Array data, CL_DATA_TYPE type)
            : base(context)
        {
            Create(context,rw, data, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="data"></param>
        /// <param name="type"></param>
        public CLBuffer(CLContext context, CL_MEM_FLAGS flags, Array data, CL_DATA_TYPE type)
            : base(context)
        {
            Create(context, flags, data, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        public CLBuffer(CLContext context, CL_MEM_FLAGS flags, CL_DATA_TYPE type, uint length)
        : base(context)
        {
            Create(context, flags, type, length);
        }

        /// <summary>
        /// 
        /// </summary>
        internal override uint RowPitch => 0;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var read_write = "";
            read_write += CanRead ? "T/" : "F/";
            read_write += CanWrite ? "T" : "F";

            return String.Format("[CLBuffer: Id={0}, ContextId={1}, ReadWrite={2}, DataType={3}, Error={4}]",
                Id, Context.Id, read_write, DataType, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static CLBuffer CreateFloatBuffer(CLContext context, uint length)
        {
            return new CLBuffer(context, CL_READ_WRITE.READ_WRITE, CL_DATA_TYPE.FLOAT, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static CLBuffer CreateBuffer(CLContext context, CL_DATA_TYPE type, uint length)
        {
            return new CLBuffer(context, CL_READ_WRITE.READ_WRITE, type, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="source"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CLBuffer CreateReadBuffer(CLContext context, Array source, CL_DATA_TYPE type)
        {
            return new CLBuffer(context, CL_READ_WRITE.READ, source, type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static CLBuffer CreateWriteBuffer(CLContext context, CL_DATA_TYPE type, uint length)
        {
            return new CLBuffer(context, CL_READ_WRITE.WRITE, type, length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rw"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        private void Create(CLContext context, CL_READ_WRITE rw, CL_DATA_TYPE type, uint length)
        {
            ResetErrorCode();
            Flags = CreateBufferFlags(rw, false);
            MemType = CL_MEM_OBJECT_TYPE.BUFFER;
            Length = length;
            DataType = type;
            DataSize = CL.SizeOf(type);
           
            CL_ERROR error;
            Id = CL.CreateBuffer(context.Id, Flags, ByteSize, null, out error);
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
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        private void Create(CLContext context, CL_MEM_FLAGS flags, CL_DATA_TYPE type, uint length)
        {
            ResetErrorCode();
            Flags = flags;
            MemType = CL_MEM_OBJECT_TYPE.BUFFER;
            Length = length;
            DataType = type;
            DataSize = CL.SizeOf(type);

            CL_ERROR error;
            Id = CL.CreateBuffer(context.Id, Flags, ByteSize, null, out error);
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
        /// <param name="context"></param>
        /// <param name="rw"></param>
        /// <param name="data"></param>
        /// <param name="type"></param>
        private void Create(CLContext context, CL_READ_WRITE rw, Array data, CL_DATA_TYPE type)
        {
            ResetErrorCode();
            Flags = CreateBufferFlags(rw, true);
            MemType = CL_MEM_OBJECT_TYPE.BUFFER;
            DataType = type;
            DataSize = CL.SizeOf(DataType);
            Length = (uint)data.Length;
            CL_ERROR error = CL_ERROR.NONE;

            Id = CL.CreateBuffer(context.Id, Flags, ByteSize, data, out error);
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
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="data"></param>
        /// <param name="type"></param>
        private void Create(CLContext context, CL_MEM_FLAGS flags, Array data, CL_DATA_TYPE type)
        {
            if (data == null)
            {
                Error = ERROR_SOURCE_DATA_IS_NULL;
                return;
            }

            ResetErrorCode();
            Flags = flags;
            MemType = CL_MEM_OBJECT_TYPE.BUFFER;
            DataType = type;
            DataSize = CL.SizeOf(DataType);
            Length = (uint)data.Length;
            CL_ERROR error = CL_ERROR.NONE;

            Id = CL.CreateBuffer(context.Id, Flags, ByteSize, data, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }

        /// <summary>
        /// Enqueue commands to read from a buffer object.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dst">The data to be read to</param>
        /// <param name="blocking">Should the function block until finished or return async.</param>
        public cl_event Read(CLCommand cmd, Array dst, bool blocking = true)
        {
            return Read(cmd, dst, 0, blocking);
        }

        /// <summary>
        /// Enqueue commands to read from a buffer object.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="src_offset">offset is the offset in the source buffer 
        /// object to read from.</param>
        /// <param name="dst">The data to be read to</param>
        /// <param name="blocking">Should the function block until finished or return async.</param>
        public cl_event Read(CLCommand cmd, Array dst, uint src_offset, bool blocking)
        {
            CheckCommand(cmd);
            CheckData(this, dst, src_offset);

            //How many bytes to to offset. 
            uint byte_offset = src_offset * DataSize;
            //The dst length in bytes
            uint byte_size = (uint)dst.Length * DataSize;

            uint wait_list_size = 0;
            cl_event[] wait_list = null;
            cl_event e;

            var error = CL.EnqueueReadBuffer(cmd.Id, Id, blocking, byte_offset, byte_size, dst,
                wait_list_size, wait_list, out e);

            Error = error.ToString();
            return e;
        }

        /// <summary>
        /// Enqueue commands to write to a buffer object from host memory.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="src">The data to be written from.</param>
        /// <param name="blocking">Should the function block until finished or return async.</param>
        public cl_event Write(CLCommand cmd, Array src, bool blocking = true)
        {
            return Write(cmd, src, 0, blocking);
        }

        /// <summary>
        /// Enqueue commands to write to a buffer object from host memory.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="offset">The offset in the buffer object to write to.</param>
        /// <param name="src">The data to be written from.</param>
        /// <param name="blocking">Should the function block until finished or return async.</param>
        public cl_event Write(CLCommand cmd, Array src, uint offset, bool blocking)
        {
            CheckCommand(cmd);
            CheckBuffer(this, offset, (uint)src.Length);
            CheckData(this, src, 0);

            //How many bytes to to offset. 
            uint byte_offset = offset * DataSize;
            //The dst length in bytes
            uint byte_size = (uint)src.Length * DataSize;

            uint wait_list_size = 0;
            cl_event[] wait_list = null;
            cl_event e;

            var error = CL.EnqueueWriteBuffer(cmd.Id, Id, blocking, byte_offset, byte_size, src, 
                wait_list_size, wait_list, out e);

            Error = error.ToString();
            return e;
        }

        /// <summary>
        /// Enqueues a command to copy a buffer object to another buffer object.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>The copied buffer.</returns>
        public CLBuffer Copy(CLCommand cmd)
        {
            var dst = new CLBuffer(Context, Flags, DataType, Length);
            Copy(cmd, dst, 0, dst.Length);
            return dst;
        }

        /// <summary>
        /// Enqueues a command to copy a buffer object to another buffer object.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dst">The buffer to copy to.</param>
        /// <param name="src_offset">The offset where to begin copying data from src_buffer.</param>
        /// <param name="dst_size">The size to copy into the dst buffer.</param>
        public cl_event Copy(CLCommand cmd, CLBuffer dst, uint src_offset, uint dst_size)
        {
            dst_size = Math.Min(dst_size, dst.Length);

            CheckCommand(cmd);
            CheckBuffer(this, src_offset, dst_size);
            CheckBuffer(dst, 0, dst_size);

            //The dst size in bytes
            uint byte_size = dst_size * DataSize;  
            //The offset into source buffer in bytes
            uint byte_offset = src_offset * DataSize;

            uint wait_list_size = 0;
            cl_event[] wait_list = null;
            cl_event e;

            var error = CL.EnqueueCopyBuffer(cmd.Id, Id, dst.Id, byte_offset, byte_size, 
                wait_list_size, wait_list, out e);

            Error = error.ToString();
            return e;
        }

    }
}
