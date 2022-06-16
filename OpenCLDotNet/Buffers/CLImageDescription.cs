using System;
using System.Collections.Generic;

using OpenCLDotNet.Core;

namespace OpenCLDotNet.Buffers
{
    public record struct CLImageDescription
    {
        public CL_MEM_OBJECT_TYPE MemType;

        public size_t Width;

        public size_t Height;

        public size_t Depth;

        public size_t ArraySize;

        public size_t RowPitch;

        public size_t SlicePitch;

        public cl_uint NumMipLevels;

        public cl_uint NumSamples;

        public cl_mem Buffer;

        public override string ToString()
        {
            return String.Format("[CLImageDescription: MemType={0}, Width={1}, Height={2}, Depth={3}]",
                MemType, Width, Height, Depth);
        }
    }
}
