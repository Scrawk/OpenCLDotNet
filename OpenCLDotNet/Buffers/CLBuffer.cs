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
    public class CLBuffer : CLMemObject
    {
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="rw"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        public CLBuffer(CLContext context, CL_READ_WRITE rw, CL_MEM_DATA_TYPE type, uint length)
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
        public CLBuffer(CLContext context, CL_READ_WRITE rw, Array data)
            : base(context)
        {
            Create(context, rw, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="data"></param>
        public CLBuffer(CLContext context, CL_MEM_FLAGS flags, Array data)
            : base(context)
        {
            Create(context, flags, data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="flags"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        public CLBuffer(CLContext context, CL_MEM_FLAGS flags, CL_MEM_DATA_TYPE type, uint length)
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

            return String.Format("[CLBuffer: Id={0}, ContextId={1}, ReadWrite={2}, Error={3}]",
                Id, Context.Id, read_write, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public static CLBuffer CreateReadBuffer(CLContext context, Array source)
        {
            return new CLBuffer(context, CL_READ_WRITE.READ, source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="type"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static CLBuffer CreateWriteBuffer(CLContext context, CL_MEM_DATA_TYPE type, uint length)
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
        private void Create(CLContext context, CL_READ_WRITE rw, CL_MEM_DATA_TYPE type, uint length)
        {
            ResetErrorCode();
            Flags = CreateFlags(rw);
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
        private void Create(CLContext context, CL_MEM_FLAGS flags, CL_MEM_DATA_TYPE type, uint length)
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
        private void Create(CLContext context, CL_READ_WRITE rw, Array data)
        {
            if(data == null)
            {
                Error = ERROR_SOURCE_DATA_IS_NULL;
                return;
            }

            ResetErrorCode();
            Flags = CreateFlags(rw);
            MemType = CL_MEM_OBJECT_TYPE.BUFFER;
            DataType = CL.TypeOf(data);
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
        private void Create(CLContext context, CL_MEM_FLAGS flags, Array data)
        {
            if (data == null)
            {
                Error = ERROR_SOURCE_DATA_IS_NULL;
                return;
            }

            ResetErrorCode();
            Flags = flags;
            MemType = CL_MEM_OBJECT_TYPE.BUFFER;
            DataType = CL.TypeOf(data);
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
        /// <param name="cmd"></param>
        /// <param name="offset"></param>
        /// <param name="dst"></param>
        /// <param name="blocking"></param>
        /// <exception cref="InvalidObjectExeception"></exception>
        public void Read(CLCommandQueue cmd, uint offset, Array dst, bool blocking)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Buffer is not valid.");

            CheckCommand(cmd);
            CheckData(this, dst, offset);

            cl_event e;
            Error = CL.EnqueueReadBuffer(cmd.Id, Id, blocking, offset,
                      ByteSize, dst, DataType, 0, null, out e).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="offset"></param>
        /// <param name="src"></param>
        /// <param name="blocking"></param>
        /// <exception cref="InvalidObjectExeception"></exception>
        public void Write(CLCommandQueue cmd, uint offset, Array src, bool blocking)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Buffer is not valid.");

            CheckCommand(cmd);
            CheckData(this, src, offset);

            cl_event e;
            Error = CL.EnqueueWriteBuffer(cmd.Id, Id, blocking, offset,
                      ByteSize, src, DataType, 0, null, out e).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="src_offset"></param>
        /// <param name="dst_offset"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="InvalidObjectExeception"></exception>
        public CLBuffer Copy(CLCommandQueue cmd, uint src_offset, uint dst_offset, uint size)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Buffer is not valid.");

            var dst = new CLBuffer(Context, Flags, DataType, size);
            if (!dst.IsValid)
                throw new InvalidObjectExeception("Dst buffer is not valid.");

            Copy(cmd, dst, src_offset, dst_offset, size);

            return dst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dst"></param>
        /// <param name="src_offset"></param>
        /// <param name="dst_offset"></param>
        /// <param name="size"></param>
        /// <exception cref="InvalidObjectExeception"></exception>
        public void Copy(CLCommandQueue cmd, CLBuffer dst, uint src_offset, uint dst_offset, uint size)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Buffer is not valid.");

            if (!dst.IsValid)
                throw new InvalidObjectExeception("Dst buffer is not valid.");

            CheckCommand(cmd);
            CheckOffset(this, src_offset, size);
            CheckOffset(dst, dst_offset, size);

            cl_event e;
            var error = CL.EnqueueCopyBuffer(cmd.Id, Id, dst.Id, src_offset, dst_offset, size,
                0, null, out e);

            Error = error.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="pattern"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <exception cref="InvalidObjectExeception"></exception>
        public void Fill(CLCommandQueue cmd, Array pattern, uint offset, uint size)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Buffer is not valid.");

            CheckCommand(cmd);
            CheckOffset(this, offset, size);

            var type = CL.TypeOf(pattern);
            var size_of = CL.SizeOf(type);
            uint byte_size = (uint)pattern.Length * size_of;
            offset *= size_of;
            size *= size_of;

            cl_event e;
            CL.EnqueueFillBuffer(cmd.Id, Id, pattern, byte_size, offset, size, 0, null, out e);
        }

    }
}
