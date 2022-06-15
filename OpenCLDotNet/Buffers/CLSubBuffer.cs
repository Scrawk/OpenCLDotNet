using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public class CLSubBuffer : CLObject
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

        public CLSubBuffer(CLBuffer buffer, CLBufferRegion region, CL_MEM_FLAGS flags)
        {
            Create(buffer, region, flags);
        }

        public cl_mem Id { get; private set; }

        public string Error { get; private set; }

        public CLBuffer Buffer { get; private set; }

        public CL_MEM_FLAGS Flags { get; private set; }

        public CLBufferRegion Region { get; private set; }

        public override string ToString()
        {
            string region = "{" + Region.Origion + ", " + Region.Size + "}";

            return String.Format("[CLSubBuffer: Id={0}, BufferId={1}, Region={2}, Error={3}]",
                Id.Value, Buffer.Id.Value, region, Error);
        }

        private void Create(CLBuffer buffer, CLBufferRegion region, CL_MEM_FLAGS flags)
        {
            Error = "NONE";
            Buffer = buffer;
            Flags = flags;
            Region = region;

            CL_ERROR error;
            Id = CL.CreateSubBuffer(buffer.Id, flags, region, out error);

            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }
        }

        public override void Print(StringBuilder builder)
        {
            builder.AppendLine(ToString());
        }

        protected override void Release()
        {
            CL.ReleaseMemObject(Id);
        }
    }
}
