using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public class CLSubBuffer : CLMemObject
    {
        public CLSubBuffer(CLBuffer buffer, CLBufferRegion region)
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

        public CLBuffer Buffer { get; private set; }

        public CLBufferRegion Region { get; private set; }

        public override bool IsValid => Buffer.IsValid;

        public override string ToString()
        {
            var read_write = "";
            read_write += CanRead ? "T/" : "F/";
            read_write += CanWrite ? "T" : "F";

            string region = "{" + Region.Origion + ", " + Region.Size + "}";

            return String.Format("[CLSubBuffer: BufferId={0}, ReadWrite={1}, Region={2}, Error={3}]",
                Buffer.Id, read_write, region, Error);
        }

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
        }


    }
}
