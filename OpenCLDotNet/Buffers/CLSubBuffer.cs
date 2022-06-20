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
    public class CLSubBuffer : CLMemObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="region"></param>
        public CLSubBuffer(CLBuffer buffer, CLBufferRegion region) 
            : base(buffer.Context, buffer.Source)
        {
            CL_MEM_FLAGS flags = 0;

            if (buffer.CanReadWrite)
                flags |= CL_MEM_FLAGS.READ_WRITE;
            else if (buffer.CanRead)
                flags |= CL_MEM_FLAGS.READ_ONLY;
            else if (buffer.CanWrite)
                flags |= CL_MEM_FLAGS.WRITE_ONLY;

            Create(buffer, region, flags);
        }

        /// <summary>
        /// 
        /// </summary>
        public CLBuffer Buffer { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public CLBufferRegion Region { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public override bool IsValid => Buffer.IsValid;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var read_write = "";
            read_write += CanRead ? "T/" : "F/";
            read_write += CanWrite ? "T" : "F";

            string region = "{" + Region.Origion + ", " + Region.Size + "}";

            return String.Format("[CLSubBuffer: BufferId={0}, ReadWrite={1}, Region={2}, Error={3}]",
                Buffer.Id, read_write, region, Error);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="region"></param>
        /// <param name="flags"></param>
        private void Create(CLBuffer buffer, CLBufferRegion region, CL_MEM_FLAGS flags)
        {
            ResetErrorCode();
            Buffer = buffer;
            Flags = flags;
            MemType = CL_MEM_OBJECT_TYPE.BUFFER;
            Region = region;

            CL_ERROR error;
            Id = CL.CreateSubBuffer(buffer.Id, flags, region, out error);

            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }

            SetErrorCodeToSuccess();
        }


    }
}
