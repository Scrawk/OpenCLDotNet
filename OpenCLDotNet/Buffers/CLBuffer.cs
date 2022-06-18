using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public class CLBuffer : CLMemObject
    {
        public CLBuffer(CLContext context, CLBufferData data)
        {
            Create(context, data);
        }

        public CLContext Context { get; private set; }

        public override string ToString()
        {
            var read_write = "";
            read_write += CanRead ? "T/" : "F/";
            read_write += CanWrite ? "T" : "F";

            return String.Format("[CLBuffer: Id={0}, ContextId={1}, ReadWrite={2}, Error={3}]",
                Id.Value, Context.Id.Value, read_write, Error);
        }

        private void Create(CLContext context, CLBufferData data)
        {
            ResetErrorCode();
            Context = context;
            Flags = data.Flags;
            MemType = CL_MEM_OBJECT_TYPE.BUFFER;

            CL_ERROR error;
            Id = CL.CreateBuffer(context.Id, Flags, data.Source, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }
        }

    }
}
