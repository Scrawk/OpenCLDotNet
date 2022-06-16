using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    public readonly record struct CLImageFormatKey
    {
        public readonly cl_context Context { get; init; }

        public readonly CL_MEM_FLAGS Flags { get; init; }

        public readonly CL_MEM_OBJECT_TYPE MemType { get; init; }

        public CLImageFormatKey(cl_context context, CL_MEM_FLAGS flags, CL_MEM_OBJECT_TYPE type)
        {
            Context = context;
            Flags = flags;
            MemType = type;
        }

        public override string ToString()
        {
            return String.Format("[CLImageFormatKey: ContextId={0}, Flags={1}, MemType={2}]",
                Context.Value, Flags, MemType);
        }
    }
}
