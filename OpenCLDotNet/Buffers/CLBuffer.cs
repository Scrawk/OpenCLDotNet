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
            ElementSize = CL.SizeOf(type);
           
            CL_ERROR error;
            Id = CL.CreateBuffer(context.Id, Flags, ByteSize, null, DataType, out error);
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
            ElementSize = CL.SizeOf(DataType);
            Length = (uint)data.Length;
            CL_ERROR error = CL_ERROR.NONE;

            Id = CL.CreateBuffer(context.Id, Flags, ByteSize, data, DataType, out error);
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
        /// <param name="data"></param>
        /// <param name="blocking"></param>
        /// <exception cref="InvalidObjectExeception"></exception>
        public void ReadBuffer(CLCommandQueue cmd, Array data, bool blocking)
        {
            if (!IsValid)
                throw new InvalidObjectExeception("Buffer is not valid.");

            CheckCommand(cmd);
            CheckData(data);

            cl_event e;
            Error = CL.EnqueueReadBuffer(cmd.Id, Id, blocking, 0,
                      ByteSize, data, DataType, 0, null, out e).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidObjectExeception"></exception>
        private void CheckCommand(CLCommandQueue cmd)
        {
            if (cmd == null)
                throw new NullReferenceException("Command is null.");

            if (!cmd.IsValid)
                throw new InvalidObjectExeception("Buffer is not valid.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="InvalidDataSizeExeception"></exception>
        /// <exception cref="InvalidDataTypeExeception"></exception>
        private void CheckData(Array data)
        {
            if (data == null)
                throw new NullReferenceException("Data is null.");

            if (data.Length != Length)
                throw new InvalidDataSizeExeception($"Data length was {data.Length} when length {Length} was expected.");

            if (DataType != CL.TypeOf(data))
                throw new InvalidDataTypeExeception($"Data type is {CL.TypeOf(data)} when type {DataType} was expected."); ;
        }

    }
}
