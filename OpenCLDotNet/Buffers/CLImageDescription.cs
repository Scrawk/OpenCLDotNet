using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    public readonly struct CLImageDescription
    {
        public readonly CL_MEM_OBJECT_TYPE Type { get; init; }

        public readonly size_t Width { get; init; }

        public readonly size_t Height { get; init; }

        public readonly size_t Depth { get; init; }

        public readonly size_t ArraySize { get; init; }

        public readonly size_t RowPitch { get; init; }

        public readonly size_t SlicePitch { get; init; }

        public readonly cl_uint NumMipLevels { get; init; }

        public readonly cl_uint NumSamples { get; init; }

        public readonly cl_mem Buffer { get; init; }

        public override string ToString()
        {
            return String.Format("[CLImageDescription: Type={0}, Width={1}, Height={2}, Depth={3}]",
                Type, Width, Height, Depth);
        }
    }
}
