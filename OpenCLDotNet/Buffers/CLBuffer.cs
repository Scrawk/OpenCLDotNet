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
    public class CLBuffer : CLMemObject
    {
  

        public CLBuffer(CLContext context, CL_READ_WRITE rw, CL_MEM_DATA_TYPE type, uint length)
            : base(context)
        {
            Create(context, rw, type, length);
        }

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


        private void Create(CLContext context, CL_READ_WRITE rw, CL_MEM_DATA_TYPE type, uint length)
        {
            ResetErrorCode();
            Flags = CreateFlags(rw);
            MemType = CL_MEM_OBJECT_TYPE.BUFFER;
            Length = length;
            DataType = type;
            ElementSize = CL.SizeOf(type);
           
            CL_ERROR error;
            Id = CL.CreateBuffer(context.Id, Flags, ByteSize, null, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }

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

            var fdata = data as float[];
            SetSource(data as float[]);

            CL_ERROR error;
            Id = CL.CreateBuffer(context.Id, Flags, ByteSize, fdata, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }

        private CL_MEM_FLAGS CreateFlags(CL_READ_WRITE rw)
        {
            CL_MEM_FLAGS flag = 0;

            switch (rw)
            {
                case CL_READ_WRITE.WRITE:
                    flag = CL_MEM_FLAGS.WRITE_ONLY;
                    //flag |= CL_MEM_FLAGS.HOST_WRITE_ONLY;
                    flag |= CL_MEM_FLAGS.ALLOC_HOST_PTR;
                    break;

                case CL_READ_WRITE.READ:
                    flag = CL_MEM_FLAGS.READ_ONLY;
                    flag |= CL_MEM_FLAGS.HOST_READ_ONLY;
                    flag |= CL_MEM_FLAGS.COPY_HOST_PTR;
                    break;
            }

            return flag;
        }

    }
}
