﻿using System;
using System.Collections.Generic;
using System.Text;

using OpenCLDotNet.Core;
using OpenCLDotNet.Utility;

namespace OpenCLDotNet.Buffers
{
    public class CLBuffer : CLMemObject
    {
        public CLBuffer(CLContext context, float[] data)
        {
            var flags = CL_MEM_FLAGS.READ_WRITE | CL_MEM_FLAGS.COPY_HOST_PTR;
            Create(context, flags, data);
        }

        public CLBuffer(CLContext context, CL_MEM_FLAGS flags, float[] data) 
        {
            Create(context, flags, data);
        }

        public CLContext Context { get; private set; }

        public override string ToString()
        {
            return String.Format("[CLBuffer: Id={0}, ContextId={1}, CanRead={2}, CanWrite={3}, Error={4}]",
                Id.Value, Context.Id.Value, CanRead, CanWrite, Error);
        }

        private void Create(CLContext context, CL_MEM_FLAGS flags, float[] data)
        {
            Error = "NONE";
            Context = context;
            Flags = flags;

            CL_ERROR error;
            Id = CL.CreateBuffer(context.Id, flags, data, out error);
            if (error != CL_ERROR.SUCCESS)
            {
                Error = error.ToString();
                return;
            }
        }

    }
}
